using ProtoBuf;
using System;
using System.ComponentModel;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	public class RiskTypeParametersUpdate
	{
		[ProtoMember(1)]
		public string Name
		{
			get;
			set;
		}
		[ProtoMember(2)]
		public int GrossRiskLimit
		{
			get;
			set;
		}
		[ProtoMember(3)]
		public decimal GrossRiskFine
		{
			get;
			set;
		}
		[ProtoMember(4)]
		public int NetRiskLimit
		{
			get;
			set;
		}
		[ProtoMember(5)]
		public decimal NetRiskFine
		{
			get;
			set;
		}
	}
}
