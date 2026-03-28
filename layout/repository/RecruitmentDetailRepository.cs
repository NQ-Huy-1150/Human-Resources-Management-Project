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
        public int createRecruitmentDetail(Candidate candidate)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "Insert into recruitment_details (recruit_id,full_name,email,address,phone_number,edu_level,year_of_exp,recruit_status, lookup_id)" +
                    "values (@id,@name,@email,@address,@phone,@edu,@year,@status, @lookup); SELECT CAST(SCOPE_IDENTITY() AS INT);";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", candidate.recruitId);
                cmd.Parameters.AddWithValue("@name", candidate.fullName);
                cmd.Parameters.AddWithValue("@email", candidate.email);
                cmd.Parameters.AddWithValue("@address", candidate.address);
                cmd.Parameters.AddWithValue("@phone", candidate.phone);
                cmd.Parameters.AddWithValue("@edu", candidate.edu_level);
                cmd.Parameters.AddWithValue("@year", candidate.yearOfExp);
                cmd.Parameters.AddWithValue("@status", candidate.status);
                cmd.Parameters.AddWithValue("@lookup", candidate.lookupId);
                object result = cmd.ExecuteScalar();
                return result != null ? Convert.ToInt32(result) : 0;
            }
        }
        public DataTable getAllRecruitmentDetailByRecruitId(int recruitId)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "select id, full_name, email, recruit_status from recruitment_details where recruit_id = @recruitId";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@recruitId", recruitId);
                DataTable data = new DataTable();
                adapter.Fill(data);
                return data;
            }
        }
        public int getTotalRecruitmentProfileCount()
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "select count(*) from recruitment_details";

                SqlCommand cmd = new SqlCommand(sql, connection);
                object rs = cmd.ExecuteScalar();
                if (rs != null)
                {
                    return Convert.ToInt32(rs);
                }
            }
            return 0;
        }
        public DataTable findById(int id)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "select * from recruitment_details where id = @id";
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@id", id);
                DataTable data = new DataTable();
                adapter.Fill(data);
                return data;
            }
        }
        public void deleteRecruitmentDetail(int id)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "Delete from recruitment_details where id = @id";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
        public void updateRecruitmentDetail(Candidate candidate)
        {
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "update recruitment_details set recruit_id = @recruitId, full_name = @name, email = @email," +
                    "address = @address, phone_number = @phone, edu_level = @edu, year_of_exp = @year" +
                    " where id = @id";

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
                connection.Open();
                string sql = "update recruitment_details set recruit_status = @status where id = @id";
                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@status", status);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
            }
        }
        public string getStatusFromLookUpId(string id)
        {
            string temp = "";
            using (SqlConnection connection = conn.dbConnection())
            {
                connection.Open();
                string sql = "Select recruit_status from recruitment_details where lookup_id = @id";

                SqlCommand cmd = new SqlCommand(sql, connection);
                cmd.Parameters.AddWithValue("@id", id);
                object rs = cmd.ExecuteScalar();
                if (rs != null)
                {
                    temp = Convert.ToString(rs);
                }
            }
            return temp;
        }
    }
}
