using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TTS.Controls;
namespace TTS
{
	public class Chat : TTSForm
	{
		public static WindowType TTSWindowType = WindowType.CHAT;
		private IContainer components;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ListBox ChatList;
		private ChatPanel chatPanel1;
		public Chat(TTSFormState state) : base(state)
		{
			this.InitializeComponent();
			this.ChatList.DataSource = ConnectedTradersManager.ConnectedTraders;
			base.Activated += delegate(object sender, System.EventArgs e)
			{
				Game.State.UnreadChatCount = 0;
			};
		}
		private void ChatList_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
		{
			if (e.Index >= 0)
			{
				string text = ((System.Windows.Forms.ListBox)sender).Items[e.Index].ToString();
				System.Drawing.Color item = ConnectedTradersManager.TraderColor[text].Item1;
				using (System.Drawing.Brush brush = new System.Drawing.SolidBrush((e.BackColor == this.ChatList.BackColor) ? this.ChatList.BackColor : System.Drawing.SystemColors.ControlLight))
				{
					e.Graphics.FillRectangle(brush, e.Bounds);
				}
				using (System.Drawing.Brush brush2 = new System.Drawing.SolidBrush(item))
				{
					e.Graphics.DrawString(text, e.Font, brush2, e.Bounds);
				}
				e.DrawFocusRectangle();
			}
		}
		private void ChatList_MouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			int num = this.ChatList.IndexFromPoint(e.Location);
			if (num != -1)
			{
				TTSFormManager.Instance.FindAddChatWindow(((System.Windows.Forms.ListBox)sender).Items[num].ToString());
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Chat));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.ChatList = new System.Windows.Forms.ListBox();
			this.chatPanel1 = new ChatPanel();
			((ISupportInitialize)this.splitContainer1).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			base.SuspendLayout();
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Panel1.Controls.Add(this.ChatList);
			this.splitContainer1.Panel2.Controls.Add(this.chatPanel1);
			this.splitContainer1.Size = new System.Drawing.Size(384, 262);
			this.splitContainer1.SplitterDistance = 100;
			this.splitContainer1.TabIndex = 0;
			this.ChatList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ChatList.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.ChatList.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.ChatList.FormattingEnabled = true;
			this.ChatList.Location = new System.Drawing.Point(0, 0);
			this.ChatList.Name = "ChatList";
			this.ChatList.Size = new System.Drawing.Size(100, 262);
			this.ChatList.TabIndex = 1;
			this.ChatList.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ChatList_DrawItem);
			this.ChatList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ChatList_MouseDoubleClick);
			this.chatPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chatPanel1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.chatPanel1.Location = new System.Drawing.Point(0, 0);
			this.chatPanel1.Name = "chatPanel1";
			this.chatPanel1.Size = new System.Drawing.Size(280, 262);
			this.chatPanel1.TabIndex = 0;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(384, 262);
			base.Controls.Add(this.splitContainer1);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "Chat";
			this.Text = "Chat";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((ISupportInitialize)this.splitContainer1).EndInit();
			this.splitContainer1.ResumeLayout(false);
			base.ResumeLayout(false);
		}
	}
}
