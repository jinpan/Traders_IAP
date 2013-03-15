using Janus.Windows.GridEX;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.ServiceModel;
using System.Windows.Forms;
using TTS.Properties;
namespace TTS
{
	public class OTCEntry : TTSForm
	{
		private IContainer components;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox TraderDropDown;
		private System.Windows.Forms.NumericUpDown PriceUpDown;
		private System.Windows.Forms.Button BuySellButton;
		private System.Windows.Forms.NumericUpDown VolumeUpDown;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox TickerDropDown;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage PendingPage;
		private GridEX PendingGrid;
		private System.Windows.Forms.TabPage CompletedPage;
		private GridEX CompletedGrid;
		private System.Windows.Forms.CheckBox AutoAcceptCheckBox;
		private System.Windows.Forms.Button SubmitButton;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown SettlePeriod;
		private System.Windows.Forms.NumericUpDown SettleTick;
		private System.Windows.Forms.CheckBox IsImmediately;
		public static WindowType TTSWindowType = WindowType.OTC_ENTRY;
		private bool IsBuy = true;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(OTCEntry));
			this.AutoAcceptCheckBox = new System.Windows.Forms.CheckBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.PendingPage = new System.Windows.Forms.TabPage();
			this.PendingGrid = new GridEX();
			this.CompletedPage = new System.Windows.Forms.TabPage();
			this.CompletedGrid = new GridEX();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.TraderDropDown = new System.Windows.Forms.ComboBox();
			this.PriceUpDown = new System.Windows.Forms.NumericUpDown();
			this.VolumeUpDown = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.TickerDropDown = new System.Windows.Forms.ComboBox();
			this.BuySellButton = new System.Windows.Forms.Button();
			this.SubmitButton = new System.Windows.Forms.Button();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.SettlePeriod = new System.Windows.Forms.NumericUpDown();
			this.SettleTick = new System.Windows.Forms.NumericUpDown();
			this.IsImmediately = new System.Windows.Forms.CheckBox();
			this.tabControl1.SuspendLayout();
			this.PendingPage.SuspendLayout();
			((ISupportInitialize)this.PendingGrid).BeginInit();
			this.CompletedPage.SuspendLayout();
			((ISupportInitialize)this.CompletedGrid).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			((ISupportInitialize)this.PriceUpDown).BeginInit();
			((ISupportInitialize)this.VolumeUpDown).BeginInit();
			((ISupportInitialize)this.SettlePeriod).BeginInit();
			((ISupportInitialize)this.SettleTick).BeginInit();
			base.SuspendLayout();
			this.AutoAcceptCheckBox.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.AutoAcceptCheckBox.AutoSize = true;
			this.AutoAcceptCheckBox.Checked = Settings.Default.IsOTCAutoAccept;
			this.AutoAcceptCheckBox.DataBindings.Add(new System.Windows.Forms.Binding("Checked", Settings.Default, "IsOTCAutoAccept", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.AutoAcceptCheckBox.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.AutoAcceptCheckBox.Location = new System.Drawing.Point(422, 2);
			this.AutoAcceptCheckBox.Name = "AutoAcceptCheckBox";
			this.AutoAcceptCheckBox.Size = new System.Drawing.Size(160, 17);
			this.AutoAcceptCheckBox.TabIndex = 0;
			this.AutoAcceptCheckBox.Text = "Auto-Accept All OTC Orders";
			this.AutoAcceptCheckBox.UseVisualStyleBackColor = true;
			this.AutoAcceptCheckBox.CheckedChanged += new System.EventHandler(this.AutoAcceptCheckBox_CheckedChanged);
			this.tabControl1.Controls.Add(this.PendingPage);
			this.tabControl1.Controls.Add(this.CompletedPage);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(584, 180);
			this.tabControl1.TabIndex = 4;
			this.PendingPage.AutoScroll = true;
			this.PendingPage.Controls.Add(this.PendingGrid);
			this.PendingPage.Location = new System.Drawing.Point(4, 22);
			this.PendingPage.Name = "PendingPage";
			this.PendingPage.Size = new System.Drawing.Size(576, 154);
			this.PendingPage.TabIndex = 0;
			this.PendingPage.Text = "Pending";
			this.PendingPage.UseVisualStyleBackColor = true;
			this.PendingGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PendingGrid.Location = new System.Drawing.Point(0, 0);
			this.PendingGrid.Name = "PendingGrid";
			this.PendingGrid.Size = new System.Drawing.Size(576, 154);
			this.PendingGrid.TabIndex = 4;
			this.CompletedPage.Controls.Add(this.CompletedGrid);
			this.CompletedPage.Location = new System.Drawing.Point(4, 22);
			this.CompletedPage.Name = "CompletedPage";
			this.CompletedPage.Size = new System.Drawing.Size(476, 195);
			this.CompletedPage.TabIndex = 1;
			this.CompletedPage.Text = "Completed";
			this.CompletedPage.UseVisualStyleBackColor = true;
			this.CompletedGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.CompletedGrid.Location = new System.Drawing.Point(0, 0);
			this.CompletedGrid.Name = "CompletedGrid";
			this.CompletedGrid.Size = new System.Drawing.Size(476, 195);
			this.CompletedGrid.TabIndex = 5;
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 6;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.SubmitButton, 5, 1);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 3, 0);
			this.tableLayoutPanel1.Controls.Add(this.label3, 4, 0);
			this.tableLayoutPanel1.Controls.Add(this.TraderDropDown, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.PriceUpDown, 4, 1);
			this.tableLayoutPanel1.Controls.Add(this.VolumeUpDown, 3, 1);
			this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.TickerDropDown, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.BuySellButton, 2, 1);
			this.tableLayoutPanel1.Controls.Add(this.label5, 3, 2);
			this.tableLayoutPanel1.Controls.Add(this.label6, 4, 2);
			this.tableLayoutPanel1.Controls.Add(this.SettlePeriod, 3, 3);
			this.tableLayoutPanel1.Controls.Add(this.SettleTick, 4, 3);
			this.tableLayoutPanel1.Controls.Add(this.IsImmediately, 1, 3);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tableLayoutPanel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 180);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(584, 82);
			this.tableLayoutPanel1.TabIndex = 2;
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label4.Location = new System.Drawing.Point(3, 3);
			this.label4.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(43, 11);
			this.label4.TabIndex = 30;
			this.label4.Text = "Trader:";
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new System.Drawing.Point(354, 3);
			this.label2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 11);
			this.label2.TabIndex = 29;
			this.label2.Text = "Volume:";
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label3.Location = new System.Drawing.Point(430, 3);
			this.label3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(34, 11);
			this.label3.TabIndex = 35;
			this.label3.Text = "Price:";
			this.TraderDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TraderDropDown.FormattingEnabled = true;
			this.TraderDropDown.Location = new System.Drawing.Point(3, 17);
			this.TraderDropDown.Name = "TraderDropDown";
			this.TraderDropDown.Size = new System.Drawing.Size(80, 21);
			this.TraderDropDown.TabIndex = 0;
			this.PriceUpDown.Increment = new decimal(new int[]
			{
				1,
				0,
				0,
				131072
			});
			this.PriceUpDown.Location = new System.Drawing.Point(430, 17);
			System.Windows.Forms.NumericUpDown arg_9EE_0 = this.PriceUpDown;
			int[] array = new int[4];
			array[0] = 1000000000;
			arg_9EE_0.Maximum = new decimal(array);
			this.PriceUpDown.Name = "PriceUpDown";
			this.PriceUpDown.Size = new System.Drawing.Size(70, 21);
			this.PriceUpDown.TabIndex = 4;
			System.Windows.Forms.NumericUpDown arg_A3B_0 = this.VolumeUpDown;
			int[] array2 = new int[4];
			array2[0] = 10;
			arg_A3B_0.Increment = new decimal(array2);
			this.VolumeUpDown.Location = new System.Drawing.Point(354, 17);
			System.Windows.Forms.NumericUpDown arg_A75_0 = this.VolumeUpDown;
			int[] array3 = new int[4];
			array3[0] = 1000000000;
			arg_A75_0.Maximum = new decimal(array3);
			this.VolumeUpDown.Name = "VolumeUpDown";
			this.VolumeUpDown.Size = new System.Drawing.Size(70, 21);
			this.VolumeUpDown.TabIndex = 3;
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new System.Drawing.Point(89, 3);
			this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(40, 11);
			this.label1.TabIndex = 1;
			this.label1.Text = "Ticker:";
			this.TickerDropDown.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TickerDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.TickerDropDown.FormattingEnabled = true;
			this.TickerDropDown.Location = new System.Drawing.Point(89, 17);
			this.TickerDropDown.Name = "TickerDropDown";
			this.TickerDropDown.Size = new System.Drawing.Size(213, 21);
			this.TickerDropDown.TabIndex = 1;
			this.BuySellButton.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.BuySellButton.Location = new System.Drawing.Point(308, 17);
			this.BuySellButton.Name = "BuySellButton";
			this.BuySellButton.Size = new System.Drawing.Size(40, 21);
			this.BuySellButton.TabIndex = 2;
			this.BuySellButton.Text = "BUY";
			this.BuySellButton.UseVisualStyleBackColor = true;
			this.BuySellButton.Click += new System.EventHandler(this.BuySellButton_Click);
			this.SubmitButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SubmitButton.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.SubmitButton.Location = new System.Drawing.Point(506, 17);
			this.SubmitButton.Name = "SubmitButton";
			this.tableLayoutPanel1.SetRowSpan(this.SubmitButton, 3);
			this.SubmitButton.Size = new System.Drawing.Size(75, 62);
			this.SubmitButton.TabIndex = 5;
			this.SubmitButton.Text = "Submit";
			this.SubmitButton.UseVisualStyleBackColor = true;
			this.SubmitButton.Click += new System.EventHandler(this.SubmitButton_Click);
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label5.Location = new System.Drawing.Point(354, 44);
			this.label5.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 11);
			this.label5.TabIndex = 36;
			this.label5.Text = "Period:";
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Tahoma", 6.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label6.Location = new System.Drawing.Point(430, 44);
			this.label6.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(29, 11);
			this.label6.TabIndex = 37;
			this.label6.Text = "Tick:";
			this.SettlePeriod.Location = new System.Drawing.Point(354, 58);
			System.Windows.Forms.NumericUpDown arg_E50_0 = this.SettlePeriod;
			int[] array4 = new int[4];
			array4[0] = 1000000000;
			arg_E50_0.Maximum = new decimal(array4);
			System.Windows.Forms.NumericUpDown arg_E6F_0 = this.SettlePeriod;
			int[] array5 = new int[4];
			array5[0] = 1;
			arg_E6F_0.Minimum = new decimal(array5);
			this.SettlePeriod.Name = "SettlePeriod";
			this.SettlePeriod.Size = new System.Drawing.Size(70, 21);
			this.SettlePeriod.TabIndex = 38;
			System.Windows.Forms.NumericUpDown arg_EBF_0 = this.SettlePeriod;
			int[] array6 = new int[4];
			array6[0] = 1;
			arg_EBF_0.Value = new decimal(array6);
			this.SettleTick.Location = new System.Drawing.Point(430, 58);
			System.Windows.Forms.NumericUpDown arg_EF9_0 = this.SettleTick;
			int[] array7 = new int[4];
			array7[0] = 1000000000;
			arg_EF9_0.Maximum = new decimal(array7);
			System.Windows.Forms.NumericUpDown arg_F18_0 = this.SettleTick;
			int[] array8 = new int[4];
			array8[0] = 1;
			arg_F18_0.Minimum = new decimal(array8);
			this.SettleTick.Name = "SettleTick";
			this.SettleTick.Size = new System.Drawing.Size(70, 21);
			this.SettleTick.TabIndex = 39;
			System.Windows.Forms.NumericUpDown arg_F68_0 = this.SettleTick;
			int[] array9 = new int[4];
			array9[0] = 1;
			arg_F68_0.Value = new decimal(array9);
			this.IsImmediately.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.IsImmediately.AutoSize = true;
			this.IsImmediately.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.tableLayoutPanel1.SetColumnSpan(this.IsImmediately, 2);
			this.IsImmediately.Location = new System.Drawing.Point(233, 60);
			this.IsImmediately.Name = "IsImmediately";
			this.IsImmediately.Size = new System.Drawing.Size(115, 17);
			this.IsImmediately.TabIndex = 40;
			this.IsImmediately.Text = "Settle Immediately";
			this.IsImmediately.UseVisualStyleBackColor = true;
			this.IsImmediately.CheckedChanged += new System.EventHandler(this.IsImmediately_CheckedChanged);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(584, 262);
			base.Controls.Add(this.AutoAcceptCheckBox);
			base.Controls.Add(this.tabControl1);
			base.Controls.Add(this.tableLayoutPanel1);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			this.MinimumSize = new System.Drawing.Size(500, 38);
			base.Name = "OTCEntry";
			this.Text = "OTC Trade";
			this.tabControl1.ResumeLayout(false);
			this.PendingPage.ResumeLayout(false);
			((ISupportInitialize)this.PendingGrid).EndInit();
			this.CompletedPage.ResumeLayout(false);
			((ISupportInitialize)this.CompletedGrid).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((ISupportInitialize)this.PriceUpDown).EndInit();
			((ISupportInitialize)this.VolumeUpDown).EndInit();
			((ISupportInitialize)this.SettlePeriod).EndInit();
			((ISupportInitialize)this.SettleTick).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		public OTCEntry(TTSFormState state) : base(state)
		{
			this.InitializeComponent();
			this.AutoAcceptCheckBox_CheckedChanged(this.AutoAcceptCheckBox, null);
			this.PendingGrid.Format();
			this.PendingGrid.SetDataBinding(Game.State.OTCPendingOrderView, "");
			this.PendingGrid.RetrieveStructure();
			this.PendingGrid.RootTable.Columns.Add(new GridEXColumn("Buyer", ColumnType.Text, EditType.NoEdit)
			{
				BoundMode = ColumnBoundMode.UnboundFetch
			});
			this.PendingGrid.RootTable.Columns.Add(new GridEXColumn("Seller", ColumnType.Text, EditType.NoEdit)
			{
				BoundMode = ColumnBoundMode.UnboundFetch
			});
			this.PendingGrid.RootTable.FormatColumns(new string[]
			{
				"Buyer",
				"Seller",
				"Ticker",
				"Price",
				"Volume",
				"SettlePeriod",
				"SettleTick"
			}, new string[]
			{
				"Buyer",
				"Seller",
				"Ticker",
				"Price",
				"Volume",
				"SettlePeriod",
				"Settlement"
			});
			this.PendingGrid.RootTable.AddButtonColumn("Accept", "Accept", "");
			this.PendingGrid.RootTable.AddButtonColumn("Decline", "Decline", "");
			this.PendingGrid.RootTable.AddSecurityVolumeDecimalFormatter(new string[]
			{
				"Volume"
			});
			this.PendingGrid.RootTable.Columns["Decline"].ButtonText = string.Empty;
			this.PendingGrid.ColumnButtonClick += delegate(object sender, ColumnActionEventArgs e)
			{
				DataRow row = ((DataRowView)this.PendingGrid.GetRow().DataRow).Row;
				int id = (int)row["ID"];
				string traderid = (string)row["TraderID"];
				string a = (string)row["Target"];
				try
				{
					if (e.Column.Key == "Decline")
					{
						ServiceManager.Execute(delegate(IClientService p)
						{
							p.UpdateOTCOrder(id, (traderid == Game.State.Trader.TraderID) ? OTCStatus.CANCELLED : OTCStatus.DECLINED);
						});
					}
					else
					{
						if (e.Column.Key == "Accept" && a == Game.State.Trader.TraderID)
						{
							ServiceManager.Execute(delegate(IClientService p)
							{
								p.UpdateOTCOrder(id, OTCStatus.ACCEPTED);
							});
						}
					}
				}
				catch (FaultException ex)
				{
					DialogHelper.ShowError(ex.Message, "Error");
				}
			};
			this.PendingGrid.RootTable.AddPeriodTickFormatter("SettlePeriod", "SettleTick", "Immediately");
			this.PendingGrid.RootTable.AddPositiveNegativeFormatter(new string[]
			{
				"Volume"
			});
			this.PendingGrid.RootTable.AddSecurityDecimalFormatter(new string[]
			{
				"Price"
			});
			this.PendingGrid.LoadingRow += delegate(object sender, RowLoadEventArgs e)
			{
				if (e.Row.DataRow == null)
				{
					return;
				}
				if ((string)e.Row.Cells["TraderID"].Value == Game.State.Trader.TraderID)
				{
					if ((decimal)e.Row.Cells["Volume"].Value > 0m)
					{
						e.Row.Cells["Buyer"].Text = "YOU";
						e.Row.Cells["Buyer"].FormatStyle = new GridEXFormatStyle
						{
							FontBold = TriState.True
						};
						e.Row.Cells["Seller"].Text = (string)e.Row.Cells["Target"].Value;
						e.Row.RowStyle = ColorHelper.RowStyleGreen;
					}
					else
					{
						e.Row.Cells["Seller"].Text = "YOU";
						e.Row.Cells["Seller"].FormatStyle = new GridEXFormatStyle
						{
							FontBold = TriState.True
						};
						e.Row.Cells["Buyer"].Text = (string)e.Row.Cells["Target"].Value;
						e.Row.RowStyle = ColorHelper.RowStyleRed;
					}
					e.Row.Cells["Accept"].ButtonStyle = ButtonStyle.NoButton;
					e.Row.Cells["Decline"].Text = "Cancel";
				}
				else
				{
					if ((string)e.Row.Cells["Target"].Value == Game.State.Trader.TraderID)
					{
						if ((decimal)e.Row.Cells["Volume"].Value < 0m)
						{
							e.Row.Cells["Buyer"].Text = "YOU";
							e.Row.Cells["Buyer"].FormatStyle = new GridEXFormatStyle
							{
								FontBold = TriState.True
							};
							e.Row.Cells["Seller"].Text = (string)e.Row.Cells["TraderID"].Value;
							e.Row.RowStyle = ColorHelper.RowStyleGreen;
						}
						else
						{
							e.Row.Cells["Seller"].Text = "YOU";
							e.Row.Cells["Seller"].FormatStyle = new GridEXFormatStyle
							{
								FontBold = TriState.True
							};
							e.Row.Cells["Buyer"].Text = (string)e.Row.Cells["TraderID"].Value;
							e.Row.RowStyle = ColorHelper.RowStyleRed;
						}
						e.Row.Cells["Decline"].Text = "Decline";
					}
				}
				e.Row.Cells["Volume"].Text = System.Math.Abs((decimal)e.Row.Cells["Volume"].Value).ToString();
			};
			this.PendingGrid.AddContextMenu(false);
			this.CompletedGrid.Format();
			this.CompletedGrid.SetDataBinding(Game.State.OTCCompletedOrderView, "");
			this.CompletedGrid.RetrieveStructure();
			this.CompletedGrid.RootTable.Columns.Add(new GridEXColumn("Buyer", ColumnType.Text, EditType.NoEdit)
			{
				BoundMode = ColumnBoundMode.UnboundFetch
			});
			this.CompletedGrid.RootTable.Columns.Add(new GridEXColumn("Seller", ColumnType.Text, EditType.NoEdit)
			{
				BoundMode = ColumnBoundMode.UnboundFetch
			});
			this.CompletedGrid.RootTable.FormatColumns(new string[]
			{
				"Buyer",
				"Seller",
				"Ticker",
				"Price",
				"Volume",
				"SettlePeriod",
				"SettleTick",
				"Status"
			}, new string[]
			{
				"Buyer",
				"Seller",
				"Ticker",
				"Price",
				"Volume",
				"SettlePeriod",
				"Settlement",
				"Status"
			});
			this.CompletedGrid.RootTable.AddPeriodTickFormatter("SettlePeriod", "SettleTick", "");
			this.CompletedGrid.RootTable.AddSecurityDecimalFormatter(new string[]
			{
				"Price"
			});
			this.CompletedGrid.RootTable.AddSecurityVolumeDecimalFormatter(new string[]
			{
				"Volume"
			});
			this.CompletedGrid.LoadingRow += delegate(object sender, RowLoadEventArgs e)
			{
				if (e.Row.DataRow == null)
				{
					return;
				}
				if ((string)e.Row.Cells["TraderID"].Value == Game.State.Trader.TraderID)
				{
					if ((decimal)e.Row.Cells["Volume"].Value > 0m)
					{
						e.Row.Cells["Buyer"].Text = "YOU";
						e.Row.Cells["Buyer"].FormatStyle = new GridEXFormatStyle
						{
							FontBold = TriState.True
						};
						e.Row.Cells["Seller"].Text = (string)e.Row.Cells["Target"].Value;
						if ((OTCStatus)e.Row.Cells["Status"].Value == OTCStatus.ACCEPTED)
						{
							e.Row.RowStyle = ColorHelper.RowStyleGreen;
						}
						else
						{
							e.Row.RowStyle = ColorHelper.RowStyleGray;
						}
					}
					else
					{
						e.Row.Cells["Seller"].Text = "YOU";
						e.Row.Cells["Seller"].FormatStyle = new GridEXFormatStyle
						{
							FontBold = TriState.True
						};
						e.Row.Cells["Buyer"].Text = (string)e.Row.Cells["Target"].Value;
						if ((OTCStatus)e.Row.Cells["Status"].Value == OTCStatus.ACCEPTED)
						{
							e.Row.RowStyle = ColorHelper.RowStyleRed;
						}
						else
						{
							e.Row.RowStyle = ColorHelper.RowStyleGray;
						}
					}
				}
				else
				{
					if ((string)e.Row.Cells["Target"].Value == Game.State.Trader.TraderID)
					{
						if ((decimal)e.Row.Cells["Volume"].Value < 0m)
						{
							e.Row.Cells["Buyer"].Text = "YOU";
							e.Row.Cells["Buyer"].FormatStyle = new GridEXFormatStyle
							{
								FontBold = TriState.True
							};
							e.Row.Cells["Seller"].Text = (string)e.Row.Cells["TraderID"].Value;
							if ((OTCStatus)e.Row.Cells["Status"].Value == OTCStatus.ACCEPTED)
							{
								e.Row.RowStyle = ColorHelper.RowStyleGreen;
							}
							else
							{
								e.Row.RowStyle = ColorHelper.RowStyleGray;
							}
						}
						else
						{
							e.Row.Cells["Seller"].Text = "YOU";
							e.Row.Cells["Seller"].FormatStyle = new GridEXFormatStyle
							{
								FontBold = TriState.True
							};
							e.Row.Cells["Buyer"].Text = (string)e.Row.Cells["TraderID"].Value;
							if ((OTCStatus)e.Row.Cells["Status"].Value == OTCStatus.ACCEPTED)
							{
								e.Row.RowStyle = ColorHelper.RowStyleRed;
							}
							else
							{
								e.Row.RowStyle = ColorHelper.RowStyleGray;
							}
						}
					}
				}
				e.Row.Cells["Volume"].Text = System.Math.Abs((decimal)e.Row.Cells["Volume"].Value).ToString();
			};
			this.CompletedGrid.AddContextMenu(false);
			this.TraderDropDown.DataSource = ConnectedTradersManager.ConnectedTraders;
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
						this.SettlePeriod.Maximum = Game.State.Securities[key].Parameters.StopPeriod;
						this.SettlePeriod.Minimum = Game.State.Securities[key].Parameters.StartPeriod;
						this.VolumeUpDown.DecimalPlaces = Game.State.Securities[key].Parameters.VolumeDecimals;
					}
					else
					{
						this.PriceUpDown.DecimalPlaces = 2;
						this.PriceUpDown.Increment = 1m;
						this.PriceUpDown.Maximum = 79228162514264337593543950335m;
						this.PriceUpDown.Minimum = 0m;
						this.SettlePeriod.Maximum = Game.State.General.Periods;
						this.SettlePeriod.Minimum = 1m;
						this.VolumeUpDown.DecimalPlaces = 0;
					}
					this.SettleTick.Maximum = Game.State.General.TicksPerPeriod;
					this.SettleTick.Minimum = 1m;
				}
			}, delegate(System.Windows.Forms.ComboBox cb)
			{
				cb.DataSource = Game.State.ActiveOTCableSecuritiesAndAssets;
			});
			this.SetUIState();
			base.AddResetHandler(delegate
			{
				this.PendingGrid.DataSource = Game.State.OTCPendingOrderView;
				this.CompletedGrid.DataSource = Game.State.OTCCompletedOrderView;
			});
			base.Load += delegate(object sender, System.EventArgs e)
			{
				this.IsImmediately.Checked = true;
			};
		}
		private void SetUIState()
		{
			if (this.IsBuy)
			{
				this.BuySellButton.Text = "BUY";
				this.SubmitButton.Text = "Submit BUY";
				this.SubmitButton.BackColor = ColorHelper.TTSGreen;
				return;
			}
			this.BuySellButton.Text = "SELL";
			this.SubmitButton.Text = "Submit SELL";
			this.SubmitButton.BackColor = ColorHelper.TTSRed;
		}
		private void AutoAcceptCheckBox_CheckedChanged(object sender, System.EventArgs e)
		{
			if (this.AutoAcceptCheckBox.Checked)
			{
				this.AutoAcceptCheckBox.BackColor = ColorHelper.TTSRed;
				return;
			}
			this.AutoAcceptCheckBox.BackColor = System.Drawing.SystemColors.Control;
		}
		private void BuySellButton_Click(object sender, System.EventArgs e)
		{
			this.IsBuy = !this.IsBuy;
			this.SetUIState();
		}
		private void SubmitButton_Click(object sender, System.EventArgs e)
		{
			if (this.TraderDropDown.SelectedItem == null)
			{
				DialogHelper.ShowError("Invalid trader.", "Error");
				return;
			}
			if (this.TickerDropDown.SelectedItem == null)
			{
				DialogHelper.ShowError("Invalid ticker.", "Error");
				return;
			}
			if (this.VolumeUpDown.Value == 0m)
			{
				DialogHelper.ShowError("Invalid volume.", "Error");
				return;
			}
			string trader = this.TraderDropDown.SelectedItem.ToString();
			string ticker = this.TickerDropDown.SelectedItem.ToString();
			decimal price = this.PriceUpDown.Value;
			decimal volume = this.VolumeUpDown.Value * (this.IsBuy ? 1 : -1);
			int? settleperiod = this.IsImmediately.Checked ? null : new int?(System.Convert.ToInt32(this.SettlePeriod.Value));
			int? settletick = this.IsImmediately.Checked ? null : new int?(System.Convert.ToInt32(this.SettleTick.Value));
			try
			{
				ServiceManager.Execute(delegate(IClientService x)
				{
					x.AddOTCOrder(trader, ticker, volume, price, settleperiod, settletick);
				});
			}
			catch (FaultException ex)
			{
				DialogHelper.ShowError(ex.Message, "Error");
			}
		}
		private void IsImmediately_CheckedChanged(object sender, System.EventArgs e)
		{
			this.SettlePeriod.Enabled = (this.SettleTick.Enabled = !this.IsImmediately.Checked);
		}
	}
}
