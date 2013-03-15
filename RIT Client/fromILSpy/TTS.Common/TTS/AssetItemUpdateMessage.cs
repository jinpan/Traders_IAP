using ProtoBuf;
using System;
namespace TTS
{
	[ProtoContract]
	public class AssetItemUpdateMessage
	{
		[ProtoMember(1)]
		public string Ticker
		{
			get;
			set;
		}
		[ProtoMember(2)]
		public int LeaseCount
		{
			get;
			set;
		}
	}
}
