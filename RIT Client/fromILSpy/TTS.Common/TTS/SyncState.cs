using ProtoBuf;
using System;
using System.Collections.Generic;
namespace TTS
{
	[ProtoContract]
	public class SyncState
	{
		[ProtoMember(1)]
		public GameParameters Current
		{
			get;
			set;
		}
		[ProtoMember(2)]
		public GeneralParameters General
		{
			get;
			set;
		}
		[ProtoMember(3)]
		public SecurityParameters[] Securities
		{
			get;
			set;
		}
		[ProtoMember(4)]
		public System.Collections.Generic.Dictionary<string, byte[]> OrderTables
		{
			get;
			set;
		}
		[ProtoMember(5)]
		public CurrencyParameters[] CurrencyParameters
		{
			get;
			set;
		}
		[ProtoMember(6)]
		public TraderTypeParameters TraderType
		{
			get;
			set;
		}
		[ProtoMember(7)]
		public TraderParameters Trader
		{
			get;
			set;
		}
		[ProtoMember(8)]
		public byte[] OrderHistoryTable
		{
			get;
			set;
		}
		[ProtoMember(9)]
		public PortfolioUpdateMessage[] SecurityPortfolio
		{
			get;
			set;
		}
		[ProtoMember(10)]
		public byte[] TransactionTable
		{
			get;
			set;
		}
		[ProtoMember(11)]
		public TradingLimitUpdateMessage[] TradingLimits
		{
			get;
			set;
		}
		[ProtoMember(12)]
		public System.Collections.Generic.Dictionary<string, decimal> Attribution
		{
			get;
			set;
		}
		[ProtoMember(13)]
		public System.Collections.Generic.Dictionary<string, decimal> Accounts
		{
			get;
			set;
		}
		[ProtoMember(14)]
		public SpreadParameters[] Spreads
		{
			get;
			set;
		}
		[ProtoMember(15)]
		public AssetParameters[] Assets
		{
			get;
			set;
		}
		[ProtoMember(16)]
		public AssetItemUpdateMessage[] AssetLeaseCount
		{
			get;
			set;
		}
		[ProtoMember(17)]
		public byte[] NewsTable
		{
			get;
			set;
		}
		[ProtoMember(18)]
		public AssetUpdateMessage[] AssetPortfolio
		{
			get;
			set;
		}
		[ProtoMember(19)]
		public byte[] TraderChart
		{
			get;
			set;
		}
		[ProtoMember(20)]
		public System.Collections.Generic.Dictionary<string, byte[]> SecurityCharts
		{
			get;
			set;
		}
		[ProtoMember(21)]
		public RiskTypeParameters[] RiskTypes
		{
			get;
			set;
		}
		[ProtoMember(22)]
		public byte[] OTCOrderTable
		{
			get;
			set;
		}
		[ProtoMember(23)]
		public System.Collections.Generic.Dictionary<string, byte[]> VolumeCharts
		{
			get;
			set;
		}
		[ProtoMember(24)]
		public ElectricityChart[] ElectricityCharts
		{
			get;
			set;
		}
	}
}
