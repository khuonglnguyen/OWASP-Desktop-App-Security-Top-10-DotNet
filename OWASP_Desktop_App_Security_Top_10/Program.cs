using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Data.Sqlite;

namespace OWASP_Desktop_App_Security_Top_10
{
    static class Program
    {
        static void Main(string[] args)
        {
            // Hardcoded password - Security Hotspot
            string password = "admin123";
            Console.Write("Enter password: ");
            string inputPassword = Console.ReadLine() ?? "";
            if (inputPassword == password)
            {
                Console.WriteLine("Login successful");
            }

            // SQL Injection vulnerability
            Console.Write("Enter user name (e.g., '; DROP TABLE users; --): ");
            string userInput = Console.ReadLine() ?? "'; DROP TABLE users; --";
            string query = $"SELECT * FROM users WHERE name = '{userInput}'";
            using (var connection = new SqliteConnection("Data Source=:memory:"))
            {
                connection.Open();
                // Create table first
                var createCommand = connection.CreateCommand();
                createCommand.CommandText = "CREATE TABLE users (id INTEGER, name TEXT)";
                createCommand.ExecuteNonQuery();

                var command = connection.CreateCommand();
                command.CommandText = query;
                command.ExecuteNonQuery();
            }

            // Command Injection
            Console.Write("Enter command (e.g., calc.exe; shutdown /s): ");
            string commandInput = Console.ReadLine() ?? "calc.exe";
            Process.Start("cmd.exe", "/c " + commandInput);

            // Insecure random number generation
            Random random = new Random();
            int insecureNumber = random.Next();
            Console.WriteLine($"Random number: {insecureNumber}");

            // Path Traversal
            Console.Write("Enter file path (e.g., ../../../etc/passwd): ");
            string filePath = Console.ReadLine() ?? "../../../etc/passwd";
            if (File.Exists(filePath))
            {
                string content = File.ReadAllText(filePath);
                Console.WriteLine(content);
            }

            // Weak hash function
            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes("password"));
                Console.WriteLine($"MD5 hash: {Convert.ToHexString(hash)}");
            }

            // Hardcoded API key
            string apiKey = "sk-1234567890abcdef";
            Console.WriteLine($"Using API key: {apiKey}");

            Console.WriteLine("Security vulnerabilities added for SonarQube scanning.");
        }
    }
}