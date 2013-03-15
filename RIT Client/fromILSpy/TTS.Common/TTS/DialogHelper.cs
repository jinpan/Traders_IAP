using System;
using System.Windows.Forms;
namespace TTS
{
	public static class DialogHelper
	{
		public static DialogResult Confirm(string msg, string caption = "Confirmation", bool isquestion = false)
		{
			return MessageBox.Show(msg, caption, isquestion ? MessageBoxButtons.YesNo : MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
		}
		public static void ShowError(string msg, string caption = "Error")
		{
			ThreadHelper.MainThread.BeginInvokeIfRequired(delegate
			{
				MessageBox.Show(ThreadHelper.MainThread, msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
			});
		}
		public static void ShowInfo(string msg, string caption = "Information")
		{
			ThreadHelper.MainThread.BeginInvokeIfRequired(delegate
			{
				MessageBox.Show(ThreadHelper.MainThread, msg, caption, MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
			});
		}
	}
}
