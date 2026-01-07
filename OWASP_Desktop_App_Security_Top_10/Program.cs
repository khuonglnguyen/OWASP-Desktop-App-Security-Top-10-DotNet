using JWT.Algorithms;
using JWT.Builder;

namespace OWASP_Desktop_App_Security_Top_10
{
    static class Program
    {
        static void Main()
        {
            var secret = "secret123";
            var builder = JwtBuilder.Create();
            builder.WithAlgorithm(new HMACSHA256Algorithm());
            // ruleid: jwt-hardcoded-secret
            builder.WithSecret(secret);
            builder.AddClaim("exp", DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds());
            builder.AddClaim("claim1", 0);
            builder.AddClaim("claim2", "claim2-value");

            var token = builder.Encode();

            Console.WriteLine(token);
        }
    }
}
