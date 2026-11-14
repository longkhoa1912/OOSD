Phạm Long khoa
1250080083 
CNPM2
# Cổng Quản lý Thực tập EDU-06

Đồ án hỗ trợ kết nối sinh viên, doanh nghiệp và giảng viên trong quá trình đăng ký, tổ chức và đánh giá thực tập. Repository gồm cơ sở dữ liệu SQL Server, backend Spring Boot, giao diện Web và ứng dụng Windows Forms.

## Công nghệ

- **Database:** Microsoft SQL Server, T-SQL, Stored Procedures, Views và Triggers.
- **Backend:** Java 25, Spring Boot 4.1.2, Maven, JdbcTemplate.
- **Web:** HTML, CSS và JavaScript tĩnh, được Spring Boot phục vụ.
- **Desktop:** C# Windows Forms, .NET Framework 4.7.2.

## Cấu trúc repository


EDU-06-QLThucTap/
README.md
 database/
   QLThucTapEDU06_DDL.sql
   EDU06_Views_Triggers.sql
   EDU06_StoredProcedures.sql
   EDU06_UC14_DanhSachBaoCao.sql
   EDU06_DuLieuDemo_MoRong.sql
   
 web/                         # Project Spring Boot (qlthuctap)
   pom.xml
   mvnw.cmd
   src/

 winforms/                    # Solution/project Visual Studio (QltThucTap)
   <solution>.sln
   <project>/

diagrams/                 
   active.draw
   class.draw
   erd.draw
   mô hình kiến trúc.draw
   sequence diagram.draw
   usecase.draw
 
```

Chỉ đưa vào `docs/diagrams` những sơ đồ liên quan đến EDU-06. Không đưa các thư mục sinh tự động như `web/target`, `winforms/bin`, `winforms/obj`, `.idea` hoặc `.vs` lên repository.

## Cài đặt cơ sở dữ liệu

Yêu cầu SQL Server và SQL Server Management Studio (SSMS).

1. Mở SSMS và kết nối đến SQL Server.
2. Chạy lần lượt các script trong `database/`: `QLThucTapEDU06_DDL.sql`, `EDU06_Views_Triggers.sql`, `EDU06_StoredProcedures.sql`, `EDU06_UC14_DanhSachBaoCao.sql`, rồi `EDU06_DuLieuDemo_MoRong.sql`.
3. Database mặc định là `QLThucTapEDU06`. Nếu dùng tên hoặc máy chủ khác, cập nhật cấu hình kết nối backend tương ứng.

Chạy script dữ liệu demo bằng SSMS để giữ nguyên tiếng Việt Unicode. Các script `Repair_*.sql`, nếu được giữ trong repository, chỉ dùng để sửa dữ liệu demo hiện có; chúng không thay thế các script cài đặt chính.

## Chạy backend và Web

Yêu cầu JDK 25 trở lên. Cấu hình tài khoản SQL Server bằng biến môi trường; không ghi mật khẩu thật vào GitHub.

Trong PowerShell, từ thư mục `web/`:

```powershell
$env:DB_USERNAME = "ten_tai_khoan_sql"
$env:DB_PASSWORD = "mat_khau_sql"
.\mvnw.cmd spring-boot:run
```

Hoặc mở project `web/` bằng IntelliJ IDEA và chạy lớp `QlthuctapApplication`. Khi backend khởi động thành công, mở:

```text
http://localhost:8080
```

Backend mặc định kết nối `localhost:1433`, database `QLThucTapEDU06`. Nếu SQL Server dùng cổng hoặc instance khác, cập nhật URL trong `src/main/resources/application.properties` ở máy chạy local. Không commit mật khẩu hoặc cấu hình riêng của máy.

## Chạy Windows Forms

Yêu cầu Visual Studio và .NET Framework 4.7.2 Developer Pack.

1. Mở file solution `.sln` trong thư mục `winforms/`.
2. Kiểm tra cấu hình URL backend trong project; mặc định backend chạy tại `http://localhost:8080`.
3. Chọn project WinForms làm Startup Project rồi nhấn **Start**.

Windows Forms gọi API Spring Boot; vì vậy cần chạy SQL Server và backend trước khi sử dụng các chức năng cần dữ liệu.

## Tài khoản và dữ liệu demo

Các tài khoản demo được tạo bởi script dữ liệu mẫu. Không sử dụng thông tin đăng nhập thật trong repository công khai. Nếu cần công bố tài khoản demo, hãy dùng dữ liệu thử nghiệm riêng và đổi mật khẩu trước khi phát hành.

## An toàn thông tin và GitHub

- Không commit mật khẩu SQL Server, token, API key, `.env` hoặc cấu hình chứa bí mật.
- Dùng cấu hình mẫu không có bí mật và hướng dẫn người dùng tự điền cấu hình local.
- Repository cần đặt **Public** để giảng viên có thể truy cập.
- Dán URL trực tiếp đến repository vào trang thông tin đồ án trong báo cáo `.docx`.

