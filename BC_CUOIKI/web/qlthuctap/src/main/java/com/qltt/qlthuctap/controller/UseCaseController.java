package com.qltt.qlthuctap.controller;

import com.qltt.qlthuctap.dto.ApiResponse;
import com.qltt.qlthuctap.repository.UseCaseRepository;
import org.springframework.dao.DataAccessException;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;
import java.util.Map;

@RestController
@RequestMapping("/api")
public class UseCaseController {
    private record Proc(String name, List<String> params) {}
    private final UseCaseRepository repository;

    private static final Map<String, Proc> PROCEDURES = Map.ofEntries(
        Map.entry("uc02", new Proc("sp_UC02_DangKy", List.of("TenDangNhap","MatKhauHash","VaiTro","Email","MaSoSinhVien","MaSoGiangVien","HoTen","Lop","Nganh","TenDoanhNghiep","LinhVuc","DiaChi","SoDienThoai"))),
        Map.entry("uc03-xem", new Proc("sp_UC03_XemHoSoCaNhan", List.of("MaTK"))),
        Map.entry("uc03-capnhat", new Proc("sp_UC03_CapNhatHoSoCaNhan", List.of("MaTK","HoTen","Lop","Nganh","SoDienThoai","DiaChi","Email"))),
        Map.entry("uc04", new Proc("sp_UC04_TimKiemDoanhNghiep", List.of("TuKhoa"))),
        Map.entry("uc05", new Proc("sp_UC05_DanhSachViTri", List.of("TuKhoa","MaDot","ChiDangTuyen"))),
        Map.entry("uc06", new Proc("sp_UC06_DangKyThucTap", List.of("MaTKSinhVien","MaViTri","ThuGioiThieu"))),
        Map.entry("uc07", new Proc("sp_UC07_TheoDoiDon", List.of("MaTKSinhVien"))),
        Map.entry("uc08", new Proc("sp_UC08_QuanLyViTri", List.of("MaTKDoanhNghiep","HanhDong","MaViTri","MaDot","TenViTri","MoTa","YeuCau","SoLuong","DiaDiem"))),
        Map.entry("uc09", new Proc("sp_UC09_XetDuyetDon", List.of("MaTKDoanhNghiep","MaDon","TrangThaiDon","GhiChu"))),
        Map.entry("uc10", new Proc("sp_UC10_TheoDoiSinhVienDoanhNghiep", List.of("MaTKDoanhNghiep"))),
        Map.entry("uc11", new Proc("sp_UC11_DanhGiaSinhVien", List.of("MaTKDoanhNghiep","MaThucTap","Diem","NhanXet"))),
        Map.entry("uc12", new Proc("sp_UC12_SinhVienDuocPhanCong", List.of("MaTKGiangVien"))),
        Map.entry("uc13", new Proc("sp_UC13_TheoDoiTienDo", List.of("MaTKGiangVien","MaThucTap"))),
        Map.entry("uc14", new Proc("sp_UC14_DanhGiaBaoCao", List.of("MaTKGiangVien","MaBaoCao","Diem","NhanXet"))),
        Map.entry("uc15", new Proc("sp_UC15_NopNhatKy", List.of("MaTKSinhVien","MaThucTap","NgayGhi","NoiDung"))),
        Map.entry("uc16", new Proc("sp_UC16_NopBaoCao", List.of("MaTKSinhVien","MaThucTap","TieuDe","NoiDung","DuongDanTep"))),
        Map.entry("uc17", new Proc("sp_UC17_QuanLyTaiKhoan", List.of("MaTKAdmin","HanhDong","MaTK","TenDangNhap","MatKhauHash","VaiTro","Email"))),
        Map.entry("uc18", new Proc("sp_UC18_QuanLyDoanhNghiep", List.of("MaTKAdmin","HanhDong","MaDN","MaTK","MaSoThue","TenDoanhNghiep","LinhVuc","DiaChi","SoDienThoai","Website","MoTa"))),
        Map.entry("uc19", new Proc("sp_UC19_QuanLyGiangVien", List.of("MaTKAdmin","HanhDong","MaGV","MaTK","MaSoGiangVien","HoTen","Khoa","SoDienThoai"))),
        Map.entry("uc20", new Proc("sp_UC20_QuanLyDotThucTap", List.of("MaTKAdmin","HanhDong","MaDot","TenDot","NgayBatDau","NgayKetThuc","HanDangKy","TrangThai","MoTa"))),
        Map.entry("uc21", new Proc("sp_UC21_ThongKe", List.of("MaTKAdmin","MaDot"))),
        Map.entry("uc22", new Proc("sp_UC22_DanhGiaDoanhNghiep", List.of("MaTKSinhVien","MaThucTap","Diem","NhanXet"))),
        Map.entry("uc23", new Proc("sp_UC23_QuanLyDanhGiaDoanhNghiep", List.of("MaTKAdmin","HanhDong","MaDGDN","Diem","NhanXet","TuKhoa")))
    );

    public UseCaseController(UseCaseRepository repository) { this.repository = repository; }

    @PostMapping("/uc/{useCase}")
    public ResponseEntity<ApiResponse<?>> execute(@PathVariable String useCase, @RequestBody Map<String, Object> body) {
        Proc proc = PROCEDURES.get(useCase.toLowerCase());
        if (proc == null) return ResponseEntity.status(HttpStatus.NOT_FOUND).body(ApiResponse.fail("Use Case không được hỗ trợ."));
        try {
            return ResponseEntity.ok(ApiResponse.ok("Đã xử lý " + useCase + ".", repository.call(proc.name(), proc.params(), body)));
        } catch (DataAccessException ex) {
            String message = ex.getMostSpecificCause() == null ? "Lỗi truy cập dữ liệu." : ex.getMostSpecificCause().getMessage();
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ApiResponse.fail(message));
        }
    }

    @GetMapping("/dondangky/danh-sach")
    public ResponseEntity<ApiResponse<?>> pendingApplications(@RequestParam int maTKDoanhNghiep) {
        try {
            return ResponseEntity.ok(ApiResponse.ok("Danh sách đơn chờ duyệt.", repository.call("sp_UC09_DanhSachDonChoDuyet", List.of("MaTKDoanhNghiep"), Map.of("MaTKDoanhNghiep", maTKDoanhNghiep))));
        } catch (DataAccessException ex) {
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ApiResponse.fail(ex.getMostSpecificCause().getMessage()));
        }
    }

    @GetMapping("/baocao/danh-sach")
    public ResponseEntity<ApiResponse<?>> assignedReports(@RequestParam int maTKGiangVien) {
        try {
            return ResponseEntity.ok(ApiResponse.ok("Danh sách báo cáo chưa chấm.", repository.call("sp_UC14_DanhSachBaoCao", List.of("MaTKGiangVien"), Map.of("MaTKGiangVien", maTKGiangVien))));
        } catch (DataAccessException ex) {
            String message = ex.getMostSpecificCause() == null ? "Lỗi truy cập dữ liệu." : ex.getMostSpecificCause().getMessage();
            return ResponseEntity.status(HttpStatus.BAD_REQUEST).body(ApiResponse.fail(message));
        }
    }

    @PostMapping("/dondangky/tao")
    public ResponseEntity<ApiResponse<?>> createApplication(@RequestBody Map<String, Object> body) { return execute("uc06", body); }

    @PostMapping("/dondangky/duyet")
    public ResponseEntity<ApiResponse<?>> reviewApplication(@RequestBody Map<String, Object> body) { return execute("uc09", body); }
}
