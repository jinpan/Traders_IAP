using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
namespace TTS
{
	public static class ChartExtensions
	{
		public static void Format(this Chart chart, bool ishighvisibility = false)
		{
			chart.BorderSkin.BackColor = System.Drawing.Color.Transparent;
			chart.BorderSkin.PageColor = System.Drawing.Color.AliceBlue;
			if (ishighvisibility)
			{
				chart.PaletteCustomColors = new System.Drawing.Color[]
				{
					System.Drawing.Color.FromArgb(0, 0, 255),
					System.Drawing.Color.FromArgb(255, 0, 0),
					System.Drawing.Color.FromArgb(0, 255, 0),
					System.Drawing.Color.FromArgb(0, 0, 44),
					System.Drawing.Color.FromArgb(255, 26, 185),
					System.Drawing.Color.FromArgb(255, 211, 0),
					System.Drawing.Color.FromArgb(0, 88, 0),
					System.Drawing.Color.FromArgb(132, 132, 255),
					System.Drawing.Color.FromArgb(158, 79, 70),
					System.Drawing.Color.FromArgb(0, 255, 193),
					System.Drawing.Color.FromArgb(0, 132, 149),
					System.Drawing.Color.FromArgb(0, 0, 123),
					System.Drawing.Color.FromArgb(149, 211, 79),
					System.Drawing.Color.FromArgb(246, 158, 220),
					System.Drawing.Color.FromArgb(211, 18, 255),
					System.Drawing.Color.FromArgb(123, 26, 106),
					System.Drawing.Color.FromArgb(246, 18, 97),
					System.Drawing.Color.FromArgb(255, 193, 132),
					System.Drawing.Color.FromArgb(35, 35, 9),
					System.Drawing.Color.FromArgb(141, 167, 123)
				};
				chart.Palette = ChartColorPalette.None;
			}
			else
			{
				chart.Palette = ChartColorPalette.Pastel;
			}
			chart.Legends.Clear();
			chart.Series.Clear();
			chart.ChartAreas.Clear();
		}
		public static void AddChartArea(this Chart chart, string areaname, bool zoomable)
		{
			ChartArea chartArea = new ChartArea();
			chartArea.AxisX.IsLabelAutoFit = false;
			chartArea.AxisX.LabelStyle.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold);
			chartArea.AxisX.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(66, 66, 66);
			chartArea.AxisX.LabelStyle.Format = "d";
			chartArea.AxisX.LabelStyle.Interval = 0.0;
			chartArea.AxisX.LabelStyle.IntervalOffset = 0.0;
			chartArea.AxisX.LabelStyle.IntervalOffsetType = DateTimeIntervalType.Auto;
			chartArea.AxisX.LabelStyle.IntervalType = DateTimeIntervalType.Auto;
			chartArea.AxisX.LineColor = System.Drawing.Color.FromArgb(222, 222, 222);
			chartArea.AxisX.MajorGrid.Interval = 0.0;
			chartArea.AxisX.MajorGrid.IntervalOffset = 0.0;
			chartArea.AxisX.MajorGrid.IntervalOffsetType = DateTimeIntervalType.Auto;
			chartArea.AxisX.MajorGrid.IntervalType = DateTimeIntervalType.Auto;
			chartArea.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(222, 222, 222);
			chartArea.AxisX.MajorTickMark.Interval = 0.0;
			chartArea.AxisX.MajorTickMark.IntervalOffset = 0.0;
			chartArea.AxisX.MajorTickMark.IntervalOffsetType = DateTimeIntervalType.Auto;
			chartArea.AxisX.MajorTickMark.IntervalType = DateTimeIntervalType.Auto;
			chartArea.AxisX.MajorTickMark.TickMarkStyle = TickMarkStyle.None;
			chartArea.AxisX.TitleAlignment = System.Drawing.StringAlignment.Near;
			chartArea.AxisX2.IsLabelAutoFit = false;
			chartArea.AxisX2.LabelStyle.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold);
			chartArea.AxisX2.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(66, 66, 66);
			chartArea.AxisX2.LabelStyle.Format = "d";
			chartArea.AxisX2.LabelStyle.Interval = 0.0;
			chartArea.AxisX2.LabelStyle.IntervalOffset = 0.0;
			chartArea.AxisX2.LabelStyle.IntervalOffsetType = DateTimeIntervalType.Auto;
			chartArea.AxisX2.LabelStyle.IntervalType = DateTimeIntervalType.Auto;
			chartArea.AxisX2.LineColor = System.Drawing.Color.FromArgb(222, 222, 222);
			chartArea.AxisX2.MajorGrid.Interval = 0.0;
			chartArea.AxisX2.MajorGrid.IntervalOffset = 0.0;
			chartArea.AxisX2.MajorGrid.IntervalOffsetType = DateTimeIntervalType.Auto;
			chartArea.AxisX2.MajorGrid.IntervalType = DateTimeIntervalType.Auto;
			chartArea.AxisX2.MajorGrid.LineColor = System.Drawing.Color.FromArgb(222, 222, 222);
			chartArea.AxisX2.MajorTickMark.Interval = 0.0;
			chartArea.AxisX2.MajorTickMark.IntervalOffset = 0.0;
			chartArea.AxisX2.MajorTickMark.IntervalOffsetType = DateTimeIntervalType.Auto;
			chartArea.AxisX2.MajorTickMark.IntervalType = DateTimeIntervalType.Auto;
			chartArea.AxisX2.MajorTickMark.TickMarkStyle = TickMarkStyle.None;
			chartArea.AxisX2.TitleAlignment = System.Drawing.StringAlignment.Near;
			chartArea.AxisY.IsLabelAutoFit = false;
			chartArea.AxisY.LabelStyle.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold);
			chartArea.AxisY.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(66, 66, 66);
			chartArea.AxisY.LabelStyle.Interval = 0.0;
			chartArea.AxisY.LabelStyle.IntervalOffset = 0.0;
			chartArea.AxisY.LabelStyle.IntervalOffsetType = DateTimeIntervalType.Auto;
			chartArea.AxisY.LabelStyle.IntervalType = DateTimeIntervalType.Auto;
			chartArea.AxisY.LineColor = System.Drawing.Color.FromArgb(222, 222, 222);
			chartArea.AxisY.MajorGrid.Interval = 0.0;
			chartArea.AxisY.MajorGrid.IntervalOffset = 0.0;
			chartArea.AxisY.MajorGrid.IntervalOffsetType = DateTimeIntervalType.Auto;
			chartArea.AxisY.MajorGrid.IntervalType = DateTimeIntervalType.Auto;
			chartArea.AxisY.MajorGrid.LineColor = System.Drawing.Color.FromArgb(222, 222, 222);
			chartArea.AxisY.MajorTickMark.Interval = 0.0;
			chartArea.AxisY.MajorTickMark.IntervalOffset = 0.0;
			chartArea.AxisY.MajorTickMark.IntervalOffsetType = DateTimeIntervalType.Auto;
			chartArea.AxisY.MajorTickMark.IntervalType = DateTimeIntervalType.Auto;
			chartArea.AxisY.MajorTickMark.TickMarkStyle = TickMarkStyle.None;
			chartArea.AxisY.IsStartedFromZero = false;
			chartArea.AxisY2.IsLabelAutoFit = false;
			chartArea.AxisY2.LabelStyle.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold);
			chartArea.AxisY2.LabelStyle.ForeColor = System.Drawing.Color.FromArgb(66, 66, 66);
			chartArea.AxisY2.LabelStyle.Interval = 0.0;
			chartArea.AxisY2.LabelStyle.IntervalOffset = 0.0;
			chartArea.AxisY2.LabelStyle.IntervalOffsetType = DateTimeIntervalType.Auto;
			chartArea.AxisY2.LabelStyle.IntervalType = DateTimeIntervalType.Auto;
			chartArea.AxisY2.LineColor = System.Drawing.Color.FromArgb(222, 222, 222);
			chartArea.AxisY2.MajorGrid.Interval = 0.0;
			chartArea.AxisY2.MajorGrid.IntervalOffset = 0.0;
			chartArea.AxisY2.MajorGrid.IntervalOffsetType = DateTimeIntervalType.Auto;
			chartArea.AxisY2.MajorGrid.IntervalType = DateTimeIntervalType.Auto;
			chartArea.AxisY2.MajorGrid.LineColor = System.Drawing.Color.FromArgb(222, 222, 222);
			chartArea.AxisY2.MajorTickMark.Interval = 0.0;
			chartArea.AxisY2.MajorTickMark.IntervalOffset = 0.0;
			chartArea.AxisY2.MajorTickMark.IntervalOffsetType = DateTimeIntervalType.Auto;
			chartArea.AxisY2.MajorTickMark.IntervalType = DateTimeIntervalType.Auto;
			chartArea.AxisY2.MajorTickMark.TickMarkStyle = TickMarkStyle.None;
			chartArea.AxisY2.IsStartedFromZero = false;
			chartArea.BorderColor = System.Drawing.Color.FromArgb(222, 222, 222);
			chartArea.BorderDashStyle = ChartDashStyle.Solid;
			chartArea.Name = areaname;
			chartArea.AxisY.LabelStyle.Format = "0.##";
			chartArea.Position.Auto = true;
			if (zoomable)
			{
				chartArea.CursorX.IsUserEnabled = true;
				chartArea.CursorX.IsUserSelectionEnabled = true;
				chartArea.AxisX.ScaleView.Zoomable = true;
				chartArea.AxisX.ScrollBar.IsPositionedInside = true;
				chartArea.AxisX.ScrollBar.LineColor = System.Drawing.Color.Black;
				chartArea.AxisX.ScaleView.MinSizeType = DateTimeIntervalType.Number;
			}
			chart.ChartAreas.Add(chartArea);
		}
		public static Series AddSeries(this Chart chart, string seriesname, string chartarea, SeriesChartType seriestype)
		{
			Series series = new Series(seriesname);
			series.ChartArea = chartarea;
			series.ChartType = seriestype;
			series.Name = seriesname;
			if (chart.Palette == ChartColorPalette.None)
			{
				series.Color = chart.PaletteCustomColors[chart.Series.Count % chart.PaletteCustomColors.Length];
			}
			else
			{
				series.Color = ChartColorPalette.Pastel.GetColorPaletteColor(chart.Series.Count);
			}
			if (seriestype == SeriesChartType.Stock || seriestype == SeriesChartType.Candlestick)
			{
				series["PriceUpColor"] = System.Drawing.Color.FromArgb(0, 154, 39).ToArgb().ToString();
				series["PriceDownColor"] = System.Drawing.Color.FromArgb(173, 12, 33).ToArgb().ToString();
				series.YValuesPerPoint = 4;
				series.BorderWidth = 2;
				series.Color = System.Drawing.Color.Black;
			}
			else
			{
				if (seriestype == SeriesChartType.Line)
				{
					series.BorderWidth = 2;
					series.EmptyPointStyle.Color = series.Color;
					series.EmptyPointStyle.BorderWidth = 2;
				}
				else
				{
					if (seriestype == SeriesChartType.Point)
					{
						series.MarkerStyle = MarkerStyle.Circle;
						series.MarkerSize = 8;
					}
				}
			}
			chart.Series.Add(series);
			return series;
		}
		public static void AddStripline(this Axis axis, double value)
		{
			StripLine stripLine = new StripLine();
			stripLine.BorderColor = System.Drawing.Color.Red;
			stripLine.BorderDashStyle = ChartDashStyle.Dash;
			stripLine.BorderWidth = 2;
			stripLine.Interval = 0.0;
			stripLine.IntervalOffset = value;
			stripLine.StripWidth = 0.0;
			axis.StripLines.Add(stripLine);
		}
		public static void AutoSizeChartAreas(this Chart chart)
		{
			chart.SuspendLayout();
			int num = chart.ChartAreas.Count - 1;
			if (chart.ChartAreas.Count == 1)
			{
				chart.ChartAreas[0].Position.FromRectangleF(new System.Drawing.RectangleF(0f, 0f, 98f, 100f));
				chart.ChartAreas[0].RecalculateAxesScale();
			}
			else
			{
				float num2 = (float)System.Math.Min(20, System.Math.Min(50, 20 * num) / num);
				float num3 = 100f - num2 * (float)num;
				chart.ChartAreas[0].Position.FromRectangleF(new System.Drawing.RectangleF(0f, 0f, 98f, num3));
				int num4 = 0;
				for (int i = 1; i < chart.ChartAreas.Count; i++)
				{
					if (chart.ChartAreas[i].Visible)
					{
						chart.ChartAreas[i].Position.FromRectangleF(new System.Drawing.RectangleF(0f, num3 + (float)num4++ * num2, 98f, num2));
						chart.ChartAreas[i].RecalculateAxesScale();
					}
				}
			}
			chart.ResumeLayout();
		}
		public static void AddLegend(this Chart chart, string chartarea, string name)
		{
			chart.Legends.Add(name);
			chart.Legends[name].IsDockedInsideChartArea = true;
			chart.Legends[name].DockedToChartArea = chartarea;
			chart.Legends[name].Enabled = true;
			chart.Legends[name].TableStyle = LegendTableStyle.Auto;
			chart.Legends[name].Position.Auto = true;
			chart.Legends[name].LegendStyle = LegendStyle.Table;
			chart.Legends[name].BackColor = System.Drawing.Color.Transparent;
		}
		public static void AddTickLabels(this ChartArea area, int ticksperperiod, int periods, bool isexistpregamedata = false, bool islabel = true)
		{
			area.AxisX.CustomLabels.Clear();
			area.AxisX.StripLines.Clear();
			if (isexistpregamedata)
			{
				area.AxisX.AddStripline(1.0);
			}
			for (int i = 1; i <= periods; i++)
			{
				if (islabel)
				{
					area.AxisX.CustomLabels.Add((double)((i - 1) * ticksperperiod + 1), (double)(i * ticksperperiod), i.ToString(), 1, LabelMarkStyle.LineSideMark);
				}
				if (i < periods)
				{
					area.AxisX.AddStripline((double)(i * ticksperperiod));
				}
			}
		}
		public static void FormatChartAreaSize(this Chart chart)
		{
			chart.SuspendLayout();
			int num = chart.ChartAreas.Count((ChartArea x) => x.Visible);
			if (num == 1)
			{
				chart.ChartAreas.First((ChartArea x) => x.Visible).Position.FromRectangleF(new System.Drawing.RectangleF(0f, 0f, 98f, 100f));
			}
			else
			{
				float num2 = (float)System.Math.Min(20, System.Math.Min(50, 20 * (num - 1)) / (num - 1));
				float num3 = 100f - num2 * (float)(num - 1);
				chart.ChartAreas[0].Position.FromRectangleF(new System.Drawing.RectangleF(0f, 0f, 98f, num3));
				int num4 = 0;
				for (int i = 1; i < chart.ChartAreas.Count; i++)
				{
					if (chart.ChartAreas[i].Visible)
					{
						chart.ChartAreas[i].Position.FromRectangleF(new System.Drawing.RectangleF(0f, num3 + (float)num4++ * num2, 98f, num2));
					}
				}
			}
			chart.ResumeLayout();
		}
	}
}
