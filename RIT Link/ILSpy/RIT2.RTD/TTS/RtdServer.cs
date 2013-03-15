using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Windows.Forms;
namespace TTS
{
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
}
