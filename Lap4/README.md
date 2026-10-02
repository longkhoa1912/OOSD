# BÁO CÁO BÀI LAB 4 - PHÂN TÍCH THIẾT KẾ HỆ THỐNG e-SHOPPING

## 1. Thông tin sinh viên
* **Họ và tên:** Phạm Long Khoa
* **Mã số sinh viên (MSSV):** 1250080083
* **Lớp / Trường:** ĐH Tài nguyên và Môi trường TP. Hồ Chí Minh

---

## 2. Tổng quan Hệ thống e-SHOPPING

### 2.1. Mục tiêu hệ thống
Xây dựng ứng dụng/website **e-SHOPPING** phục vụ mua sắm trực tuyến với các tính năng:
* Xem, tìm kiếm và xem chi tiết sản phẩm.
* Quản lý giỏ hàng (thêm, xóa, cập nhật số lượng).
* Đăng ký, đăng nhập tài khoản người dùng.
* Đặt hàng trực tuyến, lựa chọn hình thức giao hàng.
* Thanh toán bằng thẻ tín dụng.
* Nhận email xác nhận đơn hàng tự động.

### 2.2. Tác nhân (Actors) & Tích hợp hệ thống bên ngoài
Hệ thống tương tác với **4 tác nhân/hệ thống**:
1. **Khách hàng:** Người dùng trực tiếp mua sắm và thanh toán.
2. **Hệ thống quản lý sản phẩm (External):** Cung cấp và đồng bộ thông tin danh mục, sản phẩm.
3. **Dịch vụ thanh toán trực tuyến (External):** Tích hợp cổng thanh toán xác thực thẻ tín dụng.
4. **Dịch vụ Email (External):** Gửi email xác nhận đơn hàng tự động.

---

## 3. Danh sách Use Case & Đặc tả chi tiết

### 3.1. Danh sách Use Case (UC01 - UC10)
* **UC01:** Đăng ký tài khoản
* **UC02:** Đăng nhập & Đăng xuất
* **UC03:** Xem & Tìm kiếm sản phẩm
* **UC04:** Quản lý giỏ hàng
* **UC05:** Đặt hàng & Thanh toán
* **UC06:** Xem lịch sử đơn hàng
* **UC07:** Quản lý Nhóm sản phẩm
* **UC08:** Quản lý Sản phẩm
* **UC09:** Quản lý Đơn hàng & Xử lý giao hàng
* **UC10:** Quản lý Khách hàng

---

### 3.2. Đặc tả Use Case chi tiết

#### 📌 UC01 - Đăng ký tài khoản
| Mục | Nội dung |
| :--- | :--- |
| **Mã Use Case** | UC01 |
| **Tên Use Case** | Đăng ký tài khoản |
| **Tác nhân** | Khách hàng (chưa có tài khoản) |
| **Mô tả ngắn** | Cho phép người dùng tạo tài khoản mới trên e-SHOPPING. |
| **Điều kiện tiên quyết** | Mở ứng dụng và chọn chức năng "Đăng ký". |
| **Điều kiện sau** | Lưu thông tin khách hàng mới vào bảng `KHACHHANG` trong CSDL `eSHOPPING_DB`. |
| **Luồng sự kiện chính** | 1. Chọn "Đăng ký" từ màn hình chính/đăng nhập.<br>2. Hệ thống hiển thị biểu mẫu đăng ký.<br>3. Nhập: Họ tên, Tên đăng nhập, Mật khẩu, SĐT, Email, Địa chỉ, CMND/Passport.<br>4. Nhấn "Xác nhận đăng ký".<br>5. Hệ thống kiểm tra tính hợp lệ dữ liệu.<br>6. Lưu dữ liệu vào CSDL.<br>7. Thông báo "Đăng ký thành công" và đóng form. |
| **Luồng ngoại lệ** | **5a. Thiếu thông tin:** Hiển thị thông báo yêu cầu điền đầy đủ các trường.<br>**5b. Trùng Tên đăng nhập/Email:** Báo lỗi trùng lặp và yêu cầu nhập lại. |

#### 📌 UC02 - Đăng nhập & Đăng xuất
| Mục | Nội dung |
| :--- | :--- |
| **Mã Use Case** | UC02 |
| **Tên Use Case** | Đăng nhập & Đăng xuất |
| **Tác nhân** | Khách hàng |
| **Mô tả ngắn** | Xác thực tài khoản khách hàng để truy cập chức năng cá nhân và thoát phiên làm việc. |
| **Điều kiện tiên quyết** | Đã có tài khoản trong hệ thống. |
| **Điều kiện sau** | **Đăng nhập:** Lưu phiên (`CurrentMaKH`, `CurrentHoTen`), cập nhật giao diện.<br>**Đăng xuất:** Xóa thông tin phiên làm việc. |
| **Luồng sự kiện chính** | **[Đăng nhập]**<br>1. Nhấn nút "Đăng nhập" trên `FrmMain`.<br>2. Hiển thị form `FrmDangNhap`.<br>3. Nhập Tên đăng nhập & Mật khẩu -> Nhấn "Đăng nhập".<br>4. Đối chiếu dữ liệu với bảng `KHACHHANG`.<br>5. Lưu phiên làm việc, thông báo chào mừng và cập nhật tên người dùng lên header.<br>6. Đóng `FrmDangNhap`.<br>**[Đăng xuất]**<br>1. Nhấn "Đăng xuất" -> Xóa dữ liệu phiên (`CurrentMaKH = -1`, `CurrentHoTen = ""`). |
| **Luồng ngoại lệ** | **4a. Chưa nhập đủ thông tin:** Cảnh báo "Vui lòng nhập đầy đủ Tên đăng nhập và Mật khẩu!".<br>**4b. Sai thông tin:** Hiển thị "Tên đăng nhập hoặc mật khẩu không chính xác!", tự động xóa ô mật khẩu và focus lại. |

