using Microsoft.Extensions.Logging;

namespace OWASP_Desktop_App_Security_Top_10
{
    static class Program
    {
        private static readonly ILogger logger = LoggerFactory.Create(builder => { }).CreateLogger(nameof(Program));

        static void Main()
        {
            string user = "alice";
            logger.LogError("Login failed for {User", user);       // Noncompliant: Syntactically incorrect
            logger.LogError("Login failed for {}", user);          // Noncompliant: Empty placeholder
            logger.LogError("Login failed for {User-Name}", user); // Noncompliant: Only letters, numbers, and underscore are allowed for placeholders
        }
    }
}