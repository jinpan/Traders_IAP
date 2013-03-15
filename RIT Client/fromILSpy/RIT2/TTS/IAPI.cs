using System;
using System.Runtime.InteropServices;
namespace TTS
{
	[ComVisible(true), Guid("FE7B5364-D6B3-4CD6-9DEB-C1B99E73E3B7"), InterfaceType(ComInterfaceType.InterfaceIsDual)]
	public interface IAPI
	{
		int BUY
		{
			get;
		}
		int SELL
		{
			get;
		}
		int MKT
		{
			get;
		}
		int LMT
		{
			get;
		}
		int GetTimeRemaining();
		int GetTotalTime();
		int GetYearTime();
		int GetCurrentPeriod();
		double GetCash();
		double GetBP();
		double GetNLV();
		string[] GetTickers();
		object[] GetTickerInfo(string ticker);
		int[] GetOrders();
		object[] GetOrderInfo(int id);
		bool AddOrder(string ticker, int volume, double price, int dir, int type);
		void CancelOrder(int id);
		void CancelOrderExpr(string expr);
		int AddQueuedOrder(string ticker, int volume, double price, int dir, int type);
		bool IsOrderQueued(int id);
		void CancelQueuedOrder(int id);
		void ClearQueuedOrders();
	}
}
