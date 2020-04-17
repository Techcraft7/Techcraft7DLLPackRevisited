using System;
using System.Collections.Generic;
using Techcraft7_DLL_Pack.Collections;
using Techcraft7_DLL_Pack.HardwareEmulation.Registers;
using Techcraft7_DLL_Pack.HardwareEmulation.DigitalCircuits;
using Techcraft7_DLL_Pack.Net.Clients;
using Techcraft7_DLL_Pack.Net;
using System.Text;
using Techcraft7_DLL_Pack.Net.Server.SingleClient;
using System.Threading;
using System.Net.Sockets;

namespace T7DLLPack_tests
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("multitype list tests");
			Console.SetWindowSize(Console.WindowWidth + 50, Console.WindowHeight);
			MultiTypeList list = new MultiTypeList();
			list.AddTypeList(typeof(string));
			list.AddTypeList(typeof(int));
			list.AddObject(typeof(string), new MultiTypeListObject("Hello!"));
			list.AddObject(typeof(int), new MultiTypeListObject(12345));
			Console.WriteLine(list[0, typeof(int)]);
			Console.WriteLine(list[0, typeof(string)]);
			Console.WriteLine("UI tests");
			bool cancel = true;
			if (cancel)
			{
				Console.WriteLine("Canceled UI tests");
			}
			Form1 f = new Form1(cancel);
			if (f.ShowDialog() == f.DialogResult)
			{
				//do nothing!
			}
			Console.WriteLine("register tests");
			int size = 2;
			ShiftRegister r = new ShiftRegister(size, 2, false);
			for (int i = 0; i < (int) Math.Pow(2, size); i++)
			{
				bool v = i % 2 == 0;
				r.SetAdress(i, new bool[] { v, !v, v, !v, v, !v, v, !v, v, !v, v, !v });
			}
			Console.WriteLine(r.ToString());
			for (int i = 0; i < (int)Math.Pow(2, size); i++)
			{
				r.ShiftRight();
				Console.WriteLine(r.ToString());
			}
			for (int i = 0; i < (int)Math.Pow(2, size); i++)
			{
				bool v = i % 2 == 0;
				r.SetAdress(i, new bool[] { v, !v });
			}
			Console.WriteLine(r.ToString());
			for (int i = 0; i < (int)Math.Pow(2, size); i++)
			{
				r.ShiftLeft();
				Console.WriteLine(r.ToString());
			}
			Console.WriteLine("done!");
			Console.WriteLine("gate tests");
            Console.WriteLine("done!");
            Console.WriteLine("net tests");
			Console.WriteLine("SINGLE CLIENT");
			SimpleClient sc1 = new SimpleClient("127.0.0.1", 1234, new Action<byte[], Socket>(OnRec));
			SingleClientServer scserv = new SingleClientServer(1234, new Action<byte[], Socket>(ServOnRec));
			scserv.Start();
			sc1.Connect();
            sc1.Send("Hey!");
			Thread.Sleep(3000);
			scserv.Start();
			sc1.Stop();
			Console.WriteLine("MULTICLIENT");
			Console.Read();
		}

		private static void ServOnRec(byte[] data, Socket client)
		{
			Console.WriteLine("[CLIENT] " + Encoding.ASCII.GetString(data));
			client.Send(Encoding.ASCII.GetBytes("Hey! :)"));
		}

		private static void OnRec(byte[] data, Socket server) => Console.WriteLine("[SERVER] " + Encoding.ASCII.GetString(data));
	}
}