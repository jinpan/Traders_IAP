using Janus.Windows.GridEX;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.ServiceModel;
using System.Windows.Forms;
namespace TTS
{
	public class News : TTSForm
	{
		public static WindowType TTSWindowType = WindowType.NEWS;
		private IContainer components;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private GridEX NewsGrid;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label HeadlineLabel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label TickerLabel;
		private System.Windows.Forms.Label TimeLabel;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.WebBrowser BodyBrowser;
		public News(TTSFormState state) : base(state)
		{
			this.InitializeComponent();
			this.HeadlineLabel.Text = "";
			this.TickerLabel.Text = "";
			this.TimeLabel.Text = "";
			this.BodyBrowser.DocumentText = "";
			this.NewsGrid.BindingContext = new System.Windows.Forms.BindingContext();
			this.NewsGrid.SetDataBinding(Game.State.NewsView, "");
			this.NewsGrid.RetrieveStructure();
			this.NewsGrid.Format();
			this.NewsGrid.RootTable.FormatColumns(new string[]
			{
				"Period",
				"Tick",
				"Ticker",
				"Headline"
			}, new string[]
			{
				"Period",
				"Tick",
				"",
				"Headline"
			});
			this.NewsGrid.RootTable.AddPeriodTickFormatter("Period", "Tick", "");
			this.NewsGrid.RootTable.Columns["Tick"].Width = 20;
			this.NewsGrid.RootTable.Columns["Ticker"].Width = 20;
			this.NewsGrid.RootTable.FormatConditions.Add(new GridEXFormatCondition(this.NewsGrid.RootTable.Columns["IsRead"], ConditionOperator.Equal, false)
			{
				FormatStyle = new GridEXFormatStyle(ColorHelper.RowStyleYellow)
			});
			this.NewsGrid.RootTable.FormatConditions.Add(new GridEXFormatCondition(this.NewsGrid.RootTable.Columns["ColorState"], ConditionOperator.Equal, 1)
			{
				FormatStyle = new GridEXFormatStyle(ColorHelper.RowStyleRed)
			});
			this.NewsGrid.SelectedFormatStyle.BackColor = System.Drawing.SystemColors.Highlight;
			this.NewsGrid.SelectedFormatStyle.ForeColor = System.Drawing.SystemColors.HighlightText;
			this.NewsGrid.SelectionChanged += delegate(object sender, System.EventArgs e)
			{
				this.SelectionChanged();
			};
			base.AddResetHandler(delegate
			{
				this.NewsGrid.DataSource = Game.State.NewsView;
			});
			base.ResizeEnd += delegate(object sender, System.EventArgs e)
			{
				this.SetHeadlineSize();
			};
			base.Load += delegate(object sender, System.EventArgs e)
			{
				this.SetHeadlineSize();
				if (this.NewsGrid.RowCount > 0)
				{
					this.NewsGrid.FirstRow = 0;
					this.NewsGrid.Row = 0;
					this.SelectionChanged();
				}
			};
		}
		private void SetHeadlineSize()
		{
			this.HeadlineLabel.MaximumSize = new System.Drawing.Size(base.Width - this.label4.Width - 50, 0);
			this.HeadlineLabel.AutoSize = false;
			this.HeadlineLabel.AutoSize = true;
		}
		private void SelectionChanged()
		{
			if (this.NewsGrid.GetRow() == null)
			{
				this.HeadlineLabel.Text = "";
				this.TickerLabel.Text = "";
				this.TimeLabel.Text = "";
				this.BodyBrowser.DocumentText = "";
				return;
			}
			base.BeginInvoke(delegate
			{
				DataRow row = ((DataRowView)this.NewsGrid.GetRow().DataRow).Row;
				if (!(bool)row["IsRead"])
				{
					row["IsRead"] = true;
					try
					{
						ServiceManager.Execute(delegate(IClientService p)
						{
							p.ReadNews((int)this.NewsGrid.GetRow().Cells["ID"].Value);
						});
					}
					catch (FaultException)
					{
					}
				}
				this.HeadlineLabel.Text = row["Headline"].ToString();
				this.TickerLabel.Text = row["Ticker"].ToString();
				this.TimeLabel.Text = this.NewsGrid.GetRow().Cells["Tick"].Text;
				this.BodyBrowser.DocumentText = string.Format("<p style='font-family: Tahoma, Helvetica, Arial, sans-serif; font-size: x-small;'>{0}</p>", string.IsNullOrWhiteSpace(row["Body"].ToString()) ? row["Headline"].ToString() : row["Body"].ToString());
			});
		}
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
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(News));
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.NewsGrid = new GridEX();
			this.panel1 = new System.Windows.Forms.Panel();
			this.BodyBrowser = new System.Windows.Forms.WebBrowser();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.label5 = new System.Windows.Forms.Label();
			this.TimeLabel = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.HeadlineLabel = new System.Windows.Forms.Label();
			this.TickerLabel = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			((ISupportInitialize)this.splitContainer1).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((ISupportInitialize)this.NewsGrid).BeginInit();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			base.SuspendLayout();
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.splitContainer1.Panel1.Controls.Add(this.NewsGrid);
			this.splitContainer1.Panel2.Controls.Add(this.panel1);
			this.splitContainer1.Panel2.Controls.Add(this.tableLayoutPanel1);
			this.splitContainer1.Size = new System.Drawing.Size(455, 262);
			this.splitContainer1.SplitterDistance = 100;
			this.splitContainer1.TabIndex = 0;
			this.NewsGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.NewsGrid.Location = new System.Drawing.Point(0, 0);
			this.NewsGrid.Name = "NewsGrid";
			this.NewsGrid.Size = new System.Drawing.Size(455, 100);
			this.NewsGrid.TabIndex = 0;
			this.panel1.Controls.Add(this.BodyBrowser);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 32);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(455, 126);
			this.panel1.TabIndex = 1;
			this.BodyBrowser.AllowWebBrowserDrop = false;
			this.BodyBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
			this.BodyBrowser.IsWebBrowserContextMenuEnabled = false;
			this.BodyBrowser.Location = new System.Drawing.Point(0, 0);
			this.BodyBrowser.MinimumSize = new System.Drawing.Size(20, 20);
			this.BodyBrowser.Name = "BodyBrowser";
			this.BodyBrowser.ScriptErrorsSuppressed = true;
			this.BodyBrowser.Size = new System.Drawing.Size(455, 126);
			this.BodyBrowser.TabIndex = 0;
			this.BodyBrowser.WebBrowserShortcutsEnabled = false;
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.HeadlineLabel, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.TickerLabel, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(3);
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(455, 32);
			this.tableLayoutPanel1.TabIndex = 0;
			this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.flowLayoutPanel1.Controls.Add(this.label5);
			this.flowLayoutPanel1.Controls.Add(this.TimeLabel);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(380, 3);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(72, 13);
			this.flowLayoutPanel1.TabIndex = 2;
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label5.Location = new System.Drawing.Point(3, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(38, 13);
			this.label5.TabIndex = 2;
			this.label5.Text = "Time:";
			this.TimeLabel.AutoSize = true;
			this.TimeLabel.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.TimeLabel.Location = new System.Drawing.Point(47, 0);
			this.TimeLabel.Name = "TimeLabel";
			this.TimeLabel.Size = new System.Drawing.Size(22, 13);
			this.TimeLabel.TabIndex = 2;
			this.TimeLabel.Text = "???";
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label4.Location = new System.Drawing.Point(6, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(59, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Headline:";
			this.HeadlineLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.HeadlineLabel.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.HeadlineLabel, 2);
			this.HeadlineLabel.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.HeadlineLabel.Location = new System.Drawing.Point(71, 16);
			this.HeadlineLabel.Name = "HeadlineLabel";
			this.HeadlineLabel.Size = new System.Drawing.Size(22, 13);
			this.HeadlineLabel.TabIndex = 2;
			this.HeadlineLabel.Text = "???";
			this.TickerLabel.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.TickerLabel.AutoSize = true;
			this.TickerLabel.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.TickerLabel.Location = new System.Drawing.Point(71, 3);
			this.TickerLabel.Name = "TickerLabel";
			this.TickerLabel.Size = new System.Drawing.Size(22, 13);
			this.TickerLabel.TabIndex = 0;
			this.TickerLabel.Text = "???";
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new System.Drawing.Point(6, 3);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(45, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Ticker:";
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new System.Drawing.Size(455, 262);
			base.Controls.Add(this.splitContainer1);
			base.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.Name = "News";
			this.Text = "News";
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((ISupportInitialize)this.splitContainer1).EndInit();
			this.splitContainer1.ResumeLayout(false);
			((ISupportInitialize)this.NewsGrid).EndInit();
			this.panel1.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			base.ResumeLayout(false);
		}
	}
}
