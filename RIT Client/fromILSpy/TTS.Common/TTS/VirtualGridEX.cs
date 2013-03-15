using Janus.Windows.GridEX;
using System;
namespace TTS
{
	public class VirtualGridEX : GridEX
	{
		private bool _VirtualMode;
		public bool VirtualMode
		{
			get
			{
				base.BoundMode = BoundMode.Bound;
				return this._VirtualMode;
			}
			set
			{
				if (value)
				{
					base.SetDataBinding(null, "");
					base.BoundMode = BoundMode.Unbound;
					foreach (GridEXColumn gridEXColumn in base.RootTable.Columns)
					{
						gridEXColumn.BoundMode = ColumnBoundMode.UnboundFetch;
					}
				}
				this._VirtualMode = value;
			}
		}
		public new int RowCount
		{
			get
			{
				return base.RowCount;
			}
			set
			{
				if (value == 0)
				{
					base.ClearItems();
					return;
				}
				while (value > this.RowCount)
				{
					base.AddItem();
				}
				while (value < this.RowCount)
				{
					base.GetRow(this.RowCount - 1).Delete();
				}
			}
		}
	}
}
