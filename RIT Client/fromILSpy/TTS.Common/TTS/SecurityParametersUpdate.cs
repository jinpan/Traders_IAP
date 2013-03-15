using ProtoBuf;
using System;
using System.ComponentModel;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	public class SecurityParametersUpdate
	{
		[ProtoMember(1)]
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
		[ProtoMember(6), DefaultValue(true)]
		public bool IsTradeable
		{
			get;
			set;
		}
		[ProtoMember(14), DefaultValue(true)]
		public bool IsShortAllowed
		{
			get;
			set;
		}
		[ProtoMember(17), DefaultValue(2147483647)]
		public int MaxTradeSize
		{
			get;
			set;
		}
		[ProtoMember(7)]
		public bool IsFollowPath
		{
			get;
			set;
		}
		[ProtoMember(31), DefaultValue(0)]
		public decimal InterestRate
		{
			get;
			set;
		}
	}
}
