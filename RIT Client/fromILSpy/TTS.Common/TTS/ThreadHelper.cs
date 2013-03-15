using System;
using System.Windows.Forms;
namespace TTS
{
	public static class ThreadHelper
	{
		public static Form MainThread;
		public static void BeginInvokeIfRequired(this Form f, Action a)
		{
			if (f.InvokeRequired)
			{
				f.BeginInvoke(a);
				return;
			}
			a();
		}
		public static void InvokeIfRequired(this Form f, Action a)
		{
			if (f.InvokeRequired)
			{
				f.Invoke(a);
				return;
			}
			a();
		}
	}
}
