using ProtoBuf;
using System;
using System.ComponentModel;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	public class GeneralParametersUpdate
	{
		[ProtoMember(4)]
		public int MarkToMarketsPerPeriod
		{
			get;
			set;
		}
		[ProtoMember(5)]
		public bool IsSecuredLogin
		{
			get;
			set;
		}
		[ProtoMember(6)]
		public bool IsEnforceTradingLimits
		{
			get;
			set;
		}
		[ProtoMember(7)]
		public int TicksPerTenderOffer
		{
			get;
			set;
		}
		[ProtoMember(8)]
		public WindowType AllowedWindows
		{
			get;
			set;
		}
	}
}
