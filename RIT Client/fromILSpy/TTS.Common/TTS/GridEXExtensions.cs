using Janus.Windows.GridEX;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
namespace TTS
{
	public static class GridEXExtensions
	{
		private static System.Drawing.Color TTSProgressBarColor = System.Drawing.Color.LightBlue;
		public static void Format(this GridEX grid)
		{
			grid.SuspendLayout();
			grid.RowHeaders = InheritableBoolean.False;
			grid.VisualStyle = VisualStyle.Office2010;
			grid.AlternatingColors = true;
			grid.ColumnAutoSizeMode = ColumnAutoSizeMode.AllCells;
			grid.GroupByBoxVisible = false;
			grid.ColumnAutoResize = true;
			grid.AllowColumnDrag = false;
			grid.AutomaticSort = false;
			grid.EmptyRows = false;
			grid.EnterKeyBehavior = EnterKeyBehavior.NextRow;
			grid.FilterMode = FilterMode.None;
			grid.FocusStyle = FocusStyle.None;
			grid.GridLines = GridLines.Default;
			grid.TabKeyBehavior = TabKeyBehavior.ColumnNavigation;
			grid.AutoEdit = false;
			grid.UpdateOnLeave = false;
			grid.SelectedFormatStyle.BackColor = System.Drawing.Color.Empty;
			grid.SelectedFormatStyle.ForeColor = System.Drawing.Color.Empty;
			grid.CellSelectionMode = CellSelectionMode.EntireRow;
			grid.FocusCellDisplayMode = FocusCellDisplayMode.UseSelectedFormatStyle;
			grid.HideSelection = HideSelection.HideSelection;
			grid.SelectionMode = Janus.Windows.GridEX.SelectionMode.SingleSelection;
			grid.AllowAddNew = InheritableBoolean.False;
			grid.AllowDelete = InheritableBoolean.False;
			grid.AllowEdit = InheritableBoolean.False;
			grid.ResumeLayout();
		}
		public static void FormatColumns(this GridEXTable gridtable, string[] keys, string[] captions = null)
		{
			foreach (GridEXColumn gridEXColumn in gridtable.Columns)
			{
				gridEXColumn.Visible = false;
			}
			for (int i = 0; i < keys.Length; i++)
			{
				string key = keys[i];
				if (gridtable.Columns[key].Type == typeof(System.DateTime))
				{
					gridtable.Columns[key].FormatString = "HH:mm:ss";
					gridtable.Columns[key].TextAlignment = TextAlignment.Center;
				}
				else
				{
					if (gridtable.Columns[key].Type == typeof(double) || gridtable.Columns[key].Type == typeof(decimal))
					{
						gridtable.Columns[key].FormatString = "#,0.00##";
						gridtable.Columns[key].TextAlignment = TextAlignment.Far;
					}
					else
					{
						if (gridtable.Columns[key].Type == typeof(int))
						{
							gridtable.Columns[key].FormatString = "#,0";
							gridtable.Columns[key].TextAlignment = TextAlignment.Far;
						}
						else
						{
							if (gridtable.Columns[key].Type == typeof(TickerWeight))
							{
								gridtable.Columns[key].TextAlignment = TextAlignment.Far;
							}
						}
					}
				}
				gridtable.Columns[key].Visible = true;
				if (captions != null)
				{
					gridtable.Columns[key].Caption = captions[i];
				}
				gridtable.Columns[key].Position = i;
				gridtable.Columns[key].HeaderAlignment = TextAlignment.Center;
			}
			for (int j = 0; j < keys.Length; j++)
			{
				string key2 = keys[j];
				gridtable.Columns[key2].Width = gridtable.GridEX.Width / keys.Length;
			}
		}
		public static void FormatDecimalColumns(this GridEXTable gridtable, string formatstring, params string[] columns)
		{
			for (int i = 0; i < columns.Length; i++)
			{
				string key = columns[i];
				gridtable.Columns[key].FormatString = formatstring;
			}
		}
		public static void AddIListColumns(this GridEXTable gridtable, DataTable table = null)
		{
			DataTable arg_28_0;
			if ((arg_28_0 = table) == null)
			{
				arg_28_0 = ((gridtable.GridEX.DataSource as DataView) ?? new DataView(new DataTable())).Table;
			}
			table = arg_28_0;
			foreach (DataColumn dataColumn in table.Columns)
			{
				if (!gridtable.Columns.Contains(dataColumn.ColumnName))
				{
					gridtable.Columns.Add(new GridEXColumn(dataColumn.ColumnName, ColumnType.Text, EditType.NoEdit)
					{
						Caption = Regex.Replace(dataColumn.ColumnName, "[A-Z]", " $0")
					});
				}
			}
		}
		public static void AddButtonColumn(this GridEXTable gridtable, string name, string text, string header = null)
		{
			gridtable.Columns.Add(name, ColumnType.Text, EditType.NoEdit);
			gridtable.Columns[name].BoundMode = ColumnBoundMode.UnboundFetch;
			gridtable.Columns[name].ButtonStyle = ButtonStyle.ButtonCell;
			gridtable.Columns[name].ButtonDisplayMode = CellButtonDisplayMode.Always;
			gridtable.Columns[name].ButtonText = text;
			gridtable.Columns[name].TextAlignment = TextAlignment.Center;
			gridtable.Columns[name].Visible = true;
			gridtable.Columns[name].HeaderAlignment = TextAlignment.Center;
			if (header != null)
			{
				gridtable.Columns[name].Caption = header;
			}
		}
		public static void AddPositiveNegativeFormatter(this GridEXTable gridtable, params string[] cols)
		{
			GridEX gridEX = gridtable.GridEX;
			gridEX.LoadingRow += delegate(object sender, RowLoadEventArgs e)
			{
				if (e.Row.DataRow == null)
				{
					return;
				}
				string[] cols2 = cols;
				for (int i = 0; i < cols2.Length; i++)
				{
					string columnKey = cols2[i];
					if (e.Row.Cells[columnKey].Value != System.DBNull.Value)
					{
						decimal d = System.Convert.ToDecimal(e.Row.Cells[columnKey].Value);
						if (d > 0m)
						{
							e.Row.Cells[columnKey].FormatStyle = ColorHelper.CellStyleGreen;
						}
						else
						{
							if (d < 0m)
							{
								e.Row.Cells[columnKey].FormatStyle = ColorHelper.CellStyleRed;
							}
							else
							{
								e.Row.Cells[columnKey].FormatStyle = ColorHelper.CellStyleBlack;
							}
						}
					}
				}
			};
		}
		public static void AddPeriodTickFormatter(this GridEXTable gridtable, string period, string tick, string nullvalue = "")
		{
			gridtable.Columns[period].Visible = false;
			gridtable.GridEX.FormattingRow += delegate(object sender, RowLoadEventArgs e)
			{
				if (e.Row.DataRow == null)
				{
					return;
				}
				if (e.Row.RowType == RowType.Record && e.Row.Table.Columns.Contains(tick) && e.Row.Table.Columns.Contains(period))
				{
					if (e.Row.Cells[tick].Value == System.DBNull.Value || e.Row.Cells[period].Value == System.DBNull.Value)
					{
						e.Row.Cells[tick].Text = nullvalue;
						return;
					}
					e.Row.Cells[tick].Text = string.Format("P{0} : {1}", e.Row.Cells[period].Value, e.Row.Cells[tick].Value);
				}
			};
		}
		public static void AddProgressBarFormatter(this GridEXTable gridtable, string key, string maxkey)
		{
			gridtable.AddProgressBarFormatter(key, (DrawGridAreaEventArgs e) => System.Convert.ToSingle(e.Row.Cells[key].Value) / System.Convert.ToSingle(e.Row.Cells[maxkey].Value), delegate(DrawGridAreaEventArgs e)
			{
				if (e.Row.Cells[maxkey].Value is int && (int)e.Row.Cells[maxkey].Value == 2147483647)
				{
					return new string(new char[]
					{
						'∞'
					});
				}
				return string.Format("{0} / {1}", e.Row.Cells[key].Value, e.Row.Cells[maxkey].Value);
			});
		}
		public static void AddProgressBarFormatter(this GridEXTable gridtable, string key, Func<DrawGridAreaEventArgs, float> percentfunction, Func<DrawGridAreaEventArgs, string> stringfunction = null)
		{
			gridtable.Columns[key].OwnerDrawnMode = ColumnOwnerDrawnMode.Cells;
			gridtable.GridEX.DrawGridArea += delegate(object sender, DrawGridAreaEventArgs e)
			{
				if (e.Row.DataRow == null)
				{
					return;
				}
				if (e.Column.Key == key)
				{
					e.Graphics.FillRectangle(e.BackBrush, e.Bounds);
					if (e.Row.Cells[key].Value != System.DBNull.Value)
					{
						e.StringFormat.Alignment = System.Drawing.StringAlignment.Center;
						float num = System.Math.Min(System.Math.Max(percentfunction(e), 0f), 1f);
						System.Drawing.Rectangle rect = System.Drawing.Rectangle.Inflate(e.Bounds, -1, -3);
						rect.Width = (int)((float)rect.Width * num);
						System.Drawing.Brush brush = new System.Drawing.SolidBrush(GridEXExtensions.TTSProgressBarColor);
						e.Graphics.FillRectangle(brush, rect);
						if (stringfunction == null)
						{
							e.Graphics.DrawString(string.Format((num > 0f) ? "{0:p}" : "", num), e.Font, e.ForeBrush, e.Bounds, e.StringFormat);
						}
						else
						{
							e.Graphics.DrawString(stringfunction(e), e.Font, e.ForeBrush, e.Bounds, e.StringFormat);
						}
						brush.Dispose();
						e.Handled = true;
					}
				}
			};
		}
		public static void AddTickerArrayFormatter(this GridEXTable gridtable, params string[] keys)
		{
			gridtable.GridEX.LoadingRow += delegate(object sender, RowLoadEventArgs e)
			{
				if (e.Row.DataRow == null)
				{
					return;
				}
				if (e.Row.RowType == RowType.Record)
				{
					string[] keys2 = keys;
					for (int i = 0; i < keys2.Length; i++)
					{
						string text = keys2[i];
						if (e.Row.Table.Columns.Contains(text) && e.Row.Cells[text].Value != System.DBNull.Value)
						{
							e.Row.Cells[text].Text = string.Join(", ", 
								from x in (TickerWeight[])e.Row.Cells[text].Value
								select x.ToString());
						}
					}
				}
			};
		}
		public static void AddContextMenu(this GridEX grid, bool columncontrol)
		{
			ContextMenuStrip cms_gen = new ContextMenuStrip
			{
				RightToLeft = RightToLeft.No
			};
			ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();
			ToolStripSeparator value = new ToolStripSeparator();
			ToolStripMenuItem toolStripMenuItem2 = new ToolStripMenuItem();
			ToolStripMenuItem toolStripMenuItem3 = new ToolStripMenuItem();
			ToolStripMenuItem toolStripMenuItem4 = new ToolStripMenuItem();
			grid.ContextMenuStrip = cms_gen;
			toolStripMenuItem.Text = "Autosize Columns";
			toolStripMenuItem2.Text = "Copy To Clipboard";
			toolStripMenuItem3.Text = "Export To Excel";
			toolStripMenuItem4.Text = "Clear Sort";
			cms_gen.AllowMerge = false;
			cms_gen.Items.AddRange(new ToolStripItem[]
			{
				toolStripMenuItem2,
				toolStripMenuItem,
				toolStripMenuItem4
			});
			cms_gen.Closing += delegate(object sender, ToolStripDropDownClosingEventArgs e)
			{
				if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
				{
					e.Cancel = true;
				}
			};
			toolStripMenuItem.Click += delegate(object sender, System.EventArgs e)
			{
				if ((GridEX)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl != null)
				{
					cms_gen.Tag = (GridEX)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl;
				}
				if (cms_gen.Tag != null)
				{
					((GridEX)cms_gen.Tag).AutoSizeColumns();
				}
				((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).Close();
			};
			toolStripMenuItem4.Click += delegate(object sender, System.EventArgs e)
			{
				if ((GridEX)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl != null)
				{
					cms_gen.Tag = (GridEX)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl;
				}
				if (cms_gen.Tag != null)
				{
					((GridEX)cms_gen.Tag).RootTable.SortKeys.Clear();
				}
				((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).Close();
			};
			toolStripMenuItem2.Click += delegate(object sender, System.EventArgs e)
			{
				if ((GridEX)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl != null)
				{
					cms_gen.Tag = (GridEX)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl;
				}
				if ((GridEX)cms_gen.Tag != null)
				{
					Janus.Windows.GridEX.SelectionMode selectionMode = ((GridEX)cms_gen.Tag).SelectionMode;
					((GridEX)cms_gen.Tag).SelectionMode = Janus.Windows.GridEX.SelectionMode.MultipleSelection;
					for (int k = 0; k < ((GridEX)cms_gen.Tag).RowCount; k++)
					{
						((GridEX)cms_gen.Tag).SelectedItems.Add(k);
					}
					Clipboard.SetDataObject(((GridEX)cms_gen.Tag).GetClipString(), true);
					((GridEX)cms_gen.Tag).SelectedItems.Clear();
					((GridEX)cms_gen.Tag).SelectionMode = selectionMode;
				}
				((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).Close();
			};
			if (columncontrol)
			{
				cms_gen.Items.Add(value);
				for (int i = 0; i < grid.RootTable.Columns.Count; i++)
				{
					for (int j = 0; j < grid.RootTable.Columns.Count; j++)
					{
						GridEXColumn gridEXColumn = grid.RootTable.Columns[j];
						if (gridEXColumn.Position == i && gridEXColumn.Visible)
						{
							ToolStripMenuItem toolStripMenuItem5 = new ToolStripMenuItem(gridEXColumn.Caption);
							toolStripMenuItem5.Name = gridEXColumn.Key;
							toolStripMenuItem5.CheckOnClick = true;
							toolStripMenuItem5.Checked = true;
							toolStripMenuItem5.CheckedChanged += delegate(object sender, System.EventArgs e)
							{
								if ((GridEX)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl != null)
								{
									cms_gen.Tag = (GridEX)((ContextMenuStrip)((ToolStripMenuItem)sender).Owner).SourceControl;
								}
								if ((GridEX)cms_gen.Tag != null && ((GridEX)cms_gen.Tag).RootTable.Columns[((ToolStripMenuItem)sender).Name].Visible != ((ToolStripMenuItem)sender).Checked)
								{
									((GridEX)cms_gen.Tag).RootTable.Columns[((ToolStripMenuItem)sender).Name].Visible = ((ToolStripMenuItem)sender).Checked;
								}
							};
							grid.ColumnVisibleChanged += delegate(object sender, ColumnActionEventArgs e)
							{
								foreach (ToolStripItem toolStripItem in e.Column.Table.GridEX.ContextMenuStrip.Items)
								{
									if (toolStripItem.Name == e.Column.Key && ((ToolStripMenuItem)toolStripItem).Checked != e.Column.Visible)
									{
										((ToolStripMenuItem)toolStripItem).Checked = e.Column.Visible;
									}
								}
							};
							cms_gen.Items.Add(toolStripMenuItem5);
						}
					}
				}
			}
		}
	}
}
