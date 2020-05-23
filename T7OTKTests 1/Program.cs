using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T7OTKTests_1
{
	class Program
	{
		static void Main()
		{
			using (Game g = new Game())
			{
				g.Run(30);
			}
		}
	}
}
