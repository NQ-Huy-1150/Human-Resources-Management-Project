using layout.service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace layout
{
    public partial class Tuyendung : Page
    {
        public ObservableCollection<Recruitment> nhieuDonTuyenDung = new ObservableCollection<Recruitment>();
        public Tuyendung()
        {
            InitializeComponent();
            RecruitmentService recruitmentService = new RecruitmentService();
            table.ItemsSource = recruitmentService.fetchAllRecruitment().DefaultView;

        }

        private void BtnChiTiet(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
