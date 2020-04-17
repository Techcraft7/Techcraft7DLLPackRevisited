using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Techcraft7_DLL_Pack.Net.Clients
{
    using static Techcraft7_DLL_Pack.Text.ColorConsoleMethods;
    public class SimpleClient
    {
		public bool Running { get; private set; }

		private Socket clientSocket;
		private string IP;
		private int port;
		private Action<byte[], Socket> onrec;
		private int bufferSize;

		public SimpleClient(string IP, int port, Action<byte[], Socket> OnRecieve, int BufferSize = 1024)
        {
			this.IP = IP;
			this.port = port;
			bufferSize = BufferSize;
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			onrec = OnRecieve ?? throw new ArgumentNullException(nameof(OnRecieve));
        }


		public void Send(string data)
        {
			if (clientSocket.Connected)
			{
				_ = clientSocket.Send(Encoding.ASCII.GetBytes(data));
			}
        }

		public void Stop() => Running = false;

		private void ClientThread()
		{
			try
			{
				while (Running && clientSocket.Connected)
				{
					byte[] buffer = new byte[bufferSize];
					_ = clientSocket.Receive(buffer, buffer.Length, SocketFlags.None);
					onrec.Invoke(buffer, clientSocket);
				}
			}
			catch (Exception e)
			{
				WriteLineColor($"{e.GetType()}: {e.Message}\n{e.StackTrace}", ConsoleColor.Red);
			}
			Running = false;
		}

		public void Connect()
		{
			clientSocket.Connect(new IPEndPoint(IPAddress.Parse(IP), port));
			if (clientSocket.Connected)
			{
				new Thread(new ThreadStart(ClientThread)).Start();
			}
		}
	}
}
