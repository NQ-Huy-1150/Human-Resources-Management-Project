using LaptopShopApplication.Repository;
using layout.domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace layout.repository
{
    public class RecruitmentDetailRepository
    {
        MssSQLConnection conn = new MssSQLConnection();
        public void createRecruitmentDetail(Candidate candidate)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                string sql = "Insert into recruitment_detail (recruit_id,full_name,email,address,phone_number,edu_level,year_of_exp,recruit_status)" +
                    "values (@id,@name,@email,@address,@phone,@edu,@year,@status)";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", candidate.recruitId);
                cmd.Parameters.AddWithValue("@name", candidate.fullName);
                cmd.Parameters.AddWithValue("@email", candidate.email);
                cmd.Parameters.AddWithValue("@address", candidate.address);
                cmd.Parameters.AddWithValue("@phone", candidate.phone);
                cmd.Parameters.AddWithValue("@edu", candidate.edu_level);
                cmd.Parameters.AddWithValue("@year", candidate.yearOfExp);
                cmd.Parameters.AddWithValue("@status", candidate.status);
                cmd.ExecuteNonQuery();
            }
        }
        public DataTable getAllRecruitmentDetail(int id)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                string sql = $"select id, full_name, email, recruit_status from recruitment_detail where id = {id}";
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
                string sql = $"select * from recruitment_detail where id = {id}";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataTable data = new DataTable();
                adapter.Fill(data);
                return data;
            }
        }
        public void deleteRecruitmentDetail(int id)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                string sql = "Delete from recruitment_detail where id = @id";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
        public void updateRecruitmentDetail(Candidate candidate)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                string sql = "update recruitment_detail set recruit_id = @recruitId, full_name = @name, email = @email," +
                    "address = @address, phone_number = @phone, edu_level = @edu, year_of_exp = @year" +
                    "where id = @id";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@recruitId", candidate.recruitId);
                cmd.Parameters.AddWithValue("@name", candidate.fullName);
                cmd.Parameters.AddWithValue("@email", candidate.email);
                cmd.Parameters.AddWithValue("@address", candidate.address);
                cmd.Parameters.AddWithValue("@phone", candidate.phone);
                cmd.Parameters.AddWithValue("@edu", candidate.edu_level);
                cmd.Parameters.AddWithValue("@year", candidate.yearOfExp);
                cmd.Parameters.AddWithValue("@id", candidate.id);
                cmd.ExecuteNonQuery();
            }
        }
        public void updateRecruitmentDetailStatus(int id, string status)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                string sql = "update recruitment_detail set recruit_status = @status where id = @id";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
