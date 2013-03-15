using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
namespace TTS
{
	public class PromptDialog : Form
	{
		public object[] Values;
		private Control[] UserControls;
		private IContainer components;
		private TableLayoutPanel MainTableLayout;
		public PromptDialog(string title, string caption, Control[] controls, string[] labels)
		{
			this.InitializeComponent();
			this.Text = title;
			this.UserControls = controls;
			this.MainTableLayout.ColumnCount = 2;
			this.MainTableLayout.RowCount = controls.Length + 1;
			int num = 0;
			if (!string.IsNullOrEmpty(caption))
			{
				this.MainTableLayout.RowCount++;
				Label control = new Label
				{
					Text = caption,
					Anchor = AnchorStyles.Left,
					AutoSize = true,
					Padding = new Padding(0, 0, 0, 3),
					MaximumSize = new System.Drawing.Size(250, 0)
				};
				this.MainTableLayout.Controls.Add(control, 0, 0);
				this.MainTableLayout.SetColumnSpan(control, 2);
				num = 1;
			}
			for (int i = 0; i < controls.Length; i++)
			{
				if (!string.IsNullOrEmpty(labels[i]))
				{
					this.MainTableLayout.Controls.Add(new Label
					{
						Text = labels[i] + ": ",
						Anchor = AnchorStyles.Right,
						AutoSize = true
					}, 0, i + num);
				}
				if (controls[i] != null)
				{
					this.MainTableLayout.Controls.Add(controls[i], 1, i + num);
					controls[i].Width = 200;
				}
			}
			TableLayoutPanel tableLayoutPanel = new TableLayoutPanel
			{
				RowCount = 1,
				ColumnCount = 2,
				AutoSize = true,
				AutoSizeMode = AutoSizeMode.GrowAndShrink,
				Anchor = AnchorStyles.None
			};
			Button button;
			tableLayoutPanel.Controls.Add(button = new Button
			{
				Text = "OK",
				DialogResult = DialogResult.OK,
				Anchor = AnchorStyles.Right
			}, 0, 0);
			Button cancelButton;
			tableLayoutPanel.Controls.Add(cancelButton = new Button
			{
				Text = "Cancel",
				DialogResult = DialogResult.Cancel,
				Anchor = AnchorStyles.Left
			}, 1, 0);
			base.AcceptButton = button;
			base.CancelButton = cancelButton;
			this.MainTableLayout.Controls.Add(tableLayoutPanel, 0, this.MainTableLayout.RowCount);
			this.MainTableLayout.SetColumnSpan(tableLayoutPanel, 2);
			button.Click += delegate(object sender, System.EventArgs e)
			{
				this.Values = this.UserControls.Select(new Func<Control, object>(this.GetControlValue)).ToArray<object>();
			};
			base.Load += delegate(object sender, System.EventArgs e)
			{
				if (this.UserControls.Length > 0)
				{
					this.UserControls[0].Focus();
				}
			};
		}
		public T GetValue<T>(int i)
		{
			return (T)((object)this.Values[i]);
		}
		private object GetControlValue(Control control)
		{
			if (control is NumericUpDown)
			{
				return ((NumericUpDown)control).Value;
			}
			if (control is TextBox)
			{
				return ((TextBox)control).Text;
			}
			if (control is ComboBox)
			{
				return ((ComboBox)control).SelectedItem;
			}
			if (control is CheckBox)
			{
				return ((CheckBox)control).Checked;
			}
			return null;
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
			this.MainTableLayout = new TableLayoutPanel();
			base.SuspendLayout();
			this.MainTableLayout.AutoSize = true;
			this.MainTableLayout.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.MainTableLayout.ColumnCount = 1;
			this.MainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
			this.MainTableLayout.Dock = DockStyle.Fill;
			this.MainTableLayout.Location = new System.Drawing.Point(10, 10);
			this.MainTableLayout.Name = "MainTableLayout";
			this.MainTableLayout.Padding = new Padding(3);
			this.MainTableLayout.RowCount = 1;
			this.MainTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
			this.MainTableLayout.Size = new System.Drawing.Size(410, 239);
			this.MainTableLayout.TabIndex = 0;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.AutoSize = true;
			base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			base.ClientSize = new System.Drawing.Size(430, 259);
			base.Controls.Add(this.MainTableLayout);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "QueryDialog";
			base.Padding = new Padding(10);
			base.StartPosition = FormStartPosition.CenterParent;
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
