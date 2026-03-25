using layout.repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layout.service
{
    public class RoleService
    {
        RoleRepository repo = new RoleRepository();

        public DataTable getAllRoles()
        {
            return repo.findAllRoles();
        }

        public int getRoleId(string name)
        {
            return repo.findByName(name);
        }
        public string getRoleName(int id)
        {
            return repo.findById(id);
        } 
    }
}
