using LaptopShopApplication.Repository;
using layout.domain;
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

        public List<Department> getAllDepartments()
        {
            List<Department> list = new List<Department>();

            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "SELECT department_id, department_name FROM departments ORDER BY department_id ASC";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Department
                        {
                            department_id = reader["department_id"].ToString(),
                            department_name = reader["department_name"].ToString()
                        });
                    }
                }
            }

            return list;
        }

        public bool addDepartment(Department department)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "INSERT INTO departments (department_id, department_name) VALUES (@id, @name)";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = department.department_id;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = department.department_name;
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool updateDepartment(Department department)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "UPDATE departments SET department_name = @name WHERE department_id = @id";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = department.department_id;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = department.department_name;
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool deleteDepartment(string departmentId)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "DELETE FROM departments WHERE department_id = @id";

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = departmentId;
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

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
