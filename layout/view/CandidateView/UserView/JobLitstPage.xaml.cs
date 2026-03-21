using layout.service;
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
    /// Interaction logic for JobLitstPage.xaml
    /// </summary>
    public partial class JobLitstPage : Page
    {
        RecruitmentService service = new RecruitmentService();
        public JobLitstPage()
        {
            InitializeComponent();
            jobList.ItemsSource = service.fetchAllJobs().DefaultView;
        }
    }
}
