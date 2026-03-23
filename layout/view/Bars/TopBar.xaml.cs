using layout.view.CandidateView.UserView;
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

namespace layout.view.Bars
{
    /// <summary>
    /// Interaction logic for TopBar.xaml
    /// </summary>
    public partial class TopBar : UserControl
    {
        public TopBar()
        {
            InitializeComponent();
        }

        private void jobListBtn(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as HomePageWindow;
            if (mainWindow != null)
            {
                mainWindow.HomeFrame.Navigate(new JobLitstPage());
            }
        }

        private void lookUpBtn(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as HomePageWindow;
            if (mainWindow != null)
            {
                mainWindow.HomeFrame.Navigate(new LookUpPage());
            }
        }

        private void login(object sender, RoutedEventArgs e)
        {
            LoginWindow lg = new LoginWindow();
            lg.Owner = Window.GetWindow(this); // Đặt cửa sổ cha là HomePageWindow hiện tại
            lg.ShowDialog(); // Dùng ShowDialog thay vì Show
        }
        public void updateLoginStatus(string name)
        {
            logBtn.Visibility = Visibility.Collapsed;
            show.Text = name;
            show.Visibility = Visibility.Visible;
            tk.Visibility = Visibility.Visible;
            lookupSP.Margin = new Thickness(10, 0, 2, 0);
        }
        public void nonLoginPage()
        {
            tk.Visibility = Visibility.Collapsed;
            lookupSP.Margin = new Thickness(330, 0, 2, 0);
        }
    }
}
