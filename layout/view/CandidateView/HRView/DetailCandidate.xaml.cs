using layout.domain;
using layout.service;
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

namespace layout.view.CandidateView.HRView
{
    /// <summary>
    /// Interaction logic for DetailCandidate.xaml
    /// </summary>
    public partial class DetailCandidate : Page
    {
        Candidate candidate = new Candidate();
        RecruitmentService service = new RecruitmentService();
        public DetailCandidate(Candidate candidate)
        {
            InitializeComponent();
            this.candidate = candidate;
            id.Text = Convert.ToString(candidate.id);
            recruitId.Text = Convert.ToString(candidate.recruitId);
            name.Text = candidate.fullName;
            emailInput.Text = candidate.email;
            address.Text = candidate.address;
            phone.Text = candidate.phone;
            edu.Text = candidate.edu_level;
            exp.Text = Convert.ToString(candidate.yearOfExp);
            status.Text = candidate.status;
            position.Text = service.getPosition(candidate.recruitId);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void backToParentBtn(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new CandidatePage(candidate.recruitId));
            }
        }
    }
}
