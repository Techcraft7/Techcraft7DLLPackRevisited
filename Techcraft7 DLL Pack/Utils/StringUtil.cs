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

        public static bool IsOnlyAlpha(string s, bool CaseSensitive = false)
        {
            foreach (char c in CaseSensitive ? s : s.ToLower())
            {
                if (!Alphabet.Contains(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
