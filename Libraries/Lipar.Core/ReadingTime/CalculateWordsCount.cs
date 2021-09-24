﻿using System;
using System.Text.RegularExpressions;

namespace Lipar.Core.ReadingTime
{
    public static class CalculateWordsCount
    {
        private static readonly Regex _matchAllTags =
            new Regex(@"<(.|\n)*?>", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static int WordsCount(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return 0;
            }

            text = text.CleanTags().Trim();
            text = text.Replace("\t", " ");
            text = text.Replace("\n", " ");
            text = text.Replace("\r", " ");

            var words = text.Split(
                new[] { ' ', ',', ';', '.', '!', '"', '(', ')', '?', ':', '\'', '«', '»', '+', '-' },
                StringSplitOptions.RemoveEmptyEntries);
            return words.Length;
        }

        private static string CleanTags(this string data)
        {
            return data.Replace("\n", "\n ").RemoveHtmlTags();
        }

        private static string RemoveHtmlTags(this string text)
        {
            return string.IsNullOrEmpty(text) ?
                        string.Empty :
                        _matchAllTags.Replace(text, " ").Replace("&nbsp;", " ");
        }
        public static string CleanSeoUrl(string data)
        {
            data = CleanTags(data).Trim();
            data = Regex.Replace(data, @"[!*'();:@&=+$,/?%#]", "-"); // Remove all non valid chars          
            data = Regex.Replace(data, @"\s+", " ").Trim(); // convert multiple spaces into one space  
            data = Regex.Replace(data, @"\s", "-"); // //Replace spaces by dashes
            return data;
        }
    }
}
