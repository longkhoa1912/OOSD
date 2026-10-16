Phạm Long Khoa
1250080083
# Lab 6 – Phân tích & thiết kế hệ thống Quản lý Hồ sơ Bệnh án

Tài liệu `lab6.docx` là bài thực hành phân tích nghiệp vụ và thiết kế hướng đối tượng cho phần mềm **quản lý bệnh án của bệnh viện đa khoa quốc tế**.

## 1. Mục tiêu

- Khảo sát và phân loại nghiệp vụ từ đề bài (đăng ký, khám bệnh, kê toa, xét nghiệm, khám định kỳ, thanh toán).
- Xác định actor, quy tắc nghiệp vụ, yêu cầu chức năng và phi chức năng.
- Xác định các Class, thuộc tính, phương thức và ID (khóa chính/khóa ngoại) làm nền cho Class Diagram.

## 2. Nội dung tài liệu

| Phần | Nội dung |
|---|---|
| **1.1** | Khảo sát nghiệp vụ: đăng ký & cấp hồ sơ, khám bệnh & điều trị, khám định kỳ theo quý, xuất viện & hậu xuất viện |
| **1.2.1** | Bảng Đầu vào – Xử lý – Xuất kết quả (8 nhóm nghiệp vụ) |
| **1.2.2** | Bảng Bộ phận/Actor – Công việc – Loại – Quy định liên quan (6 actor) |
| **1.2.3** | Quy tắc nghiệp vụ BR01 – BR12 |
| **1.2.5** | Yêu cầu chức năng (5 mục) và phi chức năng (tiện dụng, hiệu quả, tiến hóa, tương thích) |
| **Câu 1** | Danh sách Class, thuộc tính, phương thức, ID |

## 3. Các Actor

Phòng chăm sóc khách hàng (CSKH), Thu ngân, Bác sĩ, Phòng xét nghiệm, Bệnh nhân, Quản lý.

## 4. Các Class đã xác định (Câu 1)

| # | Class | ID (PK) | Khóa ngoại |
|---|---|---|---|
| 1 | `BenhNhan` | maBenhNhan | – |
| 2 | `PhieuDangKy` | maPhieuDK | – |
| 3 | `HoSoBenhAn` | maHoSo | maBenhNhan |
| 4 | `PhieuKhamBenh` | maPhieuKham | maHoSo, maBacSi, maKhoa |
| 5 | `BacSi` | maBacSi | maKhoa |
| 6 | `KhoaDieuTri` | maKhoa | – |
| 7 | `ToaThuoc` | maToaThuoc | maPhieuKham |
| 8 | `ChiTietToaThuoc` | maChiTiet | maToaThuoc, maThuoc |
| 9 | `Thuoc` | maThuoc | – |
| 10 | `PhieuXetNghiem` | *(chưa mô tả)* | *(chưa mô tả)* |
| 12 | `NhanVienCSKH` | maNhanVien | – |

## 5. Quy tắc nghiệp vụ chính

- Mỗi bệnh nhân có **một** hồ sơ bệnh án với mã hồ sơ duy nhất; phải đóng lệ phí hằng năm để cấp và duy trì hồ sơ.
- Mỗi lần khám tạo một phiếu khám; phiếu khám có thể kèm toa thuốc và xét nghiệm.
- Hồ sơ còn hạn mới được **khám định kỳ theo quý miễn phí**; đợt khám định kỳ vẫn được ghi nhận như một lần điều trị.
- Bác sĩ xem được toàn bộ lịch sử bệnh án trước khi điều trị.
- Hồ sơ tra cứu được theo mã hồ sơ, họ tên, điện thoại, ngày sinh.

## 6. Công nghệ dự kiến (theo yêu cầu phi chức năng)

- Visual Studio 2022, .NET Framework 4.7.2 (WinForms)
- SQL Server LocalDB/Express, ADO.NET
- Kiến trúc tách lớp **UI – Service – Data**

## 7. Điểm cần hoàn thiện trong tài liệu

- **Class 10 `PhieuXetNghiem`** chỉ có phần mô tả, thiếu thuộc tính và phương thức.
- **Không có mục 11**; nhảy từ 10 sang 12. Phần phương thức của Class 12 có nhắc `CuocHen` (lịch hẹn) nhưng chưa có Class `CuocHen` (cùng các Class cho xét nghiệm, hóa đơn viện phí).
- Class `NhanVienCSKH` mô tả cả thanh toán viện phí nhưng chưa có phương thức tương ứng.
- Một số lỗi chính tả/đặt tên: `anhCha` (nên là `anh3x4`), `loiDangDung` (nên là `loiDan`), "Kham bệnh" (thiếu dấu), tiêu đề **1.2.4** bị thiếu trong đánh số.
- Chưa có **Câu 2–6**: Class Diagram, Use Case tổng quát, Use Case phân rã, Activity Diagram, Sequence Diagram "Lập toa thuốc".

## 8. Gợi ý các bước tiếp theo

1. Bổ sung Class `PhieuXetNghiem`, `LoaiXetNghiem`, `CuocHen`, `HoaDonVienPhi`, `LePhi`.
2. Vẽ Class Diagram trong StarUML với bản số (BenhNhan 1–1 HoSoBenhAn, HoSoBenhAn 1–0..* PhieuKhamBenh, ToaThuoc 1–1..* ChiTietToaThuoc…).
3. Vẽ các sơ đồ Use Case, Activity, Sequence; lưu file với tên rõ ràng (ví dụ `QLBenhAn_ClassDiagram.mdj`).
4. Xây dựng ứng dụng WinForms theo cấu trúc thư mục `Forms/`, `Services/`, `Db.cs`.
