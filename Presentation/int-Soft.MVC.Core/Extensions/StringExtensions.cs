using System;

namespace intSoft.MVC.Core.Extensions
{
    public static class StringExtensions
    {
        public static string RemoveSubString(this string originalString, string stringToRemove)
        {
            var index = originalString.IndexOf(stringToRemove, StringComparison.Ordinal);
            var cleanedString = (index < 0) ? originalString : originalString.Remove(index, stringToRemove.Length);
            return cleanedString;
        }

        public static string ToAngularExpression(this string originalString)
        {
            return string.Format("{{{{{0}}}}}", originalString);
        }

        public static string ToAngularPattern(this string pattern)
        {
            return string.Format("/{0}/", pattern);
        }

        public static string ToCamelCase(this string originalString)
        {
            var firstChar = originalString[0].ToString().ToLower();
            return originalString.Remove(0, 1).Insert(0, firstChar);
        }
    }
}
