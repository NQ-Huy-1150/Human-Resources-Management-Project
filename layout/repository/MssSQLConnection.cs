using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopShopApplication.Repository
{
    internal class MssSQLConnection
    {
        public SqlConnection dbConnection()
        {
            SqlConnection connection = null;
            Console.WriteLine("Connecting to Sql Server.....");
            try
            {
                // sửa lại tên phiên local database nếu xung đột
                // : vd SQLEXPRESS thay cho SQLEXPRESS02
                string connectionString = "Server=localhost\\SQLEXPRESS02;" +
                                         "Database=QuanLyNhanSu;" +
                                         "Integrated Security=true;" +
                                         "TrustServerCertificate=true;";

                connection = new SqlConnection(connectionString);
                connection.Open();
                Console.WriteLine("Connected to SQL Server successfully!");

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return connection;
        }
    }
}
