using System.Net;

namespace OWASP_Desktop_App_Security_Top_10
{
    static class Program
    {
        static void Main()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
        }        
    }
}
