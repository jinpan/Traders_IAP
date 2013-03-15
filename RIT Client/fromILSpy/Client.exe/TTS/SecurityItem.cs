using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
namespace TTS
{
	internal class SecurityItem : INotifyPropertyChanged
	{
		private decimal _Volume;
		private decimal _VWAP;
		private decimal _Last;
		private decimal _Ask;
		private decimal _AskSize;
		private decimal _Bid;
		private decimal _BidSize;
		public decimal BidChange;
		public decimal AskChange;
		public decimal LastChange;
		public ChartItem<SecurityChartPoint> HistoryChart;
		public ChartItem<SecurityVolumeChartPoint> VolumeChart;
		public event Action<decimal, decimal> LadderVolumeUpdated = delegate
		{
		};
		public event PropertyChangedEventHandler PropertyChanged;
		public decimal Increment
		{
			get
			{
				return new decimal(System.Math.Pow(10.0, (double)(-1 * this.Parameters.QuotedDecimals)));
			}
		}
		public SecurityParameters Parameters
		{
			get;
			set;
		}
		public System.Collections.Generic.Dictionary<decimal, decimal> LadderVolumes
		{
			get;
			set;
		}
		public System.Collections.Generic.Dictionary<decimal, decimal> LadderTraderVolumes
		{
			get;
			set;
		}
		public UpdateableDataView BidView
		{
			get;
			set;
		}
		public UpdateableDataView AskView
		{
			get;
			set;
		}
		public UpdateableDataView OpenOrderView
		{
			get;
			set;
		}
		public UpdateableDataView ArchivedOrderView
		{
			get;
			set;
		}
		public int OpenLimitOrders
		{
			get;
			set;
		}
		public decimal Volume
		{
			get
			{
				return this._Volume;
			}
			set
			{
				this._Volume = value;
				this.RaisePropertyChangedEvent("Volume");
			}
		}
		public decimal VWAP
		{
			get
			{
				return this._VWAP;
			}
			set
			{
				this._VWAP = value;
				this.RaisePropertyChangedEvent("VWAP");
			}
		}
		public decimal Last
		{
			get
			{
				return this._Last;
			}
			set
			{
				this.LastChange = value - this._Last;
				this._Last = value;
				this.HistoryChart.AddPoint(Game.State.Current.Period, Game.State.Current.Tick, new SecurityChartPoint(this._Last));
				this.RaisePropertyChangedEvent("Last");
			}
		}
		public decimal Ask
		{
			get
			{
				return this._Ask;
			}
			set
			{
				if (value != this._Ask)
				{
					this.AskChange = value - this._Ask;
					this._Ask = value;
					this.RaisePropertyChangedEvent("Ask");
				}
			}
		}
		public decimal AskSize
		{
			get
			{
				return this._AskSize;
			}
			set
			{
				if (value != this._AskSize)
				{
					this._AskSize = value;
					this.RaisePropertyChangedEvent("AskSize");
				}
			}
		}
		public decimal Bid
		{
			get
			{
				return this._Bid;
			}
			set
			{
				if (value != this._Bid)
				{
					this.BidChange = value - this._Bid;
					this._Bid = value;
					this.RaisePropertyChangedEvent("Bid");
				}
			}
		}
		public decimal BidSize
		{
			get
			{
				return this._BidSize;
			}
			set
			{
				if (value != this._BidSize)
				{
					this._BidSize = value;
					this.RaisePropertyChangedEvent("BidSize");
				}
			}
		}
		public void RemoveEventIfContained(Action<decimal, decimal> d)
		{
			if (this.LadderVolumeUpdated.GetInvocationList().Contains(d))
			{
				this.LadderVolumeUpdated -= d;
			}
		}
		public SecurityItem(SecurityParameters parameters, string traderid, DataTable ordertable, DataTable orderhistorytable)
		{
			this.Parameters = parameters;
			this.BidView = new UpdateableDataView(ordertable, string.Format("Ticker='{0}' AND Volume>0 AND VolumeRemaining>0", this.Parameters.Ticker), "Price DESC,ID ASC", DataViewRowState.CurrentRows);
			this.BidView.ListChanged += delegate(object sender, ListChangedEventArgs e)
			{
				if (e.NewIndex == 0)
				{
					DataView dataView = (DataView)sender;
					decimal num = 0m;
					this.Bid = ((dataView.Count > 0) ? ((decimal)dataView[0]["Price"]) : 0m);
					int num2 = 0;
					while (num2 < dataView.Count && (decimal)dataView[num2]["Price"] == this.Bid)
					{
						num += (decimal)dataView[num2]["VolumeRemaining"];
						num2++;
					}
					this.BidSize = num;
				}
			};
			this.AskView = new UpdateableDataView(ordertable, string.Format("Ticker='{0}' AND Volume<0 AND VolumeRemaining>0", this.Parameters.Ticker), "Price ASC,ID ASC", DataViewRowState.CurrentRows);
			this.AskView.ListChanged += delegate(object sender, ListChangedEventArgs e)
			{
				if (e.NewIndex == 0)
				{
					DataView dataView = (DataView)sender;
					decimal num = 0m;
					this.Ask = ((dataView.Count > 0) ? ((decimal)dataView[0]["Price"]) : 0m);
					int num2 = 0;
					while (num2 < dataView.Count && (decimal)dataView[num2]["Price"] == this.Ask)
					{
						num += (decimal)dataView[num2]["VolumeRemaining"];
						num2++;
					}
					this.AskSize = num;
				}
			};
			this.OpenOrderView = new UpdateableDataView(ordertable, string.Format("VolumeRemaining<>0 AND TraderID='{0}' AND Ticker='{1}'", traderid, this.Parameters.Ticker), "Period DESC, Tick DESC, ID DESC", DataViewRowState.CurrentRows);
			this.ArchivedOrderView = new UpdateableDataView(orderhistorytable, string.Format("Ticker='{2}' AND (Type={0} OR Type={1})", 1, 0, this.Parameters.Ticker), "Period DESC, Tick DESC, ID DESC", DataViewRowState.CurrentRows);
		}
		public void InitializeVolume()
		{
			this.Volume = this.VolumeChart.Data.Sum((SecurityVolumeChartPoint x) => (long)x.Volume);
		}
		public void InitializeLadder(DataTable ordertable, string traderid)
		{
			this.LadderVolumes = new System.Collections.Generic.Dictionary<decimal, decimal>();
			this.LadderTraderVolumes = new System.Collections.Generic.Dictionary<decimal, decimal>();
			foreach (DataRow dataRow in ordertable.Rows)
			{
				decimal num = (decimal)dataRow["Price"];
				decimal num2 = (decimal)dataRow["VolumeRemaining"] * System.Math.Sign((decimal)dataRow["Volume"]);
				if (this.LadderVolumes.ContainsKey(num))
				{
					System.Collections.Generic.Dictionary<decimal, decimal> ladderVolumes;
					decimal key;
					(ladderVolumes = this.LadderVolumes)[key = num] = ladderVolumes[key] + num2;
				}
				else
				{
					this.LadderVolumes.Add(num, num2);
				}
				if ((string)dataRow["TraderID"] == traderid)
				{
					if (this.LadderTraderVolumes.ContainsKey(num))
					{
						System.Collections.Generic.Dictionary<decimal, decimal> ladderTraderVolumes;
						decimal key2;
						(ladderTraderVolumes = this.LadderTraderVolumes)[key2 = num] = ladderTraderVolumes[key2] + num2;
					}
					else
					{
						this.LadderTraderVolumes.Add(num, num2);
					}
				}
			}
		}
		public void UpdateLadder(decimal price, decimal volumechange, bool istrader)
		{
			price = System.Math.Round(price, this.Parameters.QuotedDecimals);
			if (this.LadderVolumes.ContainsKey(price))
			{
				System.Collections.Generic.Dictionary<decimal, decimal> ladderVolumes;
				decimal key;
				(ladderVolumes = this.LadderVolumes)[key = price] = ladderVolumes[key] + volumechange;
			}
			else
			{
				this.LadderVolumes.Add(price, volumechange);
			}
			if (istrader)
			{
				if (this.LadderTraderVolumes.ContainsKey(price))
				{
					System.Collections.Generic.Dictionary<decimal, decimal> ladderTraderVolumes;
					decimal key2;
					(ladderTraderVolumes = this.LadderTraderVolumes)[key2 = price] = ladderTraderVolumes[key2] + volumechange;
				}
				else
				{
					this.LadderTraderVolumes.Add(price, volumechange);
				}
			}
			this.LadderVolumeUpdated(price, this.LadderVolumes[price]);
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
