/*
    EDU-06 - SQL Server database schema
    Run this script in SQL Server Management Studio or sqlcmd.
    This is an initial-install script; it does not drop existing objects.
*/

IF DB_ID(N'QLThucTapEDU06') IS NULL
BEGIN
    EXEC(N'CREATE DATABASE [QLThucTapEDU06]');
END;
GO

USE [QLThucTapEDU06];
GO

/* 1. TaiKhoan */
CREATE TABLE dbo.TaiKhoan
(
    MaTK             INT IDENTITY(1,1) NOT NULL,
    TenDangNhap      NVARCHAR(100) NOT NULL,
    MatKhauHash      NVARCHAR(255) NOT NULL,
    VaiTro           NVARCHAR(20) NOT NULL,
    Email            NVARCHAR(254) NULL,
    DangHoatDong     BIT NOT NULL CONSTRAINT DF_TaiKhoan_DangHoatDong DEFAULT (1),
    NgayTao          DATETIME2(0) NOT NULL CONSTRAINT DF_TaiKhoan_NgayTao DEFAULT (SYSDATETIME()),
    NgayCapNhat      DATETIME2(0) NULL,
    CONSTRAINT PK_TaiKhoan PRIMARY KEY (MaTK),
    CONSTRAINT UQ_TaiKhoan_TenDangNhap UNIQUE (TenDangNhap),
    CONSTRAINT UQ_TaiKhoan_MaTK_VaiTro UNIQUE (MaTK, VaiTro),
    CONSTRAINT CK_TaiKhoan_VaiTro CHECK (VaiTro IN (N'Admin', N'SinhVien', N'GiangVien', N'DoanhNghiep'))
);
GO

CREATE UNIQUE INDEX UX_TaiKhoan_Email
    ON dbo.TaiKhoan(Email)
    WHERE Email IS NOT NULL;
GO

/* 2. SinhVien */
CREATE TABLE dbo.SinhVien
(
    MaSV             INT IDENTITY(1,1) NOT NULL,
    MaTK             INT NOT NULL,
    VaiTro           NVARCHAR(20) NOT NULL CONSTRAINT DF_SinhVien_VaiTro DEFAULT (N'SinhVien'),
    MSSV             NVARCHAR(30) NOT NULL,
    HoTen            NVARCHAR(200) NOT NULL,
    Lop              NVARCHAR(100) NULL,
    Nganh            NVARCHAR(150) NULL,
    SoDienThoai      NVARCHAR(30) NULL,
    DiaChi           NVARCHAR(500) NULL,
    CONSTRAINT PK_SinhVien PRIMARY KEY (MaSV),
    CONSTRAINT UQ_SinhVien_MaTK UNIQUE (MaTK),
    CONSTRAINT UQ_SinhVien_MSSV UNIQUE (MSSV),
    CONSTRAINT CK_SinhVien_VaiTro CHECK (VaiTro = N'SinhVien'),
    CONSTRAINT FK_SinhVien_TaiKhoan FOREIGN KEY (MaTK, VaiTro) REFERENCES dbo.TaiKhoan(MaTK, VaiTro)
);
GO

/* 3. GiangVien */
CREATE TABLE dbo.GiangVien
(
    MaGV             INT IDENTITY(1,1) NOT NULL,
    MaTK             INT NOT NULL,
    VaiTro           NVARCHAR(20) NOT NULL CONSTRAINT DF_GiangVien_VaiTro DEFAULT (N'GiangVien'),
    MaSoGiangVien    NVARCHAR(30) NOT NULL,
    HoTen            NVARCHAR(200) NOT NULL,
    Khoa             NVARCHAR(150) NULL,
    SoDienThoai      NVARCHAR(30) NULL,
    CONSTRAINT PK_GiangVien PRIMARY KEY (MaGV),
    CONSTRAINT UQ_GiangVien_MaTK UNIQUE (MaTK),
    CONSTRAINT UQ_GiangVien_MaSoGiangVien UNIQUE (MaSoGiangVien),
    CONSTRAINT CK_GiangVien_VaiTro CHECK (VaiTro = N'GiangVien'),
    CONSTRAINT FK_GiangVien_TaiKhoan FOREIGN KEY (MaTK, VaiTro) REFERENCES dbo.TaiKhoan(MaTK, VaiTro)
);
GO

