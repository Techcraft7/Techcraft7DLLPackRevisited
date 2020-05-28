using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techcraft7_DLL_Pack.Utils;

namespace Techcraft7_DLL_Pack.Networking.Protocols.Primitives
{
	public sealed class ProtocolInt : ProtocolDataType<int>
	{
		public override int Read(Stream stream) => BitConverter.ToInt32(StreamUtils.ReadBytesFromStream(stream, sizeof(int)), 0);
		public override void Write(Stream stream, int value) => stream.Write(BitConverter.GetBytes(value), 0, sizeof(int));
	}
}
