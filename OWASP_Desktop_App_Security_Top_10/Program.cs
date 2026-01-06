using Microsoft.Data.Sqlite;

namespace OWASP_Desktop_App_Security_Top_10
{
    static class Program
    {
        static void Main()
        {
            RunSqlInjectionDemo();
        }

        static void RunSqlInjectionDemo()
        {
            // var normalInput = "Alice";
            // var maliciousInput = "' OR '1'='1";
            Console.Write("Enter username to search: ");
            var input = Console.ReadLine() ?? string.Empty;

            using var connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "CREATE TABLE Users (Id INTEGER PRIMARY KEY, username TEXT);";
                cmd.ExecuteNonQuery();

                cmd.CommandText = "INSERT INTO Users (username) VALUES ($u1), ($u2), ($u3);";
                cmd.Parameters.AddWithValue("$u1", "Alice");
                cmd.Parameters.AddWithValue("$u2", "Bob");
                cmd.Parameters.AddWithValue("$u3", "Eve");
                cmd.ExecuteNonQuery();
            }

            Console.WriteLine();
            Console.WriteLine("--- Demo SQL Injection (nối chuỗi) ---");
            ExecuteQuery(connection, input);
        }

        static void ExecuteQuery(SqliteConnection conn, string input)
        {
            Console.WriteLine("Input: " + QuoteForDisplay(input));
            using var cmd = conn.CreateCommand();
            
            cmd.CommandText = $"SELECT username FROM Users WHERE username = '{input}';";
            Console.WriteLine("Query: " + cmd.CommandText);

            var rows = new List<string>();
            try
            {
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    rows.Add(reader.GetString(0));
                }
                Console.WriteLine("Returned rows: " + (rows.Count == 0 ? "(none)" : string.Join(", ", rows)));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            Console.WriteLine();
        }

        static string QuoteForDisplay(string s) => s.Replace("\n", "\\n").Replace("\r", "\\r").Replace("\t", "\\t");
    }
}
