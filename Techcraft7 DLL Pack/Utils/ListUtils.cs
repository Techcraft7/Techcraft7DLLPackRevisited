using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techcraft7_DLL_Pack.Utils
{
    public static class ListUtils
    {
        public static string Join<T>(IEnumerable<T> list, string Separator) => string.Join(Separator, list.ToArray());
    }
}
