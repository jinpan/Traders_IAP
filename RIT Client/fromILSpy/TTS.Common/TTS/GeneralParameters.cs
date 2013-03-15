using ProtoBuf;
using System;
using System.ComponentModel;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	public class GeneralParameters
	{
		[ProtoMember(1), DefaultValue(1)]
		public int Periods
		{
			get;
			set;
		}
		[ProtoMember(2), DefaultValue(600)]
		public int TicksPerPeriod
		{
			get;
			set;
		}
		[ProtoMember(3), DefaultValue(600)]
		public int TicksPerYear
		{
			get;
			set;
		}
		[ProtoMember(4), DefaultValue(20)]
		public int MarkToMarketsPerPeriod
		{
			get;
			set;
		}
		[ProtoMember(5), DefaultValue(true)]
		public bool IsSecuredLogin
		{
			get;
			set;
		}
		[ProtoMember(6), DefaultValue(true)]
		public bool IsEnforceTradingLimits
		{
			get;
			set;
		}
		[ProtoMember(7), DefaultValue(30)]
		public int TicksPerTenderOffer
		{
			get;
			set;
		}
		[ProtoMember(8), DefaultValue("")]
		public string GameName
		{
			get;
			set;
		}
		[ProtoMember(10)]
		public bool IsAnonymousTrading
		{
			get;
			set;
		}
		[ProtoMember(11)]
		public bool IsRTDEnabled
		{
			get;
			set;
		}
		[ProtoMember(12)]
		public bool IsAPIEnabled
		{
			get;
			set;
		}
		[ProtoMember(15), DefaultValue(5)]
		public int APIOrdersPerSecond
		{
			get;
			set;
		}
		[ProtoMember(13)]
		public int OrderBookLimit
		{
			get;
			set;
		}
		[ProtoMember(14)]
		public int StopLossTradingLimit
		{
			get;
			set;
		}
		[ProtoMember(16), DefaultValue("")]
		public string DefaultCurrency
		{
			get;
			set;
		}
		[ProtoMember(17), DefaultValue("")]
		public string SimulationFileLocation
		{
			get;
			set;
		}
		[ProtoMember(18), DefaultValue("")]
		public string UserFileLocation
		{
			get;
			set;
		}
		[ProtoMember(9, DataFormat = DataFormat.TwosComplement), DefaultValue(WindowType.NONE)]
		public WindowType AllowedWindows
		{
			get;
			set;
		}
		[ProtoMember(20), DefaultValue("")]
		public string DefaultWorkspace
		{
			get;
			set;
		}
		public GeneralParameters()
		{
			this.SetDefaultValues();
		}
	}
}
