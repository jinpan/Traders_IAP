using ProtoBuf;
using System;
using System.ComponentModel;
using System.Linq;
namespace TTS
{
	[ProtoContract, TypeConverter(typeof(ExpandableObjectConverter))]
	[System.Serializable]
	public class TickerWeight
	{
		[ProtoMember(1)]
		public string Ticker
		{
			get;
			set;
		}
		[ProtoMember(2)]
		public decimal Weight
		{
			get;
			set;
		}
		public TickerWeight()
		{
		}
		public TickerWeight(string ticker, decimal weight)
		{
			this.Ticker = ticker;
			this.Weight = weight;
		}
		public static TickerWeight ParseString(string s)
		{
			string[] array = s.Split(new char[]
			{
				'|'
			});
			return new TickerWeight
			{
				Ticker = array[0].Trim(),
				Weight = System.Convert.ToDecimal(array[1])
			};
		}
		public static TickerWeight[] ParseArray(string s)
		{
			string[] source = s.Split(new char[]
			{
				','
			});
			return (
				from x in source
				where !string.IsNullOrWhiteSpace(x)
				select x).Select(new Func<string, TickerWeight>(TickerWeight.ParseString)).ToArray<TickerWeight>();
		}
		public override string ToString()
		{
			return string.Format("{1}x{0:0.####}", this.Weight, this.Ticker);
		}
	}
}
