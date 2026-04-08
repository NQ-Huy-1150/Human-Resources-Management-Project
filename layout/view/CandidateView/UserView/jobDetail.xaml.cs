using layout.domain;
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
    /// Interaction logic for jobDetail.xaml
    /// </summary>
    public partial class jobDetail : Page
    {
        Recruitment recruitment = new Recruitment();
        public jobDetail(Recruitment recruitment)
        {
            InitializeComponent();
            this.recruitment = recruitment;
            hiddenId.Text = Convert.ToString(recruitment.id);
            departmentId.Text = recruitment.departmentId;
            position.Text = recruitment.position;
            income.Text = recruitment.estimateIncome.ToString("N0");
            condition.Text = recruitment.condition;
            dateShow.Text = Convert.ToString(recruitment.subDeadline);
            quantity.Text = Convert.ToString(Convert.ToString(recruitment.quantity));
            status.Text = recruitment.status;
            desc.Text = recruitment.description;
        }
        private void backToParentBtn(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as HomePageWindow;
            if (mainWindow != null)
            {
                mainWindow.HomeFrame.Navigate(new JobLitstPage());
            }
        }

        private void createBtn(object sender, RoutedEventArgs e)
        {
            if(!status.Text.Equals("Đang hoạt động"))
            {
                MessageBox.Show("Hiện tại đơn này không khả dụng, vui lòng quay lại vào thời điểm khác !");
                return;
            }
            var mainWindow = Window.GetWindow(this) as HomePageWindow;
            if (mainWindow != null)
            {
                mainWindow.HomeFrame.Navigate(new CreateCandidate(Convert.ToInt32(hiddenId.Text), position.Text));
            }
        }

    }
}
