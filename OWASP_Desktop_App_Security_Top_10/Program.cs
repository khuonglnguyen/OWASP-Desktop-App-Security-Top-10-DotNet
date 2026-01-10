namespace OWASP_Desktop_App_Security_Top_10
{
    static class Program
    {
        static void Main()
        {

        }
        public static void Login(string username, string password)
        {
            Console.WriteLine($"User login attempt: {username} with password: {password}");
        }
    }
}
