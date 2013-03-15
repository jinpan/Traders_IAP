using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TTS.Properties;
namespace TTS
{
	public class SecurityCharting : TTSForm
	{
		public static WindowType TTSWindowType = WindowType.SECURITY_CHARTING;
		private LineAnnotation CurrentLine;
		private System.Drawing.Color CurrentColor = System.Drawing.Color.Black;
		private int CurrentGrouping;
		private int ThrottledX;
		private int ThrottledY;
		private int UnthrottledX;
		private int UnthrottledY;
		private double ThrottledPosition;
		private double UnthrottledPosition;
		private bool IsThrottled;
		private bool IsZoomThrottled;
		private ExpressionChartItem CurrentExpression;
		private string LastExpression;
		private int? CurrentPeriod = null;
		private string LastEnteredExpression;
		private System.Collections.Generic.List<int> MovingAverageSpans = new System.Collections.Generic.List<int>();
		private System.Collections.Generic.List<int> RSISpans = new System.Collections.Generic.List<int>();
		private System.Collections.Generic.List<int> EMASpans = new System.Collections.Generic.List<int>();
		private IContainer components;
		private System.Windows.Forms.ToolStrip MainStrip;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripTextBox ExpressionTextBox;
		private System.Windows.Forms.ToolStripLabel toolStripLabel5;
		private System.Windows.Forms.ToolStripDropDownButton AddIndicatorDropDown;
		private System.Windows.Forms.ToolStripDropDownButton RemoveIndicatorDropDown;
		private System.Windows.Forms.ToolStripMenuItem bollingerBandsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exponentialMovingAverageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mACDToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem movingAverageToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem rSIToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stochasticOscillatorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem simpleStochasticToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem smoothStochasticToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem clearAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private Chart MainChart;
		private System.Windows.Forms.Timer MouseMoveThrottle;
		private System.Windows.Forms.ToolStripLabel toolStripLabel6;
		private System.Windows.Forms.ColorDialog DrawingColorDialog;
		private System.Windows.Forms.ToolStripButton DrawingColorDropDown;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton UndoButton;
		private System.Windows.Forms.Timer ChartScrollThrottle;
		private System.Windows.Forms.ToolStripDropDownButton GroupDropDown;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		private System.Windows.Forms.ToolStripMenuItem Group15MenuItem;
		private System.Windows.Forms.ToolStripMenuItem Group30MenuItem;
		private System.Windows.Forms.ToolStripMenuItem Group60MenuItem;
		private System.Windows.Forms.ToolStripMenuItem Group120MenuItem;
		private System.Windows.Forms.ToolStripMenuItem customToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripButton ZoomOutButton;
		private System.Windows.Forms.ToolStripButton IsZoomableButton;
		private System.Windows.Forms.SaveFileDialog MainChartSaveFileDialog;
		private System.Windows.Forms.ToolStripDropDownButton TickerDropDown;
		private System.Windows.Forms.ToolStripLabel toolStripLabel3;
		private System.Windows.Forms.ToolStripDropDownButton PeriodDropDown;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem volumeToolStripMenuItem;
		private System.Windows.Forms.ToolStripDropDownButton ExportSplitButton;
		private System.Windows.Forms.ToolStripMenuItem copyImageToClipboardToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveImageToFilepngToolStripMenuItem;
		public SecurityCharting(TTSFormState state) : base(state)
		{
			this.InitializeComponent();
			this.SetSecurityGrouping(System.Convert.ToInt32(this.GroupDropDown.Text));
			this.MainChart.Format(false);
			this.MainChart.AddChartArea("Main", true);
			this.MainChart.ChartAreas["Main"].AxisY.TitleFont = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.MainChart.AddSeries("HLOC", "Main", SeriesChartType.Candlestick);
			this.MainChart.Legends.Add("Main");
			this.MainChart.Legends["Main"].Enabled = false;
			this.MainChart.Legends.Add("Indicators");
			this.MainChart.Legends["Indicators"].LegendStyle = LegendStyle.Table;
			this.MainChart.Legends["Indicators"].InsideChartArea = "Main";
			this.MainChart.Legends["Indicators"].IsTextAutoFit = true;
			this.MainChart.Legends["Indicators"].Position.Auto = true;
			this.MainChart.Legends["Indicators"].BackColor = System.Drawing.Color.Transparent;
			this.MainChart.Legends["Indicators"].Docking = Docking.Top;
			this.MainChart.Legends["Indicators"].Alignment = System.Drawing.StringAlignment.Near;
			this.MainChart.Legends["Indicators"].Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.MainChart.DataManipulator.IsStartFromFirst = true;
			this.MainChart.SuppressExceptions = true;
			this.MainChart.AddChartArea("RSI", true);
			this.MainChart.ChartAreas["RSI"].AlignWithChartArea = "Main";
			this.MainChart.ChartAreas["RSI"].AlignmentOrientation = AreaAlignmentOrientations.Vertical;
			this.MainChart.ChartAreas["RSI"].AlignmentStyle = AreaAlignmentStyles.All;
			this.MainChart.ChartAreas["RSI"].Visible = false;
			this.MainChart.ChartAreas["RSI"].AxisY.Title = "RSI";
			this.MainChart.ChartAreas["RSI"].AxisY.TitleFont = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.MainChart.ChartAreas["RSI"].AxisY.Maximum = 100.0;
			this.MainChart.ChartAreas["RSI"].AxisY.Minimum = 0.0;
			this.MainChart.ChartAreas["RSI"].AxisY.AddStripline(30.0);
			this.MainChart.ChartAreas["RSI"].AxisY.AddStripline(70.0);
			this.MainChart.AddChartArea("Volume", true);
			this.MainChart.ChartAreas["Volume"].AlignWithChartArea = "Main";
			this.MainChart.ChartAreas["Volume"].AlignmentOrientation = AreaAlignmentOrientations.Vertical;
			this.MainChart.ChartAreas["Volume"].AlignmentStyle = AreaAlignmentStyles.All;
			this.MainChart.ChartAreas["Volume"].Visible = false;
			this.MainChart.ChartAreas["Volume"].AxisY.Title = "Volume";
			this.MainChart.ChartAreas["Volume"].AxisY.TitleFont = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.MainChart.AddSeries("Volume", "Volume", SeriesChartType.Column);
			this.IsZoomableButton.PerformClick();
			base.Shown += delegate(object sender, System.EventArgs e)
			{
				this.ExpressionTextBox.TextBox.Focus();
			};
			base.AddResettingHandler(delegate
			{
				if (this.CurrentExpression != null)
				{
					this.LastExpression = this.CurrentExpression.OriginalExpressionString;
					this.CurrentExpression.Dispose();
				}
				else
				{
					this.LastExpression = null;
				}
				this.CurrentExpression = null;
			});
			base.AddResetHandler(delegate
			{
				this.BindTickerDropDown();
				this.BindPeriodDropDown();
				try
				{
					if (this.LastExpression != null)
					{
						this.LoadExpression(this.LastExpression);
						this.ExpressionTextBox.Text = this.LastExpression;
						this.LastEnteredExpression = this.LastExpression;
					}
				}
				catch
				{
					this.ExpressionTextBox.Clear();
				}
			});
			this.BindTickerDropDown();
			this.BindPeriodDropDown();
			base.FormClosed += delegate(object sender, System.Windows.Forms.FormClosedEventArgs e)
			{
				if (this.CurrentExpression != null)
				{
					this.CurrentExpression.Dispose();
				}
			};
			if (state != null && state.State != null)
			{
				try
				{
					Tuple<int[], int[], int[]> tuple = (Tuple<int[], int[], int[]>)state.State;
					int[] item = tuple.Item1;
					for (int i = 0; i < item.Length; i++)
					{
						int bars = item[i];
						this.AddMovingAverage(bars);
					}
					int[] item2 = tuple.Item2;
					for (int j = 0; j < item2.Length; j++)
					{
						int bars2 = item2[j];
						this.AddRSI(bars2);
					}
					int[] item3 = tuple.Item3;
					for (int k = 0; k < item3.Length; k++)
					{
						int bars3 = item3[k];
						this.AddEMA(bars3);
					}
				}
				catch
				{
				}
			}
		}
		protected override object GetState()
		{
			return Tuple.Create<int[], int[], int[]>(this.MovingAverageSpans.ToArray(), this.RSISpans.ToArray(), this.EMASpans.ToArray());
		}
		private void BindTickerDropDown()
		{
			this.TickerDropDown.DropDownItems.Clear();
			foreach (string current in Game.State.Securities.Keys)
			{
				string t = current;
				System.Windows.Forms.ToolStripItem toolStripItem = this.TickerDropDown.DropDownItems.Add(t);
				toolStripItem.Click += delegate(object sender, System.EventArgs e)
				{
					this.ExpressionTextBox.AppendText(string.Format("[{0}]", t));
					this.ExpressionTextBox.TextBox.Focus();
					this.ExpressionTextBox.TextBox.SelectionStart = this.ExpressionTextBox.TextBox.TextLength;
				};
			}
		}
		private void SetCurrentPeriod(int? period)
		{
			this.CurrentPeriod = period;
			this.PeriodDropDown.Text = ((!period.HasValue) ? "ALL" : period.ToString());
			try
			{
				if (!string.IsNullOrEmpty(this.LastEnteredExpression))
				{
					this.LoadExpression(this.LastEnteredExpression);
				}
			}
			catch (System.Exception ex)
			{
				DialogHelper.ShowError(ex.Message, "Error");
			}
		}
		private void BindPeriodDropDown()
		{
			this.PeriodDropDown.DropDownItems.Clear();
			System.Windows.Forms.ToolStripItem toolStripItem = this.PeriodDropDown.DropDownItems.Add("ALL");
			toolStripItem.Click += delegate(object sender, System.EventArgs e)
			{
				this.SetCurrentPeriod(null);
			};
			this.PeriodDropDown.DropDownItems.Add(new System.Windows.Forms.ToolStripSeparator());
			for (int i = 1; i <= Game.State.General.Periods; i++)
			{
				int period = i;
				toolStripItem = this.PeriodDropDown.DropDownItems.Add(period.ToString());
				toolStripItem.Click += delegate(object sender, System.EventArgs e)
				{
					this.SetCurrentPeriod(new int?(period));
				};
			}
		}
		private void SetSecurityGrouping(int groupsize)
		{
			if (this.CurrentGrouping != groupsize)
			{
				this.CurrentGrouping = groupsize;
				this.GroupDropDown.Text = this.CurrentGrouping.ToString();
				foreach (System.Windows.Forms.ToolStripMenuItem toolStripMenuItem in this.GroupDropDown.DropDownItems)
				{
					toolStripMenuItem.Checked = false;
				}
				foreach (System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2 in this.GroupDropDown.DropDownItems)
				{
					if (toolStripMenuItem2.Text.StartsWith("Custom") || System.Convert.ToInt32(toolStripMenuItem2.Text) == groupsize)
					{
						toolStripMenuItem2.Checked = true;
						break;
					}
				}
				if (this.CurrentExpression != null)
				{
					ExpressionChartItem.PlotData groups = this.CurrentExpression.GetGroups(this.CurrentGrouping);
					this.MainChart.Series["HLOC"].Points.DataBindXY(groups.X, groups.Y);
					for (int i = 0; i < this.MainChart.Series["HLOC"].Points.Count; i++)
					{
						if (!groups.IsValid[i])
						{
							this.MainChart.Series["HLOC"].Points[i].IsEmpty = true;
						}
					}
					foreach (int current in this.MovingAverageSpans)
					{
						this.InitializeMovingAverage(current);
					}
					foreach (int current2 in this.EMASpans)
					{
						this.InitializeEMA(current2);
					}
					foreach (int current3 in this.RSISpans)
					{
						this.InitializeRSI(current3);
					}
					if (this.CurrentExpression.VolumeChart != null)
					{
						ExpressionChartItem.VolumeData volume = this.CurrentExpression.GetVolume();
						this.MainChart.Series["Volume"].Points.DataBindXY(volume.X, new System.Collections.IEnumerable[]
						{
							volume.Y
						});
						for (int j = 0; j < this.MainChart.Series["Volume"].Points.Count; j++)
						{
							if (!volume.IsValid[j])
							{
								this.MainChart.Series["Volume"].Points[j].IsEmpty = true;
							}
						}
						this.SetYAxis("Volume");
					}
				}
			}
		}
		private void GroupMenuItem_Click(object sender, System.EventArgs e)
		{
			this.SetSecurityGrouping(System.Convert.ToInt32(((System.Windows.Forms.ToolStripMenuItem)sender).Text));
		}
		private void customToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			PromptDialog promptDialog = new PromptDialog("Custom Group Size", "", new System.Windows.Forms.Control[]
			{
				new System.Windows.Forms.NumericUpDown
				{
					Minimum = 1m,
					Increment = 10m,
					Maximum = 2147483647m,
					Value = this.CurrentGrouping
				}
			}, new string[]
			{
				"Group Size"
			});
			if (promptDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				this.SetSecurityGrouping(System.Convert.ToInt32(promptDialog.Values[0]));
			}
		}
		private void DrawAnnotation(string chartarea, int x, int y)
		{
			this.CurrentLine = new LineAnnotation();
			this.CurrentLine.LineColor = this.CurrentColor;
			do
			{
				this.CurrentLine.Name = RandomHelper.Next().ToString();
			}
			while (!this.MainChart.Annotations.IsUniqueName(this.CurrentLine.Name));
			this.CurrentLine.LineWidth = 2;
			this.CurrentLine.ClipToChartArea = chartarea;
			this.CurrentLine.AxisX = this.MainChart.ChartAreas[chartarea].AxisX;
			this.CurrentLine.AxisY = this.MainChart.ChartAreas[chartarea].AxisY;
			this.CurrentLine.AllowAnchorMoving = true;
			this.CurrentLine.AllowMoving = true;
			this.CurrentLine.AllowPathEditing = true;
			this.CurrentLine.AllowResizing = true;
			this.CurrentLine.AllowSelecting = true;
			this.CurrentLine.IsSizeAlwaysRelative = false;
			this.CurrentLine.X = this.MainChart.ChartAreas[chartarea].AxisX.PixelPositionToValue((double)x);
			this.CurrentLine.Y = this.MainChart.ChartAreas[chartarea].AxisY.PixelPositionToValue((double)y);
			this.CurrentLine.Right = this.MainChart.ChartAreas[0].AxisX.PixelPositionToValue((double)x);
			this.CurrentLine.Bottom = this.MainChart.ChartAreas[0].AxisY.PixelPositionToValue((double)y);
			this.CurrentLine.Tag = Tuple.Create<int, int>(x, y);
			this.MainChart.Annotations.Add(this.CurrentLine);
		}
		private void MainChart_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.MainChart.Focus();
			if (e.Button != System.Windows.Forms.MouseButtons.Right || this.MainChart.HitTest(e.X, e.Y).ChartArea == null)
			{
				if (e.Button == System.Windows.Forms.MouseButtons.Left && e.Clicks == 2)
				{
					LineAnnotation lineAnnotation = this.MainChart.HitTest(e.X, e.Y).Object as LineAnnotation;
					if (lineAnnotation != null)
					{
						lineAnnotation.IsInfinitive = !lineAnnotation.IsInfinitive;
					}
				}
				return;
			}
			if (this.CurrentLine == null)
			{
				this.DrawAnnotation(this.MainChart.HitTest(e.X, e.Y).ChartArea.Name, e.X, e.Y);
				return;
			}
			this.CurrentLine.IsInfinitive = true;
			this.CurrentLine = null;
		}
		private void MainChart_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.UnthrottledX = e.X;
			this.UnthrottledY = e.Y;
			if (!this.IsThrottled && this.CurrentLine != null && (this.UnthrottledX != this.ThrottledX || this.UnthrottledY != this.ThrottledY) && this.MainChart.HitTest(this.UnthrottledX, this.UnthrottledY).Object != null)
			{
				this.ThrottledX = this.UnthrottledX;
				this.ThrottledY = this.UnthrottledY;
				try
				{
					double right;
					double bottom;
					if (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Shift)
					{
						Tuple<int, int> tuple = (Tuple<int, int>)this.CurrentLine.Tag;
						if (System.Math.Abs(this.ThrottledX - tuple.Item1) > System.Math.Abs(this.ThrottledY - tuple.Item2))
						{
							right = this.MainChart.ChartAreas[this.CurrentLine.ClipToChartArea].AxisX.PixelPositionToValue((double)this.ThrottledX);
							bottom = this.CurrentLine.Y;
						}
						else
						{
							bottom = this.MainChart.ChartAreas[this.CurrentLine.ClipToChartArea].AxisY.PixelPositionToValue((double)this.ThrottledY);
							right = this.CurrentLine.X;
						}
					}
					else
					{
						right = this.MainChart.ChartAreas[this.CurrentLine.ClipToChartArea].AxisX.PixelPositionToValue((double)this.ThrottledX);
						bottom = this.MainChart.ChartAreas[this.CurrentLine.ClipToChartArea].AxisY.PixelPositionToValue((double)this.ThrottledY);
					}
					this.CurrentLine.Right = right;
					this.CurrentLine.Bottom = bottom;
				}
				catch (System.ArgumentException)
				{
				}
				this.IsThrottled = true;
			}
		}
		private void MainChart_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (this.CurrentLine != null && e.X != ((Tuple<int, int>)this.CurrentLine.Tag).Item1 && e.Y != ((Tuple<int, int>)this.CurrentLine.Tag).Item2)
			{
				this.CurrentLine.IsInfinitive = true;
				this.CurrentLine = null;
			}
		}
		private void MainChart_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (this.CurrentLine == null)
			{
				if (e.KeyCode == System.Windows.Forms.Keys.Delete)
				{
					Annotation annotation = this.MainChart.Annotations.FirstOrDefault((Annotation x) => x.IsSelected);
					if (annotation != null)
					{
						this.MainChart.Annotations.Remove(annotation);
						return;
					}
				}
				else
				{
					if (e.KeyCode == System.Windows.Forms.Keys.Back && this.MainChart.Annotations.Count > 0)
					{
						this.MainChart.Annotations.RemoveAt(this.MainChart.Annotations.Count - 1);
					}
				}
			}
		}
		private void UndoButton_Click(object sender, System.EventArgs e)
		{
			if (this.MainChart.Annotations.Count > 0)
			{
				this.MainChart.Annotations.RemoveAt(this.MainChart.Annotations.Count - 1);
			}
		}
		private void MainChart_Leave(object sender, System.EventArgs e)
		{
			foreach (Annotation current in this.MainChart.Annotations)
			{
				current.IsSelected = false;
			}
		}
		private void DrawingColorDropDown_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			base.OnPaint(e);
			System.Windows.Forms.ToolStripItem toolStripItem = (System.Windows.Forms.ToolStripItem)sender;
			using (System.Drawing.Brush brush = new System.Drawing.SolidBrush(this.CurrentColor))
			{
				using (System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.Black))
				{
					System.Drawing.Rectangle rect = new System.Drawing.Rectangle(toolStripItem.Width / 4, toolStripItem.Height / 4, toolStripItem.Width / 2, toolStripItem.Height / 2);
					e.Graphics.FillRectangle(brush, rect);
					e.Graphics.DrawRectangle(pen, rect);
				}
			}
		}
		private void DrawingColorDropDown_Click(object sender, System.EventArgs e)
		{
			this.DrawingColorDialog.Color = this.CurrentColor;
			if (this.DrawingColorDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				this.CurrentColor = this.DrawingColorDialog.Color;
				((System.Windows.Forms.ToolStripItem)sender).Invalidate();
			}
		}
		private void MouseMoveThrottle_Tick(object sender, System.EventArgs e)
		{
			this.IsThrottled = false;
			this.MainChart_MouseMove(this.MainChart, new System.Windows.Forms.MouseEventArgs(System.Windows.Forms.MouseButtons.None, 0, this.UnthrottledX, this.UnthrottledY, 0));
		}
		private void SecurityTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.LastEnteredExpression = this.ExpressionTextBox.Text.Trim();
				this.TryLoadExpression();
			}
		}
		private void TryLoadExpression()
		{
			try
			{
				this.LoadExpression(this.LastEnteredExpression);
			}
			catch (System.Exception ex)
			{
				DialogHelper.ShowError(ex.Message, "Error");
			}
		}
		private void LoadExpression(string expression)
		{
			try
			{
				if (this.CurrentExpression != null)
				{
					this.CurrentExpression.Dispose();
				}
				foreach (Series current in this.MainChart.Series)
				{
					current.Points.Clear();
				}
				this.MainChart.Annotations.Clear();
				this.MainChart.ChartAreas["Main"].AxisX.ScaleView.ZoomReset(0);
				this.CurrentExpression = new ExpressionChartItem(expression, this.CurrentPeriod);
				this.DrawChart();
			}
			catch (System.Exception ex)
			{
				if (this.CurrentExpression != null)
				{
					this.CurrentExpression.Dispose();
				}
				this.CurrentExpression = null;
				throw ex;
			}
		}
		private void SetXAxis(double min, double max)
		{
			foreach (ChartArea current in this.MainChart.ChartAreas)
			{
				current.AxisX.Minimum = min;
				current.AxisX.Maximum = max;
			}
		}
		private void DrawChart()
		{
			ExpressionChartItem.PlotData groups = this.CurrentExpression.GetGroups(this.CurrentGrouping);
			this.MainChart.Series["HLOC"].Points.DataBindXY(groups.X, groups.Y);
			for (int i = 0; i < this.MainChart.Series["HLOC"].Points.Count; i++)
			{
				if (!groups.IsValid[i])
				{
					this.MainChart.Series["HLOC"].Points[i].IsEmpty = true;
				}
			}
			this.MainChart.ChartAreas["Main"].AxisY.Title = this.CurrentExpression.OriginalExpressionString;
			this.MainChart.ChartAreas["Main"].AxisY.TitleAlignment = System.Drawing.StringAlignment.Center;
			this.SetXAxis(System.Convert.ToDouble(groups.MinTick - 1), System.Convert.ToDouble(groups.MinTick - 1) + (double)this.CurrentExpression.InitialTicks + System.Math.Ceiling(System.Math.Max(System.Convert.ToDouble(this.CurrentExpression.TicksAvailable - this.CurrentExpression.InitialTicks) / (double)Game.State.General.TicksPerPeriod, 1.0)) * (double)Game.State.General.TicksPerPeriod);
			foreach (ChartArea current in this.MainChart.ChartAreas)
			{
				current.AxisX.ScaleView.MinSize = (this.MainChart.ChartAreas["Main"].AxisX.Maximum - this.MainChart.ChartAreas["Main"].AxisX.Minimum) / 5.0;
			}
			this.MainChart.ChartAreas["Main"].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
			this.MainChart.ChartAreas["Main"].AddTickLabels(Game.State.General.TicksPerPeriod, Game.State.General.Periods, groups.MinTick < 1, true);
			this.MainChart.ChartAreas["Main"].AxisY.LabelStyle.Format = this.CurrentExpression.FormatString;
			if (this.CurrentExpression.VolumeChart != null)
			{
				ExpressionChartItem.VolumeData volume = this.CurrentExpression.GetVolume();
				this.MainChart.Series["Volume"].Points.DataBindXY(volume.X, new System.Collections.IEnumerable[]
				{
					volume.Y
				});
				for (int j = 0; j < this.MainChart.Series["Volume"].Points.Count; j++)
				{
					if (!volume.IsValid[j])
					{
						this.MainChart.Series["Volume"].Points[j].IsEmpty = true;
					}
				}
				this.MainChart.ChartAreas["Volume"].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
				this.MainChart.ChartAreas["Volume"].AxisY.TitleAlignment = System.Drawing.StringAlignment.Center;
				this.MainChart.ChartAreas["Volume"].AddTickLabels(Game.State.General.TicksPerPeriod, Game.State.General.Periods, groups.MinTick < 1, false);
				this.CurrentExpression.VolumeDataUpdated += delegate(ExpressionChartItem.VolumeData updatedata)
				{
					this.MainChart.Series["Volume"].Points.SuspendUpdates();
					for (int k = 0; k < updatedata.X.Length; k++)
					{
						this.MainChart.Series["Volume"].Points[updatedata.X[k]].SetValueY(new object[]
						{
							updatedata.Y[k]
						});
						this.MainChart.Series["Volume"].Points[updatedata.X[k]].IsEmpty = false;
					}
					this.MainChart.Series["Volume"].Points.ResumeUpdates();
					this.SetYAxis("Volume");
				};
				this.SetYAxis("Volume");
				this.volumeToolStripMenuItem.Enabled = true;
			}
			else
			{
				this.volumeToolStripMenuItem.Enabled = false;
				this.MainChart.ChartAreas["Volume"].Visible = false;
				if (this.RemoveIndicatorDropDown.DropDownItems.ContainsKey("Volume"))
				{
					this.RemoveIndicatorDropDown.DropDownItems.RemoveByKey("Volume");
				}
			}
			foreach (int current2 in this.MovingAverageSpans)
			{
				this.InitializeMovingAverage(current2);
			}
			foreach (int current3 in this.EMASpans)
			{
				this.InitializeEMA(current3);
			}
			foreach (int current4 in this.RSISpans)
			{
				this.InitializeRSI(current4);
			}
			this.MainChart.ChartAreas["RSI"].Visible = (this.RSISpans.Count > 0);
			this.MainChart.FormatChartAreaSize();
			this.SetYAxis("Main");
			this.CurrentExpression.GroupDataUpdated += delegate(ExpressionChartItem.PlotData updatedata)
			{
				this.MainChart.Series["HLOC"].Points.SuspendUpdates();
				for (int k = 0; k < updatedata.X.Length; k++)
				{
					this.MainChart.Series["HLOC"].Points[updatedata.X[k]].SetValueY(new object[]
					{
						updatedata.Y[0][k],
						updatedata.Y[1][k],
						updatedata.Y[2][k],
						updatedata.Y[3][k]
					});
					this.MainChart.Series["HLOC"].Points[updatedata.X[k]].IsEmpty = false;
				}
				for (int l = 0; l < updatedata.X.Length; l++)
				{
					foreach (int current5 in this.MovingAverageSpans)
					{
						string name = string.Format("MA_{0}", current5);
						this.MainChart.Series[name].Points[updatedata.X[l]].IsEmpty = false;
						this.MainChart.Series[name].Points[updatedata.X[l]].SetValueY(new object[]
						{
							this.ComputeMovingAverage(updatedata.X[l], current5)
						});
					}
					foreach (int current6 in this.RSISpans)
					{
						string name2 = string.Format("RSI_{0}", current6);
						this.MainChart.Series[name2].Points[updatedata.X[l]].IsEmpty = false;
						this.MainChart.Series[name2].Points[updatedata.X[l]].SetValueY(new object[]
						{
							this.ComputeRSI(updatedata.X[l], current6)
						});
					}
					foreach (int current7 in this.EMASpans)
					{
						string name3 = string.Format("EMA_{0}", current7);
						this.MainChart.Series[name3].Points[updatedata.X[l]].IsEmpty = false;
						this.MainChart.Series[name3].Points[updatedata.X[l]].SetValueY(new object[]
						{
							this.ComputeEMA(updatedata.X[l], current7)
						});
					}
				}
				this.MainChart.Series["HLOC"].Points.ResumeUpdates();
				foreach (ChartArea current8 in this.MainChart.ChartAreas)
				{
					current8.AxisX.Maximum = this.MainChart.ChartAreas["Main"].AxisX.Minimum + (double)this.CurrentExpression.InitialTicks + System.Math.Ceiling(System.Math.Max(System.Convert.ToDouble(this.CurrentExpression.TicksAvailable - this.CurrentExpression.InitialTicks) / (double)Game.State.General.TicksPerPeriod, 1.0)) * (double)Game.State.General.TicksPerPeriod;
				}
				this.SetYAxis("Main");
			};
		}
		private void SetYAxis(string area)
		{
			double num = -1.7976931348623157E+308;
			double num2 = 1.7976931348623157E+308;
			foreach (Series current in 
				from x in this.MainChart.Series
				where x.ChartArea == area
				select x)
			{
				if (current.Points.Count > 0)
				{
					DataPoint[] array = current.Points.Where((DataPoint x) => !x.IsEmpty && x.XValue > this.MainChart.ChartAreas[area].AxisX.ScaleView.ViewMinimum && x.XValue < this.MainChart.ChartAreas[area].AxisX.ScaleView.ViewMaximum).ToArray<DataPoint>();
					if (array.Length > 0)
					{
						double y;
						num = System.Math.Max(array.Max((DataPoint x) => x.YValues.Max((double y) => y)), num);
						num2 = System.Math.Min(array.Min((DataPoint x) => x.YValues.Min((double y) => y)), num2);
					}
				}
			}
			if (num != -1.7976931348623157E+308 && num2 != 1.7976931348623157E+308)
			{
				if (area == "Volume")
				{
					num2 = 0.0;
				}
				double num3 = num - num2;
				if (num3 == 0.0)
				{
					num3 = 1.0;
				}
				double y = System.Math.Ceiling(System.Math.Log10(num3)) - 1.0;
				num3 = System.Math.Round(num3 / System.Math.Pow(10.0, y)) * System.Math.Pow(10.0, y);
				num = (System.Math.Ceiling(num / (num3 / 10.0)) + 1.0) * (num3 / 10.0);
				num2 = (System.Math.Floor(num2 / (num3 / 10.0)) - 1.0) * (num3 / 10.0);
				if (area == "Volume")
				{
					num2 = 0.0;
					string format = "";
					if (num > 10000.0)
					{
						format = "#,K;;0";
					}
					if (num > 10000000.0)
					{
						format = "#,,M;;0";
					}
					this.MainChart.ChartAreas[area].AxisY.LabelStyle.Format = format;
				}
				this.MainChart.ChartAreas[area].AxisY.Maximum = num;
				this.MainChart.ChartAreas[area].AxisY.Minimum = num2;
				return;
			}
			this.MainChart.ChartAreas[area].AxisY.Maximum = double.NaN;
			this.MainChart.ChartAreas[area].AxisY.Minimum = double.NaN;
		}
		private void volumeToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			this.AddVolume();
		}
		private void rSIToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			PromptDialog promptDialog = new PromptDialog("RSI Span", "", new System.Windows.Forms.Control[]
			{
				new System.Windows.Forms.NumericUpDown
				{
					Minimum = 1m,
					Increment = 1m,
					Maximum = 2147483647m,
					Value = 10m
				}
			}, new string[]
			{
				"# of Bars"
			});
			if (promptDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				this.AddRSI(System.Convert.ToInt32(promptDialog.Values[0]));
			}
		}
		private void movingAverageToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			PromptDialog promptDialog = new PromptDialog("Moving Average Span", "", new System.Windows.Forms.Control[]
			{
				new System.Windows.Forms.NumericUpDown
				{
					Minimum = 1m,
					Increment = 1m,
					Maximum = 2147483647m,
					Value = 10m
				}
			}, new string[]
			{
				"# of Bars"
			});
			if (promptDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				this.AddMovingAverage(System.Convert.ToInt32(promptDialog.Values[0]));
			}
		}
		private void exponentialMovingAverageToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			PromptDialog promptDialog = new PromptDialog("Exponential Moving Average Span", "", new System.Windows.Forms.Control[]
			{
				new System.Windows.Forms.NumericUpDown
				{
					Minimum = 1m,
					Increment = 1m,
					Maximum = 2147483647m,
					Value = 10m
				}
			}, new string[]
			{
				"# of Bars"
			});
			if (promptDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				this.AddEMA(System.Convert.ToInt32(promptDialog.Values[0]));
			}
		}
		private void AddVolume()
		{
			if (!this.MainChart.ChartAreas["Volume"].Visible)
			{
				this.MainChart.ChartAreas["Volume"].Visible = true;
				this.MainChart.FormatChartAreaSize();
				this.MainChart.ChartAreas["Volume"].AxisX.ScaleView.Zoom(this.MainChart.ChartAreas["Main"].AxisX.ScaleView.ViewMinimum, this.MainChart.ChartAreas["Main"].AxisX.ScaleView.ViewMaximum);
				this.SetYAxis("Volume");
				System.Windows.Forms.ToolStripItem toolStripItem = this.RemoveIndicatorDropDown.DropDownItems.Add("Volume");
				toolStripItem.Click += delegate(object sender, System.EventArgs e)
				{
					this.RemoveIndicatorDropDown.DropDownItems.Remove((System.Windows.Forms.ToolStripItem)sender);
					this.MainChart.ChartAreas["Volume"].Visible = false;
					this.MainChart.FormatChartAreaSize();
				};
			}
		}
		private void AddMovingAverage(int bars)
		{
			if (!this.MovingAverageSpans.Contains(bars))
			{
				string name = string.Format("MA_{0}", bars);
				this.MainChart.Series.Add(name);
				this.MainChart.Series[name].ChartType = SeriesChartType.Line;
				this.MainChart.Series[name].Color = ChartColorPalette.BrightPastel.GetColorPaletteColor(this.MainChart.Series.Count - 1);
				this.MainChart.Series[name].Legend = "Indicators";
				this.MainChart.Series[name].LegendText = string.Format("MA({0})", bars);
				this.MainChart.Series[name].BorderWidth = 2;
				this.MovingAverageSpans.Add(bars);
				System.Windows.Forms.ToolStripItem toolStripItem = this.RemoveIndicatorDropDown.DropDownItems.Add(this.MainChart.Series[name].LegendText);
				toolStripItem.Click += delegate(object sender, System.EventArgs e)
				{
					this.RemoveIndicatorDropDown.DropDownItems.Remove((System.Windows.Forms.ToolStripItem)sender);
					this.MainChart.Series.Remove(this.MainChart.Series.FindByName(name));
					this.MovingAverageSpans.Remove(bars);
				};
				this.InitializeMovingAverage(bars);
			}
		}
		private void AddRSI(int bars)
		{
			if (!this.RSISpans.Contains(bars))
			{
				string name = string.Format("RSI_{0}", bars);
				this.MainChart.Series.Add(name);
				this.MainChart.Series[name].ChartArea = "RSI";
				this.MainChart.Series[name].ChartType = SeriesChartType.Line;
				this.MainChart.Series[name].Color = ChartColorPalette.BrightPastel.GetColorPaletteColor(this.MainChart.Series.Count - 1);
				this.MainChart.Series[name].Legend = "Indicators";
				this.MainChart.Series[name].LegendText = string.Format("RSI({0})", bars);
				this.MainChart.Series[name].BorderWidth = 2;
				this.RSISpans.Add(bars);
				System.Windows.Forms.ToolStripItem toolStripItem = this.RemoveIndicatorDropDown.DropDownItems.Add(this.MainChart.Series[name].LegendText);
				toolStripItem.Click += delegate(object sender, System.EventArgs e)
				{
					this.RemoveIndicatorDropDown.DropDownItems.Remove((System.Windows.Forms.ToolStripItem)sender);
					this.MainChart.Series.Remove(this.MainChart.Series.FindByName(name));
					this.RSISpans.Remove(bars);
					if (this.RSISpans.Count == 0)
					{
						this.MainChart.ChartAreas["RSI"].Visible = false;
						this.MainChart.FormatChartAreaSize();
					}
				};
				this.InitializeRSI(bars);
			}
		}
		private void AddEMA(int bars)
		{
			if (!this.EMASpans.Contains(bars))
			{
				string name = string.Format("EMA_{0}", bars);
				this.MainChart.Series.Add(name);
				this.MainChart.Series[name].ChartType = SeriesChartType.Line;
				this.MainChart.Series[name].Color = ChartColorPalette.BrightPastel.GetColorPaletteColor(this.MainChart.Series.Count - 1);
				this.MainChart.Series[name].Legend = "Indicators";
				this.MainChart.Series[name].LegendText = string.Format("EMA({0})", bars);
				this.MainChart.Series[name].BorderWidth = 2;
				this.EMASpans.Add(bars);
				System.Windows.Forms.ToolStripItem toolStripItem = this.RemoveIndicatorDropDown.DropDownItems.Add(this.MainChart.Series[name].LegendText);
				toolStripItem.Click += delegate(object sender, System.EventArgs e)
				{
					this.RemoveIndicatorDropDown.DropDownItems.Remove((System.Windows.Forms.ToolStripItem)sender);
					this.MainChart.Series.Remove(this.MainChart.Series.FindByName(name));
					this.EMASpans.Remove(bars);
				};
				this.InitializeEMA(bars);
			}
		}
		private void InitializeMovingAverage(int span)
		{
			string name = string.Format("MA_{0}", span);
			this.MainChart.Series[name].Points.Clear();
			foreach (DataPoint current in this.MainChart.Series["HLOC"].Points)
			{
				this.MainChart.Series[name].Points.Add(new DataPoint(current.XValue, current.YValues[0])
				{
					IsEmpty = true
				});
			}
			int num = 0;
			while (num < this.MainChart.Series[name].Points.Count && !this.MainChart.Series["HLOC"].Points[num].IsEmpty)
			{
				this.MainChart.Series[name].Points[num].IsEmpty = false;
				this.MainChart.Series[name].Points[num].SetValueY(new object[]
				{
					this.ComputeMovingAverage(num, span)
				});
				num++;
			}
		}
		private void InitializeRSI(int span)
		{
			string name = string.Format("RSI_{0}", span);
			this.MainChart.Series[name].Points.Clear();
			foreach (DataPoint current in this.MainChart.Series["HLOC"].Points)
			{
				this.MainChart.Series[name].Points.Add(new DataPoint(current.XValue, current.YValues[0])
				{
					IsEmpty = true
				});
			}
			int num = 0;
			while (num < this.MainChart.Series[name].Points.Count && !this.MainChart.Series["HLOC"].Points[num].IsEmpty)
			{
				this.MainChart.Series[name].Points[num].IsEmpty = false;
				this.MainChart.Series[name].Points[num].SetValueY(new object[]
				{
					this.ComputeRSI(num, span)
				});
				num++;
			}
			if (!this.MainChart.ChartAreas["RSI"].Visible)
			{
				this.MainChart.ChartAreas["RSI"].Visible = true;
				this.MainChart.FormatChartAreaSize();
				this.MainChart.ChartAreas["RSI"].AxisX.ScaleView.Zoom(this.MainChart.ChartAreas["Main"].AxisX.ScaleView.ViewMinimum, this.MainChart.ChartAreas["Main"].AxisX.ScaleView.ViewMaximum);
				this.SetYAxis("RSI");
			}
		}
		private void InitializeEMA(int span)
		{
			string name = string.Format("EMA_{0}", span);
			this.MainChart.Series[name].Points.Clear();
			foreach (DataPoint current in this.MainChart.Series["HLOC"].Points)
			{
				this.MainChart.Series[name].Points.Add(new DataPoint(current.XValue, current.YValues[0])
				{
					IsEmpty = true
				});
			}
			int num = 0;
			while (num < this.MainChart.Series[name].Points.Count && !this.MainChart.Series["HLOC"].Points[num].IsEmpty)
			{
				this.MainChart.Series[name].Points[num].IsEmpty = false;
				this.MainChart.Series[name].Points[num].SetValueY(new object[]
				{
					this.ComputeEMA(num, span)
				});
				num++;
			}
		}
		private double ComputeMovingAverage(int index, int span)
		{
			double num = 0.0;
			for (int i = System.Math.Max(index - (span - 1), 0); i <= index; i++)
			{
				num += this.MainChart.Series["HLOC"].Points[i].YValues[3];
			}
			return num / (double)(index - System.Math.Max(index - (span - 1), 0) + 1);
		}
		private double ComputeRSI(int index, int span)
		{
			span = System.Math.Min(span, index + 1);
			double num = 2.0 / (1.0 + (double)span);
			double num2 = 0.0;
			double num3 = 0.0;
			double num4 = num;
			int num5 = index;
			while (num5 >= 1 && num4 > 0.001)
			{
				double num6 = this.MainChart.Series["HLOC"].Points[num5].YValues[3] - this.MainChart.Series["HLOC"].Points[num5 - 1].YValues[3];
				if (num6 > 0.0)
				{
					num2 += num6 * num4;
				}
				else
				{
					num3 -= num6 * num4;
				}
				num4 *= 1.0 - num;
				num5--;
			}
			if (num3 < 4.94065645841247E-322)
			{
				return 100.0;
			}
			return 100.0 - 100.0 / (1.0 + num2 / num3);
		}
		private double ComputeEMA(int index, int span)
		{
			if (index + 1 <= span)
			{
				return this.ComputeMovingAverage(index, span);
			}
			double num = 2.0 / (1.0 + (double)span);
			string name = string.Format("EMA_{0}", span);
			return this.MainChart.Series[name].Points[index - 1].YValues[0] + num * (this.MainChart.Series["HLOC"].Points[index].YValues[3] - this.MainChart.Series[name].Points[index - 1].YValues[0]);
		}
		private void clearAllToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			while (this.RemoveIndicatorDropDown.DropDownItems.Count > 2)
			{
				this.RemoveIndicatorDropDown.DropDownItems[2].PerformClick();
			}
		}
		private void ZoomOutButton_Click(object sender, System.EventArgs e)
		{
			this.MainChart.ChartAreas["Main"].AxisX.ScaleView.ZoomReset(0);
			this.SetYAxis("Main");
			this.SetYAxis("Volume");
		}
		private void IsZoomableButton_Click(object sender, System.EventArgs e)
		{
			if (!this.IsZoomableButton.Checked)
			{
				this.ZoomOutButton.PerformClick();
			}
			foreach (ChartArea current in this.MainChart.ChartAreas)
			{
				current.AxisX.ScaleView.Zoomable = this.IsZoomableButton.Checked;
				current.CursorX.IsUserSelectionEnabled = this.IsZoomableButton.Checked;
				current.CursorX.IsUserEnabled = true;
			}
		}
		private void ChartScrollThrottle_Tick(object sender, System.EventArgs e)
		{
			this.IsZoomThrottled = false;
			if (this.UnthrottledPosition != this.ThrottledPosition)
			{
				this.MainChart_AxisViewChanged(null, new ViewEventArgs(null, this.UnthrottledPosition, 0.0, DateTimeIntervalType.NotSet));
			}
		}
		private void MainChart_AxisViewChanged(object sender, ViewEventArgs e)
		{
			this.UnthrottledPosition = e.NewPosition;
			if (!this.IsZoomThrottled && this.UnthrottledPosition != this.ThrottledPosition)
			{
				this.SetYAxis("Main");
				this.SetYAxis("Volume");
				this.ThrottledPosition = this.UnthrottledPosition;
				this.IsZoomThrottled = true;
			}
		}
		private void SecurityCharting_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			this.ExpressionTextBox.Text = ((TickerString)e.Data.GetData("TTS.TickerString")).ToChartString();
			this.LastEnteredExpression = this.ExpressionTextBox.Text.Trim();
			base.BeginInvoke(new System.Windows.Forms.MethodInvoker(this.TryLoadExpression));
		}
		private void SecurityCharting_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if (e.Data.GetData(typeof(string)) != null)
			{
				e.Effect = System.Windows.Forms.DragDropEffects.Copy;
			}
		}
		private void ExpressionTextBox_TextChanged(object sender, System.EventArgs e)
		{
			this.LastEnteredExpression = null;
		}
		private void copyImageToClipboardToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
			{
				this.MainChart.SaveImage(memoryStream, ChartImageFormat.Bmp);
				System.Drawing.Bitmap image = new System.Drawing.Bitmap(memoryStream);
				System.Windows.Forms.Clipboard.SetImage(image);
			}
		}
		private void saveImageToFilepngToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			if (this.MainChartSaveFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				try
				{
					this.MainChart.SaveImage(this.MainChartSaveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
				}
				catch (System.Exception ex)
				{
					DialogHelper.ShowError(ex.Message, "Error");
				}
			}
		}
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(SecurityCharting));
			this.MouseMoveThrottle = new System.Windows.Forms.Timer(this.components);
			this.DrawingColorDialog = new System.Windows.Forms.ColorDialog();
			this.MainChart = new Chart();
			this.MainStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.ExpressionTextBox = new System.Windows.Forms.ToolStripTextBox();
			this.TickerDropDown = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
			this.PeriodDropDown = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
			this.GroupDropDown = new System.Windows.Forms.ToolStripDropDownButton();
			this.Group15MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.Group30MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.Group60MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.Group120MenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.customToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
			this.AddIndicatorDropDown = new System.Windows.Forms.ToolStripDropDownButton();
			this.bollingerBandsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exponentialMovingAverageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mACDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.movingAverageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.rSIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stochasticOscillatorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.simpleStochasticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.smoothStochasticToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.volumeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.RemoveIndicatorDropDown = new System.Windows.Forms.ToolStripDropDownButton();
			this.clearAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
			this.DrawingColorDropDown = new System.Windows.Forms.ToolStripButton();
			this.UndoButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.IsZoomableButton = new System.Windows.Forms.ToolStripButton();
			this.ZoomOutButton = new System.Windows.Forms.ToolStripButton();
			this.ExportSplitButton = new System.Windows.Forms.ToolStripDropDownButton();
			this.copyImageToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveImageToFilepngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ChartScrollThrottle = new System.Windows.Forms.Timer(this.components);
			this.MainChartSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			((ISupportInitialize)this.MainChart).BeginInit();
			this.MainStrip.SuspendLayout();
			base.SuspendLayout();
			this.MouseMoveThrottle.Enabled = true;
			this.MouseMoveThrottle.Tick += new System.EventHandler(this.MouseMoveThrottle_Tick);
			this.DrawingColorDialog.AnyColor = true;
			this.MainChart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainChart.Location = new System.Drawing.Point(0, 25);
			this.MainChart.Name = "MainChart";
			this.MainChart.Size = new System.Drawing.Size(624, 417);
			this.MainChart.TabIndex = 2;
			this.MainChart.Text = "chart1";
			this.MainChart.AxisViewChanged += new System.EventHandler<ViewEventArgs>(this.MainChart_AxisViewChanged);
			this.MainChart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainChart_KeyDown);
			this.MainChart.Leave += new System.EventHandler(this.MainChart_Leave);
			this.MainChart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MainChart_MouseDown);
			this.MainChart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainChart_MouseMove);
			this.MainChart.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainChart_MouseUp);
			this.MainStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripLabel1,
				this.ExpressionTextBox,
				this.TickerDropDown,
				this.toolStripSeparator1,
				this.toolStripLabel3,
				this.PeriodDropDown,
				this.toolStripLabel2,
				this.GroupDropDown,
				this.toolStripSeparator4,
				this.toolStripLabel5,
				this.AddIndicatorDropDown,
				this.RemoveIndicatorDropDown,
				this.toolStripSeparator5,
				this.toolStripLabel6,
				this.DrawingColorDropDown,
				this.UndoButton,
				this.toolStripSeparator2,
				this.IsZoomableButton,
				this.ZoomOutButton,
				this.ExportSplitButton
			});
			this.MainStrip.Location = new System.Drawing.Point(0, 0);
			this.MainStrip.Name = "MainStrip";
			this.MainStrip.Size = new System.Drawing.Size(624, 25);
			this.MainStrip.TabIndex = 0;
			this.MainStrip.Text = "MainToolStrip";
			this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(56, 22);
			this.toolStripLabel1.Text = "Security:";
			this.ExpressionTextBox.Name = "ExpressionTextBox";
			this.ExpressionTextBox.Size = new System.Drawing.Size(100, 25);
			this.ExpressionTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SecurityTextBox_KeyPress);
			this.ExpressionTextBox.TextChanged += new System.EventHandler(this.ExpressionTextBox_TextChanged);
			this.TickerDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.TickerDropDown.Image = Resources.add;
			this.TickerDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.TickerDropDown.Name = "TickerDropDown";
			this.TickerDropDown.Size = new System.Drawing.Size(29, 22);
			this.TickerDropDown.Text = "Add Ticker";
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			this.toolStripLabel3.Name = "toolStripLabel3";
			this.toolStripLabel3.Size = new System.Drawing.Size(17, 22);
			this.toolStripLabel3.Text = "P:";
			this.PeriodDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.PeriodDropDown.Image = (System.Drawing.Image)componentResourceManager.GetObject("PeriodDropDown.Image");
			this.PeriodDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.PeriodDropDown.Name = "PeriodDropDown";
			this.PeriodDropDown.Size = new System.Drawing.Size(40, 22);
			this.PeriodDropDown.Text = "ALL";
			this.PeriodDropDown.ToolTipText = "Charting Period";
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new System.Drawing.Size(16, 22);
			this.toolStripLabel2.Text = "S:";
			this.GroupDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.GroupDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.Group15MenuItem,
				this.Group30MenuItem,
				this.Group60MenuItem,
				this.Group120MenuItem,
				this.customToolStripMenuItem
			});
			this.GroupDropDown.Image = (System.Drawing.Image)componentResourceManager.GetObject("GroupDropDown.Image");
			this.GroupDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.GroupDropDown.Name = "GroupDropDown";
			this.GroupDropDown.Size = new System.Drawing.Size(32, 22);
			this.GroupDropDown.Text = "10";
			this.GroupDropDown.ToolTipText = "Bar Size";
			this.Group15MenuItem.Name = "Group15MenuItem";
			this.Group15MenuItem.Size = new System.Drawing.Size(125, 22);
			this.Group15MenuItem.Text = "5";
			this.Group15MenuItem.Click += new System.EventHandler(this.GroupMenuItem_Click);
			this.Group30MenuItem.Name = "Group30MenuItem";
			this.Group30MenuItem.Size = new System.Drawing.Size(125, 22);
			this.Group30MenuItem.Text = "10";
			this.Group30MenuItem.Click += new System.EventHandler(this.GroupMenuItem_Click);
			this.Group60MenuItem.Name = "Group60MenuItem";
			this.Group60MenuItem.Size = new System.Drawing.Size(125, 22);
			this.Group60MenuItem.Text = "30";
			this.Group60MenuItem.Click += new System.EventHandler(this.GroupMenuItem_Click);
			this.Group120MenuItem.Name = "Group120MenuItem";
			this.Group120MenuItem.Size = new System.Drawing.Size(125, 22);
			this.Group120MenuItem.Text = "60";
			this.Group120MenuItem.Click += new System.EventHandler(this.GroupMenuItem_Click);
			this.customToolStripMenuItem.Name = "customToolStripMenuItem";
			this.customToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
			this.customToolStripMenuItem.Text = "Custom...";
			this.customToolStripMenuItem.Click += new System.EventHandler(this.customToolStripMenuItem_Click);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			this.toolStripLabel5.Name = "toolStripLabel5";
			this.toolStripLabel5.Size = new System.Drawing.Size(27, 22);
			this.toolStripLabel5.Text = "Ind:";
			this.AddIndicatorDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AddIndicatorDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.bollingerBandsToolStripMenuItem,
				this.exponentialMovingAverageToolStripMenuItem,
				this.mACDToolStripMenuItem,
				this.movingAverageToolStripMenuItem,
				this.rSIToolStripMenuItem,
				this.stochasticOscillatorToolStripMenuItem,
				this.volumeToolStripMenuItem
			});
			this.AddIndicatorDropDown.Image = Resources.add;
			this.AddIndicatorDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AddIndicatorDropDown.Name = "AddIndicatorDropDown";
			this.AddIndicatorDropDown.Size = new System.Drawing.Size(29, 22);
			this.AddIndicatorDropDown.Text = "Add Indicator";
			this.AddIndicatorDropDown.ToolTipText = "Add Indicator";
			this.bollingerBandsToolStripMenuItem.Enabled = false;
			this.bollingerBandsToolStripMenuItem.Name = "bollingerBandsToolStripMenuItem";
			this.bollingerBandsToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
			this.bollingerBandsToolStripMenuItem.Text = "Bollinger Bands";
			this.bollingerBandsToolStripMenuItem.Visible = false;
			this.exponentialMovingAverageToolStripMenuItem.Name = "exponentialMovingAverageToolStripMenuItem";
			this.exponentialMovingAverageToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
			this.exponentialMovingAverageToolStripMenuItem.Text = "Exponential Moving Average";
			this.exponentialMovingAverageToolStripMenuItem.Click += new System.EventHandler(this.exponentialMovingAverageToolStripMenuItem_Click);
			this.mACDToolStripMenuItem.Enabled = false;
			this.mACDToolStripMenuItem.Name = "mACDToolStripMenuItem";
			this.mACDToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
			this.mACDToolStripMenuItem.Text = "MACD";
			this.mACDToolStripMenuItem.Visible = false;
			this.movingAverageToolStripMenuItem.Name = "movingAverageToolStripMenuItem";
			this.movingAverageToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
			this.movingAverageToolStripMenuItem.Text = "Moving Average";
			this.movingAverageToolStripMenuItem.Click += new System.EventHandler(this.movingAverageToolStripMenuItem_Click);
			this.rSIToolStripMenuItem.Name = "rSIToolStripMenuItem";
			this.rSIToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
			this.rSIToolStripMenuItem.Text = "RSI";
			this.rSIToolStripMenuItem.Click += new System.EventHandler(this.rSIToolStripMenuItem_Click);
			this.stochasticOscillatorToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.simpleStochasticToolStripMenuItem,
				this.smoothStochasticToolStripMenuItem
			});
			this.stochasticOscillatorToolStripMenuItem.Enabled = false;
			this.stochasticOscillatorToolStripMenuItem.Name = "stochasticOscillatorToolStripMenuItem";
			this.stochasticOscillatorToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
			this.stochasticOscillatorToolStripMenuItem.Text = "Stochastic Oscillator";
			this.stochasticOscillatorToolStripMenuItem.Visible = false;
			this.simpleStochasticToolStripMenuItem.Name = "simpleStochasticToolStripMenuItem";
			this.simpleStochasticToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.simpleStochasticToolStripMenuItem.Text = "Simple Stochastic";
			this.smoothStochasticToolStripMenuItem.Name = "smoothStochasticToolStripMenuItem";
			this.smoothStochasticToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.smoothStochasticToolStripMenuItem.Text = "Smooth Stochastic";
			this.volumeToolStripMenuItem.Name = "volumeToolStripMenuItem";
			this.volumeToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
			this.volumeToolStripMenuItem.Text = "Volume";
			this.volumeToolStripMenuItem.Click += new System.EventHandler(this.volumeToolStripMenuItem_Click);
			this.RemoveIndicatorDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.RemoveIndicatorDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.clearAllToolStripMenuItem,
				this.toolStripSeparator3
			});
			this.RemoveIndicatorDropDown.Image = Resources.delete;
			this.RemoveIndicatorDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.RemoveIndicatorDropDown.Name = "RemoveIndicatorDropDown";
			this.RemoveIndicatorDropDown.Size = new System.Drawing.Size(29, 22);
			this.RemoveIndicatorDropDown.Text = "Remove Indicator";
			this.RemoveIndicatorDropDown.ToolTipText = "Remove Indicator";
			this.clearAllToolStripMenuItem.Name = "clearAllToolStripMenuItem";
			this.clearAllToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
			this.clearAllToolStripMenuItem.Text = "Clear All";
			this.clearAllToolStripMenuItem.Click += new System.EventHandler(this.clearAllToolStripMenuItem_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(115, 6);
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			this.toolStripLabel6.Name = "toolStripLabel6";
			this.toolStripLabel6.Size = new System.Drawing.Size(54, 22);
			this.toolStripLabel6.Text = "Drawing:";
			this.DrawingColorDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.DrawingColorDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.DrawingColorDropDown.Name = "DrawingColorDropDown";
			this.DrawingColorDropDown.Size = new System.Drawing.Size(23, 22);
			this.DrawingColorDropDown.Text = "Line Color";
			this.DrawingColorDropDown.Click += new System.EventHandler(this.DrawingColorDropDown_Click);
			this.DrawingColorDropDown.Paint += new System.Windows.Forms.PaintEventHandler(this.DrawingColorDropDown_Paint);
			this.UndoButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.UndoButton.Image = Resources.arrow_undo;
			this.UndoButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.UndoButton.Name = "UndoButton";
			this.UndoButton.Size = new System.Drawing.Size(23, 22);
			this.UndoButton.Text = "Undo";
			this.UndoButton.Click += new System.EventHandler(this.UndoButton_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
			this.IsZoomableButton.Checked = true;
			this.IsZoomableButton.CheckOnClick = true;
			this.IsZoomableButton.CheckState = System.Windows.Forms.CheckState.Checked;
			this.IsZoomableButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.IsZoomableButton.Image = Resources.zoom;
			this.IsZoomableButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.IsZoomableButton.Name = "IsZoomableButton";
			this.IsZoomableButton.Size = new System.Drawing.Size(23, 22);
			this.IsZoomableButton.Text = "Enable Zoom";
			this.IsZoomableButton.Click += new System.EventHandler(this.IsZoomableButton_Click);
			this.ZoomOutButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ZoomOutButton.Image = Resources.zoom_out;
			this.ZoomOutButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ZoomOutButton.Name = "ZoomOutButton";
			this.ZoomOutButton.Size = new System.Drawing.Size(23, 22);
			this.ZoomOutButton.Text = "Zoom Out";
			this.ZoomOutButton.Click += new System.EventHandler(this.ZoomOutButton_Click);
			this.ExportSplitButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ExportSplitButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.copyImageToClipboardToolStripMenuItem,
				this.saveImageToFilepngToolStripMenuItem
			});
			this.ExportSplitButton.Image = Resources.page_white_go;
			this.ExportSplitButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ExportSplitButton.Name = "ExportSplitButton";
			this.ExportSplitButton.Size = new System.Drawing.Size(29, 22);
			this.ExportSplitButton.Text = "Export";
			this.copyImageToClipboardToolStripMenuItem.Name = "copyImageToClipboardToolStripMenuItem";
			this.copyImageToClipboardToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.copyImageToClipboardToolStripMenuItem.Text = "Copy Image to Clipboard";
			this.copyImageToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyImageToClipboardToolStripMenuItem_Click);
			this.saveImageToFilepngToolStripMenuItem.Name = "saveImageToFilepngToolStripMenuItem";
			this.saveImageToFilepngToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.saveImageToFilepngToolStripMenuItem.Text = "Save Image to File (.png)";
			this.saveImageToFilepngToolStripMenuItem.Click += new System.EventHandler(this.saveImageToFilepngToolStripMenuItem_Click);
			this.ChartScrollThrottle.Enabled = true;
			this.ChartScrollThrottle.Interval = 250;
			this.ChartScrollThrottle.Tick += new System.EventHandler(this.ChartScrollThrottle_Tick);
			this.MainChartSaveFileDialog.DefaultExt = "png";
			this.MainChartSaveFileDialog.Filter = "PNG files|*.png";
			this.AllowDrop = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(624, 442);
			base.Controls.Add(this.MainChart);
			base.Controls.Add(this.MainStrip);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "SecurityCharting";
			this.Text = "Security Charting";
			base.DragDrop += new System.Windows.Forms.DragEventHandler(this.SecurityCharting_DragDrop);
			base.DragOver += new System.Windows.Forms.DragEventHandler(this.SecurityCharting_DragOver);
			((ISupportInitialize)this.MainChart).EndInit();
			this.MainStrip.ResumeLayout(false);
			this.MainStrip.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
