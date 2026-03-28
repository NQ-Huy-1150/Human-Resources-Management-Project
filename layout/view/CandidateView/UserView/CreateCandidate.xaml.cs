using layout.domain;
using layout.service;
using layout.view.Main_Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            if (!isValidInput())
            {
                return;
            }

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
        private void backToParentBtn(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as HomePageWindow;
            if (mainWindow != null)
            {
                mainWindow.HomeFrame.Navigate(new JobLitstPage());
            }
        }

        private void create()
        {
            Candidate candidate = new Candidate();
            candidate.fullName = name.Text.Trim();
            candidate.email = emailInput.Text.Trim();
            candidate.phone = phone.Text.Trim();
            candidate.address = address.Text.Trim();
            candidate.yearOfExp = Convert.ToInt32(exp.Text.Trim());
            candidate.edu_level = edu.Text.Trim();
            candidate.recruitId = Convert.ToInt32(recruitId.Text);
            string temp = GenCodeForTrackingPurposeOnly();
            value = temp;
            candidate.lookupId = value;
            // default value
            candidate.status = "Chờ xét duyệt";
            service.getCreateRecruitment(candidate);
        }

        private bool isValidInput()
        {
            if (string.IsNullOrWhiteSpace(name.Text) ||
                string.IsNullOrWhiteSpace(emailInput.Text) ||
                string.IsNullOrWhiteSpace(phone.Text) ||
                string.IsNullOrWhiteSpace(address.Text) ||
                string.IsNullOrWhiteSpace(exp.Text) ||
                string.IsNullOrWhiteSpace(edu.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin ứng viên.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            string email = emailInput.Text.Trim();
            if (!Regex.IsMatch(email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
            {
                MessageBox.Show("Email không đúng định dạng.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            string phoneNumber = phone.Text.Trim();
            if (!Regex.IsMatch(phoneNumber, @"^\d{8,15}$"))
            {
                MessageBox.Show("Số điện thoại chỉ gồm số và dài từ 8 đến 15 ký tự.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            int yearOfExp;
            if (!int.TryParse(exp.Text.Trim(), out yearOfExp) || yearOfExp < 0)
            {
                MessageBox.Show("Số năm kinh nghiệm phải là số nguyên >= 0.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
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
