using layout.domain;
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

        public List<Department> getAllDepartments()
        {
            return repo.getAllDepartments();
        }

        public bool addDepartment(Department department)
        {
            if (department == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(department.department_id) || string.IsNullOrWhiteSpace(department.department_name))
            {
                return false;
            }

            return repo.addDepartment(department);
        }

        public bool updateDepartment(Department department)
        {
            if (department == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(department.department_id) || string.IsNullOrWhiteSpace(department.department_name))
            {
                return false;
            }

            return repo.updateDepartment(department);
        }

        public bool deleteDepartment(string departmentId)
        {
            if (string.IsNullOrWhiteSpace(departmentId))
            {
                return false;
            }

            return repo.deleteDepartment(departmentId);
        }

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
