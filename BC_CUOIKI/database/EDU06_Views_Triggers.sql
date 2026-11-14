/*
  EDU-06 objects for SQL Server.
  Prerequisite: run the canonical QLThucTapEDU06 DDL first.
*/
USE [QLThucTapEDU06];
GO

CREATE OR ALTER VIEW dbo.vw_DanhSachViTriThucTap
AS
SELECT vt.MaViTri AS MaVT, vt.TenViTri, dn.TenDoanhNghiep, dn.LinhVuc,
       dt.TenDot, vt.DiaDiem, vt.SoLuong, vt.TrangThai
FROM dbo.ViTriThucTap AS vt
JOIN dbo.DoanhNghiep AS dn ON dn.MaDN = vt.MaDN
JOIN dbo.DotThucTap AS dt ON dt.MaDot = vt.MaDot;
GO

CREATE OR ALTER VIEW dbo.vw_DanhSachDonDangKy
AS
SELECT dd.MaDon, sv.MaSV, sv.MSSV AS MaSoSinhVien,
       sv.HoTen AS HoTenSinhVien, vt.MaViTri AS MaVT, dn.MaDN,
       vt.TenViTri, dn.TenDoanhNghiep, dt.MaDot, dt.TenDot,
       dd.NgayNop, dd.TrangThai AS TrangThaiDon,
       dd.ThuGioiThieu, dd.GhiChu
FROM dbo.DonDangKy AS dd
JOIN dbo.SinhVien AS sv ON sv.MaSV = dd.MaSV
JOIN dbo.ViTriThucTap AS vt ON vt.MaViTri = dd.MaViTri
JOIN dbo.DoanhNghiep AS dn ON dn.MaDN = vt.MaDN
JOIN dbo.DotThucTap AS dt ON dt.MaDot = vt.MaDot;
GO

CREATE OR ALTER VIEW dbo.vw_DanhSachThucTapChiTiet
AS
SELECT tt.MaThucTap AS MaTT, sv.MaSV, sv.MSSV AS MaSoSinhVien,
       sv.HoTen AS HoTenSinhVien, tt.MaGV, gv.HoTen AS HoTenGiangVien,
       dn.MaDN, dn.TenDoanhNghiep, dt.MaDot, dt.TenDot,
       tt.NgayBatDau, tt.NgayKetThuc, tt.TrangThai
FROM dbo.ThucTap AS tt
JOIN dbo.DonDangKy AS dd ON dd.MaDon = tt.MaDon
JOIN dbo.SinhVien AS sv ON sv.MaSV = dd.MaSV
JOIN dbo.ViTriThucTap AS vt ON vt.MaViTri = dd.MaViTri
JOIN dbo.DoanhNghiep AS dn ON dn.MaDN = vt.MaDN
JOIN dbo.DotThucTap AS dt ON dt.MaDot = vt.MaDot
LEFT JOIN dbo.GiangVien AS gv ON gv.MaGV = tt.MaGV;
GO

CREATE OR ALTER VIEW dbo.vw_DanhSachBaoCao
AS
SELECT bc.MaBaoCao AS MaBC, tt.MaThucTap AS MaTT, tt.MaGV,
       sv.MaSV, sv.HoTen AS HoTenSinhVien, dt.MaDot, dt.TenDot,
       bc.TieuDe, bc.NoiDung, bc.DuongDanTep, bc.NgayNop,
       bc.Diem, bc.NhanXet, bc.TrangThai, bc.NgayDanhGia
FROM dbo.BaoCao AS bc
JOIN dbo.ThucTap AS tt ON tt.MaThucTap = bc.MaThucTap
JOIN dbo.DonDangKy AS dd ON dd.MaDon = tt.MaDon
JOIN dbo.SinhVien AS sv ON sv.MaSV = dd.MaSV
JOIN dbo.ViTriThucTap AS vt ON vt.MaViTri = dd.MaViTri
JOIN dbo.DotThucTap AS dt ON dt.MaDot = vt.MaDot;
GO

CREATE OR ALTER VIEW dbo.vw_DanhSachDanhGiaDoanhNghiep
AS
SELECT dg.MaDanhGiaDN AS MaDGDN, tt.MaThucTap AS MaTT,
       dn.MaDN, dn.TenDoanhNghiep, sv.MaSV, sv.HoTen AS HoTenSinhVien,
       dg.Diem, dg.NhanXet, dg.NgayDanhGia
FROM dbo.DanhGiaDoanhNghiep AS dg
JOIN dbo.ThucTap AS tt ON tt.MaThucTap = dg.MaThucTap
JOIN dbo.DonDangKy AS dd ON dd.MaDon = tt.MaDon
JOIN dbo.SinhVien AS sv ON sv.MaSV = dd.MaSV
JOIN dbo.ViTriThucTap AS vt ON vt.MaViTri = dd.MaViTri
JOIN dbo.DoanhNghiep AS dn ON dn.MaDN = vt.MaDN;
GO

