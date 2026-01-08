using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace OWASP_Desktop_App_Security_Top_10
{
    static class Program
    {
        static void Main()
        {

        }

        public static string ConnectionString { get; set; } = "Server=10.0.0.101;Database=CustomerData";

        public static SqlConnection ConnectToDatabase(HttpRequest request)
        {
            string connectionString = string.Format("{0};User ID={1};Password={2}",
                ConnectionString,
                request.Form["username"],
                request.Form["password"]);

            SqlConnection connection = new SqlConnection();
            connection.ConnectionString = connectionString; // Noncompliant
            connection.Open();
            return connection;
        }
    }
}
