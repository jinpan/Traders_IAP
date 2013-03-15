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
	public class ElectricityCharting : TTSForm
	{
		private IContainer components;
		private System.Windows.Forms.ToolStrip MainStrip;
		private System.Windows.Forms.ToolStripLabel toolStripLabel3;
		private System.Windows.Forms.ToolStripDropDownButton PeriodDropDown;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton IsZoomableButton;
		private System.Windows.Forms.ToolStripButton ZoomOutButton;
		private Chart MainChart;
		private System.Windows.Forms.SaveFileDialog MainChartSaveFileDialog;
		private System.Windows.Forms.Timer ChartScrollThrottle;
		private System.Windows.Forms.ToolStripDropDownButton ExportSplitButton;
		private System.Windows.Forms.ToolStripMenuItem copyImageToClipboardToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveImageToFilepngToolStripMenuItem;
		public static WindowType TTSWindowType = WindowType.ELECTRICITY_CHARTING;
		private int? CurrentPeriod = null;
		private double ThrottledPosition;
		private double UnthrottledPosition;
		private bool IsZoomThrottled;
		private Queue<Tuple<ElectricityChart, System.Action<double>>> ElectricityEvents = new Queue<Tuple<ElectricityChart, System.Action<double>>>();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(ElectricityCharting));
			this.MainStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
			this.PeriodDropDown = new System.Windows.Forms.ToolStripDropDownButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.IsZoomableButton = new System.Windows.Forms.ToolStripButton();
			this.ZoomOutButton = new System.Windows.Forms.ToolStripButton();
			this.ExportSplitButton = new System.Windows.Forms.ToolStripDropDownButton();
			this.copyImageToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveImageToFilepngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MainChart = new Chart();
			this.MainChartSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.ChartScrollThrottle = new System.Windows.Forms.Timer(this.components);
			this.MainStrip.SuspendLayout();
			((ISupportInitialize)this.MainChart).BeginInit();
			base.SuspendLayout();
			this.MainStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripLabel3,
				this.PeriodDropDown,
				this.toolStripSeparator4,
				this.IsZoomableButton,
				this.ZoomOutButton,
				this.ExportSplitButton
			});
			this.MainStrip.Location = new System.Drawing.Point(0, 0);
			this.MainStrip.Name = "MainStrip";
			this.MainStrip.Size = new System.Drawing.Size(455, 25);
			this.MainStrip.TabIndex = 1;
			this.MainStrip.Text = "MainToolStrip";
			this.toolStripLabel3.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.toolStripLabel3.Name = "toolStripLabel3";
			this.toolStripLabel3.Size = new System.Drawing.Size(46, 22);
			this.toolStripLabel3.Text = "Period:";
			this.PeriodDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.PeriodDropDown.Image = (System.Drawing.Image)componentResourceManager.GetObject("PeriodDropDown.Image");
			this.PeriodDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.PeriodDropDown.Name = "PeriodDropDown";
			this.PeriodDropDown.Size = new System.Drawing.Size(26, 22);
			this.PeriodDropDown.Text = "1";
			this.PeriodDropDown.ToolTipText = "Charting Period";
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
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
			this.MainChart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainChart.Location = new System.Drawing.Point(0, 25);
			this.MainChart.Name = "MainChart";
			this.MainChart.Size = new System.Drawing.Size(455, 237);
			this.MainChart.TabIndex = 3;
			this.MainChart.Text = "chart1";
			this.MainChart.AxisViewChanged += new System.EventHandler<ViewEventArgs>(this.MainChart_AxisViewChanged);
			this.MainChartSaveFileDialog.DefaultExt = "png";
			this.MainChartSaveFileDialog.Filter = "PNG files|*.png";
			this.ChartScrollThrottle.Enabled = true;
			this.ChartScrollThrottle.Interval = 250;
			this.ChartScrollThrottle.Tick += new System.EventHandler(this.ChartScrollThrottle_Tick);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(455, 262);
			base.Controls.Add(this.MainChart);
			base.Controls.Add(this.MainStrip);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "ElectricityCharting";
			this.Text = "Electricity Charting";
			this.MainStrip.ResumeLayout(false);
			this.MainStrip.PerformLayout();
			((ISupportInitialize)this.MainChart).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		public ElectricityCharting(TTSFormState state) : base(state)
		{
			this.InitializeComponent();
			this.MainChart.Format(true);
			this.MainChart.AddChartArea("Main", true);
			this.MainChart.Legends.Add("Main");
			this.MainChart.Legends["Main"].LegendStyle = LegendStyle.Table;
			this.MainChart.Legends["Main"].InsideChartArea = "Main";
			this.MainChart.Legends["Main"].IsTextAutoFit = true;
			this.MainChart.Legends["Main"].Position.Auto = true;
			this.MainChart.Legends["Main"].BackColor = System.Drawing.Color.Transparent;
			this.MainChart.Legends["Main"].Docking = Docking.Top;
			this.MainChart.Legends["Main"].Alignment = System.Drawing.StringAlignment.Near;
			this.MainChart.Legends["Main"].Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.IsZoomableButton.PerformClick();
			base.AddResetHandler(new Action(this.BindPeriodDropDown));
			this.BindPeriodDropDown();
			base.FormClosed += delegate(object sender, System.Windows.Forms.FormClosedEventArgs e)
			{
				this.ClearEvents();
			};
		}
		private void BindPeriodDropDown()
		{
			this.PeriodDropDown.DropDownItems.Clear();
			for (int i = 1; i <= Game.State.General.Periods; i++)
			{
				int period = i;
				System.Windows.Forms.ToolStripItem toolStripItem = this.PeriodDropDown.DropDownItems.Add(period.ToString());
				toolStripItem.Click += delegate(object sender, System.EventArgs e)
				{
					this.SetCurrentPeriod(new int?(period));
				};
			}
			this.SetCurrentPeriod(new int?(1));
		}
		private void SetCurrentPeriod(int? period)
		{
			this.CurrentPeriod = period;
			this.PeriodDropDown.Text = period.ToString();
			try
			{
				this.InitializeElectricityChart();
			}
			catch (System.Exception ex)
			{
				DialogHelper.ShowError(ex.Message, "Error");
			}
		}
		private void ClearEvents()
		{
			while (this.ElectricityEvents.Count > 0)
			{
				Tuple<ElectricityChart, System.Action<double>> tuple = this.ElectricityEvents.Dequeue();
				tuple.Item1.ChartUpdated -= tuple.Item2;
			}
		}
		private void InitializeElectricityChart()
		{
			this.ClearEvents();
			this.MainChart.Series.Clear();
			this.MainChart.ChartAreas["Main"].AxisX.CustomLabels.Clear();
			this.MainChart.ChartAreas["Main"].AxisX.Minimum = 0.0;
			this.MainChart.ChartAreas["Main"].AxisX.Maximum = (double)Game.State.General.TicksPerPeriod;
			this.MainChart.ChartAreas["Main"].AxisX.CustomLabels.Add(0.0, (double)Game.State.General.TicksPerPeriod, this.CurrentPeriod.ToString(), 1, LabelMarkStyle.LineSideMark);
			this.MainChart.ChartAreas["Main"].AxisY.MinorGrid.Enabled = true;
			this.MainChart.ChartAreas["Main"].AxisY.MinorGrid.LineColor = this.MainChart.ChartAreas["Main"].AxisY.MajorGrid.LineColor;
			this.MainChart.ChartAreas["Main"].AxisY.MinorGrid.LineDashStyle = ChartDashStyle.Dot;
			System.Collections.Generic.IEnumerable<ElectricityChart> enumerable = 
				from x in Game.State.ElectricityCharts
				where x.Period == this.CurrentPeriod
				select x;
			foreach (ElectricityChart current in enumerable)
			{
				Series s = this.MainChart.AddSeries(current.Name, "Main", SeriesChartType.FastLine);
				s.Points.DataBindXY(current.Data.Select((double x, int i) => i + 1).ToArray<int>(), new System.Collections.IEnumerable[]
				{
					current.Data
				});
				s.BorderWidth = 2;
				System.Action<double> action = delegate(double x)
				{
					s.Points.AddXY((double)s.Points.Count, x);
				};
				current.ChartUpdated += action;
				this.ElectricityEvents.Enqueue(Tuple.Create<ElectricityChart, System.Action<double>>(current, action));
			}
			this.ZoomOutButton.PerformClick();
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
				double num3 = num - num2;
				if (num3 == 0.0)
				{
					num3 = 1.0;
				}
				double y = System.Math.Ceiling(System.Math.Log10(num3)) - 1.0;
				num3 = System.Math.Round(num3 / System.Math.Pow(10.0, y)) * System.Math.Pow(10.0, y);
				num = (System.Math.Ceiling(num / (num3 / 10.0)) + 1.0) * (num3 / 10.0);
				num2 = (System.Math.Floor(num2 / (num3 / 10.0)) - 1.0) * (num3 / 10.0);
				this.MainChart.ChartAreas[area].AxisY.Maximum = num;
				this.MainChart.ChartAreas[area].AxisY.Minimum = num2;
				return;
			}
			this.MainChart.ChartAreas[area].AxisY.Maximum = double.NaN;
			this.MainChart.ChartAreas[area].AxisY.Minimum = double.NaN;
		}
		private void ZoomOutButton_Click(object sender, System.EventArgs e)
		{
			this.MainChart.ChartAreas["Main"].AxisX.ScaleView.ZoomReset(0);
			this.SetYAxis("Main");
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
				this.ThrottledPosition = this.UnthrottledPosition;
				this.IsZoomThrottled = true;
			}
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
	}
}
