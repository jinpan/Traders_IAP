using Janus.Windows.GridEX;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace TTS
{
	public class TransactionLog : TTSForm
	{
		private IContainer components;
		private GridEX MainGrid;
		public static WindowType TTSWindowType = WindowType.TRANSACTION_LOG;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(TransactionLog));
			this.MainGrid = new GridEX();
			((ISupportInitialize)this.MainGrid).BeginInit();
			base.SuspendLayout();
			this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainGrid.Location = new System.Drawing.Point(0, 0);
			this.MainGrid.Name = "MainGrid";
			this.MainGrid.Size = new System.Drawing.Size(584, 167);
			this.MainGrid.TabIndex = 0;
			base.ClientSize = new System.Drawing.Size(584, 167);
			base.Controls.Add(this.MainGrid);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "TransactionLog";
			this.Text = "Transaction Log";
			((ISupportInitialize)this.MainGrid).EndInit();
			base.ResumeLayout(false);
		}
		public TransactionLog(TTSFormState state) : base(state)
		{
			this.InitializeComponent();
			this.MainGrid.SetDataBinding(Game.State.TransactionView, "");
			this.MainGrid.RetrieveStructure();
			this.MainGrid.Format();
			this.MainGrid.RootTable.FormatColumns(new string[]
			{
				"Period",
				"Tick",
				"Ticker",
				"Price",
				"Quantity",
				"Type",
				"Value",
				"Balance",
				"Currency"
			}, null);
			this.MainGrid.AddContextMenu(true);
			this.MainGrid.RootTable.AddPeriodTickFormatter("Period", "Tick", "");
			this.MainGrid.RootTable.AddPositiveNegativeFormatter(new string[]
			{
				"Price",
				"Quantity",
				"Value"
			});
			this.MainGrid.RootTable.AddSecurityDecimalFormatter(new string[]
			{
				"Value",
				"Price"
			});
			this.MainGrid.RootTable.FormatDecimalColumns(Game.State.Currencies[Game.State.DefaultCurrency].FormatString, new string[]
			{
				"Balance"
			});
			this.MainGrid.RootTable.AddSecurityVolumeDecimalFormatter(new string[]
			{
				"Quantity"
			});
			this.MainGrid.LoadingRow += delegate(object sender, RowLoadEventArgs e)
			{
				if (e.Row.DataRow == null)
				{
					return;
				}
				TransactionType transactionType = (TransactionType)e.Row.Cells["Type"].Value;
				if (transactionType == TransactionType.ROWORDER)
				{
					e.Row.Cells["Type"].Text = "ORDER";
					return;
				}
				if (transactionType != TransactionType.INTEREST)
				{
					return;
				}
				if (e.Row.Cells["Price"].Value != null)
				{
					e.Row.Cells["Price"].Text = string.Format("{0:0.00}%", e.Row.Cells["Price"].Value);
				}
			};
			base.AddResetHandler(delegate
			{
				this.MainGrid.DataSource = Game.State.TransactionView;
			});
		}
	}
}
