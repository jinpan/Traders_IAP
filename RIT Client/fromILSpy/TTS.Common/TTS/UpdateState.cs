using ProtoBuf;
using System;
using System.Collections.Generic;
namespace TTS
{
	[ProtoContract]
	public class UpdateState
	{
		[ProtoMember(1)]
		public int ID
		{
			get;
			set;
		}
		[ProtoMember(12)]
		public int Period
		{
			get;
			set;
		}
		[ProtoMember(13)]
		public int Tick
		{
			get;
			set;
		}
		[ProtoMember(2)]
		public System.Collections.Generic.List<OrderAddMessage> AddedOrders
		{
			get;
			set;
		}
		[ProtoMember(3)]
		public System.Collections.Generic.List<OrderUpdateMessage> UpdatedOrders
		{
			get;
			set;
		}
		[ProtoMember(4)]
		public System.Collections.Generic.List<int> CancelledOrders
		{
			get;
			set;
		}
		[ProtoMember(5)]
		public System.Collections.Generic.List<AssetUpdateMessage> UpdatedAssets
		{
			get;
			set;
		}
		[ProtoMember(6)]
		public System.Collections.Generic.List<int> UnleasedAssets
		{
			get;
			set;
		}
		[ProtoMember(7)]
		public System.Collections.Generic.Dictionary<string, decimal> UpdatedLast
		{
			get;
			set;
		}
		[ProtoMember(15)]
		public System.Collections.Generic.Dictionary<string, decimal> UpdatedVolume
		{
			get;
			set;
		}
		[ProtoMember(20)]
		public System.Collections.Generic.Dictionary<string, decimal> UpdatedVWAP
		{
			get;
			set;
		}
		[ProtoMember(8)]
		public System.Collections.Generic.Dictionary<string, PortfolioUpdateMessage> UpdatedPortfolio
		{
			get;
			set;
		}
		[ProtoMember(14)]
		public System.Collections.Generic.Dictionary<string, TradingLimitUpdateMessage> UpdatedTradingLimits
		{
			get;
			set;
		}
		[ProtoMember(9)]
		public System.Collections.Generic.List<TransactionAddMessage> AddedTransactions
		{
			get;
			set;
		}
		[ProtoMember(10)]
		public decimal? UpdatedNLV
		{
			get;
			set;
		}
		[ProtoMember(11)]
		public System.Collections.Generic.Dictionary<string, decimal> UpdatedSecurityNLV
		{
			get;
			set;
		}
		[ProtoMember(30)]
		public System.Collections.Generic.List<Tuple<int, double>> UpdatedElectricity
		{
			get;
			set;
		}
		[ProtoMember(16)]
		public System.Collections.Generic.List<OTCUpdateMessage> UpdatedOTC
		{
			get;
			set;
		}
		public UpdateState()
		{
		}
		public UpdateState(int id)
		{
			this.ID = id;
			this.AddedOrders = new System.Collections.Generic.List<OrderAddMessage>();
			this.UpdatedOrders = new System.Collections.Generic.List<OrderUpdateMessage>();
			this.CancelledOrders = new System.Collections.Generic.List<int>();
			this.UpdatedAssets = new System.Collections.Generic.List<AssetUpdateMessage>();
			this.UnleasedAssets = new System.Collections.Generic.List<int>();
			this.UpdatedLast = new System.Collections.Generic.Dictionary<string, decimal>();
			this.UpdatedVolume = new System.Collections.Generic.Dictionary<string, decimal>();
			this.UpdatedVWAP = new System.Collections.Generic.Dictionary<string, decimal>();
			this.UpdatedPortfolio = new System.Collections.Generic.Dictionary<string, PortfolioUpdateMessage>();
			this.UpdatedTradingLimits = new System.Collections.Generic.Dictionary<string, TradingLimitUpdateMessage>();
			this.AddedTransactions = new System.Collections.Generic.List<TransactionAddMessage>();
			this.UpdatedElectricity = new System.Collections.Generic.List<Tuple<int, double>>();
			this.UpdatedOTC = new System.Collections.Generic.List<OTCUpdateMessage>();
		}
	}
}
