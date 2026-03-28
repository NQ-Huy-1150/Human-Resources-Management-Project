# Tài liệu phân tích hệ thống

## 1. Tổng quan

- **Tên hệ thống**: Human Resources Management (WPF)
- **Nền tảng**: `.NET Framework 4.7.2`, C# `7.3`, desktop `WPF`
- **CSDL**: SQL Server (`HumanResourceManagement`)
- **Mục tiêu**: Quản lý nhân sự, tuyển dụng, chấm công, tính lương theo mô hình phân lớp.

Hệ thống đang tổ chức theo các lớp chính:
- `domain`: model dữ liệu nghiệp vụ
- `repository`: truy cập dữ liệu SQL
- `service`: tầng trung gian xử lý nghiệp vụ
- `view`: giao diện WPF (`xaml`, `xaml.cs`)

---

## 2. Kiến trúc hiện tại

Luồng tổng quát:

`View (UI)` → `Service` → `Repository` → `SQL Server`

Ví dụ:
- `UserView` / `AdminView` (chấm công) gọi `AttendanceService`
- `AttendanceService` gọi `AttendanceRepository`
- `AttendanceRepository` thao tác bảng `attendance`, `users`

Kết nối DB dùng lớp:
- `repository/MssSQLConnection.cs`

---

## 3. Các phân hệ nghiệp vụ chính

## 3.1. Đăng nhập & phân quyền

File chính:
- `view/MainWindow/LoginWindow.xaml.cs`
- `service/Nguoidungservice.cs`
- `service/RoleService.cs`

Luồng:
1. Người dùng nhập email/password.
2. `verifyUser(...)` kiểm tra tài khoản.
3. Lấy `role_id` để phân nhánh:
   - `Quản trị viên` → mở `MainWindow`.
   - `Nhân viên` → cập nhật trạng thái đăng nhập tại `HomePageWindow`.

Ghi nhận:
- Mật khẩu đang kiểm tra trực tiếp theo chuỗi trong DB (chưa hash).

## 3.2. Quản lý người dùng

File chính:
- `repository/NguoidungRepository.cs`

Chức năng:
- Lấy danh sách người dùng
- Tìm kiếm theo họ tên
- Thêm/sửa/xóa người dùng
- Kiểm tra trùng email/số điện thoại

Bảng liên quan:
- `users`, `roles`, `departments`, `positions`

## 3.3. Quản lý phòng ban/chức vụ

File chính:
- `repository/DepartmentRepository.cs`
- `repository/PositionRepository.cs`

Chức năng:
- CRUD phòng ban
- Lấy danh sách chức vụ
- Lấy lương cơ bản theo `pos_id`

## 3.4. Tuyển dụng

File chính:
- `repository/RecruimentRepository.cs`
- `repository/RecruitmentDetailRepository.cs`

Chức năng:
- CRUD bài tuyển dụng (`recruitments`)
- Quản lý hồ sơ ứng viên (`recruitment_details`)
- Cập nhật trạng thái ứng tuyển

## 3.5. Chấm công

File chính:
- `view/chamcong/UserView.xaml.cs`
- `view/chamcong/AdminView.xaml.cs`
- `service/AttendanceService.cs`
- `repository/AttendanceRepository.cs`
- `domain/AttendanceUserRecord.cs`
- `domain/AttendanceAdminRecord.cs`

Chức năng:
- Nhân viên vào ca / ra ca
- Xem lịch sử chấm công trong ngày
- Admin xem lịch sử toàn hệ thống
- Tính:
  - `StandardMinutes`
  - `OvertimeMinutes`
  - `MissingMinutes`
  - `StatusDisplay`

## 3.6. Lương

File chính:
- `view/luong/Luong.xaml.cs`
- `view/luong/NhanvienInfo.xaml.cs`
- `view/luong/SalaryCal.cs`
- `repository/SalaryRepository.cs`

Chức năng:
- Hiển thị bảng lương theo `payroll`
- Cập nhật phụ cấp/thưởng/khấu trừ
- Tính thực lĩnh theo công thức trong `SalaryCal.Luong.Calculate()`

