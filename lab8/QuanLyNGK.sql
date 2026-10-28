/* ============================================================
   CSDL: Quản lý cửa hàng Nước giải khát (Lab 8)
   Chạy toàn bộ script trong SSMS (SQL Server 2012 trở lên)
   ============================================================ */
IF DB_ID(N'QuanLyNGK') IS NULL CREATE DATABASE QuanLyNGK;
GO
USE QuanLyNGK;
GO

/* ---------- DANH MỤC ---------- */
CREATE TABLE NhanHieu (
    maLoai   NVARCHAR(10)  NOT NULL PRIMARY KEY,
    tenLoai  NVARCHAR(100) NOT NULL,
    moTa     NVARCHAR(200) NULL
);

CREATE TABLE NhaCungUng (
    maNCU       NVARCHAR(10)  NOT NULL PRIMARY KEY,
    tenNCU      NVARCHAR(100) NOT NULL,
    diaChi      NVARCHAR(200) NULL,
    soDienThoai NVARCHAR(15)  NOT NULL
);

CREATE TABLE NuocGiaiKhat (
    maNGK      NVARCHAR(10)  NOT NULL PRIMARY KEY,
    tenNGK     NVARCHAR(100) NOT NULL,
    donViTinh  NVARCHAR(20)  NOT NULL,
    soLuongTon INT           NOT NULL DEFAULT 0 CHECK (soLuongTon >= 0),
    donGiaBan  DECIMAL(18,0) NOT NULL CHECK (donGiaBan >= 0),
    maLoai     NVARCHAR(10)  NOT NULL REFERENCES NhanHieu(maLoai),
    maNCU      NVARCHAR(10)  NOT NULL REFERENCES NhaCungUng(maNCU)   -- BR01: 1 NGK = 1 NCC
);

CREATE TABLE NhanVien (
    maNV        NVARCHAR(10)  NOT NULL PRIMARY KEY,
    hoTen       NVARCHAR(100) NOT NULL,
    chucVu      NVARCHAR(30)  NOT NULL,
    soDienThoai NVARCHAR(15)  NULL
);

CREATE TABLE KhachHang (
    maKH          NVARCHAR(10)  NOT NULL PRIMARY KEY,
    tenKH         NVARCHAR(100) NOT NULL,
    diaChi        NVARCHAR(200) NULL,
    soDienThoai   NVARCHAR(15)  NOT NULL,
    loaiKhachHang NVARCHAR(20)  NOT NULL
        CHECK (loaiKhachHang IN (N'KhachQuen', N'KhachVangLai'))
);

/* ---------- ĐẶT HÀNG / NHẬN HÀNG NCC ---------- */
CREATE TABLE DonDatHang (
    maDDH          NVARCHAR(10) NOT NULL PRIMARY KEY,
    soDDH          NVARCHAR(20) NOT NULL,
    ngayDat        DATE         NOT NULL,
    ngayNhanDuKien DATE         NOT NULL,
    maNCU          NVARCHAR(10) NOT NULL REFERENCES NhaCungUng(maNCU),
    maNV           NVARCHAR(10) NOT NULL REFERENCES NhanVien(maNV),
    trangThai      NVARCHAR(20) NOT NULL DEFAULT N'Chờ giao',
    CHECK (ngayNhanDuKien >= ngayDat)
);

CREATE TABLE ChiTietDDH (
    maCTDDH    NVARCHAR(20) NOT NULL PRIMARY KEY,
    maDDH      NVARCHAR(10) NOT NULL REFERENCES DonDatHang(maDDH),
    maNGK      NVARCHAR(10) NOT NULL REFERENCES NuocGiaiKhat(maNGK),
    soLuongDat INT          NOT NULL CHECK (soLuongDat > 0)
);

