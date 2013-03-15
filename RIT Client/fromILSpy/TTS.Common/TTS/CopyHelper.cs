using System;
using System.Reflection;
namespace TTS
{
	public static class CopyHelper
	{
		public static void CopyTo<T, U>(this T source, U target)
		{
			System.Reflection.PropertyInfo[] properties = typeof(T).GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				System.Reflection.PropertyInfo propertyInfo = properties[i];
				System.Reflection.PropertyInfo property = typeof(U).GetProperty(propertyInfo.Name);
				if (!(property == null))
				{
					System.Reflection.MethodInfo setMethod = property.GetSetMethod();
					System.Reflection.MethodInfo getMethod = propertyInfo.GetGetMethod();
					if (setMethod != null && getMethod != null)
					{
						setMethod.Invoke(target, new object[]
						{
							getMethod.Invoke(source, null)
						});
					}
				}
			}
		}
		public static void CopyFrom<T, U>(this T target, U source)
		{
			System.Reflection.PropertyInfo[] properties = typeof(T).GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				System.Reflection.PropertyInfo propertyInfo = properties[i];
				System.Reflection.PropertyInfo property = typeof(U).GetProperty(propertyInfo.Name);
				if (!(property == null))
				{
					System.Reflection.MethodInfo setMethod = propertyInfo.GetSetMethod();
					System.Reflection.MethodInfo getMethod = property.GetGetMethod();
					if (setMethod != null && getMethod != null)
					{
						setMethod.Invoke(target, new object[]
						{
							getMethod.Invoke(source, null)
						});
					}
				}
			}
		}
	}
}
