namespace FinalProject.Utilities
{
    public class PasswordUtil
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }

        public static bool IsPasswordSecure(string password)
        {
            bool hasUpperCase = password.Any(char.IsUpper);
            bool hasNumber = password.Any(char.IsDigit);
            
            return hasUpperCase && hasNumber;
        }
    }
}
