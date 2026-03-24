using layout.view.Bars;
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
        }
        
        public HomePageWindow(string userName)
        {
            InitializeComponent();
            
        }

        public void UpdateUser(int newUserId)
        {
            this.userId = newUserId;
            topBar.updateLoginStatus(userId);
        }
    }
}
