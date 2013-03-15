using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.ServiceModel;
using System.Windows.Forms;
namespace TTS.Controls
{
	public class ChatPanel : System.Windows.Forms.UserControl
	{
		private IContainer components;
		private System.Windows.Forms.TableLayoutPanel MainTableLayout;
		private System.Windows.Forms.TextBox ChatTextBox;
		private System.Windows.Forms.Button ChatSendButton;
		private System.Windows.Forms.RichTextBox ChatMain;
		public string ChatKey;
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
			this.ChatTextBox = new System.Windows.Forms.TextBox();
			this.ChatSendButton = new System.Windows.Forms.Button();
			this.ChatMain = new System.Windows.Forms.RichTextBox();
			this.MainTableLayout.SuspendLayout();
			base.SuspendLayout();
			this.MainTableLayout.ColumnCount = 2;
			this.MainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100f));
			this.MainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.MainTableLayout.Controls.Add(this.ChatTextBox, 0, 1);
			this.MainTableLayout.Controls.Add(this.ChatSendButton, 1, 1);
			this.MainTableLayout.Controls.Add(this.ChatMain, 0, 0);
			this.MainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainTableLayout.Location = new System.Drawing.Point(0, 0);
			this.MainTableLayout.Name = "MainTableLayout";
			this.MainTableLayout.RowCount = 2;
			this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100f));
			this.MainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.MainTableLayout.Size = new System.Drawing.Size(640, 480);
			this.MainTableLayout.TabIndex = 2;
			this.ChatTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ChatTextBox.Location = new System.Drawing.Point(3, 454);
			this.ChatTextBox.Name = "ChatTextBox";
			this.ChatTextBox.Size = new System.Drawing.Size(553, 21);
			this.ChatTextBox.TabIndex = 0;
			this.ChatTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChatTextBox_KeyDown);
			this.ChatSendButton.Location = new System.Drawing.Point(562, 454);
			this.ChatSendButton.Name = "ChatSendButton";
			this.ChatSendButton.Size = new System.Drawing.Size(75, 23);
			this.ChatSendButton.TabIndex = 1;
			this.ChatSendButton.Text = "Send";
			this.ChatSendButton.UseVisualStyleBackColor = true;
			this.ChatSendButton.Click += new System.EventHandler(this.ChatSendButton_Click);
			this.ChatMain.BackColor = System.Drawing.Color.White;
			this.ChatMain.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.MainTableLayout.SetColumnSpan(this.ChatMain, 2);
			this.ChatMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ChatMain.Location = new System.Drawing.Point(3, 3);
			this.ChatMain.Name = "ChatMain";
			this.ChatMain.ReadOnly = true;
			this.ChatMain.Size = new System.Drawing.Size(634, 445);
			this.ChatMain.TabIndex = 2;
			this.ChatMain.Text = "";
			this.ChatMain.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.ChatMain_LinkClicked);
			this.ChatMain.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ChatMain_KeyPress);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.MainTableLayout);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.Name = "ChatPanel";
			base.Size = new System.Drawing.Size(640, 480);
			this.MainTableLayout.ResumeLayout(false);
			this.MainTableLayout.PerformLayout();
			base.ResumeLayout(false);
		}
		public ChatPanel() : this("")
		{
		}
		public ChatPanel(string key)
		{
			this.InitializeComponent();
			this.ChatKey = key;
			base.Load += delegate(object sender, System.EventArgs e)
			{
				this.ChatMain.Rtf = ChatManager.GetChatHistory(this.ChatKey);
				this.ChatTextBox.Focus();
				base.BeginInvoke(delegate
				{
					this.ChatMain.SelectionStart = this.ChatMain.TextLength;
					this.ChatMain.ScrollToCaret();
				});
			};
			ChatManager.ChatUpdatedDelegate updatedelegate = delegate(string chatkey, string header, string text, System.Drawing.Color c)
			{
				if (chatkey == this.ChatKey)
				{
					this.ChatMain.SelectionStart = this.ChatMain.TextLength;
					this.ChatMain.SelectionColor = c;
					this.ChatMain.SelectionFont = new System.Drawing.Font(this.ChatMain.SelectionFont, System.Drawing.FontStyle.Bold);
					this.ChatMain.AppendText("\n" + header);
					this.ChatMain.SelectionColor = System.Drawing.Color.Empty;
					this.ChatMain.SelectionFont = new System.Drawing.Font(this.ChatMain.SelectionFont, System.Drawing.FontStyle.Regular);
					this.ChatMain.AppendText(text);
					this.ChatMain.SelectionStart = this.ChatMain.TextLength;
					this.ChatMain.ScrollToCaret();
				}
			};
			ChatManager.ChatUpdated += updatedelegate;
			base.Disposed += delegate(object sender, System.EventArgs e)
			{
				ChatManager.ChatUpdated -= updatedelegate;
			};
		}
		private void ChatMain_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			this.ChatTextBox.Focus();
			if (!char.IsControl(e.KeyChar))
			{
				System.Windows.Forms.TextBox expr_1F = this.ChatTextBox;
				expr_1F.Text += e.KeyChar;
			}
			this.ChatTextBox.SelectionStart = this.ChatTextBox.Text.Length;
		}
		private void ChatTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Return)
			{
				this.ChatSendButton.PerformClick();
				e.SuppressKeyPress = true;
			}
		}
		private void ChatSendButton_Click(object sender, System.EventArgs e)
		{
			string message = this.ChatTextBox.Text;
			try
			{
				if (!string.IsNullOrWhiteSpace(message))
				{
					ServiceManager.Execute(delegate(IClientService x)
					{
						x.ChatMessage(this.ChatKey, message);
					});
					this.ChatTextBox.Clear();
					this.ChatTextBox.Focus();
				}
			}
			catch (FaultException)
			{
			}
		}
		private void ChatMain_LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)
		{
			try
			{
				Process.Start(e.LinkText);
			}
			catch
			{
			}
		}
	}
}
