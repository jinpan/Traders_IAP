using System;
using System.Collections.Generic;
namespace TTS
{
	internal class WorkspaceItem
	{
		public class WindowItem
		{
			public TTSForm Window
			{
				get;
				set;
			}
			public TTSToolStripMenuItem WindowMenuItem
			{
				get;
				set;
			}
		}
		public System.Collections.Generic.List<WorkspaceItem.WindowItem> WindowItems = new System.Collections.Generic.List<WorkspaceItem.WindowItem>();
		public TTSToolStripMenuItem WorkspaceMenuItem
		{
			get;
			set;
		}
	}
}
