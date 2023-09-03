using System.Text.RegularExpressions;

namespace my_Ecommerce_App.Utils
{
    public static class UserUtiles
    {
        public static bool IfEmail(string? str)
        {
            if (str is null)
                return false;
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            return Regex.IsMatch(str, pattern);
        }
    }
}