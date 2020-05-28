using System;
using System.IO;
using Techcraft7_DLL_Pack.Utils;

namespace Techcraft7_DLL_Pack.Networking.Protocols.Primitives
{
	public sealed class ProtocolUShort : ProtocolDataType<ushort>
	{
		public override ushort Read(Stream stream) => BitConverter.ToUInt16(StreamUtils.ReadBytesFromStream(stream, sizeof(ushort)), 0);
		public override void Write(Stream stream, ushort value) => stream.Write(BitConverter.GetBytes(value), 0, sizeof(ushort));
	}
}
