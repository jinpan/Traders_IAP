using Ciloci.Flee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
namespace TTS
{
	internal class ExpressionChartItem : System.IDisposable
	{
		public class PlotData
		{
			public int MaxTick;
			public int MinTick;
			public int[] X;
			public double[][] Y;
			public bool[] IsValid;
		}
		public class VolumeData
		{
			public int MaxTick;
			public int MinTick;
			public int[] X;
			public double[] Y;
			public bool[] IsValid;
		}
		public string ExpressionString;
		public string OriginalExpressionString;
		public double[][] ExpressionYData;
		public int[] ExpressionXData;
		private IGenericExpression<double> Expression;
		private System.Collections.Generic.List<string> Securities = new System.Collections.Generic.List<string>();
		private ExpressionContext ExpressionContext = new ExpressionContext();
		public int StartPeriod = 1;
		public int StopPeriod = Game.State.General.Periods;
		public int InitialTicks = 2147483647;
		public int TicksPerPeriod = Game.State.General.TicksPerPeriod;
		public int Grouping = 1;
		public int TicksAvailable;
		public int TotalTicks;
		private int QuotedDecimals = 2147483647;
		private System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<SecurityChartPoint>> ChartData = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<SecurityChartPoint>>();
		private System.Collections.Generic.Dictionary<string, Action<int[], SecurityChartPoint[]>> ChartUpdateHandlers = new System.Collections.Generic.Dictionary<string, Action<int[], SecurityChartPoint[]>>();
		public ChartItem<SecurityVolumeChartPoint> VolumeChart;
		private Action<int[], SecurityVolumeChartPoint[]> VolumeUpdateHandler;
		public event System.Action<ExpressionChartItem.PlotData> GroupDataUpdated = delegate
		{
		};
		public event System.Action<ExpressionChartItem.VolumeData> VolumeDataUpdated = delegate
		{
		};
		public string FormatString
		{
			get
			{
				return "0." + new string('0', this.QuotedDecimals);
			}
		}
		private string TickerToVariable(string ticker)
		{
			ticker = new string(ticker.ToUpper().Select(delegate(char x)
			{
				if (!char.IsLetterOrDigit(x))
				{
					return '_';
				}
				return x;
			}).ToArray<char>());
			ticker = ((char.IsLetter(ticker[0]) || ticker[0] == '_') ? ticker : ("_" + ticker));
			return ticker;
		}
		public ExpressionChartItem(string expression, int? period)
		{
			try
			{
				this.OriginalExpressionString = expression;
				string text = expression;
				foreach (string current in 
					from x in Game.State.Securities.Keys
					orderby x.Length descending
					select x)
				{
					text = new Regex("\\[" + current + "\\]", RegexOptions.IgnoreCase).Replace(text, " 1 ");
				}
				foreach (string current2 in 
					from x in Game.State.Securities.Keys
					orderby x.Length descending
					select x)
				{
					text = new Regex("\\b" + current2 + "\\b", RegexOptions.IgnoreCase).Replace(text, " 1 ");
				}
				new ExpressionContext().CompileGeneric<double>(text).Evaluate();
			}
			catch
			{
				throw new System.Exception("You have entered an invalid charting ticker or expression.\n\nTickers should be input with [Brackets] around them and are not case sensitive. Expressions can contain plus(+), minus(-), multiply(*), divide(/), exponents (^), and/or values.\n\nFor example, a spread can be shown as [CL-Jan]-[CL-Dec], and a conversion spread could be shown as [CL-Spot]-8*[NG-Spot].");
			}
			foreach (string current3 in 
				from x in Game.State.Securities.Keys
				orderby x.Length descending
				select x)
			{
				expression = new Regex("\\[" + current3 + "\\]", RegexOptions.IgnoreCase).Replace(expression, " " + this.TickerToVariable(current3) + " ");
			}
			foreach (string current4 in 
				from x in Game.State.Securities.Keys
				orderby x.Length descending
				select x)
			{
				expression = new Regex("\\b" + current4 + "\\b", RegexOptions.IgnoreCase).Replace(expression, " " + this.TickerToVariable(current4) + " ");
			}
			this.ExpressionString = expression;
			if (period.HasValue)
			{
				this.StartPeriod = (this.StopPeriod = period.Value);
			}
			foreach (string current5 in Game.State.Securities.Keys)
			{
				if (new Regex("\\b" + this.TickerToVariable(current5) + "\\b").IsMatch(expression))
				{
					this.Securities.Add(current5);
					this.StartPeriod = System.Math.Max(this.StartPeriod, Game.State.Securities[current5].HistoryChart.StartPeriod);
					this.StopPeriod = System.Math.Min(this.StopPeriod, Game.State.Securities[current5].HistoryChart.StopPeriod);
					this.InitialTicks = System.Math.Min(this.InitialTicks, Game.State.Securities[current5].HistoryChart.InitialTicks);
					this.ExpressionContext.Variables.DefineVariable(this.TickerToVariable(current5), typeof(double));
					this.QuotedDecimals = System.Math.Min(Game.State.Securities[current5].Parameters.QuotedDecimals, this.QuotedDecimals);
					if (this.TicksPerPeriod != Game.State.Securities[current5].HistoryChart.TicksPerPeriod)
					{
						throw new System.Exception("Mismatch in TicksPerPeriod.");
					}
				}
			}
			this.InitialTicks = (this.Securities.Any((string x) => Game.State.Securities[x].HistoryChart.StartPeriod != this.StartPeriod) ? 0 : this.InitialTicks);
			if (this.Securities.Count == 0)
			{
				throw new System.Exception("No valid securities.");
			}
			if (this.StartPeriod > this.StopPeriod)
			{
				throw new System.Exception("No overlapping periods.");
			}
			this.Expression = this.ExpressionContext.CompileGeneric<double>(expression);
			if (this.Securities.Count == 1)
			{
				this.VolumeChart = Game.State.Securities[this.Securities[0]].VolumeChart;
			}
			foreach (string current6 in this.Securities)
			{
				this.ChartData.Add(current6, new System.Collections.Generic.List<SecurityChartPoint>(Game.State.Securities[current6].HistoryChart.GetData(this.InitialTicks, this.StartPeriod, this.StopPeriod)));
			}
			this.TicksAvailable = this.ChartData.Values.Min((System.Collections.Generic.List<SecurityChartPoint> x) => x.Count);
			this.TotalTicks = this.InitialTicks + (this.StopPeriod - this.StartPeriod + 1) * this.TicksPerPeriod;
			this.ExpressionYData = new double[4][];
			for (int i = 0; i < 4; i++)
			{
				this.ExpressionYData[i] = new double[this.TotalTicks];
			}
			for (int j = 0; j < this.TicksAvailable; j++)
			{
				try
				{
					foreach (string current7 in this.Securities)
					{
						this.ExpressionContext.Variables[this.TickerToVariable(current7)] = System.Convert.ToDouble(this.ChartData[current7][j].High);
					}
					this.ExpressionYData[0][j] = this.Expression.Evaluate();
					foreach (string current8 in this.Securities)
					{
						this.ExpressionContext.Variables[this.TickerToVariable(current8)] = System.Convert.ToDouble(this.ChartData[current8][j].Low);
					}
					this.ExpressionYData[1][j] = this.Expression.Evaluate();
					foreach (string current9 in this.Securities)
					{
						this.ExpressionContext.Variables[this.TickerToVariable(current9)] = System.Convert.ToDouble(this.ChartData[current9][j].Open);
					}
					this.ExpressionYData[2][j] = this.Expression.Evaluate();
					foreach (string current10 in this.Securities)
					{
						this.ExpressionContext.Variables[this.TickerToVariable(current10)] = System.Convert.ToDouble(this.ChartData[current10][j].Close);
					}
					this.ExpressionYData[3][j] = this.Expression.Evaluate();
				}
				catch
				{
				}
			}
			this.ExpressionXData = new int[this.TotalTicks];
			for (int k = 0; k < this.ExpressionXData.Length; k++)
			{
				this.ExpressionXData[k] = k - this.InitialTicks + 1 + (this.StartPeriod - 1) * this.TicksPerPeriod;
			}
			foreach (string current11 in this.Securities)
			{
				string t = current11;
				Action<int[], SecurityChartPoint[]> value = delegate(int[] gameticks, SecurityChartPoint[] points)
				{
					this.ChartUpdated(t, gameticks, points);
				};
				Game.State.Securities[current11].HistoryChart.ChartUpdated += value;
				this.ChartUpdateHandlers.Add(current11, value);
				if (this.VolumeChart != null)
				{
					this.VolumeUpdateHandler = new Action<int[], SecurityVolumeChartPoint[]>(this.VolumeUpdated);
					this.VolumeChart.ChartUpdated += this.VolumeUpdateHandler;
				}
			}
		}
		private void VolumeUpdated(int[] gameticks, SecurityVolumeChartPoint[] points)
		{
			int[] array = (
				from x in gameticks.Where((int x, int ind) => gameticks[ind] > (this.StartPeriod - 1) * this.TicksPerPeriod && gameticks[ind] <= this.StopPeriod * this.TicksPerPeriod)
				select x - (this.StartPeriod - 1) * this.TicksPerPeriod + this.InitialTicks - 1).ToArray<int>();
			if (array.Length > 0)
			{
				this.VolumeDataUpdated(this.GetVolume(array));
			}
		}
		public ExpressionChartItem.VolumeData GetVolume(int[] indexes)
		{
			ExpressionChartItem.VolumeData volumeData = new ExpressionChartItem.VolumeData
			{
				MaxTick = this.ExpressionXData[this.ExpressionXData.Length - 1],
				MinTick = this.ExpressionXData[0]
			};
			if (indexes.Length == 0)
			{
				volumeData.X = new int[0];
				volumeData.Y = new double[0];
				return volumeData;
			}
			int num = indexes[0] / this.Grouping;
			int num2 = indexes[indexes.Length - 1] / this.Grouping;
			int num3 = num2 - num + 1;
			int num4 = 0;
			volumeData.X = new int[num3];
			volumeData.Y = new double[num3];
			for (int i = num; i <= num2; i++)
			{
				int num5 = i * this.Grouping;
				volumeData.Y[num4] = 0.0;
				int num6 = num5;
				while (num6 < num5 + this.Grouping && num6 < this.TotalTicks)
				{
					int num7 = num6 + 1 - this.InitialTicks + (this.StartPeriod - 1) * this.TicksPerPeriod;
					int num8 = num7 - 1 + this.VolumeChart.InitialTicks - (this.VolumeChart.StartPeriod - 1) * this.VolumeChart.TicksPerPeriod;
					if (num8 >= 0 && num8 < this.VolumeChart.Data.Count)
					{
						volumeData.Y[num4] += this.VolumeChart.Data[num8].Volume;
					}
					num6++;
				}
				volumeData.X[num4] = i;
				num4++;
			}
			return volumeData;
		}
		public ExpressionChartItem.VolumeData GetVolume()
		{
			ExpressionChartItem.VolumeData volumeData = new ExpressionChartItem.VolumeData
			{
				MaxTick = this.ExpressionXData[this.ExpressionXData.Length - 1],
				MinTick = this.ExpressionXData[0]
			};
			int num = 0;
			int num2 = (this.TotalTicks == 0) ? 0 : ((this.TotalTicks - 1) / this.Grouping + 1);
			volumeData.X = new int[num2];
			volumeData.Y = new double[num2];
			volumeData.IsValid = new bool[num2];
			for (int i = 0; i < this.TotalTicks; i += this.Grouping)
			{
				volumeData.Y[num] = 0.0;
				int num3 = i;
				while (num3 < i + this.Grouping && num3 < this.TotalTicks)
				{
					int num4 = num3 + 1 - this.InitialTicks + (this.StartPeriod - 1) * this.TicksPerPeriod;
					int num5 = num4 - 1 + this.VolumeChart.InitialTicks - (this.VolumeChart.StartPeriod - 1) * this.VolumeChart.TicksPerPeriod;
					if (num5 >= 0 && num5 < this.VolumeChart.Data.Count)
					{
						volumeData.Y[num] += this.VolumeChart.Data[num5].Volume;
						volumeData.IsValid[num] = true;
					}
					if (num5 >= 0)
					{
						if (!volumeData.IsValid.Any((bool x) => x))
						{
							volumeData.IsValid[num] = true;
						}
					}
					num3++;
				}
				volumeData.X[num] = this.ExpressionXData[i] + this.Grouping / 2;
				num++;
			}
			return volumeData;
		}
		private void ChartUpdated(string ticker, int[] gameticks, SecurityChartPoint[] points)
		{
			points = points.Where((SecurityChartPoint x, int ind) => gameticks[ind] > (this.StartPeriod - 1) * this.TicksPerPeriod && gameticks[ind] <= this.StopPeriod * this.TicksPerPeriod).ToArray<SecurityChartPoint>();
			int[] array = (
				from x in gameticks.Where((int x, int ind) => gameticks[ind] > (this.StartPeriod - 1) * this.TicksPerPeriod && gameticks[ind] <= this.StopPeriod * this.TicksPerPeriod)
				select x - (this.StartPeriod - 1) * this.TicksPerPeriod + this.InitialTicks - 1).ToArray<int>();
			for (int i = 0; i < points.Length; i++)
			{
				int num = array[i];
				if (num < this.ChartData[ticker].Count)
				{
					this.ChartData[ticker][num] = points[i];
				}
				else
				{
					this.ChartData[ticker].Add(points[i]);
				}
			}
			this.TicksAvailable = this.ChartData.Values.Min((System.Collections.Generic.List<SecurityChartPoint> x) => x.Count);
			array = (
				from x in array
				where x < this.TicksAvailable
				select x).ToArray<int>();
			for (int j = 0; j < array.Length; j++)
			{
				try
				{
					int num2 = array[j];
					foreach (string current in this.Securities)
					{
						this.ExpressionContext.Variables[this.TickerToVariable(current)] = System.Convert.ToDouble(this.ChartData[current][num2].High);
					}
					this.ExpressionYData[0][num2] = this.Expression.Evaluate();
					foreach (string current2 in this.Securities)
					{
						this.ExpressionContext.Variables[this.TickerToVariable(current2)] = System.Convert.ToDouble(this.ChartData[current2][num2].Low);
					}
					this.ExpressionYData[1][num2] = this.Expression.Evaluate();
					foreach (string current3 in this.Securities)
					{
						this.ExpressionContext.Variables[this.TickerToVariable(current3)] = System.Convert.ToDouble(this.ChartData[current3][num2].Open);
					}
					this.ExpressionYData[2][num2] = this.Expression.Evaluate();
					foreach (string current4 in this.Securities)
					{
						this.ExpressionContext.Variables[this.TickerToVariable(current4)] = System.Convert.ToDouble(this.ChartData[current4][num2].Close);
					}
					this.ExpressionYData[3][num2] = this.Expression.Evaluate();
				}
				catch
				{
				}
			}
			if (array.Length > 0)
			{
				this.GroupDataUpdated(this.GetGroups(array));
			}
		}
		public ExpressionChartItem.PlotData GetGroups(int[] indexes)
		{
			ExpressionChartItem.PlotData plotData = new ExpressionChartItem.PlotData
			{
				MaxTick = this.ExpressionXData[this.ExpressionXData.Length - 1],
				MinTick = this.ExpressionXData[0]
			};
			if (indexes.Length == 0)
			{
				plotData.X = new int[0];
				plotData.Y = new double[0][];
				return plotData;
			}
			int num = indexes[0] / this.Grouping;
			int num2 = indexes[indexes.Length - 1] / this.Grouping;
			int num3 = num2 - num + 1;
			int num4 = 0;
			plotData.X = new int[num3];
			plotData.Y = new double[4][];
			for (int i = 0; i < 4; i++)
			{
				plotData.Y[i] = new double[num3];
			}
			for (int j = num; j <= num2; j++)
			{
				int num5 = j * this.Grouping;
				if (num5 < this.TicksAvailable)
				{
					plotData.Y[0][num4] = (plotData.Y[1][num4] = (plotData.Y[2][num4] = this.ExpressionYData[2][num5]));
					int num6 = num5;
					while (num6 < num5 + this.Grouping && num6 < this.TicksAvailable)
					{
						plotData.Y[0][num4] = System.Math.Max(this.ExpressionYData[0][num6], plotData.Y[0][num4]);
						plotData.Y[1][num4] = System.Math.Min(this.ExpressionYData[1][num6], plotData.Y[1][num4]);
						num6++;
					}
					plotData.Y[3][num4] = this.ExpressionYData[3][num6 - 1];
				}
				plotData.X[num4] = j;
				num4++;
			}
			return plotData;
		}
		public ExpressionChartItem.PlotData GetGroups(int grouping)
		{
			this.Grouping = grouping;
			ExpressionChartItem.PlotData plotData = new ExpressionChartItem.PlotData
			{
				MaxTick = this.ExpressionXData[this.ExpressionXData.Length - 1],
				MinTick = this.ExpressionXData[0]
			};
			int num = 0;
			int num2 = (this.TotalTicks == 0) ? 0 : ((this.TotalTicks - 1) / this.Grouping + 1);
			plotData.X = new int[num2];
			plotData.Y = new double[4][];
			plotData.IsValid = new bool[num2];
			for (int i = 0; i < 4; i++)
			{
				plotData.Y[i] = new double[num2];
			}
			for (int j = 0; j < this.TotalTicks; j += this.Grouping)
			{
				if (j < this.TicksAvailable)
				{
					plotData.Y[0][num] = (plotData.Y[1][num] = (plotData.Y[2][num] = this.ExpressionYData[2][j]));
					int num3 = j;
					while (num3 < j + this.Grouping && num3 < this.TicksAvailable)
					{
						plotData.IsValid[num] = true;
						plotData.Y[0][num] = System.Math.Max(this.ExpressionYData[0][num3], plotData.Y[0][num]);
						plotData.Y[1][num] = System.Math.Min(this.ExpressionYData[1][num3], plotData.Y[1][num]);
						num3++;
					}
					plotData.Y[3][num] = this.ExpressionYData[3][num3 - 1];
				}
				plotData.X[num] = this.ExpressionXData[j] + this.Grouping / 2;
				num++;
			}
			if (num2 > 0)
			{
				if (plotData.IsValid.All((bool x) => !x))
				{
					plotData.IsValid[0] = true;
				}
			}
			return plotData;
		}
		public void Dispose()
		{
			this.Dispose(true);
			System.GC.SuppressFinalize(this);
		}
		protected void Dispose(bool iAmBeingCalledFromDisposeAndNotFinalize)
		{
			try
			{
				foreach (System.Collections.Generic.KeyValuePair<string, Action<int[], SecurityChartPoint[]>> current in this.ChartUpdateHandlers)
				{
					Game.State.Securities[current.Key].HistoryChart.ChartUpdated -= current.Value;
				}
				if (this.VolumeUpdateHandler != null)
				{
					this.VolumeChart.ChartUpdated -= this.VolumeUpdateHandler;
				}
				this.GroupDataUpdated = null;
				this.VolumeDataUpdated = null;
			}
			catch
			{
			}
		}
	}
}
