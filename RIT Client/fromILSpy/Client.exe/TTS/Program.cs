using System;
using System.Windows.Forms;
namespace TTS
{
	internal static class Program
	{
		[System.STAThread]
		private static void Main()
		{
			System.Windows.Forms.Application.EnableVisualStyles();
			System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
			System.Windows.Forms.Application.Run(new Client());
		}
	}
}
