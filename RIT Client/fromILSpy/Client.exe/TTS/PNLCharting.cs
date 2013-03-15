using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TTS.Properties;
namespace TTS
{
	public class PNLCharting : TTSForm
	{
		private IContainer components;
		private Chart TraderChart;
		private System.Windows.Forms.ToolStrip MainStrip;
		private System.Windows.Forms.ToolStripButton IsZoomableButton;
		private System.Windows.Forms.ToolStripButton ZoomOutButton;
		private System.Windows.Forms.Timer ChartScrollThrottle;
		private System.Windows.Forms.SaveFileDialog TraderChartSaveFileDialog;
		private System.Windows.Forms.ToolStripDropDownButton ExportSplitButton;
		private System.Windows.Forms.ToolStripMenuItem copyImageToClipboardToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveImageToFilepngToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem copyDataToClipboardToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveDataToFilecsvToolStripMenuItem;
		private System.Windows.Forms.SaveFileDialog TraderDataSaveFileDialog;
		public static WindowType TTSWindowType = WindowType.PNL_CHARTING;
		private Action<int[], TraderChartPoint[]> TraderChartUpdateHandler;
		private double ThrottledPosition;
		private double UnthrottledPosition;
		private bool IsZoomThrottled;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(PNLCharting));
			this.TraderChart = new Chart();
			this.MainStrip = new System.Windows.Forms.ToolStrip();
			this.IsZoomableButton = new System.Windows.Forms.ToolStripButton();
			this.ZoomOutButton = new System.Windows.Forms.ToolStripButton();
			this.ExportSplitButton = new System.Windows.Forms.ToolStripDropDownButton();
			this.copyImageToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveImageToFilepngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.copyDataToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveDataToFilecsvToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ChartScrollThrottle = new System.Windows.Forms.Timer(this.components);
			this.TraderChartSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.TraderDataSaveFileDialog = new System.Windows.Forms.SaveFileDialog();
			((ISupportInitialize)this.TraderChart).BeginInit();
			this.MainStrip.SuspendLayout();
			base.SuspendLayout();
			this.TraderChart.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TraderChart.Location = new System.Drawing.Point(0, 25);
			this.TraderChart.Name = "TraderChart";
			this.TraderChart.Size = new System.Drawing.Size(455, 237);
			this.TraderChart.TabIndex = 0;
			this.TraderChart.Text = "chart1";
			this.TraderChart.AxisViewChanged += new System.EventHandler<ViewEventArgs>(this.TraderChart_AxisViewChanged);
			this.MainStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.IsZoomableButton,
				this.ZoomOutButton,
				this.ExportSplitButton
			});
			this.MainStrip.Location = new System.Drawing.Point(0, 0);
			this.MainStrip.Name = "MainStrip";
			this.MainStrip.Size = new System.Drawing.Size(455, 25);
			this.MainStrip.TabIndex = 2;
			this.MainStrip.Text = "MainToolStrip";
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
				this.saveImageToFilepngToolStripMenuItem,
				this.toolStripSeparator1,
				this.copyDataToClipboardToolStripMenuItem,
				this.saveDataToFilecsvToolStripMenuItem
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
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(204, 6);
			this.copyDataToClipboardToolStripMenuItem.Name = "copyDataToClipboardToolStripMenuItem";
			this.copyDataToClipboardToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.copyDataToClipboardToolStripMenuItem.Text = "Copy Data to Clipboard";
			this.copyDataToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyDataToClipboardToolStripMenuItem_Click);
			this.saveDataToFilecsvToolStripMenuItem.Name = "saveDataToFilecsvToolStripMenuItem";
			this.saveDataToFilecsvToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
			this.saveDataToFilecsvToolStripMenuItem.Text = "Save Data to File (.csv)";
			this.saveDataToFilecsvToolStripMenuItem.Click += new System.EventHandler(this.saveDataToFilecsvToolStripMenuItem_Click);
			this.ChartScrollThrottle.Enabled = true;
			this.ChartScrollThrottle.Interval = 250;
			this.ChartScrollThrottle.Tick += new System.EventHandler(this.ChartScrollThrottle_Tick);
			this.TraderChartSaveFileDialog.DefaultExt = "png";
			this.TraderChartSaveFileDialog.Filter = "PNG files|*.png";
			this.TraderDataSaveFileDialog.DefaultExt = "csv";
			this.TraderDataSaveFileDialog.Filter = "CSV files|*.csv";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(455, 262);
			base.Controls.Add(this.TraderChart);
			base.Controls.Add(this.MainStrip);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "PNLCharting";
			this.Text = "P&L Charting";
			((ISupportInitialize)this.TraderChart).EndInit();
			this.MainStrip.ResumeLayout(false);
			this.MainStrip.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		public PNLCharting(TTSFormState state) : base(state)
		{
			this.InitializeComponent();
			this.TraderChart.Format(true);
			this.TraderChart.AddChartArea("NLV", true);
			this.TraderChart.AddSeries("NLV", "NLV", SeriesChartType.Line);
			this.InitializeNLVChart();
			base.AddResetHandler(new Action(this.InitializeNLVChart));
			this.ZoomOutButton.PerformClick();
			base.FormClosed += delegate(object sender, System.Windows.Forms.FormClosedEventArgs e)
			{
				Game.State.TraderChart.ChartUpdated -= this.TraderChartUpdateHandler;
			};
		}
		private void InitializeNLVChart()
		{
			foreach (Series current in this.TraderChart.Series)
			{
				current.Points.Clear();
			}
			DataPointCollection arg_94_0 = this.TraderChart.Series["NLV"].Points;
			System.Collections.IEnumerable[] array = new System.Collections.IEnumerable[1];
			array[0] = Game.State.TraderChart.Data.Select(delegate(TraderChartPoint x)
			{
				if (x != null)
				{
					return x.NLV;
				}
				return 0.0;
			}).ToArray<double>();
			arg_94_0.DataBindY(array);
			foreach (DataPoint current2 in this.TraderChart.Series["NLV"].Points)
			{
				if (current2.YValues[0] == 0.0)
				{
					current2.IsEmpty = true;
				}
			}
			this.TraderChart.ChartAreas["NLV"].AxisX.Minimum = 0.0;
			this.TraderChart.ChartAreas["NLV"].AxisX.Maximum = this.TraderChart.ChartAreas["NLV"].AxisX.Minimum + System.Math.Ceiling(System.Math.Max(System.Convert.ToDouble(Game.State.TraderChart.Data.Count) / (double)Game.State.General.TicksPerPeriod, 1.0)) * (double)Game.State.General.TicksPerPeriod;
			this.TraderChart.ChartAreas["NLV"].AddTickLabels(Game.State.General.TicksPerPeriod, Game.State.General.Periods, false, true);
			this.TraderChartUpdateHandler = delegate(int[] ii, TraderChartPoint[] p)
			{
				for (int i = 0; i < p.Length; i++)
				{
					this.TraderChart.Series["NLV"].Points.SuspendUpdates();
					if (this.TraderChart.Series["NLV"].Points.Count <= ii[i] - 1)
					{
						this.TraderChart.Series["NLV"].Points.Add(new double[]
						{
							System.Convert.ToDouble(p[i].NLV)
						});
					}
					else
					{
						this.TraderChart.Series["NLV"].Points[ii[i] - 1].YValues[0] = System.Convert.ToDouble(p[i].NLV);
						this.TraderChart.Series["NLV"].Points[ii[i] - 1].IsEmpty = false;
					}
					this.TraderChart.Series["NLV"].Points.ResumeUpdates();
				}
				this.TraderChart.ChartAreas["NLV"].AxisX.Maximum = this.TraderChart.ChartAreas["NLV"].AxisX.Minimum + System.Math.Ceiling(System.Math.Max(System.Convert.ToDouble(Game.State.TraderChart.Data.Count) / (double)Game.State.General.TicksPerPeriod, 1.0)) * (double)Game.State.General.TicksPerPeriod;
			};
			Game.State.TraderChart.ChartUpdated += this.TraderChartUpdateHandler;
		}
		private void SetYAxis(string area)
		{
			double num = -1.7976931348623157E+308;
			double num2 = 1.7976931348623157E+308;
			foreach (Series current in 
				from x in this.TraderChart.Series
				where x.ChartArea == area
				select x)
			{
				if (current.Points.Count > 0)
				{
					DataPoint[] array = current.Points.Where((DataPoint x) => !x.IsEmpty && x.XValue > this.TraderChart.ChartAreas[area].AxisX.ScaleView.ViewMinimum && x.XValue < this.TraderChart.ChartAreas[area].AxisX.ScaleView.ViewMaximum).ToArray<DataPoint>();
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
				this.TraderChart.ChartAreas[area].AxisY.Maximum = num;
				this.TraderChart.ChartAreas[area].AxisY.Minimum = num2;
				return;
			}
			this.TraderChart.ChartAreas[area].AxisY.Maximum = double.NaN;
			this.TraderChart.ChartAreas[area].AxisY.Minimum = double.NaN;
		}
		private void ZoomOutButton_Click(object sender, System.EventArgs e)
		{
			this.TraderChart.ChartAreas["NLV"].AxisX.ScaleView.ZoomReset(0);
			this.SetYAxis("NLV");
		}
		private void IsZoomableButton_Click(object sender, System.EventArgs e)
		{
			if (!this.IsZoomableButton.Checked)
			{
				this.ZoomOutButton.PerformClick();
			}
			foreach (ChartArea current in this.TraderChart.ChartAreas)
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
				this.TraderChart_AxisViewChanged(null, new ViewEventArgs(null, this.UnthrottledPosition, 0.0, DateTimeIntervalType.NotSet));
			}
		}
		private void TraderChart_AxisViewChanged(object sender, ViewEventArgs e)
		{
			this.UnthrottledPosition = e.NewPosition;
			if (!this.IsZoomThrottled && this.UnthrottledPosition != this.ThrottledPosition)
			{
				this.SetYAxis("NLV");
				this.ThrottledPosition = this.UnthrottledPosition;
				this.IsZoomThrottled = true;
			}
		}
		private void copyImageToClipboardToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
			{
				this.TraderChart.SaveImage(memoryStream, ChartImageFormat.Bmp);
				System.Drawing.Bitmap image = new System.Drawing.Bitmap(memoryStream);
				System.Windows.Forms.Clipboard.SetImage(image);
			}
		}
		private void saveImageToFilepngToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			if (this.TraderChartSaveFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				try
				{
					this.TraderChart.SaveImage(this.TraderChartSaveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
				}
				catch (System.Exception ex)
				{
					DialogHelper.ShowError(ex.Message, "Error");
				}
			}
		}
		private void copyDataToClipboardToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			System.Windows.Forms.Clipboard.SetText(this.GenerateDataTable().ToDelimitedString("\t"));
		}
		private void saveDataToFilecsvToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			if (this.TraderDataSaveFileDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				try
				{
					System.IO.File.WriteAllText(this.TraderDataSaveFileDialog.FileName, this.GenerateDataTable().ToDelimitedString(","));
				}
				catch (System.Exception ex)
				{
					DialogHelper.ShowError(ex.Message, "Error");
				}
			}
		}
		private DataTable GenerateDataTable()
		{
			DataTable dataTable = new DataTable();
			ChartItem<TraderChartPoint> traderChart = Game.State.TraderChart;
			dataTable.Columns.Add("Period", typeof(int));
			dataTable.Columns.Add("Tick", typeof(int));
			dataTable.Columns.Add("NLV", typeof(decimal));
			for (int i = 0; i < traderChart.Data.Count; i++)
			{
				DataRow dataRow = dataTable.NewRow();
				if (i >= traderChart.InitialTicks)
				{
					dataRow["Period"] = (i - traderChart.InitialTicks) / traderChart.TicksPerPeriod + 1;
					dataRow["Tick"] = (i - traderChart.InitialTicks) % traderChart.TicksPerPeriod + 1;
				}
				else
				{
					dataRow["Tick"] = i + 1;
				}
				dataRow["NLV"] = traderChart.Data[i].NLV;
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}
	}
}
