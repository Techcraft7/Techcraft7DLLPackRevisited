using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techcraft7_DLL_Pack.Utils
{
    public static class StringUtils
    {
        public const string Alphabet = "abcdefghijklmnopqrstuvwxyz";

        public static bool IsOnlyAlpha(string s)
        {
            foreach (char c in s.ToLower())
            {
                if (!Alphabet.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }

		public static string StripWhiteSpaceAtStart(string v) => new string(v.SkipWhile(c => string.IsNullOrWhiteSpace(c.ToString())).ToArray());
		public static string StripWhiteSpaceAtEnd(string v) => new string(v.Reverse().SkipWhile(c => string.IsNullOrWhiteSpace(c.ToString())).Reverse().ToArray());
	}
}
