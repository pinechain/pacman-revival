using System.Text.RegularExpressions;

using UnityEngine;

namespace Bladengine.Utils
{
    public sealed class StringUtils
    {
        private StringUtils() {}

        public static string humanizeCamelCase(string camelCaseWord)
        {
            string output = "";
            foreach (Match match in new Regex("([A-Z][a-z]+)").Matches(camelCaseWord))
            {
                output += output.Length > 0 ? match.ToString().ToLower() + " " : match + " ";
            }
            return output.TrimEnd(' ');
        }
    }
}