CREATE TABLE PhieuGiaoHang (
    maPGH         NVARCHAR(10)  NOT NULL PRIMARY KEY,
    soPhieuGiao   NVARCHAR(20)  NOT NULL,
    maDDH         NVARCHAR(10)  NOT NULL REFERENCES DonDatHang(maDDH),
    ngayGiao      DATE          NOT NULL,
    dotGiao       INT           NOT NULL CHECK (dotGiao BETWEEN 1 AND 3),   -- BR03: tối đa 3 đợt
    soTienPhaiTra DECIMAL(18,0) NOT NULL DEFAULT 0,
    maNV          NVARCHAR(10)  NOT NULL REFERENCES NhanVien(maNV),
    CONSTRAINT UQ_PGH_DDH_Dot UNIQUE (maDDH, dotGiao)
);

CREATE TABLE ChiTietPhieuGiao (
    maCTPG      NVARCHAR(20)  NOT NULL PRIMARY KEY,
    maPGH       NVARCHAR(10)  NOT NULL REFERENCES PhieuGiaoHang(maPGH),
    maNGK       NVARCHAR(10)  NOT NULL REFERENCES NuocGiaiKhat(maNGK),
    soLuongGiao INT           NOT NULL CHECK (soLuongGiao > 0),
    donGiaNhap  DECIMAL(18,0) NOT NULL CHECK (donGiaNhap >= 0)
);

/* ---------- BÁN HÀNG ---------- */
CREATE TABLE HoaDon (
    maHD               NVARCHAR(10)  NOT NULL PRIMARY KEY,
    soHoaDon           NVARCHAR(20)  NOT NULL,
    ngayLap            DATE          NOT NULL,
    maKH               NVARCHAR(10)  NOT NULL REFERENCES KhachHang(maKH),
    maNV               NVARCHAR(10)  NOT NULL REFERENCES NhanVien(maNV),
    tongTien           DECIMAL(18,0) NOT NULL DEFAULT 0,
    trangThaiThanhToan NVARCHAR(20)  NOT NULL
        CHECK (trangThaiThanhToan IN (N'Đã thanh toán', N'Ghi nợ', N'Trả một phần'))
);

CREATE TABLE ChiTietHoaDon (
    maCTHD  NVARCHAR(20)  NOT NULL PRIMARY KEY,
    maHD    NVARCHAR(10)  NOT NULL REFERENCES HoaDon(maHD),
    maNGK   NVARCHAR(10)  NOT NULL REFERENCES NuocGiaiKhat(maNGK),
    soLuong INT           NOT NULL CHECK (soLuong > 0),
    donGia  DECIMAL(18,0) NOT NULL CHECK (donGia >= 0)
);

CREATE TABLE PhieuHenGiao (
    maPhieuHen  NVARCHAR(10) NOT NULL PRIMARY KEY,
    soPhieuHen  NVARCHAR(20) NOT NULL,
    maHD        NVARCHAR(10) NOT NULL REFERENCES HoaDon(maHD),
    ngayHenGiao DATE         NOT NULL,
    dotGiao     INT          NOT NULL CHECK (dotGiao >= 1),
    trangThai   NVARCHAR(20) NOT NULL DEFAULT N'Chưa giao',
    maNV        NVARCHAR(10) NOT NULL REFERENCES NhanVien(maNV)
);

CREATE TABLE ChiTietPhieuHen (
    maCTPH     NVARCHAR(20) NOT NULL PRIMARY KEY,
    maPhieuHen NVARCHAR(10) NOT NULL REFERENCES PhieuHenGiao(maPhieuHen),
    maNGK      NVARCHAR(10) NOT NULL REFERENCES NuocGiaiKhat(maNGK),
    soLuongHen INT          NOT NULL CHECK (soLuongHen > 0)
);

