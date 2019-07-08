using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techcraft7_DLL_Pack.HardwareEmulation.Registers
{
	public class ShiftRegister
	{
		bool loop;
		int adresssize;
		int datasize;
		int size;
		private readonly bool debug = false;
		bool[,] register;

		public ShiftRegister(int AdressBitWidth, int DataBitWidth, bool LoopDataBackToStart)
		{
			loop = LoopDataBackToStart;
			adresssize = AdressBitWidth;
			datasize = DataBitWidth;
			size = (int)Math.Pow(2, AdressBitWidth);
			register = new bool[datasize, size];
			InitRegister();
		}

		public ShiftRegister(int AdressBitWidth, int DataBitWidth, bool LoopDataBackToStart, bool Debug)
		{
			debug = Debug;
			loop = LoopDataBackToStart;
			adresssize = AdressBitWidth;
			datasize = DataBitWidth;
			size = (int)Math.Pow(2, AdressBitWidth);
			register = new bool[datasize, size];
			InitRegister();
			if (debug)
			{
				Console.WriteLine("created a {0}x{1} register", datasize, size);
			}
		}

		private bool[] EmptyArray()
		{
			return new bool[datasize];
		}

		private void InitRegister()
		{
			for (int i = 0; i < size; i++)
			{

			}
		}

		public override string ToString()
		{
			string output = "";
			for (int d = 0; d < datasize; d++)
			{
				for (int a = 0; a < size; a++)
				{
					if (debug)
					{
						Console.WriteLine("Getting Value 0x{0}, 0x{1}", d.ToString("X"), a.ToString("X"));
					}
					output += register[d, a] == false ? "0 " : "1 ";
				}
				output += "\n";
			}
			return output;
		}

		public void SetAdress(int adress, bool[] value)
		{
			for (int i = 0; i < datasize; i++)
			{
				if (debug)
				{
					Console.WriteLine("Set value {0} in slot 0x{1} at adress 0x{2}", value[i], i.ToString("X"), adress.ToString("X"));
				}
				register[i, adress] = value[i];
			}
		}

		public bool[] GetAdress(int Adress)
		{
			bool[] o = new bool[datasize];
			for (int i = 0; i < datasize; i++)
			{
				o[i] = register[i, Adress];
			}
			return o;
		}

		public void ShiftRight()
		{
			bool[] first = EmptyArray();
			for (int i = register.GetUpperBound(1); i >= 0; i--)
			{
				if (i < 0)
				{
					break;
				}
				if (loop && i == register.GetUpperBound(1))
				{
					first = GetAdress(i);
					SetAdress(i, GetAdress(i - 1));
				}//got first now its time to shift
				else if (i > 0)
				{
					SetAdress(i, GetAdress(i - 1));
				}
				else if (i == 0)
				{
					SetAdress(0, first);
				}
			}
		}

		public void ShiftLeft()
		{
			bool[] first = EmptyArray();
			for (int i = 0; i <= register.GetUpperBound(1); i++)
			{
				if (i > register.GetUpperBound(1))
				{
					break;
				}
				if (loop && i == 0)
				{
					first = GetAdress(i);
					SetAdress(register.GetUpperBound(1), GetAdress(i + 1));
				}//got first now its time to shift
				else if (i < register.GetUpperBound(1))
				{
					SetAdress(i, GetAdress(i + 1));
				}
				else if (i == register.GetUpperBound(1))
				{
					SetAdress(register.GetUpperBound(1), first);
				}
			}
		}

		public void ResetAdress(int Adress)
		{
			for (int i = 0; i < datasize; i++)
			{
				register[Adress, i] = false;
			}
		}
	}
}