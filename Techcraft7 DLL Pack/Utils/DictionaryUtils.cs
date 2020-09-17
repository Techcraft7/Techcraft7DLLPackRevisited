using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techcraft7_DLL_Pack.Utils
{
	public static class DictionaryUtils
	{
		public static void ThrowIfAnyNull<K, V>(Dictionary<K, V> dict, string Name = null)
		{
			if (dict.Any(kv => kv.Value == null))
			{
				if (Name != null)
				{
					throw new ArgumentException($"The dictionary \"{Name}\" had a null value!");
				}
				throw new ArgumentException($"Dictionary had a null value!");
			}
		}
	}
}
