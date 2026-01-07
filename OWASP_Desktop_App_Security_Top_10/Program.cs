namespace OWASP_Desktop_App_Security_Top_10
{
    static class Program
    {
        // Demo for DA2 - Broken Authentication & Session Management
        // Vulnerabilities demonstrated:
        // 1. Plain text password storage
        // 2. Predictable session IDs
        // 3. No session expiration
        // 4. No proper logout mechanism

        private static Dictionary<string, string> users = new Dictionary<string, string>
        {
            { "admin", "password123" },  // Plain text password - VULNERABLE
            { "user", "userpass" }
        };

        private static Dictionary<int, string> sessions = new Dictionary<int, string>();
        private static int sessionCounter = 1;  // Predictable session ID - VULNERABLE

        static void Main()
        {
            Console.WriteLine("OWASP Desktop App Security Top 10 - DA2 Demo");
            Console.WriteLine("Broken Authentication & Session Management");
            Console.WriteLine("==========================================");

            while (true)
            {
                Console.WriteLine("\n1. Login");
                Console.WriteLine("2. Access Protected Resource");
                Console.WriteLine("3. Logout");
                Console.WriteLine("4. Exit");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();
                if (choice == null) continue;

                switch (choice)
                {
                    case "1":
                        Login();
                        break;
                    case "2":
                        AccessResource();
                        break;
                    case "3":
                        Logout();
                        break;
                    case "4":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
            }
        }

        static void Login()
        {
            string? username = Console.ReadLine();
            Console.Write("Password: ");
            string? password = Console.ReadLine();

            if (username != null && password != null && users.ContainsKey(username) && users[username] == password)
            {
                int sessionId = sessionCounter++;  // Predictable session ID
                sessions[sessionId] = username;
                Console.WriteLine($"Login successful. Your session ID is: {sessionId}");
                // Note: Session ID is displayed - VULNERABLE
            }
            else
            {
                Console.WriteLine("Invalid credentials.");
            }
        }

        static void AccessResource()
        {
            Console.Write("Enter your session ID: ");
            if (int.TryParse(Console.ReadLine(), out int sessionId))
            {
                if (sessions.ContainsKey(sessionId))
                {
                    string username = sessions[sessionId];
                    Console.WriteLine($"Welcome {username}! You have accessed the protected resource.");
                    // No session expiration - VULNERABLE
                }
                else
                {
                    Console.WriteLine("Invalid session ID.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }

        static void Logout()
        {
            Console.Write("Enter your session ID to logout: ");
            if (int.TryParse(Console.ReadLine(), out int sessionId))
            {
                if (sessions.ContainsKey(sessionId))
                {
                    sessions.Remove(sessionId);
                    Console.WriteLine("Logged out successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid session ID.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }
    }
}
