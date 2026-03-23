using layout.domain;
using layout.service;
using layout.view.Main_Window;
using layout.view.tuyendung;
using layout;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
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

namespace layout.view.CandidateView.HRView
{
    /// <summary>
    /// Interaction logic for CandidatePage.xaml
    /// </summary>
    public partial class CandidatePage : Page
    {
        int recruitId = 0;
        string departId = "";
        RecruitmentDetailService detailService = new RecruitmentDetailService();
        public CandidatePage(int recruitId, string departId)
        {
            InitializeComponent();
            this.recruitId = recruitId;
            this.departId = departId;
            tableData.ItemsSource = detailService.fetchAllRecruitment(recruitId).DefaultView;
        }
        
        private void backToParentBtn(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new Tuyendung());
            }
        }

        private void btnDetail(object sender, RoutedEventArgs e)
        {
            viewDetail();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void deleteBnt(object sender, RoutedEventArgs e)
        {
            int id = getIdFromSelectedRow();
            if (id == -1)
            {
                return;
            }
            Candidate candidate = convertDataTableToObject(id);
            if (candidate.status.Equals("Loại") || candidate.status.Equals("Huỷ đơn"))
            {
                Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>> XOA THANH CONG");
            }
            else Debug.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>> KHONG THE XOA UNG VIEN DANG XET DUYET");
        }

        private void updateBnt(object sender, RoutedEventArgs e)
        {
            int id = getIdFromSelectedRow();
            if (id == -1)
            {
                return;
            }
            Candidate candidate = convertDataTableToObject(id);
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new UpdateCandidate(candidate.id, candidate.recruitId, candidate.status, departId));
            }
        }

        private void viewDetail()
        {
            int id = getIdFromSelectedRow();
            if (id == -1)
            {
                return;
            }
            Candidate candidate = convertDataTableToObject(id);
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new DetailCandidate(candidate, departId));
            }

        }

        private int getIdFromSelectedRow()
        {
            int id = -1;

            // Ép kiểu SelectedItem về DataRowView
            DataRowView selectedRow = tableData.SelectedItem as DataRowView;

            if (selectedRow != null)
            {
                // Lấy giá trị theo Tên cột
                id = Convert.ToInt32(selectedRow["id"]);

                Debug.WriteLine(">>>>>>>>>>>>> " + id);
            }
            return id;
        }
        
        private Candidate convertDataTableToObject(int id)
        {
            Candidate can = new Candidate();
            DataTable data = detailService.fetchById(id);
            foreach (DataRow row in data.Rows)
            {
                can.id = Convert.ToInt32(row["id"].ToString());
                can.recruitId = Convert.ToInt32(row["recruit_id"].ToString());
                can.fullName = row["full_name"].ToString();
                can.email = row["email"].ToString();
                can.phone = row["phone_number"].ToString();
                can.address = row["address"].ToString();
                can.edu_level = row["edu_level"].ToString();
                can.yearOfExp = Convert.ToInt32(row["year_of_exp"].ToString());
                can.status = row["recruit_status"].ToString();
            }
            return can;
        }
    }
}
