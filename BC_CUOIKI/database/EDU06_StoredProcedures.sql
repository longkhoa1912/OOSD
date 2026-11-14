/*
  EDU-06 Stored Procedures, SQL Server.
  Requires the canonical QLThucTapEDU06 tables and EDU06_Views_Triggers.sql.
*/
USE [QLThucTapEDU06];
GO

/* UC01 - Login. The client passes the value matching MatKhauHash. */
CREATE OR ALTER PROCEDURE dbo.sp_UC01_DangNhap
    @TenDangNhap NVARCHAR(100),
    @MatKhauHash NVARCHAR(255)
AS
BEGIN
    SET NOCOUNT ON;
    SET XACT_ABORT ON;
    BEGIN TRY
        DECLARE @MaTK INT, @VaiTro NVARCHAR(20), @Email NVARCHAR(254), @HoatDong BIT;
        SELECT @MaTK = MaTK, @VaiTro = VaiTro, @Email = Email, @HoatDong = DangHoatDong
        FROM dbo.TaiKhoan
        WHERE TenDangNhap = @TenDangNhap AND MatKhauHash = @MatKhauHash;

        IF @MaTK IS NULL OR @HoatDong = 0
            THROW 51001, N'Tên đăng nhập hoặc mật khẩu không đúng, hoặc tài khoản đã bị khóa.', 1;

        SELECT @MaTK AS MaTK, @TenDangNhap AS TenDangNhap, @VaiTro AS VaiTro, @Email AS Email;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

/* UC02 - Register non-admin account. Profile creation is completed in UC03/admin flows. */
CREATE OR ALTER PROCEDURE dbo.sp_UC02_DangKy
    @TenDangNhap NVARCHAR(100), @MatKhauHash NVARCHAR(255),
    @VaiTro NVARCHAR(20), @Email NVARCHAR(254) = NULL,
    @MaSoSinhVien NVARCHAR(30) = NULL, @MaSoGiangVien NVARCHAR(30) = NULL,
    @HoTen NVARCHAR(200) = NULL, @Lop NVARCHAR(100) = NULL,
    @Nganh NVARCHAR(150) = NULL, @TenDoanhNghiep NVARCHAR(250) = NULL,
    @LinhVuc NVARCHAR(150) = NULL, @DiaChi NVARCHAR(500) = NULL,
    @SoDienThoai NVARCHAR(30) = NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        SET @TenDangNhap = NULLIF(LTRIM(RTRIM(@TenDangNhap)), N'');
        SET @VaiTro = NULLIF(LTRIM(RTRIM(@VaiTro)), N'');
        SET @Email = NULLIF(LTRIM(RTRIM(@Email)), N'');
        IF @TenDangNhap IS NULL OR NULLIF(LTRIM(RTRIM(@MatKhauHash)), N'') IS NULL
            THROW 51002, N'Tên đăng nhập và mật khẩu là bắt buộc.', 1;
        IF @VaiTro NOT IN (N'SinhVien', N'GiangVien', N'DoanhNghiep')
            THROW 51004, N'Vai trò đăng ký không hợp lệ.', 1;
        IF @VaiTro IN (N'SinhVien', N'GiangVien') AND
           (NULLIF(LTRIM(RTRIM(@HoTen)), N'') IS NULL OR
            (@VaiTro = N'SinhVien' AND NULLIF(LTRIM(RTRIM(@MaSoSinhVien)), N'') IS NULL) OR
            (@VaiTro = N'GiangVien' AND NULLIF(LTRIM(RTRIM(@MaSoGiangVien)), N'') IS NULL))
            THROW 51008, N'Hồ sơ cá nhân và mã sinh viên/giảng viên là bắt buộc.', 1;
        IF @VaiTro = N'DoanhNghiep' AND NULLIF(LTRIM(RTRIM(@TenDoanhNghiep)), N'') IS NULL
            THROW 51009, N'Tên doanh nghiệp là bắt buộc.', 1;

        BEGIN TRANSACTION;
        IF EXISTS (SELECT 1 FROM dbo.TaiKhoan WITH (UPDLOCK, HOLDLOCK) WHERE TenDangNhap = @TenDangNhap)
            THROW 51005, N'Tên đăng nhập đã tồn tại.', 1;
        IF @Email IS NOT NULL AND EXISTS (SELECT 1 FROM dbo.TaiKhoan WITH (UPDLOCK, HOLDLOCK) WHERE Email = @Email)
            THROW 51006, N'Email đã được sử dụng.', 1;

        INSERT dbo.TaiKhoan(TenDangNhap, MatKhauHash, VaiTro, Email)
        VALUES(@TenDangNhap, @MatKhauHash, @VaiTro, @Email);
        DECLARE @NewId INT = CONVERT(INT, SCOPE_IDENTITY());
        IF @VaiTro = N'SinhVien'
            INSERT dbo.SinhVien(MaTK, MSSV, HoTen, Lop, Nganh, SoDienThoai, DiaChi)
            VALUES(@NewId, @MaSoSinhVien, @HoTen, @Lop, @Nganh, @SoDienThoai, @DiaChi);
        ELSE IF @VaiTro = N'GiangVien'
            INSERT dbo.GiangVien(MaTK, MaSoGiangVien, HoTen, Khoa, SoDienThoai)
            VALUES(@NewId, @MaSoGiangVien, @HoTen, @Nganh, @SoDienThoai);
        ELSE
            INSERT dbo.DoanhNghiep(MaTK, TenDoanhNghiep, LinhVuc, DiaChi, SoDienThoai)
            VALUES(@NewId, @TenDoanhNghiep, @LinhVuc, @DiaChi, @SoDienThoai);
        COMMIT TRANSACTION;
        SELECT MaTK, TenDangNhap, VaiTro, Email, DangHoatDong, NgayTao
        FROM dbo.TaiKhoan WHERE MaTK = @NewId;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        IF ERROR_NUMBER() IN (2601, 2627) THROW 51007, N'Tên đăng nhập hoặc email đã tồn tại.', 1;
        THROW;
    END CATCH;
END;
GO

/* UC03 - Read student profile. */
CREATE OR ALTER PROCEDURE dbo.sp_UC03_XemHoSoCaNhan @MaTK INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT sv.MaSV, sv.MSSV, sv.HoTen, sv.Lop, sv.Nganh,
               sv.SoDienThoai, sv.DiaChi, tk.Email
        FROM dbo.SinhVien sv JOIN dbo.TaiKhoan tk ON tk.MaTK = sv.MaTK
        WHERE sv.MaTK = @MaTK AND tk.DangHoatDong = 1;
        IF @@ROWCOUNT = 0 THROW 51030, N'Không tìm thấy hồ sơ sinh viên đang hoạt động.', 1;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

