package com.qltt.qlthuctap.dto;

import com.fasterxml.jackson.annotation.JsonProperty;

public record ViTriDto(
        @JsonProperty("MaVT") int maVT,
        @JsonProperty("TenViTri") String tenViTri,
        @JsonProperty("TenDoanhNghiep") String tenDoanhNghiep,
        @JsonProperty("LinhVuc") String linhVuc,
        @JsonProperty("TenDot") String tenDot,
        @JsonProperty("DiaDiem") String diaDiem,
        @JsonProperty("SoLuong") int soLuong,
        @JsonProperty("TrangThai") String trangThai
) {}