using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
namespace TTS
{
	public class FlagEnumUIEditor : System.Drawing.Design.UITypeEditor
	{
		private FlagCheckedListBox flagEnumCB;
		public FlagEnumUIEditor()
		{
			this.flagEnumCB = new FlagCheckedListBox();
			this.flagEnumCB.BorderStyle = BorderStyle.None;
		}
		public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
		{
			if (context != null && context.Instance != null && provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService != null)
				{
					System.Enum enumValue = (System.Enum)System.Convert.ChangeType(value, context.PropertyDescriptor.PropertyType);
					this.flagEnumCB.EnumValue = enumValue;
					windowsFormsEditorService.DropDownControl(this.flagEnumCB);
					return this.flagEnumCB.EnumValue;
				}
			}
			return null;
		}
		public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return System.Drawing.Design.UITypeEditorEditStyle.DropDown;
		}
	}
}
