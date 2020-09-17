using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Techcraft7_DLL_Pack.Linq
{
	public static class T7LinqExtentions
	{
		public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> ie, int n) => ie.Reverse().Skip(n).Reverse();
		public static IEnumerable<T> TakeLast<T>(this IEnumerable<T> ie, int n) => ie.Reverse().Take(n).Reverse();
		public static Dictionary<K, V> ToDictionary<K, V>(this IEnumerable<KeyValuePair<K, V>> ie) => ie.ToDictionary(kv => kv.Key, kv => kv.Value);
		public static string Join<T>(this IEnumerable<T> list, string Separator) => string.Join(Separator, list.ToArray());
		public static void Foreach<T>(this IEnumerable<T> ie, Action<T> func) => ie.ToList().ForEach(func);
	}
}
