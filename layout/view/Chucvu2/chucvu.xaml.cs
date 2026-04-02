using layout.service;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace layout.view.Chucvu2
{
    public partial class chucvu : Page
    {
        private readonly PositionService positionService = new PositionService();

        public chucvu()
        {
            InitializeComponent();
            LoadData();
        }

        // 1. Tải toàn bộ danh sách chức vụ lên DataGrid
        private void LoadData()
        {
            try
            {
                // Gọi hàm lấy toàn bộ dữ liệu (bao gồm id, name, salary)
                DataTable dt = positionService.getAllPosition();
                dgDepartments.ItemsSource = dt.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi hệ thống khi tải dữ liệu: " + ex.Message, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void dgDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDepartments.SelectedItem is DataRowView row)
            {
                txtPos.Text = row["pos_name"].ToString();
                txtBaseSalary.Text = row["base_salary"].ToString();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                string posName = txtPos.Text.Trim();
                float salary = float.Parse(txtBaseSalary.Text.Trim());

                if (positionService.insertPosition(posName, salary))
                {
                    MessageBox.Show("Thêm chức vụ mới thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadData();
                    ClearFields();
                }
                else
                {
                    MessageBox.Show("Không thể thêm chức vụ. Vui lòng kiểm tra lại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        // 4. Xử lý Cập nhật
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (dgDepartments.SelectedItem is DataRowView row)
            {
                if (ValidateInput())
                {
                    int id = Convert.ToInt32(row["pos_id"]);
                    string posName = txtPos.Text.Trim();
                    float salary = float.Parse(txtBaseSalary.Text.Trim());

                    if (positionService.updatePosition(id, posName, salary))
                    {
                        MessageBox.Show("Cập nhật thông tin thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadData();
                        ClearFields();
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một chức vụ từ danh sách bên dưới trước khi cập nhật.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // 5. Xử lý Xóa
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgDepartments.SelectedItem is DataRowView row)
            {
                string posName = row["pos_name"].ToString();
                if (MessageBox.Show($"Bạn có chắc chắn muốn xóa chức vụ '{posName}' không?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    int id = Convert.ToInt32(row["pos_id"]);
                    try
                    {
                        if (positionService.deletePosition(id))
                        {
                            LoadData();
                            ClearFields();
                        }
                    }
                    catch (Exception)
                    {
                        MessageBox.Show("Không thể xóa chức vụ này vì có thể đang có nhân viên thuộc chức vụ này.", "Lỗi ràng buộc", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn chức vụ cần xóa.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // 6. Làm mới ô nhập liệu
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearFields();
        }

        private void ClearFields()
        {
            txtPos.Clear();
            txtBaseSalary.Clear();
            dgDepartments.SelectedItem = null;
        }

        // 7. Kiểm tra dữ liệu hợp lệ
        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtPos.Text))
            {
                MessageBox.Show("Vui lòng nhập Tên chức vụ.", "Dữ liệu thiếu", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!float.TryParse(txtBaseSalary.Text, out float salary) || salary < 0)
            {
                MessageBox.Show("Lương cơ bản phải là số dương hợp lệ.", "Dữ liệu sai", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }
    }
}