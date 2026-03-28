using layout.view.chamcong;
using layout.view.Department;
using layout.view.Main_Window;
using layout.view.nguoidung;
using layout.luong;
using layout.view.Chucvu2;
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
        private int userId = -1;
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
            departBtn.Background = Brushes.Transparent;
            posBtn.Background = Brushes.Transparent;
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

        private void openDashboardBtn_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                ResetMenuButtonBackground();
                mainWindow.MainFrame.Navigate(new AdminDashboardPage());
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

        private void departBtn_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                SetActiveButton(departBtn);
                mainWindow.MainFrame.Navigate(new DepartmentPage());
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

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            HomePageWindow home = new HomePageWindow(userId);
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

        public void setUserId(int userId)
        {
            this.userId = userId;
        }
        private void posBtn_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                SetActiveButton(posBtn); // Làm nổi bật nút Chức vụ
                mainWindow.MainFrame.Navigate(new chucvu()); // Điều hướng đến trang chucvu
            }
        }
    }
}
