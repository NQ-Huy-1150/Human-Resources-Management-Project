using layout.domain;
using layout.luong;
using layout.repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layout.service
{
    public class SalaryService
    {
        private readonly SalaryRepository repo = new SalaryRepository();

        public DataTable getAllPayRoll()
        {
            return repo.getAllPayRoll();
        }
        public Payroll getPayRollById(int payrollId)
        {
            return repo.getPayRollById(payrollId);
        }
        public bool UpdateSalary(Payroll payroll)
        {
            return repo.UpdateSalary(payroll);
        }
        public void createSalary(Payroll payroll)
        {
            repo.createSalary(payroll);
        }
        public DataTable getCurrentMonthAndYearPayRoll(int userId)
        {
            return repo.getCurrentMonthAndYearPayRoll(userId);
        }
        public bool isPayRollWithCurrentMonthAndYearExisted(int userId)
        {
            return repo.isPayRollWithCurrentMonthAndYearExisted(userId);
        }

        public float getTotalSalaryCurrentMonth()
        {
            return repo.getTotalSalaryCurrentMonth();
        }
    }
}
