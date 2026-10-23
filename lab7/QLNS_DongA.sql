IF DB_ID('QLNS_DongA') IS NULL CREATE DATABASE QLNS_DongA;
GO
USE QLNS_DongA;
GO
CREATE TABLE PhongBan(
    MaPhongBan    NVARCHAR(20)  PRIMARY KEY,
    TenPhongBan   NVARCHAR(100) NOT NULL,
    MaTruongPhong NVARCHAR(20)  NULL,
    MoTa          NVARCHAR(200) NULL
);
CREATE TABLE NhanVien(
    MaNV        NVARCHAR(20)  PRIMARY KEY,
    HoTen       NVARCHAR(100) NOT NULL,
    NgaySinh    DATE          NOT NULL,
    GioiTinh    NVARCHAR(10)  NOT NULL,
    DiaChi      NVARCHAR(200) NULL,
    SoDienThoai NVARCHAR(15)  NULL,
    Email       NVARCHAR(100) NULL,
    TrangThai   NVARCHAR(20)  NOT NULL DEFAULT N'Thử việc',
    MaPhongBan  NVARCHAR(20)  NOT NULL REFERENCES PhongBan(MaPhongBan)
);
ALTER TABLE PhongBan ADD CONSTRAINT FK_PB_TruongPhong
    FOREIGN KEY (MaTruongPhong) REFERENCES NhanVien(MaNV);
CREATE TABLE QuyetDinhTuyenDung(
    MaQDThuViec    NVARCHAR(50)  PRIMARY KEY,
    SoQuyetDinh    NVARCHAR(30)  NOT NULL,
    MaNV           NVARCHAR(20)  NOT NULL REFERENCES NhanVien(MaNV),
    MaPhongBan     NVARCHAR(20)  NOT NULL REFERENCES PhongBan(MaPhongBan),
    NgayKy         DATE          NOT NULL,
    ThoiGianThuViec INT          NOT NULL CHECK (ThoiGianThuViec > 0),
    LuongThuViec   DECIMAL(18,0) NOT NULL CHECK (LuongThuViec > 0),
    DieuKhoan      NVARCHAR(MAX) NULL
);
INSERT PhongBan(MaPhongBan,TenPhongBan,MoTa) VALUES
(N'PB01',N'Phòng Tài chính - Kế toán',N'Quản lý tài chính, lương'),
(N'PB02',N'Phòng Kỹ thuật',N'Nghiên cứu công thức sơn'),
(N'PB03',N'Phòng Sản xuất',N'Phân xưởng sản xuất sơn'),
(N'PB04',N'Phòng Nhân sự',N'Quản lý nhân sự'),
(N'PB05',N'Ban Chấm công',N'Chấm công, phụ trội');
