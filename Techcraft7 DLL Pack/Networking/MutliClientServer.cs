using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techcraft7_DLL_Pack.Networking.Protocols;

namespace Techcraft7_DLL_Pack.Networking
{
	public class MutliClientServer : AbstractServer
	{
		private readonly AbstractProtocol protocol;

		public MutliClientServer(AbstractProtocol p) => protocol = p ?? throw new ArgumentNullException(nameof(p));

		public override AbstractProtocol GetProtocol() => protocol;
	}
}
