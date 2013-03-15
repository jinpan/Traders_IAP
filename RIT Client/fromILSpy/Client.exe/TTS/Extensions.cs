using Janus.Windows.GridEX;
using System;
using System.Data;
using System.Text;
namespace TTS
{
	internal static class Extensions
	{
		public static string ToDelimitedString(this DataTable table, string delimiter = ",")
		{
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			for (int i = 0; i < table.Columns.Count; i++)
			{
				stringBuilder.Append(table.Columns[i].ColumnName);
				if (i < table.Columns.Count - 1)
				{
					stringBuilder.Append(delimiter);
				}
			}
			stringBuilder.AppendLine();
			foreach (DataRow dataRow in table.Rows)
			{
				for (int j = 0; j < table.Columns.Count; j++)
				{
					stringBuilder.Append(dataRow[j]);
					if (j < table.Columns.Count - 1)
					{
						stringBuilder.Append(delimiter);
					}
				}
				stringBuilder.AppendLine();
			}
			return stringBuilder.ToString();
		}
		public static void AddPoint<T>(this ChartItem<T> chart, T point) where T : ChartPoint
		{
			chart.AddPoint(Game.State.Current.Period, Game.State.Current.Tick, point);
		}
		public static void AddSecurityDecimalFormatter(this GridEXTable gridtable, params string[] cols)
		{
			GridEX gridEX = gridtable.GridEX;
			gridEX.LoadingRow += delegate(object sender, RowLoadEventArgs e)
			{
				if (e.Row.DataRow == null)
				{
					return;
				}
				if (!Game.State.Securities.ContainsKey(e.Row.Cells["Ticker"].Text))
				{
					return;
				}
				string formatString = Game.State.Securities[e.Row.Cells["Ticker"].Text].Parameters.FormatString;
				string[] cols2 = cols;
				for (int i = 0; i < cols2.Length; i++)
				{
					string columnKey = cols2[i];
					if (e.Row.Cells[columnKey].Value != System.DBNull.Value)
					{
						e.Row.Cells[columnKey].Text = ((decimal)e.Row.Cells[columnKey].Value).ToString(formatString);
					}
				}
			};
		}
		public static void AddSecurityVolumeDecimalFormatter(this GridEXTable gridtable, params string[] cols)
		{
			GridEX gridEX = gridtable.GridEX;
			gridEX.LoadingRow += delegate(object sender, RowLoadEventArgs e)
			{
				if (e.Row.DataRow == null)
				{
					return;
				}
				if (!Game.State.Securities.ContainsKey(e.Row.Cells["Ticker"].Text))
				{
					return;
				}
				string volumeFormatString = Game.State.Securities[e.Row.Cells["Ticker"].Text].Parameters.VolumeFormatString;
				string[] cols2 = cols;
				for (int i = 0; i < cols2.Length; i++)
				{
					string columnKey = cols2[i];
					if (e.Row.Cells[columnKey].Value != System.DBNull.Value)
					{
						e.Row.Cells[columnKey].Text = ((decimal)e.Row.Cells[columnKey].Value).ToString(volumeFormatString);
					}
				}
			};
		}
	}
}
