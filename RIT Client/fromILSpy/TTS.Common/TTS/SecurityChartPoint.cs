using ProtoBuf;
using System;
using System.ComponentModel;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	public class SecurityChartPoint : ChartPoint
	{
		[ProtoMember(1)]
		public double Open
		{
			get;
			set;
		}
		[ProtoMember(2)]
		public double High
		{
			get;
			set;
		}
		[ProtoMember(3)]
		public double Low
		{
			get;
			set;
		}
		[ProtoMember(4)]
		public double Close
		{
			get;
			set;
		}
		public SecurityChartPoint()
		{
		}
		public SecurityChartPoint(double open, double high, double low, double close)
		{
			this.Open = open;
			this.High = high;
			this.Low = low;
			this.Close = close;
		}
		public SecurityChartPoint(decimal open, decimal high, decimal low, decimal close)
		{
			this.Open = System.Convert.ToDouble(open);
			this.High = System.Convert.ToDouble(high);
			this.Low = System.Convert.ToDouble(low);
			this.Close = (double)System.Convert.ToUInt16(close);
		}
		public SecurityChartPoint(double price)
		{
			this.Close = price;
			this.Low = price;
			this.High = price;
			this.Open = price;
		}
		public SecurityChartPoint(decimal price)
		{
			this.Open = (this.High = (this.Low = (this.Close = System.Convert.ToDouble(price))));
		}
		public override void Merge(ChartPoint point)
		{
			SecurityChartPoint securityChartPoint = (SecurityChartPoint)point;
			if (securityChartPoint.High > this.High)
			{
				this.High = securityChartPoint.High;
			}
			if (securityChartPoint.Low < this.Low)
			{
				this.Low = securityChartPoint.Low;
			}
			this.Close = securityChartPoint.Close;
		}
		public override ChartPoint Clone(ChartPoint.CloneType type = ChartPoint.CloneType.COPY)
		{
			switch (type)
			{
			case ChartPoint.CloneType.FORWARD:
				return new SecurityChartPoint(this.Close);
			case ChartPoint.CloneType.BACKWARD:
				return new SecurityChartPoint(this.Open);
			default:
				return new SecurityChartPoint(this.Open, this.High, this.Low, this.Close);
			}
		}
	}
}
