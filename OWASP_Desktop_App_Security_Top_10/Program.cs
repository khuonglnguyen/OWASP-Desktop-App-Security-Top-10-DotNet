using System.Security.Cryptography;

namespace OWASP_Desktop_App_Security_Top_10
{
    static class Program
    {
        static void Main()
        {
            var hashProvider4 = HashAlgorithm.Create("MD5");
        }        
    }
}
