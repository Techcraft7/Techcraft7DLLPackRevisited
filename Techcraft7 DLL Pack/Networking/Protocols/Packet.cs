using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techcraft7_DLL_Pack.Text;
using PacketParam = System.Collections.Generic.KeyValuePair<string, Techcraft7_DLL_Pack.Networking.Protocols.ProtocolDataType<dynamic>>;
using PacketParams = System.Collections.Generic.Dictionary<string, Techcraft7_DLL_Pack.Networking.Protocols.ProtocolDataType<dynamic>>;

namespace Techcraft7_DLL_Pack.Networking.Protocols
{
	public abstract class Packet
	{
		public abstract PacketParams GetParameters();

		public Dictionary<string, object> Read(Stream stream)
		{
			try
			{
				Dictionary<string, object> output = new Dictionary<string, object>();
				foreach (PacketParam kv in GetParameters())
				{
					output.Add(kv.Key, kv.Value.Read(stream));
				}
				return output;
			}
			catch (Exception e)
			{
				ColorConsoleMethods.WriteLineColor($"{e.GetType()}: {e.Message}\n{e.StackTrace}", ConsoleColor.Red);
				return null;
			}
		}
		public void Write(Stream stream, Dictionary<string, object> parameters)
		{
			try
			{
				foreach (PacketParam kv in GetParameters())
				{
					kv.Value.Write(stream, parameters[kv.Key]);
				}
			}
			catch (Exception e)
			{
				ColorConsoleMethods.WriteLineColor($"{e.GetType()}: {e.Message}\n{e.StackTrace}", ConsoleColor.Red);
			}
		}
	}
}
