using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techcraft7_DLL_Pack.Utils
{
    public static class ListUtils
    {
		public static void ThrowIfAnyNull<T>(IEnumerable<T> list, string Name = null)
		{
			if (list.Any(v => v == null))
			{
				if (Name != null)
				{
					throw new ArgumentException($"The list \"{Name}\" had a null value!");
				}
				throw new ArgumentException($"List had a null value!");
			}
		}
	}
}
