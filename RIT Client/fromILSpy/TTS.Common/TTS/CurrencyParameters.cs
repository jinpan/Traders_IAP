using ProtoBuf;
using System;
using System.ComponentModel;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	public class CurrencyParameters
	{
		[ProtoMember(1), DefaultValue("")]
		public string Ticker
		{
			get;
			set;
		}
		[ProtoMember(2), DefaultValue("")]
		public string Description
		{
			get;
			set;
		}
		[ProtoMember(3), DefaultValue("")]
		public string DisplayUnit
		{
			get;
			set;
		}
		[ProtoMember(4), DefaultValue(2)]
		public int QuotedDecimals
		{
			get;
			set;
		}
		public string FormatString
		{
			get
			{
				return "#,0." + new string('0', this.QuotedDecimals);
			}
		}
		public CurrencyParameters()
		{
			this.SetDefaultValues();
		}
	}
}
