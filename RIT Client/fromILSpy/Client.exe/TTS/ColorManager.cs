using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Timers;
namespace TTS
{
	internal static class ColorManager
	{
		private static SortedDictionary<UniqueDateTime, DataRow> NewsRows;
		private static SortedDictionary<UniqueDateTime, DataRow> OrderRows;
		private static Timer ColorTimer;
		static ColorManager()
		{
			ColorManager.NewsRows = new SortedDictionary<UniqueDateTime, DataRow>();
			ColorManager.OrderRows = new SortedDictionary<UniqueDateTime, DataRow>();
			ColorManager.ColorTimer = new Timer(250.0);
			ColorManager.ColorTimer.SynchronizingObject = ThreadHelper.MainThread;
			ColorManager.ColorTimer.AutoReset = false;
			ColorManager.ColorTimer.Elapsed += delegate(object ss, ElapsedEventArgs ee)
			{
				ThreadHelper.MainThread.BeginInvokeIfRequired(new Action(ColorManager.ClearColors));
			};
			ColorManager.ColorTimer.Start();
			Game.Reset = (Action)System.Delegate.Combine(Game.Reset, delegate
			{
				ColorManager.NewsRows.Clear();
				ColorManager.OrderRows.Clear();
			});
		}
		private static void ClearColors()
		{
			System.DateTime now = System.DateTime.Now;
			Game.State.NewsTable.BeginLoadData();
			while (ColorManager.NewsRows.Count > 0 && ColorManager.NewsRows.First<System.Collections.Generic.KeyValuePair<UniqueDateTime, DataRow>>().Key.Time < now)
			{
				ColorManager.NewsRows.First<System.Collections.Generic.KeyValuePair<UniqueDateTime, DataRow>>().Value["ColorState"] = 0;
				ColorManager.NewsRows.Remove(ColorManager.NewsRows.First<System.Collections.Generic.KeyValuePair<UniqueDateTime, DataRow>>().Key);
			}
			Game.State.NewsTable.EndLoadData();
			Game.State.OrderTable.BeginLoadData();
			while (ColorManager.OrderRows.Count > 0 && ColorManager.OrderRows.First<System.Collections.Generic.KeyValuePair<UniqueDateTime, DataRow>>().Key.Time < now)
			{
				DataRow value = ColorManager.OrderRows.First<System.Collections.Generic.KeyValuePair<UniqueDateTime, DataRow>>().Value;
				if (value.RowState != DataRowState.Detached && value.RowState != DataRowState.Deleted)
				{
					value["ColorState"] = 0;
					if ((decimal)value["VolumeRemaining"] == 0m)
					{
						StateManager.ArchiveOrder(value, OrderStatus.TRANSACTED);
					}
				}
				ColorManager.OrderRows.Remove(ColorManager.OrderRows.First<System.Collections.Generic.KeyValuePair<UniqueDateTime, DataRow>>().Key);
			}
			Game.State.OrderTable.EndLoadData();
			ColorManager.ColorTimer.Start();
		}
		public static void AddNewsRow(DataRow r)
		{
			ColorManager.NewsRows.Add(new UniqueDateTime(System.DateTime.Now.AddSeconds(5.0)), r);
		}
		public static void AddOrderRow(DataRow r)
		{
			ColorManager.OrderRows.Add(new UniqueDateTime(System.DateTime.Now.AddSeconds(0.5)), r);
		}
	}
}
