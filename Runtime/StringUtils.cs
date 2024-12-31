
using System;
using System.Text;

namespace NeedrunGameUtils
{
    public class StringUtils
    {
        public static bool IsEmpty(string str)
        {
            return str == null || str.Length == 0;
        }

        public static bool IsNonEmpty(string str)
        {
            return !IsEmpty(str);
        }

        public static bool IsBlank(string str)
        {
            if (IsEmpty(str))
            {
                return true;
            }
            return str.Trim().Length == 0;
        }

        public static bool IsNonBlank(string str)
        {
            return !IsBlank(str);
        }

        public static string HidePartially(string input, int visibleChars)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            StringBuilder filteredStringBuilder = new StringBuilder();
            filteredStringBuilder.Append(input.Substring(0, Math.Min(visibleChars, input.Length)));
            for (int i = visibleChars; i < input.Length; i++)
            {
                filteredStringBuilder.Append('*');
            }
            return filteredStringBuilder.ToString();
        }
    }
}