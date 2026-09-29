# Lab 03-04 — Nạp chồng (Overloading): Nhóm dự án và nhân sự

| Thông tin | Nội dung |
| --- | --- |
| Mã sinh viên | 202418947 |
| Họ và tên | Đặng Ngọc Minh |
| Ngôn ngữ / nền tảng | C# — .NET 8 (Console Application, mã hoá UTF-8) |
| Liên kết GitHub | `https://github.com/<tai-khoan-github>/Lab03-04-Overloading-CSharp` |
| Báo cáo | `docs/BaoCao-Lab03-04.pdf` (PDF 10 trang, sinh từ `docs/BaoCao-Lab03-04.html`) |
| Sơ đồ lớp | `docs/class_diagram.png` (nguồn: `docs/class_diagram.svg` / `.html`) |

## 1. Nội dung bài thực hành

- **Phần A — Phân tích và thiết kế:** xác định ba lớp `Employee`, `SoftwareEngineer`, `ProjectTeam` với trách nhiệm, thuộc tính, constructor, phương thức, quan hệ; nêu các bất biến của mô hình; vẽ sơ đồ lớp.
- **Phần B — Cài đặt:** nạp chồng constructor và phương thức, kế thừa + ghi đè + đa hình (`virtual`/`override`), quan hệ **kết tập không sở hữu** giữa `ProjectTeam` và `Employee`, destructor để quan sát vòng đời đối tượng.
- **Phần C — Kiểm thử:** chương trình `Program.cs` chạy đúng kịch bản 15 bước của đề bài và in kết quả ra màn hình console.

## 2. Cấu trúc thư mục

```
ProjectTeamApp/
├── Employee.cs           # lớp cơ sở: 3 constructor nạp chồng, IncreaseSalary nạp chồng, thành viên ảo
├── SoftwareEngineer.cs   # kế thừa Employee: 2 constructor, ghi đè CalculateMonthlyCost()/DisplayInfo()
├── ProjectTeam.cs        # kết tập không sở hữu: 2 constructor, AddMember() nạp chồng, RemoveMember(),
│                         #   ChangeLeader(), Contains(), CalculateTotalMonthlyCost(), DisplayTeam()
├── Program.cs            # kịch bản kiểm thử 15 bước (mục C)
└── ProjectTeamApp.csproj # .NET 8, ImplicitUsings + Nullable enable
docs/
├── BaoCao-Lab03-04.pdf / .html      # báo cáo bài thực hành (HTML sinh PDF, mở được bằng Word)
├── class_diagram.png / .svg / .html # sơ đồ lớp
└── screenshots/                     # ảnh chụp biên dịch và chạy chương trình
run_output.txt             # kết quả chạy chương trình dạng văn bản
_tools/                    # script hỗ trợ: biên dịch, chạy, chụp ảnh, tạo báo cáo, vẽ sơ đồ
README.md                  # tệp này
```

## 3. Yêu cầu và cách chạy

- Cần **.NET SDK 8.0** trở lên (kiểm tra bằng lệnh `dotnet --version`).
- Biên dịch:

```bash
dotnet build ProjectTeamApp/ProjectTeamApp.csproj
```

- Chạy chương trình kiểm thử:

```bash
dotnet run --project ProjectTeamApp/ProjectTeamApp.csproj
```

- Hoặc vào trực tiếp thư mục dự án rồi chạy:

```bash
cd ProjectTeamApp
dotnet run
```

## 4. Các điểm nạp chồng / ghi đè chính

| Lớp | Constructor nạp chồng | Phương thức nạp chồng / ghi đè |
| --- | --- | --- |
| `Employee` | `Employee()`, `Employee(id, fullName)`, `Employee(id, fullName, baseSalary)` | `IncreaseSalary(amount)` và `IncreaseSalary(value, byPercentage)`; `CalculateMonthlyCost()`, `DisplayInfo()` là `virtual`; destructor `~Employee()` |
| `SoftwareEngineer` | `SoftwareEngineer(id, fullName, primaryLanguage)`, `SoftwareEngineer(id, fullName, baseSalary, primaryLanguage, technicalAllowance)` | ghi đè `CalculateMonthlyCost()` = lương cơ bản + phụ cấp kỹ thuật; ghi đè `DisplayInfo()`; destructor `~SoftwareEngineer()` |
| `ProjectTeam` | `ProjectTeam(projectCode, projectName)`, `ProjectTeam(projectCode, projectName, leader)` | `AddMember(employee)` và `AddMember(employee, makeLeader)`; `RemoveMember()`, `ChangeLeader()`, `Contains()`, `CalculateTotalMonthlyCost()`, `DisplayTeam()`; destructor `~ProjectTeam()` |

