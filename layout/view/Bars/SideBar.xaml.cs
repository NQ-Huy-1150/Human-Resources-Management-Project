using layout.view.chamcong;
using layout.view.Main_Window;
using layout.view.nguoidung;
using layout.luong;
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
    /// Interaction logic for SideBar.xaml
    /// </summary>
    public partial class SideBar : UserControl
    {
        public SideBar()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new AdminPage());
            }
        }

        private void TuyenDungPage(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                btnRecruit.Background = Brushes.YellowGreen;
                mainWindow.MainFrame.Navigate(new Tuyendung());
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                btnRecruit.Background = Brushes.YellowGreen;
                mainWindow.MainFrame.Navigate(new nguoidungPage());
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                btnRecruit.Background = Brushes.YellowGreen;
                mainWindow.MainFrame.Navigate(new Luong());
            }
        }
    }
}
