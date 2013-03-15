using System;
using System.ServiceModel;
namespace TTS
{
	[ServiceContract]
	public interface IAPIObject
	{
		[OperationContract]
		int GetTimeRemaining();
		[OperationContract]
		int GetTotalTime();
		[OperationContract]
		int GetYearTime();
		[OperationContract]
		int GetCurrentPeriod();
		[OperationContract]
		decimal GetCash();
		[OperationContract]
		decimal GetBP();
		[OperationContract]
		decimal GetNLV();
		[OperationContract]
		string[] GetTickers();
		[OperationContract]
		object[] GetTickerInfo(string ticker);
		[OperationContract]
		int[] GetOrders();
		[OperationContract]
		object[] GetOrderInfo(int id);
		[OperationContract]
		bool AddOrder(string ticker, int volume, decimal price, int dir, int type);
		[OperationContract]
		void CancelOrder(int id);
		[OperationContract]
		void CancelOrderExpr(string expr);
		[OperationContract]
		int AddQueuedOrder(string ticker, int volume, decimal price, int dir, int type);
		[OperationContract]
		bool IsOrderQueued(int id);
		[OperationContract]
		void CancelQueuedOrder(int id);
		[OperationContract]
		void ClearQueuedOrders();
	}
}
