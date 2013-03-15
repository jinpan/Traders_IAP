using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace TTS
{
	public class TTSForm : System.Windows.Forms.Form
	{
		private IContainer components;
		public TTSForm()
		{
			this.InitializeComponent();
		}
		public TTSForm(TTSFormState state) : this()
		{
			TTSForm <>4__this = this;
			if (state != null)
			{
				base.Load += delegate(object sender, System.EventArgs e)
				{
					<>4__this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
					<>4__this.Location = new System.Drawing.Point(state.X ?? <>4__this.Location.X, state.Y ?? <>4__this.Location.Y);
					<>4__this.Width = (state.Width ?? <>4__this.Width);
					<>4__this.Height = (state.Height ?? <>4__this.Height);
					<>4__this.SetState(state.State);
				};
				return;
			}
			base.Load += delegate(object sender, System.EventArgs e)
			{
				base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			};
		}
		protected void AddResetHandler(Action action)
		{
			Game.Reset = (Action)System.Delegate.Combine(Game.Reset, action);
			base.FormClosed += delegate(object sender, System.Windows.Forms.FormClosedEventArgs e)
			{
				Game.Reset = (Action)System.Delegate.Remove(Game.Reset, action);
			};
		}
		protected void AddResettingHandler(Action action)
		{
			Game.Resetting = (Action)System.Delegate.Combine(Game.Resetting, action);
			base.FormClosed += delegate(object sender, System.Windows.Forms.FormClosedEventArgs e)
			{
				Game.Resetting = (Action)System.Delegate.Remove(Game.Resetting, action);
			};
		}
		protected void AddStatusUpdatingHandler(Action action)
		{
			GameManager.StatusUpdating += action;
			base.FormClosed += delegate(object sender, System.Windows.Forms.FormClosedEventArgs e)
			{
				GameManager.StatusUpdating -= action;
			};
		}
		protected void AddStatusUpdatedHandler(Action action)
		{
			GameManager.StatusUpdated += action;
			base.FormClosed += delegate(object sender, System.Windows.Forms.FormClosedEventArgs e)
			{
				GameManager.StatusUpdated -= action;
			};
		}
		protected void AddVariablesUpdatedHandler(Action action)
		{
			Game.VariablesUpdated = (Action)System.Delegate.Combine(Game.VariablesUpdated, action);
			base.FormClosed += delegate(object sender, System.Windows.Forms.FormClosedEventArgs e)
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
		protected virtual object GetState()
		{
			return null;
		}
		protected virtual void SetState(object state)
		{
		}
		public TTSFormState GetFormState()
		{
			return new TTSFormState
			{
				WindowType = base.GetType().ToString(),
				Width = new int?(base.Width),
				Height = new int?(base.Height),
				X = new int?(base.Location.X),
				Y = new int?(base.Location.Y),
				State = this.GetState()
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
			base.SuspendLayout();
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			base.ClientSize = new System.Drawing.Size(584, 167);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "TTSForm";
			base.ResumeLayout(false);
		}
	}
}
