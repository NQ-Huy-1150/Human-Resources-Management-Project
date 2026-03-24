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
using System.Windows.Shapes;
using layout.service;

namespace layout.view.Main_Window
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Nguoidungservice service = new Nguoidungservice();
        private int userId;
        public MainWindow()
        {
            InitializeComponent();
            MainFrame.Navigate(new AdminDashboardPage());
        }
        public MainWindow(int userId)
        {
            InitializeComponent();
            this.userId = userId;
            sideBar.getUserNameForAdminPage(service.getUserNameById(this.userId));
            sideBar.setUserId(this.userId);
            MainFrame.Navigate(new AdminDashboardPage());
        }
    }
}
