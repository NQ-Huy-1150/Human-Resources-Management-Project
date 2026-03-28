using LaptopShopApplication.Repository;
using layout.domain; 
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace layout.repository
{
    public class SuperDepartmentRepository
    {
        private readonly MssSQLConnection conn = new MssSQLConnection();

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

        public bool insertDepartment(Department dept)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                string sql = "INSERT INTO departments (department_id, department_name) VALUES (@id, @name)";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = dept.department_id;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = dept.department_name;

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool updateDepartment(Department dept)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                string sql = "UPDATE departments SET department_name = @name WHERE department_id = @id";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = dept.department_id;
                    cmd.Parameters.Add("@name", SqlDbType.NVarChar).Value = dept.department_name;

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool deleteDepartment(string deptId)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                string sql = "DELETE FROM departments WHERE department_id = @id";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    cmd.Parameters.Add("@id", SqlDbType.VarChar).Value = deptId;

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }
    }
}