---

## 4. Quy định nghiệp vụ (Business Rules)

* **Sản phẩm:** Không trực tiếp lưu trữ toàn bộ mà đồng bộ với *Hệ thống quản lý sản phẩm*. Mỗi sản phẩm thuộc 1 nhóm sản phẩm và có trạng thái tồn kho riêng (`Còn hàng`, `Hết hàng`).
* **Tài khoản:** `TenDangNhap` là duy nhất. Bắt buộc đăng nhập trước khi thực hiện Đặt hàng.
* **Giỏ hàng:** Thành tiền = Đơn giá × Số lượng. Tổng tiền = Tổng các thành tiền. Không cho phép đặt sản phẩm hết hàng (`SoLuongTon = 0`).
* **Giao hàng & Phí giao hàng:**
  * 03 Hình thức: *Giao hàng thường*, *Giao hàng nhanh*, *Giao hàng nhanh trong ngày*.
  * **Miễn phí giao hàng nhanh:** Đơn hàng có tổng tiền hàng $\ge$ 1.000.000 VNĐ.
  * **Miễn phí giao nhanh trong ngày:** Đơn hàng có tổng tiền hàng $\ge$ 5.000.000 VNĐ.
* **Thanh toán qua Thẻ tín dụng:**
  * **VISA, Master, Discover:** Số thẻ 16 chữ số, mã CVV 3 chữ số.
  * **American Express (AmEx):** Số thẻ 15 chữ số, mã CVV 4 chữ số.
* **Xác nhận đơn & Gửi Email:** Đơn hàng chỉ ghi nhận thành công khi Cổng thanh toán xác thực thành công. Hệ thống tự động gửi Email xác nhận (tuyệt đối không kèm thông tin thẻ).

---

## 5. Yêu cầu phi chức năng

1. **Bảo mật:** Mật khẩu được mã hóa, số thẻ tín dụng được che/masking (`SoTheMasked`), không gửi thông tin thẻ qua email.
2. **Toàn vẹn dữ liệu:** Mã sản phẩm, mã đơn hàng không trùng lặp; Số lượng là số nguyên dương; Đơn hàng chỉ lưu khi thanh toán thành công.
3. **Hiệu năng & Khả năng tích hợp:** Tốc độ truy xuất danh mục sản phẩm nhanh, kết nối ổn định qua API với Cổng thanh toán và Dịch vụ Email.

---

## 6. Thiết kế Cơ sở Dữ liệu (SQL Server - `eSHOPPING_DB`)

Hệ thống gồm **10 bảng chính**: `NHOMSANPHAM`, `SANPHAM`, `KHACHHANG`, `GIOHANG`, `CHITIETGIOHANG`, `HINHTHUCGIAOHANG`, `NGUOINHAN`, `THANHTOAN`, `DONHANG`, `CHITIETDONHANG`.

