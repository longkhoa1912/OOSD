package com.qltt.qlthuctap.dto;

import com.fasterxml.jackson.annotation.JsonProperty;

public record UserSessionDto(
        @JsonProperty("MaTK") int maTK,
        @JsonProperty("TenDangNhap") String tenDangNhap,
        @JsonProperty("VaiTro") String vaiTro,
        @JsonProperty("Email") String email
) {}