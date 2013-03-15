using System;
using System.Drawing;
using System.Windows.Forms;
namespace TTS
{
	internal class TTSToolStripMenuItem : System.Windows.Forms.ToolStripMenuItem
	{
		public TTSToolStripMenuItem(string text) : base(text)
		{
		}
		public TTSToolStripMenuItem(string text, System.Drawing.Image image, System.EventHandler onClick) : base(text, image, onClick)
		{
		}
	}
}
