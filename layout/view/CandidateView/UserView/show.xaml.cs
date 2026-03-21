using layout.service;
using layout.view.Main_Window;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for show.xaml
    /// </summary>
    public partial class show : UserControl
    {
        RecruitmentService service = new RecruitmentService();
        public show()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Recruitment re = convertDataTableToObject(Convert.ToInt32(id.Text));

            var mainWindow = Window.GetWindow(this) as HomePageWindow;
            if (mainWindow != null)
            {
                mainWindow.HomeFrame.Navigate(new jobDetail(re));
            }
        }
        private Recruitment convertDataTableToObject(int id)
        {
            Recruitment rec = new Recruitment();
            DataTable data = service.fetchById(id);
            foreach (DataRow row in data.Rows)
            {
                rec.id = Convert.ToInt32(row["id"].ToString());
                rec.departmentId = row["departmentId"].ToString();
                rec.position = row["position"].ToString();
                rec.status = row["status"].ToString();
                rec.estimateIncome = float.Parse(row["estimateIncome"].ToString());
                rec.condition = row["condition"].ToString();
                rec.subDeadline = Convert.ToDateTime(row["sub_deadline"].ToString());
                rec.quantity = Convert.ToInt32(row["quantity"].ToString());
                rec.description = Convert.ToString(row["description"].ToString());
            }
            return rec;
        }
    }
}
