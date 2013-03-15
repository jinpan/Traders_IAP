using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
namespace TTS
{
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.StatusStrip)]
	public class BindableToolStripDropDownButton : ToolStripDropDownButton, IBindableComponent, IComponent, System.IDisposable
	{
		private BindingContext _context;
		private ControlBindingsCollection _bindings;
		public BindingContext BindingContext
		{
			get
			{
				if (this._context == null)
				{
					this._context = new BindingContext();
				}
				return this._context;
			}
			set
			{
				this._context = value;
			}
		}
		public ControlBindingsCollection DataBindings
		{
			get
			{
				if (this._bindings == null)
				{
					this._bindings = new ControlBindingsCollection(this);
				}
				return this._bindings;
			}
			set
			{
				this._bindings = value;
			}
		}
	}
}
