# LAB 5 – Hệ thống Quản lý Công ty Du lịch

## 1. Thông tin sinh viên

- **Họ và tên:** Phạm Long Khoa
- **Mã số sinh viên (MSSV):** 1250080083
- **Lớp:** 12_ĐH_CNPM2

## 2. Thông tin bài Lab

- **Tên bài Lab:** LAB 5 – Hệ thống Quản lý Công ty Du lịch
- **Ngày hoàn thành:** 09/10/2026

## 3. Môi trường phát triển

- **Hệ điều hành:** Windows 11
- **Ngôn ngữ lập trình:** C#
- **Framework:** .NET Framework 4.7.2
- **IDE:** Visual Studio 2022
- **Cơ sở dữ liệu:** SQL Server 


## 4. Giới thiệu hệ thống

Hệ thống Quản lý Công ty Du lịch hỗ trợ quản lý thông tin tour, lịch trình, đăng ký tour của khách đoàn và khách lẻ, phân công hướng dẫn viên (HDV), tính lương HDV, quyết toán kinh phí tour đoàn và thu thập ý kiến khách hàng sau chuyến đi.

Theo quy định nghiệp vụ, các tour đều mặc định xuất phát và kết thúc tại **TP. Hồ Chí Minh**. Việc đăng ký được chia thành hai loại: khách đoàn và khách lẻ; mỗi loại có quy trình đăng ký, thanh toán và tổ chức chuyến đi riêng.

## 5. Chức năng nghiệp vụ chính

### 5.1. Quản lý tour và lịch trình

- Quản lý mã tour, tên tour và số ngày.
- Lưu thông tin các điểm dừng chân, điểm tham quan và nội dung lịch trình.
- Ghi nhận thông tin điểm tham quan gồm mã số, tên địa điểm, địa chỉ, nội dung và ý nghĩa.
- Quản lý thông tin phương tiện, nơi ăn uống, khách sạn và cấp sao khách sạn tại các điểm dừng.
- Tra cứu danh sách tour và các điểm dừng thuộc tour.

### 5.2. Đăng ký tour theo đoàn

- Áp dụng cho nhóm **trên 12 người**.
- Đoàn được chọn ngày đi và địa điểm đón theo yêu cầu.
- Ghi nhận tên cơ quan hoặc đại diện gia đình, địa chỉ, số điện thoại, người đại diện và số người tham gia.
- Ghi nhận tiền đặt cọc; phần kinh phí tham quan còn lại được quyết toán sau khi chuyến đi kết thúc.
- Nếu đoàn mua bảo hiểm du lịch, phải cung cấp danh sách thành viên cùng đi.
- Lưu thông tin đăng ký để thuận tiện cho các lần tổ chức sau.

### 5.3. Đăng ký tour khách lẻ

- Áp dụng cho nhóm **dưới 12 người**.
- Khách đăng ký theo chuyến có ngày đi và ngày về theo lịch cố định của công ty.
- Có thể đăng ký tại các điểm bán vé khác nhau.
- Khách đón xe tại các điểm quy định của công ty.
- Khách phải mua vé và thanh toán tiền vé khi đăng ký.
- Hệ thống kiểm tra chỗ còn trống trước khi xác nhận đăng ký.

### 5.4. Phân công hướng dẫn viên và tính lương

- Phân công HDV cho tour đoàn hoặc chuyến khách lẻ.
- Kiểm tra lịch làm việc để tránh trùng hoặc chồng chéo lịch.
- Mỗi chuyến khách lẻ có **một HDV**; tour đoàn đông người có thể được phân công nhiều HDV.
- Lương tháng của HDV được tính bằng lương căn bản cộng tổng lương theo các tour đã thực hiện trong tháng.

### 5.5. Thanh toán và khảo sát

- Quyết toán và thu phần kinh phí tour đoàn còn lại sau khi kết thúc chuyến đi.
- Ghi nhận phiếu khảo sát và góp ý của khách hàng.
- Tổng hợp ý kiến khảo sát để lập báo cáo.

## 6. Quy tắc nghiệp vụ (Business Rules)

