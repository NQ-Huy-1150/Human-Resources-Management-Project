using layout.view.CandidateView.UserView;
using layout.view.chamcong;
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
            tkBtn.Visibility = Visibility.Visible;
            tk.Margin = new Thickness(50, 0, 2, 0);
            lookupSP.Margin = new Thickness(50, 0, 2, 0);
            logoutBtn.Visibility = Visibility.Visible;
        }
        public void nonLoginPage()
        {
            tk.Visibility = Visibility.Collapsed;
            tkBtn.Visibility = Visibility.Collapsed;
            logoutBtn.Visibility = Visibility.Collapsed;
            lookupSP.Margin = new Thickness(350, 0, 2, 0);
        }

        private void logoutBtn_Click(object sender, RoutedEventArgs e)
        {

            HomePageWindow home = new HomePageWindow();
            home.Show();
            // Đóng window đã login
            foreach (Window window in Application.Current.Windows)
            {
                if (window is HomePageWindow && window != home)
                {
                    window.Close();
                }
            }
            
        }

        private void tkBtn_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as HomePageWindow;
            if (mainWindow != null)
            {
                mainWindow.HomeFrame.Navigate(new UserView());
            }
        }
    }
}
