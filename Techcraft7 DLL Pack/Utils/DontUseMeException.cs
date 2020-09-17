using System;

namespace Techcraft7_DLL_Pack.Utils
{
	[Serializable]
	class DontUseMeException : Exception
	{
		public DontUseMeException() : base("Don't use this method!")
		{
		}
	}
}