/****************/
// Mã sinh viên: 202418947
// Họ tên:     Đặng Ngọc Minh
// Bài thực hành: Lab 03-04 - Nạp chồng (Overloading) - Nhóm dự án và nhân sự
/****************/

using System;

namespace ProjectTeamApp
{
    /// <summary>
    /// Lớp Nhân sự (cơ sở).
    /// Trách nhiệm: lưu thông tin nhân sự, tăng lương (nạp chồng),
    /// tính chi phí tháng và hiển thị thông tin ở dạng đa hình (virtual).
    /// Quan hệ: là lớp cha của SoftwareEngineer; được ProjectTeam tham chiếu (kết tập, không sở hữu).
    /// </summary>
    public class Employee
    {
        // ----- Thuộc tính -----
        private string id;            // Mã nhân sự (bất biến: không rỗng)
        private string fullName;      // Họ tên  (bất biến: không rỗng)
        private double baseSalary;    // Lương cơ bản (bất biến: không âm)

        // ----- Constructor không tham số -----
        public Employee()
            : this("UNKNOWN", "Unnamed employee", 0)
        {
        }

        // ----- Constructor 2 tham số -----
        public Employee(string id, string fullName)
            : this(id, fullName, 0)
        {
        }

        // ----- Constructor đầy đủ: kiểm tra bất biến -----
        public Employee(string id, string fullName, double baseSalary)
        {
            if (string.IsNullOrWhiteSpace(id))
                throw new ArgumentException("Mã nhân sự không được rỗng.");
            if (string.IsNullOrWhiteSpace(fullName))
                throw new ArgumentException("Họ tên không được rỗng.");
            if (baseSalary < 0)
                throw new ArgumentException("Lương cơ bản không được âm.");

            this.id = id;
            this.fullName = fullName;
            this.baseSalary = baseSalary;
        }

        // ----- Getter -----
        public string Id => id;
        public string FullName => fullName;
        public double BaseSalary => baseSalary;

        // ----- Nạp chồng 1: tăng một số tiền cố định -----
        // Ràng buộc theo đề bài: giá trị tăng phải dương.
        public void IncreaseSalary(double amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Số tiền tăng phải dương (> 0).");
            baseSalary += amount;
        }

        // ----- Nạp chồng 2: tăng theo tỷ lệ % nếu byPercentage == true, ngược lại tăng cố định -----
        // Ràng buộc theo đề bài: giá trị tăng phải dương.
        public void IncreaseSalary(double value, bool byPercentage)
        {
            if (value <= 0)
                throw new ArgumentException("Giá trị tăng phải dương (> 0).");
            if (byPercentage)
                baseSalary += baseSalary * value / 100.0;   // tăng theo phần trăm
            else
                baseSalary += value;                        // tăng cố định
        }

        // ----- Tính chi phí tháng: mặc định bằng lương cơ bản -----
        public virtual double CalculateMonthlyCost()
        {
            return baseSalary;
        }

        // ----- Hiển thị thông tin: virtual để lớp con ghi đè -----
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"[Employee] {id} - {fullName} | Lương CB: {baseSalary:N0} VND");
        }

        // ----- Destructor (finalizer): quan sát vòng đời đối tượng;
        //       finalizer của lớp dẫn xuất chạy trước rồi mới tới lớp cơ sở -----
        ~Employee()
        {
            Console.WriteLine($"[DESTRUCTOR] ~Employee(): {id} - {fullName} bị thu hồi.");
        }
    }
}
