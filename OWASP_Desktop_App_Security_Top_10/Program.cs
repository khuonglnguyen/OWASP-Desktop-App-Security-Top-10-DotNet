using System;
using Microsoft.Data.Sqlite; // Thay thế thư viện SQLite

namespace OWASP_Desktop_App_Security_Top_10
{
    static class Program
    {
        static void Main()
        {
            // Tạo cơ sở dữ liệu SQLite trong bộ nhớ
            using (var connection = new SqliteConnection("Data Source=:memory:"))
            {
                connection.Open();

                // Tạo bảng users
                string createTableQuery = @"CREATE TABLE users (
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    user TEXT NOT NULL,
                    pass TEXT NOT NULL
                );";
                using (var createTableCmd = connection.CreateCommand())
                {
                    createTableCmd.CommandText = createTableQuery;
                    createTableCmd.ExecuteNonQuery();
                }

                // Thêm dữ liệu mẫu
                string insertDataQuery = @"INSERT INTO users (user, pass) VALUES
                    ('admin', 'password123'),
                    ('user1', 'pass1'),
                    ('user2', 'pass2');";
                using (var insertDataCmd = connection.CreateCommand())
                {
                    insertDataCmd.CommandText = insertDataQuery;
                    insertDataCmd.ExecuteNonQuery();
                }

                // Nhập thông tin từ người dùng
                Console.Write("Enter username: ");
                var user = Console.ReadLine();
                Console.Write("Enter password: ");
                var pass = Console.ReadLine();

                // Truy vấn dữ liệu (nối chuỗi trực tiếp - không an toàn)
                string query = "SELECT * FROM users WHERE user = '" + user + "' AND pass = '" + pass + "'";
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = query;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            Console.WriteLine("Login successful!");
                        }
                        else
                        {
                            Console.WriteLine("Invalid username or password.");
                        }
                    }
                }
            }
        }
    }
}