/* UC03 - Update student profile and account email atomically. */
CREATE OR ALTER PROCEDURE dbo.sp_UC03_CapNhatHoSoCaNhan
    @MaTK INT, @HoTen NVARCHAR(200), @Lop NVARCHAR(100) = NULL,
    @Nganh NVARCHAR(150) = NULL, @SoDienThoai NVARCHAR(30) = NULL,
    @DiaChi NVARCHAR(500) = NULL, @Email NVARCHAR(254) = NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        SET @HoTen = NULLIF(LTRIM(RTRIM(@HoTen)), N'');
        SET @Email = NULLIF(LTRIM(RTRIM(@Email)), N'');
        IF @HoTen IS NULL THROW 51031, N'Họ tên không được để trống.', 1;
        IF @Email IS NOT NULL AND (@Email NOT LIKE N'%_@_%._%' OR @Email LIKE N'% %')
            THROW 51032, N'Email không đúng định dạng.', 1;

        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1 FROM dbo.SinhVien WITH (UPDLOCK, HOLDLOCK) WHERE MaTK = @MaTK)
            THROW 51030, N'Không tìm thấy hồ sơ sinh viên.', 1;
        IF @Email IS NOT NULL AND EXISTS
            (SELECT 1 FROM dbo.TaiKhoan WITH (UPDLOCK, HOLDLOCK) WHERE Email = @Email AND MaTK <> @MaTK)
            THROW 51033, N'Email đã được tài khoản khác sử dụng.', 1;

        UPDATE dbo.SinhVien SET HoTen = @HoTen, Lop = NULLIF(LTRIM(RTRIM(@Lop)), N''),
            Nganh = NULLIF(LTRIM(RTRIM(@Nganh)), N''),
            SoDienThoai = NULLIF(LTRIM(RTRIM(@SoDienThoai)), N''),
            DiaChi = NULLIF(LTRIM(RTRIM(@DiaChi)), N'') WHERE MaTK = @MaTK;
        UPDATE dbo.TaiKhoan SET Email = @Email, NgayCapNhat = SYSDATETIME() WHERE MaTK = @MaTK;
        COMMIT TRANSACTION;
        EXEC dbo.sp_UC03_XemHoSoCaNhan @MaTK;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        IF ERROR_NUMBER() IN (2601, 2627) THROW 51033, N'Email đã được tài khoản khác sử dụng.', 1;
        THROW;
    END CATCH;
END;
GO

/* UC04 - Search enterprises. */
CREATE OR ALTER PROCEDURE dbo.sp_UC04_TimKiemDoanhNghiep @TuKhoa NVARCHAR(200) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SET @TuKhoa = NULLIF(LTRIM(RTRIM(@TuKhoa)), N'');
        SELECT dn.MaDN, dn.TenDoanhNghiep, dn.LinhVuc, dn.DiaChi,
               dn.SoDienThoai, tk.Email, dn.Website, dn.MoTa
        FROM dbo.DoanhNghiep dn JOIN dbo.TaiKhoan tk ON tk.MaTK = dn.MaTK
        WHERE tk.DangHoatDong = 1
          AND (@TuKhoa IS NULL OR dn.TenDoanhNghiep LIKE N'%' + @TuKhoa + N'%'
               OR dn.LinhVuc LIKE N'%' + @TuKhoa + N'%'
               OR dn.DiaChi LIKE N'%' + @TuKhoa + N'%')
        ORDER BY dn.TenDoanhNghiep;
    END TRY
    BEGIN CATCH THROW; END CATCH;
END;
GO

/* UC05 - Browse/search internship positions. */
CREATE OR ALTER PROCEDURE dbo.sp_UC05_DanhSachViTri
    @TuKhoa NVARCHAR(200) = NULL, @MaDot INT = NULL, @ChiDangTuyen BIT = 1
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SET @TuKhoa = NULLIF(LTRIM(RTRIM(@TuKhoa)), N'');
        SELECT v.* FROM dbo.vw_DanhSachViTriThucTap v
        JOIN dbo.ViTriThucTap vt ON vt.MaViTri = v.MaVT
        WHERE (@MaDot IS NULL OR vt.MaDot = @MaDot)
          AND (@ChiDangTuyen = 0 OR v.TrangThai = N'DangTuyen')
          AND (@TuKhoa IS NULL OR v.TenViTri LIKE N'%' + @TuKhoa + N'%'
               OR v.TenDoanhNghiep LIKE N'%' + @TuKhoa + N'%'
               OR v.LinhVuc LIKE N'%' + @TuKhoa + N'%'
               OR v.DiaDiem LIKE N'%' + @TuKhoa + N'%')
        ORDER BY v.MaVT DESC;
    END TRY
    BEGIN CATCH THROW; END CATCH;
END;
GO

/* UC06 - Student applies to a position. */
CREATE OR ALTER PROCEDURE dbo.sp_UC06_DangKyThucTap
    @MaTKSinhVien INT, @MaViTri INT, @ThuGioiThieu NVARCHAR(MAX) = NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @MaSV INT, @TrangThaiViTri NVARCHAR(20), @SoLuong INT,
            @TrangThaiDot NVARCHAR(20), @HanDangKy DATETIME2(0),
            @BatDau DATETIME2(0), @KetThuc DATETIME2(0), @MaDon INT;
    BEGIN TRY
        BEGIN TRANSACTION;
        SELECT @MaSV = sv.MaSV FROM dbo.SinhVien sv
        JOIN dbo.TaiKhoan tk ON tk.MaTK = sv.MaTK
        WHERE sv.MaTK = @MaTKSinhVien AND tk.DangHoatDong = 1;
        IF @MaSV IS NULL THROW 51060, N'Không tìm thấy hồ sơ sinh viên đang hoạt động.', 1;

        SELECT @TrangThaiViTri = vt.TrangThai, @SoLuong = vt.SoLuong,
               @TrangThaiDot = dt.TrangThai, @HanDangKy = dt.HanDangKy,
               @BatDau = dt.NgayBatDau, @KetThuc = dt.NgayKetThuc
        FROM dbo.ViTriThucTap vt WITH (UPDLOCK, HOLDLOCK)
        JOIN dbo.DotThucTap dt ON dt.MaDot = vt.MaDot WHERE vt.MaViTri = @MaViTri;
        IF @TrangThaiViTri IS NULL THROW 51061, N'Không tìm thấy vị trí thực tập.', 1;
        IF @SoLuong <= 0 THROW 51062, N'Số lượng tuyển phải lớn hơn 0.', 1;
        IF @TrangThaiViTri <> N'DangTuyen' OR @TrangThaiDot <> N'DangMo'
            THROW 51063, N'Vị trí hoặc đợt thực tập hiện không nhận đăng ký.', 1;
        IF SYSDATETIME() < @BatDau OR SYSDATETIME() > @KetThuc
            THROW 51064, N'Hiện không nằm trong thời gian của đợt thực tập.', 1;
        IF @HanDangKy IS NOT NULL AND SYSDATETIME() > @HanDangKy
            THROW 51065, N'Đã hết hạn đăng ký.', 1;
        IF EXISTS (SELECT 1 FROM dbo.DonDangKy WITH (UPDLOCK, HOLDLOCK)
                   WHERE MaSV = @MaSV AND MaViTri = @MaViTri)
            THROW 51067, N'Sinh viên đã đăng ký vị trí này.', 1;

        INSERT dbo.DonDangKy(MaSV, MaViTri, ThuGioiThieu, TrangThai)
        VALUES(@MaSV, @MaViTri, @ThuGioiThieu, N'ChoDuyet');
        SET @MaDon = CONVERT(INT, SCOPE_IDENTITY());
        COMMIT TRANSACTION;
        SELECT MaDon, MaSV, MaViTri, NgayNop, TrangThai FROM dbo.DonDangKy WHERE MaDon = @MaDon;
    END TRY
    BEGIN CATCH
        IF XACT_STATE() <> 0 ROLLBACK TRANSACTION;
        IF ERROR_NUMBER() IN (2601, 2627) THROW 51067, N'Sinh viên đã đăng ký vị trí này.', 1;
        THROW;
    END CATCH;
