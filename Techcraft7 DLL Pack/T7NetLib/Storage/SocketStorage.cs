using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techcraft7_DLL_Pack.Utils;

namespace Techcraft7_DLL_Pack.T7NetLib.Storage
{
	public class SocketStorage
	{
		private ConcurrentDictionary<string, object> dict = new ConcurrentDictionary<string, object>();
		
		public object this[string k]
		{
			get => OtherUtils.IgnoreException(() => dict[k]);
			set => OtherUtils.IgnoreException(() =>
			{
				if (dict.ContainsKey(k))
				{
					OtherUtils.IgnoreException(() => dict[k] = value);
				}
				else
				{
					OtherUtils.IgnoreException(() => dict.TryAdd(k, value));
				}
			});
		}

		public override string ToString()
		{
			return $"{{ {string.Join(", ", dict.Select(kv => $"{kv.Key} = {kv.Value}"))} }}";
		}
	}
}
