using Newtonsoft.Json;
using System;

namespace QltThucTap.WinForms.Models
{
public sealed class ApiResponse<T>
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("data")]
    public T Data { get; set; }
}

public sealed class UserSessionDto
{
    [JsonProperty("MaTK")]
    public int MaTK { get; set; }

    [JsonProperty("TenDangNhap")]
    public string TenDangNhap { get; set; } = "";

    [JsonProperty("VaiTro")]
    public string VaiTro { get; set; } = "";

    [JsonProperty("Email")]
    public string Email { get; set; }
}

public sealed class ViTriDto
{
    [JsonProperty("MaVT")]
    public int MaVT { get; set; }

    [JsonProperty("TenViTri")]
    public string TenViTri { get; set; } = "";

    [JsonProperty("TenDoanhNghiep")]
    public string TenDoanhNghiep { get; set; } = "";

    [JsonProperty("LinhVuc")]
    public string LinhVuc { get; set; }

    [JsonProperty("TenDot")]
    public string TenDot { get; set; } = "";

    [JsonProperty("DiaDiem")]
    public string DiaDiem { get; set; }

    [JsonProperty("SoLuong")]
    public int SoLuong { get; set; }

    [JsonProperty("TrangThai")]
    public string TrangThai { get; set; } = "";
}

public sealed class DonDangKyDto
{
    [JsonProperty("MaDon")]
    public int MaDon { get; set; }

    [JsonProperty("MaSV")]
    public int MaSV { get; set; }

    [JsonProperty("MaSoSinhVien")]
    public string MaSoSinhVien { get; set; } = "";

    [JsonProperty("HoTenSinhVien")]
    public string HoTenSinhVien { get; set; } = "";

    [JsonProperty("TenViTri")]
    public string TenViTri { get; set; } = "";

    [JsonProperty("TenDoanhNghiep")]
    public string TenDoanhNghiep { get; set; } = "";

    [JsonProperty("NgayNop")]
    public DateTime NgayNop { get; set; }

    [JsonProperty("TrangThaiDon")]
    public string TrangThaiDon { get; set; } = "";

    [JsonProperty("ThuGioiThieu")]
    public string ThuGioiThieu { get; set; }

    [JsonProperty("GhiChu")]
    public string GhiChu { get; set; }

    public override string ToString() => $"{MaDon} - {HoTenSinhVien} - {TenViTri}";
}

public static class AppSession
{
    public static UserSessionDto CurrentUser { get; set; }
}
}
