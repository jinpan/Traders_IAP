using ProtoBuf;
using System;
using System.ComponentModel;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	public class RiskTypeParameters
	{
		[ProtoMember(1)]
		public string Name
		{
			get;
			set;
		}
		[ProtoMember(2), DefaultValue(0)]
		public int GrossRiskLimit
		{
			get;
			set;
		}
		[ProtoMember(3), DefaultValue(0)]
		public decimal GrossRiskFine
		{
			get;
			set;
		}
		[ProtoMember(4), DefaultValue(0)]
		public int NetRiskLimit
		{
			get;
			set;
		}
		[ProtoMember(5), DefaultValue(0)]
		public decimal NetRiskFine
		{
			get;
			set;
		}
		[ProtoMember(6)]
		public string[] Tickers
		{
			get;
			set;
		}
		public RiskTypeParameters()
		{
			this.SetDefaultValues();
		}
	}
}
