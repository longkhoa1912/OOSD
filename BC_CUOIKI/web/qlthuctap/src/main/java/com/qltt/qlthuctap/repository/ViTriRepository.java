package com.qltt.qlthuctap.repository;

import com.qltt.qlthuctap.dto.ViTriDto;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Repository;

import java.util.List;

@Repository
public class ViTriRepository {
    private final JdbcTemplate jdbcTemplate;

    public ViTriRepository(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }

    public List<ViTriDto> danhSach() {
        String sql = """
                SELECT MaVT, TenViTri, TenDoanhNghiep, LinhVuc,
                       TenDot, DiaDiem, SoLuong, TrangThai
                FROM dbo.vw_DanhSachViTriThucTap
                ORDER BY MaVT
                """;

        return jdbcTemplate.query(sql, (rs, rowNum) -> new ViTriDto(
                rs.getInt("MaVT"),
                rs.getString("TenViTri"),
                rs.getString("TenDoanhNghiep"),
                rs.getString("LinhVuc"),
                rs.getString("TenDot"),
                rs.getString("DiaDiem"),
                rs.getInt("SoLuong"),
                rs.getString("TrangThai")
        ));
    }
}