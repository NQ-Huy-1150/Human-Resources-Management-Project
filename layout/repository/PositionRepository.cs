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
        public string getPositionNameFromPositionId(int id)
        {
            string temp = "";

            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();

                string sql = "Select pos_name from positions where pos_id = @id";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", id);

                object rs = cmd.ExecuteScalar();

                if (rs != null)
                {
                    temp = rs.ToString();
                }
            }
            return temp;
        }
        // Thêm vào trong class PositionRepository
        public DataTable getAllPosition()
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                // Lấy tất cả các cột để hiển thị lên DataGrid
                string sql = "SELECT pos_id, pos_name, base_salary FROM positions ORDER BY pos_id DESC";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataTable data = new DataTable();
                adapter.Fill(data);
                return data;
            }
        }

        public bool insertPosition(string name, float salary)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "INSERT INTO positions (pos_name, base_salary) VALUES (@name, @salary)";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@salary", salary);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool updatePosition(int id, string name, float salary)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "UPDATE positions SET pos_name = @name, base_salary = @salary WHERE pos_id = @id";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@salary", salary);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool deletePosition(int id)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "DELETE FROM positions WHERE pos_id = @id";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
