using System;
using System.Collections.Generic;
using System.Data;
using System.Media;
using System.ServiceModel;
using TTS.Properties;
namespace TTS
{
	internal static class StateManager
	{
		public static void UpdateState(UpdateState state)
		{
			Game.State.Current.Period = state.Period;
			Game.State.Current.Tick = state.Tick;
			if (state.AddedOrders != null)
			{
				StateManager.AddOrders(state.AddedOrders.ToArray());
			}
			if (state.UpdatedOrders != null)
			{
				StateManager.UpdateOrders(state.UpdatedOrders.ToArray());
			}
			if (state.CancelledOrders != null)
			{
				StateManager.CancelOrder(state.CancelledOrders.ToArray());
			}
			if (state.UpdatedAssets != null)
			{
				StateManager.UpdateAsset(state.UpdatedAssets.ToArray());
			}
			if (state.UnleasedAssets != null)
			{
				StateManager.RemoveAsset(state.UnleasedAssets.ToArray());
			}
			if (state.UpdatedPortfolio != null)
			{
				foreach (PortfolioUpdateMessage current in state.UpdatedPortfolio.Values)
				{
					StateManager.UpdatePortfolio(current);
				}
			}
			if (state.UpdatedTradingLimits != null)
			{
				foreach (TradingLimitUpdateMessage current2 in state.UpdatedTradingLimits.Values)
				{
					StateManager.UpdateTradingLimits(current2);
				}
			}
			if (state.AddedTransactions != null)
			{
				StateManager.MergeTransactions(state.AddedTransactions.ToArray());
			}
			if (state.UpdatedLast != null)
			{
				foreach (System.Collections.Generic.KeyValuePair<string, decimal> current3 in state.UpdatedLast)
				{
					Game.State.Securities[current3.Key].Last = current3.Value;
				}
			}
			if (state.UpdatedVolume != null)
			{
				foreach (System.Collections.Generic.KeyValuePair<string, decimal> current4 in state.UpdatedVolume)
				{
					Game.State.Securities[current4.Key].Volume += current4.Value;
					Game.State.Securities[current4.Key].VolumeChart.AddPoint(Game.State.Current.Period, Game.State.Current.Tick, new SecurityVolumeChartPoint(current4.Value));
				}
			}
			if (state.UpdatedVWAP != null)
			{
				foreach (System.Collections.Generic.KeyValuePair<string, decimal> current5 in state.UpdatedVWAP)
				{
					Game.State.Securities[current5.Key].VWAP = current5.Value;
				}
			}
			if (state.UpdatedNLV.HasValue)
			{
				Game.State.NLV = state.UpdatedNLV.Value;
			}
			if (state.UpdatedSecurityNLV != null)
			{
				foreach (System.Collections.Generic.KeyValuePair<string, decimal> current6 in state.UpdatedSecurityNLV)
				{
					Game.State.PortfolioTable.Rows.Find(current6.Key)["NLV"] = current6.Value;
				}
			}
			if (state.UpdatedOTC != null)
			{
				foreach (OTCUpdateMessage current7 in state.UpdatedOTC)
				{
					if (current7.Status == OTCStatus.PENDING)
					{
						StateManager.AddOTCOrder(current7.ID, current7.TraderID, current7.Target, current7.Ticker, current7.Volume, current7.Price, current7.SettlePeriod, current7.SettleTick);
					}
					else
					{
						Game.State.OTCOrderTable.Rows.Find(current7.ID)["Status"] = current7.Status;
					}
				}
			}
			if (state.UpdatedElectricity != null)
			{
				foreach (Tuple<int, double> current8 in state.UpdatedElectricity)
				{
					Game.State.ElectricityCharts[current8.Item1].AddPoint(current8.Item2);
				}
			}
		}
		public static void MergeTransactions(TransactionAddMessage[] transactions)
		{
			Game.State.TransactionTable.BeginLoadData();
			for (int i = 0; i < transactions.Length; i++)
			{
				TransactionAddMessage transactionAddMessage = transactions[i];
				if (string.IsNullOrWhiteSpace(transactionAddMessage.Currency))
				{
					Game.State.Accounts[Game.State.DefaultCurrency].Balance += transactionAddMessage.Value;
				}
				DataRow dataRow = Game.State.TransactionTable.NewRow();
				dataRow["Period"] = Game.State.Current.Period;
				dataRow["Tick"] = Game.State.Current.Tick;
				dataRow["Timestamp"] = System.DateTime.Now;
				dataRow["Ticker"] = transactionAddMessage.Ticker;
				if (transactionAddMessage.Price.HasValue)
				{
					dataRow["Price"] = transactionAddMessage.Price.Value;
				}
				if (transactionAddMessage.Quantity.HasValue)
				{
					dataRow["Quantity"] = transactionAddMessage.Quantity.Value;
				}
				dataRow["Type"] = transactionAddMessage.Type;
				dataRow["Value"] = transactionAddMessage.Value;
				dataRow["Currency"] = transactionAddMessage.Currency;
				dataRow["Balance"] = transactionAddMessage.Balance;
				Game.State.TransactionTable.Rows.Add(dataRow);
			}
			Game.State.TransactionTable.EndLoadData();
		}
		public static void AddOrders(OrderAddMessage[] neworders)
		{
			Game.State.OrderHistoryTable.BeginLoadData();
			Game.State.OrderTable.BeginLoadData();
			for (int i = 0; i < neworders.Length; i++)
			{
				OrderAddMessage orderAddMessage = neworders[i];
				if (orderAddMessage.Type == OrderType.LIMIT && orderAddMessage.VolumeRemaining > 0m)
				{
					DataRow dataRow = Game.State.OrderTable.NewRow();
					dataRow["ID"] = orderAddMessage.ID;
					dataRow["TraderID"] = orderAddMessage.TraderID;
					dataRow["Price"] = orderAddMessage.Price;
					dataRow["Volume"] = orderAddMessage.Volume;
					dataRow["VolumeRemaining"] = orderAddMessage.VolumeRemaining;
					dataRow["Type"] = orderAddMessage.Type;
					dataRow["VWAP"] = orderAddMessage.VWAP;
					dataRow["Period"] = Game.State.Current.Period;
					dataRow["Tick"] = Game.State.Current.Tick;
					dataRow["Timestamp"] = System.DateTime.Now;
					dataRow["Ticker"] = orderAddMessage.Ticker;
					dataRow["ColorState"] = 2;
					Game.State.OrderTable.Rows.Add(dataRow);
					ColorManager.AddOrderRow(dataRow);
					Game.State.Securities[orderAddMessage.Ticker].UpdateLadder((decimal)dataRow["Price"], orderAddMessage.VolumeRemaining * System.Math.Sign(orderAddMessage.Volume), orderAddMessage.TraderID == Game.State.Trader.TraderID);
					if (orderAddMessage.TraderID == Game.State.Trader.TraderID)
					{
						Game.State.Securities[orderAddMessage.Ticker].OpenLimitOrders++;
					}
				}
				else
				{
					if (orderAddMessage.TraderID == Game.State.Trader.TraderID)
					{
						DataRow dataRow2 = Game.State.OrderHistoryTable.NewRow();
						dataRow2["ID"] = orderAddMessage.ID;
						dataRow2["Ticker"] = orderAddMessage.Ticker;
						if (orderAddMessage.Type == OrderType.LIMIT)
						{
							dataRow2["Price"] = orderAddMessage.Price;
						}
						dataRow2["Volume"] = orderAddMessage.Volume;
						dataRow2["VolumeRemaining"] = orderAddMessage.VolumeRemaining;
						dataRow2["Type"] = orderAddMessage.Type;
						dataRow2["VWAP"] = orderAddMessage.VWAP;
						dataRow2["Period"] = Game.State.Current.Period;
						dataRow2["Tick"] = Game.State.Current.Tick;
						dataRow2["Timestamp"] = System.DateTime.Now;
						dataRow2["Status"] = OrderStatus.TRANSACTED;
						Game.State.OrderHistoryTable.Rows.Add(dataRow2);
					}
				}
			}
			Game.State.OrderTable.EndLoadData();
			Game.State.OrderHistoryTable.EndLoadData();
		}
		public static void UpdateOrders(OrderUpdateMessage[] orderchanges)
		{
			Game.State.OrderTable.BeginLoadData();
			for (int i = 0; i < orderchanges.Length; i++)
			{
				OrderUpdateMessage orderUpdateMessage = orderchanges[i];
				DataRow dataRow = Game.State.OrderTable.Rows.Find(orderUpdateMessage.ID);
				if (dataRow != null)
				{
					decimal value = (decimal)dataRow["Volume"];
					decimal num = ((decimal)dataRow["VolumeRemaining"] - orderUpdateMessage.VolumeRemaining) * System.Math.Sign(value);
					string text = string.IsNullOrEmpty(orderUpdateMessage.TraderID) ? "ANON" : orderUpdateMessage.TraderID;
					dataRow["VWAP"] = MathHelper.CalculateVWAP((decimal)dataRow["VWAP"], System.Math.Abs(value) - (decimal)dataRow["VolumeRemaining"], System.Math.Abs(num), (decimal)dataRow["Price"]);
					dataRow["VolumeRemaining"] = orderUpdateMessage.VolumeRemaining;
					dataRow["ColorState"] = 1;
					ColorManager.AddOrderRow(dataRow);
					Game.State.Securities[(string)dataRow["Ticker"]].UpdateLadder((decimal)dataRow["Price"], num * -1m, (string)dataRow["TraderID"] == Game.State.Trader.TraderID);
					Game.State.TimeSalesTable.Rows.Add(new object[]
					{
						null,
						Game.State.Current.Period,
						Game.State.Current.Tick,
						dataRow["Ticker"],
						dataRow["Price"],
						System.Math.Abs(num),
						Game.State.General.IsAnonymousTrading ? "ANON/ANON" : string.Format("{0}/{1}", (System.Math.Sign(value) > 0) ? dataRow["TraderID"] : text, (System.Math.Sign(value) > 0) ? text : dataRow["TraderID"])
					});
				}
			}
			Game.State.OrderTable.EndLoadData();
		}
		public static void UpdateTradingLimits(TradingLimitUpdateMessage item)
		{
			DataRow dataRow = Game.State.TradingLimitTable.Rows.Find(item.Name);
			if (dataRow != null)
			{
				if (item.Gross > (decimal)dataRow["Gross"] && item.Gross > Game.State.RiskTypes[item.Name].GrossRiskLimit && (decimal)dataRow["Gross"] <= Game.State.RiskTypes[item.Name].GrossRiskLimit)
				{
					((Client)ThreadHelper.MainThread).ShowInfo("Trading Limit Warning", string.Format("Gross limits in risk category {0} exceeded.", item.Name));
				}
				if (System.Math.Abs(item.Net) > System.Math.Abs((decimal)dataRow["Net"]) && System.Math.Abs(item.Net) > Game.State.RiskTypes[item.Name].NetRiskLimit && System.Math.Abs((decimal)dataRow["Net"]) <= Game.State.RiskTypes[item.Name].NetRiskLimit)
				{
					((Client)ThreadHelper.MainThread).ShowInfo("Trading Limit Warning", string.Format("Net limits in risk category {0} exceeded.", item.Name));
				}
				Game.State.TradingLimitTable.BeginLoadData();
				dataRow["Gross"] = item.Gross;
				dataRow["Net"] = item.Net;
				Game.State.TradingLimitTable.EndLoadData();
			}
		}
		public static void UpdatePortfolio(PortfolioUpdateMessage item)
		{
			DataRow dataRow = Game.State.PortfolioTable.Rows.Find(item.Ticker);
			if (dataRow != null)
			{
				Game.State.PortfolioTable.BeginLoadData();
				item.SetRow(dataRow);
				Game.State.PortfolioTable.EndLoadData();
			}
			if (item.UpdatedContainments != null)
			{
				StateManager.UpdateAsset(item.UpdatedContainments);
			}
		}
		public static void CancelOrder(int[] orderids)
		{
			Game.State.OrderTable.BeginLoadData();
			Game.State.OrderHistoryTable.BeginLoadData();
			for (int i = 0; i < orderids.Length; i++)
			{
				int num = orderids[i];
				DataRow dataRow = Game.State.OrderTable.Rows.Find(num);
				if (dataRow != null)
				{
					Game.State.Securities[(string)dataRow["Ticker"]].UpdateLadder((decimal)dataRow["Price"], (decimal)dataRow["VolumeRemaining"] * System.Math.Sign((decimal)dataRow["Volume"] * -1m), (string)dataRow["TraderID"] == Game.State.Trader.TraderID);
					StateManager.ArchiveOrder(dataRow, OrderStatus.CANCELLED);
				}
			}
			Game.State.OrderTable.EndLoadData();
			Game.State.OrderHistoryTable.EndLoadData();
		}
		public static void ArchiveOrder(DataRow row, OrderStatus reason)
		{
			if ((string)row["TraderID"] == Game.State.Trader.TraderID)
			{
				DataRow dataRow = Game.State.OrderHistoryTable.NewRow();
				dataRow["ID"] = row["ID"];
				dataRow["Ticker"] = row["Ticker"];
				dataRow["Price"] = row["Price"];
				dataRow["Volume"] = row["Volume"];
				dataRow["VolumeRemaining"] = row["VolumeRemaining"];
				dataRow["Type"] = row["Type"];
				dataRow["VWAP"] = row["VWAP"];
				dataRow["Period"] = Game.State.Current.Period;
				dataRow["Tick"] = Game.State.Current.Tick;
				dataRow["Timestamp"] = System.DateTime.Now;
				dataRow["Status"] = reason;
				Game.State.OrderHistoryTable.Rows.Add(dataRow);
				Game.State.Securities[(string)row["Ticker"]].OpenLimitOrders--;
			}
			Game.State.OrderTable.Rows.Remove(row);
		}
		public static void AddArchiveOrder(int id, string ticker, decimal price, decimal volume, OrderType type, int period, int tick, OrderStatus status)
		{
			DataRow dataRow = Game.State.OrderHistoryTable.NewRow();
			dataRow["ID"] = id;
			dataRow["Ticker"] = ticker;
			dataRow["Price"] = price;
			dataRow["Volume"] = volume;
			dataRow["VolumeRemaining"] = 0;
			dataRow["Type"] = type;
			dataRow["VWAP"] = price;
			dataRow["Period"] = period;
			dataRow["Tick"] = tick;
			dataRow["Timestamp"] = System.DateTime.Now;
			dataRow["Status"] = status;
			Game.State.OrderHistoryTable.Rows.Add(dataRow);
		}
		public static void RemoveAsset(int[] ids)
		{
			for (int i = 0; i < ids.Length; i++)
			{
				int num = ids[i];
				Game.State.AssetTable.Rows.Remove(Game.State.AssetTable.Rows.Find(num));
			}
		}
		public static void UpdateAssetLeaseCount(string ticker, int leasecount)
		{
			DataRow dataRow = Game.State.AssetInfoTable.Rows.Find(ticker);
			if (dataRow != null)
			{
				dataRow["LeaseCount"] = leasecount;
			}
		}
		public static void UpdateAsset(AssetUpdateMessage[] items)
		{
			for (int i = 0; i < items.Length; i++)
			{
				AssetUpdateMessage assetUpdateMessage = items[i];
				DataRow dataRow = Game.State.AssetTable.Rows.Find(assetUpdateMessage.ID);
				if (dataRow != null)
				{
					assetUpdateMessage.SetRow(dataRow);
				}
				else
				{
					dataRow = Game.State.AssetTable.NewRow();
					assetUpdateMessage.SetRow(dataRow);
					Game.State.AssetTable.Rows.Add(dataRow);
				}
				Game.State.AssetInfoTable.Rows.Find(dataRow["Ticker"])["Realized"] = assetUpdateMessage.Realized;
			}
		}
		public static void UpdateAsset(ContainmentUpdateMessage[] items)
		{
			for (int i = 0; i < items.Length; i++)
			{
				ContainmentUpdateMessage containmentUpdateMessage = items[i];
				DataRow dataRow = Game.State.AssetTable.Rows.Find(containmentUpdateMessage.ID);
				if (dataRow != null)
				{
					dataRow["ContainmentUsage"] = containmentUpdateMessage.Containment;
				}
			}
		}
		public static void UpdateNews(int id, string ticker, string headline, string body)
		{
			DataRow dataRow = Game.State.NewsTable.NewRow();
			dataRow["ID"] = id;
			dataRow["Period"] = Game.State.Current.Period;
			dataRow["Tick"] = Game.State.Current.Tick;
			dataRow["Ticker"] = ticker;
			dataRow["Headline"] = headline;
			dataRow["Body"] = body;
			dataRow["IsRead"] = false;
			dataRow["ColorState"] = 1;
			Game.State.NewsTable.Rows.Add(dataRow);
			ColorManager.AddNewsRow(dataRow);
			SystemSounds.Exclamation.Play();
		}
		public static void AddOTCOrder(int id, string traderid, string target, string ticker, decimal volume, decimal price, int? settleperiod, int? settletick)
		{
			DataRow dataRow = Game.State.OTCOrderTable.NewRow();
			dataRow["ID"] = id;
			dataRow["TraderID"] = traderid;
			dataRow["Target"] = target;
			dataRow["Period"] = Game.State.Current.Period;
			dataRow["Tick"] = Game.State.Current.Tick;
			dataRow["Ticker"] = ticker;
			dataRow["Price"] = price;
			dataRow["Volume"] = volume;
			if (settleperiod.HasValue && settletick.HasValue)
			{
				dataRow["SettlePeriod"] = settleperiod.Value;
				dataRow["SettleTick"] = settletick.Value;
			}
			dataRow["Status"] = OTCStatus.PENDING;
			Game.State.OTCOrderTable.Rows.Add(dataRow);
			if (target == Game.State.Trader.TraderID && Settings.Default.IsOTCAutoAccept)
			{
				try
				{
					ServiceManager.Execute(delegate(IClientService p)
					{
						p.UpdateOTCOrder(id, OTCStatus.ACCEPTED);
					});
					((Client)ThreadHelper.MainThread).ShowInfo("OTC Order Transacted", string.Format("Accepted order from {3} to {0} {1} units of {2}", new object[]
					{
						(volume > 0m) ? "SELL" : "BUY",
						System.Math.Abs(volume),
						ticker,
						traderid
					}));
				}
				catch (FaultException ex)
				{
					DialogHelper.ShowError(ex.Message, "Error");
				}
			}
		}
	}
}
