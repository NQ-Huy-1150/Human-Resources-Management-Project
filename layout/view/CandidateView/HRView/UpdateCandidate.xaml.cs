using layout.domain;
using layout.service;
using layout.view.Main_Window;
using System;
using System.Collections.Generic;
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

namespace layout.view.CandidateView.HRView
{
    /// <summary>
    /// Interaction logic for UpdateCandidate.xaml
    /// </summary>
    public partial class UpdateCandidate : Page
    {
        int id = 0;
        int recruitId = 0;
        string status = "";
        RecruitmentDetailService service = new RecruitmentDetailService();
        public UpdateCandidate(int id, int recruitId, string status)
        {
            InitializeComponent();
            this.id = id;
            this.status = status;
            this.recruitId = recruitId;
            setDefaultValue();

        }
        private void setDefaultValue()
        {
            List<string> statusLists = new List<string>();
            statusLists.Add("Chờ xét duyệt");        // Bước 1: Ứng viên mới nộp
            statusLists.Add("Đã tiếp nhận");         // Bước 2: HR đã xem hồ sơ
            statusLists.Add("Đang phỏng vấn");       // Bước 3: Giai đoạn phỏng vấn
            statusLists.Add("Đạt (Chờ nhận việc)");  // Bước 4: Đã qua phỏng vấn, chuẩn bị làm nhân viên
            statusLists.Add("Đã nhận việc");         // Bước 5: "Chuyển thành nhân viên" thành công
            statusLists.Add("Loại");                 // Nhánh phụ: Không phù hợp
            statusLists.Add("Hủy đơn");              // Nhánh phụ: Ứng viên tự rút hồ sơ
            statusCBX.ItemsSource = statusLists;
            hiddenId.Text = Convert.ToString(this.id);
            statusCBX.SelectedItem = status;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        
        private void updateBtn(object sender, RoutedEventArgs e)
        {
            string status = statusCBX.Text;
            service.getUpdateStatus(this.id, status);
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new CandidatePage(this.recruitId));
            }

        }
        private void backToParentBtn(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new CandidatePage(this.recruitId));
            }
        }
    }
}
