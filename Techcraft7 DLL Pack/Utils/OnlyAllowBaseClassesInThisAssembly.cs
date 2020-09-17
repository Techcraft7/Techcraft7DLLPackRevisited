using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Techcraft7_DLL_Pack.Utils
{
	public class OnlyAllowBaseClassesInThisAssembly
	{
		public OnlyAllowBaseClassesInThisAssembly()
		{
			//Get the assembly of this object
			Assembly current = GetType().Assembly;
			if (GetType().Equals(typeof(OnlyAllowBaseClassesInThisAssembly)))
			{
				throw new InvalidOperationException("You cannot instantiate this class!");
			}
			//If class is a direct child of OnlyAllowBaseClassesInThisAssembly we ignore it
			if (GetType().BaseType.Equals(typeof(OnlyAllowBaseClassesInThisAssembly)))
			{
				return;
			}
			//Compare it to the assembly of the base type
			if (!current.Equals(GetType().BaseType.Assembly))
			{
				throw new InvalidOperationException($"You cannot create a child class of {GetType().BaseType} because it only allows");
			}
		}
	}
}

namespace Techcraft7_DLL_Pack.Utils
{
}