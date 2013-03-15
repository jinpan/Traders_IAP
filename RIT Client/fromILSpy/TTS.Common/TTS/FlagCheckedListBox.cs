using System;
using System.ComponentModel;
using System.Windows.Forms;
namespace TTS
{
	public class FlagCheckedListBox : CheckedListBox
	{
		private Container components;
		private bool isUpdatingCheckStates;
		private System.Type enumType;
		private System.Enum enumValue;
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public System.Enum EnumValue
		{
			get
			{
				object obj = System.Enum.ToObject(this.enumType, this.GetCurrentValue());
				return (System.Enum)obj;
			}
			set
			{
				base.Items.Clear();
				this.enumValue = value;
				this.enumType = value.GetType();
				this.FillEnumMembers();
				this.ApplyEnumValue();
			}
		}
		public FlagCheckedListBox()
		{
			this.InitializeComponent();
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
			base.CheckOnClick = true;
		}
		public FlagCheckedListBoxItem Add(int v, string c)
		{
			FlagCheckedListBoxItem flagCheckedListBoxItem = new FlagCheckedListBoxItem(v, c);
			base.Items.Add(flagCheckedListBoxItem);
			return flagCheckedListBoxItem;
		}
		public FlagCheckedListBoxItem Add(FlagCheckedListBoxItem item)
		{
			base.Items.Add(item);
			return item;
		}
		protected override void OnItemCheck(ItemCheckEventArgs e)
		{
			base.OnItemCheck(e);
			if (this.isUpdatingCheckStates)
			{
				return;
			}
			FlagCheckedListBoxItem composite = base.Items[e.Index] as FlagCheckedListBoxItem;
			this.UpdateCheckedItems(composite, e.NewValue);
		}
		protected void UpdateCheckedItems(int value)
		{
			this.isUpdatingCheckStates = true;
			for (int i = 0; i < base.Items.Count; i++)
			{
				FlagCheckedListBoxItem flagCheckedListBoxItem = base.Items[i] as FlagCheckedListBoxItem;
				if (flagCheckedListBoxItem.value == 0)
				{
					base.SetItemChecked(i, value == 0);
				}
				else
				{
					if ((flagCheckedListBoxItem.value & value) == flagCheckedListBoxItem.value && flagCheckedListBoxItem.value != 0)
					{
						base.SetItemChecked(i, true);
					}
					else
					{
						base.SetItemChecked(i, false);
					}
				}
			}
			this.isUpdatingCheckStates = false;
		}
		protected void UpdateCheckedItems(FlagCheckedListBoxItem composite, CheckState cs)
		{
			if (composite.value == 0)
			{
				this.UpdateCheckedItems(0);
			}
			int num = 0;
			for (int i = 0; i < base.Items.Count; i++)
			{
				FlagCheckedListBoxItem flagCheckedListBoxItem = base.Items[i] as FlagCheckedListBoxItem;
				if (base.GetItemChecked(i))
				{
					num |= flagCheckedListBoxItem.value;
				}
			}
			if (cs == CheckState.Unchecked)
			{
				num &= ~composite.value;
			}
			else
			{
				num |= composite.value;
			}
			this.UpdateCheckedItems(num);
		}
		public int GetCurrentValue()
		{
			int num = 0;
			for (int i = 0; i < base.Items.Count; i++)
			{
				FlagCheckedListBoxItem flagCheckedListBoxItem = base.Items[i] as FlagCheckedListBoxItem;
				if (base.GetItemChecked(i))
				{
					num |= flagCheckedListBoxItem.value;
				}
			}
			return num;
		}
		private void FillEnumMembers()
		{
			string[] names = System.Enum.GetNames(this.enumType);
			for (int i = 0; i < names.Length; i++)
			{
				string text = names[i];
				object value = System.Enum.Parse(this.enumType, text);
				int v = (int)System.Convert.ChangeType(value, typeof(int));
				this.Add(v, text);
			}
		}
		private void ApplyEnumValue()
		{
			int value = (int)System.Convert.ChangeType(this.enumValue, typeof(int));
			this.UpdateCheckedItems(value);
		}
	}
}
