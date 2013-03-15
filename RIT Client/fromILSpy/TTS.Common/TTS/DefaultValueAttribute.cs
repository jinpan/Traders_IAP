using System;
namespace TTS
{
	[System.AttributeUsage(System.AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
	internal sealed class DefaultValueAttribute : System.Attribute
	{
		public object DefaultValue
		{
			get;
			set;
		}
		public DefaultValueAttribute(object value)
		{
			this.DefaultValue = value;
		}
	}
}
