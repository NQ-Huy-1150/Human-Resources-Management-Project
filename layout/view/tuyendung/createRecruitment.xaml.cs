using layout.service;
using layout.view.Main_Window;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        public createRecruitment()
        {
            InitializeComponent();
            loadComboboxData();
        }

        private void loadComboboxData()
        {
            DataTable data = service.getAllDepartmentName();
            List<string> departName = new List<string>();
            foreach (DataRow row in data.Rows)
            {
                string name = row["ten_phongban"].ToString();
                departName.Add(name);
            }
            departmentNameCBX.ItemsSource = departName;

            DataTable positionData = positionService.getAllPositionName();
            List<string> positionName = new List<string>();
            foreach (DataRow row in positionData.Rows)
            {
                string name = row["pos_name"].ToString();
                positionName.Add(name);
            }
            positionInput.ItemsSource = positionName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

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
                string.IsNullOrWhiteSpace(incomeInput.Text) ||
                string.IsNullOrWhiteSpace(quantityInput.Text) ||
                string.IsNullOrWhiteSpace(conditionInput.Text) ||
                string.IsNullOrWhiteSpace(descInput.Text) ||
                !date.SelectedDate.HasValue)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin tuyển dụng.");
                return false;
            }

            string id = service.getIdByName(departmentNameCBX.Text);
            if (id.Equals("None"))
            {
                MessageBox.Show("Phòng ban không hợp lệ.");
                return false;
            }

            if (!float.TryParse(incomeInput.Text, out float income))
            {
                MessageBox.Show("Mức lương dự kiến không hợp lệ.");
                return false;
            }

            if (income <= 0)
            {
                MessageBox.Show("Mức lương dự kiến phải lớn hơn 0.");
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
