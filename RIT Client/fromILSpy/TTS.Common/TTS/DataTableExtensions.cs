using System;
using System.Data;
namespace TTS
{
	public static class DataTableExtensions
	{
		public static void ClearAndMerge(this DataTable table, DataTable newtable)
		{
			table.BeginLoadData();
			table.Clear();
			table.Merge(newtable, false, MissingSchemaAction.Ignore);
			table.EndLoadData();
		}
		public static DataTable SelectToDataTable(this DataTable table, string expression)
		{
			DataTable dataTable = table.Clone();
			DataRow[] array = table.Select(expression);
			DataRow[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow row = array2[i];
				dataTable.ImportRow(row);
			}
			return dataTable;
		}
	}
}
