using System.Security.AccessControl;

namespace OWASP_Desktop_App_Security_Top_10
{
    static class Program
    {
        static void Main()
        {
            var accessRule = new FileSystemAccessRule("Everyone", FileSystemRights.Write, AccessControlType.Allow);
            var fileInfo = new FileInfo("path");
            var fileSecurity = fileInfo.GetAccessControl();

            fileSecurity.SetAccessRule(accessRule); // Sensitive
            fileInfo.SetAccessControl(fileSecurity);
        }        
    }
}
