using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techcraft7_DLL_Pack.HardwareEmulation.Registers
{
    public class CPURegister8Bit
    {
        private byte val;
        public byte Value
        {
            get => val;
            set { }
        }
    }

    public class CPURegister16Bit
    {
        private ushort val;
        public ushort Value
        {
            get => val;
            set { }
        }
    }

    public class CPURegister32Bit
    {
        private uint val;
        public uint Value
        {
            get => val;
            set { }
        }
    }

    public class CPURegister64Bit
    {
        private ulong val;
        public ulong Value
        {
            get => val;
            set { }
        }
    }
}
