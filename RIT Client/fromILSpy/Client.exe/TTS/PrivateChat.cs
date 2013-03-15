using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using TTS.Controls;
namespace TTS
{
	public class PrivateChat : TTSForm
	{
		public static WindowType TTSWindowType = WindowType.CHAT;
		public string ChatKey = "";
		private IContainer components;
		public PrivateChat(TTSFormState state) : base(state)
		{
			this.InitializeComponent();
			this.ChatKey = (string)state.State;
			this.Text += string.Format(" ({0})", this.ChatKey);
			base.Shown += delegate(object sender, System.EventArgs e)
			{
				base.Controls.Add(new ChatPanel(this.ChatKey)
				{
					Dock = System.Windows.Forms.DockStyle.Fill
				});
				base.Controls[0].Focus();
			};
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(PrivateChat));
			base.SuspendLayout();
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(384, 262);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "PrivateChat";
			this.Text = "Private Chat";
			base.ResumeLayout(false);
		}
	}
}
