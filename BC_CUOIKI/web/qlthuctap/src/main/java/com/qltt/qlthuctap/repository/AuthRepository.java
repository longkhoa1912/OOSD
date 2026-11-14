package com.qltt.qlthuctap.repository;

import com.qltt.qlthuctap.dto.LoginDto;
import com.qltt.qlthuctap.dto.UserSessionDto;
import org.springframework.jdbc.core.CallableStatementCallback;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Repository;

import java.sql.ResultSet;

@Repository
public class AuthRepository {
    private final JdbcTemplate jdbcTemplate;

    public AuthRepository(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
    }

    public UserSessionDto dangNhap(LoginDto dto) {
        return jdbcTemplate.execute(
                "{call dbo.sp_UC01_DangNhap(?, ?)}",
                (CallableStatementCallback<UserSessionDto>) cs -> {
                    cs.setNString(1, dto.tenDangNhap());
                    cs.setNString(2, dto.matKhauHash());

                    try (ResultSet rs = cs.executeQuery()) {
                        if (!rs.next()) {
                            return null;
                        }

                        return new UserSessionDto(
                                rs.getInt("MaTK"),
                                rs.getString("TenDangNhap"),
                                rs.getString("VaiTro"),
                                rs.getString("Email")
                        );
                    }
                }
        );
    }
}