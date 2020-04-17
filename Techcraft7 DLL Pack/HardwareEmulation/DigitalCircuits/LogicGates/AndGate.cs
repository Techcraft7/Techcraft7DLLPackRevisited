using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Techcraft7_DLL_Pack.HardwareEmulation.DigitalCircuits;

namespace Techcraft7_DLL_Pack.HardwareEmulation.DigitalCircuits.LogicGates
{
    public class AndGate : IGate
	{
        TruthTable IGate.TTable	{ get; set; }

		public AndGate(int Inputs)
		{

		}

        void GenerateTTable(int v)
		{
			int[] o = new int[(int)System.Math.Pow(2, v)];
			for (int i = 0; i < o.Length; i++)
			{
				o[i] = i == o.Length - 1 ? 1 : 0;
			}
		}

        public void ExecuteFunction(int[] inputs)
        {

        }

        public void ExecuteFunction(bool[] inputs)
        {

        }

        void IGate.ExecuteFunctionInternal(int[] inputs)
		{
			
		}
	}
}