CREATE OR ALTER VIEW dbo.vw_ThongKeTongQuan
AS
SELECT dt.MaDot, dt.TenDot,
       (SELECT COUNT(*) FROM dbo.ViTriThucTap vt
        WHERE vt.MaDot = dt.MaDot AND vt.TrangThai = N'DangTuyen') AS TongViTriDangTuyen,
       (SELECT COUNT(*) FROM dbo.DonDangKy dd
        JOIN dbo.ViTriThucTap vt ON vt.MaViTri = dd.MaViTri
        WHERE vt.MaDot = dt.MaDot) AS TongDonGui,
       (SELECT COUNT(*) FROM dbo.DonDangKy dd
        JOIN dbo.ViTriThucTap vt ON vt.MaViTri = dd.MaViTri
        WHERE vt.MaDot = dt.MaDot AND dd.TrangThai = N'DaDuyet') AS SoDonDaDuyet,
       (SELECT COUNT(*) FROM dbo.BaoCao bc
        JOIN dbo.ThucTap tt ON tt.MaThucTap = bc.MaThucTap
        JOIN dbo.DonDangKy dd ON dd.MaDon = tt.MaDon
        JOIN dbo.ViTriThucTap vt ON vt.MaViTri = dd.MaViTri
        WHERE vt.MaDot = dt.MaDot) AS SoBaoCaoDaNop
FROM dbo.DotThucTap AS dt;
GO

CREATE OR ALTER TRIGGER dbo.trg_CheckDuplicateNhatKy
ON dbo.NhatKy
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS
    (
        SELECT 1
        FROM dbo.NhatKy AS nk
        JOIN (SELECT DISTINCT MaThucTap, NgayGhiNgay FROM inserted) AS i
          ON i.MaThucTap = nk.MaThucTap AND i.NgayGhiNgay = nk.NgayGhiNgay
        GROUP BY nk.MaThucTap, nk.NgayGhiNgay
        HAVING COUNT(*) > 1
    )
    BEGIN
        ROLLBACK TRANSACTION;
        THROW 52001, N'Mỗi ngày chỉ được phép ghi 1 nhật ký thực tập.', 1;
    END;
END;
GO

CREATE OR ALTER TRIGGER dbo.trg_AutoUpdateTrangThaiViTri
ON dbo.ThucTap
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    ;WITH ViTriMoi AS
    (
        SELECT DISTINCT dd.MaViTri
        FROM inserted i JOIN dbo.DonDangKy dd ON dd.MaDon = i.MaDon
    ), SoLuongNhan AS
    (
        SELECT dd.MaViTri, COUNT(*) AS SoSinhVienDaNhan
        FROM dbo.ThucTap tt
        JOIN dbo.DonDangKy dd ON dd.MaDon = tt.MaDon
        JOIN ViTriMoi vm ON vm.MaViTri = dd.MaViTri
        GROUP BY dd.MaViTri
    )
    UPDATE vt SET TrangThai = N'NgungTuyen'
    FROM dbo.ViTriThucTap vt
    JOIN SoLuongNhan sl ON sl.MaViTri = vt.MaViTri
    WHERE vt.SoLuong > 0 AND sl.SoSinhVienDaNhan >= vt.SoLuong;
END;
GO

CREATE OR ALTER TRIGGER dbo.trg_ValidateDiemBaoCao
ON dbo.BaoCao
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM inserted WHERE Diem IS NOT NULL AND Diem NOT BETWEEN 0 AND 10)
    BEGIN
        ROLLBACK TRANSACTION;
        THROW 52003, N'Điểm báo cáo phải nằm trong khoảng từ 0 đến 10.', 1;
    END;
END;
GO

CREATE OR ALTER TRIGGER dbo.trg_ValidateDiemDanhGia
ON dbo.DanhGiaSinhVien
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (SELECT 1 FROM inserted WHERE Diem NOT BETWEEN 0 AND 10)
    BEGIN
        ROLLBACK TRANSACTION;
        THROW 52004, N'Điểm đánh giá sinh viên phải nằm trong khoảng từ 0 đến 10.', 1;
    END;
END;
GO

CREATE OR ALTER TRIGGER dbo.trg_PreventDeleteAccount
ON dbo.TaiKhoan
INSTEAD OF DELETE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE tk
    SET DangHoatDong = 0, NgayCapNhat = SYSDATETIME()
    FROM dbo.TaiKhoan tk
    JOIN deleted d ON d.MaTK = tk.MaTK
    WHERE EXISTS (SELECT 1 FROM dbo.SinhVien sv WHERE sv.MaTK = d.MaTK)
       OR EXISTS (SELECT 1 FROM dbo.GiangVien gv WHERE gv.MaTK = d.MaTK)
       OR EXISTS (SELECT 1 FROM dbo.DoanhNghiep dn WHERE dn.MaTK = d.MaTK);

    DECLARE @SoTaiKhoanDaKhoa INT = @@ROWCOUNT;

    DELETE tk
    FROM dbo.TaiKhoan tk
    JOIN deleted d ON d.MaTK = tk.MaTK
    WHERE NOT EXISTS (SELECT 1 FROM dbo.SinhVien sv WHERE sv.MaTK = d.MaTK)
      AND NOT EXISTS (SELECT 1 FROM dbo.GiangVien gv WHERE gv.MaTK = d.MaTK)
      AND NOT EXISTS (SELECT 1 FROM dbo.DoanhNghiep dn WHERE dn.MaTK = d.MaTK);

    IF @SoTaiKhoanDaKhoa > 0
        PRINT N'Tài khoản đã được chuyển trạng thái sang ngưng hoạt động.';
END;
GO