Tương tác mới giữa chấm công và lương:
- Khi load bảng lương, hệ thống lấy thêm chấm công theo `user + month + year`.
- Gán `Attendances` vào model lương.
- Gọi `Calculate()` để tính lại `Deduction` và `NetSalary` từ dữ liệu công.

---

## 4. Mô hình dữ liệu nghiệp vụ (khái quát)

Các bảng chính nhìn thấy từ code:
- `users` (nhân sự)
- `roles` (vai trò)
- `departments` (phòng ban)
- `positions` (chức vụ, lương cơ bản)
- `attendance` (chấm công)
- `payroll` (lương tháng)
- `recruitments` (tin tuyển dụng)
- `recruitment_details` (hồ sơ ứng viên)

Quan hệ nổi bật:
- `users.role_id` → `roles.role_id`
- `users.department_id` → `departments.department_id`
- `users.pos_id` → `positions.pos_id`
- `attendance.user_id` → `users.user_id`
- `payroll.user_id` → `users.user_id`

---

## 5. Luồng nghiệp vụ trọng tâm: Chấm công → Lương

1. Nhân viên tạo bản ghi vào `attendance` qua `checkIn()` / `checkOut()`.
2. Quản trị mở trang lương (`Luong.xaml`).
3. `SalaryRepository.getAllSalary()` lấy danh sách `payroll`.
4. Với từng nhân viên/tháng/năm:
   - Truy vấn `attendance` tương ứng.
   - Tính tổng `MissingMinutes`, `OvertimeMinutes`.
   - Tính `Deduction`, `NetSalary`.
5. Hiển thị kết quả lên `DataGrid`.

---

## 6. Điểm mạnh hiện tại

- Tổ chức lớp tương đối rõ theo `domain/service/repository/view`.
- Nghiệp vụ chấm công và lương đã có đường liên kết dữ liệu.
- SQL phần lớn dùng tham số (`SqlParameter`) giúp giảm lỗi injection.

---

## 7. Rủi ro và tồn tại kỹ thuật

1. **Bảo mật đăng nhập**
   - Mật khẩu đang xử lý dạng plain text.

2. **Trùng model ở nhiều namespace**
   - Có `AttendanceAdminRecord` trong `domain` và trong `view/chamcong`.
   - Dễ gây nhầm kiểu dữ liệu khi mở rộng.

3. **Nghiệp vụ tính lương đặt trong UI folder**
   - `SalaryCal.cs` đang nằm ở `view/luong`, chưa đúng lớp nghiệp vụ thuần.

4. **Tính nhất quán dữ liệu lương**
   - Nếu chỉ tính khi load UI thì DB có thể lệch theo thời điểm.

5. **Độ tin cậy thao tác DB**
   - Một số luồng chưa có transaction khi cần cập nhật nhiều bước.

---

## 8. Đề xuất cải tiến theo thứ tự ưu tiên

### Ưu tiên cao
- Mã hóa mật khẩu (ví dụ BCrypt/PBKDF2 + salt).
- Hợp nhất model `AttendanceAdminRecord` về một nơi (`domain`).
- Tách `SalaryCal` khỏi `view` sang `domain` hoặc `service`.

### Ưu tiên trung bình
- Tạo job/command tính lương theo kỳ (tháng, năm), sau đó lưu chuẩn vào `payroll`.
- Bổ sung transaction cho các tác vụ cập nhật liên quan nhiều bảng.

### Ưu tiên thấp
- Chuẩn hóa naming (`PascalCase`) cho class/property để đồng bộ C# style.
- Bổ sung logging tập trung cho repository/service.

---

## 9. Kết luận

Hệ thống đã có đủ các phân hệ quản trị nhân sự cốt lõi và đang đi đúng hướng phân lớp. Trọng tâm kỹ thuật hiện tại là **chuẩn hóa mô hình nghiệp vụ** và **đảm bảo tính nhất quán dữ liệu lương theo chấm công** ở mức backend, thay vì chỉ phụ thuộc vào luồng hiển thị UI.
