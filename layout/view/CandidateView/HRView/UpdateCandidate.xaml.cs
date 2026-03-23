using layout.domain;
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

namespace layout.view.CandidateView.HRView
{
    /// <summary>
    /// Interaction logic for UpdateCandidate.xaml
    /// </summary>
    public partial class UpdateCandidate : Page
    {
        int id = 0;
        int recruitId = 0;
        string status = "";
        string departId = "";
        RecruitmentDetailService service = new RecruitmentDetailService();
        Nguoidungservice nguoidungservice = new Nguoidungservice();
        RoleService roleService = new RoleService();
 
        public UpdateCandidate(int id, int recruitId, string status, string departId)
        {
            InitializeComponent();
            this.id = id;
            this.status = status;
            this.recruitId = recruitId;
            this.departId = departId;
            setDefaultValue();

        }
        private void setDefaultValue()
        {
            List<string> statusLists = new List<string>();
            statusLists.Add("Chờ xét duyệt");        // Bước 1: Ứng viên mới nộp
            statusLists.Add("Đã tiếp nhận");         // Bước 2: HR đã xem hồ sơ
            statusLists.Add("Đang phỏng vấn");       // Bước 3: Giai đoạn phỏng vấn
            statusLists.Add("Đạt (Chờ nhận việc)");  // Bước 4: Đã qua phỏng vấn, chuẩn bị làm nhân viên
            statusLists.Add("Đã nhận việc");         // Bước 5: "Chuyển thành nhân viên" thành công
            statusLists.Add("Loại");                 // Nhánh phụ: Không phù hợp
            statusLists.Add("Hủy đơn");              // Nhánh phụ: Ứng viên tự rút hồ sơ
            statusCBX.ItemsSource = statusLists;
            hiddenId.Text = Convert.ToString(this.id);
            statusCBX.SelectedItem = status;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        
        private void updateBtn(object sender, RoutedEventArgs e)
        {
            string status = statusCBX.Text;
            TurnCandidateInToEmployee(status, this.id);
            service.getUpdateStatus(this.id, status);
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new CandidatePage(this.recruitId, departId));
            }

        }
        private void backToParentBtn(object sender, RoutedEventArgs e)
        {
            var mainWindow = Window.GetWindow(this) as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainFrame.Navigate(new CandidatePage(this.recruitId, departId));
            }
        }
        private void TurnCandidateInToEmployee(string status, int id)
        {
            if (status.Equals("Đã nhận việc"))
            {
                Candidate can = convertDataTableToObject(id);
                NguoiDung nguoi = new NguoiDung();
                nguoi.ma_phongban = departId;
                nguoi.mat_khau = autoGenFirstTimePassword();
                nguoi.thu_dien_tu = can.email;
                nguoi.dia_chi = can.address;
                nguoi.ho_ten = can.fullName;
                nguoi.ma_vaitro = roleService.getRoleId("Nhân viên");
                nguoi.so_dien_thoai = can.phone;
                string text = "";
                nguoidungservice.themnguoidung(nguoi, out text);
                if (!string.IsNullOrEmpty(text) && !text.Contains("thành công")) 
                {
                    MessageBox.Show("Có lỗi khi tạo tài khoản nhân viên: " + text, "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    MessageBox.Show($"Chuyển ứng viên thành nhân viên thành công.\nMật khẩu mới: {nguoi.mat_khau}", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else return;
        }
        private Candidate convertDataTableToObject(int id)
        {
            Candidate can = new Candidate();
            DataTable data = service.fetchById(id);
            foreach (DataRow row in data.Rows)
            {
                can.id = Convert.ToInt32(row["id"].ToString());
                can.recruitId = Convert.ToInt32(row["recruit_id"].ToString());
                can.fullName = row["full_name"].ToString();
                can.email = row["email"].ToString();
                can.phone = row["phone_number"].ToString();
                can.address = row["address"].ToString();
                can.edu_level = row["edu_level"].ToString();
                can.yearOfExp = Convert.ToInt32(row["year_of_exp"].ToString());
                can.status = row["recruit_status"].ToString();
            }
            return can;
        }
        private string autoGenFirstTimePassword()
        {
            Random res = new Random();
            string str = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            int size = 8;
            string code = "";
            for (int i = 0; i < size; i++)
            {
                int x = res.Next(str.Length);
                code += str[x];
            }
            return code; 
        }
    }
}