/* 4. DoanhNghiep */
CREATE TABLE dbo.DoanhNghiep
(
    MaDN             INT IDENTITY(1,1) NOT NULL,
    MaTK             INT NOT NULL,
    VaiTro           NVARCHAR(20) NOT NULL CONSTRAINT DF_DoanhNghiep_VaiTro DEFAULT (N'DoanhNghiep'),
    MaSoThue         NVARCHAR(30) NULL,
    TenDoanhNghiep   NVARCHAR(250) NOT NULL,
    LinhVuc          NVARCHAR(150) NULL,
    DiaChi           NVARCHAR(500) NULL,
    SoDienThoai      NVARCHAR(30) NULL,
    Website          NVARCHAR(500) NULL,
    MoTa             NVARCHAR(MAX) NULL,
    CONSTRAINT PK_DoanhNghiep PRIMARY KEY (MaDN),
    CONSTRAINT UQ_DoanhNghiep_MaTK UNIQUE (MaTK),
    CONSTRAINT CK_DoanhNghiep_VaiTro CHECK (VaiTro = N'DoanhNghiep'),
    CONSTRAINT FK_DoanhNghiep_TaiKhoan FOREIGN KEY (MaTK, VaiTro) REFERENCES dbo.TaiKhoan(MaTK, VaiTro)
);
GO

CREATE UNIQUE INDEX UX_DoanhNghiep_MaSoThue
    ON dbo.DoanhNghiep(MaSoThue)
    WHERE MaSoThue IS NOT NULL;
GO

/* 5. DotThucTap */
CREATE TABLE dbo.DotThucTap
(
    MaDot            INT IDENTITY(1,1) NOT NULL,
    TenDot           NVARCHAR(200) NOT NULL,
    NgayBatDau       DATETIME2(0) NOT NULL,
    NgayKetThuc      DATETIME2(0) NOT NULL,
    HanDangKy        DATETIME2(0) NULL,
    TrangThai        NVARCHAR(20) NOT NULL,
    MoTa             NVARCHAR(MAX) NULL,
    NgayTao          DATETIME2(0) NOT NULL CONSTRAINT DF_DotThucTap_NgayTao DEFAULT (SYSDATETIME()),
    CONSTRAINT PK_DotThucTap PRIMARY KEY (MaDot),
    CONSTRAINT CK_DotThucTap_Ngay CHECK (NgayKetThuc >= NgayBatDau),
    CONSTRAINT CK_DotThucTap_HanDangKy CHECK (HanDangKy IS NULL OR HanDangKy <= NgayKetThuc),
    CONSTRAINT CK_DotThucTap_TrangThai CHECK (TrangThai IN (N'ChuaMo', N'DangMo', N'DaDong'))
);
GO

/* 6. ViTriThucTap */
CREATE TABLE dbo.ViTriThucTap
(
    MaViTri          INT IDENTITY(1,1) NOT NULL,
    MaDN             INT NOT NULL,
    MaDot            INT NOT NULL,
    TenViTri         NVARCHAR(200) NOT NULL,
    MoTa             NVARCHAR(MAX) NULL,
    YeuCau            NVARCHAR(MAX) NULL,
    SoLuong          INT NOT NULL,
    DiaDiem          NVARCHAR(300) NULL,
    TrangThai        NVARCHAR(20) NOT NULL,
    NgayDang         DATETIME2(0) NOT NULL CONSTRAINT DF_ViTriThucTap_NgayDang DEFAULT (SYSDATETIME()),
    CONSTRAINT PK_ViTriThucTap PRIMARY KEY (MaViTri),
    CONSTRAINT FK_ViTriThucTap_DoanhNghiep FOREIGN KEY (MaDN) REFERENCES dbo.DoanhNghiep(MaDN),
    CONSTRAINT FK_ViTriThucTap_DotThucTap FOREIGN KEY (MaDot) REFERENCES dbo.DotThucTap(MaDot),
    CONSTRAINT CK_ViTriThucTap_SoLuong CHECK (SoLuong > 0),
    CONSTRAINT CK_ViTriThucTap_TrangThai CHECK (TrangThai IN (N'DangTuyen', N'NgungTuyen'))
);
GO

