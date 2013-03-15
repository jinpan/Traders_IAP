using ProtoBuf.ServiceModel;
using System;
using System.ServiceModel;
namespace TTS
{
	[ServiceContract(Name = "TTSClientServce", Namespace = "TTSService", SessionMode = SessionMode.Required, CallbackContract = typeof(IClientCallbackService))]
	public interface IClientService
	{
		[ProtoBehavior, OperationContract(IsInitiating = false)]
		void Ping();
		[ProtoBehavior, OperationContract(IsInitiating = true)]
		void Authenticate(string traderid, string password);
		[ProtoBehavior, OperationContract(IsInitiating = true)]
		void NewUser(string traderid, string password, string firstname, string lastname);
		[ProtoBehavior, OperationContract(IsInitiating = true, IsOneWay = true)]
		void SyncReceived();
		[ProtoBehavior, OperationContract(IsInitiating = false)]
		void AddOrder(string ticker, decimal volume, OrderType type, decimal price);
		[ProtoBehavior, OperationContract(IsInitiating = false)]
		void AddSpreadOrder(TickerWeight[] spread);
		[ProtoBehavior, OperationContract(IsInitiating = false)]
		void AddTransportArbOrder(string buyticker, string sellticker, decimal buyvolume, decimal sellvolume, string transportticker, string cost);
		[ProtoBehavior, OperationContract(IsInitiating = false)]
		void CancelOrder(string ticker, params int[] orderid);
		[ProtoBehavior, OperationContract(IsInitiating = false)]
		void CancelAllLimitOrders();
		[ProtoBehavior, OperationContract]
		void AddOTCOrder(string target, string ticker, decimal volume, decimal price, int? settleperiod, int? settletick);
		[ProtoBehavior, OperationContract]
		void UpdateOTCOrder(int id, OTCStatus status);
		[ProtoBehavior, OperationContract(IsInitiating = false)]
		void LeaseAsset(string ticker);
		[ProtoBehavior, OperationContract(IsInitiating = false)]
		void UnleaseAsset(int id);
		[ProtoBehavior, OperationContract(IsInitiating = false)]
		void UseLeasedAsset(int id, TickerWeight[] toconvert);
		[ProtoBehavior, OperationContract(IsInitiating = false)]
		void UseAsset(string ticker, TickerWeight[] toconvert);
		[ProtoBehavior, OperationContract(IsInitiating = false)]
		void ReadNews(int id);
		[ProtoBehavior, OperationContract(IsInitiating = false)]
		void BidOnTender(int id, bool isaccept, decimal bid);
		[ProtoBehavior, OperationContract(IsInitiating = false)]
		void ChatMessage(string to, string message);
		[ProtoBehavior, OperationContract(IsInitiating = false)]
		void ComplianceResponse(int id, bool isaccept, string reason);
		[ProtoBehavior, OperationContract(IsInitiating = false)]
		byte[] GetReportChunk(int index);
	}
}
