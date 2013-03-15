using ProtoBuf;
using System;
using System.Data;
namespace TTS
{
	[ProtoContract]
	public class PortfolioUpdateMessage
	{
		[ProtoMember(1)]
		public string Ticker
		{
			get;
			set;
		}
		[ProtoMember(2)]
		public decimal Position
		{
			get;
			set;
		}
		[ProtoMember(3)]
		public decimal VWAP
		{
			get;
			set;
		}
		[ProtoMember(4)]
		public decimal Realized
		{
			get;
			set;
		}
		[ProtoMember(5)]
		public ContainmentUpdateMessage[] UpdatedContainments
		{
			get;
			set;
		}
		public void SetRow(DataRow r)
		{
			r["Ticker"] = (this.Ticker ?? System.DBNull.Value);
			r["Position"] = this.Position;
			r["VWAP"] = this.VWAP;
			r["Realized"] = this.Realized;
		}
	}
}
