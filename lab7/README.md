# Quản lý Nhân sự – Công ty Sơn Đông Á

Bài thực hành **Lab 7** – Phân tích thiết kế hệ thống và xây dựng giao diện WinForms cho hệ thống quản lý nhân sự công ty sơn Đông Á.

| | |
|---|---|
| **Sinh viên** | Phạm Long Khoa |
| **MSSV** | 1250080083 |
| **Công nghệ** | C# WinForms, .NET Framework 4.8, SQL Server |
| **Công cụ** | Visual Studio, SQL Server, draw.io |

---

## 1. Giới thiệu

Hệ thống tin học hóa việc quản lý nhân sự của công ty sơn Đông Á, bao gồm:

- Tuyển dụng, thử việc và ký hợp đồng lao động (dài hạn / ngắn hạn).
- Hồ sơ BHYT, BHXH cho nhân viên ký hợp đồng dài hạn.
- Quyết định lương, quyết định chức vụ và phụ cấp.
- Phân công ca làm việc hằng tuần, chấm công, ghi nhận giờ phụ trội (hệ số **1.5**).
- Quyết định khen thưởng / kỷ luật (cá nhân hoặc tập thể phòng ban) và phiếu thu chi tương ứng.
- Tính bảng lương hằng tháng.

Phiên bản này **cài đặt 1 form**: `FrmNhanVien` – quản lý nhân viên và quyết định tuyển dụng thử việc.

---

## 2. Nội dung báo cáo (file `lab7.docx`)

| Phần | Nội dung |
|---|---|
| Yêu cầu hệ thống | Đầu vào – xử lý – xuất kết quả; bảng Actor – Công việc – Quy định; quy tắc nghiệp vụ (BR01–BR11); yêu cầu chức năng; tiêu chuẩn hệ thống |
| Câu 1 | Xác định 16 class, thuộc tính, phương thức và ID |
| Câu 2 | Mối liên kết, bản số giữa các class và Class Diagram |
| Câu 3 | Actor và Use Case tổng quát (UC01–UC08) |
| Câu 4 | Use Case phân rã: từ thử việc đến ký hợp đồng chính thức |
| Câu 5 | Activity Diagram |
| Câu 6 | Sequence Diagram |
| Phần cuối | Giao diện form `FrmNhanVien` |

### 2.1. Các class chính

`NhanVien`, `PhongBan`, `ChucVu`, `QuyetDinhTuyenDung`, `HopDongLaoDong`, `HoSoBaoHiem`, `QuyetDinhLuong`, `QuyetDinhChucVu`, `KeHoachPhanCong`, `ChiTietPhanCong`, `PhieuChamCong`, `PhieuPhuTroi`, `ChiTietPhuTroi`, `BangLuongThang`, `QuyetDinhKhenThuongKyLuat`, `PhieuThuChiKTKL`.

### 2.2. Quan hệ tiêu biểu

- `PhongBan` 1 – N `NhanVien`; mỗi phòng có 1 trưởng phòng.
- `NhanVien` 1 – N `QuyetDinhTuyenDung`, `HopDongLaoDong`, `QuyetDinhLuong`, `QuyetDinhChucVu`, `PhieuChamCong`, `BangLuongThang`.
- `NhanVien` 1 – 0..1 `HoSoBaoHiem` (chỉ với hợp đồng dài hạn).
- `KeHoachPhanCong` 1 – N `ChiTietPhanCong`; `PhieuPhuTroi` 1 – N `ChiTietPhuTroi` (Composition).
- `QuyetDinhKTKL` 1 – 0..1 `PhieuThuChiKTKL`.

### 2.3. Actor

Nhân viên · Phòng Nhân sự · Trưởng phòng / Phân xưởng · Ban chấm công · Kế toán lương / Thu ngân · Ban Giám đốc.

### 2.4. Use Case tổng quát

| Mã | Use Case |
|---|---|
| UC01 | Quản lý tuyển dụng và thử việc |
| UC02 | Quản lý hợp đồng lao động và hồ sơ bảo hiểm |
| UC03 | Quản lý quyết định lương và chức vụ |
| UC04 | Phân công ca làm việc hằng tuần |
| UC05 | Chấm công và ghi nhận giờ phụ trội |
| UC06 | Quản lý khen thưởng và kỷ luật |
| UC07 | Tính và lập bảng lương hằng tháng |
| UC08 | Tra cứu và xuất báo cáo thống kê |

