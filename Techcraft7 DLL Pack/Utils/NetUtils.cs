using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Techcraft7_DLL_Pack.Utils
{
	public static class NetUtils
	{
		public static byte ReadByte(Socket s) => ReadBytes(s, 1).First();

		public static byte[] ReadBytes(Socket s, int n)
		{
			s = s ?? throw new ArgumentNullException(nameof(s));
			byte[] buf = new byte[n];
			OtherUtils.IgnoreException(() => _ = s.Receive(buf, SocketFlags.None));
			return buf;
		}

		public static void SendBytes(Socket s, byte[] bytes) => _ = s.Send(bytes, SocketFlags.None);
		public static void SendByte(Socket s, byte v) => SendBytes(s, new byte[] { v });
	}
}