END;
GO

/* UC07 - Student tracks own applications. */
CREATE OR ALTER PROCEDURE dbo.sp_UC07_TheoDoiDon @MaTKSinhVien INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT * FROM dbo.vw_DanhSachDonDangKy
        WHERE MaSV = (SELECT MaSV FROM dbo.SinhVien WHERE MaTK = @MaTKSinhVien)
        ORDER BY NgayNop DESC;
    END TRY
    BEGIN CATCH THROW; END CATCH;
END;
GO

/* UC08 - Employer manages owned positions: CREATE, UPDATE, STOP, REOPEN. */
CREATE OR ALTER PROCEDURE dbo.sp_UC08_QuanLyViTri
    @MaTKDoanhNghiep INT, @HanhDong NVARCHAR(20), @MaViTri INT = NULL,
    @MaDot INT = NULL, @TenViTri NVARCHAR(200) = NULL, @MoTa NVARCHAR(MAX) = NULL,
    @YeuCau NVARCHAR(MAX) = NULL, @SoLuong INT = NULL, @DiaDiem NVARCHAR(300) = NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @MaDN INT, @TrangThaiDot NVARCHAR(20), @DuocNhan INT;
    BEGIN TRY
        IF @HanhDong NOT IN (N'DANHSACH',N'TAO',N'CAPNHAT',N'NGUNG_TUYEN',N'MO_TUYEN')
            THROW 51080, N'Hành động không hợp lệ.', 1;
        BEGIN TRANSACTION;
        SELECT @MaDN = dn.MaDN FROM dbo.DoanhNghiep dn
        JOIN dbo.TaiKhoan tk ON tk.MaTK = dn.MaTK
        WHERE dn.MaTK = @MaTKDoanhNghiep AND tk.DangHoatDong = 1;
        IF @MaDN IS NULL THROW 51081, N'Tài khoản doanh nghiệp không hợp lệ.', 1;
        IF @HanhDong = N'DANHSACH'
        BEGIN
            COMMIT TRANSACTION;
            SELECT MaViTri, MaDN, MaDot, TenViTri, MoTa, YeuCau, SoLuong,
                   DiaDiem, TrangThai, NgayDang
            FROM dbo.ViTriThucTap WHERE MaDN = @MaDN ORDER BY NgayDang DESC;
            RETURN;
        END;

        IF @HanhDong = N'TAO'
        BEGIN
            IF NULLIF(LTRIM(RTRIM(@TenViTri)),N'') IS NULL OR @MaDot IS NULL OR @SoLuong IS NULL OR @SoLuong <= 0
                THROW 51082, N'Tên vị trí, đợt và số lượng tuyển hợp lệ là bắt buộc.', 1;
            SELECT @TrangThaiDot = TrangThai FROM dbo.DotThucTap WITH (UPDLOCK,HOLDLOCK) WHERE MaDot = @MaDot;
            IF @TrangThaiDot IS NULL OR @TrangThaiDot <> N'DangMo'
                THROW 51083, N'Đợt thực tập không mở tuyển.', 1;
            INSERT dbo.ViTriThucTap(MaDN,MaDot,TenViTri,MoTa,YeuCau,SoLuong,DiaDiem,TrangThai)
            VALUES(@MaDN,@MaDot,LTRIM(RTRIM(@TenViTri)),@MoTa,@YeuCau,@SoLuong,@DiaDiem,N'DangTuyen');
            SET @MaViTri = CONVERT(INT,SCOPE_IDENTITY());
        END
        ELSE
        BEGIN
            IF @MaViTri IS NULL OR NOT EXISTS
                (SELECT 1 FROM dbo.ViTriThucTap WITH (UPDLOCK,HOLDLOCK) WHERE MaViTri=@MaViTri AND MaDN=@MaDN)
                THROW 51084, N'Vị trí không thuộc doanh nghiệp này.', 1;
            IF @HanhDong = N'CAPNHAT'
            BEGIN
                IF NULLIF(LTRIM(RTRIM(@TenViTri)),N'') IS NULL OR @SoLuong IS NULL OR @SoLuong <= 0
                    THROW 51082, N'Tên vị trí và số lượng tuyển hợp lệ là bắt buộc.', 1;
                SELECT @DuocNhan = COUNT(*) FROM dbo.DonDangKy WHERE MaViTri=@MaViTri AND TrangThai=N'DaDuyet';
                IF @SoLuong < @DuocNhan THROW 51085, N'Số lượng mới thấp hơn số sinh viên đã được duyệt.', 1;
                UPDATE dbo.ViTriThucTap SET TenViTri=LTRIM(RTRIM(@TenViTri)),MoTa=@MoTa,
                    YeuCau=@YeuCau,SoLuong=@SoLuong,DiaDiem=@DiaDiem WHERE MaViTri=@MaViTri AND MaDN=@MaDN;
            END
            ELSE IF @HanhDong = N'NGUNG_TUYEN'
                UPDATE dbo.ViTriThucTap SET TrangThai=N'NgungTuyen' WHERE MaViTri=@MaViTri AND MaDN=@MaDN;
            ELSE
            BEGIN
                IF NOT EXISTS (SELECT 1 FROM dbo.ViTriThucTap vt JOIN dbo.DotThucTap dt ON dt.MaDot=vt.MaDot
                    WHERE vt.MaViTri=@MaViTri AND vt.MaDN=@MaDN AND vt.SoLuong>0 AND dt.TrangThai=N'DangMo')
                    THROW 51086, N'Đợt thực tập không mở hoặc số lượng tuyển không hợp lệ.', 1;
                UPDATE dbo.ViTriThucTap SET TrangThai=N'DangTuyen' WHERE MaViTri=@MaViTri AND MaDN=@MaDN;
            END
        END;
        COMMIT TRANSACTION;
        SELECT MaViTri,MaDN,MaDot,TenViTri,MoTa,YeuCau,SoLuong,DiaDiem,TrangThai,NgayDang
        FROM dbo.ViTriThucTap WHERE MaViTri=@MaViTri;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