## 5. Bất biến của mô hình

1. Mã nhân sự và họ tên không được rỗng.
2. Lương cơ bản không âm; phụ cấp kỹ thuật không âm.
3. Giá trị tăng lương phải dương (cả hai phiên bản `IncreaseSalary`).
4. Ngôn ngữ lập trình chính của kỹ sư phần mềm không được rỗng.
5. Không có hai thành viên cùng mã nhân sự trong một nhóm.
6. Trưởng nhóm phải thuộc danh sách thành viên và không xuất hiện hai lần trong nhóm.
7. Không xoá được trưởng nhóm khi chưa chọn trưởng nhóm thay thế.
8. Huỷ `ProjectTeam` chỉ giải phóng cấu trúc danh sách nội bộ, **không** huỷ các đối tượng `Employee`.

## 6. Kiểm thử theo kịch bản (mục C)

Chương trình `Program.cs` thực hiện đủ 15 bước; các bước được chú thích ngay trong mã nguồn:

| Bước | Nội dung | Kết quả quan sát |
| --- | --- | --- |
| 1 | Tạo 2 `Employee` bằng 2 constructor khác nhau | Hợp lệ, dùng lại ở bước 3, 9, 10, 12, 15 |
| 2 | Tạo 2 `SoftwareEngineer` bằng 2 constructor khác nhau | Hợp lệ, dùng lại ở bước 4, 7, 9 |
| 3 | `IncreaseSalary(3000000)` | Lương 15.000.000 → 18.000.000 VND |
| 4 | `IncreaseSalary(10, true)` | Lương 22.000.000 → 24.200.000 VND; chi phí tháng 29.200.000 VND |
| 5 | Tạo nhóm chưa có trưởng nhóm | `Trưởng nhóm: (chưa có)` |
| 6 | `AddMember(employee)` | `True` |
| 7 | `AddMember(employee, true)` | `True`, S01 là trưởng nhóm |
| 8 | Thêm lại thành viên đã tồn tại | `False` (bị từ chối) |
| 9 | `DisplayTeam()` — gọi đa hình | In đúng loại thực tế của từng nhân sự |
| 10 | `CalculateTotalMonthlyCost()` | 0 VND (lương và phụ cấp đều bằng 0) |
| 11 | Xóa trưởng nhóm hiện tại | `False` (bị từ chối) |
| 12 | Đổi trưởng nhóm rồi xóa người từng là trưởng nhóm | E02 thành trưởng nhóm, xóa S01 thành công |
| 13 | Nhóm thứ ba dùng lại nhân sự của nhóm thứ nhất | `True` — quan hệ kết tập nhiều nhóm |
| 14 | Hủy nhóm thứ ba bằng khối lệnh cục bộ | Ba dòng `[DESTRUCTOR]` được in; E01 không bị hủy |
| 15 | Kiểm tra nhân sự vẫn tồn tại sau khi nhóm bị hủy | E01 vẫn in bình thường và vẫn thuộc PRJ01 |

Kết quả chạy đầy đủ được lưu ở `run_output.txt` và trong Phụ lục của báo cáo.

## 7. Ghi chú kỹ thuật

- Chương trình đặt `Console.OutputEncoding = Encoding.UTF8`; các tệp mã nguồn lưu ở dạng UTF-8 để hiển thị đúng tiếng Việt.
- Destructor của `ProjectTeam` chỉ giải phóng danh sách liên kết nội bộ (`members.Clear()`), không giải phóng các đối tượng `Employee` — đúng yêu cầu về quan hệ kết tập không sở hữu.
- Thư mục `_tools/` chứa các script hỗ trợ (không thuộc phần nộp chính): `cmd/build.cmd`, `cmd/run.cmd`, `capture-run.ps1` + `ConCapture.cs` (chụp ảnh màn hình console), `make-report.ps1` (sinh báo cáo Word/PDF), `make-diagram.mjs` (sinh sơ đồ lớp SVG/HTML/PNG), `install-dotnet.ps1`.
