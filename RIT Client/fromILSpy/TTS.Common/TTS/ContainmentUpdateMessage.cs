using ProtoBuf;
using System;
namespace TTS
{
	[ProtoContract]
	public class ContainmentUpdateMessage
	{
		[ProtoMember(1)]
		public int ID
		{
			get;
			set;
		}
		[ProtoMember(2)]
		public int Containment
		{
			get;
			set;
		}
	}
}
