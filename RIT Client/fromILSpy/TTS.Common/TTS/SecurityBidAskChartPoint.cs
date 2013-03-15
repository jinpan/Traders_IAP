using ProtoBuf;
using System;
using System.ComponentModel;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	public class SecurityBidAskChartPoint : ChartPoint
	{
		[ProtoMember(1)]
		public double Bid
		{
			get;
			set;
		}
		[ProtoMember(2)]
		public double Ask
		{
			get;
			set;
		}
		public SecurityBidAskChartPoint()
		{
		}
		public SecurityBidAskChartPoint(double bid, double ask)
		{
			this.Bid = bid;
			this.Ask = ask;
		}
		public SecurityBidAskChartPoint(decimal bid, decimal ask)
		{
			this.Bid = System.Convert.ToDouble(bid);
			this.Ask = System.Convert.ToDouble(ask);
		}
		public override void Merge(ChartPoint point)
		{
			SecurityBidAskChartPoint securityBidAskChartPoint = (SecurityBidAskChartPoint)point;
			this.Bid = securityBidAskChartPoint.Bid;
			this.Ask = securityBidAskChartPoint.Ask;
		}
		public override ChartPoint Clone(ChartPoint.CloneType type = ChartPoint.CloneType.COPY)
		{
			return new SecurityBidAskChartPoint(this.Bid, this.Ask);
		}
	}
}
