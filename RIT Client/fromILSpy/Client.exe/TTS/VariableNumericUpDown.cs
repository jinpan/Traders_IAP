using System;
using System.Windows.Forms;
namespace TTS
{
	public class VariableNumericUpDown : System.Windows.Forms.NumericUpDown
	{
		public decimal IncrementBig
		{
			get;
			set;
		}
		public decimal IncrementSmall
		{
			get;
			set;
		}
		public decimal ValueLimit
		{
			get;
			set;
		}
		public decimal IncrementLimit
		{
			get;
			set;
		}
		public decimal IncrementLimitBig
		{
			get;
			set;
		}
		public decimal IncrementLimitSmall
		{
			get;
			set;
		}
		public override void DownButton()
		{
			decimal increment = base.Increment;
			if (base.Value < this.ValueLimit)
			{
				base.Increment = this.IncrementLimit;
				if (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control)
				{
					base.Increment = this.IncrementLimitBig;
				}
				else
				{
					if (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Shift)
					{
						base.Increment = this.IncrementLimitSmall;
					}
				}
			}
			else
			{
				if (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control)
				{
					base.Increment = this.IncrementBig;
				}
				else
				{
					if (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Shift)
					{
						base.Increment = this.IncrementSmall;
					}
				}
			}
			base.DownButton();
			base.Increment = increment;
		}
		public override void UpButton()
		{
			decimal increment = base.Increment;
			if (base.Value < this.ValueLimit)
			{
				base.Increment = this.IncrementLimit;
				if (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control)
				{
					base.Increment = this.IncrementLimitBig;
				}
				else
				{
					if (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Shift)
					{
						base.Increment = this.IncrementLimitSmall;
					}
				}
			}
			else
			{
				if (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control)
				{
					base.Increment = this.IncrementBig;
				}
				else
				{
					if (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Shift)
					{
						base.Increment = this.IncrementSmall;
					}
				}
			}
			base.UpButton();
			base.Increment = increment;
		}
	}
}
