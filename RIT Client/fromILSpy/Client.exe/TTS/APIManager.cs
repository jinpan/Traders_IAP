using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
namespace TTS
{
	internal static class APIManager
	{
		private class QueuedOrder
		{
			public int ID;
			public string Ticker;
			public decimal Volume;
			public decimal Price;
			public OrderType Type;
			public QueuedOrder(int id, string ticker, decimal volume, decimal price, OrderType type)
			{
				this.ID = id;
				this.Ticker = ticker;
				this.Volume = volume;
				this.Price = price;
				this.Type = type;
			}
		}
		public const string NamedPipe = "306W/TTS/API";
		private static ServiceHost Host;
		private static System.Collections.Generic.Dictionary<int, APIManager.QueuedOrder> OrderDetails;
		private static Queue<int> OrderQueue;
		private static object OrderSync;
		private static System.Threading.ManualResetEvent OrderSyncEvent;
		private static int OrderID;
		public static event System.Action<bool> StateChanged;
		static APIManager()
		{
			APIManager.Host = null;
			APIManager.StateChanged = delegate
			{
			};
			APIManager.OrderDetails = new System.Collections.Generic.Dictionary<int, APIManager.QueuedOrder>();
			APIManager.OrderQueue = new Queue<int>();
			APIManager.OrderSync = new object();
			APIManager.OrderSyncEvent = new System.Threading.ManualResetEvent(false);
			APIManager.OrderID = 0;
			Game.Reset = (Action)System.Delegate.Combine(Game.Reset, new Action(APIManager.SetState));
			Game.VariablesUpdated = (Action)System.Delegate.Combine(Game.VariablesUpdated, new Action(APIManager.SetState));
			ServiceManager.Disconnected += new Action(APIManager.Stop);
			System.Threading.ThreadPool.QueueUserWorkItem(delegate(object x)
			{
				APIManager.ProcessOrderQueue();
			});
		}
		public static int EnqueueOrder(string ticker, decimal volume, OrderType type, decimal price)
		{
			int num = System.Threading.Interlocked.Increment(ref APIManager.OrderID);
			lock (APIManager.OrderSync)
			{
				APIManager.QueuedOrder value = new APIManager.QueuedOrder(num, ticker, volume, price, type);
				APIManager.OrderDetails.Add(num, value);
				APIManager.OrderQueue.Enqueue(num);
				APIManager.OrderSyncEvent.Set();
			}
			return num;
		}
		public static bool IsOrderQueued(int id)
		{
			bool result;
			lock (APIManager.OrderSync)
			{
				result = APIManager.OrderDetails.ContainsKey(id);
			}
			return result;
		}
		public static void DequeueOrder(int id)
		{
			lock (APIManager.OrderSync)
			{
				if (APIManager.OrderDetails.ContainsKey(id))
				{
					APIManager.OrderDetails.Remove(id);
				}
			}
		}
		public static void DequeueAllOrders()
		{
			lock (APIManager.OrderSync)
			{
				APIManager.OrderDetails.Clear();
				APIManager.OrderQueue.Clear();
			}
		}
		public static void ProcessOrderQueue()
		{
			while (true)
			{
				bool flag = false;
				APIManager.OrderSyncEvent.WaitOne();
				bool flag2 = false;
				try
				{
					object orderSync;
					System.Threading.Monitor.Enter(orderSync = APIManager.OrderSync, ref flag2);
					APIManager.QueuedOrder order = null;
					while (APIManager.OrderQueue.Count > 0)
					{
						int key = APIManager.OrderQueue.Dequeue();
						if (APIManager.OrderDetails.ContainsKey(key))
						{
							order = APIManager.OrderDetails[key];
							APIManager.OrderDetails.Remove(key);
							break;
						}
					}
					if (order != null)
					{
						try
						{
							ServiceManager.Execute(delegate(IClientService p)
							{
								p.AddOrder(order.Ticker, order.Volume, order.Type, order.Price);
							});
						}
						catch
						{
						}
						flag = true;
					}
					else
					{
						APIManager.OrderSyncEvent.Reset();
					}
				}
				finally
				{
					if (flag2)
					{
						object orderSync;
						System.Threading.Monitor.Exit(orderSync);
					}
				}
				if (flag)
				{
					System.Threading.Thread.Sleep((Game.State.General.APIOrdersPerSecond > 0) ? (1000 / Game.State.General.APIOrdersPerSecond) : 1000);
				}
			}
		}
		private static void SetState()
		{
			if (Game.State.General.IsAPIEnabled)
			{
				APIManager.Start();
				return;
			}
			APIManager.Stop();
		}
		public static void Start()
		{
			try
			{
				if (APIManager.Host == null)
				{
					APIManager.Host = new ServiceHost(typeof(APIObject), new Uri[]
					{
						new Uri("net.pipe://localhost")
					});
					APIManager.Host.AddServiceEndpoint(typeof(IAPIObject), new NetNamedPipeBinding(), "306W/TTS/API");
					APIManager.Host.AddServiceEndpoint(typeof(IAPIObject), new NetNamedPipeBinding(), "Rotman/RIT/API");
					APIManager.Host.Open();
					APIManager.StateChanged(true);
				}
			}
			catch
			{
				((Client)ThreadHelper.MainThread).ShowInfo("API Error", string.Format("Another instance of the {0} client is currently running. Only the first instance will have an API.", Client.Skin.GetString("program_name")));
				APIManager.Stop();
			}
		}
		public static void Stop()
		{
			try
			{
				if (APIManager.Host != null)
				{
					APIManager.Host.Abort();
				}
			}
			catch
			{
			}
			finally
			{
				APIManager.Host = null;
				APIManager.StateChanged(false);
			}
		}
	}
}
