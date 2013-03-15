using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Windows.Forms;
using TTS.Properties;
namespace TTS
{
	[CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, UseSynchronizationContext = false)]
	internal class ClientCallbackService : IClientCallbackService
	{
		private int UpdateID;
		private bool IsSynced;
		private SortedDictionary<int, UpdateState> OrderedUpdateQueue = new SortedDictionary<int, UpdateState>();
		private Queue<Action> PresyncQueue = new Queue<Action>();
		private object SyncRoot = new object();
		private void EnqueueOrderedUpdate(UpdateState update = null)
		{
			lock (this.SyncRoot)
			{
				if (update != null)
				{
					this.OrderedUpdateQueue.Add(update.ID, update);
				}
				while (this.IsSynced && this.OrderedUpdateQueue.Count > 0 && this.OrderedUpdateQueue.First<System.Collections.Generic.KeyValuePair<int, UpdateState>>().Key == this.UpdateID)
				{
					UpdateState state = this.OrderedUpdateQueue[this.UpdateID];
					this.OrderedUpdateQueue.Remove(this.UpdateID);
					this.UpdateID++;
					ThreadHelper.MainThread.BeginInvokeIfRequired(delegate
					{
						StateManager.UpdateState(state);
					});
				}
			}
		}
		private void EnqueueCallback(Action callback)
		{
			lock (this.SyncRoot)
			{
				if (this.IsSynced)
				{
					ThreadHelper.MainThread.BeginInvokeIfRequired(callback);
				}
				else
				{
					this.PresyncQueue.Enqueue(callback);
				}
			}
		}
		public void Ping()
		{
		}
		public void SendMessage(string message, SystemMessageType type)
		{
			ThreadHelper.MainThread.BeginInvokeIfRequired(delegate
			{
				switch (type)
				{
				case SystemMessageType.INFO:
					if (Settings.Default.IsSilentAlerts)
					{
						((Client)ThreadHelper.MainThread).ShowInfo("Information", message);
						return;
					}
					DialogHelper.ShowInfo(message, "Information");
					return;
				case SystemMessageType.ERROR:
					DialogHelper.ShowError(message, "Error");
					return;
				case SystemMessageType.BLOCKINGINFO:
					DialogHelper.ShowInfo(message, "Information");
					return;
				default:
					return;
				}
			});
		}
		public void SendNewTraderMessage(string traderid)
		{
			ThreadHelper.MainThread.BeginInvokeIfRequired(delegate
			{
				if (DialogHelper.Confirm("Trader ID does not exist. Create a new account?", "Confirmation", false) == System.Windows.Forms.DialogResult.OK)
				{
					((Client)ThreadHelper.MainThread).SetUIState(Client.UIState.NEW_USER);
				}
			});
		}
		public void Sync(SyncState state)
		{
			ServiceManager.Execute(delegate(IClientService x)
			{
				x.SyncReceived();
			});
			((Client)ThreadHelper.MainThread).SetLoadingStatus("Syncing...");
			Game game = new Game();
			game.Sync(state);
			Game.State = game;
			ConnectedTradersManager.Initialize();
			ChatManager.Initialize();
			lock (this.SyncRoot)
			{
				this.IsSynced = true;
				this.EnqueueOrderedUpdate(null);
				while (this.PresyncQueue.Count > 0)
				{
					Action a = this.PresyncQueue.Dequeue();
					ThreadHelper.MainThread.BeginInvokeIfRequired(a);
				}
			}
			((Client)ThreadHelper.MainThread).SetLoadingStatus("UI...");
			ThreadHelper.MainThread.BeginInvokeIfRequired(delegate
			{
				GameManager.UpdateStatus(Game.State.Current);
				((Client)ThreadHelper.MainThread).SetUIState(Client.UIState.ACTIVE);
			});
		}
		public void Reset(SyncState state)
		{
			Game gamestate = new Game();
			gamestate.Sync(state);
			this.EnqueueCallback(delegate
			{
				Game.State = gamestate;
			});
		}
		public void OrderedUpdate(UpdateState state)
		{
			this.EnqueueOrderedUpdate(state);
		}
		public void Heartbeat(int tick)
		{
			this.EnqueueCallback(delegate
			{
				GameManager.UpdateTick(tick);
			});
		}
		public void UpdateGameStatus(GameParameters current)
		{
			this.EnqueueCallback(delegate
			{
				GameManager.UpdateStatus(current);
			});
		}
		public void UpdateAssetLeaseCount(AssetItemUpdateMessage item)
		{
			this.EnqueueCallback(delegate
			{
				StateManager.UpdateAssetLeaseCount(item.Ticker, item.LeaseCount);
			});
		}
		public void UpdateNews(int id, string ticker, string headline, string body)
		{
			this.EnqueueCallback(delegate
			{
				StateManager.UpdateNews(id, ticker, headline, body);
			});
		}
		public void TenderOffer(int id, int expiretick, string ticker, string caption, decimal bid, decimal volume)
		{
			this.EnqueueCallback(delegate
			{
				TenderOrder tenderOrder = new TenderOrder(id, expiretick, caption, (bid == 0m) ? Game.State.Securities[ticker].Last : bid, Game.State.Securities[ticker].Parameters.QuotedDecimals, bid != 0m, volume);
				tenderOrder.MdiParent = ThreadHelper.MainThread;
				tenderOrder.Show();
				Client.SetParent((int)tenderOrder.Handle, (int)ThreadHelper.MainThread.Handle);
			});
		}
		public void AddOrderHistory(int id, string ticker, decimal price, decimal volume, OrderType type, int period, int tick, OrderStatus status)
		{
			this.EnqueueCallback(delegate
			{
				StateManager.AddArchiveOrder(id, ticker, price, volume, type, period, tick, status);
			});
		}
		public void UpdateVariable(VariablesUpdateMessage update)
		{
			this.EnqueueCallback(delegate
			{
				Game.State.Update(update.General, update.Security, update.Asset, update.TradingLimit);
			});
		}
		public void AddConnectedTraders(string[] traderids)
		{
			if (traderids == null)
			{
				return;
			}
			this.EnqueueCallback(delegate
			{
				foreach (string current in 
					from x in traderids
					where Game.State.Trader.TraderID != x
					select x)
				{
					ConnectedTradersManager.AddClient(current);
				}
			});
		}
		public void RemoveConnectedTrader(string traderid)
		{
			this.EnqueueCallback(delegate
			{
				ConnectedTradersManager.RemoveClient(traderid);
			});
		}
		public void ChatMessage(string from, string to, string message)
		{
			this.EnqueueCallback(delegate
			{
				ChatManager.AddChatText(from, to, message);
			});
		}
		public void ToggleReportDownload(bool visible)
		{
			this.EnqueueCallback(delegate
			{
				((Client)ThreadHelper.MainThread).ToggleTraderReportDownload(visible);
			});
		}
		public void ComplianceMessage(int id, MessageType type, string message)
		{
			this.EnqueueCallback(delegate
			{
				new ComplianceEvent(id, type, message).ShowDialog(ThreadHelper.MainThread);
			});
		}
	}
}
