using Janus.Windows.GridEX;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Windows.Forms;
using TTS.Properties;
namespace TTS
{
	public class Assets : TTSForm
	{
		private IContainer components;
		private GridEX ContainersGrid;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.TabPage tabPage3;
		private GridEX TransportersGrid;
		private GridEX ProducersGrid;
		private System.Windows.Forms.TabPage tabPage4;
		private GridEX ConvertersGrid;
		public static WindowType TTSWindowType = WindowType.ASSETS;
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}
		private void InitializeComponent()
		{
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Assets));
			this.ContainersGrid = new GridEX();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.TransportersGrid = new GridEX();
			this.tabPage4 = new System.Windows.Forms.TabPage();
			this.ConvertersGrid = new GridEX();
			this.tabPage3 = new System.Windows.Forms.TabPage();
			this.ProducersGrid = new GridEX();
			((ISupportInitialize)this.ContainersGrid).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.tabPage2.SuspendLayout();
			((ISupportInitialize)this.TransportersGrid).BeginInit();
			this.tabPage4.SuspendLayout();
			((ISupportInitialize)this.ConvertersGrid).BeginInit();
			this.tabPage3.SuspendLayout();
			((ISupportInitialize)this.ProducersGrid).BeginInit();
			base.SuspendLayout();
			this.ContainersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ContainersGrid.Location = new System.Drawing.Point(0, 0);
			this.ContainersGrid.Name = "ContainersGrid";
			this.ContainersGrid.Size = new System.Drawing.Size(576, 141);
			this.ContainersGrid.TabIndex = 0;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.Controls.Add(this.tabPage4);
			this.tabControl1.Controls.Add(this.tabPage3);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(584, 167);
			this.tabControl1.TabIndex = 1;
			this.tabPage1.Controls.Add(this.ContainersGrid);
			this.tabPage1.Location = new System.Drawing.Point(4, 22);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Size = new System.Drawing.Size(576, 141);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Storage";
			this.tabPage1.UseVisualStyleBackColor = true;
			this.tabPage2.Controls.Add(this.TransportersGrid);
			this.tabPage2.Location = new System.Drawing.Point(4, 22);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Size = new System.Drawing.Size(576, 141);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Transport";
			this.tabPage2.UseVisualStyleBackColor = true;
			this.TransportersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.TransportersGrid.Location = new System.Drawing.Point(0, 0);
			this.TransportersGrid.Name = "TransportersGrid";
			this.TransportersGrid.Size = new System.Drawing.Size(576, 141);
			this.TransportersGrid.TabIndex = 1;
			this.tabPage4.Controls.Add(this.ConvertersGrid);
			this.tabPage4.Location = new System.Drawing.Point(4, 22);
			this.tabPage4.Name = "tabPage4";
			this.tabPage4.Size = new System.Drawing.Size(576, 141);
			this.tabPage4.TabIndex = 3;
			this.tabPage4.Text = "Converters";
			this.tabPage4.UseVisualStyleBackColor = true;
			this.ConvertersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ConvertersGrid.Location = new System.Drawing.Point(0, 0);
			this.ConvertersGrid.Name = "ConvertersGrid";
			this.ConvertersGrid.Size = new System.Drawing.Size(576, 141);
			this.ConvertersGrid.TabIndex = 2;
			this.tabPage3.Controls.Add(this.ProducersGrid);
			this.tabPage3.Location = new System.Drawing.Point(4, 22);
			this.tabPage3.Name = "tabPage3";
			this.tabPage3.Size = new System.Drawing.Size(576, 141);
			this.tabPage3.TabIndex = 2;
			this.tabPage3.Text = "Producers";
			this.tabPage3.UseVisualStyleBackColor = true;
			this.ProducersGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ProducersGrid.Location = new System.Drawing.Point(0, 0);
			this.ProducersGrid.Name = "ProducersGrid";
			this.ProducersGrid.Size = new System.Drawing.Size(576, 141);
			this.ProducersGrid.TabIndex = 2;
			base.ClientSize = new System.Drawing.Size(584, 167);
			base.Controls.Add(this.tabControl1);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "Assets";
			this.Text = "Assets";
			((ISupportInitialize)this.ContainersGrid).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.tabPage2.ResumeLayout(false);
			((ISupportInitialize)this.TransportersGrid).EndInit();
			this.tabPage4.ResumeLayout(false);
			((ISupportInitialize)this.ConvertersGrid).EndInit();
			this.tabPage3.ResumeLayout(false);
			((ISupportInitialize)this.ProducersGrid).EndInit();
			base.ResumeLayout(false);
		}
		public Assets(TTSFormState state) : base(state)
		{
			this.InitializeComponent();
			InheritableBoolean repeatHeaders = InheritableBoolean.False;
			GridEXFormatStyle formatStyle = new GridEXFormatStyle(ColorHelper.RowStyleYellow)
			{
				FontBold = TriState.True
			};
			int rowHeight = 30;
			this.ContainersGrid.SuspendLayout();
			this.ContainersGrid.DataSource = Game.State.AssetContainerView;
			this.ContainersGrid.RetrieveStructure(true);
			this.ContainersGrid.Format();
			this.ContainersGrid.AlternatingColors = false;
			this.ContainersGrid.RootTable.FormatColumns(new string[]
			{
				"Ticker",
				"LeaseCount",
				"LeasePrice",
				"TicksPerLease",
				"Containment",
				"Realized"
			}, null);
			this.ContainersGrid.RootTable.AddButtonColumn("LeaseAsset", "Lease", "");
			this.ContainersGrid.RootTable.AddProgressBarFormatter("LeaseCount", "TotalQuantity");
			this.ContainersGrid.RepeatHeaders = repeatHeaders;
			this.ContainersGrid.RootTable.RowFormatStyle = new GridEXFormatStyle(formatStyle);
			this.ContainersGrid.RootTable.RowHeight = rowHeight;
			this.ContainersGrid.RootTable.ChildTables[0].FormatColumns(new string[]
			{
				"StartLeasePeriod",
				"StartLeaseTick",
				"NextLeasePeriod",
				"NextLeaseTick",
				"ContainmentUsage"
			}, null);
			this.ContainersGrid.RootTable.ChildTables[0].AddButtonColumn("UnleaseAsset", "End Lease", "");
			this.ContainersGrid.RootTable.ChildTables[0].AddPeriodTickFormatter("StartLeasePeriod", "StartLeaseTick", "");
			this.ContainersGrid.RootTable.ChildTables[0].AddPeriodTickFormatter("NextLeasePeriod", "NextLeaseTick", "");
			this.ContainersGrid.ColumnButtonClick += delegate(object sender, ColumnActionEventArgs e)
			{
				try
				{
					if (e.Column.Key == "LeaseAsset")
					{
						ServiceManager.Execute(delegate(IClientService p)
						{
							p.LeaseAsset(e.Column.GridEX.GetRow().Cells["Ticker"].Value.ToString());
						});
					}
					else
					{
						if (e.Column.Key == "UnleaseAsset")
						{
							ServiceManager.Execute(delegate(IClientService p)
							{
								p.UnleaseAsset((int)e.Column.GridEX.GetRow().Cells["ID"].Value);
							});
						}
					}
					e.Column.GridEX.GetRow().Expanded = true;
				}
				catch (FaultException ex)
				{
					DialogHelper.ShowError(ex.Message, "Error");
				}
			};
			this.ContainersGrid.RootTable.ChildTables[0].AddProgressBarFormatter("ContainmentUsage", delegate(DrawGridAreaEventArgs e)
			{
				if (e.Row.Cells["ContainmentUsage"].Value != System.DBNull.Value)
				{
					return System.Convert.ToSingle(e.Row.Cells["ContainmentUsage"].Value) / System.Convert.ToSingle(((TickerWeight)Game.State.AssetInfoTable.Rows.Find(e.Row.Cells["Ticker"].Value)["Containment"]).Weight);
				}
				return 0f;
			}, delegate(DrawGridAreaEventArgs e)
			{
				if (e.Row.Cells["ContainmentUsage"].Value != System.DBNull.Value)
				{
					return string.Format("{0} / {1}", e.Row.Cells["ContainmentUsage"].Value, ((TickerWeight)Game.State.AssetInfoTable.Rows.Find(e.Row.Cells["Ticker"].Value)["Containment"]).Weight);
				}
				return "";
			});
			this.ContainersGrid.CollapsingRow += delegate(object sender, RowActionCancelEventArgs e)
			{
				e.Cancel = true;
			};
			this.ContainersGrid.ResumeLayout();
			this.TransportersGrid.SuspendLayout();
			this.TransportersGrid.DataSource = Game.State.AssetTransporterView;
			this.TransportersGrid.RetrieveStructure(true);
			this.TransportersGrid.Format();
			this.TransportersGrid.AlternatingColors = false;
			this.TransportersGrid.RootTable.AddIListColumns(Game.State.AssetInfoTable);
			this.TransportersGrid.RootTable.FormatColumns(new string[]
			{
				"Ticker",
				"Type",
				"LeaseCount",
				"LeasePrice",
				"TicksPerLease",
				"ConvertFrom",
				"ConvertTo",
				"TicksPerConversion",
				"Realized"
			}, null);
			this.TransportersGrid.RootTable.AddButtonColumn("LeaseUseAsset", "Lease & Use", "");
			this.TransportersGrid.RootTable.AddProgressBarFormatter("LeaseCount", "TotalQuantity");
			this.TransportersGrid.RootTable.AddTickerArrayFormatter(new string[]
			{
				"ConvertFrom",
				"ConvertTo"
			});
			this.TransportersGrid.RootTable.AddTickerArrayFormatter(new string[]
			{
				"ConvertFrom",
				"ConvertTo"
			});
			this.TransportersGrid.RepeatHeaders = repeatHeaders;
			this.TransportersGrid.RootTable.RowFormatStyle = new GridEXFormatStyle(formatStyle);
			this.TransportersGrid.RootTable.RowHeight = rowHeight;
			this.TransportersGrid.RootTable.ChildTables[0].AddIListColumns(Game.State.AssetTable);
			this.TransportersGrid.RootTable.ChildTables[0].Columns.Add("Progress", ColumnType.Text);
			this.TransportersGrid.RootTable.ChildTables[0].FormatColumns(new string[]
			{
				"StartLeasePeriod",
				"StartLeaseTick",
				"ConvertFrom",
				"ConvertTo",
				"ConvertFinishPeriod",
				"ConvertFinishTick",
				"Progress"
			}, null);
			this.TransportersGrid.RootTable.ChildTables[0].AddButtonColumn("BackhaulAsset", "Backhaul", "");
			this.TransportersGrid.RootTable.ChildTables[0].AddPeriodTickFormatter("StartLeasePeriod", "StartLeaseTick", "");
			this.TransportersGrid.RootTable.ChildTables[0].AddPeriodTickFormatter("ConvertFinishPeriod", "ConvertFinishTick", "");
			this.TransportersGrid.RootTable.ChildTables[0].AddProgressBarFormatter("Progress", delegate(DrawGridAreaEventArgs e)
			{
				if (e.Row.Cells["ConvertFinishPeriod"].Value != System.DBNull.Value && e.Row.Cells["ConvertFinishTick"].Value != System.DBNull.Value)
				{
					return 1f - (float)(((int)e.Row.Cells["ConvertFinishPeriod"].Value - Game.State.Current.Period) * Game.State.General.TicksPerPeriod + (int)e.Row.Cells["ConvertFinishTick"].Value - Game.State.Current.Tick) / System.Convert.ToSingle((int)Game.State.AssetInfoTable.Rows.Find(e.Row.Cells["Ticker"].Value)["TicksPerConversion"]);
				}
				return 0f;
			}, null);
			this.TransportersGrid.ColumnButtonClick += delegate(object sender, ColumnActionEventArgs e)
			{
				try
				{
					string ticker = e.Column.GridEX.GetRow().Cells["Ticker"].Value.ToString();
					if (e.Column.Key == "LeaseUseAsset")
					{
						TickerWeight[] toconvert;
						if (this.ShowAssetUsageDialog("Use " + ticker, "Select amount to transport:", (TickerWeight[])Game.State.AssetInfoTable.Rows.Find(ticker)["ConvertFrom"], (TickerWeight[])Game.State.AssetInfoTable.Rows.Find(ticker)["ConvertTo"], out toconvert) == System.Windows.Forms.DialogResult.OK)
						{
							ServiceManager.Execute(delegate(IClientService p)
							{
								p.UseAsset(ticker, toconvert);
							});
						}
					}
					e.Column.GridEX.GetRow().Expanded = true;
				}
				catch (FaultException ex)
				{
					DialogHelper.ShowError(ex.Message, "Error");
				}
			};
			this.TransportersGrid.LoadingRow += delegate(object sender, RowLoadEventArgs e)
			{
				if (e.Row.DataRow == null)
				{
					return;
				}
				if (e.Row.Parent == null && e.Row.Cells["DisplayCost"].Value != System.DBNull.Value && !string.IsNullOrWhiteSpace(e.Row.Cells["DisplayCost"].Text))
				{
					e.Row.Cells["LeasePrice"].Text = e.Row.Cells["DisplayCost"].Text;
				}
			};
			this.TransportersGrid.CollapsingRow += delegate(object sender, RowActionCancelEventArgs e)
			{
				e.Cancel = true;
			};
			this.TransportersGrid.ResumeLayout();
			this.ConvertersGrid.SuspendLayout();
			this.ConvertersGrid.DataSource = Game.State.AssetConverterView;
			this.ConvertersGrid.RetrieveStructure(true);
			this.ConvertersGrid.Format();
			this.ConvertersGrid.AlternatingColors = false;
			this.ConvertersGrid.RootTable.AddIListColumns(Game.State.AssetInfoTable);
			this.ConvertersGrid.RootTable.FormatColumns(new string[]
			{
				"Ticker",
				"Type",
				"LeaseCount",
				"LeasePrice",
				"TicksPerLease",
				"ConvertFrom",
				"ConvertTo",
				"TicksPerConversion",
				"Realized"
			}, null);
			this.ConvertersGrid.RootTable.AddButtonColumn("LeaseAsset", "Lease", "");
			this.ConvertersGrid.RootTable.AddProgressBarFormatter("LeaseCount", "TotalQuantity");
			this.ConvertersGrid.RootTable.AddTickerArrayFormatter(new string[]
			{
				"ConvertFrom",
				"ConvertTo"
			});
			this.ConvertersGrid.RootTable.AddTickerArrayFormatter(new string[]
			{
				"ConvertFrom",
				"ConvertTo"
			});
			this.ConvertersGrid.RepeatHeaders = repeatHeaders;
			this.ConvertersGrid.RootTable.RowFormatStyle = new GridEXFormatStyle(formatStyle);
			this.ConvertersGrid.RootTable.RowHeight = rowHeight;
			this.ConvertersGrid.RootTable.ChildTables[0].AddIListColumns(Game.State.AssetTable);
			this.ConvertersGrid.RootTable.ChildTables[0].Columns.Add("Progress", ColumnType.Text);
			this.ConvertersGrid.RootTable.ChildTables[0].FormatColumns(new string[]
			{
				"StartLeasePeriod",
				"StartLeaseTick",
				"NextLeasePeriod",
				"NextLeaseTick",
				"ConvertFrom",
				"ConvertTo",
				"ConvertFinishPeriod",
				"ConvertFinishTick",
				"Progress"
			}, null);
			this.ConvertersGrid.RootTable.ChildTables[0].AddButtonColumn("UseAsset", "Use Asset", "");
			this.ConvertersGrid.RootTable.ChildTables[0].AddButtonColumn("UnleaseAsset", "End Lease", "");
			this.ConvertersGrid.RootTable.ChildTables[0].AddPeriodTickFormatter("StartLeasePeriod", "StartLeaseTick", "");
			this.ConvertersGrid.RootTable.ChildTables[0].AddPeriodTickFormatter("NextLeasePeriod", "NextLeaseTick", "");
			this.ConvertersGrid.RootTable.ChildTables[0].AddPeriodTickFormatter("ConvertFinishPeriod", "ConvertFinishTick", "");
			this.ConvertersGrid.RootTable.ChildTables[0].AddProgressBarFormatter("Progress", delegate(DrawGridAreaEventArgs e)
			{
				if (e.Row.Cells["ConvertFinishPeriod"].Value != System.DBNull.Value && e.Row.Cells["ConvertFinishTick"].Value != System.DBNull.Value)
				{
					return 1f - (float)(((int)e.Row.Cells["ConvertFinishPeriod"].Value - Game.State.Current.Period) * Game.State.General.TicksPerPeriod + (int)e.Row.Cells["ConvertFinishTick"].Value - Game.State.Current.Tick) / System.Convert.ToSingle((int)Game.State.AssetInfoTable.Rows.Find(e.Row.Cells["Ticker"].Value)["TicksPerConversion"]);
				}
				return 0f;
			}, null);
			this.ConvertersGrid.ColumnButtonClick += delegate(object sender, ColumnActionEventArgs e)
			{
				try
				{
					string ticker = e.Column.GridEX.GetRow().Cells["Ticker"].Value.ToString();
					if (e.Column.Key == "LeaseAsset")
					{
						ServiceManager.Execute(delegate(IClientService p)
						{
							p.LeaseAsset(ticker);
						});
					}
					else
					{
						if (e.Column.Key == "UnleaseAsset")
						{
							int id = (int)e.Column.GridEX.GetRow().Cells["ID"].Value;
							ServiceManager.Execute(delegate(IClientService p)
							{
								p.UnleaseAsset(id);
							});
						}
						else
						{
							if (e.Column.Key == "UseAsset")
							{
								int id = (int)e.Column.GridEX.GetRow().Cells["ID"].Value;
								TickerWeight[] toconvert;
								if (this.ShowAssetUsageDialog("Use " + ticker, "Select amount to convert:", (TickerWeight[])Game.State.AssetInfoTable.Rows.Find(ticker)["ConvertFrom"], (TickerWeight[])Game.State.AssetInfoTable.Rows.Find(ticker)["ConvertTo"], out toconvert) == System.Windows.Forms.DialogResult.OK)
								{
									ServiceManager.Execute(delegate(IClientService p)
									{
										p.UseLeasedAsset(id, toconvert);
									});
								}
							}
						}
					}
					e.Column.GridEX.GetRow().Expanded = true;
				}
				catch (FaultException ex)
				{
					DialogHelper.ShowError(ex.Message, "Error");
				}
			};
			this.ConvertersGrid.CollapsingRow += delegate(object sender, RowActionCancelEventArgs e)
			{
				e.Cancel = true;
			};
			this.ConvertersGrid.ResumeLayout();
			base.AddResetHandler(delegate
			{
				this.ContainersGrid.DataSource = Game.State.AssetContainerView;
				this.TransportersGrid.DataSource = Game.State.AssetTransporterView;
				this.ConvertersGrid.DataSource = Game.State.AssetConverterView;
				this.ProducersGrid.DataSource = Game.State.AssetProducerView;
				this.ContainersGrid.ExpandRecords();
				this.TransportersGrid.ExpandRecords();
				this.ConvertersGrid.ExpandRecords();
				this.ProducersGrid.ExpandRecords();
			});
			base.Load += delegate(object sender, System.EventArgs e)
			{
				this.ContainersGrid.ExpandRecords();
				this.TransportersGrid.ExpandRecords();
				this.ConvertersGrid.ExpandRecords();
				this.ProducersGrid.ExpandRecords();
			};
		}
		public System.Windows.Forms.DialogResult ShowAssetUsageDialog(string title, string caption, TickerWeight[] inputweights, TickerWeight[] outputweights, out TickerWeight[] userinputweights)
		{
			decimal minfraction = System.Math.Max(System.Math.Min((
				from x in inputweights
				select (decimal)Game.State.PortfolioTable.Rows.Find(x.Ticker)["Position"] / x.Weight).Min(), 1m), 0m);
			System.Windows.Forms.NumericUpDown[] udcontrols = inputweights.Select(delegate(TickerWeight x)
			{
				System.Windows.Forms.NumericUpDown numericUpDown = new System.Windows.Forms.NumericUpDown();
				numericUpDown.Maximum = x.Weight;
				numericUpDown.Value = x.Weight * minfraction;
				numericUpDown.Increment = x.Weight / MathHelper.GreatestCommonFactor(MathHelper.GreatestCommonFactor((
					from y in inputweights
					select y.Weight).ToArray<decimal>()), MathHelper.GreatestCommonFactor((
					from y in outputweights
					select y.Weight).ToArray<decimal>()));
				return numericUpDown;
			}).ToArray<System.Windows.Forms.NumericUpDown>();
			System.Windows.Forms.Control[] second = new System.Windows.Forms.Control[]
			{
				new System.Windows.Forms.PictureBox
				{
					SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize,
					Image = Resources.arrow_down,
					Anchor = System.Windows.Forms.AnchorStyles.None
				}
			};
			System.Windows.Forms.TextBox[] tbcontrols = (
				from x in outputweights
				select new System.Windows.Forms.TextBox
				{
					Text = System.Math.Round(x.Weight * minfraction).ToString("0"),
					ReadOnly = true
				}).ToArray<System.Windows.Forms.TextBox>();
			decimal[] weights = (
				from x in inputweights
				select x.Weight).Concat(
				from x in outputweights
				select x.Weight).ToArray<decimal>();
			for (int j = 0; j < udcontrols.Length; j++)
			{
				udcontrols[j].TextChanged += delegate(object sender, System.EventArgs e)
				{
					if (!((System.Windows.Forms.NumericUpDown)sender).Focused)
					{
						return;
					}
					for (int j = 0; j < weights.Length; j++)
					{
						if (j < udcontrols.Length)
						{
							udcontrols[j].Value = System.Convert.ToInt64(weights[j]) * System.Convert.ToInt64(((System.Windows.Forms.NumericUpDown)sender).Value) / System.Convert.ToInt64(((System.Windows.Forms.NumericUpDown)sender).Maximum);
						}
						else
						{
							tbcontrols[j - udcontrols.Length].Text = (System.Convert.ToInt64(weights[j]) * System.Convert.ToInt64(((System.Windows.Forms.NumericUpDown)sender).Value) / System.Convert.ToInt64(((System.Windows.Forms.NumericUpDown)sender).Maximum)).ToString("0");
						}
					}
				};
			}
			System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();
			list.AddRange(
				from x in inputweights
				select x.Ticker);
			list.Add(null);
			list.AddRange(
				from x in outputweights
				select x.Ticker);
			PromptDialog f = new PromptDialog(title, caption, udcontrols.Cast<System.Windows.Forms.Control>().Concat(second).Concat(tbcontrols).ToArray<System.Windows.Forms.Control>(), list.ToArray());
			System.Windows.Forms.DialogResult dialogResult = f.ShowDialog();
			if (dialogResult == System.Windows.Forms.DialogResult.OK)
			{
				userinputweights = inputweights.Select((TickerWeight x, int i) => new TickerWeight
				{
					Ticker = x.Ticker,
					Weight = f.GetValue<decimal>(i)
				}).ToArray<TickerWeight>();
			}
			else
			{
				userinputweights = null;
			}
			return dialogResult;
		}
	}
}
