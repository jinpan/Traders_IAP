using ProtoBuf;
using System;
using System.ComponentModel;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	public class TraderAssetChartPoint : ChartPoint
	{
		[ProtoMember(1)]
		public double Realized
		{
			get;
			set;
		}
		public TraderAssetChartPoint()
		{
		}
		public TraderAssetChartPoint(double realized)
		{
			this.Realized = realized;
		}
		public TraderAssetChartPoint(decimal realized)
		{
			this.Realized = System.Convert.ToDouble(realized);
		}
		public override void Merge(ChartPoint point)
		{
			TraderAssetChartPoint traderAssetChartPoint = (TraderAssetChartPoint)point;
			this.Realized = traderAssetChartPoint.Realized;
		}
		public override ChartPoint Clone(ChartPoint.CloneType type = ChartPoint.CloneType.COPY)
		{
			return new TraderAssetChartPoint(this.Realized);
		}
	}
}
