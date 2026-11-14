package com.qltt.qlthuctap.controller;

import com.qltt.qlthuctap.dto.ApiResponse;
import com.qltt.qlthuctap.dto.ViTriDto;
import com.qltt.qlthuctap.repository.ViTriRepository;
import org.springframework.dao.DataAccessException;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

import java.util.List;

@RestController
@RequestMapping("/api/vitri")
public class ViTriController {
    private final ViTriRepository viTriRepository;

    public ViTriController(ViTriRepository viTriRepository) {
        this.viTriRepository = viTriRepository;
    }

    @GetMapping("/danh-sach")
    public ResponseEntity<ApiResponse<List<ViTriDto>>> danhSach() {
        try {
            return ResponseEntity.ok(
                    ApiResponse.ok("Lấy danh sách vị trí thành công.", viTriRepository.danhSach())
            );
        } catch (DataAccessException ex) {
            return ResponseEntity.status(HttpStatus.INTERNAL_SERVER_ERROR)
                    .body(ApiResponse.fail("Không thể tải danh sách vị trí."));
        }
    }
}