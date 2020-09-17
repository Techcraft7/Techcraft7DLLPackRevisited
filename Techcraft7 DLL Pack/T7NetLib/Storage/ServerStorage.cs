using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using Techcraft7_DLL_Pack.Utils;

namespace Techcraft7_DLL_Pack.T7NetLib.Storage
{
	public class ServerStorage
	{
		private ConcurrentDictionary<Socket, SocketStorage> dict = new ConcurrentDictionary<Socket, SocketStorage>();

		public object this[Socket s, string k]
		{
			get => OtherUtils.IgnoreException(() => dict[s][k]);
			set => OtherUtils.IgnoreException(() =>
			{
				if (!dict.ContainsKey(s))
				{
					_ = dict.TryAdd(s, new SocketStorage());
				}
				SocketStorage temp = dict[s];
				temp[k] = value;
				dict[s] = temp;
			});
		}

		public IReadOnlyCollection<SocketStorage> GetStorages() => new ReadOnlyCollection<SocketStorage>(dict.Values.ToList());
		public void Remove(Socket s)
		{
			int x = 0;
			while (x < 100 || !dict.TryRemove(s, out _))
			{
				x++;
			}
		}

		public override string ToString()
		{
			return $"{{\n{string.Join(",\n", dict.Select(kv => $"{kv.Key.Handle} = {kv.Value}"))}\n}}";
		}
	}
}
