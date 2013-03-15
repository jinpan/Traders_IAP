using System;
using System.ServiceModel;
namespace TTS
{
	internal static class ServiceManager
	{
		private static IClientService ClientServiceProxy;
		private static System.EventHandler DisconnectHandler;
		public static event Action Disconnected;
		static ServiceManager()
		{
			ServiceManager.Disconnected = delegate
			{
			};
			ServiceManager.DisconnectHandler = delegate(object state, System.EventArgs e)
			{
				ServiceManager.Disconnect();
			};
		}
		public static bool Connect(string address, int port)
		{
			try
			{
				string uri = string.Concat(new string[]
				{
					"net.tcp://",
					address,
					":",
					port.ToString(),
					"/TTSClientService"
				});
				bool result;
				if (ServiceManager.ClientServiceProxy != null && ((IClientChannel)ServiceManager.ClientServiceProxy).State == CommunicationState.Opened)
				{
					if (!(((IClientChannel)ServiceManager.ClientServiceProxy).RemoteAddress.Uri.Host != address) && ((IClientChannel)ServiceManager.ClientServiceProxy).RemoteAddress.Uri.Port == port)
					{
						result = true;
						return result;
					}
					((IClientChannel)ServiceManager.ClientServiceProxy).Closed -= ServiceManager.DisconnectHandler;
					((IClientChannel)ServiceManager.ClientServiceProxy).Faulted -= ServiceManager.DisconnectHandler;
					((IClientChannel)ServiceManager.ClientServiceProxy).Abort();
				}
				NetTcpBinding netTcpBinding = new NetTcpBinding(SecurityMode.None);
				netTcpBinding.MaxReceivedMessageSize = 2147483647L;
				netTcpBinding.MaxBufferSize = 2147483647;
				netTcpBinding.MaxBufferPoolSize = 2147483647L;
				netTcpBinding.ReaderQuotas.MaxArrayLength = 2147483647;
				netTcpBinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
				netTcpBinding.ReaderQuotas.MaxDepth = 2147483647;
				netTcpBinding.ReaderQuotas.MaxNameTableCharCount = 2147483647;
				netTcpBinding.ReaderQuotas.MaxStringContentLength = 2147483647;
				netTcpBinding.ReceiveTimeout = new System.TimeSpan(0, 8, 0, 0);
				netTcpBinding.SendTimeout = new System.TimeSpan(0, 0, 0, 10);
				netTcpBinding.ReliableSession.Enabled = false;
				netTcpBinding.ReliableSession.Ordered = false;
				ServiceManager.ClientServiceProxy = new DuplexChannelFactory<IClientService>(new ClientCallbackService(), netTcpBinding, new EndpointAddress(uri)).CreateChannel();
				((IClientChannel)ServiceManager.ClientServiceProxy).OperationTimeout = new System.TimeSpan(0, 0, 1, 0, 0);
				((IClientChannel)ServiceManager.ClientServiceProxy).Opened += delegate(object state, System.EventArgs e)
				{
				};
				((IClientChannel)ServiceManager.ClientServiceProxy).Closed += delegate(object state, System.EventArgs e)
				{
				};
				((IClientChannel)ServiceManager.ClientServiceProxy).Faulted += delegate(object state, System.EventArgs e)
				{
				};
				((IClientChannel)ServiceManager.ClientServiceProxy).Closed += ServiceManager.DisconnectHandler;
				((IClientChannel)ServiceManager.ClientServiceProxy).Faulted += ServiceManager.DisconnectHandler;
				((IClientChannel)ServiceManager.ClientServiceProxy).Open();
				result = true;
				return result;
			}
			catch (UriFormatException)
			{
				DialogHelper.ShowError("Invalid address format.", "Connection Error");
				ServiceManager.Disconnected();
			}
			catch (CommunicationException)
			{
				DialogHelper.ShowError(string.Format("Unable to connect to {0}:{1}.", address, port), "Connection Error");
			}
			return false;
		}
		public static void Disconnect()
		{
			try
			{
				if (ServiceManager.ClientServiceProxy != null)
				{
					((IClientChannel)ServiceManager.ClientServiceProxy).Abort();
				}
			}
			finally
			{
				if (ServiceManager.ClientServiceProxy != null && ServiceManager.Disconnected != null)
				{
					ServiceManager.Disconnected();
				}
				ServiceManager.ClientServiceProxy = null;
			}
		}
		public static T Execute<T>(Func<IClientService, T> action)
		{
			try
			{
				return action(ServiceManager.ClientServiceProxy);
			}
			catch (CommunicationException ex)
			{
				if (ex is FaultException)
				{
					throw ex;
				}
			}
			catch (System.TimeoutException)
			{
			}
			catch (System.ObjectDisposedException)
			{
			}
			catch (System.NullReferenceException)
			{
			}
			return default(T);
		}
		public static void Execute(System.Action<IClientService> action)
		{
			try
			{
				action(ServiceManager.ClientServiceProxy);
			}
			catch (CommunicationException ex)
			{
				if (ex is FaultException)
				{
					throw ex;
				}
			}
			catch (System.TimeoutException)
			{
			}
			catch (System.ObjectDisposedException)
			{
			}
			catch (System.NullReferenceException)
			{
			}
		}
	}
}
