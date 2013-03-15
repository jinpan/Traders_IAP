using ProtoBuf;
using System;
namespace TTS
{
	[ProtoContract]
	public class TransactionAddMessage
	{
		[ProtoMember(1)]
		public int ID
		{
			get;
			set;
		}
		[ProtoMember(2)]
		public string Ticker
		{
			get;
			set;
		}
		[ProtoMember(3)]
		public decimal? Price
		{
			get;
			set;
		}
		[ProtoMember(4)]
		public decimal? Quantity
		{
			get;
			set;
		}
		[ProtoMember(5)]
		public TransactionType Type
		{
			get;
			set;
		}
		[ProtoMember(6)]
		public decimal Value
		{
			get;
			set;
		}
		[ProtoMember(7)]
		public string Currency
		{
			get;
			set;
		}
		[ProtoMember(8)]
		public decimal Balance
		{
			get;
			set;
		}
	}
}
