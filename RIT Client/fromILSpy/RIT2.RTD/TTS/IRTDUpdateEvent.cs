using System;
using System.Runtime.InteropServices;
namespace TTS
{
	[Guid("A43788C1-D91B-11D3-8F39-00C04F3651B8")]
	public interface IRTDUpdateEvent
	{
		int HeartbeatInterval
		{
			get;
			set;
		}
		void UpdateNotify();
		void Disconnect();
	}
}
