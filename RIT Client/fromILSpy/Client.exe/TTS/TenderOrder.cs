using System;
using System.ComponentModel;
using System.Drawing;
using System.ServiceModel;
using System.Windows.Forms;
namespace TTS
{
	public class TenderOrder : System.Windows.Forms.Form
	{
		private IContainer components;
		private System.Windows.Forms.TableLayoutPanel MainTableLayout;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown BidNumericUpDown;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button DeclineButton;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Timer TenderTimer;
		private int ID;
		private int StopTick;
		private decimal InitialBid;
		private bool IsBidFixed;
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
			this.MainTableLayout = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.BidNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label2 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.DeclineButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.TenderTimer = new System.Windows.Forms.Timer(this.components);
			this.MainTableLayout.SuspendLayout();
			((ISupportInitialize)this.BidNumericUpDown).BeginInit();
			this.tableLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.MainTableLayout.AutoSize = true;
			this.MainTableLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.MainTableLayout.ColumnCount = 2;
			this.MainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.MainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.MainTableLayout.Controls.Add(this.label1, 0, 0);
			this.MainTableLayout.Controls.Add(this.BidNumericUpDown, 1, 1);
			this.MainTableLayout.Controls.Add(this.label2, 0, 1);
			this.MainTableLayout.Controls.Add(this.tableLayoutPanel1, 0, 2);
			this.MainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainTableLayout.Location = new System.Drawing.Point(10, 10);
			this.MainTableLayout.Name = "MainTableLayout";
			this.MainTableLayout.Padding = new System.Windows.Forms.Padding(3);
			this.MainTableLayout.RowCount = 3;
			this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.MainTableLayout.Size = new System.Drawing.Size(282, 111);
			this.MainTableLayout.TabIndex = 0;
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.MainTableLayout.SetColumnSpan(this.label1, 2);
			this.label1.Location = new System.Drawing.Point(6, 3);
			this.label1.MaximumSize = new System.Drawing.Size(250, 0);
			this.label1.Name = "label1";
			this.label1.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.label1.Size = new System.Drawing.Size(22, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "???";
			this.BidNumericUpDown.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.BidNumericUpDown.DecimalPlaces = 2;
			this.BidNumericUpDown.Increment = new decimal(new int[]
			{
				1,
				0,
				0,
				131072
			});
			this.BidNumericUpDown.Location = new System.Drawing.Point(46, 22);
			System.Windows.Forms.NumericUpDown arg_308_0 = this.BidNumericUpDown;
			int[] array = new int[4];
			array[0] = 1215752192;
			array[1] = 23;
			arg_308_0.Maximum = new decimal(array);
			this.BidNumericUpDown.Name = "BidNumericUpDown";
			this.BidNumericUpDown.Size = new System.Drawing.Size(219, 21);
			this.BidNumericUpDown.TabIndex = 0;
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 26);
			this.label2.MaximumSize = new System.Drawing.Size(250, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(34, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Price:";
			this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.MainTableLayout.SetColumnSpan(this.tableLayoutPanel1, 2);
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50f));
			this.tableLayoutPanel1.Controls.Add(this.DeclineButton, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.OKButton, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(47, 62);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50f));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(188, 29);
			this.tableLayoutPanel1.TabIndex = 3;
			this.DeclineButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.DeclineButton.Location = new System.Drawing.Point(97, 3);
			this.DeclineButton.Name = "DeclineButton";
			this.DeclineButton.Size = new System.Drawing.Size(88, 23);
			this.DeclineButton.TabIndex = 1;
			this.DeclineButton.Text = "Decline";
			this.DeclineButton.UseVisualStyleBackColor = true;
			this.DeclineButton.Click += new System.EventHandler(this.DeclineButton_Click);
			this.OKButton.Location = new System.Drawing.Point(3, 3);
			this.OKButton.Name = "OKButton";
			this.OKButton.Size = new System.Drawing.Size(88, 23);
			this.OKButton.TabIndex = 0;
			this.OKButton.Text = "Submit Tender";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			base.CancelButton = this.DeclineButton;
			base.ClientSize = new System.Drawing.Size(302, 131);
			base.ControlBox = false;
			base.Controls.Add(this.MainTableLayout);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TenderOrder";
			base.Padding = new System.Windows.Forms.Padding(10);
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Tender Order";
			this.MainTableLayout.ResumeLayout(false);
			this.MainTableLayout.PerformLayout();
			((ISupportInitialize)this.BidNumericUpDown).EndInit();
			this.tableLayoutPanel1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		public TenderOrder(int id, int stoptick, string caption, decimal initialbid, int decimalplaces, bool isbidfixed, decimal volume)
		{
			this.InitializeComponent();
			this.ID = id;
			this.StopTick = stoptick;
			this.InitialBid = initialbid;
			this.IsBidFixed = isbidfixed;
			this.label1.Text = caption;
			this.BidNumericUpDown.DecimalPlaces = decimalplaces;
			this.BidNumericUpDown.Increment = System.Convert.ToDecimal(System.Math.Pow(10.0, (double)(-1 * decimalplaces)));
			this.BidNumericUpDown.Value = initialbid;
			if (isbidfixed)
			{
				this.label2.Visible = (this.BidNumericUpDown.Visible = false);
				this.OKButton.BackColor = ((volume > 0m) ? ColorHelper.TTSGreen : ColorHelper.TTSRed);
				this.OKButton.Text = ((volume > 0m) ? "BUY" : "SELL");
				base.Shown += delegate(object sender, System.EventArgs e)
				{
					this.label1.Focus();
				};
			}
			else
			{
				this.BidNumericUpDown.Select(0, this.BidNumericUpDown.Text.Length);
				this.OKButton.BackColor = ((volume > 0m) ? ColorHelper.TTSGreen : ColorHelper.TTSRed);
				this.OKButton.Text = ((volume > 0m) ? "Submit Bid" : "Submit Offer");
			}
			this.DeclineButton.Text = string.Format("Decline ({0})", this.StopTick - Game.State.Current.Tick);
			this.TenderTimer.Tick += delegate(object sender, System.EventArgs e)
			{
				this.DeclineButton.Text = string.Format("Decline ({0})", System.Math.Max(this.StopTick - Game.State.Current.Tick, 0));
				if (Game.State.Current.Tick >= this.StopTick)
				{
					this.TenderTimer.Stop();
					try
					{
						ServiceManager.Execute(delegate(IClientService p)
						{
							p.BidOnTender(this.ID, false, 0m);
						});
					}
					catch (FaultException)
					{
					}
					finally
					{
						base.Close();
					}
				}
			};
			this.TenderTimer.Start();
		}
		private void DeclineButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				ServiceManager.Execute(delegate(IClientService p)
				{
					p.BidOnTender(this.ID, false, 0m);
				});
				base.Close();
			}
			catch (FaultException ex)
			{
				DialogHelper.ShowError(ex.Message, "Error");
			}
		}
		private void OKButton_Click(object sender, System.EventArgs e)
		{
			decimal bid = this.BidNumericUpDown.Value;
			if (!this.IsBidFixed && (bid > this.InitialBid * 1.5m || bid < this.InitialBid / 2m))
			{
				DialogHelper.ShowError("Price is outside allowable bounds.", "Error");
				return;
			}
			try
			{
				ServiceManager.Execute(delegate(IClientService p)
				{
					p.BidOnTender(this.ID, true, bid);
				});
				base.Close();
			}
			catch (FaultException ex)
			{
				DialogHelper.ShowError(ex.Message, "Error");
			}
		}
	}
}
