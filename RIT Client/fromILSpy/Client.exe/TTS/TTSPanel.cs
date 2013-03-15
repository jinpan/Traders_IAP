using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace TTS
{
	public class TTSPanel : System.Windows.Forms.UserControl
	{
		private IContainer components;
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
			base.SuspendLayout();
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.Name = "TTSPanel";
			base.ResumeLayout(false);
		}
		public TTSPanel()
		{
			this.InitializeComponent();
		}
		protected void AddResetHandler(Action action)
		{
			Game.Reset = (Action)System.Delegate.Combine(Game.Reset, action);
			base.Disposed += delegate(object sender, System.EventArgs e)
			{
				Game.Reset = (Action)System.Delegate.Remove(Game.Reset, action);
			};
		}
		protected void AddResettingHandler(Action action)
		{
			Game.Resetting = (Action)System.Delegate.Combine(Game.Resetting, action);
			base.Disposed += delegate(object sender, System.EventArgs e)
			{
				Game.Resetting = (Action)System.Delegate.Remove(Game.Resetting, action);
			};
		}
		protected void AddStatusUpdatingHandler(Action action)
		{
			GameManager.StatusUpdating += action;
			base.Disposed += delegate(object sender, System.EventArgs e)
			{
				GameManager.StatusUpdating -= action;
			};
		}
		protected void AddStatusUpdatedHandler(Action action)
		{
			GameManager.StatusUpdated += action;
			base.Disposed += delegate(object sender, System.EventArgs e)
			{
				GameManager.StatusUpdated -= action;
			};
		}
		protected void AddVariablesUpdatedHandler(Action action)
		{
			Game.VariablesUpdated = (Action)System.Delegate.Combine(Game.VariablesUpdated, action);
			base.Disposed += delegate(object sender, System.EventArgs e)
			{
				Game.Reset = (Action)System.Delegate.Remove(Game.Reset, action);
			};
		}
		protected void BindComboBox(System.Windows.Forms.ComboBox cb, System.EventHandler onselectedvaluechanged, System.Action<System.Windows.Forms.ComboBox> binddatasource)
		{
			Tuple<object, int> lastitem = Tuple.Create<object, int>(null, -1);
			cb.SelectedValueChanged += onselectedvaluechanged;
			binddatasource(cb);
			this.ResizeCombo(cb);
			this.AddStatusUpdatingHandler(delegate
			{
				lastitem = Tuple.Create<object, int>(cb.SelectedItem, cb.SelectedIndex);
				cb.SelectedValueChanged -= onselectedvaluechanged;
				cb.Enabled = false;
			});
			this.AddStatusUpdatedHandler(delegate
			{
				binddatasource(cb);
				this.ResizeCombo(cb);
				cb.SelectedIndex = -1;
				cb.SelectedValueChanged += onselectedvaluechanged;
				if (cb.Items.Count == 0)
				{
					onselectedvaluechanged(cb, new System.EventArgs());
				}
				else
				{
					if (lastitem.Item1 != null && cb.Items.Contains(lastitem.Item1))
					{
						cb.SelectedIndex = cb.Items.IndexOf(lastitem.Item1);
					}
					else
					{
						cb.SelectedIndex = System.Math.Min(cb.Items.Count - 1, lastitem.Item2);
					}
				}
				cb.Enabled = true;
			});
		}
		protected void ResizeCombo(System.Windows.Forms.ComboBox cb)
		{
			float num = 80f;
			foreach (string text in cb.Items)
			{
				num = System.Math.Max((float)(System.Windows.Forms.TextRenderer.MeasureText(text, cb.Font).Width + System.Windows.Forms.SystemInformation.VerticalScrollBarWidth), num);
			}
			cb.Width = System.Convert.ToInt32(num);
		}
	}
}
