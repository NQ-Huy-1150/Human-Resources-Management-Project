using layout.service;
using layout.view.Main_Window;
using System;
using System.Collections.Generic;
using System.Deployment.Internal;
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

namespace layout.view.tuyendung
{
    /// <summary>
    /// Interaction logic for deletePage.xaml
    /// </summary>
    public partial class deletePage : Page
    {
        RecruitmentService service = new RecruitmentService();
        public deletePage(int id)
        {
            InitializeComponent();
            recruitId.Text = Convert.ToString(id);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new AdminDashboardPage());
            }
        }
        private void deleteBtn(object sender, RoutedEventArgs e)
        {
            service.getDelete(Convert.ToInt32(recruitId.Text));
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new Tuyendung());
            }
        }
        private void backToParentBtn(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new Tuyendung());
            }
        }
    }
}
