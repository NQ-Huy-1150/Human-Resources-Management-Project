using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaptopShopApplication.Repository
{
    public class MssSQLConnection
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
                                         "Database=HumanResourceManagement;" +
                                         "Integrated Security=true;" +
                                         "TrustServerCertificate=true;";

                return new SqlConnection(connectionString);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return connection;
        }
    }
}
