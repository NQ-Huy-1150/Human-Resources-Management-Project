using System;
using System.Collections.ObjectModel;
using System.Windows;
using Microsoft.Data.SqlClient; // Dùng bản Microsoft người khác vừa cài

namespace WpfApp16
{
    public class PhongBan
    {
        // Phải khớp với cột department_id và department_name trong SQL
        public string ma_phongban { get; set; }
        public string ten_phongban { get; set; }
    }
    public partial class MainWindow : Window
    {
        public ObservableCollection<PhongBan> dsPhongBan { get; set; }

        // Đã cập nhật Server: .\SQLEXPRESS2022 và Database: HumanResourceManagement
        private string connectionString = @"Data Source=.\SQLEXPRESS2022;Initial Catalog=HumanResourceManagement;Integrated Security=True;TrustServerCertificate=True";

        public MainWindow()
        {
            InitializeComponent();
            LoadDataFromSQL();
        }

        private void LoadDataFromSQL()
        {
            dsPhongBan = new ObservableCollection<PhongBan>();
            // Truy vấn đúng tên bảng 'departments'
            string query = "SELECT department_id, department_name FROM departments";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        dsPhongBan.Add(new PhongBan
                        {
                            // Đọc đúng tên cột trong SQL của người khác
                            ma_phongban = reader["department_id"].ToString(),
                            ten_phongban = reader["department_name"].ToString()
                        });
                    }
                    dgPhongBan.ItemsSource = dsPhongBan;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống: " + ex.Message);
                }
            }
        }

        // Cập nhật hàm Add để đẩy dữ liệu thật vào SQL
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            string insertQuery = "INSERT INTO departments (department_id, department_name) VALUES ('MKT', N'Marketing')";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(insertQuery, conn);
                    cmd.ExecuteNonQuery();
                    LoadDataFromSQL(); // Refresh lại bảng
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e) { }
        private void Button_Click_1(object sender, RoutedEventArgs e) { }
        private void BtnEdit_Click(object sender, RoutedEventArgs e) { }
        private void BtnDelete_Click(object sender, RoutedEventArgs e) { }
    }
}