/* 7. DonDangKy */
CREATE TABLE dbo.DonDangKy
(
    MaDon            INT IDENTITY(1,1) NOT NULL,
    MaSV             INT NOT NULL,
    MaViTri          INT NOT NULL,
    NgayNop          DATETIME2(0) NOT NULL CONSTRAINT DF_DonDangKy_NgayNop DEFAULT (SYSDATETIME()),
    ThuGioiThieu     NVARCHAR(MAX) NULL,
    TrangThai        NVARCHAR(20) NOT NULL CONSTRAINT DF_DonDangKy_TrangThai DEFAULT (N'ChoDuyet'),
    GhiChu           NVARCHAR(MAX) NULL,
    NgayCapNhat      DATETIME2(0) NULL,
    CONSTRAINT PK_DonDangKy PRIMARY KEY (MaDon),
    CONSTRAINT FK_DonDangKy_SinhVien FOREIGN KEY (MaSV) REFERENCES dbo.SinhVien(MaSV),
    CONSTRAINT FK_DonDangKy_ViTriThucTap FOREIGN KEY (MaViTri) REFERENCES dbo.ViTriThucTap(MaViTri),
    CONSTRAINT UQ_DonDangKy_SinhVien_ViTri UNIQUE (MaSV, MaViTri),
    CONSTRAINT CK_DonDangKy_TrangThai CHECK (TrangThai IN (N'ChoDuyet', N'DaDuyet', N'TuChoi', N'DaHuy'))
);
GO

/* 8. ThucTap */
CREATE TABLE dbo.ThucTap
(
    MaThucTap        INT IDENTITY(1,1) NOT NULL,
    MaDon            INT NOT NULL,
    MaGV             INT NULL,
    NgayBatDau       DATETIME2(0) NULL,
    NgayKetThuc      DATETIME2(0) NULL,
    TrangThai        NVARCHAR(30) NOT NULL,
    GhiChu           NVARCHAR(MAX) NULL,
    NgayTao          DATETIME2(0) NOT NULL CONSTRAINT DF_ThucTap_NgayTao DEFAULT (SYSDATETIME()),
    CONSTRAINT PK_ThucTap PRIMARY KEY (MaThucTap),
    CONSTRAINT UQ_ThucTap_MaDon UNIQUE (MaDon),
    CONSTRAINT FK_ThucTap_DonDangKy FOREIGN KEY (MaDon) REFERENCES dbo.DonDangKy(MaDon),
    CONSTRAINT FK_ThucTap_GiangVien FOREIGN KEY (MaGV) REFERENCES dbo.GiangVien(MaGV),
    CONSTRAINT CK_ThucTap_Ngay CHECK (NgayBatDau IS NULL OR NgayKetThuc IS NULL OR NgayKetThuc >= NgayBatDau)
);
GO

/* 9. NhatKy: computed date key enforces at most one log per internship per calendar day. */
CREATE TABLE dbo.NhatKy
(
    MaNhatKy         INT IDENTITY(1,1) NOT NULL,
    MaThucTap        INT NOT NULL,
    NgayGhi          DATETIME2(0) NOT NULL,
    NgayGhiNgay      AS CONVERT(DATE, NgayGhi) PERSISTED,
    NoiDung          NVARCHAR(MAX) NOT NULL,
    NgayTao          DATETIME2(0) NOT NULL CONSTRAINT DF_NhatKy_NgayTao DEFAULT (SYSDATETIME()),
    CONSTRAINT PK_NhatKy PRIMARY KEY (MaNhatKy),
    CONSTRAINT FK_NhatKy_ThucTap FOREIGN KEY (MaThucTap) REFERENCES dbo.ThucTap(MaThucTap),
    CONSTRAINT UQ_NhatKy_ThucTap_Ngay UNIQUE (MaThucTap, NgayGhiNgay)
);
GO

