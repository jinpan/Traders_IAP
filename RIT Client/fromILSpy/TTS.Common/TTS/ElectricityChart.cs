using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	public class ElectricityChart
	{
		public event System.Action<double> ChartUpdated = delegate
		{
		};
		[ProtoMember(1)]
		public string Name
		{
			get;
			set;
		}
		[ProtoMember(2)]
		public bool IsShow
		{
			get;
			set;
		}
		[ProtoMember(3)]
		public System.Collections.Generic.List<double> Data
		{
			get;
			set;
		}
		[ProtoMember(4)]
		public int Period
		{
			get;
			set;
		}
		public ElectricityChart()
		{
			this.Data = new System.Collections.Generic.List<double>();
		}
		public override string ToString()
		{
			return string.Format("{0}: {1}", this.Period, this.Name);
		}
		public void AddPoint(double point)
		{
			this.Data.Add(point);
			this.ChartUpdated(point);
		}
		public ElectricityChart Clone(int currentperiod, int currenttick)
		{
			ElectricityChart electricityChart = new ElectricityChart
			{
				Name = this.Name,
				IsShow = this.IsShow,
				Period = this.Period
			};
			if (this.IsShow || currentperiod > this.Period)
			{
				electricityChart.Data = this.Data;
			}
			else
			{
				if (currentperiod == this.Period)
				{
					electricityChart.Data.AddRange(this.Data.GetRange(0, currenttick));
				}
			}
			return electricityChart;
		}
	}
}
