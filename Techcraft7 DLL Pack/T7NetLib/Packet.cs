using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Techcraft7_DLL_Pack.Linq;
using Techcraft7_DLL_Pack.T7NetLib.DataTypes;
using Techcraft7_DLL_Pack.Utils;
using PacketArg = System.Collections.Generic.KeyValuePair<string, Techcraft7_DLL_Pack.T7NetLib.DataTypes.NetDataType>;
using PacketArgs = System.Collections.Generic.Dictionary<string, object>;
using PacketInfo = System.Collections.Generic.Dictionary<string, Techcraft7_DLL_Pack.T7NetLib.DataTypes.NetDataType>;

namespace Techcraft7_DLL_Pack.T7NetLib
{
	public abstract class Packet
	{
		/// <summary>
		/// The side that the packet is being handled on
		/// </summary>
		public Side HandlingSide { get; internal set; }

		public Packet()
		{
			try
			{
				CheckArgs(GetArgs());
			}
			catch (DontUseMeException)
			{
				// Do nothing
			}
		}

		internal void CheckArgs(PacketInfo args)
		{
			if (args == null || (args != null && args.Any(kv => kv.Value == null)))
			{
				throw new NoNullAllowedException($"{nameof(GetArgs)} must not return null and must not have a null value!");
			}
		}

		public abstract PacketInfo GetArgs();
		public abstract ushort GetID();

		private bool ValidateArgs(PacketArgs args, PacketInfo expected, out string invalid)
		{
			List<string> invalidThings = new List<string>();
			if (args.Count != expected.Count)
			{
				invalid = "Argument count did not match";
				return false;
			}
			foreach (PacketArg kv in expected)
			{
				if (!args.ContainsKey(kv.Key))
				{
					invalidThings.Add($"{kv.Key} is not in {nameof(GetArgs)}");
				}
			}
			if (invalidThings.Count > 0)
			{
				invalid = string.Join(", ", invalidThings);
				return false;
			}
			invalid = string.Empty;
			return true;
		}

		public PacketArgs Read(Socket s)
		{
			PacketArgs buf = new PacketArgs();
			foreach (PacketArg kv in GetUsedArgs(false))
			{
				buf.Add(kv.Key, kv.Value.Read(s));
			}
			return buf;
		}

		public void Send(Socket s, PacketArgs args)
		{
			SendInternal(s, args, GetUsedArgs(true));
		}

		protected virtual PacketInfo GetUsedArgs(bool send) => GetArgs();

		protected void SendInternal(Socket s, PacketArgs args, PacketInfo expected)
		{
			s = s ?? throw new ArgumentNullException(nameof(s));
			if (!ValidateArgs(args, expected, out string invalids))
			{
				throw new ArgumentException($"Arguments are invalid: {invalids}");
			}
			new NetUShort().Write(s, GetID());
			foreach (PacketArg kv in expected)
			{
				kv.Value.Write(s, args[kv.Key]);
			}
			AfterSend(s, args);
		}

		public virtual void AfterSend(Socket s, PacketArgs args)
		{
			// Do nothing by default!
		}

		public static void ValidatePacketList(IEnumerable<Packet> packets, Side side, out IEnumerable<Packet> newPackets)
		{
			packets = packets ?? throw new ArgumentNullException(nameof(packets));
			if (packets.Any(p => p == null))
			{
				throw new NoNullAllowedException("One of the packets supplied was null!");
			}
			List<Packet> temp = new List<Packet>();
			packets.Foreach(p =>
			{
				p.HandlingSide = side;
				temp.Add(p);
			});
			newPackets = temp;
		}

		public abstract void Handle(Socket s, PacketArgs args);
	}
}