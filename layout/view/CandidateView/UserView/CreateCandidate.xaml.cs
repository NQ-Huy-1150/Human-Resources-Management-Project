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
            string temp = GenCodeForTrackingPurposeOnly();
            value = temp;
            candidate.lookupId = value;
            // default value
            candidate.status = "Chờ xét duyệt";
            service.getCreateRecruitment(candidate);
        }
        private void backToParentBtn(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as HomePageWindow;
            if (mainWindow != null)
            {
                mainWindow.HomeFrame.Navigate(new JobLitstPage());
            }
        }

        private string GenCodeForTrackingPurposeOnly()
        {
            Random res = new Random();
            string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int size = 5;
            string code = "UV-";
            for (int i = 0; i < size; i++)
            {
                int x = res.Next(str.Length);
                code += str[x];
            }
            return code; // Ví dụ: UV-X7R2B
        }
    }
}
