using System;
using System.ComponentModel;
using System.Data;
namespace TTS
{
	internal class UpdateableDataView : DataView
	{
		private string BaseRowFilter = "";
		public new string RowFilter
		{
			get
			{
				return base.RowFilter;
			}
			set
			{
				this.BaseRowFilter = value;
				base.RowFilter = value;
			}
		}
		public UpdateableDataView(DataTable table, string rowfilter, string sort, DataViewRowState rowstate) : base(table, rowfilter, sort, rowstate)
		{
			this.BaseRowFilter = rowfilter;
		}
		public UpdateableDataView(DataTable table) : base(table)
		{
		}
		public UpdateableDataView()
		{
		}
		public void RaiseUpdate()
		{
			base.Table.BeginLoadData();
			for (int i = 0; i < base.Count; i++)
			{
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.ItemChanged, i));
			}
			base.Table.EndLoadData();
		}
		public void AppendRowFilter(string expr)
		{
			if (string.IsNullOrWhiteSpace(this.BaseRowFilter))
			{
				base.RowFilter = expr;
				return;
			}
			base.RowFilter = string.Format("({0}) AND ({1})", this.BaseRowFilter, expr);
		}
	}
}
