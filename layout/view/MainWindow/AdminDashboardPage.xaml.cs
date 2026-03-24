using layout.repository;
using layout.service;
using System;
using System.Linq;
using System.Windows.Controls;

namespace layout.view.Main_Window
{
    public partial class AdminDashboardPage : Page
    {
        private readonly Nguoidungservice nguoidungService = new Nguoidungservice();
        private readonly RecruitmentDetailService recruitmentDetailService = new RecruitmentDetailService();
        private readonly SalaryRepository salaryRepository = new SalaryRepository();

        public AdminDashboardPage()
        {
            InitializeComponent();
            loadStatistics();
        }

        private void loadStatistics()
        {
            try
            {
                int employeeCount = nguoidungService.getAllNguoidung().Rows.Count;
                double totalSalary = salaryRepository.getAllSalary().Sum(s => s.NetSalary);
                int recruitmentProfileCount = recruitmentDetailService.getTotalRecruitmentProfileCount();

                employeeCountText.Text = employeeCount.ToString("N0");
                totalSalaryText.Text = totalSalary.ToString("N0") + " VNĐ";
                recruitmentProfileCountText.Text = recruitmentProfileCount.ToString("N0");
            }
            catch (Exception)
            {
                employeeCountText.Text = "0";
                totalSalaryText.Text = "0 VNĐ";
                recruitmentProfileCountText.Text = "0";
            }
        }
    }
}
