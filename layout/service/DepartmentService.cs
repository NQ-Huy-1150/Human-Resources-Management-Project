using layout.repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layout.service
{
    public class DepartmentService
    {
        DepartmentRepository repo = new DepartmentRepository();
        public DataTable getAllDepartmentName()
        {
            return repo.findAllDepartmentName();
        }
        public string getIdByName(string name) {
            return repo.findByName(name);
        }
        public string fetchById(string id)
        {
            return repo.findById(id);
        }
    }
}
