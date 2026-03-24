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
                string sql = "select department_name AS ten_phongban from departments";
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
                connection.Open();
                string sql = "Select department_id from departments where department_name = @name";

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
                connection.Open();
                string sql = "Select department_name from departments where department_id = @id";

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
