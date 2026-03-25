using LaptopShopApplication.Repository;
using System;
using System.Data;
using System.Data.SqlClient;

namespace layout.repository
{
    public class PositionRepository
    {
        private readonly MssSQLConnection conn = new MssSQLConnection();

        public DataTable getAllPositionName()
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "select pos_id, pos_name from positions order by pos_name";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataTable data = new DataTable();
                adapter.Fill(data);
                return data;
            }
        }
        public float getBaseSalaryFromPositionId(int id)
        {
            float temp = -1;
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "Select base_salary from positions where pos_id = @id";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", id);
                object rs = cmd.ExecuteScalar();
                if (rs != null)
                {
                    temp = float.Parse(rs.ToString());
                }
            }
            return temp;
        }
    }
}
