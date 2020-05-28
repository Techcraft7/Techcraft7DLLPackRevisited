using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techcraft7_DLL_Pack.Networking.Protocols
{
	public abstract class ProtocolDataType<T>
	{
		public abstract T Read(Stream stream);
		public abstract void Write(Stream stream, T value);
	}
}
