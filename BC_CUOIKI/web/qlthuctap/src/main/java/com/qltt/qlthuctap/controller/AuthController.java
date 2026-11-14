package com.qltt.qlthuctap.controller;

import com.qltt.qlthuctap.dto.ApiResponse;
import com.qltt.qlthuctap.dto.LoginDto;
import com.qltt.qlthuctap.dto.UserSessionDto;
import com.qltt.qlthuctap.repository.AuthRepository;
import org.springframework.dao.DataAccessException;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.sql.SQLException;

@RestController
@RequestMapping("/api/auth")
public class AuthController {
    private final AuthRepository authRepository;

    public AuthController(AuthRepository authRepository) {
        this.authRepository = authRepository;
    }

    @PostMapping("/login")
    public ResponseEntity<ApiResponse<UserSessionDto>> login(
            @RequestBody LoginDto dto
    ) {
        try {
            UserSessionDto session = authRepository.dangNhap(dto);
            return ResponseEntity.ok(ApiResponse.ok("Đăng nhập thành công.", session));
        } catch (DataAccessException ex) {
            SQLException sqlException = findSqlException(ex);

            if (sqlException != null && sqlException.getErrorCode() == 51001) {
                return ResponseEntity.status(HttpStatus.UNAUTHORIZED)
                        .body(ApiResponse.fail(
                                "Tên đăng nhập hoặc mật khẩu không đúng, hoặc tài khoản đã bị khóa."
                        ));
            }

            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                    .body(ApiResponse.fail("Không thể xử lý yêu cầu đăng nhập."));
        }
    }

    private SQLException findSqlException(Throwable error) {
        Throwable current = error;

        while (current != null) {
            if (current instanceof SQLException sqlException) {
                return sqlException;
            }
            current = current.getCause();
        }

        return null;
    }
}