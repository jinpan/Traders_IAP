using System;
using System.ComponentModel;
using System.Drawing.Design;
namespace TTS
{
	[Editor(typeof(FlagEnumUIEditor), typeof(System.Drawing.Design.UITypeEditor)), System.Flags]
	public enum WindowType
	{
		NONE = 0,
		PORTFOLIO = 1,
		BUYSELL_ENTRY = 2,
		SPREAD_ENTRY = 4,
		TRANSPORTATION_ARBITRAGE_ENTRY = 8,
		OTC_ENTRY = 16,
		BOOK_TRADER = 32,
		LADDER_TRADER = 64,
		TRADE_BLOTTER = 128,
		ASSETS = 256,
		TRANSACTION_LOG = 512,
		NEWS = 1024,
		SECURITY_CHARTING = 2048,
		TRADER_INFO = 4096,
		CHAT = 8192,
		KILL = 16384,
		TIME_AND_SALES = 32768,
		ELECTRICITY_CHARTING = 65536,
		PNL_CHARTING = 131072,
		CHARTING = 133120
	}
}
