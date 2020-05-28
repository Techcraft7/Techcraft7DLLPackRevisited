using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techcraft7_DLL_Pack.Networking.Protocols;

namespace Techcraft7_DLL_Pack.Networking
{
	public abstract class AbstractServer
	{
		public abstract AbstractProtocol GetProtocol();
	}
}
