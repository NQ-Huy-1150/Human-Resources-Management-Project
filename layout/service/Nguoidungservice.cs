using layout.domain;
using layout.repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace layout.service
{
    
    internal class Nguoidungservice
    {
        private NguoidungRepository repository = new NguoidungRepository();
        //Thêm người dùng
        public bool themnguoidung(NguoiDung obj, out string message)
        {
            return repository.addNguoidung1(obj, out message);
        }
        //Lấy tất cả người dùng
        public DataTable getAllNguoidung()
        {
            return repository.getAllRecruitment();
        }

        public bool capnhatNguoidung(DataRowView row)
        {
            return repository.updateNguoidung(row);
        }

        //Xóa người dùng
        public bool XoaNguoiDung(int id)
        {
            return repository.deleteNguoidung(id);
        }
        public bool verifyUser(string email, string pass)
        {
            return repository.isEmailAndPasswordExisted(email,pass);
        }

        public DataTable getUserRoleAndIdByEmail(string email)
        {
            return repository.getUserByEmail(email);
        }
        public int getUserIdByName(string name)
        {
            return repository.getUserIdFromName(name);
        }

        public string getUserNameById(int userId)
        {
            return repository.getUserNameFromId(userId);
        }
        public int getRoleId(int userId)
        {
            return repository.getUserRoleId(userId);
        }
        public int getUserIdFromEmail(string email)
        {
            return repository.getUserIdFromEmail(email);
        }
        public int getPositionId(int userId)
        {
            return repository.getUserPositionId(userId);
        } 
    }
}
