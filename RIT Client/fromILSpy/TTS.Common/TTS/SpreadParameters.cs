using ProtoBuf;
using System;
using System.ComponentModel;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	public class SpreadParameters
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
		[ProtoMember(3), DefaultValue(1)]
		public int StartPeriod
		{
			get;
			set;
		}
		[ProtoMember(4), DefaultValue(1)]
		public int StopPeriod
		{
			get;
			set;
		}
		[ProtoMember(5)]
		public TickerWeight[] UnderlyingTickers
		{
			get;
			set;
		}
		public SpreadParameters()
		{
			this.SetDefaultValues();
		}
	}
}
