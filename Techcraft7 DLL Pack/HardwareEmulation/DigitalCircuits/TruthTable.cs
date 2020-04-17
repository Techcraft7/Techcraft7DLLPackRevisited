using System;
using System.Text;

namespace Techcraft7_DLL_Pack.HardwareEmulation.DigitalCircuits
{
	public struct TruthTable
	{
		public static readonly TruthTable AllZeroTruthTable = new TruthTable(0, new int[] { 0 });

		private readonly int[] Outputs;
		private readonly int Inputs;

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="Inputs">Number of inputs</param>
		/// <param name="Outputs">Outputs of the function, use 0 and 1... LENGTH OF Outputs MUST BE EQUAL TO 2^Inputs!</param>
		public TruthTable(int Inputs, int[] Outputs)
		{
			this.Outputs = Outputs;
			this.Inputs = Inputs;
			if (Outputs == null || Outputs.Length == 0)
			{
				throw new NullReferenceException("Outputs was null or empty");
			}
			else if (Outputs.Length != (int)System.Math.Pow(2,  Inputs))
			{
				throw new InvalidOperationException("Outputs.Length was not equal to 2^Inputs!");
			}
			else
			{

			}
		}
		private void CheckInputs(Array input)
		{
			if (input == null || input.Length == 0)
			{
				throw new NullReferenceException("Input was null or empty");
			}
			else if (input.Length != Inputs)
			{
				throw new InvalidOperationException("Outputs.Length was not equal to Input.Length!");
			}
			if (input.GetValue(0).GetType() == typeof(int))
			{
				for (int i = 0; i < input.Length; i++)
				{
					if ((int)input.GetValue(i) != 0 || (int)input.GetValue(i) != 1)
					{
						throw new InvalidOperationException("Input contained values that werent 0, 1, false, or true!");
					}
				}
			}
			else if (input.GetValue(0).GetType() == typeof(bool))//already checked length so we should be good!
			{
				for (int i = 0; i < input.Length; i++)
				{
					if ((bool)input.GetValue(i) != false || (bool)input.GetValue(i) != true)
					{
						throw new InvalidOperationException("Input contained values that werent 0, 1, false, or true!");
					}
				}
			}
			else
			{
				throw new InvalidOperationException("Input contained values that werent 0, 1, false, or true!");
			}
		}

		public int RunFunctionOutputInt(int[] Input)
		{
			CheckInputs(Input);
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < Input.Length; i++)
			{
				sb.Append(Input[i]);
			}
			return Outputs[BinaryStringToDecimal(sb.ToString())];
		}

		public bool RunFunctionOutputBool(bool[] Input)
		{
			CheckInputs(Input);
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < Input.Length; i++)
			{
				sb.Append(BoolToInt(Input[i]));
			}
			return IntToBool(Outputs[BinaryStringToDecimal(sb.ToString())]);
		}

		private int BoolToInt(bool v)
		{
			switch (v)
			{
				case false:
					return 0;
				case true:
					return 1;
				default:
					StringBuilder sb = new StringBuilder();
					sb.Append("Attemped to convert ");
					sb.Append(v);
					sb.Append(" to a System.Int32!");
					throw new InvalidOperationException(sb.ToString());
			}
		}

		private bool IntToBool(int v)
		{
			switch (v)
			{
				case 0:
					return false;
				case 1:
					return true;
				default:
					StringBuilder sb = new StringBuilder();
					sb.Append("Attemped to convert ");
					sb.Append(v);
					sb.Append(" to a System.Boolean!");
					throw new InvalidOperationException(sb.ToString());
			}
		}

		private int BinaryStringToDecimal(string v)
		{
			int dec = 0;
			int pow = 0;
			//go backwards to make things easier
			for (int i = v.Length - 1; i >= 0; i--)
			{
				//each place doubles in value, but since were going backwards, we can't just use 'i' as our power...
				dec += (int)System.Math.Pow(2, pow);
				pow++;
			}
			return dec;
		}
	}
}