using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace TTS
{
	public class BuySellEntry : TTSForm
	{
		public static WindowType TTSWindowType = WindowType.BUYSELL_ENTRY;
		private IContainer components;
		private System.Windows.Forms.TableLayoutPanel OrderEntryPanelTableLayout;
		public BuySellEntry(TTSFormState state) : base(state)
		{
			this.InitializeComponent();
			OrderEntryPanel orderEntryPanel = new OrderEntryPanel(true);
			OrderEntryPanel expr_17 = orderEntryPanel;
			expr_17.AddOrderEntryPanelClick = (Action)System.Delegate.Combine(expr_17.AddOrderEntryPanelClick, delegate
			{
				this.AddOrderEntryPanel();
			});
			this.OrderEntryPanelTableLayout.Controls.Add(orderEntryPanel);
			if (state != null && state.State != null)
			{
				try
				{
					Tuple<bool, bool, decimal, string, decimal>[] array = (Tuple<bool, bool, decimal, string, decimal>[])state.State;
					for (int i = 0; i < array.Length; i++)
					{
						if (i > 0)
						{
							orderEntryPanel = this.AddOrderEntryPanel();
						}
						orderEntryPanel.SetState(array[i]);
					}
				}
				catch
				{
				}
			}
		}
		private OrderEntryPanel AddOrderEntryPanel()
		{
			OrderEntryPanel orderEntryPanel = new OrderEntryPanel(false);
			OrderEntryPanel expr_08 = orderEntryPanel;
			expr_08.RemoveOrderEntryPanelClick = (System.Action<OrderEntryPanel>)System.Delegate.Combine(expr_08.RemoveOrderEntryPanelClick, delegate(OrderEntryPanel sender)
			{
				int row = this.OrderEntryPanelTableLayout.GetRow(sender);
				this.OrderEntryPanelTableLayout.Controls.Remove(sender);
				for (int i = row; i < this.OrderEntryPanelTableLayout.Controls.Count; i++)
				{
					this.OrderEntryPanelTableLayout.SetRow(this.OrderEntryPanelTableLayout.Controls[i], i);
				}
				this.OrderEntryPanelTableLayout.RowCount = this.OrderEntryPanelTableLayout.Controls.Count;
			});
			this.OrderEntryPanelTableLayout.RowCount++;
			this.OrderEntryPanelTableLayout.Controls.Add(orderEntryPanel, 0, this.OrderEntryPanelTableLayout.RowCount - 1);
			return orderEntryPanel;
		}
		protected override object GetState()
		{
			System.Collections.Generic.List<Tuple<bool, bool, decimal, string, decimal>> list = new System.Collections.Generic.List<Tuple<bool, bool, decimal, string, decimal>>();
			foreach (OrderEntryPanel orderEntryPanel in this.OrderEntryPanelTableLayout.Controls)
			{
				list.Add(orderEntryPanel.GetState());
			}
			return list.ToArray();
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(BuySellEntry));
			this.OrderEntryPanelTableLayout = new System.Windows.Forms.TableLayoutPanel();
			base.SuspendLayout();
			this.OrderEntryPanelTableLayout.AutoSize = true;
			this.OrderEntryPanelTableLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.OrderEntryPanelTableLayout.ColumnCount = 1;
			this.OrderEntryPanelTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.OrderEntryPanelTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.OrderEntryPanelTableLayout.Location = new System.Drawing.Point(0, 0);
			this.OrderEntryPanelTableLayout.Name = "OrderEntryPanelTableLayout";
			this.OrderEntryPanelTableLayout.RowCount = 1;
			this.OrderEntryPanelTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.OrderEntryPanelTableLayout.Size = new System.Drawing.Size(464, 41);
			this.OrderEntryPanelTableLayout.TabIndex = 0;
			this.AutoSize = true;
			base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			base.ClientSize = new System.Drawing.Size(464, 41);
			base.Controls.Add(this.OrderEntryPanelTableLayout);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "BuySellEntry";
			this.Text = "Buy/Sell Entry";
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
