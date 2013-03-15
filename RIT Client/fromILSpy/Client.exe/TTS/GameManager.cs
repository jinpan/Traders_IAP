using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Timers;
namespace TTS
{
	internal static class GameManager
	{
		private static Timer PeriodTimer;
		public static event System.Action<int> PeriodTicked;
		public static event Action StatusUpdating;
		public static event Action StatusUpdated;
		static GameManager()
		{
			GameManager.PeriodTicked = delegate
			{
			};
			GameManager.StatusUpdating = delegate
			{
			};
			GameManager.StatusUpdated = delegate
			{
			};
			GameManager.PeriodTimer = new Timer();
			GameManager.PeriodTimer.AutoReset = true;
			GameManager.PeriodTimer.Enabled = false;
			GameManager.PeriodTimer.Elapsed += delegate(object sender, ElapsedEventArgs e)
			{
				GameManager.Tick();
			};
			GameManager.PeriodTimer.SynchronizingObject = ThreadHelper.MainThread;
			ServiceManager.Disconnected += delegate
			{
				GameManager.PeriodTimer.Stop();
			};
		}
		public static void Tick()
		{
			if (Game.State == null || !GameManager.PeriodTimer.Enabled)
			{
				return;
			}
			if (!Game.State.Current.IsActive)
			{
				return;
			}
			ThreadHelper.MainThread.BeginInvokeIfRequired(delegate
			{
				Game.State.AssetConverterView.RaiseUpdate();
				Game.State.AssetProducerView.RaiseUpdate();
				Game.State.AssetTransporterView.RaiseUpdate();
				Game.State.AssetContainerView.RaiseUpdate();
			});
		}
		public static void UpdateTick(int tick)
		{
			Game.State.Current.Tick = tick;
		}
		public static void UpdateStatus(GameParameters current)
		{
			GameManager.StatusUpdating();
			Game.State.Current.Period = current.Period;
			Game.State.Current.MillisecondsPerTick = current.MillisecondsPerTick;
			Game.State.Current.IsActive = current.IsActive;
			Game.State.Current.IsPaused = current.IsPaused;
			Game.State.Current.Tick = current.Tick;
			string[] array = (
				from x in Game.State.Securities
				where Game.State.Current.Period >= x.Value.Parameters.StartPeriod && Game.State.Current.Period <= x.Value.Parameters.StopPeriod
				select x.Value.Parameters.Ticker).ToArray<string>();
			string[] source = (
				from x in Game.State.Securities
				where x.Value.Parameters.IsTradeable && Game.State.Current.Period >= x.Value.Parameters.StartPeriod && Game.State.Current.Period <= x.Value.Parameters.StopPeriod && (Game.State.TraderType.ExcludedTickers == null || !Game.State.TraderType.ExcludedTickers.Contains(x.Value.Parameters.Ticker))
				select x.Value.Parameters.Ticker).ToArray<string>();
			string[] source2 = (
				from x in Game.State.AssetInfoTable.Select(string.Format(System.Globalization.CultureInfo.InvariantCulture.NumberFormat, "{0}>=[StartPeriod] AND {0}<=[StopPeriod]", new object[]
				{
					Game.State.Current.Period
				}))
				select (string)x["Ticker"]).ToArray<string>();
			string[] source3 = (
				from x in Game.State.AssetInfoTable.Select(string.Format(System.Globalization.CultureInfo.InvariantCulture.NumberFormat, "{0}>=[StartPeriod] AND {0}<=[StopPeriod] AND [Type] IN ({1},{2})", new object[]
				{
					Game.State.Current.Period,
					1,
					2
				}))
				select (string)x["Ticker"]).ToArray<string>();
			string[] second = (
				from x in Game.State.AssetInfoTable.Select(string.Format(System.Globalization.CultureInfo.InvariantCulture.NumberFormat, "{0}>=[StartPeriod] AND {0}<=[StopPeriod] AND [Type] IN ({1},{2})", new object[]
				{
					Game.State.Current.Period,
					0,
					3
				}))
				select (string)x["Ticker"]).ToArray<string>();
			Game.State.ActiveTradableSecurities.Merge(source.ToList<string>());
			Game.State.ActiveSecurities.Merge(array.ToList<string>());
			Game.State.ActiveTransportationAssets.Merge(source3.ToList<string>());
			Game.State.ActiveOTCableSecuritiesAndAssets.Merge(array.Concat(second).ToList<string>());
			Game.State.MarketView.RowFilter = string.Format("[Ticker] IN ('{0}')", string.Join("','", 
				from x in array
				select x.DataTableStringEscape()));
			Game.State.AssetContainerView.AppendRowFilter(string.Format("[Ticker] IN ('{0}')", string.Join("','", 
				from x in source2
				select x.DataTableStringEscape())));
			Game.State.AssetTransporterView.AppendRowFilter(string.Format("[Ticker] IN ('{0}')", string.Join("','", 
				from x in source2
				select x.DataTableStringEscape())));
			Game.State.AssetConverterView.AppendRowFilter(string.Format("[Ticker] IN ('{0}')", string.Join("','", 
				from x in source2
				select x.DataTableStringEscape())));
			Game.State.AssetProducerView.AppendRowFilter(string.Format("[Ticker] IN ('{0}')", string.Join("','", 
				from x in source2
				select x.DataTableStringEscape())));
			if (Game.State.Current.Status == GameStatus.ACTIVE)
			{
				GameManager.PeriodTimer.Interval = (double)Game.State.Current.MillisecondsPerTick;
				GameManager.PeriodTimer.Start();
			}
			else
			{
				GameManager.PeriodTimer.Stop();
			}
			GameManager.StatusUpdated();
		}
	}
}
