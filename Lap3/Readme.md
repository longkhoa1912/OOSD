# BÁO CÁO BÀI LAB 3 - HỆ THỐNG QUẢN LÝ KHÁCH SẠN

## 1. Thông tin sinh viên
* **Họ và tên:** Phạm Long Khoa
* **Mã số sinh viên (MSSV):** 1250080083
* **Lớp CNPM2
* 

---

## 2. Thông tin bài Lab
* **Tên bài Lab:** LAB 3 - Xây dựng Giao diện & Chức năng Hệ thống Quản lý Khách sạn trên Windows Forms
* **Môi trường & Công nghệ sử dụng:**
  * **IDE:** Visual Studio 2022
  * **Nền tảng:** C# Windows Forms (.NET Framework 4.7.2 / 4.8)
  * **Hệ quản trị CSDL:** Microsoft SQL Server 2022
  * **Quản lý mã nguồn:** Git & GitHub

---

## 3. Nội dung đã thực hiện
* [x] **Cơ sở dữ liệu (SQL Server):**
  * Xây dựng kịch bản SQL `QuanLyKhachSan.sql` chứa cấu trúc các bảng: `PhieuDatPhong`, `HoaDon`, `PhieuDenBu`, `DichVu`, `ChiTietPhieuSuDungDV`,...
* [x] **Thiết kế Giao diện (Windows Forms):**
  * `FrmMain`: Màn hình điều hướng chính của hệ thống.
  * `FrmDanhMuc`: Màn hình quản lý danh mục.
  * `FrmPhongTienNghi`: Quản lý danh sách phòng và trang thiết bị tiện nghi.
  * `FrmDatPhong`: Quản lý đặt phòng và nhận phòng.
  * `FrmDichVu`: Quản lý các dịch vụ khách sạn.
  * `FrmTraPhong`: Xử lý trả phòng và thanh toán hóa đơn.
  * `FrmThongKe`: Thống kê phiếu đặt, khách đang ở, doanh thu hóa đơn, đền bù và bảng chi tiết dịch vụ sử dụng theo khoảng thời gian.
* [x] **Lập trình Logic & Xử lý:**
  * Lớp xử lý dữ liệu tập trung `Db.cs` (`SqlDataAdapter`, `DataTable`).
  * Đăng ký và kết nối sự kiện chuyển đổi giữa các Form từ `FrmMain`.
  * Tính toán thống kê dữ liệu động theo khoảng thời gian lọc (`DateTimePicker`).
  * Thêm hộp thoại xác nhận an toàn trước khi thoát ứng dụng.

---

## 4. Kết quả đạt được
* Giao diện trực quan, đồng bộ và đúng theo yêu cầu thiết kế.
* Chức năng thống kê (`FrmThongKe`) lấy dữ liệu chính xác từ CSDL SQL Server và tổng hợp báo cáo chi tiết theo thời gian chọn.
* Các liên kết form điều hướng hoạt động mượt mà, không gặp lỗi rò rỉ bộ nhớ hay xung đột sự kiện.

---

## 5. Lỗi gặp phải & Cách khắc phục

| STT | Lỗi gặp phải | Nguyên nhân | Cách khắc phục |
| :---: | :--- | :--- | :--- |
| **1** | Mất giao diện Visual Designer trên `FrmMain` (Báo lỗi `button6_Click`) | Đổi tên sự kiện trong file `FrmMain.cs` nhưng trong file `FrmMain.Designer.cs` chưa cập nhật. | Mở `FrmMain.Designer.cs`, sửa lại dòng gán sự kiện `this.button6.Click += new System.EventHandler(this.btnThongKe_Click);`. |
| **2** | Không xóa được thư mục cũ trực tiếp trên giao diện web GitHub | GitHub Web không hỗ trợ nút Delete trực tiếp cho thư mục rỗng/chứa file. | Nhấn phím `.` trên GitHub để mở VS Code Web (`github.dev`), thực hiện xóa thư mục và `Commit & Push` lại. |
| **3** | Khai báo thiếu hoặc lỗi chuỗi kết nối CSDL | Chuỗi `connectionString` chưa khớp với tên SQL Instance trên máy local. | Cập nhật tham số `Data Source` trong lớp `Db.cs` phù hợp với máy chạy. |

---

## 6. Hướng dẫn Giảng viên kiểm tra / Chạy lại chương trình

### Bước 1: Khởi tạo Cơ sở dữ liệu
1. Mở **SQL Server Management Studio (SSMS)**.
2. Mở file script `QuanLyKhachSan.sql` nằm ở thư mục gốc dự án.
3. Nhấn **Execute (F5)** để tạo CSDL `QuanLyKhachSan` và các bảng/dữ liệu mẫu.

### Bước 2: Cấu hình chuỗi kết nối trong dự án
1. Mở tệp solution `Quanlykhachsang.sln` bằng **Visual Studio**.
2. Tìm đến file `Db.cs` trong dự án.
3. Thay đổi tham số `Data Source` trong chuỗi kết nối tương ứng với Server máy tính của Giảng viên:
   ```csharp
   private static string connectionString = @"Data Source=YOUR_SERVER_NAME;Initial Catalog=QuanLyKhachSan;Integrated Security=True";
