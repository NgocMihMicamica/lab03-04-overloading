/****************/
// Mã sinh viên: 202418947
// Họ tên:     Đặng Ngọc Minh
// Bài thực hành: Lab 03-04 - Nạp chồng (Overloading) - Nhóm dự án và nhân sự
/****************/

using System;

namespace ProjectTeamApp
{
    /// <summary>
    /// Chương trình kiểm thử theo kịch bản đề bài (mục C).
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // 0. Bảng thông tin bài thực hành
            Console.WriteLine("=========================================================");
            Console.WriteLine(" LAB 03-04 - NAP CHONG (OVERLOADING)");
            Console.WriteLine(" Ma sinh vien: 202418947 - Ho ten: Dang Ngoc Minh");
            Console.WriteLine(" Chu de: Nhom du an va nhan su");
            Console.WriteLine("=========================================================\n");

            // 1. Hai Employee bằng hai constructor khác nhau
            var emp1 = new Employee("E01", "Nguyen Van An");
            var emp2 = new Employee("E02", "Tran Thi Binh", 15000000);

            // 2. Hai SoftwareEngineer bằng hai constructor khác nhau
            var dev1 = new SoftwareEngineer("S01", "Le Van Cuong", "C#");
            var dev2 = new SoftwareEngineer("S02", "Pham Thi Dung", 22000000, "Java", 5000000);

            // 3. Tăng lương cố định
            emp2.IncreaseSalary(3000000);
            Console.WriteLine("[3] Sau khi tang luong co dinh +3.000.000:");
            emp2.DisplayInfo();

            // 4. Tăng lương theo phần trăm
            dev2.IncreaseSalary(10, true);
            Console.WriteLine("\n[4] Sau khi tang luong 10%:");
            dev2.DisplayInfo();

            // 5. Tạo nhóm dự án KHÔNG có trưởng nhóm
            var team1 = new ProjectTeam("PRJ01", "He thong quan ly ban hang");

            // 6. Thêm nhân sự thường
            Console.WriteLine("\n[6] Them E01 vao nhom: " + team1.AddMember(emp1));

            // 7. Thêm kỹ sư và đặt làm trưởng nhóm
            Console.WriteLine("[7] Them S01 va dat lam truong nhom: " + team1.AddMember(dev1, true));

            // 8. Thử thêm lại thành viên đã tồn tại (phải bị từ chối)
            Console.WriteLine("[8] Them lai E01 (da ton tai): " + team1.AddMember(emp1));

            // 9. Hiển thị danh sách bằng lời gọi đa hình
            Console.WriteLine("\n[9] Danh sach nhom (da hinh):");
            team1.DisplayTeam();

            // 10. Tổng chi phí nhân sự hàng tháng
            Console.WriteLine("\n[10] Tong chi phi nhan su hang thang: "
                              + team1.CalculateTotalMonthlyCost().ToString("N0") + " VND");

            // 11. Thử xóa trưởng nhóm hiện tại (phải bị từ chối)
            Console.WriteLine("[11] Xoa truong nhom S01: " + team1.RemoveMember("S01")
                              + " (tu choi vi S01 dang la truong nhom)");

            // 12. Đổi trưởng nhóm rồi xóa người từng là trưởng nhóm
            team1.ChangeLeader(emp2);
            Console.WriteLine("[12a] Doi truong nhom sang E02: " + team1.Leader!.FullName);
            Console.WriteLine("[12b] Xoa S01 (truong nhom cu): " + team1.RemoveMember("S01"));

            // 13 + 14. Nhóm thứ 3 dùng lại nhân sự của nhóm 1 (kết tập nhiều-nhóm),
            //          sau đó hủy nhóm 3 bằng cách kết thúc khối lệnh cục bộ.
            CreateAndDestroyTeam3(emp1);
            GC.Collect();                   // ép thu hồi đối tượng không còn tham chiếu
            GC.WaitForPendingFinalizers();  // chờ finalizer chạy xong
            GC.Collect();

            // 15. Chứng minh nhân sự vẫn tồn tại sau khi nhóm 3 bị hủy
            Console.WriteLine("\n[15] Sau khi nhom 3 bi huy, E01 van ton tai:");
            emp1.DisplayInfo();
            team1.DisplayTeam();

            Console.WriteLine("\nKET THUC CHUONG TRINH (GC se thu hoi cac doi tuong con lai).");
        }

        /// <summary>
        /// Bước 13: tạo nhóm thứ ba và thêm lại một nhân sự đã thuộc nhóm thứ nhất
        ///          (chứng minh quan hệ kết tập: một nhân sự thuộc nhiều nhóm).
        /// Bước 14: nhóm thứ ba và nhân sự tạm thời chỉ tồn tại trong khối lệnh cục bộ này;
        ///          khi phương thức kết thúc, chúng thành đối tượng mồ côi để GC thu hồi
        ///          và destructor in ra thông báo quan sát vòng đời.
        /// </summary>
        static void CreateAndDestroyTeam3(Employee shared)
        {
            var team3 = new ProjectTeam("PRJ03", "Website cong ty");
            Console.WriteLine("\n[13] Nhom 3 them lai E01 (da o nhom 1): " + team3.AddMember(shared));
            team3.DisplayTeam();

            // Nhân sự tạm thời: chỉ tồn tại trong khối lệnh cục bộ này
            var tempIntern = new SoftwareEngineer("S99", "Nguyen Thi Tam", "Python");
            tempIntern.DisplayInfo();

            Console.WriteLine("\n[14] Nhom 3 va nhan su tam thoi nam trong khoi cuc bo, chuan bi huy...");
        }
    }
}
