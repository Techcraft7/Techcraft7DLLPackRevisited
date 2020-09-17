using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Techcraft7_DLL_Pack.T7NetLib
{
	internal class ClientThreadState
	{
		public Thread Thread { get; set; }
		public Socket Socket { get; set; }
	}
}
