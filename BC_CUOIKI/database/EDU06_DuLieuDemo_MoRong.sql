/*
  EDU-06 demo data expansion for QLThucTapEDU06.
  Safe to run more than once: rows are inserted only when their demo key is absent.
  Adds sample users, positions, applications, internships, logs, reports and reviews.
*/
USE [QLThucTapEDU06];
SET NOCOUNT ON;
SET XACT_ABORT ON;
SET ANSI_NULLS ON;
SET QUOTED_IDENTIFIER ON;
SET ANSI_PADDING ON;
SET ANSI_WARNINGS ON;
SET CONCAT_NULL_YIELDS_NULL ON;
SET ARITHABORT ON;
SET NUMERIC_ROUNDABORT OFF;

BEGIN TRY
    BEGIN TRANSACTION;

    DECLARE @MaDot INT;
    SELECT TOP (1) @MaDot = MaDot
    FROM dbo.DotThucTap
    WHERE TrangThai = N'DangMo'
    ORDER BY NgayBatDau DESC, MaDot DESC;

    IF @MaDot IS NULL
        THROW 53001, N'Cần có ít nhất một đợt thực tập đang mở trước khi thêm dữ liệu demo.', 1;

    DECLARE @Students TABLE
    (
        TenDangNhap NVARCHAR(100), Email NVARCHAR(254), MSSV NVARCHAR(30),
        HoTen NVARCHAR(200), Lop NVARCHAR(100), Nganh NVARCHAR(150), SoDienThoai NVARCHAR(30)
    );
    INSERT @Students VALUES
      (N'sinhvien.demo03',N'sv03.demo@edu06.local',N'EDU06-SV-003',N'Nguyễn Minh Anh',N'CNTT-K17',N'Công nghệ phần mềm',N'0901000003'),
      (N'sinhvien.demo04',N'sv04.demo@edu06.local',N'EDU06-SV-004',N'Lê Hoàng Nam',N'CNTT-K17',N'Hệ thống thông tin',N'0901000004'),
      (N'sinhvien.demo05',N'sv05.demo@edu06.local',N'EDU06-SV-005',N'Phạm Ngọc Hà',N'CNTT-K18',N'Khoa học dữ liệu',N'0901000005'),
      (N'sinhvien.demo06',N'sv06.demo@edu06.local',N'EDU06-SV-006',N'Võ Thành Đạt',N'CNTT-K18',N'An toàn thông tin',N'0901000006'),
      (N'sinhvien.demo07',N'sv07.demo@edu06.local',N'EDU06-SV-007',N'Đặng Thùy Linh',N'CNTT-K17',N'Công nghệ phần mềm',N'0901000007'),
      (N'sinhvien.demo08',N'sv08.demo@edu06.local',N'EDU06-SV-008',N'Bùi Gia Huy',N'CNTT-K18',N'Mạng máy tính',N'0901000008'),
      (N'sinhvien.demo09',N'sv09.demo@edu06.local',N'EDU06-SV-009',N'Đỗ Khánh Vy',N'CNTT-K17',N'Hệ thống thông tin',N'0901000009'),
      (N'sinhvien.demo10',N'sv10.demo@edu06.local',N'EDU06-SV-010',N'Phan Quốc Bảo',N'CNTT-K18',N'Khoa học dữ liệu',N'0901000010'),
      (N'sinhvien.demo11',N'sv11.demo@edu06.local',N'EDU06-SV-011',N'Hoàng Yến Nhi',N'CNTT-K17',N'Thương mại điện tử',N'0901000011'),
      (N'sinhvien.demo12',N'sv12.demo@edu06.local',N'EDU06-SV-012',N'Trần Đức Minh',N'CNTT-K18',N'Công nghệ phần mềm',N'0901000012');

    INSERT dbo.TaiKhoan(TenDangNhap,MatKhauHash,VaiTro,Email)
    SELECT s.TenDangNhap,N'Demo@123',N'SinhVien',s.Email
    FROM @Students s
    WHERE NOT EXISTS (SELECT 1 FROM dbo.TaiKhoan t WHERE t.TenDangNhap=s.TenDangNhap);

    INSERT dbo.SinhVien(MaTK,MSSV,HoTen,Lop,Nganh,SoDienThoai)
    SELECT t.MaTK,s.MSSV,s.HoTen,s.Lop,s.Nganh,s.SoDienThoai
    FROM @Students s JOIN dbo.TaiKhoan t ON t.TenDangNhap=s.TenDangNhap
    WHERE NOT EXISTS (SELECT 1 FROM dbo.SinhVien sv WHERE sv.MaTK=t.MaTK OR sv.MSSV=s.MSSV);

    DECLARE @Companies TABLE
    (
        TenDangNhap NVARCHAR(100), Email NVARCHAR(254), MaSoThue NVARCHAR(30),
        TenDoanhNghiep NVARCHAR(250), LinhVuc NVARCHAR(150), DiaChi NVARCHAR(500), SoDienThoai NVARCHAR(30)
    );
    INSERT @Companies VALUES
      (N'doanhnghiep.demo3',N'cty3.demo@edu06.local',N'EDU06-DEMO-MST-003',N'Công ty Phần mềm Ánh Dương',N'Phần mềm',N'TP. Hồ Chí Minh',N'02873000003'),
      (N'doanhnghiep.demo4',N'cty4.demo@edu06.local',N'EDU06-DEMO-MST-004',N'Công ty Công nghệ Mây Việt',N'Điện toán đám mây',N'TP. Hồ Chí Minh',N'02873000004'),
      (N'doanhnghiep.demo5',N'cty5.demo@edu06.local',N'EDU06-DEMO-MST-005',N'Công ty Giải pháp Số Tương Lai',N'Giải pháp số',N'Bình Dương',N'02743000005');

    INSERT dbo.TaiKhoan(TenDangNhap,MatKhauHash,VaiTro,Email)
    SELECT c.TenDangNhap,N'Demo@123',N'DoanhNghiep',c.Email
    FROM @Companies c
    WHERE NOT EXISTS (SELECT 1 FROM dbo.TaiKhoan t WHERE t.TenDangNhap=c.TenDangNhap);

    INSERT dbo.DoanhNghiep(MaTK,MaSoThue,TenDoanhNghiep,LinhVuc,DiaChi,SoDienThoai,Website,MoTa)
    SELECT t.MaTK,c.MaSoThue,c.TenDoanhNghiep,c.LinhVuc,c.DiaChi,c.SoDienThoai,
           N'https://example.edu06.local',N'Dữ liệu doanh nghiệp mẫu phục vụ trình diễn đồ án EDU-06.'
    FROM @Companies c JOIN dbo.TaiKhoan t ON t.TenDangNhap=c.TenDangNhap
    WHERE NOT EXISTS (SELECT 1 FROM dbo.DoanhNghiep d WHERE d.MaTK=t.MaTK OR d.MaSoThue=c.MaSoThue);

    DECLARE @Lecturers TABLE
    (
        TenDangNhap NVARCHAR(100), Email NVARCHAR(254), MaSoGiangVien NVARCHAR(30),
        HoTen NVARCHAR(200), Khoa NVARCHAR(150), SoDienThoai NVARCHAR(30)
    );
    INSERT @Lecturers VALUES
      (N'giangvien.demo2',N'gv02.demo@edu06.local',N'EDU06-GV-002',N'Nguyễn Thu Trang',N'Công nghệ thông tin',N'0902000002'),
      (N'giangvien.demo3',N'gv03.demo@edu06.local',N'EDU06-GV-003',N'Lê Quốc Hưng',N'Hệ thống thông tin',N'0902000003');

    INSERT dbo.TaiKhoan(TenDangNhap,MatKhauHash,VaiTro,Email)
    SELECT g.TenDangNhap,N'Demo@123',N'GiangVien',g.Email
    FROM @Lecturers g
    WHERE NOT EXISTS (SELECT 1 FROM dbo.TaiKhoan t WHERE t.TenDangNhap=g.TenDangNhap);

    INSERT dbo.GiangVien(MaTK,MaSoGiangVien,HoTen,Khoa,SoDienThoai)
    SELECT t.MaTK,g.MaSoGiangVien,g.HoTen,g.Khoa,g.SoDienThoai
    FROM @Lecturers g JOIN dbo.TaiKhoan t ON t.TenDangNhap=g.TenDangNhap
    WHERE NOT EXISTS (SELECT 1 FROM dbo.GiangVien gv WHERE gv.MaTK=t.MaTK OR gv.MaSoGiangVien=g.MaSoGiangVien);

    DECLARE @Positions TABLE
    (
        TenDangNhapDN NVARCHAR(100), TenViTri NVARCHAR(200), MoTa NVARCHAR(MAX),
        YeuCau NVARCHAR(MAX), SoLuong INT, DiaDiem NVARCHAR(300)
    );
    INSERT @Positions VALUES
      (N'doanhnghiep.demo1',N'EDU06 DEMO - Java Backend Intern',N'Hỗ trợ xây dựng API và dịch vụ cho ứng dụng doanh nghiệp.',N'Java, Spring Boot, SQL cơ bản.',6,N'TP. Hồ Chí Minh'),
      (N'doanhnghiep.demo1',N'EDU06 DEMO - QA Automation Intern',N'Thực hành kiểm thử chức năng và tự động hóa kiểm thử.',N'Tư duy logic, biết một ngôn ngữ lập trình.',5,N'TP. Hồ Chí Minh'),
      (N'doanhnghiep.demo1',N'EDU06 DEMO - UI UX Design Intern',N'Tham gia thiết kế giao diện và luồng trải nghiệm người dùng.',N'Biết Figma, có tư duy thiết kế.',4,N'TP. Hồ Chí Minh'),
      (N'doanhnghiep.demo1',N'EDU06 DEMO - Business Analyst Intern',N'Hỗ trợ thu thập yêu cầu và viết tài liệu nghiệp vụ.',N'Giao tiếp tốt, tư duy phân tích.',4,N'TP. Hồ Chí Minh'),
      (N'doanhnghiep.demo2',N'EDU06 DEMO - Data Analyst Intern',N'Phân tích dữ liệu và trình bày kết quả cho nhóm sản phẩm.',N'SQL, Excel; biết Power BI là lợi thế.',5,N'TP. Hồ Chí Minh'),
      (N'doanhnghiep.demo2',N'EDU06 DEMO - Data Engineer Intern',N'Hỗ trợ xây dựng quy trình thu thập và xử lý dữ liệu.',N'Python, SQL, kiến thức cơ sở dữ liệu.',4,N'TP. Hồ Chí Minh'),
      (N'doanhnghiep.demo2',N'EDU06 DEMO - Machine Learning Intern',N'Thử nghiệm mô hình học máy trên bộ dữ liệu mẫu.',N'Python, xác suất thống kê cơ bản.',3,N'TP. Hồ Chí Minh'),
      (N'doanhnghiep.demo2',N'EDU06 DEMO - Database Intern',N'Thực hành thiết kế và tối ưu truy vấn cơ sở dữ liệu.',N'SQL Server, chuẩn hóa dữ liệu.',4,N'TP. Hồ Chí Minh'),
      (N'doanhnghiep.demo3',N'EDU06 DEMO - Frontend React Intern',N'Xây dựng các thành phần giao diện cho cổng dịch vụ.',N'HTML, CSS, JavaScript; biết React là lợi thế.',5,N'TP. Hồ Chí Minh'),
      (N'doanhnghiep.demo3',N'EDU06 DEMO - Mobile Flutter Intern',N'Tham gia phát triển ứng dụng di động đa nền tảng.',N'Dart cơ bản, có sản phẩm học tập.',3,N'TP. Hồ Chí Minh'),
      (N'doanhnghiep.demo3',N'EDU06 DEMO - DevOps Intern',N'Tìm hiểu quy trình triển khai và giám sát ứng dụng.',N'Linux, Git, Docker cơ bản.',3,N'TP. Hồ Chí Minh'),
      (N'doanhnghiep.demo3',N'EDU06 DEMO - Security Intern',N'Hỗ trợ rà soát cấu hình và thực hành bảo mật ứng dụng.',N'Mạng máy tính, kiến thức bảo mật cơ bản.',2,N'TP. Hồ Chí Minh'),
      (N'doanhnghiep.demo4',N'EDU06 DEMO - Cloud Engineer Intern',N'Thực hành triển khai dịch vụ mẫu trên nền tảng đám mây.',N'Linux, mạng máy tính, kiến thức cloud cơ bản.',4,N'TP. Hồ Chí Minh'),
      (N'doanhnghiep.demo4',N'EDU06 DEMO - Product Operations Intern',N'Hỗ trợ theo dõi phản hồi và vận hành sản phẩm số.',N'Excel, giao tiếp và phối hợp nhóm tốt.',3,N'TP. Hồ Chí Minh'),
      (N'doanhnghiep.demo4',N'EDU06 DEMO - Data Visualization Intern',N'Tạo báo cáo và biểu đồ trực quan từ dữ liệu nghiệp vụ.',N'SQL, Power BI hoặc công cụ tương đương.',4,N'TP. Hồ Chí Minh'),
      (N'doanhnghiep.demo4',N'EDU06 DEMO - Content Technology Intern',N'Biên soạn nội dung hướng dẫn cho nền tảng công nghệ.',N'Viết rõ ràng, quan tâm đến sản phẩm số.',3,N'TP. Hồ Chí Minh'),
      (N'doanhnghiep.demo5',N'EDU06 DEMO - ERP Consultant Intern',N'Tìm hiểu quy trình doanh nghiệp và cấu hình hệ thống ERP.',N'Tư duy quy trình, Excel và SQL cơ bản.',4,N'Bình Dương'),
      (N'doanhnghiep.demo5',N'EDU06 DEMO - Cybersecurity Intern',N'Thực hành kiểm tra an toàn trên môi trường lab.',N'Mạng máy tính, Linux và bảo mật cơ bản.',3,N'Bình Dương'),
      (N'doanhnghiep.demo5',N'EDU06 DEMO - Customer Success Intern',N'Hỗ trợ tiếp nhận yêu cầu và hướng dẫn khách hàng sử dụng hệ thống.',N'Giao tiếp tốt, sử dụng công cụ văn phòng.',4,N'Bình Dương'),
      (N'doanhnghiep.demo5',N'EDU06 DEMO - Software Support Intern',N'Phân tích lỗi phần mềm và phối hợp nhóm kỹ thuật xử lý.',N'Kiến thức lập trình và cơ sở dữ liệu cơ bản.',4,N'Bình Dương');

    INSERT dbo.ViTriThucTap(MaDN,MaDot,TenViTri,MoTa,YeuCau,SoLuong,DiaDiem,TrangThai)
    SELECT dn.MaDN,@MaDot,p.TenViTri,p.MoTa,p.YeuCau,p.SoLuong,p.DiaDiem,N'DangTuyen'
    FROM @Positions p
    JOIN dbo.TaiKhoan tk ON tk.TenDangNhap=p.TenDangNhapDN
    JOIN dbo.DoanhNghiep dn ON dn.MaTK=tk.MaTK
    WHERE NOT EXISTS
      (SELECT 1 FROM dbo.ViTriThucTap old WHERE old.MaDN=dn.MaDN AND old.MaDot=@MaDot AND old.TenViTri=p.TenViTri);

    DECLARE @DemoPositions TABLE (MaViTri INT PRIMARY KEY, Seq INT NOT NULL);
    INSERT @DemoPositions(MaViTri,Seq)
    SELECT MaViTri,ROW_NUMBER() OVER(ORDER BY MaViTri)
    FROM dbo.ViTriThucTap
    WHERE MaDot=@MaDot AND TenViTri LIKE N'EDU06 DEMO - %';
    DECLARE @PositionCount INT=(SELECT COUNT(*) FROM @DemoPositions);
    IF @PositionCount < 12
        THROW 53002, N'Không tìm thấy đủ vị trí demo để tạo hồ sơ ứng tuyển.', 1;

    DECLARE @DemoStudents TABLE (MaSV INT PRIMARY KEY, Seq INT NOT NULL);
    INSERT @DemoStudents(MaSV,Seq)
    SELECT sv.MaSV,ROW_NUMBER() OVER(ORDER BY sv.MaSV)
    FROM dbo.SinhVien sv JOIN dbo.TaiKhoan tk ON tk.MaTK=sv.MaTK
    WHERE tk.TenDangNhap LIKE N'sinhvien.demo%';

    DECLARE @Choices TABLE (N INT PRIMARY KEY);
    INSERT @Choices VALUES (1),(2),(3);

    INSERT dbo.DonDangKy(MaSV,MaViTri,ThuGioiThieu,TrangThai,GhiChu)
    SELECT s.MaSV,p.MaViTri,
           N'Dữ liệu demo EDU-06: sinh viên mong muốn được học hỏi và vận dụng kiến thức vào công việc thực tế.',
           CASE c.N WHEN 1 THEN N'DaDuyet' WHEN 2 THEN N'ChoDuyet' ELSE N'TuChoi' END,
           N'Hồ sơ minh họa cho giao diện web.'
    FROM @DemoStudents s
    CROSS JOIN @Choices c
    JOIN @DemoPositions p ON p.Seq=((s.Seq-1)*3+c.N-1)%@PositionCount+1
    WHERE NOT EXISTS (SELECT 1 FROM dbo.DonDangKy old WHERE old.MaSV=s.MaSV AND old.MaViTri=p.MaViTri);

    DECLARE @DemoGV TABLE (MaGV INT PRIMARY KEY, Seq INT NOT NULL);
    INSERT @DemoGV(MaGV,Seq)
    SELECT MaGV,ROW_NUMBER() OVER(ORDER BY MaGV) FROM dbo.GiangVien;
    DECLARE @GVCount INT=(SELECT COUNT(*) FROM @DemoGV);

    ;WITH Approved AS
    (
        SELECT dd.MaDon,ROW_NUMBER() OVER(ORDER BY dd.MaDon) AS Seq
        FROM dbo.DonDangKy dd
        JOIN dbo.ViTriThucTap vt ON vt.MaViTri=dd.MaViTri
        WHERE vt.MaDot=@MaDot AND vt.TenViTri LIKE N'EDU06 DEMO - %'
          AND dd.TrangThai=N'DaDuyet'
          AND NOT EXISTS (SELECT 1 FROM dbo.ThucTap tt WHERE tt.MaDon=dd.MaDon)
    )
    INSERT dbo.ThucTap(MaDon,MaGV,NgayBatDau,NgayKetThuc,TrangThai,GhiChu)
    SELECT a.MaDon,g.MaGV,DATEADD(day,-(a.Seq%10),SYSDATETIME()),DATEADD(month,3,SYSDATETIME()),
           N'DangThucTap',N'Hồ sơ thực tập mẫu phục vụ trình diễn EDU-06.'
    FROM Approved a
    JOIN @DemoGV g ON g.Seq=((a.Seq-1)%NULLIF(@GVCount,0))+1;

    DECLARE @DemoInternships TABLE (MaThucTap INT PRIMARY KEY, Seq INT NOT NULL);
    INSERT @DemoInternships(MaThucTap,Seq)
    SELECT tt.MaThucTap,ROW_NUMBER() OVER(ORDER BY tt.MaThucTap)
    FROM dbo.ThucTap tt
    JOIN dbo.DonDangKy dd ON dd.MaDon=tt.MaDon
    JOIN dbo.ViTriThucTap vt ON vt.MaViTri=dd.MaViTri
    WHERE vt.MaDot=@MaDot AND vt.TenViTri LIKE N'EDU06 DEMO - %';

    INSERT dbo.NhatKy(MaThucTap,NgayGhi,NoiDung)
    SELECT i.MaThucTap,DATEADD(day,-c.N,CONVERT(DATETIME2(0),CONVERT(DATE,SYSDATETIME()))),
           CASE c.N WHEN 1 THEN N'Dữ liệu demo: hoàn thành công việc được giao và cập nhật tiến độ.'
                    ELSE N'Dữ liệu demo: tham gia họp nhóm, tìm hiểu quy trình và ghi nhận kết quả.' END
    FROM @DemoInternships i CROSS JOIN @Choices c
    WHERE c.N IN (1,2)
      AND NOT EXISTS
        (SELECT 1 FROM dbo.NhatKy n WHERE n.MaThucTap=i.MaThucTap
         AND n.NgayGhiNgay=CONVERT(DATE,DATEADD(day,-c.N,SYSDATETIME())));

    INSERT dbo.BaoCao(MaThucTap,TieuDe,NoiDung,DuongDanTep,NgayNop,Diem,NhanXet,TrangThai,NgayDanhGia)
    SELECT i.MaThucTap,N'Báo cáo demo EDU06 - kỳ thực tập',
           N'Nội dung báo cáo mẫu: mục tiêu, công việc đã thực hiện, kết quả và bài học kinh nghiệm.',
           N'https://example.edu06.local/bao-cao-demo',DATEADD(day,-1,SYSDATETIME()),
           CASE WHEN i.Seq%2=0 THEN CONVERT(DECIMAL(4,2),8.50) ELSE NULL END,
           CASE WHEN i.Seq%2=0 THEN N'Báo cáo mẫu đã được đánh giá.' ELSE NULL END,
           CASE WHEN i.Seq%2=0 THEN N'DaDanhGia' ELSE N'DaNop' END,
           CASE WHEN i.Seq%2=0 THEN SYSDATETIME() ELSE NULL END
    FROM @DemoInternships i
    WHERE NOT EXISTS (SELECT 1 FROM dbo.BaoCao b WHERE b.MaThucTap=i.MaThucTap AND b.TieuDe=N'Báo cáo demo EDU06 - kỳ thực tập');

    INSERT dbo.DanhGiaSinhVien(MaThucTap,Diem,NhanXet)
    SELECT i.MaThucTap,CONVERT(DECIMAL(4,2),7.50+(i.Seq%25)/10.0),N'Đánh giá mẫu: chủ động học hỏi và hoàn thành nhiệm vụ được giao.'
    FROM @DemoInternships i
    WHERE NOT EXISTS (SELECT 1 FROM dbo.DanhGiaSinhVien d WHERE d.MaThucTap=i.MaThucTap);

    INSERT dbo.DanhGiaDoanhNghiep(MaThucTap,Diem,NhanXet)
    SELECT i.MaThucTap,CONVERT(DECIMAL(4,2),8.00+(i.Seq%20)/10.0),N'Đánh giá mẫu: môi trường thực tập thân thiện, công việc phù hợp.'
    FROM @DemoInternships i
    WHERE NOT EXISTS (SELECT 1 FROM dbo.DanhGiaDoanhNghiep d WHERE d.MaThucTap=i.MaThucTap);

    COMMIT TRANSACTION;

    SELECT N'TaiKhoan' AS Bang,COUNT(*) AS SoDong FROM dbo.TaiKhoan
    UNION ALL SELECT N'SinhVien',COUNT(*) FROM dbo.SinhVien
    UNION ALL SELECT N'DoanhNghiep',COUNT(*) FROM dbo.DoanhNghiep
    UNION ALL SELECT N'GiangVien',COUNT(*) FROM dbo.GiangVien
    UNION ALL SELECT N'ViTriThucTap',COUNT(*) FROM dbo.ViTriThucTap
    UNION ALL SELECT N'DonDangKy',COUNT(*) FROM dbo.DonDangKy
    UNION ALL SELECT N'ThucTap',COUNT(*) FROM dbo.ThucTap
    UNION ALL SELECT N'NhatKy',COUNT(*) FROM dbo.NhatKy
    UNION ALL SELECT N'BaoCao',COUNT(*) FROM dbo.BaoCao
    UNION ALL SELECT N'DanhGiaSinhVien',COUNT(*) FROM dbo.DanhGiaSinhVien
    UNION ALL SELECT N'DanhGiaDoanhNghiep',COUNT(*) FROM dbo.DanhGiaDoanhNghiep;
END TRY
BEGIN CATCH
    IF XACT_STATE()<>0 ROLLBACK TRANSACTION;
    THROW;
END CATCH;
GO
