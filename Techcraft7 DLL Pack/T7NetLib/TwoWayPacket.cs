using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Techcraft7_DLL_Pack.T7NetLib.DataTypes;
using Techcraft7_DLL_Pack.Utils;

namespace Techcraft7_DLL_Pack.T7NetLib
{
	public abstract class TwoWayPacket : Packet
	{
		public override Dictionary<string, NetDataType> GetArgs() => throw new DontUseMeException();

		/// <summary>
		/// Data that the CLIENT RECIEVES
		/// </summary>
		public abstract Dictionary<string, NetDataType> GetArgsClient();

		/// <summary>
		/// Data that the SERVER recieves from the client
		/// </summary>
		public abstract Dictionary<string, NetDataType> GetArgsServer();

		public TwoWayPacket()
		{
			CheckArgs(GetArgsClient());
			CheckArgs(GetArgsServer());
		}

		public override void Handle(Socket s, Dictionary<string, object> args)
		{
			switch (HandlingSide)
			{
				case Side.CLIENT:
					HandleClient(s, args);
					break;
				case Side.SERVER:
					HandleServer(s, args);
					break;
			}
		}

		protected override Dictionary<string, NetDataType> GetUsedArgs(bool send)
		{
			if (send)
			{
				switch (HandlingSide)
				{
					case Side.CLIENT:
						return GetArgsServer();
					case Side.SERVER:
						return GetArgsClient();
				}
			}
			else
			{
				switch (HandlingSide)
				{
					case Side.CLIENT:
						return GetArgsClient();
					case Side.SERVER:
						return GetArgsServer();
				}
			}
			throw new InvalidOperationException("This should not happen!");
		}

		public abstract void HandleServer(Socket s, Dictionary<string, object> args);

		public abstract void HandleClient(Socket s, Dictionary<string, object> args);
	}
}
