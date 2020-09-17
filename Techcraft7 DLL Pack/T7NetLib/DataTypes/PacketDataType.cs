using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Techcraft7_DLL_Pack.Utils;

namespace Techcraft7_DLL_Pack.T7NetLib.DataTypes
{
	public abstract class PacketDataType<T> : NetDataType
	{
		public static readonly Type GENERIC = typeof(NetInt).BaseType.GetGenericTypeDefinition();

		public override object Read(Socket s) => ReadValue(s);
		public override void Write(Socket s, object v)
		{
			if (!(v is T))
			{
				throw new ArgumentException($"{nameof(v)} was not a(n) {typeof(T)} or one of its children");
			}
			WriteValue(s, (T)v);
		}

		public abstract T ReadValue(Socket s);
		public abstract void WriteValue(Socket s, T v);
	}
}
