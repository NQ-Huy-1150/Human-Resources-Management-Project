using LaptopShopApplication.Repository;
using layout.repository;
using layout.view.Main_Window;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
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
using static layout.luong.SalaryDay;

namespace layout.luong
{
    /// <summary>
    /// Interaction logic for Luong.xaml
    /// </summary>
    public partial class Luong : Page
    {
        public Luong()
        {
            InitializeComponent();
        }

        private void LoadSalaryData()
        {
            try
            {
                SalaryRepository salaryRepository = new SalaryRepository();
                List<SalaryDay.Luong> salaryList = salaryRepository.getAllSalary();

                DgNhanVien.ItemsSource = salaryList;

                if (salaryList == null || salaryList.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu lương.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu lương: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadSalaryData(); 
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SalaryDay.Luong selected = DgNhanVien.SelectedItem as SalaryDay.Luong;

            if (selected == null)
            {
                MessageBox.Show("Vui lòng chọn một nhân viên.", "Thông Báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NavigationService.Navigate(new NhanvienInfo(selected));
        }
    }
}
