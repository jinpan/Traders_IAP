using ProtoBuf;
using System;
using System.ComponentModel;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	public class AssetParameters
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
		[ProtoMember(3), DefaultValue(AssetType.CONTAINER)]
		public AssetType Type
		{
			get;
			set;
		}
		[ProtoMember(4), DefaultValue(2147483647)]
		public int TotalQuantity
		{
			get;
			set;
		}
		[ProtoMember(5), DefaultValue(1)]
		public int StartPeriod
		{
			get;
			set;
		}
		[ProtoMember(6), DefaultValue(1)]
		public int StopPeriod
		{
			get;
			set;
		}
		[ProtoMember(7), DefaultValue("")]
		public string Currency
		{
			get;
			set;
		}
		[ProtoMember(8), DefaultValue(0)]
		public decimal LeasePrice
		{
			get;
			set;
		}
		[ProtoMember(20), DefaultValue(0)]
		public decimal VariableLeasePrice
		{
			get;
			set;
		}
		[ProtoMember(9), DefaultValue(0)]
		public int TicksPerLease
		{
			get;
			set;
		}
		[ProtoMember(10)]
		public TickerWeight Containment
		{
			get;
			set;
		}
		[ProtoMember(11)]
		public TickerWeight[] UsageCost
		{
			get;
			set;
		}
		[ProtoMember(12)]
		public TickerWeight[] ConvertFrom
		{
			get;
			set;
		}
		[ProtoMember(13)]
		public TickerWeight[] ConvertTo
		{
			get;
			set;
		}
		[ProtoMember(15), DefaultValue(0)]
		public int TicksPerConversion
		{
			get;
			set;
		}
		[ProtoMember(16), DefaultValue(false)]
		public bool IsAllowBackhaul
		{
			get;
			set;
		}
		[ProtoMember(17), DefaultValue("")]
		public string DisplayCost
		{
			get;
			set;
		}
		[ProtoMember(18), DefaultValue(2147483647)]
		public int LeaseLimit
		{
			get;
			set;
		}
		[ProtoMember(19), DefaultValue(1)]
		public int DistressedMultiplier
		{
			get;
			set;
		}
		public AssetParameters()
		{
			this.SetDefaultValues();
		}
	}
}
