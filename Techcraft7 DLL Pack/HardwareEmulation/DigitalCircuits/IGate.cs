using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techcraft7_DLL_Pack.HardwareEmulation.DigitalCircuits
{
	interface IGate
	{
		TruthTable TTable { get; set; }
		TruthTable Techcraft7_DLL_Pack.HardwareEmulation.DigitalCircuits.IGate.TTable { get; set; }

		void ExecuteFunction();
	}
}
