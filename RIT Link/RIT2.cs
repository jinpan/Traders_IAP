// C:\Program Files (x86)\Rotman\RIT RTD Excel Link\RIT2.dll
// RIT2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

// Architecture: AnyCPU (64-bit preferred)
// Runtime: .NET 4.0

using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.ServiceModel;
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyCompany("Rotman")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCopyright("Copyright © Chan, Mak, McCurdy 2011")]
[assembly: AssemblyDescription("Rotman Interactive Trader")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: AssemblyProduct("RIT Application Programming Interfacae")]
[assembly: AssemblyTitle("Rotman RIT Application Programming Interface")]
[assembly: AssemblyTrademark("")]
[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: ComVisible(true)]
[assembly: Guid("5b339a57-02da-4aa7-bcea-15fb8b292d16")]
[assembly: TargetFramework(".NETFramework,Version=v4.0", FrameworkDisplayName = ".NET Framework 4")]
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
	[ComVisible(true), Guid("FE7B5364-D6B3-4CD6-9DEB-C1B99E73E3B7"), InterfaceType(ComInterfaceType.InterfaceIsDual)]
	public interface IAPI
	{
		int BUY
		{
			get;
		}
		int SELL
		{
			get;
		}
		int MKT
		{
			get;
		}
		int LMT
		{
			get;
		}
		int GetTimeRemaining();
		int GetTotalTime();
		int GetYearTime();
		int GetCurrentPeriod();
		double GetCash();
		double GetBP();
		double GetNLV();
		string[] GetTickers();
		object[] GetTickerInfo(string ticker);
		int[] GetOrders();
		object[] GetOrderInfo(int id);
		bool AddOrder(string ticker, int volume, double price, int dir, int type);
		void CancelOrder(int id);
		void CancelOrderExpr(string expr);
		int AddQueuedOrder(string ticker, int volume, double price, int dir, int type);
		bool IsOrderQueued(int id);
		void CancelQueuedOrder(int id);
		void ClearQueuedOrders();
	}
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

// C:\Program Files (x86)\Rotman\RIT RTD Excel Link\RIT2.RTD.dll
// RIT2.RTD, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null