CREATE TABLE PhieuGhiNo (
    maPhieuNo     NVARCHAR(10)  NOT NULL PRIMARY KEY,
    soPhieuNo     NVARCHAR(20)  NOT NULL,
    ngayLap       DATE          NOT NULL,
    maKH          NVARCHAR(10)  NOT NULL REFERENCES KhachHang(maKH),
    maHD          NVARCHAR(10)  NOT NULL REFERENCES HoaDon(maHD),
    soTienNo      DECIMAL(18,0) NOT NULL CHECK (soTienNo > 0),
    ngayDuKienTra DATE          NOT NULL,
    lyDoNo        NVARCHAR(200) NOT NULL,
    trangThai     NVARCHAR(20)  NOT NULL DEFAULT N'Đang nợ',
    maNV          NVARCHAR(10)  NOT NULL REFERENCES NhanVien(maNV),
    CHECK (ngayDuKienTra >= ngayLap),                       -- Ngày dự kiến trả >= ngày lập
    CONSTRAINT UQ_PhieuGhiNo_HD UNIQUE (maHD)               -- 1 hóa đơn tối đa 1 phiếu ghi nợ
);
GO

/* ---------- DỮ LIỆU MẪU ---------- */
INSERT INTO NhanHieu(maLoai, tenLoai, moTa) VALUES
 (N'NH01', N'Pepsi',       N'Nước ngọt có gas Pepsi'),
 (N'NH02', N'Coca Cola',   N'Nước ngọt có gas Coca Cola'),
 (N'NH03', N'Bia Sài Gòn', N'Bia Sài Gòn các loại');

INSERT INTO NhaCungUng(maNCU, tenNCU, diaChi, soDienThoai) VALUES
 (N'NCU01', N'Công ty Suntory PepsiCo VN', N'Quận 7, TP.HCM',  N'0281111111'),
 (N'NCU02', N'Coca-Cola Beverages VN',     N'Thủ Đức, TP.HCM', N'0282222222'),
 (N'NCU03', N'Tổng Công ty Sabeco',        N'Quận 1, TP.HCM',  N'0283333333');

INSERT INTO NuocGiaiKhat(maNGK, tenNGK, donViTinh, soLuongTon, donGiaBan, maLoai, maNCU) VALUES
 (N'NGK01', N'Pepsi lon 330ml',         N'Lon',   120,   9000, N'NH01', N'NCU01'),
 (N'NGK02', N'Pepsi chai 1.5L',         N'Chai',   60,  17000, N'NH01', N'NCU01'),
 (N'NGK03', N'Coca Cola lon 330ml',     N'Lon',   150,   9500, N'NH02', N'NCU02'),
 (N'NGK04', N'Coca Cola thùng 24 lon',  N'Thùng',   8, 220000, N'NH02', N'NCU02'),
 (N'NGK05', N'Bia Sài Gòn Lager lon',   N'Lon',   200,  14000, N'NH03', N'NCU03'),
 (N'NGK06', N'Bia Sài Gòn két 24 chai', N'Két',     5, 280000, N'NH03', N'NCU03');

INSERT INTO NhanVien(maNV, hoTen, chucVu, soDienThoai) VALUES
 (N'NV01', N'Nguyễn Văn An', N'Bán hàng', N'0901000001'),
 (N'NV02', N'Trần Thị Bình', N'Thủ kho',  N'0901000002'),
 (N'NV03', N'Lê Văn Cường',  N'Quản lý',  N'0901000003');

INSERT INTO KhachHang(maKH, tenKH, diaChi, soDienThoai, loaiKhachHang) VALUES
 (N'KH01', N'Nguyễn Thị Hoa',     N'Quận 3, TP.HCM',  N'0912000001', N'KhachQuen'),
 (N'KH02', N'Quán nhậu Bình Dân', N'Quận 10, TP.HCM', N'0912000002', N'KhachQuen'),
 (N'KH03', N'Khách vãng lai',     N'',                N'0000000000', N'KhachVangLai'),
 (N'KH04', N'Phạm Minh Tuấn',     N'Quận 5, TP.HCM',  N'0912000004', N'KhachVangLai');
GO
