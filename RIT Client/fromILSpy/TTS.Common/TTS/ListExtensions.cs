using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Reflection;
namespace TTS
{
	public static class ListExtensions
	{
		public static void Merge<T>(this BindingList<T> list, System.Collections.Generic.List<T> otherlist)
		{
			System.Collections.Generic.List<T> list2 = new System.Collections.Generic.List<T>();
			foreach (T current in list)
			{
				if (!otherlist.Contains(current))
				{
					list2.Add(current);
				}
			}
			foreach (T current2 in list2)
			{
				list.Remove(current2);
			}
			foreach (T current3 in otherlist)
			{
				if (!list.Contains(current3))
				{
					list.Add(current3);
				}
			}
		}
		public static System.Collections.Generic.Dictionary<T, U> Clone<T, U>(this System.Collections.Generic.Dictionary<T, U> dictionary)
		{
			System.Collections.Generic.Dictionary<T, U> dictionary2 = new System.Collections.Generic.Dictionary<T, U>();
			foreach (System.Collections.Generic.KeyValuePair<T, U> current in dictionary)
			{
				dictionary2.Add(current.Key, current.Value);
			}
			return dictionary2;
		}
		public static DataTable ToDataTable<T>(this System.Collections.Generic.List<T> list, string tablename)
		{
			DataTable dataTable = new DataTable(tablename);
			System.Reflection.PropertyInfo[] properties = typeof(T).GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
			System.Reflection.PropertyInfo[] array = properties;
			for (int i = 0; i < array.Length; i++)
			{
				System.Reflection.PropertyInfo propertyInfo = array[i];
				System.Type coreType = ListExtensions.GetCoreType(propertyInfo.PropertyType);
				dataTable.Columns.Add(propertyInfo.Name, coreType);
			}
			foreach (T current in list)
			{
				object[] array2 = new object[properties.Length];
				for (int j = 0; j < properties.Length; j++)
				{
					array2[j] = properties[j].GetValue(current, null);
				}
				dataTable.Rows.Add(array2);
			}
			return dataTable;
		}
		private static bool IsNullable(System.Type t)
		{
			return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(System.Nullable<>));
		}
		private static System.Type GetCoreType(System.Type t)
		{
			if (!(t != null) || !ListExtensions.IsNullable(t))
			{
				return t;
			}
			if (!t.IsValueType)
			{
				return t;
			}
			return System.Nullable.GetUnderlyingType(t);
		}
	}
}
