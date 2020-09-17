using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
using System.Linq;
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
	public class Client
	{
		public string Name = null;
		public bool Verbose = false;
		public bool Connected => serverSock != null && serverSock.Connected;
		public SocketStorage Storage { get; private set; } = new SocketStorage();

		private readonly EndPoint serverEP;
		private readonly IEnumerable<Packet> packets;
		private Socket serverSock;
		private Thread conThread;
		private Thread sendThread;
		private ConcurrentQueue<Tuple<Packet, Dictionary<string, object>>> sendQueue = new ConcurrentQueue<Tuple<Packet, Dictionary<string, object>>>();

		public Client(EndPoint server, IEnumerable<Packet> packets)
		{
			serverEP = server;
			Packet.ValidatePacketList(packets, Side.CLIENT, out packets);
			this.packets = packets;
		}

		public void Send(Packet p, Dictionary<string, object> args)
		{
			if (conThread == null)
			{
				throw new InvalidOperationException($"{nameof(conThread)} is null!");
			}
			if (!conThread.IsAlive)
			{
				throw new InvalidOperationException("Client is not running!");
			}
			p = p ?? throw new ArgumentNullException(nameof(p));
			args = args ?? throw new ArgumentNullException(nameof(args));
			sendQueue.Enqueue(Tuple.Create(p, args));
		}

		public void Start()
		{
			serverSock = new Socket(SocketType.Stream, ProtocolType.Tcp);
			sendThread = new Thread(() =>
			{
				while (serverSock.Connected)
				{
					try
					{
						if (sendQueue.TryDequeue(out Tuple<Packet, Dictionary<string, object>> t))
						{
							if (DEBUG_LOGGING)
							{
								Warn($"Sending {t.Item1.GetType().Name} -> {{ {string.Join(", ", t.Item2.Select(kv => $"{kv.Key} = {kv.Value}"))} }}");
							}
							t.Item1.Send(serverSock, t.Item2);
						}
					}
					catch (Exception e)
					{
						Error(e);
					}
				}
			})
			{
				Name = Name ?? "T7NL > Send Thread"
			};
			conThread = new Thread(() =>
			{
				Info("Starting client");
				try
				{
					Progress("Connecting...");
					serverSock.Connect(serverEP);
					Success("Connected!");
				}
				catch (Exception e)
				{
					Error("Error while connecting:");
					Error(e);
					return;
				}
				sendThread.Start();
				while (serverSock.Connected)
				{
					try
					{
						ushort pID = new NetUShort().ReadValue(serverSock);
						foreach (Packet p in packets)
						{
							if (p.GetID() == pID)
							{
								if (Verbose)
								{
									Log($"Got packet: 0x{pID:X}");
								}
								try
								{
									p.HandlingSide = Side.CLIENT;
									Dictionary<string, object> dict = p.Read(serverSock);
									if (DEBUG_LOGGING)
									{
										Warn($"Processing packet {p.GetType().Name} -> {{ {string.Join(", ", dict.Select(kv => $"{kv.Key} = {kv.Value}"))} }}");
									}
									p.Handle(serverSock, dict);
								}
								catch (ThreadAbortException)
								{
									serverSock.Disconnect(false);
									break;
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
					catch (ThreadAbortException)
					{
						if (Verbose)
						{
							Warn("Thread aborted!");
						}
						if (serverSock != null)
						{
							OtherUtils.IgnoreException(() =>
							{
								serverSock.Disconnect(false);
							});
						}
						break;
					}
				}
				Success("Client thread exited gracefully");
			})
			{
				Name = Name ?? "T7NetLib Client"
			};
			conThread.Start();
			while (serverSock == null || serverSock.Connected)
			{
				// Do nothing!
			}

			Success("Client started!");
		}

		public void Stop()
		{
			Info("Stopping!");
			StopThread(conThread);
			StopThread(sendThread);
			Success("Stopped!");
		}

		private void StopThread(Thread t)
		{
			if (t != null && t.IsAlive)
			{
				OtherUtils.IgnoreException(() =>
				{
					if (serverSock != null && serverSock.Connected)
					{
						OtherUtils.IgnoreException(() =>
						{
							serverSock.Disconnect(false);
						});
						OtherUtils.IgnoreException(() =>
						{
							serverSock.Close(1000);
						});
					}
				});
				while (t.IsAlive)
				{
					OtherUtils.IgnoreException(() =>
					{
						t.Abort();
					});
					Progress("Waiting for client to disconnect...");
					Thread.Sleep(250);
				}
			}
		}
	}
}