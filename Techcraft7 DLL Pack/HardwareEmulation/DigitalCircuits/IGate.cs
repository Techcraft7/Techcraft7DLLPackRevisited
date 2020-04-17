using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techcraft7_DLL_Pack.HardwareEmulation.DigitalCircuits
{
	public interface IGate
	{
		TruthTable TTable { get; set; }

		void ExecuteFunctionInternal(int[] inputs);
	}
}
