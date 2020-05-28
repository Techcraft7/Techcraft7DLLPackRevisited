using System;
using System.IO;

namespace Techcraft7_DLL_Pack.Networking.Protocols.Primitives
{
	public sealed class ProcolDouble : ProtocolDataType<double>
	{
		public override double Read(Stream stream) => throw new NotImplementedException();
		public override void Write(Stream stream, double value) => throw new NotImplementedException();
	}
}
