-- Script tạo cơ sở dữ liệu cho form "Lập phiếu đăng ký tour theo đoàn"
-- Chạy toàn bộ script này trong SQL Server Management Studio (SSMS).

IF DB_ID(N'QuanLyTourDuLich') IS NULL
    CREATE DATABASE QuanLyTourDuLich;
GO

USE QuanLyTourDuLich;
GO

IF OBJECT_ID(N'DatCoc') IS NOT NULL DROP TABLE DatCoc;
IF OBJECT_ID(N'DanhSachNguoiDi') IS NOT NULL DROP TABLE DanhSachNguoiDi;
IF OBJECT_ID(N'PhieuDangKyDoan') IS NOT NULL DROP TABLE PhieuDangKyDoan;
IF OBJECT_ID(N'DoanKhach') IS NOT NULL DROP TABLE DoanKhach;
IF OBJECT_ID(N'Tour') IS NOT NULL DROP TABLE Tour;
GO

CREATE TABLE Tour (
    MaTour   VARCHAR(10)   NOT NULL PRIMARY KEY,
    TenTour  NVARCHAR(200) NOT NULL,
    SoNgay   INT           NOT NULL CHECK (SoNgay > 0),
    SoDem    INT           NOT NULL CHECK (SoDem >= 0),
    DonGia   DECIMAL(18,0) NOT NULL CHECK (DonGia >= 0)
);

CREATE TABLE DoanKhach (
    MaDoan       INT IDENTITY(1,1) PRIMARY KEY,
    TenCoQuan    NVARCHAR(200) NOT NULL,
    DiaChi       NVARCHAR(300) NOT NULL,
    DienThoai    VARCHAR(20)   NOT NULL,
    NguoiDaiDien NVARCHAR(100) NOT NULL
);

CREATE TABLE PhieuDangKyDoan (
    MaPhieu    INT IDENTITY(1,1) PRIMARY KEY,
    MaDoan     INT           NOT NULL REFERENCES DoanKhach(MaDoan),
    MaTour     VARCHAR(10)   NOT NULL REFERENCES Tour(MaTour),
    NgayDi     DATE          NOT NULL,
    SoNguoi    INT           NOT NULL CHECK (SoNguoi > 12),
    DiaDiemDon NVARCHAR(300) NOT NULL,
    CoBaoHiem  BIT           NOT NULL DEFAULT 0,
    NgayLap    DATETIME      NOT NULL DEFAULT GETDATE()
);

CREATE TABLE DanhSachNguoiDi (
    MaNguoiDi INT IDENTITY(1,1) PRIMARY KEY,
    MaPhieu   INT           NOT NULL REFERENCES PhieuDangKyDoan(MaPhieu),
    HoTen     NVARCHAR(100) NOT NULL,
    NamSinh   INT           NOT NULL,
    GioiTinh  NVARCHAR(10)  NOT NULL
);

CREATE TABLE DatCoc (
    MaCoc   INT IDENTITY(1,1) PRIMARY KEY,
    MaPhieu INT           NOT NULL REFERENCES PhieuDangKyDoan(MaPhieu),
    SoTien  DECIMAL(18,0) NOT NULL CHECK (SoTien > 0),
    NgayCoc DATETIME      NOT NULL DEFAULT GETDATE()
);
GO

-- Dữ liệu tour mẫu để thử form (bạn có thể thay bằng dữ liệu thật)
INSERT INTO Tour (MaTour, TenTour, SoNgay, SoDem, DonGia) VALUES
('T001', N'TP.HCM - Đà Lạt',            3, 2, 2500000),
('T002', N'TP.HCM - Nha Trang',         4, 3, 3800000),
('T003', N'TP.HCM - Phú Quốc',          3, 2, 4200000),
('T004', N'TP.HCM - Hà Nội - Hạ Long',  5, 4, 7900000);
GO