/* UC09 - Employer approves/rejects an application, creates internship on approval. */
CREATE OR ALTER PROCEDURE dbo.sp_UC09_XetDuyetDon
    @MaTKDoanhNghiep INT, @MaDon INT, @TrangThaiDon NVARCHAR(20), @GhiChu NVARCHAR(MAX)=NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    DECLARE @MaViTri INT, @TrangThaiHienTai NVARCHAR(20), @SoLuong INT, @DaDuyet INT, @MaTT INT;
    BEGIN TRY
        IF @TrangThaiDon NOT IN (N'DaDuyet',N'TuChoi') THROW 51090, N'Kết quả duyệt không hợp lệ.', 1;
        BEGIN TRANSACTION;
        SELECT @MaViTri=dd.MaViTri,@TrangThaiHienTai=dd.TrangThai
        FROM dbo.DonDangKy dd WITH (UPDLOCK,HOLDLOCK)
        JOIN dbo.ViTriThucTap vt ON vt.MaViTri=dd.MaViTri
        JOIN dbo.DoanhNghiep dn ON dn.MaDN=vt.MaDN
        JOIN dbo.TaiKhoan tk ON tk.MaTK=dn.MaTK
        WHERE dd.MaDon=@MaDon AND dn.MaTK=@MaTKDoanhNghiep AND tk.DangHoatDong=1;
        IF @MaViTri IS NULL THROW 51091, N'Không tìm thấy đơn thuộc doanh nghiệp này.', 1;
        IF @TrangThaiHienTai<>N'ChoDuyet' THROW 51092, N'Đơn không còn ở trạng thái chờ duyệt.', 1;
        IF @TrangThaiDon=N'DaDuyet'
        BEGIN
            SELECT @SoLuong=SoLuong FROM dbo.ViTriThucTap WITH (UPDLOCK,HOLDLOCK) WHERE MaViTri=@MaViTri;
            SELECT @DaDuyet=COUNT(*) FROM dbo.DonDangKy WHERE MaViTri=@MaViTri AND TrangThai=N'DaDuyet';
            IF @SoLuong<=0 OR @DaDuyet>=@SoLuong THROW 51093, N'Vị trí đã tuyển đủ số lượng.', 1;
        END;
        UPDATE dbo.DonDangKy SET TrangThai=@TrangThaiDon,GhiChu=COALESCE(@GhiChu,GhiChu),NgayCapNhat=SYSDATETIME()
        WHERE MaDon=@MaDon;
        IF @TrangThaiDon=N'DaDuyet'
        BEGIN
            INSERT dbo.ThucTap(MaDon,TrangThai) VALUES(@MaDon,N'ChuaBatDau');
            SET @MaTT=CONVERT(INT,SCOPE_IDENTITY());
        END;
        COMMIT TRANSACTION;
        SELECT dd.MaDon,dd.MaSV,dd.MaViTri,dd.TrangThai,dd.GhiChu,@MaTT AS MaThucTap
        FROM dbo.DonDangKy dd WHERE dd.MaDon=@MaDon;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

/* UC09 - Employer lists only pending applications for positions it owns. */
CREATE OR ALTER PROCEDURE dbo.sp_UC09_DanhSachDonChoDuyet
    @MaTKDoanhNghiep INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.DoanhNghiep dn
                       JOIN dbo.TaiKhoan tk ON tk.MaTK = dn.MaTK
                       WHERE dn.MaTK = @MaTKDoanhNghiep AND tk.DangHoatDong = 1)
            THROW 51094, N'Tài khoản doanh nghiệp không hợp lệ hoặc đã bị khóa.', 1;

        SELECT * FROM dbo.vw_DanhSachDonDangKy
        WHERE MaDN = (SELECT MaDN FROM dbo.DoanhNghiep WHERE MaTK = @MaTKDoanhNghiep)
          AND TrangThaiDon = N'ChoDuyet'
        ORDER BY NgayNop;
    END TRY
    BEGIN CATCH
        THROW;
    END CATCH;
END;
GO

/* UC10 - Employer tracks interns at its own company. */
CREATE OR ALTER PROCEDURE dbo.sp_UC10_TheoDoiSinhVienDoanhNghiep @MaTKDoanhNghiep INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT v.* FROM dbo.vw_DanhSachThucTapChiTiet v
        JOIN dbo.DoanhNghiep dn ON dn.MaDN=v.MaDN
        WHERE dn.MaTK=@MaTKDoanhNghiep ORDER BY v.NgayBatDau DESC;
    END TRY
    BEGIN CATCH THROW; END CATCH;
END;
GO

/* UC11 - Employer evaluates a student on one internship. */
CREATE OR ALTER PROCEDURE dbo.sp_UC11_DanhGiaSinhVien
    @MaTKDoanhNghiep INT, @MaThucTap INT, @Diem DECIMAL(4,2), @NhanXet NVARCHAR(MAX)=NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        IF @Diem IS NULL OR @Diem NOT BETWEEN 0 AND 10 THROW 51180, N'Điểm phải từ 0 đến 10.', 1;
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1 FROM dbo.ThucTap tt JOIN dbo.DonDangKy dd ON dd.MaDon=tt.MaDon
            JOIN dbo.ViTriThucTap vt ON vt.MaViTri=dd.MaViTri JOIN dbo.DoanhNghiep dn ON dn.MaDN=vt.MaDN
            JOIN dbo.TaiKhoan tk ON tk.MaTK=dn.MaTK
            WHERE tt.MaThucTap=@MaThucTap AND dn.MaTK=@MaTKDoanhNghiep AND tk.DangHoatDong=1)
            THROW 51181, N'Hồ sơ thực tập không thuộc doanh nghiệp này.', 1;
        INSERT dbo.DanhGiaSinhVien(MaThucTap,Diem,NhanXet) VALUES(@MaThucTap,@Diem,@NhanXet);
        COMMIT TRANSACTION;
        SELECT MaDanhGiaSV,MaThucTap,Diem,NhanXet,NgayDanhGia FROM dbo.DanhGiaSinhVien WHERE MaThucTap=@MaThucTap;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK TRANSACTION;
        IF ERROR_NUMBER() IN(2601,2627) THROW 51183, N'Hồ sơ này đã có đánh giá sinh viên.', 1;
        THROW;
    END CATCH;
END;
GO

/* UC12 - Lecturer views assigned internships. */
CREATE OR ALTER PROCEDURE dbo.sp_UC12_SinhVienDuocPhanCong @MaTKGiangVien INT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT v.* FROM dbo.vw_DanhSachThucTapChiTiet v
        JOIN dbo.GiangVien gv ON gv.MaGV=v.MaGV
        WHERE gv.MaTK=@MaTKGiangVien ORDER BY v.HoTenSinhVien;
    END TRY
    BEGIN CATCH THROW; END CATCH;
END;
GO

/* UC13 - Lecturer monitors an assigned internship and its activity counts. */
CREATE OR ALTER PROCEDURE dbo.sp_UC13_TheoDoiTienDo @MaTKGiangVien INT, @MaThucTap INT=NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        SELECT v.*, (SELECT COUNT(*) FROM dbo.NhatKy nk WHERE nk.MaThucTap=v.MaTT) AS SoNhatKy,
               (SELECT COUNT(*) FROM dbo.BaoCao bc WHERE bc.MaThucTap=v.MaTT) AS SoBaoCao,
               (SELECT MAX(nk.NgayGhi) FROM dbo.NhatKy nk WHERE nk.MaThucTap=v.MaTT) AS NgayCapNhatNhatKy
        FROM dbo.vw_DanhSachThucTapChiTiet v JOIN dbo.GiangVien gv ON gv.MaGV=v.MaGV
        WHERE gv.MaTK=@MaTKGiangVien AND (@MaThucTap IS NULL OR v.MaTT=@MaThucTap);
    END TRY
    BEGIN CATCH THROW; END CATCH;
