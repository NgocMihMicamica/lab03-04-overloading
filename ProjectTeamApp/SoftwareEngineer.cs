/****************/
// Mã sinh viên: 202418947
// Họ tên:     Đặng Ngọc Minh
// Bài thực hành: Lab 03-04 - Nạp chồng (Overloading) - Nhóm dự án và nhân sự
/****************/

using System;

namespace ProjectTeamApp
{
    /// <summary>
    /// Lớp Kỹ sư phần mềm, kế thừa công khai từ Employee.
    /// Bổ sung: ngôn ngữ lập trình chính và phụ cấp kỹ thuật.
    /// Ghi đè CalculateMonthlyCost() và DisplayInfo().
    /// </summary>
    public class SoftwareEngineer : Employee
    {
        // ----- Thuộc tính bổ sung -----
        private string primaryLanguage;     // Ngôn ngữ lập trình chính (không rỗng)
        private double technicalAllowance;  // Phụ cấp kỹ thuật (không âm)

        // ----- Constructor 1 -----
        public SoftwareEngineer(string id, string fullName, string primaryLanguage)
            : base(id, fullName)
        {
            Validate(primaryLanguage, 0);
            this.primaryLanguage = primaryLanguage;
            this.technicalAllowance = 0;
        }

        // ----- Constructor 2 (đầy đủ) -----
        public SoftwareEngineer(string id, string fullName, double baseSalary,
                                string primaryLanguage, double technicalAllowance)
            : base(id, fullName, baseSalary)
        {
            Validate(primaryLanguage, technicalAllowance);
            this.primaryLanguage = primaryLanguage;
            this.technicalAllowance = technicalAllowance;
        }

        // ----- Kiểm tra bất biến riêng của lớp -----
        private static void Validate(string language, double allowance)
        {
            if (string.IsNullOrWhiteSpace(language))
                throw new ArgumentException("Ngôn ngữ chính không được rỗng.");
            if (allowance < 0)
                throw new ArgumentException("Phụ cấp kỹ thuật không được âm.");
        }

        // ----- Getter -----
        public string PrimaryLanguage => primaryLanguage;
        public double TechnicalAllowance => technicalAllowance;

        // ----- Ghi đè: chi phí tháng = lương CB + phụ cấp -----
        public override double CalculateMonthlyCost()
        {
            return BaseSalary + technicalAllowance;
        }

        // ----- Ghi đè hiển thị -----
        public override void DisplayInfo()
        {
            Console.WriteLine($"[SoftwareEngineer] {Id} - {FullName} | Lương CB: {BaseSalary:N0} VND"
                              + $" | Ngôn ngữ: {primaryLanguage} | Phụ cấp: {technicalAllowance:N0} VND"
                              + $" | Chi phí tháng: {CalculateMonthlyCost():N0} VND");
        }

        // ----- Destructor: in thông báo để quan sát vòng đời đối tượng -----
        ~SoftwareEngineer()
        {
            Console.WriteLine($"[DESTRUCTOR] ~SoftwareEngineer(): {Id} - {FullName} bị thu hồi.");
        }
    }
}
