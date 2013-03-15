using ProtoBuf;
using System;
namespace TTS
{
	[ProtoContract]
	public class OrderAddMessage
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
		public decimal Volume
		{
			get;
			set;
		}
		[ProtoMember(4)]
		public decimal VolumeRemaining
		{
			get;
			set;
		}
		[ProtoMember(5)]
		public OrderType Type
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
		public decimal VWAP
		{
			get;
			set;
		}
		[ProtoMember(8)]
		public string Ticker
		{
			get;
			set;
		}
	}
}
