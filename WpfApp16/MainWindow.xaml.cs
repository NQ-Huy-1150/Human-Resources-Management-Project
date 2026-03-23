using System.Collections.ObjectModel;
using System.Windows;

namespace WpfApp16
{
    // Lớp mô phỏng dữ liệu phòng ban
    public class PhongBan
    {
        public string ma_phongban { get; set; }
        public string ten_phongban { get; set; }
    }

    public partial class MainWindow : Window
    {
        // ObservableCollection giúp bảng DataGrid tự động cập nhật khi thêm/xóa
        public ObservableCollection<PhongBan> dsPhongBan { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            KhoiTaoDuLieu();
        }

        private void KhoiTaoDuLieu()
        {
            // Tạo dữ liệu giả ban đầu
            dsPhongBan = new ObservableCollection<PhongBan>
            {
                new PhongBan { ma_phongban = "IT", ten_phongban = "Phòng Công nghệ" },
                new PhongBan { ma_phongban = "HR", ten_phongban = "Phòng Nhân sự" },
                new PhongBan { ma_phongban = "KT", ten_phongban = "Phòng Kế toán" }
            };

            // Gán nguồn dữ liệu cho DataGrid có tên dgPhongBan trong XAML
            dgPhongBan.ItemsSource = dsPhongBan;
        }

        // Xử lý sự kiện khi bấm nút "+ Thêm phòng ban"
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            dsPhongBan.Add(new PhongBan
            {
                ma_phongban = "MOI",
                ten_phongban = "Phòng Ban Mới"
            });
        }

        // Xử lý sự kiện khi bấm nút "Sửa" trong từng dòng
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgPhongBan.SelectedItem as PhongBan;
            if (selected != null)
            {
                MessageBox.Show($"Đang thực hiện sửa cho: {selected.ten_phongban}");
            }
        }

        // Xử lý sự kiện khi bấm nút "Xóa" trong từng dòng
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgPhongBan.SelectedItem as PhongBan;
            if (selected != null)
            {
                var result = MessageBox.Show($"Người khác có chắc muốn xóa {selected.ten_phongban} không?",
                                           "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    dsPhongBan.Remove(selected);
                }
            }
        }

        // Các hàm hỗ trợ sự kiện Click từ Menu của trưởng nhóm
        private void Button_Click(object sender, RoutedEventArgs e) { }
        private void Button_Click_1(object sender, RoutedEventArgs e) { }
        private void Button_Click_2(object sender, RoutedEventArgs e) { }
    }
}