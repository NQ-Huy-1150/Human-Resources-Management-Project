using layout.service;
using System.Windows;

namespace layout.view.Main_Window
{
    /// <summary>
    /// Interaction logic for HomePageWindow.xaml
    /// </summary>
    public partial class HomePageWindow : Window
    {
        int userId = -1;

        public HomePageWindow()
        {
            InitializeComponent();
            topBar.nonLoginPage();
            HomeFrame.Navigate(new HomeWelcomePage());
        }

        public HomePageWindow(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            UpdateUser(this.userId);
        }

        public void UpdateUser(int newUserId)
        {
            this.userId = newUserId;
            topBar.updateLoginStatus(userId);
            HomeFrame.Navigate(new HomeWelcomePage(userId));
        }
    }
}
