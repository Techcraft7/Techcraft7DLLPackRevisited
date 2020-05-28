using System;
using System.IO;
using Techcraft7_DLL_Pack.Utils;

namespace Techcraft7_DLL_Pack.Networking.Protocols.Primitives
{
	public sealed class ProtocolUInt : ProtocolDataType<uint>
	{
		public override uint Read(Stream stream) => BitConverter.ToUInt32(StreamUtils.ReadBytesFromStream(stream, sizeof(uint)), 0);
		public override void Write(Stream stream, uint value) => stream.Write(BitConverter.GetBytes(value), 0, sizeof(uint));
	}
}
