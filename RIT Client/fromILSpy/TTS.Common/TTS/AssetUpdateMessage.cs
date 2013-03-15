using ProtoBuf;
using System;
using System.Data;
namespace TTS
{
	[ProtoContract]
	public class AssetUpdateMessage
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
		public int? StartLeasePeriod
		{
			get;
			set;
		}
		[ProtoMember(4)]
		public int? StartLeaseTick
		{
			get;
			set;
		}
		[ProtoMember(5)]
		public int? NextLeasePeriod
		{
			get;
			set;
		}
		[ProtoMember(6)]
		public int? NextLeaseTick
		{
			get;
			set;
		}
		[ProtoMember(7)]
		public int? ContainmentUsage
		{
			get;
			set;
		}
		[ProtoMember(8)]
		public TickerWeight[] ConvertFrom
		{
			get;
			set;
		}
		[ProtoMember(9)]
		public TickerWeight[] ConvertTo
		{
			get;
			set;
		}
		[ProtoMember(10)]
		public int? ConvertFinishPeriod
		{
			get;
			set;
		}
		[ProtoMember(11)]
		public int? ConvertFinishTick
		{
			get;
			set;
		}
		[ProtoMember(12)]
		public decimal Realized
		{
			get;
			set;
		}
		public void SetRow(DataRow r)
		{
			r["ID"] = this.ID;
			r["Ticker"] = (this.Ticker ?? System.DBNull.Value);
			r["StartLeasePeriod"] = (this.StartLeasePeriod ?? System.DBNull.Value);
			r["StartLeaseTick"] = (this.StartLeaseTick ?? System.DBNull.Value);
			r["NextLeasePeriod"] = (this.NextLeasePeriod ?? System.DBNull.Value);
			r["NextLeaseTick"] = (this.NextLeaseTick ?? System.DBNull.Value);
			r["ContainmentUsage"] = (this.ContainmentUsage ?? System.DBNull.Value);
			r["ConvertFrom"] = (this.ConvertFrom ?? System.DBNull.Value);
			r["ConvertTo"] = (this.ConvertTo ?? System.DBNull.Value);
			r["ConvertFinishPeriod"] = (this.ConvertFinishPeriod ?? System.DBNull.Value);
			r["ConvertFinishTick"] = (this.ConvertFinishTick ?? System.DBNull.Value);
		}
	}
}
