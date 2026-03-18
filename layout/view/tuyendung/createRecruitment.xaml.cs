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
        DepartmentService service = new DepartmentService();
        RecruitmentService reService = new RecruitmentService();
        public createRecruitment()
        {
            InitializeComponent();
            DataTable data = service.getAllDepartmentName();
            List<string> departName = new List<string>();
            foreach (DataRow row in data.Rows)
            {
                string name = row["ten_phongban"].ToString();
                departName.Add(name);
            }
            departmentNameCBX.ItemsSource = departName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void createBtn(object sender, RoutedEventArgs e)
        {
            createRecruit();
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

        private void createRecruit()
        {
            string id = service.getIdByName(departmentNameCBX.Text);
            if (id.Equals("None"))
            {
                return;
            }
            Recruitment re = new Recruitment();
            re.departmentId = id;
            re.position = positionInput.Text;
            re.estimateIncome = float.Parse(incomeInput.Text);
            re.subDeadline = Convert.ToDateTime(date.Text);
            re.condition = conditionInput.Text;
            re.status = "Đang hoạt động";
            re.quantity = Convert.ToInt32(quantityInput.Text);
            reService.getCreateRecruitment(re);
        }


    }
}
