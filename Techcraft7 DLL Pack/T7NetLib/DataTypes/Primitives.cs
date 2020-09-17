using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using Techcraft7_DLL_Pack.Utils;

namespace Techcraft7_DLL_Pack.T7NetLib.DataTypes
{
	public sealed class NetByte : PacketDataType<byte>
	{
		public override byte ReadValue(Socket s) => NetUtils.ReadByte(s);
		public override void WriteValue(Socket s, byte v) => s.Send(new byte[] { v }, SocketFlags.None);
	}

	public sealed class NetBytes : NetArray<byte>
	{
		protected override byte ReadOne(Socket s) => NetUtils.ReadByte(s);
		protected override void WriteOne(Socket s, byte v) => NetUtils.SendByte(s, v);
	}

	public sealed class NetSByte : PacketDataType<sbyte>
	{
		public override sbyte ReadValue(Socket s)
		{
			unchecked
			{
				return (sbyte)NetUtils.ReadByte(s);
			}
		}

		public override void WriteValue(Socket s, sbyte v)
		{
			unchecked
			{
				s.Send(new byte[] { (byte)v }, SocketFlags.None);
			}
		}
	}

	public sealed class NetSBytes : NetArray<sbyte>
	{
		protected override sbyte ReadOne(Socket s) => new NetSByte().ReadValue(s);		protected override void WriteOne(Socket s, sbyte v) => new NetSByte().WriteValue(s, v);
	}

	public sealed class NetInt : PacketDataType<int>
	{
		public override int ReadValue(Socket s) => BitConverter.ToInt32(NetUtils.ReadBytes(s, sizeof(int)), 0);
		public override void WriteValue(Socket s, int v) => NetUtils.SendBytes(s, BitConverter.GetBytes(v));
	}

	public sealed class NetInts : NetArray<int>
	{
		protected override int ReadOne(Socket s) => new NetInt().ReadValue(s);
		protected override void WriteOne(Socket s, int v) => new NetInt().WriteValue(s, v);
	}

	public sealed class NetUInt : PacketDataType<uint>
	{
		public override uint ReadValue(Socket s) => BitConverter.ToUInt32(NetUtils.ReadBytes(s, sizeof(uint)), 0);
		public override void WriteValue(Socket s, uint v) => NetUtils.SendBytes(s, BitConverter.GetBytes(v));
	}

	public sealed class NetUInts : NetArray<uint>
	{
		protected override uint ReadOne(Socket s) => new NetUInt().ReadValue(s);
		protected override void WriteOne(Socket s, uint v) => new NetUInt().WriteValue(s, v);
	}

	public sealed class NetShort : PacketDataType<short>
	{
		public override short ReadValue(Socket s) => BitConverter.ToInt16(NetUtils.ReadBytes(s, sizeof(short)), 0);
		public override void WriteValue(Socket s, short v) => NetUtils.SendBytes(s, BitConverter.GetBytes(v));
	}

	public sealed class NetShorts : NetArray<short>
	{
		protected override short ReadOne(Socket s) => new NetShort().ReadValue(s);
		protected override void WriteOne(Socket s, short v) => new NetShort().WriteValue(s, v);
	}

	public sealed class NetUShort : PacketDataType<ushort>
	{
		public override ushort ReadValue(Socket s) => BitConverter.ToUInt16(NetUtils.ReadBytes(s, sizeof(ushort)), 0);
		public override void WriteValue(Socket s, ushort v) => NetUtils.SendBytes(s, BitConverter.GetBytes(v));
	}

	public sealed class NetUShorts : NetArray<ushort>
	{
		protected override ushort ReadOne(Socket s) => new NetUShort().ReadValue(s);
		protected override void WriteOne(Socket s, ushort v) => new NetUShort().WriteValue(s, v);
	}

	public sealed class NetLong : PacketDataType<long>
	{
		public override long ReadValue(Socket s) => BitConverter.ToInt64(NetUtils.ReadBytes(s, sizeof(long)), 0);
		public override void WriteValue(Socket s, long v) => NetUtils.SendBytes(s, BitConverter.GetBytes(v));
	}

	public sealed class NetLongs : NetArray<long>
	{
		protected override long ReadOne(Socket s) => new NetLong().ReadValue(s);
		protected override void WriteOne(Socket s, long v) => new NetLong().WriteValue(s, v);
	}

	public sealed class NetULong : PacketDataType<ulong>
	{
		public override ulong ReadValue(Socket s) => BitConverter.ToUInt64(NetUtils.ReadBytes(s, sizeof(ulong)), 0);
		public override void WriteValue(Socket s, ulong v) => NetUtils.SendBytes(s, BitConverter.GetBytes(v));
	}

	public sealed class NetULongs : NetArray<ulong>
	{
		protected override ulong ReadOne(Socket s) => new NetULong().ReadValue(s);
		protected override void WriteOne(Socket s, ulong v) => new NetULong().WriteValue(s, v);
	}

	public class NetString : PacketDataType<string>
	{
		protected readonly Encoding encoder;

		public NetString(Encoding e) : base() => encoder = e ?? throw new ArgumentNullException(nameof(e));

		public override string ReadValue(Socket s) => encoder.GetString(NetUtils.ReadBytes(s, new NetInt().ReadValue(s)));

		public override void WriteValue(Socket s, string v)
		{
			new NetInt().WriteValue(s, v.Length);
			NetUtils.SendBytes(s, encoder.GetBytes(v));
		}
	}

	public sealed class NetStringFixed : PacketDataType<string>
	{
		private readonly int maxLength;
		private readonly Encoding encoder;

		public NetStringFixed(Encoding e, int maxLength)
		{
			this.maxLength = maxLength;
			encoder = e ?? throw new ArgumentNullException(nameof(e));
		}

		public override string ReadValue(Socket s) => encoder.GetString(NetUtils.ReadBytes(s, Math.Min(new NetInt().ReadValue(s), maxLength)));

		public override void WriteValue(Socket s, string v) => new NetString(encoder).WriteValue(s, v.Substring(0, Math.Min(v.Length, maxLength)));
	}

	public class NetStrings : NetArray<string>
	{
		private readonly Encoding encoding;

		public NetStrings(Encoding e) => encoding = e ?? throw new ArgumentNullException(nameof(e));

		protected override string ReadOne(Socket s) => new NetString(encoding).ReadValue(s);
		protected override void WriteOne(Socket s, string v) => new NetString(encoding).WriteValue(s, v);
	}
}