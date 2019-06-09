using System;
using System.Collections.Generic;
using Techcraft7_DLL_Pack.Collections;

namespace T7DLLPack_tests
{
	class Program
	{
		static void Main(string[] args)
		{
			MultiTypeList list = new MultiTypeList();
			list.AddTypeList(typeof(string));
			list.AddTypeList(typeof(int));
			list.AddObject(typeof(string), new MultiTypeListObject("Hello!"));
			list.AddObject(typeof(int), new MultiTypeListObject(12345));
			Console.WriteLine(list[0, typeof(int)]);
			Console.WriteLine(list[0, typeof(string)]);
			Console.Read();
		}
	}
}