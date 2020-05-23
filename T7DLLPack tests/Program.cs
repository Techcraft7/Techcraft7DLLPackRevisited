using System;
using System.Collections.Generic;
using Techcraft7_DLL_Pack.Collections;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace T7DLLPack_tests
{
	class Program
	{
		static void Main()
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
			Console.WriteLine("done!");
			Console.Read();

		}
	}
}