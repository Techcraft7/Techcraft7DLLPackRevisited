using System.Net.Sockets;
using Techcraft7_DLL_Pack.Utils;

namespace Techcraft7_DLL_Pack.T7NetLib.DataTypes
{
	public abstract class NetDataType : OnlyAllowBaseClassesInThisAssembly
	{
		public abstract object Read(Socket s);
		public abstract void Write(Socket s, object v);
	}
}
