using System.Security.Cryptography;

namespace OWASP_Desktop_App_Security_Top_10
{
    static class Program
    {
        static void Main()
        {

        }
        public static void EncryptWithAesCbc()
        {
            Aes key = Aes.Create();
            // ruleid: cbc-mode
            key.Mode = CipherMode.CBC;
            using var encryptor = key.CreateEncryptor();
            byte[] msg = new byte[32];
            var cipherText = encryptor.TransformFinalBlock(msg, 0, msg.Length);
        }
    }
}
