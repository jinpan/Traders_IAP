using Janus.Windows.GridEX;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
namespace TTS
{
	public class TimeSales : TTSForm
	{
		public static WindowType TTSWindowType = WindowType.TIME_AND_SALES;
		private DataView TimeSalesView;
		private bool Initialized;
		private IContainer components;
		private System.Windows.Forms.ToolStrip MainToolStrip;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripComboBox TickerComboBox;
		private GridEX MainGrid;
		public TimeSales(TTSFormState state) : base(state)
		{
			this.InitializeComponent();
			base.BindComboBox(this.TickerComboBox.ComboBox, new System.EventHandler(this.ComboBox_SelectedIndexChanged), delegate(System.Windows.Forms.ComboBox cb)
			{
				cb.DataSource = Game.State.ActiveTradableSecurities;
			});
		}
		private void ComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if (this.TickerComboBox.ComboBox.SelectedItem != null)
			{
				string arg = this.TickerComboBox.ComboBox.SelectedItem.ToString();
				if (!this.Initialized)
				{
					this.TimeSalesView = new DataView(Game.State.TimeSalesTable, string.Format("Ticker='{0}'", arg), "Period DESC, Tick DESC, ID DESC", DataViewRowState.CurrentRows);
					base.AddResetHandler(delegate
					{
						this.TimeSalesView.Table = Game.State.TimeSalesTable;
					});
					this.MainGrid.SetDataBinding(this.TimeSalesView, "");
					this.MainGrid.RetrieveStructure();
					this.MainGrid.Format();
					this.MainGrid.RootTable.FormatColumns(new string[]
					{
						"Period",
						"Tick",
						"Ticker",
						"Price",
						"Volume",
						"Value",
						"Buyer/Seller"
					}, null);
					this.MainGrid.AddContextMenu(true);
					this.MainGrid.RootTable.AddPeriodTickFormatter("Period", "Tick", "");
					this.MainGrid.RootTable.AddSecurityDecimalFormatter(new string[]
					{
						"Value",
						"Price"
					});
					this.MainGrid.RootTable.AddSecurityVolumeDecimalFormatter(new string[]
					{
						"Volume"
					});
					this.MainGrid.RootTable.Columns["Buyer/Seller"].TextAlignment = TextAlignment.Center;
				}
				else
				{
					this.TimeSalesView.RowFilter = string.Format("Ticker='{0}'", arg);
				}
				this.Initialized = true;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(TimeSales));
			this.MainToolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.TickerComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.MainGrid = new GridEX();
			this.MainToolStrip.SuspendLayout();
			((ISupportInitialize)this.MainGrid).BeginInit();
			base.SuspendLayout();
			this.MainToolStrip.CanOverflow = false;
			this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripLabel1,
				this.TickerComboBox
			});
			this.MainToolStrip.Location = new System.Drawing.Point(0, 0);
			this.MainToolStrip.Name = "MainToolStrip";
			this.MainToolStrip.Size = new System.Drawing.Size(584, 25);
			this.MainToolStrip.TabIndex = 2;
			this.MainToolStrip.Text = "toolStrip1";
			this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(45, 22);
			this.toolStripLabel1.Text = "Ticker:";
			this.TickerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TickerComboBox.DropDownWidth = 100;
			this.TickerComboBox.Name = "TickerComboBox";
			this.TickerComboBox.Size = new System.Drawing.Size(121, 25);
			this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainGrid.FilterMode = FilterMode.Automatic;
			this.MainGrid.Location = new System.Drawing.Point(0, 25);
			this.MainGrid.Name = "MainGrid";
			this.MainGrid.Size = new System.Drawing.Size(584, 142);
			this.MainGrid.TabIndex = 3;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(584, 167);
			base.Controls.Add(this.MainGrid);
			base.Controls.Add(this.MainToolStrip);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "TimeSales";
			this.Text = "Time & Sales";
			this.MainToolStrip.ResumeLayout(false);
			this.MainToolStrip.PerformLayout();
			((ISupportInitialize)this.MainGrid).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