---

## 3. Chức năng form `FrmNhanVien`

- **Thêm** nhân viên, đồng thời lập quyết định thử việc (phòng ban, ngày ký, số tháng thử việc, lương thử việc, điều khoản).
- **Sửa** thông tin nhân viên (Mã NV bị khóa khi sửa).
- **Xóa** nhân viên (chặn nếu đã phát sinh quyết định hoặc đang là trưởng phòng).
- **Tìm kiếm** theo mã / họ tên và lọc theo phòng ban.
- **DataGridView** hiển thị danh sách; bấm vào dòng để đưa dữ liệu lên form.

### Đáp ứng yêu cầu chức năng hệ thống

| STT | Yêu cầu | Cách thực hiện |
|---|---|---|
| 1 | Kiểm tra dữ liệu | Bắt buộc Mã NV, Họ tên, Phòng ban; đủ 18 tuổi; SĐT, Email đúng định dạng; thời gian và lương thử việc > 0 |
| 2 | Transaction | Thêm nhân viên và lập quyết định thử việc trong cùng một `SqlTransaction`, lỗi thì Rollback |
| 3 | Thông báo trạng thái | `MessageBox` báo thành công hoặc lỗi chi tiết cho mọi thao tác |
| 4 | Tra cứu quan hệ | `ComboBox` cho Phòng ban, Giới tính, Trạng thái; `DataGridView` cho danh sách |
| 5 | Ngăn thao tác rủi ro | Khóa Mã NV khi sửa; ràng buộc khóa ngoại; chặn xóa nhân viên đã có quyết định |

---

## 4. Cấu trúc thư mục

```
QuanLyNhanSuDongA/
├── Forms/
│   ├── FrmNhanVien.cs
│   └── FrmNhanVien.Designer.cs
├── App.config                  # Chuỗi kết nối SQL Server
├── Db.cs                       # Lớp truy cập dữ liệu dùng chung
├── Program.cs                  # Điểm khởi chạy
├── QLNS_DongA.sql              # Script tạo CSDL và dữ liệu mẫu
├── QuanLyNhanSuDongA.csproj
├── QuanLyNhanSuDongA.sln
└── README.md
```

## 5. Cơ sở dữ liệu

Database `QLNS_DongA` gồm 3 bảng:

| Bảng | Khóa chính | Khóa ngoại |
|---|---|---|
| `PhongBan` | `MaPhongBan` | `MaTruongPhong` → `NhanVien` |
| `NhanVien` | `MaNV` | `MaPhongBan` → `PhongBan` |
| `QuyetDinhTuyenDung` | `MaQDThuViec` | `MaNV` → `NhanVien`, `MaPhongBan` → `PhongBan` |

Script có sẵn 5 phòng ban mẫu: Tài chính – Kế toán, Kỹ thuật, Sản xuất, Nhân sự, Ban Chấm công.

## 6. Hướng dẫn chạy

1. Mở SQL Server Management Studio, chạy file `QLNS_DongA.sql`.
2. Mở `QuanLyNhanSuDongA.sln` bằng Visual Studio.
3. Mở `App.config`, sửa `Server=.` nếu tên SQL Server của bạn khác (ví dụ `Server=.\SQLEXPRESS`).
4. Nhấn **F5** để chạy.

> Nếu dùng .NET 6 trở lên: đổi `TargetFramework` trong `.csproj`, cài NuGet `Microsoft.Data.SqlClient` và `System.Configuration.ConfigurationManager`, rồi đổi `using System.Data.SqlClient` thành `using Microsoft.Data.SqlClient`.

## 7. Hướng phát triển

Các form chưa làm: hợp đồng lao động và bảo hiểm, quyết định lương / chức vụ, phân công ca, chấm công và phụ trội, khen thưởng / kỷ luật, bảng lương hằng tháng. Theo tiêu chuẩn tiến hóa, có thể tách mã thành 3 lớp GUI – BUS – DAL để dễ thay đổi chính sách (tỷ lệ BHXH/BHYT, công thức phụ trội).
