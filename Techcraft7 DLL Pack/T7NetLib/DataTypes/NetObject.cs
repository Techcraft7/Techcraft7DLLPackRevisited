using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Techcraft7_DLL_Pack.Utils;

namespace Techcraft7_DLL_Pack.T7NetLib.DataTypes
{
	/// <summary>
	/// <para>Networking data type to send and recieve objects over the network</para>
	/// <para>See <see cref="IByteable"/> on object to byte conversion</para>
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class NetObject<T> : PacketDataType<T> where T : class, IByteable<T>
	{
		private readonly T instance;

		/// <summary>
		/// Constructor, requires an instance to convert
		/// </summary>
		/// <param name="instance">Instance of the object you want to convert</param>
		public NetObject(T instance)
		{
			this.instance = instance ?? throw new ArgumentNullException("Cannot have a null instance!");
		}

		public override T ReadValue(Socket s) => instance.Get(NetUtils.ReadBytes(s, instance.GetSize()));
		public override void WriteValue(Socket s, T v) => NetUtils.SendBytes(s, v.GetBytes());
	}
}
