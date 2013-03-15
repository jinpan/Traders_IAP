using System;
using System.Reflection;
namespace TTS
{
	public static class DefaultValueAttributeHelper
	{
		public static void SetDefaultValues(this object obj)
		{
			System.Type type = obj.GetType();
			System.Reflection.PropertyInfo[] properties = type.GetProperties();
			for (int i = 0; i < properties.Length; i++)
			{
				System.Reflection.PropertyInfo propertyInfo = properties[i];
				object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(DefaultValueAttribute), true);
				for (int j = 0; j < customAttributes.Length; j++)
				{
					object obj2 = customAttributes[j];
					DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)obj2;
					propertyInfo.SetValue(obj, System.Convert.ChangeType(defaultValueAttribute.DefaultValue, propertyInfo.PropertyType), null);
				}
			}
		}
	}
}
