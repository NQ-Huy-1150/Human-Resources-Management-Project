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

namespace layout.view.CandidateView.UserView
{
    /// <summary>
    /// Interaction logic for CreateCandidate.xaml
    /// </summary>
    public partial class CreateCandidate : Page
    {
        int recruitmentId = 0;
        string canPosition = "";
        string value = "";
        RecruitmentDetailService service = new RecruitmentDetailService();
        public CreateCandidate(int recruitmentId, string canPosition)
        {
            InitializeComponent();
            this.recruitmentId = recruitmentId;
            this.canPosition = canPosition;
            recruitId.Text = Convert.ToString(recruitmentId);
            position.Text = canPosition;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            create();
            if (string.IsNullOrEmpty(value))
            {
                MessageBox.Show("Tạo hồ sơ ứng tuyển thất bại.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            var mainWindow = Window.GetWindow(this) as HomePageWindow;
            if (mainWindow != null)
            {
                mainWindow.HomeFrame.Navigate(new ThankYouPage(value));
            }

        }
        private void create()
        {
            Candidate candidate = new Candidate();
            candidate.fullName = name.Text;
            candidate.email = emailInput.Text;
            candidate.phone = phone.Text;
            candidate.address = address.Text;
            candidate.yearOfExp = Convert.ToInt32(exp.Text);
            candidate.edu_level = edu.Text;
            candidate.recruitId = Convert.ToInt32(recruitId.Text);
            // default value
            candidate.status = "Chờ xét duyệt";
            int candidateId = service.getCreateRecruitment(candidate);
            value = candidateId > 0 ? ("UV-" + candidateId) : "";
        }
        private void backToParentBtn(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as HomePageWindow;
            if (mainWindow != null)
            {
                mainWindow.HomeFrame.Navigate(new JobLitstPage());
            }
        }


    }
}
