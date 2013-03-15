using System;
using System.ComponentModel;
using System.Drawing;
using System.ServiceModel;
using System.Windows.Forms;
using TTS.Properties;
namespace TTS
{
	public class SpreadEntryPanel : TTSPanel
	{
		public Action AddSpreadEntryPanelClick = delegate
		{
		};
		public System.Action<SpreadEntryPanel> RemoveSpreadEntryPanelClick = delegate
		{
		};
		private bool IsFirst;
		private IContainer components;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.ComboBox BuyDropDown;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox SellDropDown;
		private System.Windows.Forms.Button SwitchButton;
		private System.Windows.Forms.Button SubmitButton;
		private System.Windows.Forms.NumericUpDown SellVolumeUpDown;
		private System.Windows.Forms.NumericUpDown BuyVolumeUpDown;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label AddRemovePanelLink;
		public SpreadEntryPanel(bool isfirst)
		{
			this.InitializeComponent();
			this.IsFirst = isfirst;
			if (!isfirst)
			{
				this.AddRemovePanelLink.Text = "[–]";
				this.AddRemovePanelLink.ForeColor = System.Drawing.Color.Red;
			}
			this.BuyDropDown.BindingContext = new System.Windows.Forms.BindingContext();
			base.BindComboBox(this.BuyDropDown, delegate(object sender, System.EventArgs e)
			{
			}, delegate(System.Windows.Forms.ComboBox cb)
			{
				cb.DataSource = Game.State.ActiveTradableSecurities;
			});
			this.SellDropDown.BindingContext = new System.Windows.Forms.BindingContext();
			base.BindComboBox(this.SellDropDown, delegate(object sender, System.EventArgs e)
			{
			}, delegate(System.Windows.Forms.ComboBox cb)
			{
				cb.DataSource = Game.State.ActiveTradableSecurities;
			});
		}
		public Tuple<string, decimal, string, decimal> GetState()
		{
			return Tuple.Create<string, decimal, string, decimal>((this.BuyDropDown.SelectedItem ?? "").ToString(), this.BuyVolumeUpDown.Value, (this.SellDropDown.SelectedItem ?? "").ToString(), this.SellVolumeUpDown.Value);
		}
		public void SetState(Tuple<string, decimal, string, decimal> state)
		{
			this.BuyDropDown.SelectedItem = state.Item1;
			this.BuyVolumeUpDown.Value = state.Item2;
			this.SellDropDown.SelectedItem = state.Item3;
			this.SellVolumeUpDown.Value = state.Item4;
		}
		private void AddRemovePanelButton_Click(object sender, System.EventArgs e)
		{
			if (this.IsFirst)
			{
				this.AddSpreadEntryPanelClick();
				return;
			}
			this.RemoveSpreadEntryPanelClick(this);
		}
		private void SwitchButton_Click(object sender, System.EventArgs e)
		{
			object selectedItem = this.BuyDropDown.SelectedItem;
			this.BuyDropDown.SelectedItem = this.SellDropDown.SelectedItem;
			this.SellDropDown.SelectedItem = selectedItem;
			decimal value = this.BuyVolumeUpDown.Value;
			this.BuyVolumeUpDown.Value = this.SellVolumeUpDown.Value;
			this.SellVolumeUpDown.Value = value;
		}
		private void SellVolumeUpDown_ValueChanged(object sender, System.EventArgs e)
		{
		}
		private void BuyVolumeUpDown_ValueChanged(object sender, System.EventArgs e)
		{
			this.SellVolumeUpDown.Value = this.BuyVolumeUpDown.Value;
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
			if (this.SellDropDown.SelectedItem == this.BuyDropDown.SelectedItem)
			{
				DialogHelper.ShowError("Buy and sell ticker cannot be the same.", "Error");
				return;
			}
			if (this.BuyVolumeUpDown.Value == 0m)
			{
				DialogHelper.ShowError("Invalid buy volume.", "Error");
				return;
			}
			if (this.SellVolumeUpDown.Value == 0m)
			{
				DialogHelper.ShowError("Invalid sell volume.", "Error");
				return;
			}
			string buyticker = this.BuyDropDown.SelectedItem.ToString();
			string sellticker = this.SellDropDown.SelectedItem.ToString();
			int buyvolume = System.Convert.ToInt32(this.BuyVolumeUpDown.Value);
			int sellvolume = System.Convert.ToInt32(this.SellVolumeUpDown.Value);
			try
			{
				ServiceManager.Execute(delegate(IClientService x)
				{
					x.AddSpreadOrder(new TickerWeight[]
					{
						new TickerWeight(buyticker, buyvolume),
						new TickerWeight(sellticker, sellvolume * -1)
					});
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.BuyVolumeUpDown = new System.Windows.Forms.NumericUpDown();
			this.BuyDropDown = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.SubmitButton = new System.Windows.Forms.Button();
			this.SellVolumeUpDown = new System.Windows.Forms.NumericUpDown();
			this.SellDropDown = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.SwitchButton = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.AddRemovePanelLink = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			((ISupportInitialize)this.BuyVolumeUpDown).BeginInit();
			((ISupportInitialize)this.SellVolumeUpDown).BeginInit();
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
			this.tableLayoutPanel1.Controls.Add(this.BuyVolumeUpDown, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.BuyDropDown, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.SubmitButton, 6, 1);
			this.tableLayoutPanel1.Controls.Add(this.SellVolumeUpDown, 5, 1);
			this.tableLayoutPanel1.Controls.Add(this.SellDropDown, 4, 1);
			this.tableLayoutPanel1.Controls.Add(this.label3, 5, 0);
			this.tableLayoutPanel1.Controls.Add(this.label4, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.SwitchButton, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.label2, 4, 0);
			this.tableLayoutPanel1.Controls.Add(this.AddRemovePanelLink, 6, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(411, 41);
			this.tableLayoutPanel1.TabIndex = 1;
			this.BuyVolumeUpDown.Anchor = System.Windows.Forms.AnchorStyles.None;
			System.Windows.Forms.NumericUpDown arg_33B_0 = this.BuyVolumeUpDown;
			int[] array = new int[4];
			array[0] = 10;
			arg_33B_0.Increment = new decimal(array);
			this.BuyVolumeUpDown.Location = new System.Drawing.Point(3, 17);
			System.Windows.Forms.NumericUpDown arg_36E_0 = this.BuyVolumeUpDown;
			int[] array2 = new int[4];
			array2[0] = 1000000000;
			arg_36E_0.Maximum = new decimal(array2);
			this.BuyVolumeUpDown.Name = "BuyVolumeUpDown";
			this.BuyVolumeUpDown.Size = new System.Drawing.Size(68, 21);
			this.BuyVolumeUpDown.TabIndex = 1;
			this.BuyVolumeUpDown.ValueChanged += new System.EventHandler(this.BuyVolumeUpDown_ValueChanged);
			this.BuyDropDown.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.BuyDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.BuyDropDown.FormattingEnabled = true;
			this.BuyDropDown.Location = new System.Drawing.Point(77, 17);
			this.BuyDropDown.Name = "BuyDropDown";
			this.BuyDropDown.Size = new System.Drawing.Size(80, 21);
			this.BuyDropDown.TabIndex = 0;
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new System.Drawing.Point(3, 3);
			this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(27, 11);
			this.label1.TabIndex = 28;
			this.label1.Text = "Buy:";
			this.SubmitButton.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.SubmitButton.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.SubmitButton.Location = new System.Drawing.Point(353, 17);
			this.SubmitButton.Name = "SubmitButton";
			this.SubmitButton.Size = new System.Drawing.Size(55, 21);
			this.SubmitButton.TabIndex = 5;
			this.SubmitButton.Text = "Submit";
			this.SubmitButton.UseVisualStyleBackColor = true;
			this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
			this.SellVolumeUpDown.Anchor = System.Windows.Forms.AnchorStyles.None;
			System.Windows.Forms.NumericUpDown arg_579_0 = this.SellVolumeUpDown;
			int[] array3 = new int[4];
			array3[0] = 10;
			arg_579_0.Increment = new decimal(array3);
			this.SellVolumeUpDown.Location = new System.Drawing.Point(279, 17);
			System.Windows.Forms.NumericUpDown arg_5B0_0 = this.SellVolumeUpDown;
			int[] array4 = new int[4];
			array4[0] = 1000000000;
			arg_5B0_0.Maximum = new decimal(array4);
			this.SellVolumeUpDown.Name = "SellVolumeUpDown";
			this.SellVolumeUpDown.Size = new System.Drawing.Size(68, 21);
			this.SellVolumeUpDown.TabIndex = 4;
			this.SellVolumeUpDown.ValueChanged += new System.EventHandler(this.SellVolumeUpDown_ValueChanged);
			this.SellDropDown.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.SellDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.SellDropDown.FormattingEnabled = true;
			this.SellDropDown.Location = new System.Drawing.Point(193, 17);
			this.SellDropDown.Name = "SellDropDown";
			this.SellDropDown.Size = new System.Drawing.Size(80, 21);
			this.SellDropDown.TabIndex = 3;
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label3.Location = new System.Drawing.Point(279, 3);
			this.label3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(66, 11);
			this.label3.TabIndex = 35;
			this.label3.Text = "Sell Volume:";
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label4.Location = new System.Drawing.Point(77, 3);
			this.label4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(67, 11);
			this.label4.TabIndex = 40;
			this.label4.Text = "Buy Volume:";
			this.SwitchButton.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.SwitchButton.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.SwitchButton.Image = Resources.arrow_refresh;
			this.SwitchButton.Location = new System.Drawing.Point(160, 17);
			this.SwitchButton.Margin = new System.Windows.Forms.Padding(0);
			this.SwitchButton.Name = "SwitchButton";
			this.SwitchButton.Size = new System.Drawing.Size(30, 21);
			this.SwitchButton.TabIndex = 2;
			this.SwitchButton.UseVisualStyleBackColor = true;
			this.SwitchButton.Click += new System.EventHandler(this.SwitchButton_Click);
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new System.Drawing.Point(193, 3);
			this.label2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(26, 11);
			this.label2.TabIndex = 29;
			this.label2.Text = "Sell:";
			this.AddRemovePanelLink.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.AddRemovePanelLink.AutoSize = true;
			this.AddRemovePanelLink.Cursor = System.Windows.Forms.Cursors.Hand;
			this.AddRemovePanelLink.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold);
			this.AddRemovePanelLink.ForeColor = System.Drawing.Color.Green;
			this.AddRemovePanelLink.Location = new System.Drawing.Point(389, 2);
			this.AddRemovePanelLink.Margin = new System.Windows.Forms.Padding(0, 2, 0, 0);
			this.AddRemovePanelLink.Name = "AddRemovePanelLink";
			this.AddRemovePanelLink.Size = new System.Drawing.Size(22, 11);
			this.AddRemovePanelLink.TabIndex = 41;
			this.AddRemovePanelLink.Text = "[+]";
			this.AddRemovePanelLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.AddRemovePanelLink.Click += new System.EventHandler(this.AddRemovePanelButton_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			base.Controls.Add(this.tableLayoutPanel1);
			base.Margin = new System.Windows.Forms.Padding(0);
			base.Name = "SpreadEntryPanel";
			base.Size = new System.Drawing.Size(411, 41);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((ISupportInitialize)this.BuyVolumeUpDown).EndInit();
			((ISupportInitialize)this.SellVolumeUpDown).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
