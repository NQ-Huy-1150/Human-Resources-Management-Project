using layout.domain;
using layout.luong;
using layout.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layout.service
{
    public class SalaryService
    {
        private readonly SalaryRepository repo = new SalaryRepository();

        public List<SalaryCal.Luong> getAllSalary()
        {
            return repo.getAllSalary();
        }
        public bool UpdateSalary(int id, double allowance, double bonus, double deduction)
        {
            return repo.UpdateSalary(id, allowance, bonus, deduction);
        }
        public void createSalary(Payroll payroll)
        {
            repo.createSalary(payroll);
        }
    }
}
