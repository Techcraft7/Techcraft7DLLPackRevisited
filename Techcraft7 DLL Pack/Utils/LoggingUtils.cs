using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Techcraft7_DLL_Pack.Text;

namespace Techcraft7_DLL_Pack.Utils
{
	public static class LoggingUtils
	{
		public static bool ENABLE_LOGGING = true;
		public static bool DEBUG_LOGGING = false;

		public static void Log(string txt, ConsoleColor color = ConsoleColor.Gray, string defaultPrefix = "T7LOGGER")
		{
			if (ENABLE_LOGGING)
			{
				ColorConsoleMethods.WriteLineColor($"[{Thread.CurrentThread.Name ?? defaultPrefix}] {txt}", color);
			}
		}

#pragma warning disable IDE0060 // Remove unused parameter
		public static void Info(string txt, ConsoleColor color = ConsoleColor.Gray, string defaultPrefix = "INFO") => Log(txt, ConsoleColor.Magenta);
		public static void Error(string txt) => Log(txt, ConsoleColor.Red, "ERROR");
		public static void Error(Exception e) => Log($"{e.GetType().Name}: {e.Message}\n{e.StackTrace}", ConsoleColor.Red, "ERROR");
		public static void Warn(string txt) => Log(txt, ConsoleColor.Yellow, "WARN");
		public static void Success(string txt) => Log(txt, ConsoleColor.Green, "SUCCESS");
		public static void Progress(string txt) => Log(txt, ConsoleColor.Cyan, "PROGRESS");
#pragma warning restore IDE0060 // Remove unused parameter
	}
}
