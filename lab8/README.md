phạm long khoa
1250080083
# Lab 8 – Phân tích & Thiết kế Hệ thống Quản lý Cửa hàng Nước Giải Khát

Tài liệu phân tích nghiệp vụ và thiết kế UML cho hệ thống quản lý cửa hàng kinh doanh nước giải khát (Pepsi, Coca Cola, Bia Sài Gòn, ...).

## 1. Giới thiệu

Hệ thống hỗ trợ cửa hàng quản lý toàn bộ quy trình: danh mục hàng hóa, đặt hàng và nhận hàng từ nhà cung ứng, bán hàng, giao hàng nhiều đợt cho khách quen, và quản lý công nợ.

## 2. Phạm vi nghiệp vụ

| Nhóm nghiệp vụ | Mô tả ngắn |
| --- | --- |
| Danh mục & Tồn kho | Quản lý nhãn hiệu, NGK, nhà cung ứng, cập nhật tồn kho |
| Đặt hàng & Nhập hàng | Lập Đơn đặt hàng (ĐĐH), nhận hàng tối đa 3 đợt/ĐĐH kèm Phiếu giao hàng |
| Bán hàng & Hóa đơn | Kiểm tra tồn kho, lập hóa đơn, trừ tồn kho |
| Giao hàng nhiều đợt | Phiếu hẹn giao hàng cho khách quen khi kho thiếu hàng |
| Công nợ | Phiếu ghi nợ, theo dõi và thu hồi nợ khách quen |

## 3. Tác nhân (Actor)

- **Nhân viên Bán hàng**: tiếp nhận mua hàng, lập hóa đơn, phiếu hẹn giao, phiếu ghi nợ.
- **Nhân viên Kho**: lập ĐĐH, tiếp nhận phiếu giao hàng, nhập/xuất kho.
- **Quản lý / Chủ cửa hàng**: quản lý danh mục, xem báo cáo doanh thu, tồn kho, công nợ.
- **Khách hàng** *(external)*: khách quen hoặc khách vãng lai.
- **Nhà cung ứng** *(external)*: nhận ĐĐH và giao hàng tối đa 3 đợt.

## 4. Danh sách Use Case

| Mã | Use Case |
| --- | --- |
| UC01 | Quản lý danh mục Nhãn hiệu, NGK & Nhà cung ứng |
| UC02 | Quản lý đặt hàng Nhà cung ứng (lập ĐĐH) |
| UC03 | Quản lý nhận hàng & Phiếu giao hàng từ NCC (tối đa 3 đợt) |
| UC04 | Bán hàng & lập Hóa đơn thanh toán |
| UC05 | Quản lý Phiếu hẹn giao hàng nhiều đợt (khách quen) |
| UC06 | Quản lý Phiếu ghi nợ & thu nợ (khách quen) |
| UC07 | Tra cứu & báo cáo thống kê (doanh thu, tồn kho, công nợ) |

## 5. Quy tắc nghiệp vụ chính

- **BR01**: Mỗi loại NGK thuộc đúng 1 nhãn hiệu và chỉ do 1 nhà cung ứng cung cấp.
- **BR03**: Một ĐĐH được giao tối đa 3 đợt, mỗi đợt có Phiếu giao hàng riêng.
- **BR04–BR05**: Bán hàng bắt buộc kiểm tra tồn kho; đủ hàng thì lập hóa đơn và giao ngay.
- **BR06**: Kho thiếu + khách vãng lai → chỉ bán số lượng hiện có, thanh toán đủ.
- **BR07**: Kho thiếu + khách quen → lập hóa đơn và tạo các Phiếu hẹn giao nhiều đợt.
- **BR08**: Khách vãng lai thanh toán 100% tại thời điểm mua.
- **BR09**: Chỉ khách quen được ghi nợ qua Phiếu ghi nợ.
- **BR10**: Tồn kho tự động cộng khi xác nhận phiếu giao từ NCC, tự động trừ khi lập hóa đơn / giao đợt.

(Xem đầy đủ BR01–BR10 trong file `lab8.docx`.)

## 6. Yêu cầu chức năng

1. **Kiểm tra dữ liệu**: không để trống trường bắt buộc; số đợt giao ≤ 3; số lượng bán ≤ tồn kho (khách vãng lai); ngày dự kiến trả ≥ ngày lập phiếu.
2. **Transaction**: bán hàng (hóa đơn + trừ kho + phiếu hẹn + phiếu nợ) và nhận hàng (phiếu giao + cộng kho) thực hiện trong cùng một transaction.
3. **Thông báo trạng thái**: mọi thao tác đều báo thành công hoặc lỗi chi tiết.
4. **Tra cứu quan hệ**: ComboBox cho khóa ngoại, DataGridView cho danh sách và chi tiết.
5. **Ngăn thao tác rủi ro**: khóa mã khi sửa, ràng buộc FK, phân quyền không cho sửa/xóa hóa đơn và phiếu nợ đã chốt.

## 7. Công nghệ & kiến trúc

| Hạng mục | Chi tiết |
| --- | --- |
| Ngôn ngữ / UI | C# WinForms (.NET Framework 4.7.2) |
| IDE | Visual Studio 2022 |
| Cơ sở dữ liệu | SQL Server |
| Mô hình hóa | StarUML v5.0+ |
| Kiến trúc | 3 lớp: UI → Service → Data |

## 8. Mô hình lớp (Class)

14 class chính:

`NhanHieu`, `NhaCungUng`, `NuocGiaiKhat`, `NhanVien`, `KhachHang`, `DonDatHang`, `ChiTietDDH`, `PhieuGiaoHang`, `ChiTietPhieuGiao`, `HoaDon`, `ChiTietHoaDon`, `PhieuHenGiao`, `ChiTietPhieuHen`, `PhieuGhiNo`.

Các mối liên kết nổi bật:

- `NhanHieu` 1 – 0..* `NuocGiaiKhat`
- `NhaCungUng` 1 – 1..* `NuocGiaiKhat`
- `DonDatHang` 1 – 1..3 `PhieuGiaoHang` (Aggregation)
- `HoaDon` 1 – 1..* `ChiTietHoaDon` (Composition)
- `HoaDon` 1 – 0..* `PhieuHenGiao` (Composition)
- `HoaDon` 1 – 0..1 `PhieuGhiNo`

## 9. Cấu trúc nội dung tài liệu

| Phần | Nội dung |
| --- | --- |
| 1 | Tổng quan & khảo sát nghiệp vụ (bảng IPO, actor, quy tắc, yêu cầu, tiêu chuẩn) |
| Câu 1 | Xác định Actor và Use Case tổng quát |
| Câu 2 | Xác định Class, thuộc tính, phương thức, ID |
| Câu 3 | Mối liên kết giữa các class và Class Diagram |
| Câu 4 | Use Case Diagram phân rã chi tiết |
| Câu 5 | Activity Diagram |
| Câu 6 | Sequence Diagram |

## 10. Cách sử dụng

1. Mở file `lab8.docx` để xem toàn bộ phân tích và các sơ đồ.
2. Dùng StarUML v5.0+ để mở/chỉnh sửa các sơ đồ UML.
3. Dựa trên Class Diagram để thiết kế CSDL SQL Server và xây dựng ứng dụng WinForms theo kiến trúc 3 lớp.
