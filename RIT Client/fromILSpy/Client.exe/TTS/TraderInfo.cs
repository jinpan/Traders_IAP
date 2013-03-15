using Janus.Windows.GridEX;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace TTS
{
	public class TraderInfo : TTSForm
	{
		private IContainer components;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label TraderIDLabel;
		private System.Windows.Forms.Label NLVLabel;
		private System.Windows.Forms.Label NameLabel;
		private GridEX TradingLimitsGrid;
		public static WindowType TTSWindowType = WindowType.TRADER_INFO;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(TraderInfo));
			this.TradingLimitsGrid = new GridEX();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.NLVLabel = new System.Windows.Forms.Label();
			this.NameLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.TraderIDLabel = new System.Windows.Forms.Label();
			((ISupportInitialize)this.TradingLimitsGrid).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.TradingLimitsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TradingLimitsGrid.Location = new System.Drawing.Point(10, 63);
			this.TradingLimitsGrid.Name = "TradingLimitsGrid";
			this.TradingLimitsGrid.Size = new System.Drawing.Size(214, 94);
			this.TradingLimitsGrid.TabIndex = 1;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.NLVLabel, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.NameLabel, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.TraderIDLabel, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(10, 10);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333f));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5f));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(214, 53);
			this.tableLayoutPanel1.TabIndex = 0;
			this.NLVLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.NLVLabel.AutoSize = true;
			this.NLVLabel.Location = new System.Drawing.Point(73, 33);
			this.NLVLabel.Name = "NLVLabel";
			this.NLVLabel.Size = new System.Drawing.Size(22, 13);
			this.NLVLabel.TabIndex = 7;
			this.NLVLabel.Text = "???";
			this.NameLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.NameLabel.AutoSize = true;
			this.NameLabel.Location = new System.Drawing.Point(73, 17);
			this.NameLabel.Name = "NameLabel";
			this.NameLabel.Size = new System.Drawing.Size(22, 13);
			this.NameLabel.TabIndex = 5;
			this.NameLabel.Text = "???";
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new System.Drawing.Point(3, 1);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Trader ID:";
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new System.Drawing.Point(25, 17);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(42, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Name:";
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label4.Location = new System.Drawing.Point(35, 33);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(32, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "P&&L:";
			this.TraderIDLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.TraderIDLabel.AutoSize = true;
			this.TraderIDLabel.Location = new System.Drawing.Point(73, 1);
			this.TraderIDLabel.Name = "TraderIDLabel";
			this.TraderIDLabel.Size = new System.Drawing.Size(22, 13);
			this.TraderIDLabel.TabIndex = 4;
			this.TraderIDLabel.Text = "???";
			base.ClientSize = new System.Drawing.Size(234, 167);
			base.Controls.Add(this.TradingLimitsGrid);
			base.Controls.Add(this.tableLayoutPanel1);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "TraderInfo";
			base.Padding = new System.Windows.Forms.Padding(10);
			this.Text = "Trader Info";
			((ISupportInitialize)this.TradingLimitsGrid).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			base.ResumeLayout(false);
		}
		public TraderInfo(TTSFormState state) : base(state)
		{
			this.InitializeComponent();
			this.TradingLimitsGrid.SetDataBinding(Game.State.TradingLimitTable, "");
			this.TradingLimitsGrid.RetrieveStructure();
			this.TradingLimitsGrid.Format();
			this.TradingLimitsGrid.RootTable.FormatColumns(new string[]
			{
				"Name",
				"Gross",
				"Net"
			}, new string[]
			{
				"Limit",
				"Gross",
				"Net"
			});
			this.TradingLimitsGrid.AddContextMenu(false);
			this.TradingLimitsGrid.LoadingRow += delegate(object sender, RowLoadEventArgs e)
			{
				e.Row.Cells["Gross"].Text = string.Format("{0:#,0.##} / {1:#,0.##}", e.Row.Cells["Gross"].Value, Game.State.RiskTypes[e.Row.Cells["Name"].Text].GrossRiskLimit);
				e.Row.Cells["Net"].Text = string.Format("{0:#,0.##;(#,0.##);#,0.##} / {1:#,0.##;(#,0.##);#,0.##}", (decimal)e.Row.Cells["Net"].Value, (((decimal)e.Row.Cells["Net"].Value < 0m) ? -1 : 1) * Game.State.RiskTypes[e.Row.Cells["Name"].Text].NetRiskLimit);
				if ((decimal)e.Row.Cells["Gross"].Value > Game.State.RiskTypes[e.Row.Cells["Name"].Text].GrossRiskLimit || System.Math.Abs((decimal)e.Row.Cells["Net"].Value) > Game.State.RiskTypes[e.Row.Cells["Name"].Text].NetRiskLimit)
				{
					e.Row.RowStyle = ColorHelper.RowStyleRed;
				}
			};
			this.GameReset();
			base.AddResetHandler(new Action(this.GameReset));
			base.AddVariablesUpdatedHandler(new Action(this.TradingLimitsGrid.Refresh));
		}
		private void GameReset()
		{
			this.TraderIDLabel.Text = Game.State.Trader.TraderID;
			this.NameLabel.Text = Game.State.Trader.FirstName + " " + Game.State.Trader.LastName;
			this.NLVLabel.DataBindings.Clear();
			this.NLVLabel.DataBindings.Add("Text", Game.State, "NLV", true, System.Windows.Forms.DataSourceUpdateMode.Never, "", Game.State.Currencies[Game.State.DefaultCurrency].FormatString);
			this.TradingLimitsGrid.SetDataBinding(Game.State.TradingLimitTable, "");
		}
	}
}