| ID | Quy tắc |
|---|---|
| BR01 | Tất cả tour mặc định xuất phát và kết thúc tại TP. Hồ Chí Minh. |
| BR02 | Khách đăng ký trên 12 người là khách đoàn, được chọn ngày đi và địa điểm đón theo yêu cầu. |
| BR03 | Khách đoàn phải đặt cọc trước; nếu không thực hiện chuyến đi sẽ mất khoản tiền cọc. |
| BR04 | Kinh phí tham quan của khách đoàn được thanh toán sau khi chuyến đi kết thúc. |
| BR05 | Nếu đoàn mua bảo hiểm du lịch, phải nộp danh sách chi tiết các thành viên cùng đi. |
| BR06 | Khách đăng ký dưới 12 người là khách lẻ, tham gia chuyến có ngày đi và ngày về theo lịch công ty. |
| BR07 | Khách lẻ phải thanh toán toàn bộ tiền vé khi đăng ký và đón tour tại điểm quy định. |
| BR08 | Lịch làm việc của HDV không được trùng hoặc chồng chéo giữa các tour/chuyến. |
| BR09 | Mỗi chuyến khách lẻ có một HDV; tour đoàn đông người có thể có nhiều HDV. |
| BR10 | Lương tháng của HDV bằng lương căn bản cộng tổng lương theo từng tour đã thực hiện trong tháng. |

> **Lưu ý nghiệp vụ:** Tài liệu quy định khách đoàn là trên 12 người và khách lẻ là dưới 12 người, nên trường hợp đúng 12 người chưa được xác định rõ. Cần thống nhất quy tắc này trước khi triển khai kiểm tra dữ liệu.

## 7. Yêu cầu chức năng

1. **Kiểm tra dữ liệu:** Không để trống khóa chính hoặc trường bắt buộc; kiểm tra số người, số ngày/đêm và ngày đi/ngày về hợp lệ; phân loại khách đoàn/khách lẻ theo quy định.
2. **Giao dịch dữ liệu:** Lập phiếu đăng ký, ghi nhận đặt cọc, thanh toán vé khách lẻ, quyết toán tour đoàn và phân công HDV trong giao dịch phù hợp để tránh cập nhật dở dang.
3. **Thông báo trạng thái:** Thông báo thành công hoặc nêu rõ nguyên nhân từ chối, chẳng hạn trùng lịch HDV hoặc chuyến đã hết chỗ.
4. **Tra cứu:** Hỗ trợ chọn các khóa ngoại như mã tour, mã chuyến và mã HDV; hiển thị danh sách tour, lịch trình, điểm tham quan và danh sách người mua bảo hiểm.
5. **Ngăn thao tác rủi ro:** Khóa mã khi sửa thông tin; sử dụng ràng buộc khóa ngoại để ngăn xóa dữ liệu đã phát sinh lịch trình, đăng ký hoặc thanh toán.

## 8. Yêu cầu phi chức năng

- **Tính tiện dụng:** Giao diện phù hợp quy trình đăng ký tour đoàn/khách lẻ và phân công HDV; tên điều khiển nhất quán; thông tin tour, lịch trình và giá vé liên quan cần được hiển thị thuận tiện.
- **Hiệu quả:** Truy vấn có điều kiện theo các thông tin như điểm đến, ngày đi và giá; chỉ tải dữ liệu cần thiết; dùng transaction khi cập nhật nhiều bảng liên quan.
- **Khả năng bảo trì:** Tách UI, Service và Data để thay đổi chính sách đặt cọc, khuyến mãi hoặc tính lương ít ảnh hưởng đến giao diện.
- **Tương thích:** Visual Studio 2022, .NET Framework 4.7.2, SQL Server LocalDB/Express và ADO.NET.

## 9. Các mối quan hệ lớp chính

