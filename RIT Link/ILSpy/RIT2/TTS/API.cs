using System;
using System.Runtime.InteropServices;
using System.ServiceModel;
namespace TTS
{
	[ClassInterface(ClassInterfaceType.None), ComVisible(true), Guid("93AD5E6E-40BF-4362-88A8-59D0E31E4714"), ProgId("RIT2.API")]
	public class API : IAPI
	{
		private const string EndPoint = "net.pipe://localhost/Rotman/RIT/API";
		private IAPIObject Proxy;
		public int BUY
		{
			get
			{
				return 1;
			}
		}
		public int SELL
		{
			get
			{
				return -1;
			}
		}
		public int LMT
		{
			get
			{
				return 1;
			}
		}
		public int MKT
		{
			get
			{
				return 0;
			}
		}
		private void Open()
		{
			try
			{
				this.Proxy = new ChannelFactory<IAPIObject>(new NetNamedPipeBinding(), new EndpointAddress("net.pipe://localhost/Rotman/RIT/API")).CreateChannel();
				((IClientChannel)this.Proxy).Faulted += delegate(object sender, EventArgs e)
				{
					this.Proxy = null;
				};
				((IClientChannel)this.Proxy).Closed += delegate(object sender, EventArgs e)
				{
					this.Proxy = null;
				};
			}
			catch
			{
			}
		}
		private void Close()
		{
			if (this.Proxy != null)
			{
				try
				{
					((ICommunicationObject)this.Proxy).Close();
				}
				finally
				{
					this.Proxy = null;
				}
			}
		}
		private T Execute<T>(Func<IAPIObject, T> action)
		{
			T result;
			try
			{
				this.Open();
				result = action(this.Proxy);
			}
			catch
			{
				result = default(T);
			}
			finally
			{
				this.Close();
			}
			return result;
		}
		private void Execute(Action<IAPIObject> action)
		{
			try
			{
				this.Open();
				action(this.Proxy);
			}
			catch
			{
			}
			finally
			{
				this.Close();
			}
		}
		public int GetTimeRemaining()
		{
			return this.Execute<int>((IAPIObject x) => x.GetTimeRemaining());
		}
		public int GetTotalTime()
		{
			return this.Execute<int>((IAPIObject x) => x.GetTotalTime());
		}
		public int GetYearTime()
		{
			return this.Execute<int>((IAPIObject x) => x.GetYearTime());
		}
		public int GetCurrentPeriod()
		{
			return this.Execute<int>((IAPIObject x) => x.GetCurrentPeriod());
		}
		public double GetCash()
		{
			return Convert.ToDouble(this.Execute<decimal>((IAPIObject x) => x.GetCash()));
		}
		public double GetBP()
		{
			return Convert.ToDouble(this.Execute<decimal>((IAPIObject x) => x.GetBP()));
		}
		public double GetNLV()
		{
			return Convert.ToDouble(this.Execute<decimal>((IAPIObject x) => x.GetNLV()));
		}
		public string[] GetTickers()
		{
			return this.Execute<string[]>((IAPIObject x) => x.GetTickers());
		}
		public object[] GetTickerInfo(string ticker)
		{
			return this.Execute<object[]>((IAPIObject x) => x.GetTickerInfo(ticker));
		}
		public int[] GetOrders()
		{
			return this.Execute<int[]>((IAPIObject x) => x.GetOrders());
		}
		public object[] GetOrderInfo(int id)
		{
			return this.Execute<object[]>((IAPIObject x) => x.GetOrderInfo(id));
		}
		public bool AddOrder(string ticker, int volume, double price, int dir, int type)
		{
			return this.Execute<bool>((IAPIObject x) => x.AddOrder(ticker, volume, Convert.ToDecimal(price), dir, type));
		}
		public void CancelOrder(int id)
		{
			this.Execute(delegate(IAPIObject x)
			{
				x.CancelOrder(id);
			});
		}
		public void CancelOrderExpr(string expr)
		{
			this.Execute(delegate(IAPIObject x)
			{
				x.CancelOrderExpr(expr);
			});
		}
		public int AddQueuedOrder(string ticker, int volume, double price, int dir, int type)
		{
			return this.Execute<int>((IAPIObject x) => x.AddQueuedOrder(ticker, volume, Convert.ToDecimal(price), dir, type));
		}
		public bool IsOrderQueued(int id)
		{
			return this.Execute<bool>((IAPIObject x) => x.IsOrderQueued(id));
		}
		public void CancelQueuedOrder(int id)
		{
			this.Execute(delegate(IAPIObject x)
			{
				x.CancelQueuedOrder(id);
			});
		}
		public void ClearQueuedOrders()
		{
			this.Execute(delegate(IAPIObject x)
			{
				x.ClearQueuedOrders();
			});
		}
	}
}
