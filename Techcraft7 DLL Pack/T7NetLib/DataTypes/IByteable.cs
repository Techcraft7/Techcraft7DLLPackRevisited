namespace Techcraft7_DLL_Pack.T7NetLib.DataTypes
{
	/// <summary>
	/// Represents an object that can be converted to bytes and back
	/// </summary>
	public interface IByteable<T>
	{
		/// <summary>
		/// Get the size of the object
		/// </summary>
		/// <returns>The size of the object</returns>
		int GetSize();
		/// <summary>
		/// Reads a <typeparamref name="T"/> from <paramref name="bs"/>
		/// </summary>
		/// <param name="bs">Bytes to read from</param>
		/// <returns>Instance of <typeparamref name="T"/></returns>
		T Get(byte[] bs);

		/// <summary>
		/// Get the bytes of the current object
		/// </summary>
		/// <returns>Bytes of the current object</returns>
		byte[] GetBytes();
	}
}