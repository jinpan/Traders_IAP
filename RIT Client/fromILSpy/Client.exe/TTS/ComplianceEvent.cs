using System;
using System.ComponentModel;
using System.Drawing;
using System.ServiceModel;
using System.Windows.Forms;
namespace TTS
{
	public class ComplianceEvent : System.Windows.Forms.Form
	{
		private int ID;
		private MessageType MessageType;
		private IContainer components;
		private System.Windows.Forms.TableLayoutPanel MainTableLayout;
		private System.Windows.Forms.Label MessageBody;
		private System.Windows.Forms.Label ReasonLabel;
		private System.Windows.Forms.TextBox ReasonTextBox;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button OKButton;
		private System.Windows.Forms.Button DeclineButton;
		public ComplianceEvent(int id, MessageType type, string message)
		{
			this.InitializeComponent();
			this.MessageBody.Text = message;
			this.ID = id;
			this.MessageType = type;
			if (type != MessageType.DETAILED_RESPONSE)
			{
				this.ReasonLabel.Visible = (this.ReasonTextBox.Visible = (this.ReasonLabel.Enabled = (this.ReasonTextBox.Enabled = false)));
			}
			if (type == MessageType.INFO || type == MessageType.DETAILS)
			{
				this.DeclineButton.Visible = (this.DeclineButton.Enabled = false);
				this.OKButton.Text = "OK";
			}
			Action onreset = new Action(base.Close);
			base.FormClosing += delegate(object sender, System.Windows.Forms.FormClosingEventArgs e)
			{
				Game.Resetting = (Action)System.Delegate.Remove(Game.Resetting, onreset);
			};
			base.FormClosing += delegate(object sender, System.Windows.Forms.FormClosingEventArgs e)
			{
				ServiceManager.Disconnected -= onreset;
			};
			ServiceManager.Disconnected += onreset;
			Game.Resetting = (Action)System.Delegate.Combine(Game.Resetting, onreset);
		}
		private void DeclineButton_Click(object sender, System.EventArgs e)
		{
			try
			{
				string reason = this.ReasonTextBox.Text.Trim();
				ServiceManager.Execute(delegate(IClientService p)
				{
					p.ComplianceResponse(this.ID, false, reason);
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
			try
			{
				if (this.MessageType != MessageType.DETAILS)
				{
					string reason = this.ReasonTextBox.Text.Trim();
					ServiceManager.Execute(delegate(IClientService p)
					{
						p.ComplianceResponse(this.ID, true, reason);
					});
				}
				base.Close();
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
			this.MainTableLayout = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.DeclineButton = new System.Windows.Forms.Button();
			this.OKButton = new System.Windows.Forms.Button();
			this.MessageBody = new System.Windows.Forms.Label();
			this.ReasonLabel = new System.Windows.Forms.Label();
			this.ReasonTextBox = new System.Windows.Forms.TextBox();
			this.MainTableLayout.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.MainTableLayout.AutoSize = true;
			this.MainTableLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.MainTableLayout.ColumnCount = 2;
			this.MainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.MainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.MainTableLayout.Controls.Add(this.tableLayoutPanel1, 0, 3);
			this.MainTableLayout.Controls.Add(this.MessageBody, 0, 0);
			this.MainTableLayout.Controls.Add(this.ReasonLabel, 0, 2);
			this.MainTableLayout.Controls.Add(this.ReasonTextBox, 1, 2);
			this.MainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainTableLayout.Location = new System.Drawing.Point(10, 10);
			this.MainTableLayout.Name = "MainTableLayout";
			this.MainTableLayout.Padding = new System.Windows.Forms.Padding(3);
			this.MainTableLayout.RowCount = 4;
			this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 5f));
			this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.MainTableLayout.Size = new System.Drawing.Size(331, 144);
			this.MainTableLayout.TabIndex = 0;
			this.tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.MainTableLayout.SetColumnSpan(this.tableLayoutPanel1, 2);
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.DeclineButton, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.OKButton, 0, 0);
			this.tableLayoutPanel1.Location = new System.Drawing.Point(74, 113);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(188, 29);
			this.tableLayoutPanel1.TabIndex = 5;
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
			this.OKButton.Text = "Accept";
			this.OKButton.UseVisualStyleBackColor = true;
			this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
			this.MessageBody.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.MessageBody.AutoSize = true;
			this.MainTableLayout.SetColumnSpan(this.MessageBody, 2);
			this.MessageBody.Location = new System.Drawing.Point(6, 3);
			this.MessageBody.MaximumSize = new System.Drawing.Size(320, 0);
			this.MessageBody.MinimumSize = new System.Drawing.Size(320, 0);
			this.MessageBody.Name = "MessageBody";
			this.MessageBody.Padding = new System.Windows.Forms.Padding(0, 0, 0, 3);
			this.MessageBody.Size = new System.Drawing.Size(320, 16);
			this.MessageBody.TabIndex = 0;
			this.MessageBody.Text = "???";
			this.ReasonLabel.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right);
			this.ReasonLabel.AutoSize = true;
			this.ReasonLabel.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.ReasonLabel.Location = new System.Drawing.Point(6, 24);
			this.ReasonLabel.MaximumSize = new System.Drawing.Size(250, 0);
			this.ReasonLabel.Name = "ReasonLabel";
			this.ReasonLabel.Size = new System.Drawing.Size(52, 13);
			this.ReasonLabel.TabIndex = 2;
			this.ReasonLabel.Text = "Reason:";
			this.ReasonTextBox.Location = new System.Drawing.Point(64, 27);
			this.ReasonTextBox.Multiline = true;
			this.ReasonTextBox.Name = "ReasonTextBox";
			this.ReasonTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.ReasonTextBox.Size = new System.Drawing.Size(266, 80);
			this.ReasonTextBox.TabIndex = 4;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			base.ClientSize = new System.Drawing.Size(351, 164);
			base.ControlBox = false;
			base.Controls.Add(this.MainTableLayout);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "ComplianceEvent";
			base.Padding = new System.Windows.Forms.Padding(10);
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Server Message";
			this.MainTableLayout.ResumeLayout(false);
			this.MainTableLayout.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
