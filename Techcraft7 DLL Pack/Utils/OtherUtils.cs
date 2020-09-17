using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techcraft7_DLL_Pack.Utils
{
	public static class OtherUtils
	{
		public static void InLineSwitch<T>(T value, Dictionary<T[], Action> cases)
		{
			if (cases.Any(kv => kv.Value == null))
			{
				throw new ArgumentNullException($"{nameof(cases)} has a null value");
			}
			//Add all cases to a list,
			//remove all duplicates,
			//if the size before we removed
			//duplicates and the size after
			//are not the same,
			//then there were repeated cases
			List<T> used = new List<T>();
			cases.Select(kv => kv.Key).ToList().ForEach(l => l.ToList().ForEach(v => used.Add(v)));
			int before = used.Count;
			used = used.Distinct().ToList();
			if (used.Count != before)
			{
				throw new ArgumentException("Repeated cases detected!");
			}
			foreach (KeyValuePair<T[], Action> kv in cases)
			{
				if (kv.Key.Any(v => value.Equals(v)))
				{
					kv.Value.Invoke();
				}
			}
		}

		public static T IgnoreException<T>(Func<T> func) where T : class
		{
			func = func ?? throw new ArgumentNullException(nameof(func));
			try
			{
				return func.Invoke();
			}
			catch
			{
				return null;
			}
		}

		public static T? IgnoreException<T>(Func<T?> func) where T : struct
		{
			func = func ?? throw new ArgumentNullException(nameof(func));
			try
			{
				return func.Invoke();
			}
			catch
			{
				return null;
			}
		}

		public static void IgnoreException(Action a)
		{
			a = a ?? throw new ArgumentNullException(nameof(a));
			try
			{
				a.Invoke();
			}
			catch
			{
				//Do nothing!
			}
		}
	}
}
