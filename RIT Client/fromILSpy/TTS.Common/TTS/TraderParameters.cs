using ProtoBuf;
using System;
using System.ComponentModel;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	public class TraderParameters
	{
		[ProtoMember(1), DefaultValue("")]
		public string TraderID
		{
			get;
			set;
		}
		[ProtoMember(2), DefaultValue("")]
		public string FirstName
		{
			get;
			set;
		}
		[ProtoMember(3), DefaultValue("")]
		public string LastName
		{
			get;
			set;
		}
		[ProtoMember(4), DefaultValue("")]
		public string Password
		{
			get;
			set;
		}
		[ProtoMember(5), DefaultValue("")]
		public string Type
		{
			get;
			set;
		}
		public TraderParameters()
		{
			this.SetDefaultValues();
		}
	}
}
