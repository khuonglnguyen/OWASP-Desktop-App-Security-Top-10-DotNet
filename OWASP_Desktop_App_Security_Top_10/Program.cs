using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.Data.Sqlite;
using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;

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

            var config = new Config();
            // Use exact lowercase method name to match SCS0015 pattern
            config.setPassword("NotSoSecr3tP@ssword");

            // Add a common API usage with a hardcoded password that Security Code Scan should detect
            var cred = new System.Net.NetworkCredential("admin", "NotSoSecr3tP@ssword");
            Console.WriteLine($"Created credential for user: {cred.UserName}");
        }



        public static void JwtTest1()
        {
            var payload = new Dictionary<string, object>
        {
            { "claim1", 0 },
            { "claim2", "claim2-value" }
        };

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            const string key = "razdvatri";

            // ruleid: jwt-hardcoded-secret
            var token = encoder.Encode(payload, key);
            Console.WriteLine(token);
        }
    }

    // Small helper class to demonstrate password assignment for analyzer detection
    class Config
    {
        public string Password { get; set; }

        // lowercase method name to match analyzer pattern
        public void setPassword(string password)
        {
            Password = password;
            // Simulate storing the password in configuration
            Console.WriteLine("Config password set.");
        }
    }
}