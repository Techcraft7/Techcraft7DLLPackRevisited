using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Techcraft7_DLL_Pack.Utils;

namespace Techcraft7_DLL_Pack.T7NetLib.DataTypes
{
	public abstract class NetArray<T> : PacketDataType<T[]>
	{
		public static new readonly Type GENERIC = typeof(NetInts).BaseType.GetGenericTypeDefinition();

		public override T[] ReadValue(Socket s) => ReadArray(s);
		public override void WriteValue(Socket s, T[] v)
		{
			if (!(v is IEnumerable<T>))
			{
				throw new ArgumentException($"{nameof(v)} is NOT an array of ${typeof(T)}'s");
			}
			WriteArray(s, v);
		}

		private T[] ReadArray(Socket s)
		{
			List<T> buf = new List<T>();
			uint size = new NetUInt().ReadValue(s);
			for (uint i = 0; i < size; i++)
			{
				buf.Add(ReadOne(s));
			}
			return buf.ToArray();
		}

		private void WriteArray(Socket s, T[] v)
		{
			v = v ?? throw new ArgumentNullException(nameof(v));
			new NetUInt().WriteValue(s, (uint)v.Length);
			foreach (T i in v)
			{
				WriteOne(s, i);
			}
		}

		protected abstract T ReadOne(Socket s);
		protected abstract void WriteOne(Socket s, T v);
	}
}
