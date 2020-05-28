using System;
using System.IO;
using System.Text;
using Techcraft7_DLL_Pack.Utils;

namespace Techcraft7_DLL_Pack.Networking.Protocols.Primitives
{
	public sealed class ProtocolString : ProtocolDataType<string>
	{
		private static Encoding encoding;
		private static readonly ProtocolInt pInt = new ProtocolInt();
		public static Encoding Encoding
		{
			get => encoding;
			set => encoding = value ?? throw new ArgumentNullException(nameof(value));
		}

		public override string Read(Stream stream)
		{
			int len = pInt.Read(stream);
			return encoding.GetString(StreamUtils.ReadBytesFromStream(stream, len));
		}

		public override void Write(Stream stream, string value)
		{
			byte[] data = encoding.GetBytes(value);
			pInt.Write(stream, data.Length);
			stream.Write(data, 0, data.Length);
		}
	}
}
