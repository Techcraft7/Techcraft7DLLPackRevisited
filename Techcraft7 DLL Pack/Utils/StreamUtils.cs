using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techcraft7_DLL_Pack.Utils
{
	public static class StreamUtils
	{
		public static byte[] ReadBytesFromStream(Stream s, int n)
		{
			byte[] data = new byte[n];
			_ = s.Read(data, 0, n);
			return data;
		}
	}
}
