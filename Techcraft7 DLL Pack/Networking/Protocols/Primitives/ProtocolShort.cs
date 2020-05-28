using System;
using System.IO;
using Techcraft7_DLL_Pack.Utils;

namespace Techcraft7_DLL_Pack.Networking.Protocols.Primitives
{
	public sealed class ProtocolShort : ProtocolDataType<short>
	{
		public override short Read(Stream stream) => BitConverter.ToInt16(StreamUtils.ReadBytesFromStream(stream, sizeof(short)), 0);
		public override void Write(Stream stream, short value) => stream.Write(BitConverter.GetBytes(value), 0, sizeof(short));
	}
}
