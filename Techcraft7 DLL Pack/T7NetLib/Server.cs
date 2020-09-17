using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Management.Instrumentation;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Techcraft7_DLL_Pack.T7NetLib.DataTypes;
using Techcraft7_DLL_Pack.T7NetLib.Storage;
using Techcraft7_DLL_Pack.Utils;

namespace Techcraft7_DLL_Pack.T7NetLib
{
	using static LoggingUtils;
	public class Server
	{
		public readonly int MaxClients;
		public readonly ushort Port;
		public bool Running { get; private set; }
		public ServerStorage ClientStorages { get; private set; } = new ServerStorage();
		public SocketStorage Storage { get; private set; } = new SocketStorage();

		private static readonly NetUShort PACKET_ID_READER = new NetUShort();
		private List<ClientThreadState> threads = new List<ClientThreadState>();
		private Socket serverSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
		private IEnumerable<Packet> packets;
		private Action<IntPtr> OnDisconnect;

		public Server(int maxClients, ushort port, IEnumerable<Packet> packets, Action<IntPtr> onDisconnect = null)
		{
			MaxClients = maxClients;
			Port = port;
			Packet.ValidatePacketList(packets, Side.SERVER, out packets);
			this.packets = packets;
			OnDisconnect = onDisconnect;
		}

		public void Start() => Task.Run(() =>
		{
			Thread.CurrentThread.Name = "T7NetLib Server";
			Info("Starting server");
			Progress("Opening server socket");
			serverSocket.Bind(new IPEndPoint(IPAddress.Any, Port));
			serverSocket.Listen(100);
			CreateThreads();
			Progress("Starting threads");
			threads.ForEach(cts => cts.Thread.Start());
			Success("Server started!");
		});

		public void Broadcast(Packet p, Dictionary<string, object> args)
		{
			p = p ?? throw new ArgumentNullException(nameof(p));
			if (DEBUG_LOGGING)
			{
				Warn($"BROADCAST {p.GetType().Name} -> {{ {string.Join(", ", args.Select(kv => $"{kv.Key} = {kv.Value}"))} }}");
			}
			threads.ForEach(cts =>
			{
				OtherUtils.IgnoreException(() =>
				{
					if (cts.Socket != null)
					{
						if (cts.Socket.Connected)
						{
							p.Send(cts.Socket, args);
						}
					}
				});
			});
		}

		private Socket WaitForClient()
		{
			Task<Socket> getSocket = new Task<Socket>(() => serverSocket.Accept());
			getSocket.Start();
			Task.WaitAny(getSocket);
			return getSocket.Result;
		}

		private void ClientThread()
		{
			while (true)
			{
				Socket s = null;
				try
				{
					Progress("Waiting for client...");
					SetStateSocket(Thread.CurrentThread, null);
					s = WaitForClient();
					IntPtr handle;
					if (s != null)
					{
						handle = s.Handle;
						SetStateSocket(Thread.CurrentThread, s);
						Success($"Got connection at {s.RemoteEndPoint}!");
						while (s.Connected)
						{
							try
							{
								ushort pID = PACKET_ID_READER.ReadValue(s);
								Info($"Got packet: 0x{pID:X}");
								foreach (Packet p in packets)
								{
									if (p.GetID() == pID)
									{
										try
										{
											p.HandlingSide = Side.SERVER;
											p.Handle(s, p.Read(s));
										}
										catch (Exception e)
										{
											Error($"Error handling packet 0x{pID:X}:");
											Error(e);
										}
										break;
									}
								}
							}
							catch (SocketException)
							{
								break;
							}
						}
						Info("Client is no longer connected!");
						ClientStorages.Remove(s);
						if (OnDisconnect != null)
						{
							OtherUtils.IgnoreException(() => OnDisconnect.Invoke(handle));
						}
					}
				}
				catch (ThreadAbortException)
				{
					Warn("Thread is aborting!");
					if (s != null)
					{
						Progress("Disconnecting client");
						s.Disconnect(false);
					}
					break;
				}
				catch (Exception e)
				{
					Error(e);
				}
			}
			Success("Client thread exited gracefully");
		}

		private void SetStateSocket(Thread t, Socket s)
		{
			int i = threads.FindIndex(v => v.Thread.ManagedThreadId == t.ManagedThreadId);
			if (i > -1 && i < threads.Count)
			{
				threads[i].Socket = s;
			}
			else
			{
				throw new InstanceNotFoundException("Could not find thread state!");
			}
		}

		private void CreateThreads()
		{
			Info("Creating threads");
			for (int i = 0; i < MaxClients; i++)
			{
				//Without this we might get two threads with the name name!
				threads.Add(new ClientThreadState()
				{
					Thread = new Thread(new ThreadStart(ClientThread))
					{
						Name = $"Client Thread {i + 1}"
					}
				});
				Thread.Sleep(25);
			}
			Success($"Created {threads.Count} client threads");
		}

		private IEnumerable<bool> GetStoppedThreads() => threads.Select(cts => cts.Thread == null || (cts.Thread.ThreadState == ThreadState.Stopped));


		public void Stop()
		{
			Info("Stopping server!");
			threads.ForEach(cts => cts.Thread.Abort());
			//Wait for all threads to stop
			Task stopTask = new Task(() =>
			{
				Thread.CurrentThread.Name = "T7NetLib Server Stopper";
				while (GetStoppedThreads().Any(b => !b))
				{
					IEnumerable<bool> stopped = GetStoppedThreads();
					Progress($"{stopped.Where(b => b).Count()}/{stopped.Count()} threads stopped!");
					Thread.Sleep(1000);
				}
			});
			Task.WaitAll(stopTask);
			Success("Server Stopped!");
		}
	}
}
