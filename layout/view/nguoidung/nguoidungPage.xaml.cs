using layout.repository;
using layout.service;
using layout.view.Main_Window;
using layout.view.Nguoidung;
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

namespace layout.view.nguoidung
{
    /// <summary>
    /// Interaction logic for nguoidungPage.xaml
    /// </summary>
    public partial class nguoidungPage : Page
    {
        NguoidungRepository repo = new NguoidungRepository();
        Nguoidungservice service = new Nguoidungservice();
        public nguoidungPage()
        {
            InitializeComponent();
            LoadNguoiDung();
            NguoidungGrid.AutoGeneratingColumn += NguoidungGrid_AutoGeneratingColumn;
        }
        private void NguoidungGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "mat_khau")
            {
                e.Cancel = true; // Không tạo cột này
            }
        }
        private void LoadNguoiDung()
        {
            DataTable data = service.getAllNguoidung();
            NguoidungGrid.ItemsSource = data.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                
                mainWindow.MainFrame.Navigate(new Themnguoidung());
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)NguoidungGrid.SelectedItem;

            if (row != null)
            {
                bool result = repo.updateNguoidung(row);

                if (result)
                {
                    MessageBox.Show("Cập nhật thành công");
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại");
                }
            }

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)NguoidungGrid.SelectedItem;

            if (row != null)
            {
                int id = Convert.ToInt32(row["ma_nguoidung"]);

                // Hỏi xác nhận trước khi xóa
                var result = MessageBox.Show($"Bạn có chắc muốn xóa người dùng {row["ho_ten"]}?",
                                             "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    bool ok = service.XoaNguoiDung(id);

                    MessageBox.Show(ok ? "Xóa thành công" : "Xóa thất bại");

                    if (ok)
                    {
                        // Load lại DataGrid
                        NguoidungGrid.ItemsSource = service.getAllNguoidung().DefaultView;
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn người dùng để xóa.");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {

                //mainWindow.MainFrame.Navigate(new Timkiem());
            }


        }
    }
}