END;
GO

/* UC14 - Assigned lecturer grades a report. */
CREATE OR ALTER PROCEDURE dbo.sp_UC14_DanhGiaBaoCao
    @MaTKGiangVien INT, @MaBaoCao INT, @Diem DECIMAL(4,2), @NhanXet NVARCHAR(MAX)=NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        IF @Diem IS NULL OR @Diem NOT BETWEEN 0 AND 10 THROW 51170, N'Điểm báo cáo phải từ 0 đến 10.', 1;
        BEGIN TRANSACTION;
        UPDATE bc SET Diem=@Diem,NhanXet=@NhanXet,TrangThai=N'DaDanhGia',NgayDanhGia=SYSDATETIME()
        FROM dbo.BaoCao bc JOIN dbo.ThucTap tt ON tt.MaThucTap=bc.MaThucTap
        JOIN dbo.GiangVien gv ON gv.MaGV=tt.MaGV
        WHERE bc.MaBaoCao=@MaBaoCao AND gv.MaTK=@MaTKGiangVien;
        IF @@ROWCOUNT=0 THROW 51172, N'Không tìm thấy báo cáo thuộc sinh viên được phân công.', 1;
        COMMIT TRANSACTION;
        SELECT MaBaoCao,MaThucTap,Diem,NhanXet,TrangThai,NgayDanhGia FROM dbo.BaoCao WHERE MaBaoCao=@MaBaoCao;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

/* UC15 - Student submits a daily log. */
CREATE OR ALTER PROCEDURE dbo.sp_UC15_NopNhatKy
    @MaTKSinhVien INT, @MaThucTap INT, @NgayGhi DATETIME2(0), @NoiDung NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        IF @NgayGhi IS NULL OR NULLIF(LTRIM(RTRIM(@NoiDung)),N'') IS NULL
            THROW 51150, N'Ngày ghi và nội dung là bắt buộc.', 1;
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1 FROM dbo.ThucTap tt JOIN dbo.DonDangKy dd ON dd.MaDon=tt.MaDon
            JOIN dbo.SinhVien sv ON sv.MaSV=dd.MaSV JOIN dbo.TaiKhoan tk ON tk.MaTK=sv.MaTK
            WHERE tt.MaThucTap=@MaThucTap AND sv.MaTK=@MaTKSinhVien AND tk.DangHoatDong=1)
            THROW 51152, N'Hồ sơ thực tập không thuộc sinh viên này.', 1;
        IF EXISTS (SELECT 1 FROM dbo.ThucTap WHERE MaThucTap=@MaThucTap AND
            ((NgayBatDau IS NOT NULL AND CONVERT(DATE,@NgayGhi)<CONVERT(DATE,NgayBatDau)) OR
             (NgayKetThuc IS NOT NULL AND CONVERT(DATE,@NgayGhi)>CONVERT(DATE,NgayKetThuc))))
            THROW 51153, N'Ngày ghi ngoài thời gian thực tập.', 1;
        INSERT dbo.NhatKy(MaThucTap,NgayGhi,NoiDung) VALUES(@MaThucTap,@NgayGhi,@NoiDung);
        DECLARE @Id INT=CONVERT(INT,SCOPE_IDENTITY());
        COMMIT TRANSACTION;
        SELECT MaNhatKy,MaThucTap,NgayGhi,NoiDung FROM dbo.NhatKy WHERE MaNhatKy=@Id;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK TRANSACTION;
        IF ERROR_NUMBER() IN(2601,2627) THROW 51154, N'Mỗi ngày chỉ được ghi một nhật ký.', 1;
        THROW;
    END CATCH;
END;
GO

/* UC16 - Student submits a report. */
CREATE OR ALTER PROCEDURE dbo.sp_UC16_NopBaoCao
    @MaTKSinhVien INT, @MaThucTap INT, @TieuDe NVARCHAR(300),
    @NoiDung NVARCHAR(MAX)=NULL, @DuongDanTep NVARCHAR(1000)=NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        IF NULLIF(LTRIM(RTRIM(@TieuDe)),N'') IS NULL OR
           (NULLIF(LTRIM(RTRIM(@NoiDung)),N'') IS NULL AND NULLIF(LTRIM(RTRIM(@DuongDanTep)),N'') IS NULL)
            THROW 51160, N'Tiêu đề và nội dung hoặc đường dẫn tệp là bắt buộc.', 1;
        BEGIN TRANSACTION;
        IF NOT EXISTS (SELECT 1 FROM dbo.ThucTap tt JOIN dbo.DonDangKy dd ON dd.MaDon=tt.MaDon
            JOIN dbo.SinhVien sv ON sv.MaSV=dd.MaSV JOIN dbo.TaiKhoan tk ON tk.MaTK=sv.MaTK
            WHERE tt.MaThucTap=@MaThucTap AND sv.MaTK=@MaTKSinhVien AND tk.DangHoatDong=1)
            THROW 51163, N'Hồ sơ thực tập không thuộc sinh viên này.', 1;
        INSERT dbo.BaoCao(MaThucTap,TieuDe,NoiDung,DuongDanTep,TrangThai)
        VALUES(@MaThucTap,LTRIM(RTRIM(@TieuDe)),@NoiDung,@DuongDanTep,N'DaNop');
        DECLARE @Id INT=CONVERT(INT,SCOPE_IDENTITY());
        COMMIT TRANSACTION;
        SELECT MaBaoCao,MaThucTap,TieuDe,NgayNop,TrangThai FROM dbo.BaoCao WHERE MaBaoCao=@Id;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

