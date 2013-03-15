using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Windows.Forms;
namespace TTS
{
	public class TransportArbEntry : TTSForm
	{
		public static WindowType TTSWindowType = WindowType.TRANSPORTATION_ARBITRAGE_ENTRY;
		private IContainer components;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button SubmitButton;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown SellUpDown;
		private System.Windows.Forms.ComboBox BuyDropDown;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ComboBox SellDropDown;
		private System.Windows.Forms.NumericUpDown BuyUpDown;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox TransportationDropDown;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox CostTextBox;
		public TransportArbEntry(TTSFormState state) : base(state)
		{
			this.InitializeComponent();
			this.SellDropDown.BindingContext = new System.Windows.Forms.BindingContext();
			base.BindComboBox(this.SellDropDown, delegate(object sender, System.EventArgs e)
			{
				if (this.BuyDropDown.SelectedItem != null && this.SellDropDown.SelectedItem != null)
				{
					this.TransportationDropDown.Items.Clear();
					this.TransportationDropDown.Items.AddRange((
						from x in Game.State.ActiveTransportationAssets
						where ((TickerWeight[])Game.State.AssetInfoTable.Rows.Find(x)["ConvertFrom"]).Any((TickerWeight y) => y.Ticker == this.BuyDropDown.SelectedItem.ToString()) && ((TickerWeight[])Game.State.AssetInfoTable.Rows.Find(x)["ConvertTo"]).Any((TickerWeight y) => y.Ticker == this.SellDropDown.SelectedItem.ToString())
						select x).ToArray<string>());
					if (this.TransportationDropDown.Items.Count > 0)
					{
						this.TransportationDropDown.SelectedIndex = 0;
					}
					base.ResizeCombo(this.TransportationDropDown);
				}
			}, delegate(System.Windows.Forms.ComboBox cb)
			{
				cb.DataSource = Game.State.ActiveTradableSecurities;
			});
			this.BuyDropDown.BindingContext = new System.Windows.Forms.BindingContext();
			base.BindComboBox(this.BuyDropDown, delegate(object sender, System.EventArgs e)
			{
				if (this.BuyDropDown.SelectedItem != null && this.SellDropDown.SelectedItem != null)
				{
					this.TransportationDropDown.Items.Clear();
					this.TransportationDropDown.Items.AddRange((
						from x in Game.State.ActiveTransportationAssets
						where ((TickerWeight[])Game.State.AssetInfoTable.Rows.Find(x)["ConvertFrom"]).Any((TickerWeight y) => y.Ticker == this.BuyDropDown.SelectedItem.ToString()) && ((TickerWeight[])Game.State.AssetInfoTable.Rows.Find(x)["ConvertTo"]).Any((TickerWeight y) => y.Ticker == this.SellDropDown.SelectedItem.ToString())
						select x).ToArray<string>());
					if (this.TransportationDropDown.Items.Count > 0)
					{
						this.TransportationDropDown.SelectedIndex = 0;
					}
					base.ResizeCombo(this.TransportationDropDown);
				}
			}, delegate(System.Windows.Forms.ComboBox cb)
			{
				cb.DataSource = Game.State.ActiveTradableSecurities;
			});
			this.TransportationDropDown.SelectedIndexChanged += delegate(object sender, System.EventArgs e)
			{
				if (this.TransportationDropDown.SelectedItem != null)
				{
					string key = this.TransportationDropDown.SelectedItem.ToString();
					AssetType assetType = (AssetType)Game.State.AssetInfoTable.Rows.Find(key)["Type"];
					if (assetType == AssetType.SHIP && !this.CostTextBox.Text.StartsWith("WS"))
					{
						this.CostTextBox.Text = "WS" + this.CostTextBox.Text;
						return;
					}
					if (assetType == AssetType.PIPELINE)
					{
						this.CostTextBox.Text = this.CostTextBox.Text.Replace("W", "").Replace("S", "");
					}
				}
			};
		}
		private void SubmitButton_Click(object sender, System.EventArgs e)
		{
			if (this.SellDropDown.SelectedItem == null)
			{
				DialogHelper.ShowError("Invalid buy ticker.", "Error");
				return;
			}
			if (this.BuyDropDown.SelectedItem == null)
			{
				DialogHelper.ShowError("Invalid sell ticker.", "Error");
				return;
			}
			if (this.TransportationDropDown.SelectedItem == null)
			{
				DialogHelper.ShowError("Invalid transportation ticker.", "Error");
				return;
			}
			if (this.SellDropDown.SelectedItem == this.BuyDropDown.SelectedItem)
			{
				DialogHelper.ShowError("Buy and sell ticker cannot be the same.", "Error");
				return;
			}
			if (this.BuyUpDown.Value == 0m)
			{
				DialogHelper.ShowError("Invalid buy volume.", "Error");
				return;
			}
			if (this.SellUpDown.Value == 0m)
			{
				DialogHelper.ShowError("Invalid sell volume.", "Error");
				return;
			}
			string buyticker = this.BuyDropDown.SelectedItem.ToString();
			string sellticker = this.SellDropDown.SelectedItem.ToString();
			string transportticker = this.TransportationDropDown.SelectedItem.ToString();
			AssetType assetType = (AssetType)Game.State.AssetInfoTable.Rows.Find(transportticker)["Type"];
			int buyvolume = System.Convert.ToInt32(this.BuyUpDown.Value);
			int sellvolume = System.Convert.ToInt32(this.SellUpDown.Value);
			string cost = this.CostTextBox.Text.Trim();
			TickerWeight[] array = (TickerWeight[])Game.State.AssetInfoTable.Rows.Find(transportticker)["ConvertFrom"];
			TickerWeight[] array2 = (TickerWeight[])Game.State.AssetInfoTable.Rows.Find(transportticker)["ConvertTo"];
			if (buyvolume * array2[0].Weight / array[0].Weight % 1m != 0m && System.Windows.Forms.DialogResult.OK != DialogHelper.Confirm(string.Format("Specified buy volume will result in lost cargo (volume should be a multiple of {0:0}).\n\nAre you sure you wish to continue?", array[0].Weight / MathHelper.GreatestCommonFactor(new decimal[]
			{
				array2[0].Weight,
				array[0].Weight
			})), "Confirmation", false))
			{
				return;
			}
			try
			{
				ServiceManager.Execute(delegate(IClientService x)
				{
					x.AddTransportArbOrder(buyticker, sellticker, buyvolume, sellvolume, transportticker, cost);
				});
				this.CostTextBox.Text = ((assetType == AssetType.SHIP) ? "WS" : "");
				((Client)ThreadHelper.MainThread).ShowInfo("Deal Entry Succesful", string.Format("Transportation booked at {0}, for {1}x{2} to {3}x{4}.", new object[]
				{
					cost,
					buyvolume,
					buyticker,
					sellvolume,
					sellticker
				}));
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(TransportArbEntry));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SellUpDown = new System.Windows.Forms.NumericUpDown();
			this.BuyDropDown = new System.Windows.Forms.ComboBox();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.SellDropDown = new System.Windows.Forms.ComboBox();
			this.BuyUpDown = new System.Windows.Forms.NumericUpDown();
			this.TransportationDropDown = new System.Windows.Forms.ComboBox();
			this.SubmitButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.CostTextBox = new System.Windows.Forms.TextBox();
			this.tableLayoutPanel1.SuspendLayout();
			((ISupportInitialize)this.SellUpDown).BeginInit();
			((ISupportInitialize)this.BuyUpDown).BeginInit();
			base.SuspendLayout();
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 7;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label3, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.SellUpDown, 3, 1);
			this.tableLayoutPanel1.Controls.Add(this.BuyDropDown, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label4, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label5, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.SellDropDown, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.BuyUpDown, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.TransportationDropDown, 4, 1);
			this.tableLayoutPanel1.Controls.Add(this.SubmitButton, 6, 1);
			this.tableLayoutPanel1.Controls.Add(this.label2, 4, 0);
			this.tableLayoutPanel1.Controls.Add(this.label6, 5, 0);
			this.tableLayoutPanel1.Controls.Add(this.CostTextBox, 5, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(757, 41);
			this.tableLayoutPanel1.TabIndex = 0;
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new System.Drawing.Point(3, 3);
			this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(27, 11);
			this.label1.TabIndex = 28;
			this.label1.Text = "Buy:";
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label3.Location = new System.Drawing.Point(251, 3);
			this.label3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(66, 11);
			this.label3.TabIndex = 35;
			this.label3.Text = "Sell Volume:";
			System.Windows.Forms.NumericUpDown arg_48C_0 = this.SellUpDown;
			int[] array = new int[4];
			array[0] = 10;
			arg_48C_0.Increment = new decimal(array);
			this.SellUpDown.Location = new System.Drawing.Point(251, 17);
			System.Windows.Forms.NumericUpDown arg_4C3_0 = this.SellUpDown;
			int[] array2 = new int[4];
			array2[0] = 1000000000;
			arg_4C3_0.Maximum = new decimal(array2);
			this.SellUpDown.Name = "SellUpDown";
			this.SellUpDown.Size = new System.Drawing.Size(70, 21);
			this.SellUpDown.TabIndex = 3;
			this.BuyDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.BuyDropDown.FormattingEnabled = true;
			this.BuyDropDown.Location = new System.Drawing.Point(3, 17);
			this.BuyDropDown.Name = "BuyDropDown";
			this.BuyDropDown.Size = new System.Drawing.Size(80, 21);
			this.BuyDropDown.TabIndex = 0;
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label4.Location = new System.Drawing.Point(89, 3);
			this.label4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(67, 11);
			this.label4.TabIndex = 38;
			this.label4.Text = "Buy Volume:";
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label5.Location = new System.Drawing.Point(165, 3);
			this.label5.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(26, 11);
			this.label5.TabIndex = 39;
			this.label5.Text = "Sell:";
			this.SellDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SellDropDown.FormattingEnabled = true;
			this.SellDropDown.Location = new System.Drawing.Point(165, 17);
			this.SellDropDown.Name = "SellDropDown";
			this.SellDropDown.Size = new System.Drawing.Size(80, 21);
			this.SellDropDown.TabIndex = 2;
			System.Windows.Forms.NumericUpDown arg_6EF_0 = this.BuyUpDown;
			int[] array3 = new int[4];
			array3[0] = 10;
			arg_6EF_0.Increment = new decimal(array3);
			this.BuyUpDown.Location = new System.Drawing.Point(89, 17);
			System.Windows.Forms.NumericUpDown arg_726_0 = this.BuyUpDown;
			int[] array4 = new int[4];
			array4[0] = 1000000000;
			arg_726_0.Maximum = new decimal(array4);
			this.BuyUpDown.Name = "BuyUpDown";
			this.BuyUpDown.Size = new System.Drawing.Size(70, 21);
			this.BuyUpDown.TabIndex = 1;
			this.TransportationDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TransportationDropDown.FormattingEnabled = true;
			this.TransportationDropDown.Location = new System.Drawing.Point(327, 17);
			this.TransportationDropDown.Name = "TransportationDropDown";
			this.TransportationDropDown.Size = new System.Drawing.Size(120, 21);
			this.TransportationDropDown.TabIndex = 4;
			this.SubmitButton.BackColor = System.Drawing.SystemColors.Control;
			this.SubmitButton.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.SubmitButton.Location = new System.Drawing.Point(529, 17);
			this.SubmitButton.Name = "SubmitButton";
			this.SubmitButton.Size = new System.Drawing.Size(50, 21);
			this.SubmitButton.TabIndex = 6;
			this.SubmitButton.Text = "Submit";
			this.SubmitButton.UseVisualStyleBackColor = true;
			this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new System.Drawing.Point(327, 3);
			this.label2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(83, 11);
			this.label2.TabIndex = 29;
			this.label2.Text = "Transportation:";
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label6.Location = new System.Drawing.Point(453, 3);
			this.label6.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(30, 11);
			this.label6.TabIndex = 41;
			this.label6.Text = "Cost:";
			this.CostTextBox.Location = new System.Drawing.Point(453, 17);
			this.CostTextBox.Name = "CostTextBox";
			this.CostTextBox.Size = new System.Drawing.Size(70, 21);
			this.CostTextBox.TabIndex = 5;
			this.AutoSize = true;
			base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			base.ClientSize = new System.Drawing.Size(757, 41);
			base.Controls.Add(this.tableLayoutPanel1);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "TransportArbEntry";
			this.Text = "Transportation Arbitrage Entry";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((ISupportInitialize)this.SellUpDown).EndInit();
			((ISupportInitialize)this.BuyUpDown).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
