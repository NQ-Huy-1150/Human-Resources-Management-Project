using layout.service;
using layout.view.CandidateView.HRView;
using layout.view.Main_Window;
using layout.view.tuyendung;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Diagnostics;
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

namespace layout
{
    public partial class Tuyendung : Page
    {
        RecruitmentService reService = new RecruitmentService();
        public Tuyendung()
        {
            InitializeComponent();
            RecruitmentService recruitmentService = new RecruitmentService();
            table.ItemsSource = recruitmentService.fetchAllRecruitment().DefaultView;

        }

        private void btnDetail(object sender, RoutedEventArgs e)
        {
            viewDetail();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new AdminDashboardPage());
            }
        }

        private void CreateBtn(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new createRecruitment());
            }
        }

        private void deleteBnt(object sender, RoutedEventArgs e)
        {
            int id = getIdFromSelectedRow();
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new deletePage(id));
            }
        }
        private void updateBnt(object sender, RoutedEventArgs e)
        {
            int id = getIdFromSelectedRow();

            if (id != -1)
            {
                Recruitment re = convertDataTableToObject(id);
                var mainWindow = Window.GetWindow(this) as MainWindow;
                if (mainWindow != null)
                {
                    mainWindow.MainFrame.Navigate(new updatePage(re));
                }
            }
        }
        private void viewDetail()
        {
            int id = getIdFromSelectedRow();
            
            if(id != -1)
            {
                Recruitment re = convertDataTableToObject(id);
                var mainWindow = Window.GetWindow(this) as MainWindow; 
                if (mainWindow != null)
                {
                    mainWindow.MainFrame.Navigate(new detailPage(re));
                }
            }
        }
        private int getIdFromSelectedRow()
        {
            int id = -1;

            // Ép kiểu SelectedItem về DataRowView
            DataRowView selectedRow = table.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                // Lấy giá trị theo Tên cột
                id = Convert.ToInt32(selectedRow["id"]);

                Debug.WriteLine(">>>>>>>>>>>>> " + id);
            }
            return id;
        }
        private string getDepartmentIdFromSelectedRow()
        {
            string text = "";

            // Ép kiểu SelectedItem về DataRowView
            DataRowView selectedRow = table.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                // Lấy giá trị theo Tên cột
                text = Convert.ToString(selectedRow["departmentId"]);

                Debug.WriteLine(">>>>>>>>>>>>> " + text);
            }
            return text;
        }
        private Recruitment convertDataTableToObject(int id)
        {
            Recruitment rec = new Recruitment();
            DataTable data =  reService.fetchById(id);
            foreach (DataRow row in data.Rows)
            {
                rec.id = Convert.ToInt32(row["id"].ToString());
                rec.departmentId = row["departmentId"].ToString();
                rec.position = row["position"].ToString();
                rec.status = row["status"].ToString();
                rec.estimateIncome = float.Parse(row["estimateIncome"].ToString());
                rec.condition = row["condition"].ToString();
                rec.subDeadline = Convert.ToDateTime(row["sub_deadline"].ToString());
                rec.quantity = Convert.ToInt32(row["quantity"].ToString());
                rec.description = Convert.ToString(row["description"].ToString());
            }
            return rec;
        }

        private void viewCandidate(object sender, RoutedEventArgs e)
        {
            int recruitId = getIdFromSelectedRow();
            string departmentId = getDepartmentIdFromSelectedRow();
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new CandidatePage(recruitId, departmentId));
            }
        }
    }
}
