using System.Text.RegularExpressions;

namespace Stolzenberg.Extensions 
{
    public static class StringExtension 
    {
        public static string RemoveSpecialCharacters(this string str)
        {
            return Regex.Replace(str, "[^a-zA-Z0-9_. ]+", "", RegexOptions.Compiled);
        }

        public static bool CheckHTML(this string str) 
        {
            Regex urlRegex = new Regex(@"^(ht|f)tp(s?)\:\/\/[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&%\$#_]*)?$");
            return urlRegex.IsMatch(str);
        }
    }
}