/* UC17 - Administrator account administration. */
CREATE OR ALTER PROCEDURE dbo.sp_UC17_QuanLyTaiKhoan
    @MaTKAdmin INT, @HanhDong NVARCHAR(20), @MaTK INT=NULL,
    @TenDangNhap NVARCHAR(100)=NULL, @MatKhauHash NVARCHAR(255)=NULL,
    @VaiTro NVARCHAR(20)=NULL, @Email NVARCHAR(254)=NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        IF @HanhDong NOT IN(N'DANHSACH',N'TAO',N'CAPNHAT',N'KHOA',N'MOKHOA',N'XOA')
            THROW 51170, N'Hành động quản lý tài khoản không hợp lệ.', 1;
        IF NOT EXISTS(SELECT 1 FROM dbo.TaiKhoan WHERE MaTK=@MaTKAdmin AND VaiTro=N'Admin' AND DangHoatDong=1)
            THROW 51171, N'Chỉ Admin đang hoạt động mới được quản lý tài khoản.', 1;
        IF @HanhDong=N'DANHSACH'
        BEGIN
            SELECT MaTK,TenDangNhap,VaiTro,Email,DangHoatDong,NgayTao,NgayCapNhat FROM dbo.TaiKhoan ORDER BY MaTK;
            RETURN;
        END;
        BEGIN TRANSACTION;
        IF @HanhDong=N'TAO'
        BEGIN
            IF NULLIF(LTRIM(RTRIM(@TenDangNhap)),N'') IS NULL OR NULLIF(LTRIM(RTRIM(@MatKhauHash)),N'') IS NULL OR
               @VaiTro NOT IN(N'Admin',N'SinhVien',N'GiangVien',N'DoanhNghiep')
                THROW 51172, N'Tên đăng nhập, mật khẩu và vai trò hợp lệ là bắt buộc.', 1;
            INSERT dbo.TaiKhoan(TenDangNhap,MatKhauHash,VaiTro,Email)
            VALUES(LTRIM(RTRIM(@TenDangNhap)),@MatKhauHash,@VaiTro,NULLIF(LTRIM(RTRIM(@Email)),N''));
            SET @MaTK=CONVERT(INT,SCOPE_IDENTITY());
        END
        ELSE IF @HanhDong=N'CAPNHAT'
        BEGIN
            IF @MaTK IS NULL OR NOT EXISTS(SELECT 1 FROM dbo.TaiKhoan WHERE MaTK=@MaTK)
                THROW 51173, N'Không tìm thấy tài khoản.', 1;
            IF @TenDangNhap IS NULL THROW 51174, N'Tên đăng nhập là bắt buộc khi cập nhật.', 1;
            UPDATE dbo.TaiKhoan SET TenDangNhap=LTRIM(RTRIM(@TenDangNhap)),
                Email=NULLIF(LTRIM(RTRIM(@Email)),N''),
                MatKhauHash=COALESCE(NULLIF(@MatKhauHash,N''),MatKhauHash),NgayCapNhat=SYSDATETIME()
            WHERE MaTK=@MaTK;
        END
        ELSE IF @HanhDong=N'KHOA' OR @HanhDong=N'XOA'
        BEGIN
            IF @MaTK IS NULL OR NOT EXISTS(SELECT 1 FROM dbo.TaiKhoan WHERE MaTK=@MaTK)
                THROW 51173, N'Không tìm thấy tài khoản.', 1;
            IF EXISTS(SELECT 1 FROM dbo.TaiKhoan WHERE MaTK=@MaTK AND VaiTro=N'Admin') AND
               (SELECT COUNT(*) FROM dbo.TaiKhoan WHERE VaiTro=N'Admin' AND DangHoatDong=1)<=1
                THROW 51175, N'Không thể khóa tài khoản Admin đang hoạt động cuối cùng.', 1;
            UPDATE dbo.TaiKhoan SET DangHoatDong=0,NgayCapNhat=SYSDATETIME() WHERE MaTK=@MaTK;
        END
        ELSE
        BEGIN
            IF @MaTK IS NULL THROW 51173, N'Cần mã tài khoản.', 1;
            UPDATE dbo.TaiKhoan SET DangHoatDong=1,NgayCapNhat=SYSDATETIME() WHERE MaTK=@MaTK;
            IF @@ROWCOUNT=0 THROW 51173, N'Không tìm thấy tài khoản.', 1;
        END;
        COMMIT TRANSACTION;
        SELECT MaTK,TenDangNhap,VaiTro,Email,DangHoatDong,NgayTao,NgayCapNhat FROM dbo.TaiKhoan WHERE MaTK=@MaTK;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK TRANSACTION;
        IF ERROR_NUMBER() IN(2601,2627) THROW 51176, N'Tên đăng nhập hoặc email đã tồn tại.', 1;
        THROW;
    END CATCH;
END;
GO

/* UC18 - Administrator company management. */
CREATE OR ALTER PROCEDURE dbo.sp_UC18_QuanLyDoanhNghiep
    @MaTKAdmin INT, @HanhDong NVARCHAR(20), @MaDN INT=NULL, @MaTK INT=NULL,
    @MaSoThue NVARCHAR(30)=NULL, @TenDoanhNghiep NVARCHAR(250)=NULL,
    @LinhVuc NVARCHAR(150)=NULL, @DiaChi NVARCHAR(500)=NULL,
    @SoDienThoai NVARCHAR(30)=NULL, @Website NVARCHAR(500)=NULL, @MoTa NVARCHAR(MAX)=NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        IF NOT EXISTS(SELECT 1 FROM dbo.TaiKhoan WHERE MaTK=@MaTKAdmin AND VaiTro=N'Admin' AND DangHoatDong=1)
            THROW 51801, N'Chỉ Admin đang hoạt động mới được quản lý doanh nghiệp.', 1;
        IF @HanhDong=N'DANHSACH'
        BEGIN
            SELECT dn.*,tk.Email,tk.DangHoatDong FROM dbo.DoanhNghiep dn JOIN dbo.TaiKhoan tk ON tk.MaTK=dn.MaTK ORDER BY dn.TenDoanhNghiep;
            RETURN;
        END;
        BEGIN TRANSACTION;
        IF @HanhDong=N'TAO'
        BEGIN
            IF @MaTK IS NULL OR NULLIF(LTRIM(RTRIM(@TenDoanhNghiep)),N'') IS NULL
                THROW 51802, N'Tài khoản doanh nghiệp và tên doanh nghiệp là bắt buộc.', 1;
            IF NOT EXISTS(SELECT 1 FROM dbo.TaiKhoan WHERE MaTK=@MaTK AND VaiTro=N'DoanhNghiep')
                THROW 51803, N'Tài khoản phải có vai trò DoanhNghiep.', 1;
            INSERT dbo.DoanhNghiep(MaTK,MaSoThue,TenDoanhNghiep,LinhVuc,DiaChi,SoDienThoai,Website,MoTa)
            VALUES(@MaTK,@MaSoThue,@TenDoanhNghiep,@LinhVuc,@DiaChi,@SoDienThoai,@Website,@MoTa);
            SET @MaDN=CONVERT(INT,SCOPE_IDENTITY());
        END
        ELSE IF @HanhDong=N'CAPNHAT'
        BEGIN
            IF @MaDN IS NULL OR NULLIF(LTRIM(RTRIM(@TenDoanhNghiep)),N'') IS NULL
                THROW 51804, N'Mã và tên doanh nghiệp là bắt buộc.', 1;
            UPDATE dbo.DoanhNghiep SET MaSoThue=@MaSoThue,TenDoanhNghiep=@TenDoanhNghiep,
                LinhVuc=@LinhVuc,DiaChi=@DiaChi,SoDienThoai=@SoDienThoai,Website=@Website,MoTa=@MoTa WHERE MaDN=@MaDN;
            IF @@ROWCOUNT=0 THROW 51805, N'Không tìm thấy doanh nghiệp.', 1;
        END
        ELSE IF @HanhDong IN(N'KHOA',N'MOKHOA',N'XOA')
        BEGIN
            UPDATE tk SET DangHoatDong=CASE WHEN @HanhDong IN(N'KHOA',N'XOA') THEN 0 ELSE 1 END,NgayCapNhat=SYSDATETIME()
            FROM dbo.TaiKhoan tk JOIN dbo.DoanhNghiep dn ON dn.MaTK=tk.MaTK WHERE dn.MaDN=@MaDN;
            IF @@ROWCOUNT=0 THROW 51805, N'Không tìm thấy doanh nghiệp.', 1;
        END
        ELSE THROW 51806, N'Hành động không hợp lệ.', 1;
        COMMIT TRANSACTION;
        SELECT dn.*,tk.Email,tk.DangHoatDong FROM dbo.DoanhNghiep dn JOIN dbo.TaiKhoan tk ON tk.MaTK=dn.MaTK WHERE dn.MaDN=@MaDN;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK TRANSACTION;
        IF ERROR_NUMBER() IN(2601,2627) THROW 51807, N'Mã số thuế hoặc tài khoản đã tồn tại.', 1;
        THROW;
    END CATCH;
