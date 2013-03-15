using System;
using System.ComponentModel;
using System.Drawing;
using System.ServiceModel;
using System.Windows.Forms;
namespace TTS
{
	public class KillAll : TTSForm
	{
		private IContainer components;
		private System.Windows.Forms.Button button1;
		public static WindowType TTSWindowType = WindowType.KILL;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(KillAll));
			this.button1 = new System.Windows.Forms.Button();
			base.SuspendLayout();
			this.button1.BackColor = System.Drawing.Color.Firebrick;
			this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.button1.Font = new System.Drawing.Font("Tahoma", 14.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.button1.Location = new System.Drawing.Point(0, 0);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(284, 162);
			this.button1.TabIndex = 0;
			this.button1.Text = "Kill All Orders";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(284, 162);
			base.Controls.Add(this.button1);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "KillAll";
			this.Text = "Kill All Orders";
			base.ResumeLayout(false);
		}
		public KillAll(TTSFormState state) : base(state)
		{
			this.InitializeComponent();
		}
		private void button1_Click(object sender, System.EventArgs e)
		{
			try
			{
				ServiceManager.Execute(delegate(IClientService p)
				{
					p.CancelAllLimitOrders();
				});
			}
			catch (FaultException ex)
			{
				DialogHelper.ShowError(ex.Message, "Error");
			}
		}
	}
}
