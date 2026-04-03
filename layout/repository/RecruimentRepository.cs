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
    public class RecruimentRepository
    {
        MssSQLConnection conn = new MssSQLConnection();
        public void createRecruitment(Recruitment recruitment)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "Insert into recruitments (departmentId, position, " +
                    "estimateIncome, condition, sub_deadline, quantity, status, description)  " +
                    "values (@department, @position, @income, @condition, @deadline, @quantity, @status, @desc)";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@department", recruitment.departmentId);
                cmd.Parameters.AddWithValue("@position", recruitment.position);
                cmd.Parameters.AddWithValue("@income", recruitment.estimateIncome);
                cmd.Parameters.AddWithValue("@condition", recruitment.condition);
                cmd.Parameters.AddWithValue("@deadline", recruitment.subDeadline);
                cmd.Parameters.AddWithValue("@quantity", recruitment.quantity);
                cmd.Parameters.AddWithValue("@status", recruitment.status);
                cmd.Parameters.AddWithValue("@desc", recruitment.description);
                cmd.ExecuteNonQuery();
            }
        }
        public DataTable getAllRecruitment()
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "select id, departmentId, position, sub_deadline, quantity, status from recruitments";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataTable data = new DataTable();
                adapter.Fill(data);
                return data;
            }
        }
        public DataTable findById(int id)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "select * from recruitments where id = @id";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@id", id);
                DataTable data = new DataTable();
                adapter.Fill(data);
                return data;
            }
        }
        public void deleteRecruitment(int id)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                SqlTransaction transaction = connection.BeginTransaction();
                try
                {
                    string deleteDetailSql = "Delete from recruitment_details where recruit_id = @id";
                    SqlCommand deleteDetailCmd = new SqlCommand(deleteDetailSql, connection, transaction);
                    deleteDetailCmd.Parameters.AddWithValue("@id", id);
                    deleteDetailCmd.ExecuteNonQuery();

                    string deleteRecruitmentSql = "Delete from recruitments where id = @id";
                    SqlCommand deleteRecruitmentCmd = new SqlCommand(deleteRecruitmentSql, connection, transaction);
                    deleteRecruitmentCmd.Parameters.AddWithValue("@id", id);
                    deleteRecruitmentCmd.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
        public void updateRecruitment(Recruitment recruitment)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "update recruitments set departmentId = @department, position = @position, estimateIncome = @income," +
                    "condition = @condition, sub_deadline = @deadline, quantity = @quantity, status = @status, description  = @desc " +
                    "where id = @id";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@department", recruitment.departmentId);
                cmd.Parameters.AddWithValue("@position", recruitment.position);
                cmd.Parameters.AddWithValue("@income", recruitment.estimateIncome);
                cmd.Parameters.AddWithValue("@condition", recruitment.condition);
                cmd.Parameters.AddWithValue("@deadline", recruitment.subDeadline);
                cmd.Parameters.AddWithValue("@quantity", recruitment.quantity);
                cmd.Parameters.AddWithValue("@status", recruitment.status);
                cmd.Parameters.AddWithValue("@desc", recruitment.description);
                cmd.Parameters.AddWithValue("@id", recruitment.id);
                cmd.ExecuteNonQuery();
            }
        }
        public string getPositionByRecruitmentId(int id)
        {
            string position = "";
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "Select position from recruitments where id = @id";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", id);
                object rs = cmd.ExecuteScalar();
                if (rs != null)
                {
                    position = Convert.ToString(rs);
                }
            }   
            if (position.Equals(""))
            {
                return "None";
            }
            return position;
        }
        public DataTable getAllJobs()
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "select r.id, d.department_name as ten_phongban, r.position, r.estimateIncome from recruitments r join departments d on r.departmentId = d.department_id";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataTable data = new DataTable();
                adapter.Fill(data);
                return data;
            }
        }
    }
}
