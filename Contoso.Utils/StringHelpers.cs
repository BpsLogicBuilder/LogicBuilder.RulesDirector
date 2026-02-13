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
            catch (System.FormatException)
            {
                // If a format-related exception is thrown, the email is not valid
                return false;
            }
        }
    }
}
