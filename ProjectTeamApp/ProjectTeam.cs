/****************/
// Mã sinh viên: 202418947
// Họ tên:     Đặng Ngọc Minh
// Bài thực hành: Lab 03-04 - Nạp chồng (Overloading) - Nhóm dự án và nhân sự
/****************/

using System;
using System.Collections.Generic;

namespace ProjectTeamApp
{
    /// <summary>
    /// Lớp Nhóm dự án.
    /// Trách nhiệm: quản lý mã/tên dự án, trưởng nhóm và danh sách thành viên.
    /// Quan hệ: KẾT TẬP (aggregation) không sở hữu - chỉ giữ THAM CHIẾU tới Employee;
    ///          nhân sự tồn tại độc lập, có thể thuộc nhiều nhóm.
    /// Bất biến: không trùng mã thành viên; trưởng nhóm phải nằm trong danh sách.
    /// </summary>
    public class ProjectTeam
    {
        // ----- Thuộc tính -----
        private string projectCode;
        private string projectName;
        private Employee? leader;                   // liên kết không sở hữu (null: nhóm chưa có trưởng nhóm)
        private List<Employee> members;             // danh sách liên kết không sở hữu

        // ----- Constructor 1: nhóm chưa có trưởng nhóm -----
        public ProjectTeam(string projectCode, string projectName)
        {
            this.projectCode = projectCode;
            this.projectName = projectName;
            members = new List<Employee>();
        }

        // ----- Constructor 2: thiết lập trưởng nhóm, tự động đưa vào danh sách thành viên -----
        public ProjectTeam(string projectCode, string projectName, Employee leader)
            : this(projectCode, projectName)
        {
            this.leader = leader;
            members.Add(leader);
        }

        // ----- Getter -----
        public string ProjectCode => projectCode;
        public string ProjectName => projectName;
        public Employee? Leader => leader;
        public IReadOnlyList<Employee> Members => members;

        // ----- Nạp chồng 1: thêm thành viên thường -----
        public bool AddMember(Employee employee)
        {
            return AddMember(employee, false);
        }

        // ----- Nạp chồng 2: thêm thành viên, có thể đặt làm trưởng nhóm -----
        public bool AddMember(Employee employee, bool makeLeader)
        {
            // Bất biến: không thêm trùng nhân sự trong cùng một nhóm
            if (Contains(employee.Id))
                return false;

            members.Add(employee);

            if (makeLeader)
                leader = employee;   // trưởng nhóm cũ vẫn giữ vai trò thành viên

            return true;
        }

        // ----- Kiểm tra mã nhân sự có trong nhóm -----
        public bool Contains(string employeeId)
        {
            foreach (var e in members)
                if (e.Id == employeeId)
                    return true;
            return false;
        }

        // ----- Xóa thành viên theo mã -----
        // Ràng buộc: không được xóa trưởng nhóm khi chưa có trưởng nhóm thay thế
        public bool RemoveMember(string employeeId)
        {
            if (leader != null && leader.Id == employeeId)
                return false;     // từ chối xóa trưởng nhóm hiện tại

            return members.RemoveAll(e => e.Id == employeeId) > 0;
        }

        // ----- Đổi trưởng nhóm -----
        // Ràng buộc: trưởng nhóm mới phải được thêm vào nhóm nếu chưa là thành viên
        public bool ChangeLeader(Employee employee)
        {
            if (!Contains(employee.Id))
                members.Add(employee);

            leader = employee;
            return true;
        }

        // ----- Tổng chi phí nhân sự hàng tháng (gọi đa hình) -----
        public double CalculateTotalMonthlyCost()
        {
            double total = 0;
            foreach (var e in members)
                total += e.CalculateMonthlyCost();   // đa hình: gọi phiên bản của lớp thực
            return total;
        }

        // ----- Hiển thị danh sách bằng lời gọi đa hình (tên theo đề bài: displayTeam) -----
        public void DisplayTeam()
        {
            Console.WriteLine($"=== Nhóm {projectCode} - {projectName} ===");
            Console.WriteLine($"Trưởng nhóm: {(leader != null ? leader.FullName + " (" + leader.Id + ")" : "(chưa có)")}");
            foreach (var e in members)
                e.DisplayInfo();     // đa hình
        }

        // ----- Destructor: CHỈ hủy cấu trúc nội bộ, KHÔNG hủy các đối tượng Employee -----
        ~ProjectTeam()
        {
            members.Clear();         // nhả tham chiếu, không gọi hủy đối tượng nhân sự
            Console.WriteLine($"[DESTRUCTOR] ~ProjectTeam(): {projectCode} bị thu hồi (nhân sự vẫn tồn tại).");
        }
    }
}
