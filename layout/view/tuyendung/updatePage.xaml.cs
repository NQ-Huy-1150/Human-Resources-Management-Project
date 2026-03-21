using layout.service;
using layout.view.Main_Window;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Interaction logic for updatePage.xaml
    /// </summary>
    public partial class updatePage : Page
    {
        DepartmentService service = new DepartmentService();
        RecruitmentService reService = new RecruitmentService();
        public updatePage(Recruitment re)
        {
            InitializeComponent();
            setDefaultValue(re);
            
        }
        private void backToParentBtn(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new Tuyendung());
            }
        }
        private void updateBtn(object sender, RoutedEventArgs e)
        {
            updateRecruit();
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new Tuyendung());
            }
        }

        private void setDefaultValue(Recruitment recruitment)
        {
            List<string> statusLists = new List<string>();
            statusLists.Add("Đang hoạt động");
            statusLists.Add("Tạm dừng");
            statusLists.Add("Huỷ bỏ");
            statusLists.Add("Đã đóng");
            statusLists.Add("Đã hoàn thành");
            statusCBX.ItemsSource = statusLists;

            DataTable data = service.getAllDepartmentName();
            List<string> departName = new List<string>();
            foreach (DataRow row in data.Rows)
            {
                string name = row["ten_phongban"].ToString();
                departName.Add(name);
            }
            departmentNameCBX.ItemsSource = departName;
            string depart = service.fetchById(recruitment.departmentId);
            departmentNameCBX.SelectedItem = depart;

            hiddenId.Text = Convert.ToString(recruitment.id);        
            positionInput.Text = recruitment.position;
            incomeInput.Text = Convert.ToString(recruitment.estimateIncome);
            conditionInput.Text = recruitment.condition;
            date.Text = Convert.ToString(recruitment.subDeadline);
            quantityInput.Text = Convert.ToString(Convert.ToString(recruitment.quantity));
            statusCBX.SelectedItem = recruitment.status;
            descInput.Text = recruitment.description;
        }
        private void updateRecruit()
        {
            string id = service.getIdByName(departmentNameCBX.Text);
            if (id.Equals("None"))
            {
                return;
            }
            Recruitment re = new Recruitment();
            re.id = Convert.ToInt32(hiddenId.Text);
            re.departmentId = id;
            re.position = positionInput.Text;
            re.estimateIncome = float.Parse(incomeInput.Text);
            re.subDeadline = Convert.ToDateTime(date.Text);
            re.condition = conditionInput.Text;
            re.status = statusCBX.Text;
            re.quantity = Convert.ToInt32(quantityInput.Text);
            re.description = descInput.Text;
            reService.getUpdate(re);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
