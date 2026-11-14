package com.qltt.qlthuctap.dto;

import com.fasterxml.jackson.annotation.JsonProperty;

public record LoginDto(
        @JsonProperty("TenDangNhap") String tenDangNhap,
        @JsonProperty("MatKhauHash") String matKhauHash
) {}