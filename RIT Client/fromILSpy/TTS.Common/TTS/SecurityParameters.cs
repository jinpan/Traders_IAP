using ProtoBuf;
using System;
using System.ComponentModel;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	public class SecurityParameters
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
		[ProtoMember(3), DefaultValue(SecurityType.SPOT)]
		public SecurityType Type
		{
			get;
			set;
		}
		[ProtoMember(4), DefaultValue(1)]
		public int UnitMultiplier
		{
			get;
			set;
		}
		[ProtoMember(5), DefaultValue("")]
		public string DisplayUnit
		{
			get;
			set;
		}
		[ProtoMember(6)]
		public bool IsTradeable
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
		[ProtoMember(8), DefaultValue(1)]
		public int StartPeriod
		{
			get;
			set;
		}
		[ProtoMember(9), DefaultValue(1)]
		public int StopPeriod
		{
			get;
			set;
		}
		[ProtoMember(10), DefaultValue(0)]
		public decimal StartPrice
		{
			get;
			set;
		}
		[ProtoMember(11), DefaultValue(0)]
		public decimal MinPrice
		{
			get;
			set;
		}
		[ProtoMember(12), DefaultValue(1000000000)]
		public decimal MaxPrice
		{
			get;
			set;
		}
		[ProtoMember(13), DefaultValue(2)]
		public int QuotedDecimals
		{
			get;
			set;
		}
		[ProtoMember(14)]
		public bool IsShortAllowed
		{
			get;
			set;
		}
		[ProtoMember(15), DefaultValue(0)]
		public decimal TradingFee
		{
			get;
			set;
		}
		[ProtoMember(24), DefaultValue(0)]
		public decimal LimitOrderRebate
		{
			get;
			set;
		}
		[ProtoMember(16), DefaultValue(TradingFeeType.PER_UNIT)]
		public TradingFeeType TradingFeeType
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
		[ProtoMember(18), DefaultValue("")]
		public string Currency
		{
			get;
			set;
		}
		[ProtoMember(19)]
		public string RequiredTicker
		{
			get;
			set;
		}
		[ProtoMember(20)]
		public TickerWeight[] UnderlyingTickers
		{
			get;
			set;
		}
		[ProtoMember(21), DefaultValue("")]
		public string RiskType
		{
			get;
			set;
		}
		[ProtoMember(22), DefaultValue(1)]
		public decimal RiskUnit
		{
			get;
			set;
		}
		[ProtoMember(23), DefaultValue(0)]
		public decimal DistressedSettlement
		{
			get;
			set;
		}
		[ProtoMember(30), DefaultValue(0)]
		public decimal BondCoupon
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
		[ProtoMember(32), DefaultValue(0)]
		public int InterestPaymentsPerPeriod
		{
			get;
			set;
		}
		[ProtoMember(33), DefaultValue(null)]
		public string BaseSecurity
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
		public int VolumeDecimals
		{
			get
			{
				if (this.Type != SecurityType.CURRENCY)
				{
					return 0;
				}
				return this.QuotedDecimals;
			}
		}
		public string VolumeFormatString
		{
			get
			{
				return "#,0." + new string('0', this.VolumeDecimals);
			}
		}
		public decimal Increment
		{
			get
			{
				return new decimal(System.Math.Pow(10.0, (double)(-1 * this.QuotedDecimals)));
			}
		}
		public SecurityParameters()
		{
			this.SetDefaultValues();
		}
	}
}
