namespace OWASP_Desktop_App_Security_Top_10
{
    static class Program
    {
        static void Main()
        {

        }

        public class UserManager
        {
            public class User
            {
                public string Username { get; set; }
                public string Role { get; set; }
                public bool IsAuthenticated { get; set; }
            }

            private User currentUser;

            public UserManager()
            {
                currentUser = new User();
            }

            public void Login(string username, string password)
            {
                // Giả lập đăng nhập
                if (username == "user" && password == "pass")
                {
                    currentUser.Username = username;
                    currentUser.Role = "User";
                    currentUser.IsAuthenticated = true;
                }
                else if (username == "admin" && password == "admin123")
                {
                    currentUser.Username = username;
                    currentUser.Role = "Admin";
                    currentUser.IsAuthenticated = true;
                }
            }

            // LỖ HỔNG: Không kiểm tra authorization
            public void DeleteUser(string username)
            {
                // Chỉ kiểm tra authentication, KHÔNG kiểm tra authorization
                if (currentUser.IsAuthenticated)
                {
                    Console.WriteLine($"Deleting user: {username}");
                    // Code xóa user... 
                }
                else
                {
                    throw new UnauthorizedAccessException("User not authenticated");
                }
            }

            // LỖ HỔNG: Không kiểm tra quyền trước khi truy cập sensitive data
            public string GetSensitiveData()
            {
                // Truy cập trực tiếp mà không kiểm tra role
                return "Sensitive configuration data:  API_KEY=secret123";
            }

            // LỖ HỔNG: Cho phép thay đổi quyền mà không kiểm tra
            public void ChangeUserRole(string username, string newRole)
            {
                if (currentUser.IsAuthenticated)
                {
                    Console.WriteLine($"Changing {username} role to {newRole}");
                    // User thường có thể tự nâng quyền lên Admin! 
                }
            }

            // LỖ HỔNG: Không validate quyền truy cập file
            public void AccessSystemFile(string filePath)
            {
                if (currentUser.IsAuthenticated)
                {
                    // Cho phép đọc bất kỳ file nào mà không kiểm tra quyền
                    System.IO.File.ReadAllText(filePath);
                }
            }
        }

        // LỖ HỔNG:  Form admin có thể được truy cập trực tiếp
        public class AdminPanel
        {
            public AdminPanel()
            {
                // Không có kiểm tra authorization nào! 
            }

            private void InitializeComponent()
            {
                // UI components...
            }

            private void LoadAdminData()
            {
                // Load sensitive admin data
                Console.WriteLine("Loading admin data...");
            }

            // Bất kỳ ai cũng có thể gọi hàm này
            private void btnDeleteAllUsers_Click(object sender, EventArgs e)
            {
                // Xóa tất cả users mà không kiểm tra quyền
                Console.WriteLine("Deleting all users...");
            }
        }
    }
}