END;
GO

/* UC19 - Administrator lecturer management. */
CREATE OR ALTER PROCEDURE dbo.sp_UC19_QuanLyGiangVien
    @MaTKAdmin INT, @HanhDong NVARCHAR(20), @MaGV INT=NULL, @MaTK INT=NULL,
    @MaSoGiangVien NVARCHAR(30)=NULL, @HoTen NVARCHAR(200)=NULL,
    @Khoa NVARCHAR(150)=NULL, @SoDienThoai NVARCHAR(30)=NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        IF NOT EXISTS(SELECT 1 FROM dbo.TaiKhoan WHERE MaTK=@MaTKAdmin AND VaiTro=N'Admin' AND DangHoatDong=1)
            THROW 51901, N'Chỉ Admin đang hoạt động mới được quản lý giảng viên.', 1;
        IF @HanhDong=N'DANHSACH'
        BEGIN
            SELECT gv.*,tk.TenDangNhap,tk.Email,tk.DangHoatDong FROM dbo.GiangVien gv JOIN dbo.TaiKhoan tk ON tk.MaTK=gv.MaTK ORDER BY gv.HoTen;
            RETURN;
        END;
        BEGIN TRANSACTION;
        IF @HanhDong=N'TAO'
        BEGIN
            IF @MaTK IS NULL OR @MaSoGiangVien IS NULL OR NULLIF(LTRIM(RTRIM(@HoTen)),N'') IS NULL
                THROW 51902, N'Tài khoản, mã giảng viên và họ tên là bắt buộc.', 1;
            IF NOT EXISTS(SELECT 1 FROM dbo.TaiKhoan WHERE MaTK=@MaTK AND VaiTro=N'GiangVien')
                THROW 51903, N'Tài khoản phải có vai trò GiangVien.', 1;
            INSERT dbo.GiangVien(MaTK,MaSoGiangVien,HoTen,Khoa,SoDienThoai)
            VALUES(@MaTK,@MaSoGiangVien,@HoTen,@Khoa,@SoDienThoai);
            SET @MaGV=CONVERT(INT,SCOPE_IDENTITY());
        END
        ELSE IF @HanhDong=N'CAPNHAT'
        BEGIN
            UPDATE dbo.GiangVien SET MaSoGiangVien=@MaSoGiangVien,HoTen=@HoTen,Khoa=@Khoa,SoDienThoai=@SoDienThoai WHERE MaGV=@MaGV;
            IF @@ROWCOUNT=0 THROW 51904, N'Không tìm thấy giảng viên.', 1;
        END
        ELSE IF @HanhDong IN(N'KHOA',N'MOKHOA',N'XOA')
        BEGIN
            UPDATE tk SET DangHoatDong=CASE WHEN @HanhDong IN(N'KHOA',N'XOA') THEN 0 ELSE 1 END,NgayCapNhat=SYSDATETIME()
            FROM dbo.TaiKhoan tk JOIN dbo.GiangVien gv ON gv.MaTK=tk.MaTK WHERE gv.MaGV=@MaGV;
            IF @@ROWCOUNT=0 THROW 51904, N'Không tìm thấy giảng viên.', 1;
        END
        ELSE THROW 51905, N'Hành động không hợp lệ.', 1;
        COMMIT TRANSACTION;
        SELECT gv.*,tk.TenDangNhap,tk.Email,tk.DangHoatDong FROM dbo.GiangVien gv JOIN dbo.TaiKhoan tk ON tk.MaTK=gv.MaTK WHERE gv.MaGV=@MaGV;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK TRANSACTION;
        IF ERROR_NUMBER() IN(2601,2627) THROW 51906, N'Mã giảng viên hoặc tài khoản đã tồn tại.', 1;
        THROW;
    END CATCH;
END;
GO

/* UC20 - Administrator internship-period management. */
CREATE OR ALTER PROCEDURE dbo.sp_UC20_QuanLyDotThucTap
    @MaTKAdmin INT, @HanhDong NVARCHAR(20), @MaDot INT=NULL,
    @TenDot NVARCHAR(200)=NULL, @NgayBatDau DATETIME2(0)=NULL,
    @NgayKetThuc DATETIME2(0)=NULL, @HanDangKy DATETIME2(0)=NULL,
    @TrangThai NVARCHAR(20)=NULL, @MoTa NVARCHAR(MAX)=NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        IF NOT EXISTS(SELECT 1 FROM dbo.TaiKhoan WHERE MaTK=@MaTKAdmin AND VaiTro=N'Admin' AND DangHoatDong=1)
            THROW 52001, N'Chỉ Admin đang hoạt động mới được quản lý đợt thực tập.', 1;
        IF @HanhDong=N'DANHSACH'
        BEGIN SELECT * FROM dbo.DotThucTap ORDER BY NgayBatDau DESC; RETURN; END;
        IF @HanhDong NOT IN(N'TAO',N'CAPNHAT',N'DOITRANGTHAI',N'XOA') THROW 52002, N'Hành động không hợp lệ.', 1;
        BEGIN TRANSACTION;
        IF @HanhDong=N'TAO'
        BEGIN
            IF NULLIF(LTRIM(RTRIM(@TenDot)),N'') IS NULL OR @NgayBatDau IS NULL OR @NgayKetThuc IS NULL
                THROW 52003, N'Tên đợt và hai mốc thời gian là bắt buộc.', 1;
            IF @NgayKetThuc<@NgayBatDau OR (@HanDangKy IS NOT NULL AND @HanDangKy>@NgayKetThuc)
                THROW 52004, N'Thời gian đợt hoặc hạn đăng ký không hợp lệ.', 1;
            IF @TrangThai NOT IN(N'ChuaMo',N'DangMo',N'DaDong') SET @TrangThai=N'ChuaMo';
            INSERT dbo.DotThucTap(TenDot,NgayBatDau,NgayKetThuc,HanDangKy,TrangThai,MoTa)
            VALUES(@TenDot,@NgayBatDau,@NgayKetThuc,@HanDangKy,@TrangThai,@MoTa);
            SET @MaDot=CONVERT(INT,SCOPE_IDENTITY());
        END
        ELSE IF @HanhDong=N'CAPNHAT'
        BEGIN
            IF @MaDot IS NULL OR @TenDot IS NULL OR @NgayBatDau IS NULL OR @NgayKetThuc IS NULL OR @NgayKetThuc<@NgayBatDau
                THROW 52004, N'Dữ liệu cập nhật đợt thực tập không hợp lệ.', 1;
            UPDATE dbo.DotThucTap SET TenDot=@TenDot,NgayBatDau=@NgayBatDau,NgayKetThuc=@NgayKetThuc,
                HanDangKy=@HanDangKy,MoTa=@MoTa WHERE MaDot=@MaDot;
            IF @@ROWCOUNT=0 THROW 52005, N'Không tìm thấy đợt thực tập.', 1;
        END
        ELSE IF @HanhDong=N'XOA'
        BEGIN
            IF @MaDot IS NULL THROW 52005, N'Cần mã đợt thực tập để xóa.', 1;
            IF EXISTS(SELECT 1 FROM dbo.ViTriThucTap WHERE MaDot=@MaDot)
                THROW 52007, N'Đợt đã có vị trí liên quan; hãy chuyển trạng thái sang DaDong thay vì xóa.', 1;
            DELETE dbo.DotThucTap WHERE MaDot=@MaDot;
            IF @@ROWCOUNT=0 THROW 52005, N'Không tìm thấy đợt thực tập.', 1;
        END
        ELSE
        BEGIN
            IF @TrangThai NOT IN(N'ChuaMo',N'DangMo',N'DaDong') THROW 52006, N'Trạng thái đợt không hợp lệ.', 1;
            UPDATE dbo.DotThucTap SET TrangThai=@TrangThai WHERE MaDot=@MaDot;
            IF @@ROWCOUNT=0 THROW 52005, N'Không tìm thấy đợt thực tập.', 1;
        END;
        COMMIT TRANSACTION;
        SELECT * FROM dbo.DotThucTap WHERE MaDot=@MaDot;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO

