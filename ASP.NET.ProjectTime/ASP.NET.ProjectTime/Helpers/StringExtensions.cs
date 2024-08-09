using System;

namespace ASP.NET.ProjectTime.Helpers
{
    public static class StringExtensions
    {
        public static string FirstCharToUpper(this string input)
        {
            if (input == null)
            {
                throw new ArgumentNullException(nameof(input));
            }
            else if (input == "")
            {
                throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
            }
            else
            {
                return input[0].ToString().ToUpper() + input.Substring(1);
            }
        }
    }
}