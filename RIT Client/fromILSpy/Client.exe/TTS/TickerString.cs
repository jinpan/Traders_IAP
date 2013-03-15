using System;
namespace TTS
{
	public class TickerString
	{
		public string Ticker
		{
			get;
			set;
		}
		public TickerString(string ticker)
		{
			this.Ticker = ticker;
		}
		public string ToChartString()
		{
			return string.Format("[{0}]", this.Ticker);
		}
		public override string ToString()
		{
			return this.Ticker;
		}
	}
}
