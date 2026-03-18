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

namespace layout.view.tuyendung
{
    /// <summary>
    /// Interaction logic for detailPage.xaml
    /// </summary>
    public partial class detailPage : Page
    {
        public detailPage(Recruitment recruitment)
        {
            InitializeComponent();
            recruitId.Text = Convert.ToString(recruitment.id);
            departmentId.Text = recruitment.departmentId;
            position.Text = recruitment.position;
            income.Text = Convert.ToString(recruitment.estimateIncome);
            condition.Text = recruitment.condition;
            dateShow.Text = Convert.ToString(recruitment.subDeadline);
            quantity.Text = Convert.ToString(Convert.ToString(recruitment.quantity));
            status.Text = recruitment.status;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

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
