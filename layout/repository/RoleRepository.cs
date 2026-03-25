using LaptopShopApplication.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layout.repository
{
    public class RoleRepository
    {
        MssSQLConnection conn = new MssSQLConnection();

        public DataTable findAllRoles()
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                string sql = "select role_id, role_name from roles order by role_id";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataTable data = new DataTable();
                adapter.Fill(data);
                return data;
            }
        }

        public int findByName(string name)
        {
            int id = 0;
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "Select role_id from roles where role_name = @name";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@name",name);
                object rs = cmd.ExecuteScalar();
                if (rs != null)
                {
                    id = Convert.ToInt32(rs);
                }
            }
            return id;
        }
        public string findById(int id)
        {
            string name = "";
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "Select role_name from roles where role_id = @id";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", id);
                object rs = cmd.ExecuteScalar();
                if (rs != null)
                {
                    name = Convert.ToString(rs);
                }
            }
            return name;
        }
    }
}