// Architecture: AnyCPU (64-bit preferred)
// Runtime: .NET 4.0

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.ServiceModel;
using System.Windows.Forms;
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints)]
[assembly: AssemblyCompany("BP")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCopyright("Copyright © Chan, Mak, McCurdy 2011")]
[assembly: AssemblyDescription("Simulation Trading Assessment Recruiting")]
[assembly: AssemblyFileVersion("1.0.0.0")]
[assembly: AssemblyProduct("STAR Real Time Data")]
[assembly: AssemblyTitle("BP STAR Real Time Data")]
[assembly: AssemblyTrademark("")]
[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: ComVisible(true)]
[assembly: Guid("3ad442f3-16a6-47ba-83c3-09c9a2548935")]
[assembly: TargetFramework(".NETFramework,Version=v4.0,Profile=Client", FrameworkDisplayName = ".NET Framework 4 Client Profile")]
namespace TTS
{
	[ServiceContract]
	public interface IRTDObject
	{
		[OperationContract]
		Dictionary<string, object> GetData(string[] topics);
		[OperationContract]
		int GetRefreshInterval();
	}
	[Guid("EC0E6191-DB51-11D3-8F3E-00C04F3651B8")]
	public interface IRtdServer
	{
		int ServerStart(IRTDUpdateEvent callback);
		object ConnectData(int topicId, [MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)] ref Array strings, ref bool newValues);
		void DisconnectData(int topicId);
		int Heartbeat();
		void ServerTerminate();
		[return: MarshalAs(UnmanagedType.SafeArray, SafeArraySubType = VarEnum.VT_VARIANT)]
		Array RefreshData(ref int topicCount);
	}
	[Guid("F5A08459-D202-4606-87B0-9EEB5F70A159"), ProgId("RIT2.RTD")]
	public class RtdServer : IRtdServer
	{
		private const string EndPoint = "net.pipe://localhost/Rotman/RIT/RTD";
		private const int DisconnectedRefreshInterval = 2000;
		private const int MaxRefreshInterval = 200;
		private IRTDUpdateEvent ExcelCallback;
		private Timer RefreshTimer;
		private Dictionary<int, string> Topics = new Dictionary<int, string>();
		private Dictionary<string, int> TopicsCount = new Dictionary<string, int>();
		private IRTDObject Proxy;
		private int RefreshInterval = 500;
		public int ServerStart(IRTDUpdateEvent callback)
		{
			this.ExcelCallback = callback;
			this.RefreshTimer = new Timer();
			this.RefreshTimer.Tick += delegate(object sender, EventArgs e)
			{
				this.RefreshTimer.Stop();
				this.ExcelCallback.UpdateNotify();
			};
			this.RefreshTimer.Interval = this.RefreshInterval;
			this.CreateProxy();
			return 1;
		}
		public void ServerTerminate()
		{
			if (this.RefreshTimer != null)
			{
				this.RefreshTimer.Dispose();
				this.RefreshTimer = null;
			}
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
		public object ConnectData(int topicId, ref Array strings, ref bool newValues)
		{
			string text = this.ToTopic(strings);
			this.Topics[topicId] = text;
			if (!this.TopicsCount.ContainsKey(text))
			{
				this.TopicsCount.Add(text, 0);
			}
			Dictionary<string, int> topicsCount;
			string key;
			(topicsCount = this.TopicsCount)[key = text] = topicsCount[key] + 1;
			this.RefreshTimer.Start();
			return text;
		}
		public void DisconnectData(int topicId)
		{
			string text = this.Topics[topicId];
			Dictionary<string, int> topicsCount;
			string key;
			(topicsCount = this.TopicsCount)[key = text] = topicsCount[key] - 1;
			if (this.TopicsCount[text] <= 0)
			{
				this.TopicsCount.Remove(text);
			}
			this.Topics.Remove(topicId);
		}
		public Array RefreshData(ref int topicCount)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			object[,] array = new object[2, this.Topics.Count];
			try
			{
				if (this.Proxy == null)
				{
					this.CreateProxy();
				}
				dictionary = this.Proxy.GetData(this.TopicsCount.Keys.ToArray<string>());
				int num = 0;
				foreach (KeyValuePair<int, string> current in this.Topics)
				{
					array[0, num] = current.Key;
					array[1, num] = dictionary[current.Value];
					num++;
				}
				this.RefreshTimer.Interval = this.Proxy.GetRefreshInterval();
			}
			catch
			{
				this.Proxy = null;
				int num2 = 0;
				foreach (KeyValuePair<int, string> current2 in this.Topics)
				{
					array[0, num2] = current2.Key;
					array[1, num2] = current2.Value;
					num2++;
				}
				this.RefreshTimer.Interval = 2000;
			}
			topicCount = this.Topics.Count;
			this.RefreshTimer.Start();
			return array;
		}
		public int Heartbeat()
		{
			return 1;
		}
		private void CreateProxy()
		{
			try
			{
				this.Proxy = new ChannelFactory<IRTDObject>(new NetNamedPipeBinding(), new EndpointAddress("net.pipe://localhost/Rotman/RIT/RTD")).CreateChannel();
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
		private string ToTopic(Array strings)
		{
			return string.Join("|", (
				from string x in strings
				select x.ToUpper().Trim() into x
				where !string.IsNullOrEmpty(x)
				select x).ToArray<string>());
		}
	}
	[Guid("A43788C1-D91B-11D3-8F39-00C04F3651B8")]
	public interface IRTDUpdateEvent
	{
		int HeartbeatInterval
		{
			get;
			set;
		}
		void UpdateNotify();
		void Disconnect();
	}
}