/* UC21 - Administrator statistics. */
CREATE OR ALTER PROCEDURE dbo.sp_UC21_ThongKe
    @MaTKAdmin INT, @MaDot INT=NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        IF NOT EXISTS(SELECT 1 FROM dbo.TaiKhoan WHERE MaTK=@MaTKAdmin AND VaiTro=N'Admin' AND DangHoatDong=1)
            THROW 52101, N'Chỉ Admin đang hoạt động mới được xem thống kê.', 1;
        SELECT * FROM dbo.vw_ThongKeTongQuan WHERE @MaDot IS NULL OR MaDot=@MaDot ORDER BY MaDot DESC;
    END TRY
    BEGIN CATCH THROW; END CATCH;
END;
GO

/* UC22 - Student evaluates the enterprise for their internship. */
CREATE OR ALTER PROCEDURE dbo.sp_UC22_DanhGiaDoanhNghiep
    @MaTKSinhVien INT, @MaThucTap INT, @Diem DECIMAL(4,2), @NhanXet NVARCHAR(MAX)=NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        IF @Diem IS NULL OR @Diem NOT BETWEEN 0 AND 10 THROW 52201, N'Điểm đánh giá phải từ 0 đến 10.', 1;
        BEGIN TRANSACTION;
        IF NOT EXISTS(SELECT 1 FROM dbo.ThucTap tt JOIN dbo.DonDangKy dd ON dd.MaDon=tt.MaDon
            JOIN dbo.SinhVien sv ON sv.MaSV=dd.MaSV JOIN dbo.TaiKhoan tk ON tk.MaTK=sv.MaTK
            WHERE tt.MaThucTap=@MaThucTap AND sv.MaTK=@MaTKSinhVien AND tk.DangHoatDong=1)
            THROW 52202, N'Hồ sơ thực tập không thuộc sinh viên này.', 1;
        INSERT dbo.DanhGiaDoanhNghiep(MaThucTap,Diem,NhanXet) VALUES(@MaThucTap,@Diem,@NhanXet);
        COMMIT TRANSACTION;
        SELECT * FROM dbo.vw_DanhSachDanhGiaDoanhNghiep WHERE MaTT=@MaThucTap;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK TRANSACTION;
        IF ERROR_NUMBER() IN(2601,2627) THROW 52203, N'Hồ sơ này đã có đánh giá doanh nghiệp.', 1;
        THROW;
    END CATCH;
END;
GO

/* UC23 - Admin lists, updates, and deletes enterprise reviews. */
CREATE OR ALTER PROCEDURE dbo.sp_UC23_QuanLyDanhGiaDoanhNghiep
    @MaTKAdmin INT, @HanhDong NVARCHAR(20)=N'DANHSACH',
    @MaDGDN INT=NULL, @Diem DECIMAL(4,2)=NULL,
    @NhanXet NVARCHAR(MAX)=NULL, @TuKhoa NVARCHAR(200)=NULL
AS
BEGIN
    SET NOCOUNT ON; SET XACT_ABORT ON;
    BEGIN TRY
        IF NOT EXISTS (SELECT 1 FROM dbo.TaiKhoan
                       WHERE MaTK=@MaTKAdmin AND VaiTro=N'Admin' AND DangHoatDong=1)
            THROW 52301, N'Chỉ Admin đang hoạt động mới được quản lý đánh giá doanh nghiệp.', 1;

        SET @TuKhoa=NULLIF(LTRIM(RTRIM(@TuKhoa)),N'');
        IF @HanhDong=N'DANHSACH'
        BEGIN
            SELECT * FROM dbo.vw_DanhSachDanhGiaDoanhNghiep
            WHERE @TuKhoa IS NULL OR TenDoanhNghiep LIKE N'%'+@TuKhoa+N'%'
               OR HoTenSinhVien LIKE N'%'+@TuKhoa+N'%'
            ORDER BY NgayDanhGia DESC;
            RETURN;
        END;
        IF @HanhDong NOT IN(N'CAPNHAT',N'XOA')
            THROW 52302, N'Hành động quản lý đánh giá không hợp lệ.', 1;
        IF @MaDGDN IS NULL OR NOT EXISTS(SELECT 1 FROM dbo.DanhGiaDoanhNghiep WHERE MaDanhGiaDN=@MaDGDN)
            THROW 52303, N'Không tìm thấy đánh giá doanh nghiệp.', 1;

        BEGIN TRANSACTION;
        IF @HanhDong=N'CAPNHAT'
        BEGIN
            IF @Diem IS NULL OR @Diem NOT BETWEEN 0 AND 10
                THROW 52304, N'Điểm đánh giá phải nằm trong khoảng 0 đến 10.', 1;
            UPDATE dbo.DanhGiaDoanhNghiep
            SET Diem=@Diem, NhanXet=@NhanXet, NgayDanhGia=SYSDATETIME()
            WHERE MaDanhGiaDN=@MaDGDN;
        END
        ELSE
        BEGIN
            DELETE dbo.DanhGiaDoanhNghiep WHERE MaDanhGiaDN=@MaDGDN;
        END;
        COMMIT TRANSACTION;
        SELECT * FROM dbo.vw_DanhSachDanhGiaDoanhNghiep
        WHERE (@HanhDong=N'CAPNHAT' AND MaDGDN=@MaDGDN)
           OR (@HanhDong=N'XOA' AND (@TuKhoa IS NULL OR TenDoanhNghiep LIKE N'%'+@TuKhoa+N'%' OR HoTenSinhVien LIKE N'%'+@TuKhoa+N'%'))
        ORDER BY NgayDanhGia DESC;
    END TRY
    BEGIN CATCH
        IF XACT_STATE()<>0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH;
END;
GO
