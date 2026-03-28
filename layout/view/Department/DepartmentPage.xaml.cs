using layout.domain;
using layout.service;
using layout.view.Main_Window;
using System.Windows;
using System.Windows.Controls;
using DepartmentModel = layout.domain.Department;

namespace layout.view.Department
{
    /// <summary>
    /// Interaction logic for DepartmentPage.xaml
    /// </summary>
    public partial class DepartmentPage : Page
    {
        private readonly DepartmentService service = new DepartmentService();

        public DepartmentPage()
        {
            InitializeComponent();
            loadData();
        }

        private void loadData()
        {
            dgDepartments.ItemsSource = service.getAllDepartments();
        }

        private DepartmentModel getDepartmentFromInput()
        {
            return new DepartmentModel
            {
                department_id = txtId.Text.Trim(),
                department_name = txtName.Text.Trim()
            };
        }

        private bool isValidInput()
        {
            if (!string.IsNullOrWhiteSpace(txtId.Text) && !string.IsNullOrWhiteSpace(txtName.Text))
            {
                return true;
            }

            MessageBox.Show("Vui lòng nhập đầy đủ Mã và Tên phòng ban!");
            return false;
        }

        private void dgDepartments_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgDepartments.SelectedItem is DepartmentModel selected)
            {
                txtId.Text = selected.department_id;
                txtName.Text = selected.department_name;

                txtId.IsReadOnly = true;
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!isValidInput())
            {
                return;
            }

            if (service.addDepartment(getDepartmentFromInput()))
            {
                MessageBox.Show("Thêm phòng ban mới thành công!");
                clearInputs();
                loadData();
            }
            else
            {
                MessageBox.Show("Thêm thất bại!");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!isValidInput())
            {
                return;
            }

            if (service.updateDepartment(getDepartmentFromInput()))
            {
                MessageBox.Show("Cập nhật thông tin thành công!");
                loadData();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgDepartments.SelectedItem is DepartmentModel selected)
            {
                var result = MessageBox.Show($"Bạn có chắc muốn xóa phòng {selected.department_name} không?",
                                           "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    if (service.deleteDepartment(selected.department_id))
                    {
                        MessageBox.Show("Đã xóa phòng ban!");
                        clearInputs();
                        loadData();
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
            clearInputs();
        }

        private void clearInputs()
        {
            txtId.Text = "";
            txtName.Text = "";
            txtId.IsReadOnly = false;
            dgDepartments.SelectedItem = null;
        }

        private void OpenDashboard_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new AdminDashboardPage());
            }
        }
    }
}
