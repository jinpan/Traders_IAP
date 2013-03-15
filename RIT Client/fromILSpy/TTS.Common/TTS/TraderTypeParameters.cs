using ProtoBuf;
using System;
using System.ComponentModel;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	public class TraderTypeParameters
	{
		[ProtoMember(1), DefaultValue("")]
		public string Name
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
		[ProtoMember(3), DefaultValue(new string[]
		{

		})]
		public string[] ExcludedTickers
		{
			get;
			set;
		}
		[ProtoMember(4)]
		public TickerWeight[] StartingTickers
		{
			get;
			set;
		}
		[ProtoMember(5), DefaultValue(0)]
		public decimal StartingBalance
		{
			get;
			set;
		}
		public TraderTypeParameters()
		{
			this.SetDefaultValues();
			this.StartingTickers = new TickerWeight[0];
		}
	}
}
