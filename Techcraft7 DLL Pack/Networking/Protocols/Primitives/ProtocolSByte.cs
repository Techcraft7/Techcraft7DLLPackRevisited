using System;
using System.IO;

namespace Techcraft7_DLL_Pack.Networking.Protocols.Primitives
{
	public sealed class ProtocolSByte : ProtocolDataType<sbyte>
	{
		public override sbyte Read(Stream stream) => throw new NotImplementedException();
		public override void Write(Stream stream, sbyte value) => throw new NotImplementedException();
	}
}
