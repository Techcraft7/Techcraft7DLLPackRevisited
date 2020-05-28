using System;
using System.IO;

namespace Techcraft7_DLL_Pack.Networking.Protocols.Primitives
{
	public sealed class ProtocolLong : ProtocolDataType<long>
	{
		public override long Read(Stream stream) => throw new NotImplementedException();
		public override void Write(Stream stream, long value) => throw new NotImplementedException();
	}
}
