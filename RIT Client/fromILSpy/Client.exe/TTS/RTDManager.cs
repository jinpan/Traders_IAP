using System;
using System.ServiceModel;
namespace TTS
{
	internal static class RTDManager
	{
		public const string NamedPipe = "306W/TTS/RTD";
		public const int RowLimit = 100;
		private static ServiceHost Host;
		public static event System.Action<bool> StateChanged;
		static RTDManager()
		{
			RTDManager.Host = null;
			RTDManager.StateChanged = delegate
			{
			};
			Game.Reset = (Action)System.Delegate.Combine(Game.Reset, new Action(RTDManager.SetState));
			Game.VariablesUpdated = (Action)System.Delegate.Combine(Game.VariablesUpdated, new Action(RTDManager.SetState));
			ServiceManager.Disconnected += new Action(RTDManager.Stop);
		}
		private static void SetState()
		{
			if (Game.State.General.IsRTDEnabled)
			{
				RTDManager.Start();
				return;
			}
			RTDManager.Stop();
		}
		public static void Start()
		{
			try
			{
				if (RTDManager.Host == null)
				{
					RTDManager.Host = new ServiceHost(typeof(RTDObject), new Uri[]
					{
						new Uri("net.pipe://localhost")
					});
					RTDManager.Host.AddServiceEndpoint(typeof(IRTDObject), new NetNamedPipeBinding(), "306W/TTS/RTD");
					RTDManager.Host.AddServiceEndpoint(typeof(IRTDObject), new NetNamedPipeBinding(), "Rotman/RIT/RTD");
					RTDManager.Host.Open();
					RTDManager.StateChanged(true);
				}
			}
			catch
			{
				((Client)ThreadHelper.MainThread).ShowInfo("RTD Error", string.Format("Another instance of the {0} client is currently running. Only the first instance will update Excel.", Client.Skin.GetString("program_name")));
				RTDManager.Stop();
			}
		}
		public static void Stop()
		{
			try
			{
				if (RTDManager.Host != null)
				{
					RTDManager.Host.Abort();
				}
			}
			catch
			{
			}
			finally
			{
				RTDManager.Host = null;
				RTDManager.StateChanged(false);
			}
		}
	}
}
