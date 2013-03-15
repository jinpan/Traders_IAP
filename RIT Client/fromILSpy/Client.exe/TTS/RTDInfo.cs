using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
namespace TTS
{
	public class RTDInfo : System.Windows.Forms.Form
	{
		private const string RTDDefinitions = "\r\nGeneral\r\nTRADERID Trader's trader ID\r\nPL Overall Trader's P/L\r\nTRADERNAME Name\r\nTIMEREMAINING Time remaining in the period\r\nPERIOD The period # that is running\r\nYEARTIME Ticks in a year\r\nTIMESPEED Speed game is currently running\r\n\r\nAsset Information\r\nALLASSETTICKERS Comma-delimited list of all asset tickers\r\nALLASSETTICKERINFO Table of all asset tickers and detailed info\r\n\r\nSecurity Information\r\nALLTICKERS Comma-delimited list of all security tickers\r\nALLTICKERINFO Table of all security tickers and detailed info\r\nTICKER|LAST Last\r\nTICKER|BID Bid\r\nTICKER|ASK Ask\r\nTICKER|VOLUME Volume\r\nTICKER|POSITION Position\r\nTICKER|MKTSELL|N The VWAP that would occur if you send an order to market sell N volume\r\nTICKER|MKTBUY|N The VWAP that would occur if you send an order to market buy N volume\r\nTICKER|COST VWAP\r\nTICKER|PLUNR Unrealized P/L\r\nTICKER|PLREL Realized P/L\r\nTICKER|BIDBOOK Book view, bid side\r\nTICKER|ASKBOOK Book view, ask side\r\nTICKER|OPENORDERS Open personal orders to buy/sell\r\nTICKER|ALLORDERS All personal orders to buy/sell\r\nTICKER|BID|N The Nth bid in the book\r\nTICKER|BSZ|N The size of the Nth bid in the book\r\nTICKER|ASK|N The Nth ask in the book\r\nTICKER|ASZ|N The size of the Nth ask in the book\r\nTICKER|AGBID|N The aggregate (by price) Nth bid in the book\r\nTICKER|AGBSZ|N The size of the aggregate (by price) Nth bid in the book\r\nTICKER|AGASK|N The aggregate (by price) Nth ask in the book\r\nTICKER|AGASZ|N The size of the aggregate (by price) Nth ask in the book\r\nTICKER|INTERESTRATE The currency interest rate\r\nTICKER|LIMITORDERS The number of live limit orders\r\n\r\nNews Information\r\nNEWS|N Nth news item, most recent last\r\nLATESTNEWS|N Nth news item, most recent first\r\n";
		private IContainer components;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		public RTDInfo()
		{
			this.InitializeComponent();
			string[] array = "\r\nGeneral\r\nTRADERID Trader's trader ID\r\nPL Overall Trader's P/L\r\nTRADERNAME Name\r\nTIMEREMAINING Time remaining in the period\r\nPERIOD The period # that is running\r\nYEARTIME Ticks in a year\r\nTIMESPEED Speed game is currently running\r\n\r\nAsset Information\r\nALLASSETTICKERS Comma-delimited list of all asset tickers\r\nALLASSETTICKERINFO Table of all asset tickers and detailed info\r\n\r\nSecurity Information\r\nALLTICKERS Comma-delimited list of all security tickers\r\nALLTICKERINFO Table of all security tickers and detailed info\r\nTICKER|LAST Last\r\nTICKER|BID Bid\r\nTICKER|ASK Ask\r\nTICKER|VOLUME Volume\r\nTICKER|POSITION Position\r\nTICKER|MKTSELL|N The VWAP that would occur if you send an order to market sell N volume\r\nTICKER|MKTBUY|N The VWAP that would occur if you send an order to market buy N volume\r\nTICKER|COST VWAP\r\nTICKER|PLUNR Unrealized P/L\r\nTICKER|PLREL Realized P/L\r\nTICKER|BIDBOOK Book view, bid side\r\nTICKER|ASKBOOK Book view, ask side\r\nTICKER|OPENORDERS Open personal orders to buy/sell\r\nTICKER|ALLORDERS All personal orders to buy/sell\r\nTICKER|BID|N The Nth bid in the book\r\nTICKER|BSZ|N The size of the Nth bid in the book\r\nTICKER|ASK|N The Nth ask in the book\r\nTICKER|ASZ|N The size of the Nth ask in the book\r\nTICKER|AGBID|N The aggregate (by price) Nth bid in the book\r\nTICKER|AGBSZ|N The size of the aggregate (by price) Nth bid in the book\r\nTICKER|AGASK|N The aggregate (by price) Nth ask in the book\r\nTICKER|AGASZ|N The size of the aggregate (by price) Nth ask in the book\r\nTICKER|INTERESTRATE The currency interest rate\r\nTICKER|LIMITORDERS The number of live limit orders\r\n\r\nNews Information\r\nNEWS|N Nth news item, most recent last\r\nLATESTNEWS|N Nth news item, most recent first\r\n".Split(new string[]
			{
				System.Environment.NewLine
			}, System.StringSplitOptions.None);
			bool isheader = false;
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				string text = array2[i];
				if (string.IsNullOrWhiteSpace(text))
				{
					isheader = true;
				}
				else
				{
					this.AddRTDLine(text, isheader);
					isheader = false;
				}
			}
		}
		private void AddRTDLine(string line, bool isheader)
		{
			string[] array = line.Split(new char[]
			{
				' '
			});
			string[] array2 = array[0].Split(new char[]
			{
				'|'
			});
			string text = (array.Length > 1) ? string.Join(" ", array.Skip(1)) : "";
			int num = 2;
			this.tableLayoutPanel1.RowCount++;
			this.tableLayoutPanel1.SetRow(this.panel1, this.tableLayoutPanel1.RowCount - 1);
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.AutoSize));
			if (isheader)
			{
				System.Windows.Forms.Label control = new System.Windows.Forms.Label
				{
					AutoSize = true,
					Text = line.Trim(),
					Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0)
				};
				this.tableLayoutPanel1.Controls.Add(control, 0, this.tableLayoutPanel1.RowCount - num);
				this.tableLayoutPanel1.SetColumnSpan(control, 4);
				return;
			}
			if (array2.Length > 0)
			{
				this.tableLayoutPanel1.Controls.Add(new System.Windows.Forms.Label
				{
					AutoSize = true,
					Text = array2[0],
					Font = new System.Drawing.Font("Tahoma", 8.25f, (array2[0] == "TICKER") ? System.Drawing.FontStyle.Italic : System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0)
				}, 0, this.tableLayoutPanel1.RowCount - num);
			}
			if (array2.Length > 1)
			{
				this.tableLayoutPanel1.Controls.Add(new System.Windows.Forms.Label
				{
					AutoSize = true,
					Text = array2[1],
					Font = new System.Drawing.Font("Tahoma", 8.25f, (array2[1] == "N") ? System.Drawing.FontStyle.Italic : System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0)
				}, 1, this.tableLayoutPanel1.RowCount - num);
			}
			if (array2.Length > 2)
			{
				this.tableLayoutPanel1.Controls.Add(new System.Windows.Forms.Label
				{
					AutoSize = true,
					Text = array2[2],
					Font = new System.Drawing.Font("Tahoma", 8.25f, (array2[2] == "N") ? System.Drawing.FontStyle.Italic : System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0)
				}, 2, this.tableLayoutPanel1.RowCount - num);
			}
			if (!string.IsNullOrEmpty(text))
			{
				this.tableLayoutPanel1.Controls.Add(new System.Windows.Forms.Label
				{
					AutoSize = true,
					Text = text
				}, 3, this.tableLayoutPanel1.RowCount - num);
			}
		}
		private void button1_Click(object sender, System.EventArgs e)
		{
			System.Windows.Forms.Clipboard.SetText("\r\nGeneral\r\nTRADERID Trader's trader ID\r\nPL Overall Trader's P/L\r\nTRADERNAME Name\r\nTIMEREMAINING Time remaining in the period\r\nPERIOD The period # that is running\r\nYEARTIME Ticks in a year\r\nTIMESPEED Speed game is currently running\r\n\r\nAsset Information\r\nALLASSETTICKERS Comma-delimited list of all asset tickers\r\nALLASSETTICKERINFO Table of all asset tickers and detailed info\r\n\r\nSecurity Information\r\nALLTICKERS Comma-delimited list of all security tickers\r\nALLTICKERINFO Table of all security tickers and detailed info\r\nTICKER|LAST Last\r\nTICKER|BID Bid\r\nTICKER|ASK Ask\r\nTICKER|VOLUME Volume\r\nTICKER|POSITION Position\r\nTICKER|MKTSELL|N The VWAP that would occur if you send an order to market sell N volume\r\nTICKER|MKTBUY|N The VWAP that would occur if you send an order to market buy N volume\r\nTICKER|COST VWAP\r\nTICKER|PLUNR Unrealized P/L\r\nTICKER|PLREL Realized P/L\r\nTICKER|BIDBOOK Book view, bid side\r\nTICKER|ASKBOOK Book view, ask side\r\nTICKER|OPENORDERS Open personal orders to buy/sell\r\nTICKER|ALLORDERS All personal orders to buy/sell\r\nTICKER|BID|N The Nth bid in the book\r\nTICKER|BSZ|N The size of the Nth bid in the book\r\nTICKER|ASK|N The Nth ask in the book\r\nTICKER|ASZ|N The size of the Nth ask in the book\r\nTICKER|AGBID|N The aggregate (by price) Nth bid in the book\r\nTICKER|AGBSZ|N The size of the aggregate (by price) Nth bid in the book\r\nTICKER|AGASK|N The aggregate (by price) Nth ask in the book\r\nTICKER|AGASZ|N The size of the aggregate (by price) Nth ask in the book\r\nTICKER|INTERESTRATE The currency interest rate\r\nTICKER|LIMITORDERS The number of live limit orders\r\n\r\nNews Information\r\nNEWS|N Nth news item, most recent last\r\nLATESTNEWS|N Nth news item, most recent first\r\n");
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label6 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.panel1.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			base.SuspendLayout();
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 4;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.label6, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label4, 2, 3);
			this.tableLayoutPanel1.Controls.Add(this.label5, 3, 3);
			this.tableLayoutPanel1.Controls.Add(this.label7, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 4);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.Size = new System.Drawing.Size(320, 262);
			this.tableLayoutPanel1.TabIndex = 0;
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label6.Location = new System.Drawing.Point(13, 49);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(50, 13);
			this.label6.TabIndex = 5;
			this.label6.Text = "[Field1]";
			this.label1.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.label1, 4);
			this.label1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new System.Drawing.Point(13, 10);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(82, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Excel Syntax:";
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new System.Drawing.Point(69, 49);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(50, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "[Field2]";
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label3.Location = new System.Drawing.Point(13, 36);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(0, 13);
			this.label3.TabIndex = 2;
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label4.Location = new System.Drawing.Point(125, 49);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(50, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "[Field3]";
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label5.Location = new System.Drawing.Point(181, 49);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(71, 13);
			this.label5.TabIndex = 4;
			this.label5.Text = "Description";
			this.label7.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.label7, 4);
			this.label7.Location = new System.Drawing.Point(13, 23);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(246, 13);
			this.label7.TabIndex = 6;
			this.label7.Text = string.Format("=RTD(\"{0}\"{1}{1}\"[Field1]\"{1}\"[Field2]\"{1}\"[Field3]\")", Client.Skin.GetString("rtd_string").ToUpper(), System.Globalization.CultureInfo.CurrentCulture.TextInfo.ListSeparator);
			this.panel1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.panel1.AutoSize = true;
			this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.SetColumnSpan(this.panel1, 4);
			this.panel1.Controls.Add(this.tableLayoutPanel2);
			this.panel1.Location = new System.Drawing.Point(45, 135);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(230, 43);
			this.panel1.TabIndex = 7;
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this.button1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.button2, 1, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(230, 43);
			this.tableLayoutPanel2.TabIndex = 0;
			this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.button1.Location = new System.Drawing.Point(10, 10);
			this.button1.Margin = new System.Windows.Forms.Padding(10);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(115, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Copy To Clipboard";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			this.button2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button2.Location = new System.Drawing.Point(145, 10);
			this.button2.Margin = new System.Windows.Forms.Padding(10);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 0;
			this.button2.Text = "OK";
			this.button2.UseVisualStyleBackColor = true;
			base.AcceptButton = this.button2;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			base.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			base.ClientSize = new System.Drawing.Size(320, 262);
			base.Controls.Add(this.tableLayoutPanel1);
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "RTDInfo";
			base.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "RTD Info";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}
	}
}
