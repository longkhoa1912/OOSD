USE [QLThucTapEDU06];
GO
SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

CREATE OR ALTER PROCEDURE dbo.sp_UC14_DanhSachBaoCao
    @MaTKGiangVien INT
AS
BEGIN
    SET NOCOUNT ON;

    BEGIN TRY
        IF @MaTKGiangVien IS NULL
            THROW 51173, N'Mã tài khoản giảng viên không được để trống.', 1;

        SELECT
            bc.MaBaoCao,
            bc.MaThucTap,
            sv.MaSV,
            sv.MSSV AS MaSoSinhVien,
            sv.HoTen AS HoTenSinhVien,
            bc.TieuDe,
            bc.DuongDanTep,
            bc.NgayNop,
            bc.Diem,
            bc.NhanXet,
            bc.TrangThai
        FROM dbo.BaoCao AS bc
        INNER JOIN dbo.ThucTap AS tt ON tt.MaThucTap = bc.MaThucTap
        INNER JOIN dbo.GiangVien AS gv ON gv.MaGV = tt.MaGV
        INNER JOIN dbo.DonDangKy AS dd ON dd.MaDon = tt.MaDon
        INNER JOIN dbo.SinhVien AS sv ON sv.MaSV = dd.MaSV
        WHERE gv.MaTK = @MaTKGiangVien
          AND bc.TrangThai = N'DaNop'
        ORDER BY bc.NgayNop DESC, bc.MaBaoCao DESC;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO
