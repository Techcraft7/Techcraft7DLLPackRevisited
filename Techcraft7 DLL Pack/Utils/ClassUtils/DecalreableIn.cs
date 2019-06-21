using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Techcraft7_DLL_Pack.Utils.ClassUtils
{
	/// <summary>
	/// Only allows children to be declared in certain types.. subclasses allowed for multiple classes that can declare the child object
	/// </summary>
	/// <typeparam name="T">The type that is allowed to declare the child object</typeparam>
	public class DecalreableIn<T>
	{
		/// <summary>
		/// Constructor... run CheckDeclaration(); in your constructor to check for illegal declarations!
		/// </summary>
		public DecalreableIn()
		{
			CheckDeclaration();
		}

		public void CheckDeclaration()
		{
			if (GetType() != typeof(T))
			{
				throw new InvalidOperationException(string.Format("Attempted to declare a {0} outside of {1}", GetType(), typeof(T)));
			}
		}
	}
}
