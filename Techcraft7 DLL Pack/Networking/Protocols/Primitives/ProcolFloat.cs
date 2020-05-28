using System;
using System.IO;

namespace Techcraft7_DLL_Pack.Networking.Protocols.Primitives
{
	public sealed class ProcolFloat : ProtocolDataType<float>
	{
		public override float Read(Stream stream) => throw new NotImplementedException();
		public override void Write(Stream stream, float value) => throw new NotImplementedException();
	}
}
