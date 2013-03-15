using ProtoBuf;
using System;
using System.ComponentModel;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	public class SecurityVolumeChartPoint : ChartPoint
	{
		[ProtoMember(1)]
		public double Volume
		{
			get;
			set;
		}
		public SecurityVolumeChartPoint()
		{
		}
		public SecurityVolumeChartPoint(double volume)
		{
			this.Volume = volume;
		}
		public SecurityVolumeChartPoint(decimal volume)
		{
			this.Volume = System.Convert.ToDouble(volume);
		}
		public override void Merge(ChartPoint point)
		{
			SecurityVolumeChartPoint securityVolumeChartPoint = (SecurityVolumeChartPoint)point;
			this.Volume += securityVolumeChartPoint.Volume;
		}
		public override ChartPoint Clone(ChartPoint.CloneType type = ChartPoint.CloneType.COPY)
		{
			if (type == ChartPoint.CloneType.COPY)
			{
				return new SecurityVolumeChartPoint(this.Volume);
			}
			return new SecurityVolumeChartPoint(0.0);
		}
	}
}
