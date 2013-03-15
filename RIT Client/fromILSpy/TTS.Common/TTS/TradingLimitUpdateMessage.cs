using ProtoBuf;
using System;
namespace TTS
{
	[ProtoContract]
	public class TradingLimitUpdateMessage
	{
		[ProtoMember(1)]
		public string Name
		{
			get;
			set;
		}
		[ProtoMember(2)]
		public decimal Gross
		{
			get;
			set;
		}
		[ProtoMember(3)]
		public decimal Net
		{
			get;
			set;
		}
	}
}
