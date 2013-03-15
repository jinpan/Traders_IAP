using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
namespace TTS
{
	[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single, UseSynchronizationContext = true, MaxItemsInObjectGraph = 2147483647)]
	public class APIObject : IAPIObject
	{
		private APIObject()
		{
		}
		public int GetTimeRemaining()
		{
			return Game.State.General.TicksPerPeriod - Game.State.Current.Tick;
		}
		public int GetTotalTime()
		{
			return Game.State.General.TicksPerPeriod;
		}
		public int GetYearTime()
		{
			return Game.State.General.TicksPerYear;
		}
		public int GetCurrentPeriod()
		{
			return Game.State.Current.Period;
		}
		public decimal GetCash()
		{
			return Game.State.Accounts[Game.State.DefaultCurrency].Balance;
		}
		public decimal GetBP()
		{
			return 0m;
		}
		public decimal GetNLV()
		{
			return Game.State.NLV;
		}
		public string[] GetTickers()
		{
			return Game.State.Securities.Keys.ToArray<string>();
		}
		public object[] GetTickerInfo(string ticker)
		{
			if (Game.State.Securities.ContainsKey(ticker) && Game.State.PortfolioTable.Rows.Find(ticker) != null)
			{
				SecurityItem securityItem = Game.State.Securities[ticker];
				DataRow dataRow = Game.State.PortfolioTable.Rows.Find(ticker);
				return new object[]
				{
					securityItem.Parameters.Ticker,
					dataRow["Position"],
					securityItem.Last,
					securityItem.BidSize,
					securityItem.Bid,
					securityItem.Ask,
					securityItem.AskSize,
					0,
					securityItem.Volume,
					dataRow["VWAP"],
					dataRow["Unrealized"],
					dataRow["Realized"],
					securityItem.Parameters.Currency,
					securityItem.Volume.ToString()
				};
			}
			return null;
		}
		public int[] GetOrders()
		{
			System.Collections.Generic.List<int> list = new System.Collections.Generic.List<int>();
			foreach (DataRowView dataRowView in Game.State.OpenOrderView)
			{
				list.Add((int)dataRowView["ID"]);
			}
			return list.ToArray();
		}
		public object[] GetOrderInfo(int id)
		{
			DataRow dataRow = Game.State.OrderTable.Rows.Find(id);
			if (dataRow != null)
			{
				return new object[]
				{
					dataRow["ID"],
					dataRow["Ticker"],
					dataRow["Period"],
					((decimal)dataRow["Volume"] > 0m) ? "BUY" : "SELL",
					((OrderType)dataRow["Type"]).ToString(),
					dataRow["Volume"],
					dataRow["Price"],
					((decimal)dataRow["VolumeRemaining"] == System.Math.Abs((decimal)dataRow["Volume"])) ? "LIVE" : "PARTIAL",
					dataRow["VolumeRemaining"]
				};
			}
			return null;
		}
		public bool AddOrder(string ticker, decimal volume, decimal price, int dir, int type)
		{
			this.AddQueuedOrder(ticker, volume, price, dir, type);
			return true;
		}
		public void CancelOrder(int id)
		{
			DataRow row = Game.State.OrderTable.Rows.Find(id);
			if (row != null)
			{
				try
				{
					ServiceManager.Execute(delegate(IClientService p)
					{
						p.CancelOrder((string)row["Ticker"], new int[]
						{
							id
						});
					});
				}
				catch (FaultException)
				{
				}
			}
		}
		public void CancelOrderExpr(string expr)
		{
			try
			{
				System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<int>> dictionary = new System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<int>>();
				if (!string.IsNullOrWhiteSpace(expr))
				{
					DataView dataView = new DataView(Game.State.OpenOrderView.Table, Game.State.OpenOrderView.RowFilter, "", Game.State.OpenOrderView.RowStateFilter);
					dataView.RowFilter = string.Concat(new string[]
					{
						"(",
						dataView.RowFilter,
						") AND (",
						expr,
						")"
					});
					foreach (DataRowView dataRowView in dataView)
					{
						string key = (string)dataRowView["Ticker"];
						if (!dictionary.ContainsKey(key))
						{
							dictionary.Add(key, new System.Collections.Generic.List<int>());
						}
						dictionary[key].Add((int)dataRowView["ID"]);
					}
					foreach (System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.List<int>> current in dictionary)
					{
						try
						{
							System.Collections.Generic.KeyValuePair<string, System.Collections.Generic.List<int>> k = current;
							ServiceManager.Execute(delegate(IClientService p)
							{
								p.CancelOrder(k.Key, k.Value.ToArray());
							});
						}
						catch (FaultException)
						{
						}
					}
				}
			}
			catch
			{
			}
		}
		public int AddQueuedOrder(string ticker, decimal volume, decimal price, int dir, int type)
		{
			return APIManager.EnqueueOrder(ticker, volume * System.Math.Sign(dir), (type == 1) ? OrderType.LIMIT : OrderType.MARKET, price);
		}
		public bool IsOrderQueued(int id)
		{
			return APIManager.IsOrderQueued(id);
		}
		public void CancelQueuedOrder(int id)
		{
			APIManager.DequeueOrder(id);
		}
		public void ClearQueuedOrders()
		{
			APIManager.DequeueAllOrders();
		}
	}
}
