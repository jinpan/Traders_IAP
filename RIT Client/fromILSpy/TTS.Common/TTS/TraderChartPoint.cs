using ProtoBuf;
using System;
using System.ComponentModel;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	public class TraderChartPoint : ChartPoint
	{
		[ProtoMember(1)]
		public double NLV
		{
			get;
			set;
		}
		[ProtoMember(2)]
		public double Cash
		{
			get;
			set;
		}
		[ProtoMember(3)]
		public double VaR
		{
			get;
			set;
		}
		public TraderChartPoint()
		{
		}
		public TraderChartPoint(decimal nlv, decimal cash, decimal var)
		{
			this.NLV = System.Convert.ToDouble(nlv);
			this.Cash = System.Convert.ToDouble(cash);
			this.VaR = System.Convert.ToDouble(var);
		}
		public TraderChartPoint(double nlv, double cash, double vaR)
		{
			this.NLV = nlv;
			this.Cash = cash;
			this.VaR = vaR;
		}
		public override void Merge(ChartPoint point)
		{
			TraderChartPoint traderChartPoint = (TraderChartPoint)point;
			this.NLV = traderChartPoint.NLV;
			this.Cash = traderChartPoint.Cash;
			this.VaR = traderChartPoint.VaR;
		}
		public override ChartPoint Clone(ChartPoint.CloneType type = ChartPoint.CloneType.COPY)
		{
			return new TraderChartPoint(this.NLV, this.Cash, this.VaR);
		}
	}
}
