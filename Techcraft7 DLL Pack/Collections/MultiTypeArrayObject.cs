using System;

namespace Techcraft7_DLL_Pack.Collections
{
	public class MultiTypeListObject
	{
		public object obj;
		private Type T;

		public MultiTypeListObject(object obj)
		{
			this.obj = obj;
			T = obj.GetType();
		}
	}
}