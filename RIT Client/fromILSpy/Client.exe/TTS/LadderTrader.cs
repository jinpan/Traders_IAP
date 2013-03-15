using Janus.Windows.GridEX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Windows.Forms;
using TTS.Properties;
namespace TTS
{
	public class LadderTrader : TTSForm
	{
		private IContainer components;
		private System.Windows.Forms.ToolStrip MainToolStrip;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripComboBox TickerComboBox;
		private System.Windows.Forms.Timer GridTimer;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label PositionLabel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label VWAPLabel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label LastLabel;
		private VirtualGridEX LadderGrid;
		private System.Windows.Forms.ToolStripButton ShortcutTradesButton;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		private System.Windows.Forms.ToolStripLabel SpringLabel;
		public static WindowType TTSWindowType = WindowType.LADDER_TRADER;
		private System.Windows.Forms.NumericUpDown V;
		private string LastTicker;
		private System.Collections.Generic.Dictionary<string, decimal> CachedShortcutSettings = new System.Collections.Generic.Dictionary<string, decimal>();
		private int LadderSize = 100;
		private int RecenterLimit = 10;
		private int RowsOnScreen;
		private decimal MidmarketValue;
		private Action<decimal, decimal> SecurityUpdated;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(LadderTrader));
			this.GridTimer = new System.Windows.Forms.Timer(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.LadderGrid = new VirtualGridEX();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.LastLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.PositionLabel = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.VWAPLabel = new System.Windows.Forms.Label();
			this.MainToolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.TickerComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.SpringLabel = new System.Windows.Forms.ToolStripLabel();
			this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
			this.ShortcutTradesButton = new System.Windows.Forms.ToolStripButton();
			this.panel1.SuspendLayout();
			((ISupportInitialize)this.LadderGrid).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			this.MainToolStrip.SuspendLayout();
			base.SuspendLayout();
			this.GridTimer.Interval = 500;
			this.panel1.Controls.Add(this.LadderGrid);
			this.panel1.Controls.Add(this.tableLayoutPanel1);
			this.panel1.Controls.Add(this.MainToolStrip);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(384, 462);
			this.panel1.TabIndex = 1;
			this.LadderGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LadderGrid.Location = new System.Drawing.Point(0, 44);
			this.LadderGrid.Name = "LadderGrid";
			this.LadderGrid.Size = new System.Drawing.Size(384, 418);
			this.LadderGrid.TabIndex = 5;
			this.LadderGrid.VirtualMode = false;
			this.LadderGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LadderGrid_MouseDown);
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 25);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 19f));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(384, 19);
			this.tableLayoutPanel1.TabIndex = 4;
			this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel1.Controls.Add(this.label2);
			this.flowLayoutPanel1.Controls.Add(this.LastLabel);
			this.flowLayoutPanel1.Controls.Add(this.label1);
			this.flowLayoutPanel1.Controls.Add(this.PositionLabel);
			this.flowLayoutPanel1.Controls.Add(this.label3);
			this.flowLayoutPanel1.Controls.Add(this.VWAPLabel);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(85, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(213, 13);
			this.flowLayoutPanel1.TabIndex = 0;
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new System.Drawing.Point(3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(34, 13);
			this.label2.TabIndex = 4;
			this.label2.Text = "Last:";
			this.LastLabel.AutoSize = true;
			this.LastLabel.Location = new System.Drawing.Point(43, 0);
			this.LastLabel.Name = "LastLabel";
			this.LastLabel.Size = new System.Drawing.Size(15, 13);
			this.LastLabel.TabIndex = 5;
			this.LastLabel.Text = "--";
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new System.Drawing.Point(64, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(55, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Position:";
			this.PositionLabel.AutoSize = true;
			this.PositionLabel.Location = new System.Drawing.Point(125, 0);
			this.PositionLabel.Name = "PositionLabel";
			this.PositionLabel.Size = new System.Drawing.Size(15, 13);
			this.PositionLabel.TabIndex = 1;
			this.PositionLabel.Text = "--";
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label3.Location = new System.Drawing.Point(146, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(43, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "VWAP:";
			this.VWAPLabel.AutoSize = true;
			this.VWAPLabel.Location = new System.Drawing.Point(195, 0);
			this.VWAPLabel.Name = "VWAPLabel";
			this.VWAPLabel.Size = new System.Drawing.Size(15, 13);
			this.VWAPLabel.TabIndex = 3;
			this.VWAPLabel.Text = "--";
			this.MainToolStrip.CanOverflow = false;
			this.MainToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripLabel1,
				this.TickerComboBox,
				this.SpringLabel,
				this.toolStripLabel2,
				this.ShortcutTradesButton
			});
			this.MainToolStrip.Location = new System.Drawing.Point(0, 0);
			this.MainToolStrip.Name = "MainToolStrip";
			this.MainToolStrip.Size = new System.Drawing.Size(384, 25);
			this.MainToolStrip.TabIndex = 1;
			this.MainToolStrip.Text = "toolStrip1";
			this.MainToolStrip.Layout += new System.Windows.Forms.LayoutEventHandler(this.MainToolStrip_Layout);
			this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(45, 22);
			this.toolStripLabel1.Text = "Ticker:";
			this.TickerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TickerComboBox.DropDownWidth = 100;
			this.TickerComboBox.Name = "TickerComboBox";
			this.TickerComboBox.Size = new System.Drawing.Size(121, 25);
			this.SpringLabel.AutoSize = false;
			this.SpringLabel.Name = "SpringLabel";
			this.SpringLabel.Size = new System.Drawing.Size(10, 22);
			this.toolStripLabel2.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.toolStripLabel2.Image = Resources.lightning;
			this.toolStripLabel2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.toolStripLabel2.Name = "toolStripLabel2";
			this.toolStripLabel2.Size = new System.Drawing.Size(26, 22);
			this.toolStripLabel2.Text = ":";
			this.toolStripLabel2.ToolTipText = "Shortcut Trades";
			this.ShortcutTradesButton.CheckOnClick = true;
			this.ShortcutTradesButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.ShortcutTradesButton.Image = Resources.lightning;
			this.ShortcutTradesButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ShortcutTradesButton.Name = "ShortcutTradesButton";
			this.ShortcutTradesButton.Size = new System.Drawing.Size(32, 22);
			this.ShortcutTradesButton.Text = "OFF";
			this.ShortcutTradesButton.CheckedChanged += new System.EventHandler(this.ShortcutTradesButton_CheckedChanged);
			this.AllowDrop = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(384, 462);
			base.Controls.Add(this.panel1);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "LadderTrader";
			this.Text = "Ladder Trader";
			base.DragDrop += new System.Windows.Forms.DragEventHandler(this.LadderTrader_DragDrop);
			base.DragOver += new System.Windows.Forms.DragEventHandler(this.LadderTrader_DragOver);
			base.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LadderGrid_KeyPress);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((ISupportInitialize)this.LadderGrid).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			this.MainToolStrip.ResumeLayout(false);
			this.MainToolStrip.PerformLayout();
			base.ResumeLayout(false);
		}
		public LadderTrader(TTSFormState state) : base(state)
		{
			this.InitializeComponent();
			this.MainToolStrip.Items.Add(new System.Windows.Forms.ToolStripLabel("V:")
			{
				Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0)
			});
			this.MainToolStrip.Items.Add(new System.Windows.Forms.ToolStripControlHost(this.V = new System.Windows.Forms.NumericUpDown
			{
				Increment = 10m,
				Maximum = 1000m,
				Value = 10m,
				Height = 22
			}));
			this.LadderGrid.Format();
			this.LadderGrid.RootTable = new GridEXTable();
			this.LadderGrid.VirtualMode = true;
			this.LadderGrid.RootTable.Columns.Add(new GridEXColumn("Bid Qty")
			{
				BoundMode = ColumnBoundMode.UnboundFetch,
				DataTypeCode = System.TypeCode.Int32
			});
			this.LadderGrid.RootTable.Columns.Add(new GridEXColumn("Bid Size")
			{
				BoundMode = ColumnBoundMode.UnboundFetch,
				DataTypeCode = System.TypeCode.Int32,
				CellStyle = new GridEXFormatStyle(ColorHelper.RowStyleGreen)
			});
			this.LadderGrid.RootTable.Columns.Add(new GridEXColumn("Price")
			{
				BoundMode = ColumnBoundMode.UnboundFetch,
				DataTypeCode = System.TypeCode.Decimal
			});
			this.LadderGrid.RootTable.Columns.Add(new GridEXColumn("Ask Size")
			{
				BoundMode = ColumnBoundMode.UnboundFetch,
				DataTypeCode = System.TypeCode.Int32,
				CellStyle = new GridEXFormatStyle(ColorHelper.RowStyleRed)
			});
			this.LadderGrid.RootTable.Columns.Add(new GridEXColumn("Ask Qty")
			{
				BoundMode = ColumnBoundMode.UnboundFetch,
				DataTypeCode = System.TypeCode.Int32
			});
			this.LadderGrid.RootTable.FormatColumns(new string[]
			{
				"Bid Qty",
				"Bid Size",
				"Price",
				"Ask Size",
				"Ask Qty"
			}, null);
			foreach (GridEXColumn gridEXColumn in this.LadderGrid.RootTable.Columns)
			{
				gridEXColumn.TextAlignment = TextAlignment.Center;
			}
			this.LadderGrid.RowCount = this.LadderSize;
			this.MidmarketValue = 0m;
			this.LadderGrid.LoadingRow += delegate(object ss, RowLoadEventArgs ee)
			{
				if (this.TickerComboBox.ComboBox.SelectedItem != null)
				{
					string key = this.TickerComboBox.ComboBox.SelectedItem.ToString();
					decimal num = System.Math.Round((Game.State.Securities[key].Bid + Game.State.Securities[key].Ask) / 2m, Game.State.Securities[key].Parameters.QuotedDecimals);
					if (System.Math.Abs(this.MidmarketValue - num) / Game.State.Securities[key].Increment > this.RecenterLimit)
					{
						this.MidmarketValue = num;
					}
					decimal num2 = System.Math.Round((this.LadderSize / 2 - ee.Row.RowIndex) * System.Convert.ToDecimal(System.Math.Pow(10.0, (double)(Game.State.Securities[key].Parameters.QuotedDecimals * -1))) + this.MidmarketValue, Game.State.Securities[key].Parameters.QuotedDecimals);
					if (Game.State.Securities[key].LadderVolumes.ContainsKey(num2))
					{
						if (Game.State.Securities[key].LadderVolumes[num2] > 0m)
						{
							ee.Row.Cells["Bid Size"].Value = System.Math.Abs(Game.State.Securities[key].LadderVolumes[num2]);
						}
						else
						{
							if (Game.State.Securities[key].LadderVolumes[num2] < 0m)
							{
								ee.Row.Cells["Ask Size"].Value = System.Math.Abs(Game.State.Securities[key].LadderVolumes[num2]);
							}
						}
					}
					if (Game.State.Securities[key].LadderTraderVolumes.ContainsKey(num2))
					{
						if (Game.State.Securities[key].LadderTraderVolumes[num2] > 0m)
						{
							ee.Row.Cells["Bid Qty"].Value = System.Math.Abs(Game.State.Securities[key].LadderTraderVolumes[num2]);
						}
						else
						{
							if (Game.State.Securities[key].LadderTraderVolumes[num2] < 0m)
							{
								ee.Row.Cells["Ask Qty"].Value = System.Math.Abs(Game.State.Securities[key].LadderTraderVolumes[num2]);
							}
						}
					}
					ee.Row.Cells["Price"].Value = num2;
				}
			};
			this.SecurityUpdated = delegate(decimal price, decimal volume)
			{
				this.LadderGrid.Refresh();
			};
			base.BindComboBox(this.TickerComboBox.ComboBox, new System.EventHandler(this.ComboBox_SelectedIndexChanged), delegate(System.Windows.Forms.ComboBox cb)
			{
				cb.DataSource = Game.State.ActiveTradableSecurities;
			});
			if (state != null && state.State != null)
			{
				this.TickerComboBox.ComboBox.SelectedItem = state.State;
			}
			base.FormClosed += delegate(object sender, System.Windows.Forms.FormClosedEventArgs e)
			{
				foreach (SecurityItem current in Game.State.Securities.Values)
				{
					current.RemoveEventIfContained(this.SecurityUpdated);
				}
			};
		}
		protected override object GetState()
		{
			return this.TickerComboBox.ComboBox.SelectedItem;
		}
		private void CenterLadder()
		{
			this.RowsOnScreen = this.LadderGrid.Size.Height / 18 - 1;
			this.LadderGrid.Row = this.LadderSize / 2;
			this.LadderGrid.FirstRow = (this.LadderSize - this.RowsOnScreen) / 2;
		}
		private void ComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.PositionLabel.DataBindings.Clear();
			this.VWAPLabel.DataBindings.Clear();
			this.LastLabel.DataBindings.Clear();
			foreach (SecurityItem current in Game.State.Securities.Values)
			{
				current.RemoveEventIfContained(this.SecurityUpdated);
			}
			if (this.TickerComboBox.ComboBox.SelectedItem != null)
			{
				string ticker = this.TickerComboBox.ComboBox.SelectedItem.ToString();
				if (this.LastTicker != null)
				{
					if (!this.CachedShortcutSettings.ContainsKey(this.LastTicker))
					{
						this.CachedShortcutSettings.Add(this.LastTicker, this.V.Value);
					}
					else
					{
						this.CachedShortcutSettings[this.LastTicker] = this.V.Value;
					}
				}
				this.LastTicker = ticker;
				if (Game.State.Securities[ticker].Parameters.Type == SecurityType.STOCK)
				{
					this.V.Maximum = 999999m;
					this.V.Increment = 100m;
					this.V.Value = 100m;
					this.MainToolStrip.PerformLayout();
					this.MainToolStrip.PerformLayout();
				}
				else
				{
					this.V.Maximum = 9999m;
					this.V.Increment = 10m;
					this.V.Value = 10m;
					this.MainToolStrip.PerformLayout();
					this.MainToolStrip.PerformLayout();
				}
				if (this.CachedShortcutSettings.ContainsKey(ticker))
				{
					this.V.Value = System.Math.Min(this.CachedShortcutSettings[ticker], this.V.Maximum);
				}
				this.LadderGrid.RootTable.FormatDecimalColumns(Game.State.Securities[ticker].Parameters.FormatString, new string[]
				{
					"Price"
				});
				this.CenterLadder();
				this.MidmarketValue = System.Math.Round((Game.State.Securities[ticker].Bid + Game.State.Securities[ticker].Ask) / 2m, Game.State.Securities[ticker].Parameters.QuotedDecimals);
				Game.State.Securities[ticker].LadderVolumeUpdated += this.SecurityUpdated;
				this.LadderGrid.Refresh();
				DataView dataSource = new DataView(Game.State.PortfolioTable, string.Format("Ticker='{0}'", ticker), "", DataViewRowState.CurrentRows);
				System.Windows.Forms.Binding binding = this.LastLabel.DataBindings.Add("Text", dataSource, "Last");
				binding = new System.Windows.Forms.Binding("ForeColor", dataSource, "Last", true);
				binding.Format += delegate(object ss, System.Windows.Forms.ConvertEventArgs ee)
				{
					ee.Value = ((Game.State.Securities[ticker].LastChange != 0m) ? ((Game.State.Securities[ticker].LastChange > 0m) ? System.Drawing.Color.DarkGreen : System.Drawing.Color.DarkRed) : System.Drawing.Color.Black);
				};
				this.LastLabel.DataBindings.Add(binding);
				binding = this.PositionLabel.DataBindings.Add("Text", dataSource, "Position");
				binding = new System.Windows.Forms.Binding("ForeColor", dataSource, "Position", true);
				binding.Format += delegate(object ss, System.Windows.Forms.ConvertEventArgs ee)
				{
					decimal d = (decimal)ee.Value;
					ee.Value = ((d != 0m) ? ((d > 0m) ? System.Drawing.Color.DarkGreen : System.Drawing.Color.DarkRed) : System.Drawing.Color.Black);
				};
				this.PositionLabel.DataBindings.Add(binding);
				binding = this.VWAPLabel.DataBindings.Add("Text", dataSource, "VWAP", true, System.Windows.Forms.DataSourceUpdateMode.Never, null, Game.State.Securities[ticker].Parameters.FormatString);
				return;
			}
			this.PositionLabel.Text = (this.VWAPLabel.Text = (this.LastLabel.Text = "--"));
			this.LadderGrid.Refresh();
		}
		private void LadderGrid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == ' ')
			{
				this.CenterLadder();
				if (this.TickerComboBox.ComboBox.SelectedItem != null)
				{
					string key = this.TickerComboBox.ComboBox.SelectedItem.ToString();
					this.MidmarketValue = System.Math.Round((Game.State.Securities[key].Bid + Game.State.Securities[key].Ask) / 2m, Game.State.Securities[key].Parameters.QuotedDecimals);
					this.LadderGrid.Refresh();
				}
			}
		}
		private void LadderGrid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (this.TickerComboBox.ComboBox.SelectedItem != null && this.ShortcutTradesButton.Checked && this.V.Value > 0m)
			{
				string ticker = this.TickerComboBox.ComboBox.SelectedItem.ToString();
				if (Game.State.Current.IsActive && ((GridEX)sender).HitTest(e.X, e.Y) == GridArea.Cell)
				{
					try
					{
						int rowPosition = ((GridEX)sender).RowPositionFromPoint(e.X, e.Y);
						string key = ((GridEX)sender).ColumnFromPoint(e.X, e.Y).Key;
						GridEXRow row = ((GridEX)sender).GetRow(rowPosition);
						decimal volume = this.V.Value;
						decimal price = (decimal)row.Cells["Price"].Value;
						if (e.Button == System.Windows.Forms.MouseButtons.Left && (key == "Bid Qty" || key == "Ask Qty"))
						{
							int[] ids = (
								from x in Game.State.OrderTable.Select(string.Format(System.Globalization.CultureInfo.InvariantCulture.NumberFormat, "[Price] = {0}", new object[]
								{
									price
								}))
								select (int)x["ID"]).ToArray<int>();
							ServiceManager.Execute(delegate(IClientService p)
							{
								p.CancelOrder(ticker, ids);
							});
						}
						else
						{
							if (e.Button == System.Windows.Forms.MouseButtons.Left)
							{
								if (key == "Bid Size")
								{
									ServiceManager.Execute(delegate(IClientService x)
									{
										x.AddOrder(ticker, volume, OrderType.LIMIT, price);
									});
								}
								else
								{
									if (key == "Ask Size")
									{
										ServiceManager.Execute(delegate(IClientService x)
										{
											x.AddOrder(ticker, volume * -1m, OrderType.LIMIT, price);
										});
									}
								}
							}
						}
					}
					catch (FaultException ex)
					{
						DialogHelper.ShowError(ex.Message, "Error");
					}
				}
			}
		}
		private void MainToolStrip_Layout(object sender, System.Windows.Forms.LayoutEventArgs e)
		{
			int num = this.MainToolStrip.DisplayRectangle.Width;
			foreach (System.Windows.Forms.ToolStripItem toolStripItem in this.MainToolStrip.Items)
			{
				if (toolStripItem != this.SpringLabel)
				{
					num -= toolStripItem.Width;
					num -= toolStripItem.Margin.Horizontal;
				}
			}
			this.SpringLabel.Width = System.Math.Max(0, num - this.SpringLabel.Margin.Horizontal);
		}
		private void ShortcutTradesButton_CheckedChanged(object sender, System.EventArgs e)
		{
			((System.Windows.Forms.ToolStripButton)sender).AutoSize = false;
			if (((System.Windows.Forms.ToolStripButton)sender).Checked)
			{
				((System.Windows.Forms.ToolStripButton)sender).Text = "ON";
				return;
			}
			((System.Windows.Forms.ToolStripButton)sender).Text = "OFF";
		}
		private void LadderTrader_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			int num = this.TickerComboBox.ComboBox.Items.IndexOf(e.Data.GetData("TTS.TickerString").ToString());
			if (num >= 0)
			{
				this.TickerComboBox.ComboBox.SelectedIndex = num;
			}
		}
		private void LadderTrader_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if (e.Data.GetData(typeof(string)) != null)
			{
				e.Effect = System.Windows.Forms.DragDropEffects.Copy;
			}
		}
	}
}
