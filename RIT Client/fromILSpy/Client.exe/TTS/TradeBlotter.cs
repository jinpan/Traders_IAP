using Janus.Windows.GridEX;
using System;
using System.ComponentModel;
using System.Drawing;
using System.ServiceModel;
using System.Windows.Forms;
namespace TTS
{
	public class TradeBlotter : TTSForm
	{
		public static WindowType TTSWindowType = WindowType.TRADE_BLOTTER;
		private IContainer components;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage OpenOrdersPage;
		private System.Windows.Forms.TabPage ArchivedOrdersPage;
		private System.Windows.Forms.TabPage TendersPage;
		private System.Windows.Forms.TabPage EndowmentsPage;
		private GridEX LiveOrdersGrid;
		private GridEX HistoryGrid;
		private System.Windows.Forms.LinkLabel CancelAllLabel;
		private GridEX TendersGrid;
		private GridEX EndowmentsGrid;
		public TradeBlotter(TTSFormState state) : base(state)
		{
			this.InitializeComponent();
			this.LiveOrdersGrid.Format();
			this.LiveOrdersGrid.SetDataBinding(Game.State.OpenOrderView, "");
			this.LiveOrdersGrid.RetrieveStructure();
			this.LiveOrdersGrid.RootTable.FormatColumns(new string[]
			{
				"ID",
				"Timestamp",
				"Tick",
				"Ticker",
				"Price",
				"Volume",
				"VolumeRemaining",
				"VWAP"
			}, new string[]
			{
				"ID",
				"Timestamp",
				"Tick",
				"Ticker",
				"Price",
				"Type",
				"Filled",
				"VWAP"
			});
			this.LiveOrdersGrid.RootTable.AddButtonColumn("Cancel", "Cancel", "");
			this.LiveOrdersGrid.ColumnButtonClick += delegate(object sender, ColumnActionEventArgs e)
			{
				GridEXRow row = this.LiveOrdersGrid.GetRow();
				if (row != null)
				{
					string ticker = (string)row.Cells["Ticker"].Value;
					int id = (int)this.LiveOrdersGrid.GetRow().Cells["ID"].Value;
					try
					{
						ServiceManager.Execute(delegate(IClientService p)
						{
							p.CancelOrder(ticker, new int[]
							{
								id
							});
						});
					}
					catch (FaultException ex)
					{
						DialogHelper.ShowError(ex.Message, "Error");
					}
				}
			};
			this.LiveOrdersGrid.RootTable.AddPeriodTickFormatter("Period", "Tick", "");
			this.LiveOrdersGrid.RootTable.AddPositiveNegativeFormatter(new string[]
			{
				"Volume"
			});
			this.LiveOrdersGrid.RootTable.AddProgressBarFormatter("VolumeRemaining", delegate(DrawGridAreaEventArgs e)
			{
				if (e.Row.Cells["VolumeRemaining"].Value != System.DBNull.Value)
				{
					return 1f - System.Convert.ToSingle(e.Row.Cells["VolumeRemaining"].Value) / System.Math.Abs(System.Convert.ToSingle(e.Row.Cells["Volume"].Value));
				}
				return 0f;
			}, delegate(DrawGridAreaEventArgs e)
			{
				if (e.Row.Cells["VolumeRemaining"].Value != System.DBNull.Value)
				{
					return string.Format("{0} / {1}", System.Math.Abs((decimal)e.Row.Cells["Volume"].Value) - (decimal)e.Row.Cells["VolumeRemaining"].Value, System.Math.Abs((decimal)e.Row.Cells["Volume"].Value));
				}
				return "";
			});
			this.LiveOrdersGrid.RootTable.AddSecurityDecimalFormatter(new string[]
			{
				"Price",
				"VWAP"
			});
			this.LiveOrdersGrid.LoadingRow += delegate(object sender, RowLoadEventArgs e)
			{
				if (e.Row.DataRow == null)
				{
					return;
				}
				e.Row.Cells["Volume"].Text = ((System.Math.Sign((decimal)e.Row.Cells["Volume"].Value) > 0) ? "BUY" : "SELL");
			};
			this.LiveOrdersGrid.AddContextMenu(true);
			this.HistoryGrid.Format();
			this.HistoryGrid.SetDataBinding(Game.State.ArchivedOrderView, "");
			this.HistoryGrid.RetrieveStructure();
			this.HistoryGrid.RootTable.FormatColumns(new string[]
			{
				"Timestamp",
				"Tick",
				"Ticker",
				"Price",
				"Type",
				"Volume",
				"VolumeRemaining",
				"VWAP",
				"Status"
			}, new string[]
			{
				"Timestamp",
				"Tick",
				"Ticker",
				"Price",
				"Type",
				"Type",
				"Filled",
				"VWAP",
				"Status"
			});
			this.HistoryGrid.RootTable.AddPeriodTickFormatter("Period", "Tick", "");
			this.HistoryGrid.RootTable.AddPositiveNegativeFormatter(new string[]
			{
				"Volume"
			});
			this.HistoryGrid.RootTable.AddProgressBarFormatter("VolumeRemaining", delegate(DrawGridAreaEventArgs e)
			{
				if (e.Row.Cells["VolumeRemaining"].Value != System.DBNull.Value)
				{
					return 1f - System.Convert.ToSingle(e.Row.Cells["VolumeRemaining"].Value) / System.Math.Abs(System.Convert.ToSingle(e.Row.Cells["Volume"].Value));
				}
				return 0f;
			}, delegate(DrawGridAreaEventArgs e)
			{
				if (e.Row.Cells["VolumeRemaining"].Value != System.DBNull.Value)
				{
					return string.Format("{0} / {1}", System.Math.Abs((decimal)e.Row.Cells["Volume"].Value) - (decimal)e.Row.Cells["VolumeRemaining"].Value, System.Math.Abs((decimal)e.Row.Cells["Volume"].Value));
				}
				return "";
			});
			this.HistoryGrid.RootTable.AddSecurityDecimalFormatter(new string[]
			{
				"Price",
				"VWAP"
			});
			this.HistoryGrid.LoadingRow += delegate(object sender, RowLoadEventArgs e)
			{
				if (e.Row.DataRow == null)
				{
					return;
				}
				e.Row.Cells["Volume"].Text = ((System.Math.Sign((decimal)e.Row.Cells["Volume"].Value) > 0) ? "BUY" : "SELL");
			};
			this.HistoryGrid.AddContextMenu(true);
			this.TendersGrid.Format();
			this.TendersGrid.SetDataBinding(Game.State.TenderView, "");
			this.TendersGrid.RetrieveStructure();
			this.TendersGrid.RootTable.FormatColumns(new string[]
			{
				"Timestamp",
				"Tick",
				"Ticker",
				"Price",
				"Volume",
				"VolumeRemaining",
				"Status"
			}, new string[]
			{
				"Timestamp",
				"Tick",
				"Ticker",
				"Price",
				"Type",
				"Volume",
				"Status"
			});
			this.TendersGrid.RootTable.AddPeriodTickFormatter("Period", "Tick", "");
			this.TendersGrid.RootTable.AddPositiveNegativeFormatter(new string[]
			{
				"Volume"
			});
			this.TendersGrid.RootTable.AddSecurityDecimalFormatter(new string[]
			{
				"Price"
			});
			this.TendersGrid.LoadingRow += delegate(object sender, RowLoadEventArgs e)
			{
				if (e.Row.DataRow == null)
				{
					return;
				}
				e.Row.Cells["Volume"].Text = ((System.Math.Sign((decimal)e.Row.Cells["Volume"].Value) > 0) ? "BUY" : "SELL");
				e.Row.Cells["VolumeRemaining"].Text = System.Math.Abs((decimal)e.Row.Cells["Volume"].Value).ToString();
				if ((OrderStatus)e.Row.Cells["Status"].Value == OrderStatus.DECLINED)
				{
					e.Row.Cells["Price"].Text = "";
				}
			};
			this.TendersGrid.AddContextMenu(true);
			this.EndowmentsGrid.Format();
			this.EndowmentsGrid.SetDataBinding(Game.State.EndowmentView, "");
			this.EndowmentsGrid.RetrieveStructure();
			this.EndowmentsGrid.RootTable.FormatColumns(new string[]
			{
				"Timestamp",
				"Tick",
				"Ticker",
				"Price",
				"Volume",
				"VolumeRemaining"
			}, new string[]
			{
				"Timestamp",
				"Tick",
				"Ticker",
				"Price",
				"Type",
				"Volume"
			});
			this.EndowmentsGrid.RootTable.AddPeriodTickFormatter("Period", "Tick", "");
			this.EndowmentsGrid.RootTable.AddPositiveNegativeFormatter(new string[]
			{
				"Volume"
			});
			this.EndowmentsGrid.RootTable.AddSecurityDecimalFormatter(new string[]
			{
				"Price"
			});
			this.EndowmentsGrid.LoadingRow += delegate(object sender, RowLoadEventArgs e)
			{
				if (e.Row.DataRow == null)
				{
					return;
				}
				e.Row.Cells["Volume"].Text = ((System.Math.Sign((decimal)e.Row.Cells["Volume"].Value) > 0) ? "BUY" : "SELL");
				e.Row.Cells["VolumeRemaining"].Text = System.Math.Abs((decimal)e.Row.Cells["Volume"].Value).ToString();
			};
			this.EndowmentsGrid.AddContextMenu(true);
			base.AddResetHandler(delegate
			{
				this.LiveOrdersGrid.DataSource = Game.State.OpenOrderView;
				this.HistoryGrid.DataSource = Game.State.ArchivedOrderView;
				this.TendersGrid.DataSource = Game.State.TenderView;
				this.EndowmentsGrid.DataSource = Game.State.EndowmentView;
			});
		}
		private void CancelAllLabel_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			try
			{
				ServiceManager.Execute(delegate(IClientService p)
				{
					p.CancelAllLimitOrders();
				});
			}
			catch (FaultException ex)
			{
				DialogHelper.ShowError(ex.Message, "Error");
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(TradeBlotter));
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.OpenOrdersPage = new System.Windows.Forms.TabPage();
			this.LiveOrdersGrid = new GridEX();
			this.ArchivedOrdersPage = new System.Windows.Forms.TabPage();
			this.HistoryGrid = new GridEX();
			this.TendersPage = new System.Windows.Forms.TabPage();
			this.TendersGrid = new GridEX();
			this.EndowmentsPage = new System.Windows.Forms.TabPage();
			this.EndowmentsGrid = new GridEX();
			this.CancelAllLabel = new System.Windows.Forms.LinkLabel();
			this.tabControl1.SuspendLayout();
			this.OpenOrdersPage.SuspendLayout();
			((ISupportInitialize)this.LiveOrdersGrid).BeginInit();
			this.ArchivedOrdersPage.SuspendLayout();
			((ISupportInitialize)this.HistoryGrid).BeginInit();
			this.TendersPage.SuspendLayout();
			((ISupportInitialize)this.TendersGrid).BeginInit();
			this.EndowmentsPage.SuspendLayout();
			((ISupportInitialize)this.EndowmentsGrid).BeginInit();
			base.SuspendLayout();
			this.tabControl1.Controls.Add(this.OpenOrdersPage);
			this.tabControl1.Controls.Add(this.ArchivedOrdersPage);
			this.tabControl1.Controls.Add(this.TendersPage);
			this.tabControl1.Controls.Add(this.EndowmentsPage);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(584, 167);
			this.tabControl1.TabIndex = 0;
			this.OpenOrdersPage.Controls.Add(this.LiveOrdersGrid);
			this.OpenOrdersPage.Location = new System.Drawing.Point(4, 22);
			this.OpenOrdersPage.Name = "OpenOrdersPage";
			this.OpenOrdersPage.Size = new System.Drawing.Size(576, 141);
			this.OpenOrdersPage.TabIndex = 0;
			this.OpenOrdersPage.Text = "Live/Partial";
			this.OpenOrdersPage.UseVisualStyleBackColor = true;
			this.LiveOrdersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LiveOrdersGrid.Location = new System.Drawing.Point(0, 0);
			this.LiveOrdersGrid.Name = "LiveOrdersGrid";
			this.LiveOrdersGrid.Size = new System.Drawing.Size(576, 141);
			this.LiveOrdersGrid.TabIndex = 2;
			this.ArchivedOrdersPage.Controls.Add(this.HistoryGrid);
			this.ArchivedOrdersPage.Location = new System.Drawing.Point(4, 22);
			this.ArchivedOrdersPage.Name = "ArchivedOrdersPage";
			this.ArchivedOrdersPage.Size = new System.Drawing.Size(576, 141);
			this.ArchivedOrdersPage.TabIndex = 1;
			this.ArchivedOrdersPage.Text = "Filled/Cancelled";
			this.ArchivedOrdersPage.UseVisualStyleBackColor = true;
			this.HistoryGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.HistoryGrid.Location = new System.Drawing.Point(0, 0);
			this.HistoryGrid.Name = "HistoryGrid";
			this.HistoryGrid.Size = new System.Drawing.Size(576, 141);
			this.HistoryGrid.TabIndex = 1;
			this.TendersPage.Controls.Add(this.TendersGrid);
			this.TendersPage.Location = new System.Drawing.Point(4, 22);
			this.TendersPage.Name = "TendersPage";
			this.TendersPage.Size = new System.Drawing.Size(576, 141);
			this.TendersPage.TabIndex = 2;
			this.TendersPage.Text = "Tenders";
			this.TendersPage.UseVisualStyleBackColor = true;
			this.TendersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TendersGrid.Location = new System.Drawing.Point(0, 0);
			this.TendersGrid.Name = "TendersGrid";
			this.TendersGrid.Size = new System.Drawing.Size(576, 141);
			this.TendersGrid.TabIndex = 2;
			this.EndowmentsPage.Controls.Add(this.EndowmentsGrid);
			this.EndowmentsPage.Location = new System.Drawing.Point(4, 22);
			this.EndowmentsPage.Name = "EndowmentsPage";
			this.EndowmentsPage.Size = new System.Drawing.Size(576, 141);
			this.EndowmentsPage.TabIndex = 3;
			this.EndowmentsPage.Text = "Endowments";
			this.EndowmentsPage.UseVisualStyleBackColor = true;
			this.EndowmentsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.EndowmentsGrid.Location = new System.Drawing.Point(0, 0);
			this.EndowmentsGrid.Name = "EndowmentsGrid";
			this.EndowmentsGrid.Size = new System.Drawing.Size(576, 141);
			this.EndowmentsGrid.TabIndex = 2;
			this.CancelAllLabel.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.CancelAllLabel.AutoSize = true;
			this.CancelAllLabel.Location = new System.Drawing.Point(528, 2);
			this.CancelAllLabel.Name = "CancelAllLabel";
			this.CancelAllLabel.Size = new System.Drawing.Size(53, 13);
			this.CancelAllLabel.TabIndex = 1;
			this.CancelAllLabel.TabStop = true;
			this.CancelAllLabel.Text = "Cancel All";
			this.CancelAllLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CancelAllLabel_LinkClicked);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(584, 167);
			base.Controls.Add(this.CancelAllLabel);
			base.Controls.Add(this.tabControl1);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "TradeBlotter";
			this.Text = "Trade Blotter";
			this.tabControl1.ResumeLayout(false);
			this.OpenOrdersPage.ResumeLayout(false);
			((ISupportInitialize)this.LiveOrdersGrid).EndInit();
			this.ArchivedOrdersPage.ResumeLayout(false);
			((ISupportInitialize)this.HistoryGrid).EndInit();
			this.TendersPage.ResumeLayout(false);
			((ISupportInitialize)this.TendersGrid).EndInit();
			this.EndowmentsPage.ResumeLayout(false);
			((ISupportInitialize)this.EndowmentsGrid).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
