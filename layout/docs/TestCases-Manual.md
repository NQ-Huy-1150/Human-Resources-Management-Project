# Test Cases (Manual) - HRM Demo

## 1) Chấm công & tính lương

### TC-ATT-01: Vào ca thành công
- **Tiền điều kiện**: User chưa vào ca hôm nay.
- **Bước test**:
  1. Mở `Chấm công` (User).
  2. Bấm `VÀO CA`.
- **Kỳ vọng**:
  - Hiện thông báo vào ca thành công.
  - Nút `VÀO CA` bị disable, `RA CA` enable.

### TC-ATT-02: Ra ca thành công (đủ giờ)
- **Tiền điều kiện**: Có ca mở.
- **Bước test**:
  1. Bấm `RA CA`.
- **Kỳ vọng**:
  - Cập nhật `check_out`.
  - Trạng thái thành `Đã xong ca`.
  - Payroll tháng hiện tại được cập nhật.

### TC-ATT-03: Phạt khi thiếu giờ, netSalary không âm
- **Tiền điều kiện**: Payroll tháng hiện tại có `net_salary = 0`.
- **Bước test**:
  1. Tạo ca làm rất ngắn để bị phạt lớn.
  2. Bấm `RA CA`.
- **Kỳ vọng**:
  - `deduction` tăng.
  - `net_salary` vẫn **= 0** (không âm, không nhảy sai lên lương cơ bản).

### TC-ATT-04: Tăng ca
- **Tiền điều kiện**: Có ca mở, thời gian làm > 480 phút.
- **Bước test**:
  1. Bấm `RA CA` sau thời lượng > 8h.
- **Kỳ vọng**:
  - `shift_type` = `Tăng ca`.
  - `bonus` tăng.

## 2) Xóa cascade User

### TC-USER-DEL-01: Xóa user có dữ liệu liên quan
- **Tiền điều kiện**: User có dữ liệu trong `attendance` và `payroll`.
- **Bước test**:
  1. Vào quản lý người dùng.
  2. Xóa user.
- **Kỳ vọng**:
  - Xóa thành công.
  - Bản ghi liên quan trong `attendance` và `payroll` của user bị xóa.
  - Không lỗi khóa ngoại.

## 3) Xóa cascade Tuyển dụng

### TC-REC-DEL-01: Xóa đơn tuyển dụng có hồ sơ ứng viên
- **Tiền điều kiện**: Recruitment có dữ liệu trong `recruitment_details`.
- **Bước test**:
  1. Vào `Tuyển dụng`.
  2. Xóa đơn.
- **Kỳ vọng**:
  - Xóa thành công.
  - Dữ liệu `recruitment_details` thuộc đơn đó bị xóa trước.
  - Không lỗi khóa ngoại.

## 4) Create Candidate validation

### TC-CAN-01: Thiếu trường bắt buộc
- **Bước test**: Để trống 1 trường (vd: Email), bấm tạo.
- **Kỳ vọng**: Hiện cảnh báo, không tạo hồ sơ.

### TC-CAN-02: Email sai định dạng
- **Bước test**: Nhập email sai (`abc@`), bấm tạo.
- **Kỳ vọng**: Hiện cảnh báo email không hợp lệ.

### TC-CAN-03: SĐT sai định dạng
- **Bước test**: Nhập ký tự chữ hoặc ít hơn 8 số.
- **Kỳ vọng**: Hiện cảnh báo SĐT không hợp lệ.

### TC-CAN-04: Kinh nghiệm âm/không phải số
- **Bước test**: Nhập `-1` hoặc `abc` ở năm kinh nghiệm.
- **Kỳ vọng**: Hiện cảnh báo, không tạo hồ sơ.

## 5) Smoke test nhanh sau mỗi lần sửa
- Build solution: phải thành công.
- Mở 3 màn hình chính: `Người dùng`, `Tuyển dụng`, `Chấm công`.
- Thử 1 ca `VÀO CA -> RA CA`.
- Thử 1 lần xóa user có dữ liệu liên quan.
- Thử 1 lần xóa tuyển dụng có ứng viên.
