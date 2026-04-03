using layout.domain;
using layout.repository;
using System;
using System.Collections.Generic;

namespace layout.service
{
    public class SuperDepartmentService
    {
        private readonly SuperDepartmentRepository repository = new SuperDepartmentRepository();

        public List<Department> getAllDepartments()
        {
            return repository.getAllDepartments();
        }

        public bool addDepartment(Department dept)
        {
            if (string.IsNullOrEmpty(dept.department_id) || string.IsNullOrEmpty(dept.department_name))
            {
                return false;
            }
            return repository.insertDepartment(dept);
        }

        public bool updateDepartment(Department dept)
        {
            return repository.updateDepartment(dept);
        }

        public bool deleteDepartment(string deptId)
        {
            return repository.deleteDepartment(deptId);
        }
    }
}