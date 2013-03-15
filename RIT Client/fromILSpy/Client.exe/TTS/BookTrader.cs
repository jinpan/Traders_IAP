using Janus.Windows.GridEX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.ServiceModel;
using System.Windows.Forms;
using TTS.Properties;
namespace TTS
{
	public class BookTrader : TTSForm
	{
		private IContainer components;
		private System.Windows.Forms.ToolStrip MainToolStrip;
		private System.Windows.Forms.Panel panel1;
		private GridEX AskGrid;
		private GridEX BidGrid;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStripComboBox TickerComboBox;
		private System.Windows.Forms.ToolStripLabel SpringLabel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label PositionLabel;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label VWAPLabel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label LastLabel;
		private System.Windows.Forms.ToolStripLabel toolStripLabel2;
		private System.Windows.Forms.ToolStripButton ShortcutTradesButton;
		public static WindowType TTSWindowType = WindowType.BOOK_TRADER;
		private System.Windows.Forms.NumericUpDown V;
		private System.Windows.Forms.NumericUpDown O;
		private bool Initialized;
		private string LastTicker;
		private System.Collections.Generic.Dictionary<string, Tuple<decimal, decimal>> CachedShortcutSettings = new System.Collections.Generic.Dictionary<string, Tuple<decimal, decimal>>();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(BookTrader));
			this.MainToolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.TickerComboBox = new System.Windows.Forms.ToolStripComboBox();
			this.SpringLabel = new System.Windows.Forms.ToolStripLabel();
			this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
			this.ShortcutTradesButton = new System.Windows.Forms.ToolStripButton();
			this.panel1 = new System.Windows.Forms.Panel();
			this.AskGrid = new GridEX();
			this.BidGrid = new GridEX();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.label2 = new System.Windows.Forms.Label();
			this.LastLabel = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.PositionLabel = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.VWAPLabel = new System.Windows.Forms.Label();
			this.MainToolStrip.SuspendLayout();
			this.panel1.SuspendLayout();
			((ISupportInitialize)this.AskGrid).BeginInit();
			((ISupportInitialize)this.BidGrid).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
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
			this.panel1.Controls.Add(this.AskGrid);
			this.panel1.Controls.Add(this.BidGrid);
			this.panel1.Controls.Add(this.tableLayoutPanel1);
			this.panel1.Controls.Add(this.MainToolStrip);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(384, 462);
			this.panel1.TabIndex = 1;
			this.AskGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AskGrid.Location = new System.Drawing.Point(195, 44);
			this.AskGrid.Name = "AskGrid";
			this.AskGrid.Size = new System.Drawing.Size(189, 418);
			this.AskGrid.TabIndex = 3;
			this.AskGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_MouseDown);
			this.BidGrid.Dock = System.Windows.Forms.DockStyle.Left;
			this.BidGrid.Location = new System.Drawing.Point(0, 44);
			this.BidGrid.Name = "BidGrid";
			this.BidGrid.Size = new System.Drawing.Size(195, 418);
			this.BidGrid.TabIndex = 2;
			this.BidGrid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Grid_MouseDown);
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
			this.flowLayoutPanel1.Location = new System.Drawing.Point(89, 3);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(205, 13);
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
			this.label3.Size = new System.Drawing.Size(35, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Cost:";
			this.VWAPLabel.AutoSize = true;
			this.VWAPLabel.Location = new System.Drawing.Point(187, 0);
			this.VWAPLabel.Name = "VWAPLabel";
			this.VWAPLabel.Size = new System.Drawing.Size(15, 13);
			this.VWAPLabel.TabIndex = 3;
			this.VWAPLabel.Text = "--";
			this.AllowDrop = true;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(384, 462);
			base.Controls.Add(this.panel1);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.Name = "BookTrader";
			this.Text = "Book Trader";
			base.DragDrop += new System.Windows.Forms.DragEventHandler(this.BookTrader_DragDrop);
			base.DragOver += new System.Windows.Forms.DragEventHandler(this.BookTrader_DragOver);
			base.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Grid_KeyPress);
			this.MainToolStrip.ResumeLayout(false);
			this.MainToolStrip.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((ISupportInitialize)this.AskGrid).EndInit();
			((ISupportInitialize)this.BidGrid).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			base.ResumeLayout(false);
		}
		public BookTrader(TTSFormState state) : base(state)
		{
			this.InitializeComponent();
			this.panel1.SizeChanged += delegate(object sender, System.EventArgs e)
			{
				this.BidGrid.Width = this.panel1.Width / 2;
			};
			this.MainToolStrip.Items.Add(new System.Windows.Forms.ToolStripLabel("V:")
			{
				Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0)
			});
			this.MainToolStrip.Items.Add(new System.Windows.Forms.ToolStripControlHost(this.V = new System.Windows.Forms.NumericUpDown
			{
				Maximum = 9999m,
				Increment = 10m,
				Value = 10m,
				Height = 22,
				AutoSize = true
			})
			{
				AutoSize = true
			});
			this.MainToolStrip.Items.Add(new System.Windows.Forms.ToolStripLabel("O:")
			{
				Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0)
			});
			this.MainToolStrip.Items.Add(new System.Windows.Forms.ToolStripControlHost(this.O = new System.Windows.Forms.NumericUpDown
			{
				Maximum = 9999m,
				Value = 1m,
				Height = 22,
				AutoSize = true
			})
			{
				AutoSize = true
			});
			base.BindComboBox(this.TickerComboBox.ComboBox, new System.EventHandler(this.ComboBox_SelectedIndexChanged), delegate(System.Windows.Forms.ComboBox cb)
			{
				cb.DataSource = Game.State.ActiveTradableSecurities;
			});
			this.BidGrid.Format();
			this.BidGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.AskGrid.Format();
			this.AskGrid.RightToLeft = System.Windows.Forms.RightToLeft.No;
			if (state != null && state.State != null)
			{
				this.TickerComboBox.ComboBox.SelectedItem = state.State;
			}
			base.FormClosed += delegate(object sender, System.Windows.Forms.FormClosedEventArgs e)
			{
				if (this.BidGrid.DataSource != null)
				{
					DataView dataView = (DataView)this.BidGrid.DataSource;
					this.BidGrid.DataSource = null;
					dataView.Dispose();
				}
				if (this.AskGrid.DataSource != null)
				{
					DataView dataView2 = (DataView)this.AskGrid.DataSource;
					this.AskGrid.DataSource = null;
					dataView2.Dispose();
				}
			};
		}
		protected override object GetState()
		{
			return this.TickerComboBox.ComboBox.SelectedItem;
		}
		private void ComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.PositionLabel.DataBindings.Clear();
			this.VWAPLabel.DataBindings.Clear();
			this.LastLabel.DataBindings.Clear();
			if (this.BidGrid.DataSource != null)
			{
				DataView dataView = (DataView)this.BidGrid.DataSource;
				this.BidGrid.DataSource = null;
				dataView.Dispose();
			}
			if (this.AskGrid.DataSource != null)
			{
				DataView dataView2 = (DataView)this.AskGrid.DataSource;
				this.AskGrid.DataSource = null;
				dataView2.Dispose();
			}
			if (this.TickerComboBox.ComboBox.SelectedItem != null)
			{
				string ticker = this.TickerComboBox.ComboBox.SelectedItem.ToString();
				if (this.LastTicker != null)
				{
					if (!this.CachedShortcutSettings.ContainsKey(this.LastTicker))
					{
						this.CachedShortcutSettings.Add(this.LastTicker, Tuple.Create<decimal, decimal>(this.V.Value, this.O.Value));
					}
					else
					{
						this.CachedShortcutSettings[this.LastTicker] = Tuple.Create<decimal, decimal>(this.V.Value, this.O.Value);
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
					this.V.Value = System.Math.Min(this.CachedShortcutSettings[ticker].Item1, this.V.Maximum);
					this.O.Value = this.CachedShortcutSettings[ticker].Item2;
				}
				this.BidGrid.SetDataBinding(new DataView(Game.State.OrderTable, string.Format("Ticker='{0}' AND Volume>0", ticker), "Price DESC,ID ASC", DataViewRowState.CurrentRows), "");
				if (!this.Initialized)
				{
					this.BidGrid.RetrieveStructure();
					this.BidGrid.RootTable.FormatColumns(new string[]
					{
						"Price",
						"VolumeRemaining",
						"TraderID"
					}, new string[]
					{
						"Price",
						"Volume",
						"Trader"
					});
					this.BidGrid.RootTable.FormatConditions.Add(new GridEXFormatCondition(this.BidGrid.RootTable.Columns["TraderID"], ConditionOperator.Equal, Game.State.Trader.TraderID)
					{
						FormatStyle = new GridEXFormatStyle(ColorHelper.RowStyleBlue)
					});
					this.BidGrid.RootTable.FormatConditions.Add(new GridEXFormatCondition(this.BidGrid.RootTable.Columns["ColorState"], ConditionOperator.Equal, 1)
					{
						FormatStyle = new GridEXFormatStyle(ColorHelper.RowStyleRed)
					});
					this.BidGrid.RootTable.FormatConditions.Add(new GridEXFormatCondition(this.BidGrid.RootTable.Columns["ColorState"], ConditionOperator.Equal, 2)
					{
						FormatStyle = new GridEXFormatStyle(ColorHelper.RowStyleGreen)
					});
					foreach (GridEXColumn gridEXColumn in this.BidGrid.RootTable.Columns)
					{
						gridEXColumn.RightToLeft = System.Windows.Forms.RightToLeft.No;
						gridEXColumn.TextAlignment = TextAlignment.Center;
					}
					this.BidGrid.LoadingRow += delegate(object se, RowLoadEventArgs ex)
					{
						if (ex.Row.DataRow == null)
						{
							return;
						}
						if (Game.State.General.IsAnonymousTrading && (string)ex.Row.Cells["TraderID"].Value != Game.State.Trader.TraderID)
						{
							ex.Row.Cells["TraderID"].Text = "ANON";
						}
					};
				}
				this.BidGrid.RootTable.FormatDecimalColumns(Game.State.Securities[ticker].Parameters.FormatString, new string[]
				{
					"Price"
				});
				this.BidGrid.RootTable.FormatDecimalColumns(Game.State.Securities[ticker].Parameters.VolumeFormatString, new string[]
				{
					"VolumeRemaining"
				});
				this.BidGrid.Refresh();
				this.AskGrid.SetDataBinding(new DataView(Game.State.OrderTable, string.Format("Ticker='{0}' AND Volume<0", ticker), "Price ASC,ID ASC", DataViewRowState.CurrentRows), "");
				if (!this.Initialized)
				{
					this.AskGrid.RetrieveStructure();
					this.AskGrid.RootTable.FormatColumns(new string[]
					{
						"Price",
						"VolumeRemaining",
						"TraderID"
					}, new string[]
					{
						"Price",
						"Volume",
						"Trader"
					});
					this.AskGrid.RootTable.FormatConditions.Add(new GridEXFormatCondition(this.AskGrid.RootTable.Columns["TraderID"], ConditionOperator.Equal, Game.State.Trader.TraderID)
					{
						FormatStyle = new GridEXFormatStyle(ColorHelper.RowStyleBlue)
					});
					this.AskGrid.RootTable.FormatConditions.Add(new GridEXFormatCondition(this.AskGrid.RootTable.Columns["ColorState"], ConditionOperator.Equal, 1)
					{
						FormatStyle = new GridEXFormatStyle(ColorHelper.RowStyleRed)
					});
					this.AskGrid.RootTable.FormatConditions.Add(new GridEXFormatCondition(this.AskGrid.RootTable.Columns["ColorState"], ConditionOperator.Equal, 2)
					{
						FormatStyle = new GridEXFormatStyle(ColorHelper.RowStyleGreen)
					});
					foreach (GridEXColumn gridEXColumn2 in this.AskGrid.RootTable.Columns)
					{
						gridEXColumn2.RightToLeft = System.Windows.Forms.RightToLeft.No;
						gridEXColumn2.TextAlignment = TextAlignment.Center;
					}
					this.AskGrid.LoadingRow += delegate(object se, RowLoadEventArgs ex)
					{
						if (ex.Row.DataRow == null)
						{
							return;
						}
						if (Game.State.General.IsAnonymousTrading && (string)ex.Row.Cells["TraderID"].Value != Game.State.Trader.TraderID)
						{
							ex.Row.Cells["TraderID"].Text = "ANON";
						}
					};
				}
				this.AskGrid.RootTable.FormatDecimalColumns(Game.State.Securities[ticker].Parameters.FormatString, new string[]
				{
					"Price"
				});
				this.AskGrid.RootTable.FormatDecimalColumns(Game.State.Securities[ticker].Parameters.VolumeFormatString, new string[]
				{
					"VolumeRemaining"
				});
				this.AskGrid.Refresh();
				this.Initialized = true;
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
		}
		private void Grid_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (this.TickerComboBox.ComboBox.SelectedItem != null && this.ShortcutTradesButton.Checked && this.V.Value > 0m)
			{
				string ticker = this.TickerComboBox.ComboBox.SelectedItem.ToString();
				if (Game.State.Current.IsActive && ((GridEX)sender).HitTest(e.X, e.Y) == GridArea.Cell)
				{
					try
					{
						int num = ((GridEX)sender).RowPositionFromPoint(e.X, e.Y);
						DataRow row = ((DataRowView)((GridEX)sender).GetRow(num).DataRow).Row;
						int dir = (sender == this.BidGrid) ? 1 : -1;
						decimal volume = System.Convert.ToDecimal(this.V.Value);
						decimal offset = System.Convert.ToDecimal(System.Math.Pow(10.0, (double)(-1 * Game.State.Securities[ticker].Parameters.QuotedDecimals))) * this.O.Value;
						decimal price = (decimal)row["Price"];
						if (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Shift && e.Button == System.Windows.Forms.MouseButtons.Right)
						{
							decimal totalvolume = 0m;
							for (int i = num; i >= 0; i--)
							{
								totalvolume += (decimal)((GridEX)sender).GetRow(i).Cells["VolumeRemaining"].Value;
							}
							if (totalvolume > 0m)
							{
								ServiceManager.Execute(delegate(IClientService x)
								{
									x.AddOrder(ticker, totalvolume * dir * -1m, OrderType.LIMIT, price);
								});
							}
						}
						else
						{
							if (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Alt && e.Button == System.Windows.Forms.MouseButtons.Left)
							{
								if ((string)row["TraderID"] == Game.State.Trader.TraderID)
								{
									ServiceManager.Execute(delegate(IClientService x)
									{
										x.CancelOrder(ticker, new int[]
										{
											(int)row["ID"]
										});
									});
								}
							}
							else
							{
								if (e.Button == System.Windows.Forms.MouseButtons.Right && System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.None)
								{
									ServiceManager.Execute(delegate(IClientService x)
									{
										x.AddOrder(ticker, volume * dir * -1m, OrderType.MARKET, 0m);
									});
								}
								else
								{
									if (e.Button == System.Windows.Forms.MouseButtons.Left && System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.None)
									{
										ServiceManager.Execute(delegate(IClientService x)
										{
											x.AddOrder(ticker, volume * dir, OrderType.LIMIT, price + offset * dir);
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
		private void Grid_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == ' ')
			{
				if (this.AskGrid.RowCount > 0)
				{
					this.AskGrid.FirstRow = (this.AskGrid.Row = 0);
				}
				if (this.BidGrid.RowCount > 0)
				{
					this.BidGrid.FirstRow = (this.BidGrid.Row = 0);
				}
			}
		}
		private void BookTrader_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
		{
			if (e.Data.GetData(typeof(string)) != null)
			{
				e.Effect = System.Windows.Forms.DragDropEffects.Copy;
			}
		}
		private void BookTrader_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
		{
			int num = this.TickerComboBox.ComboBox.Items.IndexOf(e.Data.GetData("TTS.TickerString").ToString());
			if (num >= 0)
			{
				this.TickerComboBox.ComboBox.SelectedIndex = num;
			}
		}
	}
}
