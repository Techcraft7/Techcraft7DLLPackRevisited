using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techcraft7_DLL_Pack.HardwareEmulation.DigitalCircuits.LogicGates
{
	class AndGate : IGate
	{
		private TruthTable t;
		public TruthTable IGate.TTable
		{
			get
			{
				return t;
			}
			set
			{
				if (value == null)
				{
				}
			}
		}

		public AndGate(int Inputs)
		{

		}

		void GenerateTTable(int v)
		{
			int[] o = new int[(int)Math.Pow(2, v)];
			for (int i = 0; i < o.Length; i++)
			{
				o[i] = i == o.Length - 1 ? 1 : 0;
			}
		}

		public void IGate.ExecuteFunction()
		{
			
		}
	}
}