/* 10. BaoCao */
CREATE TABLE dbo.BaoCao
(
    MaBaoCao         INT IDENTITY(1,1) NOT NULL,
    MaThucTap        INT NOT NULL,
    TieuDe           NVARCHAR(300) NOT NULL,
    NoiDung          NVARCHAR(MAX) NULL,
    DuongDanTep      NVARCHAR(1000) NULL,
    NgayNop          DATETIME2(0) NOT NULL CONSTRAINT DF_BaoCao_NgayNop DEFAULT (SYSDATETIME()),
    Diem             DECIMAL(4,2) NULL,
    NhanXet          NVARCHAR(MAX) NULL,
    TrangThai        NVARCHAR(20) NOT NULL CONSTRAINT DF_BaoCao_TrangThai DEFAULT (N'DaNop'),
    NgayDanhGia      DATETIME2(0) NULL,
    CONSTRAINT PK_BaoCao PRIMARY KEY (MaBaoCao),
    CONSTRAINT FK_BaoCao_ThucTap FOREIGN KEY (MaThucTap) REFERENCES dbo.ThucTap(MaThucTap),
    CONSTRAINT CK_BaoCao_Diem CHECK (Diem IS NULL OR Diem BETWEEN 0 AND 10),
    CONSTRAINT CK_BaoCao_TrangThai CHECK (TrangThai IN (N'DaNop', N'DaDanhGia'))
);
GO

/* 11. DanhGiaSinhVien: one evaluation row per internship (BR09). */
CREATE TABLE dbo.DanhGiaSinhVien
(
    MaDanhGiaSV      INT IDENTITY(1,1) NOT NULL,
    MaThucTap        INT NOT NULL,
    Diem             DECIMAL(4,2) NOT NULL,
    NhanXet          NVARCHAR(MAX) NULL,
    NgayDanhGia      DATETIME2(0) NOT NULL CONSTRAINT DF_DanhGiaSinhVien_NgayDanhGia DEFAULT (SYSDATETIME()),
    CONSTRAINT PK_DanhGiaSinhVien PRIMARY KEY (MaDanhGiaSV),
    CONSTRAINT UQ_DanhGiaSinhVien_MaThucTap UNIQUE (MaThucTap),
    CONSTRAINT FK_DanhGiaSinhVien_ThucTap FOREIGN KEY (MaThucTap) REFERENCES dbo.ThucTap(MaThucTap),
    CONSTRAINT CK_DanhGiaSinhVien_Diem CHECK (Diem BETWEEN 0 AND 10)
);
GO

/* 12. DanhGiaDoanhNghiep: one student evaluation row per internship. */
CREATE TABLE dbo.DanhGiaDoanhNghiep
(
    MaDanhGiaDN      INT IDENTITY(1,1) NOT NULL,
    MaThucTap        INT NOT NULL,
    Diem             DECIMAL(4,2) NOT NULL,
    NhanXet          NVARCHAR(MAX) NULL,
    NgayDanhGia      DATETIME2(0) NOT NULL CONSTRAINT DF_DanhGiaDoanhNghiep_NgayDanhGia DEFAULT (SYSDATETIME()),
    CONSTRAINT PK_DanhGiaDoanhNghiep PRIMARY KEY (MaDanhGiaDN),
    CONSTRAINT UQ_DanhGiaDoanhNghiep_MaThucTap UNIQUE (MaThucTap),
    CONSTRAINT FK_DanhGiaDoanhNghiep_ThucTap FOREIGN KEY (MaThucTap) REFERENCES dbo.ThucTap(MaThucTap),
    CONSTRAINT CK_DanhGiaDoanhNghiep_Diem CHECK (Diem BETWEEN 0 AND 10)
);
GO

