using ProtoBuf.ServiceModel;
using System;
using System.ServiceModel;
namespace TTS
{
	public interface IClientCallbackService
	{
		[ProtoBehavior, OperationContract]
		void Ping();
		[ProtoBehavior, OperationContract(IsOneWay = true)]
		void SendMessage(string message, SystemMessageType type);
		[ProtoBehavior, OperationContract(IsOneWay = true)]
		void SendNewTraderMessage(string traderid);
		[ProtoBehavior, OperationContract]
		void Sync(SyncState state);
		[ProtoBehavior, OperationContract(IsOneWay = true)]
		void OrderedUpdate(UpdateState state);
		[ProtoBehavior, OperationContract]
		void Reset(SyncState state);
		[ProtoBehavior, OperationContract(IsOneWay = true)]
		void Heartbeat(int tick);
		[ProtoBehavior, OperationContract(IsOneWay = true)]
		void UpdateGameStatus(GameParameters current);
		[ProtoBehavior, OperationContract(IsOneWay = true)]
		void UpdateAssetLeaseCount(AssetItemUpdateMessage item);
		[ProtoBehavior, OperationContract(IsOneWay = true)]
		void UpdateNews(int id, string ticker, string headline, string body);
		[ProtoBehavior, OperationContract(IsOneWay = true)]
		void TenderOffer(int id, int expiretick, string ticker, string caption, decimal bid, decimal volume);
		[ProtoBehavior, OperationContract(IsOneWay = true)]
		void AddOrderHistory(int id, string ticker, decimal price, decimal volume, OrderType type, int period, int tick, OrderStatus status);
		[ProtoBehavior, OperationContract(IsOneWay = true)]
		void UpdateVariable(VariablesUpdateMessage update);
		[ProtoBehavior, OperationContract(IsOneWay = true)]
		void AddConnectedTraders(string[] traderids);
		[ProtoBehavior, OperationContract(IsOneWay = true)]
		void RemoveConnectedTrader(string traderid);
		[ProtoBehavior, OperationContract(IsOneWay = true)]
		void ChatMessage(string from, string to, string message);
		[ProtoBehavior, OperationContract(IsOneWay = true)]
		void ToggleReportDownload(bool visible);
		[ProtoBehavior, OperationContract(IsOneWay = true)]
		void ComplianceMessage(int id, MessageType type, string message);
	}
}
