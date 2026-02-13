namespace Contoso.Utils
{
    public static class StringHelpers
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                return new System.Net.Mail.MailAddress(email).Address == email;
            }
            catch
            {
                // If an exception is thrown, the email is not valid
                // Intentionally catch all exceptions to return false for any invalid email format
                return false;
            }
        }
    }
}
