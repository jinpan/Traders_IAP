using ProtoBuf;
using System;
using System.ComponentModel;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	public class TraderSecurityChartPoint : ChartPoint
	{
		[ProtoMember(1)]
		public double Position
		{
			get;
			set;
		}
		[ProtoMember(2)]
		public double VWAP
		{
			get;
			set;
		}
		[ProtoMember(3)]
		public double NLV
		{
			get;
			set;
		}
		[ProtoMember(4)]
		public double Realized
		{
			get;
			set;
		}
		[ProtoMember(5)]
		public double Unrealized
		{
			get;
			set;
		}
		public TraderSecurityChartPoint()
		{
		}
		public TraderSecurityChartPoint(double position, double vwap, double nlv, double realized, double unrealized)
		{
			this.Position = position;
			this.VWAP = vwap;
			this.NLV = nlv;
			this.Realized = realized;
			this.Unrealized = unrealized;
		}
		public TraderSecurityChartPoint(decimal position, decimal vwap, decimal nlv, decimal realized, decimal unrealized)
		{
			this.Position = System.Convert.ToDouble(position);
			this.VWAP = System.Convert.ToDouble(vwap);
			this.NLV = System.Convert.ToDouble(nlv);
			this.Realized = System.Convert.ToDouble(realized);
			this.Unrealized = System.Convert.ToDouble(unrealized);
		}
		public override void Merge(ChartPoint point)
		{
			TraderSecurityChartPoint traderSecurityChartPoint = (TraderSecurityChartPoint)point;
			this.Position = traderSecurityChartPoint.Position;
			this.VWAP = traderSecurityChartPoint.VWAP;
			this.NLV = traderSecurityChartPoint.NLV;
			this.Realized = traderSecurityChartPoint.Realized;
			this.Unrealized = traderSecurityChartPoint.Unrealized;
		}
		public override ChartPoint Clone(ChartPoint.CloneType type = ChartPoint.CloneType.COPY)
		{
			return new TraderSecurityChartPoint(this.Position, this.VWAP, this.NLV, this.Realized, this.Unrealized);
		}
	}
}
