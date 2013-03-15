using System;
namespace TTS
{
	[System.Serializable]
	public class TTSFormState
	{
		public string WindowType;
		public int? Width;
		public int? Height;
		public int? X;
		public int? Y;
		public object State;
		public TTSFormState()
		{
		}
		public TTSFormState(object state)
		{
			this.State = state;
		}
	}
}