### Script khởi tạo CSDL:
```sql
CREATE DATABASE eSHOPPING_DB;
GO
USE eSHOPPING_DB;
GO

-- 1. BẢNG NHÓM SẢN PHẨM
CREATE TABLE NHOMSANPHAM (
    MaNhom NVARCHAR(20) PRIMARY KEY,
    TenNhom NVARCHAR(100) NOT NULL
);

-- 2. BẢNG SẢN PHẨM
CREATE TABLE SANPHAM (
    MaSP NVARCHAR(20) PRIMARY KEY,
    TenSP NVARCHAR(200) NOT NULL,
    MaNhom NVARCHAR(20) NOT NULL,
    NhaSanXuat NVARCHAR(100),
    HinhAnh NVARCHAR(255),
    MoTa NVARCHAR(MAX),
    ThongSoKyThuat NVARCHAR(MAX),
    GiaBan DECIMAL(18, 2) NOT NULL CHECK (GiaBan >= 0),
    SoLuongTon INT NOT NULL DEFAULT 0 CHECK (SoLuongTon >= 0),
    TrangThai NVARCHAR(50) DEFAULT N'Còn hàng',
    CONSTRAINT FK_SANPHAM_NHOM FOREIGN KEY (MaNhom) REFERENCES NHOMSANPHAM(MaNhom) ON DELETE CASCADE
);

-- 3. BẢNG KHÁCH HÀNG
CREATE TABLE KHACHHANG (
    MaKH INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE,
    CMND_Passport VARCHAR(20) NOT NULL,
    DiaChi NVARCHAR(255),
    SoDienThoai VARCHAR(15),
    TenDangNhap VARCHAR(50) NOT NULL UNIQUE,
    MatKhau VARCHAR(255) NOT NULL,
    Email VARCHAR(100)
);

-- 4. BẢNG GIỎ HÀNG
CREATE TABLE GIOHANG (
    MaGioHang INT IDENTITY(1,1) PRIMARY KEY,
    MaKH INT NOT NULL UNIQUE,
    NgayTao DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_GIOHANG_KH FOREIGN KEY (MaKH) REFERENCES KHACHHANG(MaKH) ON DELETE CASCADE
);

-- 5. BẢNG CHI TIẾT GIỎ HÀNG
CREATE TABLE CHITIETGIOHANG (
    MaGioHang INT NOT NULL,
    MaSP NVARCHAR(20) NOT NULL,
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    PRIMARY KEY (MaGioHang, MaSP),
    CONSTRAINT FK_CTGH_GIOHANG FOREIGN KEY (MaGioHang) REFERENCES GIOHANG(MaGioHang) ON DELETE CASCADE,
    CONSTRAINT FK_CTGH_SANPHAM FOREIGN KEY (MaSP) REFERENCES SANPHAM(MaSP)
);

-- 6. BẢNG HÌNH THỨC GIAO HÀNG
CREATE TABLE HINHTHUCGIAOHANG (
    MaHTGH INT IDENTITY(1,1) PRIMARY KEY,
    TenHTGH NVARCHAR(100) NOT NULL,
    PhiCoDinh DECIMAL(18, 2) NOT NULL DEFAULT 0
);

-- 7. BẢNG THÔNG TIN NGƯỜI NHẬN
CREATE TABLE NGUOINHAN (
    MaNguoiNhan INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    DiaChi NVARCHAR(255) NOT NULL,
    SoDienThoai VARCHAR(15) NOT NULL
);

-- 8. BẢNG THANH TOÁN
CREATE TABLE THANHTOAN (
    MaThanhToan INT IDENTITY(1,1) PRIMARY KEY,
    LoaiThe VARCHAR(20) NOT NULL,
    SoTheMasked VARCHAR(20) NOT NULL,
    PhiGiaoDich DECIMAL(18, 2) DEFAULT 0,
    NgayThanhToan DATETIME DEFAULT GETDATE(),
    TrangThaiThanhToan NVARCHAR(50) DEFAULT N'Thành công'
);

-- 9. BẢNG ĐƠN HÀNG
CREATE TABLE DONHANG (
    MaDonHang INT IDENTITY(1,1) PRIMARY KEY,
    MaKH INT NOT NULL,
    MaNguoiNhan INT NOT NULL,
    MaHTGH INT NOT NULL,
    MaThanhToan INT NOT NULL,
    NgayDat DATETIME DEFAULT GETDATE(),
    TongTienHang DECIMAL(18, 2) NOT NULL CHECK (TongTienHang >= 0),
    PhiGiaoHang DECIMAL(18, 2) NOT NULL DEFAULT 0 CHECK (PhiGiaoHang >= 0),
    TongThanhToan DECIMAL(18, 2) NOT NULL CHECK (TongThanhToan >= 0),
    TrangThai NVARCHAR(50) DEFAULT N'DatHang',
    CONSTRAINT FK_DONHANG_KH FOREIGN KEY (MaKH) REFERENCES KHACHHANG(MaKH),
    CONSTRAINT FK_DONHANG_NGUOINHAN FOREIGN KEY (MaNguoiNhan) REFERENCES NGUOINHAN(MaNguoiNhan),
    CONSTRAINT FK_DONHANG_HTGH FOREIGN KEY (MaHTGH) REFERENCES HINHTHUCGIAOHANG(MaHTGH),
    CONSTRAINT FK_DONHANG_THANHTOAN FOREIGN KEY (MaThanhToan) REFERENCES THANHTOAN(MaThanhToan)
);

-- 10. BẢNG CHI TIẾT ĐƠN HÀNG
CREATE TABLE CHITIETDONHANG (
    MaDonHang INT NOT NULL,
    MaSP NVARCHAR(20) NOT NULL,
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    DonGia DECIMAL(18, 2) NOT NULL CHECK (DonGia >= 0),
    ThanhTien AS (SoLuong * DonGia),
    PRIMARY KEY (MaDonHang, MaSP),
    CONSTRAINT FK_CTDH_DONHANG FOREIGN KEY (MaDonHang) REFERENCES DONHANG(MaDonHang) ON DELETE CASCADE,
    CONSTRAINT FK_CTDH_SANPHAM FOREIGN KEY (MaSP) REFERENCES SANPHAM(MaSP)
);
