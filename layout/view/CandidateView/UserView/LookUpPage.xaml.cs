using layout.repository;
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
    /// Interaction logic for LookUpPage.xaml
    /// </summary>
    public partial class LookUpPage : Page
    {
        RecruitmentDetailService service = new RecruitmentDetailService();
        public LookUpPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            status.Text = service.findByLookupId(id.Text);
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
