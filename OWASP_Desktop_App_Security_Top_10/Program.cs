using System.Security.Cryptography;

namespace OWASP_Desktop_App_Security_Top_10
{
    static class Program
    {
        static void Main()
        {
            var md5 = new MD5CryptoServiceProvider();
            // ruleid: insecure-crypto-hash
            var hashValue = md5.ComputeHash(new byte[] { });
        }
    }
}
