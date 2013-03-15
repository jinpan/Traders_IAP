using ProtoBuf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
namespace TTS
{
	internal class Game : INotifyPropertyChanged
	{
		public static Action Resetting = delegate
		{
		};
		public static Action Reset = delegate
		{
		};
		public static Action VariablesUpdated = delegate
		{
		};
		private static Game _State = null;
		private decimal _NLV;
		private int _UnreadNewsCount;
		private int _PendingOTCCount;
		private int _UnreadChatCount;
		public BindingList<string> ActiveSecurities = new BindingList<string>();
		public BindingList<string> ActiveOTCableSecuritiesAndAssets = new BindingList<string>();
		public BindingList<string> ActiveTradableSecurities = new BindingList<string>();
		public BindingList<string> ActiveTransportationAssets = new BindingList<string>();
		public UpdateableDataView OpenOrderView;
		public UpdateableDataView ArchivedOrderView;
		public UpdateableDataView TenderView;
		public UpdateableDataView EndowmentView;
		public UpdateableDataView TransactionView;
		public UpdateableDataView AssetContainerView;
		public UpdateableDataView AssetTransporterView;
		public UpdateableDataView AssetConverterView;
		public UpdateableDataView AssetProducerView;
		public UpdateableDataView MarketView;
		public UpdateableDataView NewsView;
		public UpdateableDataView NewsUnreadView;
		public UpdateableDataView OTCCompletedOrderView;
		public UpdateableDataView OTCPendingOrderView;
		public GameParameters Current = new GameParameters();
		public GeneralParameters General = new GeneralParameters();
		public TraderParameters Trader = new TraderParameters();
		public CaseInsensitiveDictionary<string, SecurityItem> Securities = new CaseInsensitiveDictionary<string, SecurityItem>(System.StringComparer.OrdinalIgnoreCase);
		public CaseInsensitiveDictionary<string, CurrencyParameters> Currencies = new CaseInsensitiveDictionary<string, CurrencyParameters>(System.StringComparer.OrdinalIgnoreCase);
		public CaseInsensitiveDictionary<string, AccountItem> Accounts = new CaseInsensitiveDictionary<string, AccountItem>(System.StringComparer.OrdinalIgnoreCase);
		public CaseInsensitiveDictionary<string, SpreadParameters> Spreads = new CaseInsensitiveDictionary<string, SpreadParameters>(System.StringComparer.OrdinalIgnoreCase);
		public TraderTypeParameters TraderType = new TraderTypeParameters();
		public string DefaultCurrency = "";
		public CaseInsensitiveDictionary<string, RiskTypeParameters> RiskTypes = new CaseInsensitiveDictionary<string, RiskTypeParameters>();
		public ChartItem<TraderChartPoint> TraderChart;
		public System.Collections.Generic.List<ElectricityChart> ElectricityCharts = new System.Collections.Generic.List<ElectricityChart>();
		public event PropertyChangedEventHandler PropertyChanged;
		public static Game State
		{
			get
			{
				return Game._State;
			}
			set
			{
				ThreadHelper.MainThread.InvokeIfRequired(delegate
				{
					Game.Resetting();
					Game._State = value;
					GameManager.UpdateStatus(Game._State.Current);
					if (TTSFormManager.Instance != null)
					{
						TTSFormManager.Instance.EnforceAllowedWindows();
					}
					Game.Reset();
				});
			}
		}
		public decimal NLV
		{
			get
			{
				return this._NLV;
			}
			set
			{
				this.TraderChart.AddPoint(this.Current.Period, this.Current.Tick, new TraderChartPoint(value, this.Accounts[this.DefaultCurrency].Balance, 0m));
				if (value != this._NLV)
				{
					this._NLV = value;
					this.RaisePropertyChangedEvent("NLV");
				}
			}
		}
		public int UnreadNewsCount
		{
			get
			{
				return this._UnreadNewsCount;
			}
			set
			{
				if (value != this._UnreadNewsCount)
				{
					this._UnreadNewsCount = value;
					this.RaisePropertyChangedEvent("UnreadNewsCount");
				}
			}
		}
		public int PendingOTCCount
		{
			get
			{
				return this._PendingOTCCount;
			}
			set
			{
				if (value != this._PendingOTCCount)
				{
					this._PendingOTCCount = value;
					this.RaisePropertyChangedEvent("PendingOTCCount");
				}
			}
		}
		public int UnreadChatCount
		{
			get
			{
				return this._UnreadChatCount;
			}
			set
			{
				if (value != this._UnreadChatCount)
				{
					this._UnreadChatCount = value;
					this.RaisePropertyChangedEvent("UnreadChatCount");
				}
			}
		}
		public DataTable TradingLimitTable
		{
			get;
			set;
		}
		public DataTable OrderHistoryTable
		{
			get;
			set;
		}
		public DataTable PortfolioTable
		{
			get;
			set;
		}
		public DataTable OrderTable
		{
			get;
			set;
		}
		public DataSet Assets
		{
			get;
			set;
		}
		public DataTable AssetInfoTable
		{
			get;
			set;
		}
		public DataTable AssetTable
		{
			get;
			set;
		}
		public DataTable TransactionTable
		{
			get;
			set;
		}
		public DataTable NewsTable
		{
			get;
			set;
		}
		public DataTable OTCOrderTable
		{
			get;
			set;
		}
		public DataTable TimeSalesTable
		{
			get;
			set;
		}
		public Game()
		{
			DataTable dataTable = new DataTable("TradingLimitTable");
			dataTable.Columns.Add("Name", typeof(string));
			dataTable.PrimaryKey = new DataColumn[]
			{
				dataTable.Columns["Name"]
			};
			dataTable.Columns.Add("Gross", typeof(decimal));
			dataTable.Columns.Add("Net", typeof(decimal));
			this.TradingLimitTable = dataTable;
			dataTable = new DataTable("PortfolioTable");
			dataTable.Columns.Add("Ticker", typeof(string));
			dataTable.PrimaryKey = new DataColumn[]
			{
				dataTable.Columns["Ticker"]
			};
			dataTable.Columns.Add("Position", typeof(decimal));
			dataTable.Columns.Add("VWAP", typeof(decimal));
			dataTable.Columns.Add("UnitMultiplier", typeof(int));
			dataTable.Columns.Add("FutureMultiplier", typeof(int));
			dataTable.Columns.Add("Type", typeof(SecurityType));
			dataTable.Columns.Add("Realized", typeof(decimal));
			dataTable.Columns.Add("Bid", typeof(decimal));
			dataTable.Columns.Add("Ask", typeof(decimal));
			dataTable.Columns.Add("BidSize", typeof(int));
			dataTable.Columns.Add("AskSize", typeof(int));
			dataTable.Columns.Add("Last", typeof(decimal));
			dataTable.Columns.Add("NLV", typeof(decimal));
			dataTable.Columns.Add("Unrealized", typeof(decimal), "IIF(FutureMultiplier=0,NLV,NLV-Position*VWAP*UnitMultiplier)");
			dataTable.Columns.Add("Volume", typeof(decimal));
			dataTable.Columns.Add("SecurityVWAP", typeof(decimal));
			this.PortfolioTable = dataTable;
			dataTable = new DataTable("OrderTable");
			dataTable.Columns.Add("ID", typeof(int));
			dataTable.PrimaryKey = new DataColumn[]
			{
				dataTable.Columns["ID"]
			};
			dataTable.Columns.Add("TraderID", typeof(string));
			dataTable.Columns.Add("Price", typeof(decimal));
			dataTable.Columns.Add("Volume", typeof(decimal));
			dataTable.Columns.Add("VolumeRemaining", typeof(decimal));
			dataTable.Columns.Add("Type", typeof(OrderType));
			dataTable.Columns.Add("VWAP", typeof(decimal));
			dataTable.Columns.Add("Period", typeof(int));
			dataTable.Columns.Add("Tick", typeof(int));
			dataTable.Columns.Add("Timestamp", typeof(System.DateTime));
			dataTable.Columns.Add("Ticker", typeof(string));
			dataTable.Columns.Add(new DataColumn("ColorState", typeof(int))
			{
				DefaultValue = 0
			});
			this.OrderTable = dataTable;
			dataTable = new DataTable("OrderHistoryTable");
			dataTable.Columns.Add("ID", typeof(int));
			dataTable.PrimaryKey = new DataColumn[]
			{
				dataTable.Columns["ID"]
			};
			dataTable.Columns.Add("Ticker", typeof(string));
			dataTable.Columns.Add("Price", typeof(decimal));
			dataTable.Columns.Add("Volume", typeof(decimal));
			dataTable.Columns.Add("VolumeRemaining", typeof(decimal));
			dataTable.Columns.Add("Type", typeof(OrderType));
			dataTable.Columns.Add("VWAP", typeof(decimal));
			dataTable.Columns.Add("Period", typeof(int));
			dataTable.Columns.Add("Tick", typeof(int));
			dataTable.Columns.Add("Timestamp", typeof(System.DateTime));
			dataTable.Columns.Add("Status", typeof(OrderStatus));
			this.OrderHistoryTable = dataTable;
			dataTable = new DataTable("TransactionTable");
			dataTable.Columns.Add("ID", typeof(int));
			dataTable.Columns["ID"].AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[]
			{
				dataTable.Columns["ID"]
			};
			dataTable.Columns.Add("Period", typeof(int));
			dataTable.Columns.Add("Tick", typeof(int));
			dataTable.Columns.Add("Timestamp", typeof(System.DateTime));
			dataTable.Columns.Add("Ticker", typeof(string));
			dataTable.Columns.Add("Price", typeof(decimal));
			dataTable.Columns.Add("Quantity", typeof(decimal));
			dataTable.Columns.Add("Currency", typeof(string));
			dataTable.Columns.Add("Type", typeof(TransactionType));
			dataTable.Columns.Add("Value", typeof(decimal));
			dataTable.Columns.Add("Balance", typeof(decimal));
			this.TransactionTable = dataTable;
			dataTable = new System.Collections.Generic.List<AssetParameters>().ToDataTable("AssetInfoTable");
			dataTable.PrimaryKey = new DataColumn[]
			{
				dataTable.Columns["Ticker"]
			};
			dataTable.Columns.Add("LeaseCount", typeof(int));
			dataTable.Columns.Add("Realized", typeof(decimal));
			this.AssetInfoTable = dataTable;
			dataTable = new System.Collections.Generic.List<AssetUpdateMessage>().ToDataTable("AssetTable");
			dataTable.PrimaryKey = new DataColumn[]
			{
				dataTable.Columns["ID"]
			};
			this.AssetTable = dataTable;
			this.Assets = new DataSet();
			this.Assets.Tables.Add(this.AssetInfoTable);
			this.Assets.Tables.Add(this.AssetTable);
			this.Assets.Relations.Add(this.AssetInfoTable.Columns["Ticker"], this.AssetTable.Columns["Ticker"]);
			dataTable = new DataTable("NewsTable");
			dataTable.Columns.Add("ID", typeof(int));
			dataTable.Columns["ID"].AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[]
			{
				dataTable.Columns["ID"]
			};
			dataTable.Columns.Add("Period", typeof(int));
			dataTable.Columns.Add("Tick", typeof(int));
			dataTable.Columns.Add("Ticker", typeof(string));
			dataTable.Columns.Add("Headline", typeof(string));
			dataTable.Columns.Add("Body", typeof(string));
			dataTable.Columns.Add(new DataColumn("IsRead", typeof(bool))
			{
				DefaultValue = false
			});
			dataTable.Columns.Add("ColorState", typeof(int));
			this.NewsTable = dataTable;
			dataTable = new DataTable("TimeSalesTable");
			dataTable.Columns.Add("ID", typeof(int));
			dataTable.Columns["ID"].AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[]
			{
				dataTable.Columns["ID"]
			};
			dataTable.Columns.Add("Period", typeof(int));
			dataTable.Columns.Add("Tick", typeof(int));
			dataTable.Columns.Add("Ticker", typeof(string));
			dataTable.Columns.Add("Price", typeof(decimal));
			dataTable.Columns.Add("Volume", typeof(decimal));
			dataTable.Columns.Add("Buyer/Seller", typeof(string));
			dataTable.Columns.Add("Value", typeof(decimal), "Price*Volume");
			this.TimeSalesTable = dataTable;
			dataTable = new DataTable("OTCOrderTable");
			dataTable.Columns.Add("ID", typeof(int));
			dataTable.PrimaryKey = new DataColumn[]
			{
				dataTable.Columns["ID"]
			};
			dataTable.Columns.Add("TraderID", typeof(string));
			dataTable.Columns.Add("Target", typeof(string));
			dataTable.Columns.Add("Period", typeof(int));
			dataTable.Columns.Add("Tick", typeof(int));
			dataTable.Columns.Add("Ticker", typeof(string));
			dataTable.Columns.Add("Volume", typeof(decimal));
			dataTable.Columns.Add("Price", typeof(decimal));
			dataTable.Columns.Add("SettlePeriod", typeof(int));
			dataTable.Columns.Add("SettleTick", typeof(int));
			dataTable.Columns.Add("Status", typeof(OTCStatus));
			this.OTCOrderTable = dataTable;
			this.OpenOrderView = new UpdateableDataView(this.OrderTable, "", "Period DESC, Tick DESC, ID DESC", DataViewRowState.CurrentRows);
			this.ArchivedOrderView = new UpdateableDataView(this.OrderHistoryTable, string.Format("Type={0} OR Type={1}", 1, 0), "Period DESC, Tick DESC, ID DESC", DataViewRowState.CurrentRows);
			this.TenderView = new UpdateableDataView(this.OrderHistoryTable, string.Format("Type={0}", 2), "Period DESC, Tick DESC, ID DESC", DataViewRowState.CurrentRows);
			this.EndowmentView = new UpdateableDataView(this.OrderHistoryTable, string.Format("Type={0}", 3), "Period DESC, Tick DESC, ID DESC", DataViewRowState.CurrentRows);
			this.TransactionView = new UpdateableDataView(this.TransactionTable, "", "Period DESC, Tick DESC, ID DESC", DataViewRowState.CurrentRows);
			this.AssetContainerView = new UpdateableDataView(this.AssetInfoTable, string.Format("Type={0}", 0), "Ticker ASC", DataViewRowState.CurrentRows);
			this.AssetTransporterView = new UpdateableDataView(this.AssetInfoTable, string.Format("Type={0} OR Type={1}", 1, 2), "Ticker ASC", DataViewRowState.CurrentRows);
			this.AssetConverterView = new UpdateableDataView(this.AssetInfoTable, string.Format("Type={0} OR Type={1}", 3, 4), "Ticker ASC", DataViewRowState.CurrentRows);
			this.AssetProducerView = new UpdateableDataView(this.AssetInfoTable, string.Format("Type={0}", 5), "Ticker ASC", DataViewRowState.CurrentRows);
			this.MarketView = new UpdateableDataView(this.PortfolioTable);
			this.OTCCompletedOrderView = new UpdateableDataView(this.OTCOrderTable, string.Format("Status<>{0}", 0), "Period DESC, Tick DESC", DataViewRowState.CurrentRows);
			this.OTCPendingOrderView = new UpdateableDataView(this.OTCOrderTable, string.Format("Status={0}", 0), "Period DESC, Tick DESC", DataViewRowState.CurrentRows);
			this.NewsView = new UpdateableDataView(this.NewsTable, "", "Period DESC, Tick DESC, ID DESC", DataViewRowState.CurrentRows);
			this.NewsUnreadView = new UpdateableDataView(this.NewsTable, "IsRead=0", "", DataViewRowState.CurrentRows);
			this.NewsUnreadView.ListChanged += delegate(object sender, ListChangedEventArgs e)
			{
				this.UnreadNewsCount = ((DataView)sender).Count;
			};
			this.OTCPendingOrderView.ListChanged += delegate(object sender, ListChangedEventArgs e)
			{
				this.PendingOTCCount = ((DataView)sender).Count;
			};
		}
		public void Sync(SyncState state)
		{
			this.ActiveTradableSecurities.Clear();
			this.ActiveTransportationAssets.Clear();
			this.ActiveSecurities.Clear();
			this.Securities.Clear();
			this.Accounts.Clear();
			this.Currencies.Clear();
			this.TradingLimitTable.Clear();
			this.PortfolioTable.Clear();
			this.Spreads.Clear();
			this.RiskTypes.Clear();
			this.PropertyChanged = null;
			this.Assets.Clear();
			this.OrderTable.Clear();
			this.TimeSalesTable.Clear();
			this.ElectricityCharts.Clear();
			this._NLV = 0m;
			state.Current.CopyTo(this.Current);
			state.General.CopyTo(this.General);
			state.TraderType.CopyTo(this.TraderType);
			state.Trader.CopyTo(this.Trader);
			this.OrderHistoryTable.ClearAndMerge(new ProtoTable(state.OrderHistoryTable).Table);
			this.TransactionTable.ClearAndMerge(new ProtoTable(state.TransactionTable).Table);
			this.OrderTable.BeginLoadData();
			if (state.Securities != null)
			{
				SecurityParameters[] securities = state.Securities;
				for (int i = 0; i < securities.Length; i++)
				{
					SecurityParameters securityParameters = securities[i];
					this.OrderTable.Columns["Ticker"].DefaultValue = securityParameters.Ticker;
					DataTable table = new ProtoTable(state.OrderTables[securityParameters.Ticker]).Table;
					int num = 0;
					foreach (DataRow dataRow in table.Rows)
					{
						if ((string)dataRow["TraderID"] == this.Trader.TraderID)
						{
							num++;
						}
						this.OrderTable.Rows.Add(dataRow.ItemArray);
					}
					using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(state.SecurityCharts[securityParameters.Ticker]))
					{
						using (System.IO.MemoryStream memoryStream2 = new System.IO.MemoryStream(state.VolumeCharts[securityParameters.Ticker]))
						{
							this.Securities.Add(securityParameters.Ticker, new SecurityItem(securityParameters, state.Trader.TraderID, this.OrderTable, this.OrderHistoryTable)
							{
								HistoryChart = Serializer.Deserialize<ChartItem<SecurityChartPoint>>(memoryStream),
								VolumeChart = Serializer.Deserialize<ChartItem<SecurityVolumeChartPoint>>(memoryStream2)
							});
							this.Securities[securityParameters.Ticker].InitializeVolume();
							this.Securities[securityParameters.Ticker].InitializeLadder(table, this.Trader.TraderID);
							this.Securities[securityParameters.Ticker].OpenLimitOrders = num;
						}
					}
				}
				this.OrderTable.Columns["Ticker"].DefaultValue = System.DBNull.Value;
			}
			this.OrderTable.EndLoadData();
			if (state.Accounts != null)
			{
				foreach (System.Collections.Generic.KeyValuePair<string, decimal> current in state.Accounts)
				{
					this.Accounts.Add(current.Key, new AccountItem
					{
						Balance = current.Value
					});
				}
			}
			if (state.CurrencyParameters != null)
			{
				CurrencyParameters[] currencyParameters = state.CurrencyParameters;
				for (int j = 0; j < currencyParameters.Length; j++)
				{
					CurrencyParameters currencyParameters2 = currencyParameters[j];
					this.Currencies.Add(currencyParameters2.Ticker, currencyParameters2);
				}
			}
			if (state.RiskTypes != null)
			{
				RiskTypeParameters[] riskTypes = state.RiskTypes;
				for (int k = 0; k < riskTypes.Length; k++)
				{
					RiskTypeParameters riskTypeParameters = riskTypes[k];
					this.RiskTypes.Add(riskTypeParameters.Name, riskTypeParameters);
				}
			}
			if (state.TradingLimits != null)
			{
				TradingLimitUpdateMessage[] tradingLimits = state.TradingLimits;
				for (int l = 0; l < tradingLimits.Length; l++)
				{
					TradingLimitUpdateMessage tradingLimitUpdateMessage = tradingLimits[l];
					this.TradingLimitTable.Rows.Add(new object[]
					{
						tradingLimitUpdateMessage.Name,
						tradingLimitUpdateMessage.Gross,
						tradingLimitUpdateMessage.Net
					});
				}
			}
			if (state.SecurityPortfolio != null)
			{
				PortfolioUpdateMessage[] securityPortfolio = state.SecurityPortfolio;
				for (int m = 0; m < securityPortfolio.Length; m++)
				{
					PortfolioUpdateMessage portfolioUpdateMessage = securityPortfolio[m];
					this.PortfolioTable.Rows.Add(new object[]
					{
						portfolioUpdateMessage.Ticker,
						portfolioUpdateMessage.Position,
						portfolioUpdateMessage.VWAP,
						this.Securities[portfolioUpdateMessage.Ticker].Parameters.UnitMultiplier,
						(this.Securities[portfolioUpdateMessage.Ticker].Parameters.Type == SecurityType.FUTURE) ? 0 : 1,
						this.Securities[portfolioUpdateMessage.Ticker].Parameters.Type
					});
				}
			}
			if (state.Spreads != null)
			{
				SpreadParameters[] spreads = state.Spreads;
				for (int n = 0; n < spreads.Length; n++)
				{
					SpreadParameters spreadParameters = spreads[n];
					this.Spreads.Add(spreadParameters.Ticker, spreadParameters);
				}
			}
			this.AssetInfoTable.ClearAndMerge(new System.Collections.Generic.List<AssetParameters>(state.Assets ?? new AssetParameters[0]).ToDataTable(""));
			if (state.AssetLeaseCount != null)
			{
				AssetItemUpdateMessage[] assetLeaseCount = state.AssetLeaseCount;
				for (int num2 = 0; num2 < assetLeaseCount.Length; num2++)
				{
					AssetItemUpdateMessage assetItemUpdateMessage = assetLeaseCount[num2];
					this.AssetInfoTable.Rows.Find(assetItemUpdateMessage.Ticker)["LeaseCount"] = assetItemUpdateMessage.LeaseCount;
				}
			}
			this.AssetTable.ClearAndMerge(new System.Collections.Generic.List<AssetUpdateMessage>(state.AssetPortfolio ?? new AssetUpdateMessage[0]).ToDataTable(""));
			this.OTCOrderTable.ClearAndMerge(new ProtoTable(state.OTCOrderTable).Table);
			if (state.Attribution != null)
			{
				foreach (System.Collections.Generic.KeyValuePair<string, decimal> current2 in state.Attribution)
				{
					if (this.Securities.ContainsKey(current2.Key))
					{
						this.PortfolioTable.Rows.Find(current2.Key)["Realized"] = current2.Value;
					}
					if (this.AssetInfoTable.Rows.Find(current2.Key) != null)
					{
						this.AssetInfoTable.Rows.Find(current2.Key)["Realized"] = current2.Value;
					}
				}
			}
			this.NewsTable.ClearAndMerge(new ProtoTable(state.NewsTable).Table);
			using (System.IO.MemoryStream memoryStream3 = new System.IO.MemoryStream(state.TraderChart))
			{
				this.TraderChart = Serializer.Deserialize<ChartItem<TraderChartPoint>>(memoryStream3);
			}
			if (state.ElectricityCharts != null)
			{
				this.ElectricityCharts.AddRange(state.ElectricityCharts);
			}
			foreach (SecurityItem current3 in this.Securities.Values)
			{
				string ticker = current3.Parameters.Ticker;
				current3.PropertyChanged += delegate(object sender, PropertyChangedEventArgs e)
				{
					if (e.PropertyName == "Volume")
					{
						this.PortfolioTable.Rows.Find(ticker)["Volume"] = ((SecurityItem)sender).Volume;
						return;
					}
					if (e.PropertyName == "Last")
					{
						this.PortfolioTable.Rows.Find(ticker)["Last"] = ((SecurityItem)sender).Last;
						return;
					}
					if (e.PropertyName == "Bid")
					{
						this.PortfolioTable.Rows.Find(ticker)["Bid"] = ((SecurityItem)sender).Bid;
						return;
					}
					if (e.PropertyName == "Ask")
					{
						this.PortfolioTable.Rows.Find(ticker)["Ask"] = ((SecurityItem)sender).Ask;
						return;
					}
					if (e.PropertyName == "BidSize")
					{
						this.PortfolioTable.Rows.Find(ticker)["BidSize"] = ((SecurityItem)sender).BidSize;
						return;
					}
					if (e.PropertyName == "AskSize")
					{
						this.PortfolioTable.Rows.Find(ticker)["AskSize"] = ((SecurityItem)sender).AskSize;
						return;
					}
					if (e.PropertyName == "VWAP")
					{
						this.PortfolioTable.Rows.Find(ticker)["SecurityVWAP"] = ((SecurityItem)sender).VWAP;
					}
				};
			}
			this.OpenOrderView.RowFilter = string.Format("VolumeRemaining<>0 AND TraderID='{0}'", this.Trader.TraderID.DataTableStringEscape());
			this.NewsUnreadView.RaiseUpdate();
			this.OTCPendingOrderView.RaiseUpdate();
			foreach (SecurityItem current4 in this.Securities.Values)
			{
				current4.BidView.RaiseUpdate();
				current4.AskView.RaiseUpdate();
			}
		}
		public void Update(GeneralParametersUpdate general, SecurityParametersUpdate security, AssetParametersUpdate assets, RiskTypeParametersUpdate tradinglimit)
		{
			if (general != null)
			{
				general.CopyTo(Game.State.General);
				TTSFormManager.Instance.EnforceAllowedWindows();
				((Client)ThreadHelper.MainThread).EnforceAllowedWindows();
			}
			if (security != null)
			{
				security.CopyTo(Game.State.Securities[security.Ticker].Parameters);
				GameManager.UpdateStatus(Game.State.Current);
			}
			if (assets != null)
			{
				DataTable dataTable = new System.Collections.Generic.List<AssetParametersUpdate>(new AssetParametersUpdate[]
				{
					assets
				}).ToDataTable("AssetInfoUpdateTable");
				DataRow dataRow = this.AssetInfoTable.Rows.Find(assets.Ticker);
				foreach (DataColumn dataColumn in dataTable.Columns)
				{
					dataRow[dataColumn.ColumnName] = dataTable.Rows[0][dataColumn.ColumnName];
				}
				Game.State.AssetContainerView.RaiseUpdate();
			}
			if (tradinglimit != null)
			{
				tradinglimit.CopyTo(Game.State.RiskTypes[tradinglimit.Name]);
			}
			if (general != null || security != null || assets != null || tradinglimit != null)
			{
				Game.VariablesUpdated();
			}
		}
		private void RaisePropertyChangedEvent(string info)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
	}
}