/* Foreign-key lookup indexes */
CREATE INDEX IX_ViTriThucTap_MaDN_MaDot ON dbo.ViTriThucTap(MaDN, MaDot);
CREATE INDEX IX_DonDangKy_MaViTri_TrangThai ON dbo.DonDangKy(MaViTri, TrangThai);
CREATE INDEX IX_DonDangKy_MaSV_TrangThai ON dbo.DonDangKy(MaSV, TrangThai);
CREATE INDEX IX_ThucTap_MaGV ON dbo.ThucTap(MaGV);
CREATE INDEX IX_NhatKy_MaThucTap_NgayGhi ON dbo.NhatKy(MaThucTap, NgayGhi);
CREATE INDEX IX_BaoCao_MaThucTap_NgayNop ON dbo.BaoCao(MaThucTap, NgayNop);
GO
USE [QLThucTapEDU06];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;

BEGIN TRY
    BEGIN TRANSACTION;

    /* TaiKhoan */
    INSERT INTO dbo.TaiKhoan (TenDangNhap, MatKhauHash, VaiTro, Email)
    SELECT v.TenDangNhap, N'Demo@123', v.VaiTro, v.Email
    FROM (VALUES
        (N'admin.demo',        N'Admin',       N'admin.demo@edu06.local'),
        (N'sinhvien.demo1',    N'SinhVien',    N'sv1.demo@edu06.local'),
        (N'sinhvien.demo2',    N'SinhVien',    N'sv2.demo@edu06.local'),
        (N'giangvien.demo',    N'GiangVien',   N'gv.demo@edu06.local'),
        (N'doanhnghiep.demo1', N'DoanhNghiep', N'cty1.demo@edu06.local'),
        (N'doanhnghiep.demo2', N'DoanhNghiep', N'cty2.demo@edu06.local')
    ) AS v(TenDangNhap, VaiTro, Email)
    WHERE NOT EXISTS (
        SELECT 1
        FROM dbo.TaiKhoan tk
        WHERE tk.TenDangNhap = v.TenDangNhap
    );

    /* SinhVien */
    INSERT INTO dbo.SinhVien
        (MaTK, MSSV, HoTen, Lop, Nganh, SoDienThoai, DiaChi)
    SELECT tk.MaTK, v.MSSV, v.HoTen, v.Lop, v.Nganh, v.SoDienThoai, v.DiaChi
    FROM (VALUES
        (N'sinhvien.demo1', N'EDU06-SV-001', N'Nguyễn Minh Anh',
         N'12-DH-CNPM2', N'Công nghệ phần mềm', N'0901000001', N'TP. Hồ Chí Minh'),
        (N'sinhvien.demo2', N'EDU06-SV-002', N'Trần Quốc Bảo',
         N'12-DH-CNPM2', N'Công nghệ phần mềm', N'0901000002', N'TP. Hồ Chí Minh')
    ) AS v(TenDangNhap, MSSV, HoTen, Lop, Nganh, SoDienThoai, DiaChi)
    INNER JOIN dbo.TaiKhoan tk ON tk.TenDangNhap = v.TenDangNhap
    WHERE NOT EXISTS (
        SELECT 1 FROM dbo.SinhVien sv WHERE sv.MaTK = tk.MaTK
    );

    /* GiangVien */
    INSERT INTO dbo.GiangVien
        (MaTK, MaSoGiangVien, HoTen, Khoa, SoDienThoai)
    SELECT tk.MaTK, N'EDU06-GV-001', N'TS. Lê Thu Hà',
           N'Công nghệ thông tin', N'0902000001'
    FROM dbo.TaiKhoan tk
    WHERE tk.TenDangNhap = N'giangvien.demo'
      AND NOT EXISTS (
          SELECT 1 FROM dbo.GiangVien gv WHERE gv.MaTK = tk.MaTK
      );

    /* DoanhNghiep */
    INSERT INTO dbo.DoanhNghiep
        (MaTK, MaSoThue, TenDoanhNghiep, LinhVuc, DiaChi,
         SoDienThoai, Website, MoTa)
    SELECT tk.MaTK, v.MaSoThue, v.TenDoanhNghiep, v.LinhVuc, v.DiaChi,
           v.SoDienThoai, v.Website, v.MoTa
    FROM (VALUES
        (N'doanhnghiep.demo1', N'DEMO-EDU06-001',
         N'Công ty Công nghệ Sao Việt', N'Phần mềm', N'TP. Hồ Chí Minh',
         N'0903000001', N'https://saoviet.example',
         N'Doanh nghiệp mẫu trong lĩnh vực phát triển phần mềm.'),
        (N'doanhnghiep.demo2', N'DEMO-EDU06-002',
         N'Công ty Dữ liệu Minh Châu', N'Dữ liệu và AI', N'TP. Hồ Chí Minh',
         N'0903000002', N'https://minhchau.example',
         N'Doanh nghiệp mẫu trong lĩnh vực dữ liệu và trí tuệ nhân tạo.')
    ) AS v(TenDangNhap, MaSoThue, TenDoanhNghiep, LinhVuc, DiaChi,
           SoDienThoai, Website, MoTa)
    INNER JOIN dbo.TaiKhoan tk ON tk.TenDangNhap = v.TenDangNhap
    WHERE NOT EXISTS (
        SELECT 1 FROM dbo.DoanhNghiep dn WHERE dn.MaTK = tk.MaTK
    );

    DECLARE
        @MaSV1 INT, @MaSV2 INT, @MaGV INT,
        @MaDN1 INT, @MaDN2 INT, @MaDot INT,
        @MaViTri1 INT, @MaViTri2 INT,
        @MaDon1 INT, @MaDon2 INT, @MaThucTap INT;

    SELECT @MaSV1 = MaSV
    FROM dbo.SinhVien
    WHERE MSSV = N'EDU06-SV-001';

    SELECT @MaSV2 = MaSV
    FROM dbo.SinhVien
    WHERE MSSV = N'EDU06-SV-002';

    SELECT @MaGV = gv.MaGV
    FROM dbo.GiangVien gv
    INNER JOIN dbo.TaiKhoan tk ON tk.MaTK = gv.MaTK
    WHERE tk.TenDangNhap = N'giangvien.demo';

    SELECT @MaDN1 = dn.MaDN
    FROM dbo.DoanhNghiep dn
    INNER JOIN dbo.TaiKhoan tk ON tk.MaTK = dn.MaTK
    WHERE tk.TenDangNhap = N'doanhnghiep.demo1';

    SELECT @MaDN2 = dn.MaDN
    FROM dbo.DoanhNghiep dn
    INNER JOIN dbo.TaiKhoan tk ON tk.MaTK = dn.MaTK
    WHERE tk.TenDangNhap = N'doanhnghiep.demo2';

    /* DotThucTap */
    IF NOT EXISTS (
        SELECT 1 FROM dbo.DotThucTap
        WHERE TenDot = N'EDU06 - Thu Dong 2026'
    )
    BEGIN
        INSERT INTO dbo.DotThucTap
            (TenDot, NgayBatDau, NgayKetThuc, HanDangKy, TrangThai, MoTa)
        VALUES
            (N'EDU06 - Thu Dong 2026',
             '2026-09-01T00:00:00', '2027-01-31T23:59:59',
             '2026-11-30T23:59:59', N'DangMo',
             N'Đợt thực tập mẫu EDU-06.');
    END;

    SELECT @MaDot = MaDot
    FROM dbo.DotThucTap
    WHERE TenDot = N'EDU06 - Thu Dong 2026';

    /* ViTriThucTap */
    IF NOT EXISTS (
        SELECT 1 FROM dbo.ViTriThucTap
        WHERE MaDN = @MaDN1 AND MaDot = @MaDot
          AND TenViTri = N'Lập trình viên Java Intern'
    )
    BEGIN
        INSERT INTO dbo.ViTriThucTap
            (MaDN, MaDot, TenViTri, MoTa, YeuCau, SoLuong, DiaDiem, TrangThai)
        VALUES
            (@MaDN1, @MaDot, N'Lập trình viên Java Intern',
             N'Tham gia phát triển API cho hệ thống quản lý thực tập.',
             N'Biết Java cơ bản, SQL Server và Git.',
             3, N'TP. Hồ Chí Minh', N'DangTuyen');
    END;

    IF NOT EXISTS (
        SELECT 1 FROM dbo.ViTriThucTap
        WHERE MaDN = @MaDN2 AND MaDot = @MaDot
          AND TenViTri = N'Thực tập sinh phân tích dữ liệu'
    )
    BEGIN
        INSERT INTO dbo.ViTriThucTap
            (MaDN, MaDot, TenViTri, MoTa, YeuCau, SoLuong, DiaDiem, TrangThai)
        VALUES
            (@MaDN2, @MaDot, N'Thực tập sinh phân tích dữ liệu',
             N'Hỗ trợ làm sạch dữ liệu và xây dựng báo cáo.',
             N'Biết SQL cơ bản và Excel.',
             2, N'TP. Hồ Chí Minh', N'DangTuyen');
    END;

    SELECT @MaViTri1 = MaViTri
    FROM dbo.ViTriThucTap
    WHERE MaDN = @MaDN1 AND MaDot = @MaDot
      AND TenViTri = N'Lập trình viên Java Intern';

    SELECT @MaViTri2 = MaViTri
    FROM dbo.ViTriThucTap
    WHERE MaDN = @MaDN2 AND MaDot = @MaDot
      AND TenViTri = N'Thực tập sinh phân tích dữ liệu';

    /* DonDangKy */
    IF NOT EXISTS (
        SELECT 1 FROM dbo.DonDangKy
        WHERE MaSV = @MaSV1 AND MaViTri = @MaViTri1
    )
    BEGIN
        INSERT INTO dbo.DonDangKy
            (MaSV, MaViTri, NgayNop, ThuGioiThieu, TrangThai, GhiChu)
        VALUES
            (@MaSV1, @MaViTri1, '2026-10-05T10:00:00',
             N'Tôi mong muốn áp dụng kiến thức Java và cơ sở dữ liệu vào dự án thực tế.',
             N'DaDuyet', N'Hồ sơ phù hợp với vị trí.');
    END;

    IF NOT EXISTS (
        SELECT 1 FROM dbo.DonDangKy
        WHERE MaSV = @MaSV2 AND MaViTri = @MaViTri2
    )
    BEGIN
        INSERT INTO dbo.DonDangKy
            (MaSV, MaViTri, NgayNop, ThuGioiThieu, TrangThai)
        VALUES
            (@MaSV2, @MaViTri2, '2026-10-10T09:30:00',
             N'Tôi quan tâm đến phân tích dữ liệu và mong muốn được học hỏi.',
             N'ChoDuyet');
    END;

    SELECT @MaDon1 = MaDon
    FROM dbo.DonDangKy
    WHERE MaSV = @MaSV1 AND MaViTri = @MaViTri1;

    SELECT @MaDon2 = MaDon
    FROM dbo.DonDangKy
    WHERE MaSV = @MaSV2 AND MaViTri = @MaViTri2;

    /* ThucTap */
    IF NOT EXISTS (
        SELECT 1 FROM dbo.ThucTap WHERE MaDon = @MaDon1
    )
    BEGIN
        INSERT INTO dbo.ThucTap
            (MaDon, MaGV, NgayBatDau, NgayKetThuc, TrangThai, GhiChu)
        VALUES
            (@MaDon1, @MaGV, '2026-10-15T08:00:00', '2026-12-31T17:00:00',
             N'DangThucTap', N'Hồ sơ mẫu đã được phân công giảng viên.');
    END;

    SELECT @MaThucTap = MaThucTap
    FROM dbo.ThucTap
    WHERE MaDon = @MaDon1;

    /* NhatKy */
    IF NOT EXISTS (
        SELECT 1 FROM dbo.NhatKy
        WHERE MaThucTap = @MaThucTap
          AND NgayGhiNgay = CONVERT(DATE, '2026-10-16')
    )
        INSERT INTO dbo.NhatKy (MaThucTap, NgayGhi, NoiDung)
        VALUES (@MaThucTap, '2026-10-16T17:00:00',
                N'Tìm hiểu kiến trúc hệ thống và cấu trúc cơ sở dữ liệu.');

    IF NOT EXISTS (
        SELECT 1 FROM dbo.NhatKy
        WHERE MaThucTap = @MaThucTap
          AND NgayGhiNgay = CONVERT(DATE, '2026-10-17')
    )
        INSERT INTO dbo.NhatKy (MaThucTap, NgayGhi, NoiDung)
        VALUES (@MaThucTap, '2026-10-17T17:00:00',
                N'Hoàn thành API tra cứu vị trí và kiểm tra dữ liệu trả về.');

    /* BaoCao */
    IF NOT EXISTS (
        SELECT 1 FROM dbo.BaoCao
        WHERE MaThucTap = @MaThucTap
          AND TieuDe = N'Báo cáo tiến độ thực tập giữa kỳ'
    )
    BEGIN
        INSERT INTO dbo.BaoCao
            (MaThucTap, TieuDe, NoiDung, DuongDanTep, NgayNop,
             Diem, NhanXet, TrangThai, NgayDanhGia)
        VALUES
            (@MaThucTap, N'Báo cáo tiến độ thực tập giữa kỳ',
             N'Báo cáo mẫu về các công việc đã thực hiện trong giai đoạn đầu.',
             N'https://example.local/reports/edu06-midterm.pdf',
             '2026-10-20T16:00:00', 8.50,
             N'Trình bày rõ ràng, cần bổ sung thêm kiểm thử.',
             N'DaDanhGia', '2026-10-22T10:00:00');
    END;

    /* DanhGiaSinhVien */
    IF NOT EXISTS (
        SELECT 1 FROM dbo.DanhGiaSinhVien
        WHERE MaThucTap = @MaThucTap
    )
    BEGIN
        INSERT INTO dbo.DanhGiaSinhVien
            (MaThucTap, Diem, NhanXet, NgayDanhGia)
        VALUES
            (@MaThucTap, 9.00, N'Chủ động, hoàn thành công việc đúng hạn.',
             '2026-12-30T15:00:00');
    END;

    /* DanhGiaDoanhNghiep */
    IF NOT EXISTS (
        SELECT 1 FROM dbo.DanhGiaDoanhNghiep
        WHERE MaThucTap = @MaThucTap
    )
    BEGIN
        INSERT INTO dbo.DanhGiaDoanhNghiep
            (MaThucTap, Diem, NhanXet, NgayDanhGia)
        VALUES
            (@MaThucTap, 8.75,
             N'Môi trường làm việc tốt, có người hướng dẫn và giao việc phù hợp.',
             '2026-12-30T16:00:00');
    END;

    COMMIT TRANSACTION;
    PRINT N'Đã thêm dữ liệu mẫu cho đủ 12 bảng.';
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0
        ROLLBACK TRANSACTION;

    THROW;
END CATCH;
GO

SELECT TenDangNhap, VaiTro, Email, DangHoatDong
FROM dbo.TaiKhoan
WHERE TenDangNhap IN
(
    N'admin.demo',
    N'sinhvien.demo1',
    N'sinhvien.demo2',
    N'giangvien.demo',
    N'doanhnghiep.demo1',
    N'doanhnghiep.demo2'
)
ORDER BY VaiTro, TenDangNhap;
GO

