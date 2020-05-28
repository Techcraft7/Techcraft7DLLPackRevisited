using System;
using System.IO;

namespace Techcraft7_DLL_Pack.Networking.Protocols.Primitives
{
	public sealed class ProtocolULong : ProtocolDataType<ulong>
	{
		public override ulong Read(Stream stream) => throw new NotImplementedException();
		public override void Write(Stream stream, ulong value) => throw new NotImplementedException();
	}
}
