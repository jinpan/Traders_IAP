using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Windows.Forms;
namespace TTS
{
	public class OrderEntryPanel : TTSPanel
	{
		private IContainer components;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button SubmitButton;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox TickerDropDown;
		private System.Windows.Forms.NumericUpDown PriceUpDown;
		private System.Windows.Forms.Button TypeButton;
		private System.Windows.Forms.Button BuySellButton;
		private System.Windows.Forms.NumericUpDown VolumeUpDown;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label AddRemovePanelLink;
		public Action AddOrderEntryPanelClick = delegate
		{
		};
		public System.Action<OrderEntryPanel> RemoveOrderEntryPanelClick = delegate
		{
		};
		private bool IsBuy = true;
		private bool IsMarket = true;
		private bool IsFirst;
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.SubmitButton = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.TickerDropDown = new System.Windows.Forms.ComboBox();
			this.PriceUpDown = new System.Windows.Forms.NumericUpDown();
			this.TypeButton = new System.Windows.Forms.Button();
			this.BuySellButton = new System.Windows.Forms.Button();
			this.VolumeUpDown = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.AddRemovePanelLink = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			((ISupportInitialize)this.PriceUpDown).BeginInit();
			((ISupportInitialize)this.VolumeUpDown).BeginInit();
			base.SuspendLayout();
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 6;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.SubmitButton, 5, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 4, 0);
			this.tableLayoutPanel1.Controls.Add(this.TickerDropDown, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.PriceUpDown, 4, 1);
			this.tableLayoutPanel1.Controls.Add(this.TypeButton, 3, 1);
			this.tableLayoutPanel1.Controls.Add(this.BuySellButton, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.VolumeUpDown, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.label2, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.AddRemovePanelLink, 5, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(411, 41);
			this.tableLayoutPanel1.TabIndex = 1;
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new System.Drawing.Point(3, 3);
			this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 11);
			this.label1.TabIndex = 28;
			this.label1.Text = "Ticker:";
			this.SubmitButton.BackColor = System.Drawing.Color.LightGreen;
			this.SubmitButton.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.SubmitButton.Location = new System.Drawing.Point(333, 17);
			this.SubmitButton.Name = "SubmitButton";
			this.SubmitButton.Size = new System.Drawing.Size(75, 21);
			this.SubmitButton.TabIndex = 5;
			this.SubmitButton.Text = "Submit BUY";
			this.SubmitButton.UseVisualStyleBackColor = false;
			this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label3.Location = new System.Drawing.Point(257, 3);
			this.label3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(34, 11);
			this.label3.TabIndex = 35;
			this.label3.Text = "Price:";
			this.TickerDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TickerDropDown.FormattingEnabled = true;
			this.TickerDropDown.Location = new System.Drawing.Point(3, 17);
			this.TickerDropDown.Name = "TickerDropDown";
			this.TickerDropDown.Size = new System.Drawing.Size(80, 21);
			this.TickerDropDown.TabIndex = 0;
			this.PriceUpDown.Increment = new decimal(new int[]
			{
				1,
				0,
				0,
				131072
			});
			this.PriceUpDown.Location = new System.Drawing.Point(257, 17);
			System.Windows.Forms.NumericUpDown arg_576_0 = this.PriceUpDown;
			int[] array = new int[4];
			array[0] = 1000000000;
			arg_576_0.Maximum = new decimal(array);
			this.PriceUpDown.Name = "PriceUpDown";
			this.PriceUpDown.Size = new System.Drawing.Size(70, 21);
			this.PriceUpDown.TabIndex = 4;
			this.TypeButton.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.TypeButton.Location = new System.Drawing.Point(211, 17);
			this.TypeButton.Name = "TypeButton";
			this.TypeButton.Size = new System.Drawing.Size(40, 21);
			this.TypeButton.TabIndex = 3;
			this.TypeButton.Text = "MKT";
			this.TypeButton.UseVisualStyleBackColor = true;
			this.TypeButton.Click += new System.EventHandler(this.TypeButton_Click);
			this.BuySellButton.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.BuySellButton.Location = new System.Drawing.Point(89, 17);
			this.BuySellButton.Name = "BuySellButton";
			this.BuySellButton.Size = new System.Drawing.Size(40, 21);
			this.BuySellButton.TabIndex = 1;
			this.BuySellButton.Text = "BUY";
			this.BuySellButton.UseVisualStyleBackColor = true;
			this.BuySellButton.Click += new System.EventHandler(this.BuySellButton_Click);
			System.Windows.Forms.NumericUpDown arg_6EE_0 = this.VolumeUpDown;
			int[] array2 = new int[4];
			array2[0] = 10;
			arg_6EE_0.Increment = new decimal(array2);
			this.VolumeUpDown.Location = new System.Drawing.Point(135, 17);
			System.Windows.Forms.NumericUpDown arg_725_0 = this.VolumeUpDown;
			int[] array3 = new int[4];
			array3[0] = 1000000000;
			arg_725_0.Maximum = new decimal(array3);
			this.VolumeUpDown.Name = "VolumeUpDown";
			this.VolumeUpDown.Size = new System.Drawing.Size(70, 21);
			this.VolumeUpDown.TabIndex = 2;
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new System.Drawing.Point(135, 3);
			this.label2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 11);
			this.label2.TabIndex = 29;
			this.label2.Text = "Volume:";
			this.AddRemovePanelLink.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.AddRemovePanelLink.AutoSize = true;
			this.AddRemovePanelLink.Cursor = System.Windows.Forms.Cursors.Hand;
			this.AddRemovePanelLink.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold);
			this.AddRemovePanelLink.ForeColor = System.Drawing.Color.Green;
			this.AddRemovePanelLink.Location = new System.Drawing.Point(389, 2);
			this.AddRemovePanelLink.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
			this.AddRemovePanelLink.Name = "AddRemovePanelLink";
			this.AddRemovePanelLink.Size = new System.Drawing.Size(22, 11);
			this.AddRemovePanelLink.TabIndex = 37;
			this.AddRemovePanelLink.Text = "[+]";
			this.AddRemovePanelLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.AddRemovePanelLink.Click += new System.EventHandler(this.AddRemovePanelButton_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			base.Controls.Add(this.tableLayoutPanel1);
			base.Margin = new System.Windows.Forms.Padding(0);
			base.Name = "OrderEntryPanel";
			base.Size = new System.Drawing.Size(411, 41);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((ISupportInitialize)this.PriceUpDown).EndInit();
			((ISupportInitialize)this.VolumeUpDown).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		public OrderEntryPanel(bool isfirst)
		{
			this.InitializeComponent();
			this.IsFirst = isfirst;
			if (!isfirst)
			{
				this.AddRemovePanelLink.Text = "[–]";
				this.AddRemovePanelLink.ForeColor = System.Drawing.Color.Red;
			}
			base.BindComboBox(this.TickerDropDown, delegate(object sender, System.EventArgs e)
			{
				if (this.TickerDropDown.SelectedItem != null)
				{
					string key = this.TickerDropDown.SelectedItem.ToString();
					if (Game.State.Securities.ContainsKey(key))
					{
						this.PriceUpDown.DecimalPlaces = Game.State.Securities[key].Parameters.QuotedDecimals;
						this.PriceUpDown.Increment = Game.State.Securities[key].Parameters.Increment;
						this.PriceUpDown.Maximum = Game.State.Securities[key].Parameters.MaxPrice;
						this.PriceUpDown.Minimum = Game.State.Securities[key].Parameters.MinPrice;
						this.VolumeUpDown.DecimalPlaces = Game.State.Securities[key].Parameters.VolumeDecimals;
						this.VolumeUpDown.Maximum = Game.State.Securities[key].Parameters.MaxTradeSize;
						return;
					}
					if (Game.State.Spreads.ContainsKey(key))
					{
						this.IsMarket = true;
						this.SetUIState();
					}
				}
			}, delegate(System.Windows.Forms.ComboBox cb)
			{
				cb.DataSource = Game.State.ActiveTradableSecurities;
			});
			this.SetUIState();
		}
		public Tuple<bool, bool, decimal, string, decimal> GetState()
		{
			return Tuple.Create<bool, bool, decimal, string, decimal>(this.IsBuy, this.IsMarket, this.PriceUpDown.Value, (this.TickerDropDown.SelectedItem ?? "").ToString(), this.VolumeUpDown.Value);
		}
		public void SetState(Tuple<bool, bool, decimal, string, decimal> state)
		{
			this.IsBuy = state.Item1;
			this.IsMarket = state.Item2;
			this.PriceUpDown.Value = state.Item3;
			this.TickerDropDown.SelectedItem = state.Item4;
			this.VolumeUpDown.Value = state.Item5;
			this.SetUIState();
		}
		private void SetUIState()
		{
			if (this.IsBuy)
			{
				this.BuySellButton.Text = "BUY";
				this.SubmitButton.Text = "Submit BUY";
				this.SubmitButton.BackColor = ColorHelper.TTSGreen;
			}
			else
			{
				this.BuySellButton.Text = "SELL";
				this.SubmitButton.Text = "Submit SELL";
				this.SubmitButton.BackColor = ColorHelper.TTSRed;
			}
			if (this.IsMarket)
			{
				this.TypeButton.Text = "MKT";
				this.PriceUpDown.Text = "";
				this.PriceUpDown.Enabled = false;
				return;
			}
			this.TypeButton.Text = "LMT";
			this.PriceUpDown.Text = this.PriceUpDown.Value.ToString();
			this.PriceUpDown.Enabled = true;
		}
		private void TypeButton_Click(object sender, System.EventArgs e)
		{
			this.IsMarket = !this.IsMarket;
			this.SetUIState();
		}
		private void BuySellButton_Click(object sender, System.EventArgs e)
		{
			this.IsBuy = !this.IsBuy;
			this.SetUIState();
		}
		private void AddRemovePanelButton_Click(object sender, System.EventArgs e)
		{
			if (this.IsFirst)
			{
				this.AddOrderEntryPanelClick();
				return;
			}
			this.RemoveOrderEntryPanelClick(this);
		}
		private void SubmitButton_Click(object sender, System.EventArgs e)
		{
			if (this.TickerDropDown.SelectedItem == null)
			{
				DialogHelper.ShowError("Invalid ticker.", "Error");
				return;
			}
			if (this.PriceUpDown.Value == 0m && !this.IsMarket)
			{
				DialogHelper.ShowError("Invalid price.", "Error");
				return;
			}
			if (this.VolumeUpDown.Value == 0m)
			{
				DialogHelper.ShowError("Invalid volume.", "Error");
				return;
			}
			string ticker = this.TickerDropDown.SelectedItem.ToString();
			decimal price = this.PriceUpDown.Value;
			decimal volume = this.VolumeUpDown.Value * (this.IsBuy ? 1 : -1);
			OrderType type = this.IsMarket ? OrderType.MARKET : OrderType.LIMIT;
			try
			{
				if (Game.State.Securities.ContainsKey(ticker))
				{
					ServiceManager.Execute(delegate(IClientService x)
					{
						x.AddOrder(ticker, volume, type, price);
					});
				}
				else
				{
					if (Game.State.Spreads.ContainsKey(ticker))
					{
						ServiceManager.Execute(delegate(IClientService x)
						{
							x.AddSpreadOrder((
								from y in Game.State.Spreads[ticker].UnderlyingTickers
								select new TickerWeight(y.Ticker, System.Convert.ToInt32(y.Weight * volume))).ToArray<TickerWeight>());
						});
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
