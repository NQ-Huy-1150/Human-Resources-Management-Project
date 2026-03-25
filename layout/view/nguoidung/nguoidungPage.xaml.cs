using layout.service;
using layout.view.Main_Window;
using layout.view.Nguoidung;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace layout.view.nguoidung
{
    /// <summary>
    /// Interaction logic for nguoidungPage.xaml
    /// </summary>
    public partial class nguoidungPage : Page
    {
        private readonly Nguoidungservice service = new Nguoidungservice();

        public nguoidungPage()
        {
            InitializeComponent();
            loadNguoiDung();
            NguoidungGrid.AutoGeneratingColumn += NguoidungGrid_AutoGeneratingColumn;
        }

        private void NguoidungGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.PropertyName == "mat_khau")
            {
                e.Cancel = true; // Không tạo cột này
                return;
            }

            if (e.PropertyName == "ma_nguoidung" || e.PropertyName == "vai_tro")
            {
                e.Column.IsReadOnly = true;
            }
        }

        private void loadNguoiDung()
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
            DataRowView row = NguoidungGrid.SelectedItem as DataRowView;

            if (row != null)
            {
                if (!isValidUserRow(row))
                {
                    return;
                }

                bool result = service.capnhatNguoidung(row);

                if (result)
                {
                    MessageBox.Show("Cập nhật thành công");
                    loadNguoiDung();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại");
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn người dùng cần cập nhật.");
            }

        }

        private bool isValidUserRow(DataRowView row)
        {
            string hoTen = Convert.ToString(row["ho_ten"]).Trim();
            string email = Convert.ToString(row["thu_dien_tu"]).Trim();
            string matKhau = Convert.ToString(row["mat_khau"]).Trim();
            string diaChi = Convert.ToString(row["dia_chi"]).Trim();
            string soDienThoai = Convert.ToString(row["so_dien_thoai"]).Trim();
            string maPhongBan = Convert.ToString(row["ma_phongban"]).Trim();

            if (string.IsNullOrWhiteSpace(hoTen) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(matKhau) ||
                string.IsNullOrWhiteSpace(diaChi) ||
                string.IsNullOrWhiteSpace(soDienThoai) ||
                string.IsNullOrWhiteSpace(maPhongBan))
            {
                MessageBox.Show("Không được để trống các trường thông tin bắt buộc.");
                return false;
            }

            if (!Regex.IsMatch(email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
            {
                MessageBox.Show("Email không đúng định dạng.");
                return false;
            }

            if (!Regex.IsMatch(soDienThoai, @"^\d{8,15}$"))
            {
                MessageBox.Show("Số điện thoại chỉ gồm số và độ dài từ 8 đến 15 ký tự.");
                return false;
            }

            return true;
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
                        loadNguoiDung();
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
