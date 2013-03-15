using ProtoBuf;
using System;
using System.ComponentModel;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	public class AssetParametersUpdate
	{
		[ProtoMember(1)]
		public string Ticker
		{
			get;
			set;
		}
		[ProtoMember(2)]
		public string Description
		{
			get;
			set;
		}
		[ProtoMember(4)]
		public int TotalQuantity
		{
			get;
			set;
		}
		[ProtoMember(8)]
		public decimal LeasePrice
		{
			get;
			set;
		}
		[ProtoMember(20)]
		public decimal VariableLeasePrice
		{
			get;
			set;
		}
		[ProtoMember(9)]
		public int TicksPerLease
		{
			get;
			set;
		}
		[ProtoMember(15)]
		public int TicksPerConversion
		{
			get;
			set;
		}
		[ProtoMember(16)]
		public bool IsAllowBackhaul
		{
			get;
			set;
		}
		[ProtoMember(17)]
		public string DisplayCost
		{
			get;
			set;
		}
		[ProtoMember(18)]
		public int LeaseLimit
		{
			get;
			set;
		}
		[ProtoMember(19)]
		public int DistressedMultiplier
		{
			get;
			set;
		}
	}
}
