using Janus.Windows.GridEX;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
namespace TTS
{
	public class Portfolio : TTSForm
	{
		public static WindowType TTSWindowType = WindowType.PORTFOLIO;
		private IContainer components;
		private GridEX MainGrid;
		public Portfolio(TTSFormState state) : base(state)
		{
			this.InitializeComponent();
			this.MainGrid.SetDataBinding(Game.State.MarketView, "");
			this.MainGrid.RetrieveStructure();
			this.MainGrid.Format();
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			foreach (GridEXColumn gridEXColumn in this.MainGrid.RootTable.Columns)
			{
				list.Add(gridEXColumn.Key);
			}
			this.MainGrid.RootTable.FormatColumns(new string[]
			{
				"Ticker",
				"Type",
				"UnitMultiplier",
				"Position",
				"VWAP",
				"Last",
				"Bid",
				"Ask",
				"NLV",
				"Realized",
				"Unrealized",
				"SecurityVWAP",
				"Volume"
			}, new string[]
			{
				"Ticker",
				"Type",
				"Contract Size",
				"Position",
				"Cost",
				"Last",
				"Bid",
				"Ask",
				"NLV",
				"Realized P&L",
				"Unrealized P&L",
				"VWAP",
				"Volume"
			});
			this.MainGrid.RootTable.AddPositiveNegativeFormatter(new string[]
			{
				"Position"
			});
			this.MainGrid.RootTable.AddSecurityVolumeDecimalFormatter(new string[]
			{
				"Position",
				"Volume"
			});
			this.MainGrid.RootTable.AddSecurityDecimalFormatter(new string[]
			{
				"VWAP",
				"Bid",
				"Ask",
				"Last"
			});
			this.MainGrid.RootTable.FormatDecimalColumns(Game.State.Currencies[Game.State.DefaultCurrency].FormatString, new string[]
			{
				"NLV",
				"Realized",
				"Unrealized",
				"SecurityVWAP"
			});
			this.MainGrid.AddContextMenu(true);
			this.MainGrid.LoadingRow += delegate(object sender, RowLoadEventArgs e)
			{
				if (e.Row.DataRow == null)
				{
					return;
				}
				string key = e.Row.Cells["Ticker"].Value.ToString();
				if (!string.IsNullOrWhiteSpace(Game.State.Securities[key].Parameters.DisplayUnit))
				{
					e.Row.Cells["UnitMultiplier"].Text = string.Format("{0} {1}", Game.State.Securities[key].Parameters.UnitMultiplier, Game.State.Securities[key].Parameters.DisplayUnit);
				}
				if (Game.State.Securities[key].BidChange > 0m)
				{
					e.Row.Cells["Bid"].FormatStyle = ColorHelper.RowStyleGreen;
				}
				else
				{
					if (Game.State.Securities[key].BidChange < 0m)
					{
						e.Row.Cells["Bid"].FormatStyle = ColorHelper.RowStyleRed;
					}
				}
				if (Game.State.Securities[key].AskChange > 0m)
				{
					e.Row.Cells["Ask"].FormatStyle = ColorHelper.RowStyleGreen;
				}
				else
				{
					if (Game.State.Securities[key].AskChange < 0m)
					{
						e.Row.Cells["Ask"].FormatStyle = ColorHelper.RowStyleRed;
					}
				}
				if (Game.State.Securities[key].LastChange > 0m)
				{
					e.Row.Cells["Last"].FormatStyle = ColorHelper.RowStyleGreen;
				}
				else
				{
					if (Game.State.Securities[key].LastChange < 0m)
					{
						e.Row.Cells["Last"].FormatStyle = ColorHelper.RowStyleRed;
					}
				}
				foreach (GridEXCell gridEXCell in (System.Collections.IEnumerable)e.Row.Cells)
				{
					gridEXCell.ToolTipText = Game.State.Securities[e.Row.Cells["Ticker"].Text].Parameters.Description;
				}
				if (!Game.State.Securities[key].Parameters.IsTradeable)
				{
					e.Row.RowStyle = ColorHelper.RowStyleGray;
				}
				if (Game.State.Securities[key].Parameters.Type == SecurityType.CURRENCY)
				{
					e.Row.Cells["Realized"].Text = "";
					e.Row.Cells["Unrealized"].Text = "";
					e.Row.Cells["SecurityVWAP"].Text = "";
				}
			};
			base.AddResetHandler(delegate
			{
				this.MainGrid.DataSource = Game.State.MarketView;
			});
		}
		private void MainGrid_RowDrag(object sender, RowDragEventArgs e)
		{
			GridEXRow row = this.MainGrid.GetRow();
			if (row != null && row.RowType == RowType.Record && row.DataRow != null)
			{
				System.Windows.Forms.DataObject dataObject = new System.Windows.Forms.DataObject();
				dataObject.SetData(new TickerString((string)row.Cells["Ticker"].Value));
				GridEXColumn gridEXColumn = this.MainGrid.ColumnFromPoint();
				if (gridEXColumn != null)
				{
					if (gridEXColumn.DataMember == "Ticker" || System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control)
					{
						string[] array = (
							from x in this.MainGrid.RootTable.Columns.OfType<GridEXColumn>()
							where x.Visible
							orderby x.Position
							select this.GetRTDString((string)row.Cells["Ticker"].Value, x.DataMember)).ToArray<string>();
						array[0] = (string)row.Cells["Ticker"].Value;
						dataObject.SetData(System.Windows.Forms.DataFormats.Text, string.Join("\t", array));
					}
					else
					{
						dataObject.SetData(System.Windows.Forms.DataFormats.Text, this.GetRTDString((string)row.Cells["Ticker"].Value, gridEXColumn.DataMember));
					}
				}
				this.MainGrid.DoDragDrop(dataObject, System.Windows.Forms.DragDropEffects.Copy);
			}
		}
		private string GetRTDString(string ticker, string field)
		{
			string text = "";
			if (field == "UnitMultiplier")
			{
				text = "SIZE";
			}
			else
			{
				if (field == "Type")
				{
					text = "TYPE";
				}
				else
				{
					if (field == "Position")
					{
						text = "POSITION";
					}
					else
					{
						if (field == "VWAP")
						{
							text = "COST";
						}
						else
						{
							if (field == "Last")
							{
								text = "LAST";
							}
							else
							{
								if (field == "Bid")
								{
									text = "BID";
								}
								else
								{
									if (field == "Ask")
									{
										text = "ASK";
									}
									else
									{
										if (field == "NLV")
										{
											text = "NLV";
										}
										else
										{
											if (field == "Realized")
											{
												text = "PLREL";
											}
											else
											{
												if (field == "Unrealized")
												{
													text = "PLUNR";
												}
												else
												{
													if (field == "Volume")
													{
														text = "VOLUME";
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			if (!string.IsNullOrEmpty(text))
			{
				return string.Format("=RTD(\"{2}\"{3}{3}\"{0}\"{3}\"{1}\")", new object[]
				{
					ticker,
					text,
					Client.Skin.GetString("rtd_string"),
					System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator
				});
			}
			return "";
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Portfolio));
			this.MainGrid = new GridEX();
			((ISupportInitialize)this.MainGrid).BeginInit();
			base.SuspendLayout();
			this.MainGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainGrid.Location = new System.Drawing.Point(0, 0);
			this.MainGrid.Name = "MainGrid";
			this.MainGrid.Size = new System.Drawing.Size(584, 167);
			this.MainGrid.TabIndex = 1;
			this.MainGrid.RowDrag += new RowDragEventHandler(this.MainGrid_RowDrag);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(584, 167);
			base.Controls.Add(this.MainGrid);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "Portfolio";
			this.Text = "Portfolio";
			((ISupportInitialize)this.MainGrid).EndInit();
			base.ResumeLayout(false);
		}
	}
}
