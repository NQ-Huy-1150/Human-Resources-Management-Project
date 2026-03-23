using layout.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layout.service
{
    public class RoleService
    {
        RoleRepository repo = new RoleRepository();
        public int getRoleId(string name)
        {
            return repo.findByName(name);
        }
    }
}
