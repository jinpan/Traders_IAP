using ProtoBuf;
using System;
namespace TTS
{
	[ProtoContract]
	public class OrderUpdateMessage
	{
		[ProtoMember(1)]
		public int ID
		{
			get;
			set;
		}
		[ProtoMember(2)]
		public decimal VolumeRemaining
		{
			get;
			set;
		}
		[ProtoMember(3)]
		public string TraderID
		{
			get;
			set;
		}
	}
}
