using System.Text.RegularExpressions;

namespace Stolzenberg.Extensions 
{
    public static class StringExtension 
    {
        public static string RemoveSpecialCharacters(this string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_. ]+", "", RegexOptions.Compiled);
        }
    }
}