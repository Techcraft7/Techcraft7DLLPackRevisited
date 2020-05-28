using System;
using System.IO;

namespace Techcraft7_DLL_Pack.Networking.Protocols.Primitives
{
	public sealed class ProtocolByte : ProtocolDataType<byte>
	{
		public override byte Read(Stream stream) => throw new NotImplementedException();
		public override void Write(Stream stream, byte value) => throw new NotImplementedException();
	}
}
