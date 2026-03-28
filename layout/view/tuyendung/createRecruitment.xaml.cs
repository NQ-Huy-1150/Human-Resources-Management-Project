using layout.service;
using layout.view.Main_Window;
using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace layout.view.tuyendung
{
    /// <summary>
    /// Interaction logic for createRecruitment.xaml
    /// </summary>
    public partial class createRecruitment : Page
    {
        private readonly DepartmentService service = new DepartmentService();
        private readonly PositionService positionService = new PositionService();
        private readonly RecruitmentService reService = new RecruitmentService();
        private DataTable positionData;

        public createRecruitment()
        {
            InitializeComponent();
            loadComboboxData();
        }

        private void loadComboboxData()
        {
            DataTable data = service.getAllDepartmentName();
            departmentNameCBX.ItemsSource = data.DefaultView;
            departmentNameCBX.DisplayMemberPath = "ten_phongban";
            departmentNameCBX.SelectedValuePath = "ten_phongban";

            positionData = positionService.getAllPositionName();
            positionInput.ItemsSource = positionData.DefaultView;
            positionInput.DisplayMemberPath = "pos_name";
            positionInput.SelectedValuePath = "pos_id";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new AdminDashboardPage());
            }

        }

        private void positionInput_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (positionInput.SelectedValue == null)
            {
                incomeInput.Text = string.Empty;
                return;
            }

            int positionId = Convert.ToInt32(positionInput.SelectedValue);
            float baseSalary = positionService.getBaseSalary(positionId);
            incomeInput.Text = baseSalary.ToString("N0");
        }

        private void createBtn(object sender, RoutedEventArgs e)
        {
            if (!createRecruit())
            {
                return;
            }

            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new Tuyendung());
            }
        }

        private void backToParentBtn(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new Tuyendung());
            }
        }

        private bool createRecruit()
        {
            if (string.IsNullOrWhiteSpace(departmentNameCBX.Text) ||
                string.IsNullOrWhiteSpace(positionInput.Text) ||
                string.IsNullOrWhiteSpace(quantityInput.Text) ||
                string.IsNullOrWhiteSpace(conditionInput.Text) ||
                string.IsNullOrWhiteSpace(descInput.Text) ||
                !date.SelectedDate.HasValue)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin tuyển dụng.");
                return false;
            }

            if (positionInput.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn vị trí tuyển dụng.");
                return false;
            }

            string id = service.getIdByName(departmentNameCBX.Text);
            if (id.Equals("None"))
            {
                MessageBox.Show("Phòng ban không hợp lệ.");
                return false;
            }

            int positionId = Convert.ToInt32(positionInput.SelectedValue);
            float income = positionService.getBaseSalary(positionId);
            if (income <= 0)
            {
                MessageBox.Show("Không tìm thấy mức lương theo vị trí đã chọn.");
                return false;
            }

            if (!int.TryParse(quantityInput.Text, out int quantity))
            {
                MessageBox.Show("Số lượng hồ sơ không hợp lệ.");
                return false;
            }

            if (quantity <= 0)
            {
                MessageBox.Show("Số lượng hồ sơ phải lớn hơn 0.");
                return false;
            }

            if (date.SelectedDate.Value.Date < DateTime.Today)
            {
                MessageBox.Show("Hạn nộp hồ sơ phải từ hôm nay trở đi.");
                return false;
            }

            Recruitment re = new Recruitment();
            re.departmentId = id;
            re.position = positionInput.Text;
            re.estimateIncome = income;
            re.subDeadline = date.SelectedDate.Value;
            re.condition = conditionInput.Text;
            re.status = "Đang hoạt động";
            re.quantity = quantity;
            re.description = descInput.Text;
            reService.getCreateRecruitment(re);
            return true;
        }


    }
}
