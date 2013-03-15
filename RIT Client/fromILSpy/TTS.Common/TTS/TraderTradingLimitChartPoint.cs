using ProtoBuf;
using System;
using System.ComponentModel;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	public class TraderTradingLimitChartPoint : ChartPoint
	{
		[ProtoMember(1)]
		public double Gross
		{
			get;
			set;
		}
		[ProtoMember(2)]
		public double Net
		{
			get;
			set;
		}
		public TraderTradingLimitChartPoint()
		{
		}
		public TraderTradingLimitChartPoint(decimal gross, decimal net)
		{
			this.Gross = System.Convert.ToDouble(gross);
			this.Net = System.Convert.ToDouble(net);
		}
		public TraderTradingLimitChartPoint(double gross, double net)
		{
			this.Gross = gross;
			this.Net = net;
		}
		public override void Merge(ChartPoint point)
		{
			TraderTradingLimitChartPoint traderTradingLimitChartPoint = (TraderTradingLimitChartPoint)point;
			this.Gross = traderTradingLimitChartPoint.Gross;
			this.Net = traderTradingLimitChartPoint.Net;
		}
		public override ChartPoint Clone(ChartPoint.CloneType type = ChartPoint.CloneType.COPY)
		{
			return new TraderTradingLimitChartPoint(this.Gross, this.Net);
		}
	}
}
