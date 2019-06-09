using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Techcraft7_DLL_Pack.Collections
{

	public class MultiTypeList
	{
		private Dictionary<Type, List<MultiTypeListObject>> dict = new Dictionary<Type, List<MultiTypeListObject>>();

		public void AddTypeList(Type T)
		{
			if (dict.ContainsKey(T))
			{
				throw new InvalidTypeException("Type List " + T.ToString() + " already exists!");
			}
			else
			{
				dict.Add(T, new List<MultiTypeListObject>());
			}
		}

		public void RemoveObject(MultiTypeListObject obj)
		{
			dict[obj.obj.GetType()].Remove(obj);
		}

		public void RemoveObjectList(Type ListType)
		{
			dict.Remove(ListType);
		}

		public void AddTypeList(Type T, List<MultiTypeListObject> list)
		{
			if (dict.ContainsKey(T))
			{
				throw new InvalidTypeException("Type " + T.ToString() + " already exists!");
			}
			else
			{
				if (CheckList(list) == false)
				{
					throw new ArgumentException("List contained types that werent all " + T.ToString());
				}
				dict.Add(T, list);
			}
		}

		public void AddObject(Type T, MultiTypeListObject obj)
		{
			dict[T].Add(obj);
			if (CheckList(dict[T]) == false)
			{
				dict[T].Remove(obj);
				throw new ArgumentException("Attemted to add a " + obj.obj.GetType().ToString() + " to a list of " + T.ToString());
			}
		}

		private bool CheckList(List<MultiTypeListObject> list)
		{
			bool pass = true;
			foreach (MultiTypeListObject x in list)
			{
				foreach (MultiTypeListObject y in list)
				{
					if (x.obj.GetType() != y.obj.GetType())
					{
						pass = false;
					}
				}
			}
			return pass;
		}

		public object this[int i, Type T]
		{
			get
			{
				try
				{
					return dict[T][i].obj;
				}
				catch (Exception e)
				{
					Console.WriteLine("Error getting list item returning null...\n" + e.GetType().ToString() + ": " + e.Message);
					return null;
				}
			}
		}
	}
}