| STT | Quan hệ | Bội số | Ý nghĩa |
|---:|---|---|---|
| 1 | `Tour` – `NoiDungChan` | 1–N | Một tour có từ một đến nhiều nơi dừng chân; quan hệ Composition. |
| 2 | `NoiDungChan` – `DiemThamQuan` | N–N | Một nơi dừng có thể có nhiều điểm tham quan và một điểm tham quan có thể xuất hiện tại nhiều nơi dừng. |
| 3 | `NoiDungChan` – `PhuongTien` | N–N | Một nơi dừng có thể liên quan nhiều phương tiện và một phương tiện có thể được sử dụng tại nhiều nơi dừng. |
| 4 | `Tour` – `ChuyenDi` | 1–N | Một tour có thể có nhiều chuyến đi; mỗi chuyến thuộc về một tour. |
| 5 | `KhachDoan` – `PhieuDangKyDoan` | 1–N | Một khách đoàn có thể có nhiều phiếu đăng ký; mỗi phiếu thuộc một khách đoàn. |
| 6 | `PhieuDangKyDoan` – `Tour` | N–1 | Mỗi phiếu đăng ký ứng với một tour; một tour có thể xuất hiện trong nhiều phiếu. |
| 7 | `KhachDoan` – `DanhSachBaoHiem` | 1–0..1 | Một khách đoàn có thể không có hoặc có một danh sách bảo hiểm. |
| 8 | `DanhSachBaoHiem` – `NguoiDiCung` | 1–N | Một danh sách bảo hiểm có nhiều người đi cùng; quan hệ Composition. |
| 9 | `KhachLe` – `VeTour` | 1–N | Một khách lẻ có thể mua nhiều vé; mỗi vé thuộc một khách lẻ. |
| 10 | `VeTour` – `ChuyenDi` | N–1 | Mỗi vé thuộc một chuyến đi; một chuyến có thể có nhiều vé. |
| 11 | `NhanVienHDV` – `PhanCongHDV` | 1–N | Một HDV có thể nhận nhiều phân công; mỗi phân công gắn với một HDV. |
| 12 | `PhanCongHDV` – `ChuyenDi` | N–0..1 | Mỗi phân công có thể gắn với tối đa một chuyến đi; một chuyến có thể có nhiều phân công. |
| 13 | `PhanCongHDV` – `PhieuDangKyDoan` | N–0..1 | Mỗi phân công có thể gắn với tối đa một phiếu đăng ký đoàn; một phiếu có thể có nhiều phân công HDV. |
| 14 | `KhachHang` – `PhieuKhaoSat` | 1–N | Một khách hàng có thể gửi nhiều phiếu khảo sát; mỗi phiếu thuộc một khách hàng. |

## 10. Hướng dẫn thiết lập và chạy chương trình

> Tài liệu được cung cấp chưa nêu tên file solution, cấu trúc thư mục, tên database hoặc chuỗi kết nối cụ thể. Hãy cập nhật các thông tin này theo mã nguồn thực tế của dự án.

1. Cài đặt Visual Studio 2022 và workload phát triển ứng dụng .NET desktop.
2. Cài đặt hoặc bật SQL Server LocalDB/SQL Server Express.
3. Mở file solution (`.sln`) của dự án trong Visual Studio.
4. Cấu hình chuỗi kết nối SQL Server trong phần cấu hình hoặc lớp truy cập dữ liệu của dự án.
5. Tạo hoặc khôi phục cơ sở dữ liệu theo script/schema của dự án.
6. Kiểm tra các khóa chính, khóa ngoại và dữ liệu tham chiếu cần thiết.
7. Build solution, xử lý lỗi nếu có, sau đó chạy ứng dụng bằng **Start** trong Visual Studio.

## 11. Kết quả đạt được

Theo nội dung báo cáo LAB 5, các nội dung phân tích và thiết kế bao gồm:

- Mô tả hoạt động nghiệp vụ và yêu cầu hệ thống.
- Xác định các quy tắc nghiệp vụ BR01–BR10.
- Tổng hợp yêu cầu chức năng và phi chức năng.
- Mô tả các mối quan hệ và bội số giữa các lớp trong mô hình lớp.


```markdown
![Giao diện quản lý tour](docs/images/quan-ly-tour.png)
```

## 12. Lỗi gặp phải và cách khắc phục

Báo cáo nguồn chưa cung cấp danh sách lỗi phát sinh thực tế. Có thể ghi bổ sung theo mẫu sau khi chạy chương trình:

| STT | Lỗi gặp phải | Nguyên nhân | Cách khắc phục |
|---:|---|---|---|
| 1 |
---
