using LaptopShopApplication.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace layout.repository
{
    public class DepartmentRepository
    {
        MssSQLConnection conn = new MssSQLConnection();
        public DataTable findAllDepartmentName()
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                string sql = "select ten_phongban from phongban ";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataTable data = new DataTable();
                adapter.Fill(data);
                return data;
            }
        }
        public string findByName(string name)
        {
            string departId = "";
            using (SqlConnection connection = conn.dbConnection())
            {
                string sql = "Select ma_phongban from phongban where ten_phongban = @name";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@name", name);
                object rs = cmd.ExecuteScalar();
                if (rs != null)
                {
                    departId = Convert.ToString(rs);
                }
            }
            if(departId.Equals(""))
            {
                return "None";
            }
            return departId;
        }
        public string findById(string id)
        {
            string departName = "";
            using (SqlConnection connection = conn.dbConnection())
            {
                string sql = "Select ten_phongban from phongban where ma_phongban = @id";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", id);
                object rs = cmd.ExecuteScalar();
                if (rs != null)
                {
                    departName = Convert.ToString(rs);
                }
            }
            if (departName.Equals(""))
            {
                return "None";
            }
            return departName;
        }
    }
}
