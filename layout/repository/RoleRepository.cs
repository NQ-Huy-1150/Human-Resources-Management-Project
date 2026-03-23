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
        public int findByName(string name)
        {
            int id = 0;
            using (SqlConnection connection = conn.dbConnection())
            {
                string sql = "Select ma_vaitro from vaitro where ten_vaitro = @name";

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
    }
}
