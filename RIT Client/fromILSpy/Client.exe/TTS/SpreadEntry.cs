using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace TTS
{
	public class SpreadEntry : TTSForm
	{
		private IContainer components;
		private System.Windows.Forms.TableLayoutPanel SpreadEntryPanelTableLayout;
		public static WindowType TTSWindowType = WindowType.SPREAD_ENTRY;
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(SpreadEntry));
			this.SpreadEntryPanelTableLayout = new System.Windows.Forms.TableLayoutPanel();
			base.SuspendLayout();
			this.SpreadEntryPanelTableLayout.AutoSize = true;
			this.SpreadEntryPanelTableLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.SpreadEntryPanelTableLayout.ColumnCount = 1;
			this.SpreadEntryPanelTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.SpreadEntryPanelTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SpreadEntryPanelTableLayout.Location = new System.Drawing.Point(0, 0);
			this.SpreadEntryPanelTableLayout.Name = "SpreadEntryPanelTableLayout";
			this.SpreadEntryPanelTableLayout.RowCount = 1;
			this.SpreadEntryPanelTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.SpreadEntryPanelTableLayout.Size = new System.Drawing.Size(518, 41);
			this.SpreadEntryPanelTableLayout.TabIndex = 0;
			this.AutoSize = true;
			base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			base.ClientSize = new System.Drawing.Size(518, 41);
			base.Controls.Add(this.SpreadEntryPanelTableLayout);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "SpreadEntry";
			this.Text = "Spread Entry";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		public SpreadEntry(TTSFormState state) : base(state)
		{
			this.InitializeComponent();
			SpreadEntryPanel spreadEntryPanel = new SpreadEntryPanel(true);
			SpreadEntryPanel expr_17 = spreadEntryPanel;
			expr_17.AddSpreadEntryPanelClick = (Action)System.Delegate.Combine(expr_17.AddSpreadEntryPanelClick, delegate
			{
				this.AddSpreadEntryPanel();
			});
			this.SpreadEntryPanelTableLayout.Controls.Add(spreadEntryPanel);
			if (state != null && state.State != null)
			{
				try
				{
					Tuple<string, decimal, string, decimal>[] array = (Tuple<string, decimal, string, decimal>[])state.State;
					for (int i = 0; i < array.Length; i++)
					{
						if (i > 0)
						{
							spreadEntryPanel = this.AddSpreadEntryPanel();
						}
						spreadEntryPanel.SetState(array[i]);
					}
				}
				catch
				{
				}
			}
		}
		protected override object GetState()
		{
			System.Collections.Generic.List<Tuple<string, decimal, string, decimal>> list = new System.Collections.Generic.List<Tuple<string, decimal, string, decimal>>();
			foreach (SpreadEntryPanel spreadEntryPanel in this.SpreadEntryPanelTableLayout.Controls)
			{
				list.Add(spreadEntryPanel.GetState());
			}
			return list.ToArray();
		}
		private SpreadEntryPanel AddSpreadEntryPanel()
		{
			SpreadEntryPanel spreadEntryPanel = new SpreadEntryPanel(false);
			SpreadEntryPanel expr_08 = spreadEntryPanel;
			expr_08.RemoveSpreadEntryPanelClick = (System.Action<SpreadEntryPanel>)System.Delegate.Combine(expr_08.RemoveSpreadEntryPanelClick, delegate(SpreadEntryPanel sender)
			{
				int row = this.SpreadEntryPanelTableLayout.GetRow(sender);
				this.SpreadEntryPanelTableLayout.Controls.Remove(sender);
				for (int i = row; i < this.SpreadEntryPanelTableLayout.Controls.Count; i++)
				{
					this.SpreadEntryPanelTableLayout.SetRow(this.SpreadEntryPanelTableLayout.Controls[i], i);
				}
				this.SpreadEntryPanelTableLayout.RowCount = this.SpreadEntryPanelTableLayout.Controls.Count;
			});
			this.SpreadEntryPanelTableLayout.RowCount++;
			this.SpreadEntryPanelTableLayout.Controls.Add(spreadEntryPanel, 0, this.SpreadEntryPanelTableLayout.RowCount - 1);
			return spreadEntryPanel;
		}
	}
}
