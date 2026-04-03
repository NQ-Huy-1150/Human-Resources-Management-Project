using layout.domain;
using layout.service;
using System.Windows;
using System.Windows.Controls;

namespace layout.view.phongban
{
    public partial class DepartmentPage : Page
    {
        private readonly SuperDepartmentService _service = new SuperDepartmentService();

        public DepartmentPage()
        {
            InitializeComponent();
            LoadData(); 
        }

        private void LoadData()
        {
            dgDepartments.ItemsSource = _service.getAllDepartments();
        }

        private void dgDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDepartments.SelectedItem is Department selected)
            {
                txtId.Text = selected.department_id;
                txtName.Text = selected.department_name;

                txtId.IsReadOnly = true;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtId.Text) || string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Mã và Tên phòng ban!");
                return;
            }

            var dept = new Department
            {
                department_id = txtId.Text.Trim(),
                department_name = txtName.Text.Trim()
            };

            if (_service.addDepartment(dept))
            {
                MessageBox.Show("Thêm phòng ban mới thành công!");
                ClearInputs();
                LoadData();
            }
            else
            {
                MessageBox.Show("Thêm thất bại!");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var dept = new Department
            {
                department_id = txtId.Text.Trim(),
                department_name = txtName.Text.Trim()
            };

            if (_service.updateDepartment(dept))
            {
                MessageBox.Show("Cập nhật thông tin thành công!");
                LoadData();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!");
            }
        }

        // 5. Nút Xóa
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgDepartments.SelectedItem is Department selected)
            {
                var result = MessageBox.Show($"Bạn có chắc muốn xóa phòng {selected.department_name} không?",
                                           "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    if (_service.deleteDepartment(selected.department_id))
                    {
                        MessageBox.Show("Đã xóa phòng ban!");
                        ClearInputs();
                        LoadData();
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa phòng ban này!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Bạn hãy chọn một phòng ban trong bảng trước khi xóa!");
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearInputs();
        }

        private void ClearInputs()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtId.IsReadOnly = false; 
            dgDepartments.SelectedItem = null;
        }
    }
}