using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Techcraft7_DLL_Pack.Net;
using System.Threading;

namespace Techcraft7_DLL_Pack.Net.Server.SingleClient
{
    using static Techcraft7_DLL_Pack.Text.ColorConsoleMethods;
    public class SingleClientServer
    {
		public bool Running { get; private set; }

		const ConsoleColor SUCCESS = ConsoleColor.Green;
		const ConsoleColor INFO = ConsoleColor.Yellow;
		const ConsoleColor ERROR = ConsoleColor.Red;
		const ConsoleColor OTHER = ConsoleColor.Cyan;

		Socket client;
        Socket server;
		int backlog = 100;
		int port = 0;
		int bufferSize = 1024;
        bool inavlid = false;
		byte[] buffer;
        Action<byte[], Socket> onrec;
		Thread servThread;

        public SingleClientServer(int Port, Action<byte[], Socket> WhenRecieved, int Backlog = 100, int BufferSize = 1024)
		{
			bufferSize = BufferSize;
			port = Port;
			backlog = Backlog;
			onrec = WhenRecieved ?? throw new ArgumentNullException(nameof(WhenRecieved));
			servThread = new Thread(new ThreadStart(ServerThread));
			Running = false;
        }

		public void Start()
		{
			if (!Running)
			{
				Running = true;
				servThread.Start();
			}
		}

		public void Send(string data)
        {
            if (inavlid)
            {
                WriteLineColor("Invalid Server setup! Cannot send data!", ConsoleColor.Red);
                return;
            }
            client.Send(Encoding.ASCII.GetBytes(data));
        }

		private void ServerThread()
		{
			try
			{
				WriteLineColor("Single Client Server Starting...", OTHER);
				server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
				server.Bind(new IPEndPoint(IPAddress.Any, port));
				server.Listen(backlog);
				WriteLineColor("Done!", SUCCESS);
				while (Running && (client != null ? client.Connected : true))
				{
					WriteLineColor("Waiting for a connection", INFO);
					client = server.Accept();
					WriteLineColor($"We got a connection! Remote Endpoint: {client.RemoteEndPoint}", SUCCESS);
					while (client.Connected)
					{
						WriteLineColor("Waiting for data...", INFO);
						buffer = new byte[bufferSize];
						_ = client.Receive(buffer, buffer.Length, SocketFlags.None);
						onrec.Invoke(buffer, client);
					}
				}
			}
			catch (Exception e)
			{
				WriteLineColor($"{e.GetType()}: {e.Message}\n{e.StackTrace}", ERROR);
			}
			Running = false;
		}

		public void Stop() => Running = false;
	}
}
