using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RyanGrandeGifts.Helpers
{
    public static class StringFormatHelper
    {
        public static string CaptialiseFirst(string input)
        {
            string first = input.Substring(0,1).ToUpper();
            string rest = input.Substring(1).ToLower();
            return first + rest;
        }
        public static string ToTitleCase(this string str)
        {
            var tokens = str.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            for (var i = 0; i < tokens.Length; i++)
            {
                var token = tokens[i];
                tokens[i] = token.Substring(0, 1).ToUpper() + token.Substring(1).ToLower();
            }

            return string.Join("", tokens);
        }
    }
}
