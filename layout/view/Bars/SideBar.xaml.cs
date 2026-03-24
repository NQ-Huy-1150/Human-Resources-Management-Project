using layout.view.chamcong;
using layout.view.Main_Window;
using layout.view.nguoidung;
using layout.luong;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace layout.view.Bars
{
    /// <summary>
    /// Interaction logic for SideBar.xaml
    /// </summary>
    public partial class SideBar : UserControl
    {
        public SideBar()
        {
            InitializeComponent();
        }

        private void ResetMenuButtonBackground()
        {
            attenBtn.Background = Brushes.Transparent;
            btnRecruit.Background = Brushes.Transparent;
            usersBtn.Background = Brushes.Transparent;
            salaryBtn.Background = Brushes.Transparent;
        }

        private void SetActiveButton(Button activeButton)
        {
            ResetMenuButtonBackground();
            activeButton.Background = Brushes.YellowGreen;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                SetActiveButton(attenBtn);
                mainWindow.MainFrame.Navigate(new AdminPage());
            }
        }

        private void TuyenDungPage(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                SetActiveButton(btnRecruit);
                mainWindow.MainFrame.Navigate(new Tuyendung());
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                SetActiveButton(usersBtn);
                mainWindow.MainFrame.Navigate(new nguoidungPage());
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                SetActiveButton(salaryBtn);
                mainWindow.MainFrame.Navigate(new Luong());
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            HomePageWindow home = new HomePageWindow();
            home.Show();
            // Đóng window đã login
            foreach (Window window in Application.Current.Windows)
            {
                if (window is MainWindow && window != home)
                {
                    window.Close();
                }
            }
        }

        public void getUserNameForAdminPage(string name)
        {
            username.Text = $"@{name}";
        }
    }
}
