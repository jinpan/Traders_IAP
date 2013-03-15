using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
namespace TTS
{
	[System.Windows.Forms.Design.ToolStripItemDesignerAvailability(System.Windows.Forms.Design.ToolStripItemDesignerAvailability.All)]
	public class TTSFormMenuItem : System.Windows.Forms.ToolStripMenuItem
	{
		public WindowType TTSWindowType
		{
			get;
			set;
		}
		public TTSFormMenuItem()
		{
		}
		public TTSFormMenuItem(string text) : base(text)
		{
		}
		public TTSFormMenuItem(System.Drawing.Image image) : base(image)
		{
		}
		public TTSFormMenuItem(string text, System.Drawing.Image image) : base(text, image)
		{
		}
		public TTSFormMenuItem(string text, System.Drawing.Image image, System.EventHandler onClick) : base(text, image, onClick)
		{
		}
		public TTSFormMenuItem(string text, System.Drawing.Image image, System.EventHandler onClick, string name) : base(text, image, onClick, name)
		{
		}
		public TTSFormMenuItem(string text, System.Drawing.Image image, params System.Windows.Forms.ToolStripItem[] dropDownItems) : base(text, image, dropDownItems)
		{
		}
		public TTSFormMenuItem(string text, System.Drawing.Image image, System.EventHandler onClick, System.Windows.Forms.Keys shortcutKeys) : base(text, image, onClick, shortcutKeys)
		{
		}
	}
}
