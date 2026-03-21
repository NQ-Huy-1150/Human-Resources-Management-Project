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
    /// Interaction logic for ThankYouPage.xaml
    /// </summary>
    public partial class ThankYouPage : Page
    {
        public ThankYouPage(string value)
        {
            InitializeComponent();
            lookup.Text = value;
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
