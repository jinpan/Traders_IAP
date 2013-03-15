using ProtoBuf;
using System;
namespace TTS
{
	[ProtoContract]
	public class OTCUpdateMessage
	{
		[ProtoMember(1)]
		public int ID
		{
			get;
			set;
		}
		[ProtoMember(2)]
		public string TraderID
		{
			get;
			set;
		}
		[ProtoMember(3)]
		public string Target
		{
			get;
			set;
		}
		[ProtoMember(4)]
		public string Ticker
		{
			get;
			set;
		}
		[ProtoMember(5)]
		public decimal Volume
		{
			get;
			set;
		}
		[ProtoMember(6)]
		public decimal Price
		{
			get;
			set;
		}
		[ProtoMember(7)]
		public OTCStatus Status
		{
			get;
			set;
		}
		[ProtoMember(8)]
		public int? SettlePeriod
		{
			get;
			set;
		}
		[ProtoMember(9)]
		public int? SettleTick
		{
			get;
			set;
		}
	}
}
