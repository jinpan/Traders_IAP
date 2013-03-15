using Janus.Windows.GridEX;
using MRG.Controls.UI;
using ProtoBuf;
using SnapFormExtender;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Deployment.Application;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Threading;
using System.Timers;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using TTS.Properties;
namespace TTS
{
	public class Client : System.Windows.Forms.Form
	{
		public enum UIState
		{
			NONE,
			LOGIN,
			LOGIN_CONNECTING,
			NEW_USER,
			NEW_USER_CONNECTING,
			ACTIVE
		}
		private IContainer components;
		private System.Windows.Forms.TableLayoutPanel LoginTableLayout;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox LoginTraderID;
		private System.Windows.Forms.TextBox LoginPassword;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox LoginAddress;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.NumericUpDown LoginPort;
		private System.Windows.Forms.TableLayoutPanel LoginTableLayoutContainer;
		private System.Windows.Forms.Button LoginButton;
		private System.Windows.Forms.Panel LoginPanel;
		private LoadingCircle LoginLoadingCircle;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel NewUserPanel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
		private System.Windows.Forms.Button NewUserCancel;
		private LoadingCircle NewUserLoadingCircle;
		private System.Windows.Forms.TextBox NewUserLastName;
		private System.Windows.Forms.TextBox NewUserFirstName;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox NewUserPasswordConfirm;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox NewUserTraderID;
		private System.Windows.Forms.TextBox NewUserPassword;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TextBox NewUserAddress;
		private System.Windows.Forms.NumericUpDown NewUserPort;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Button NewUserButton;
		private System.Windows.Forms.Button LoginNewUser;
		private System.Windows.Forms.ErrorProvider NewUserErrorProvider;
		private System.Windows.Forms.StatusStrip MainStatusStrip;
		private System.Windows.Forms.ToolStripDropDownButton WorkspacesDropDown;
		private System.Windows.Forms.ToolStripDropDownButton WindowsDropDown;
		private System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeOrderEntriesToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tileHorizontallyToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem tileVerticallyToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem newWorkspaceToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem removeCurrentWorkspaceToolStripMenuItem;
		private SnapFormExtender SnapForm;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
		private BindableToolStripStatusLabel MainPeriodStatus;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
		private BindableToolStripStatusLabel MainTickStatus;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
		private BindableToolStripStatusLabel MainTotalTickStatus;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
		private BindableToolStripStatusLabel MainGameStatus;
		private System.Windows.Forms.ToolStripProgressBar MainTickProgressBar;
		private BindableToolStripStatusLabel NewsLabel;
		private BindableToolStripStatusLabel MainStatusLabel;
		private BindableToolStripStatusLabel GameLabel;
		private System.Windows.Forms.Label NewUserLoadingStatus;
		private System.Windows.Forms.Label LoginLoadingStatus;
		private BindableToolStripStatusLabel bindableToolStripStatusLabel1;
		private BindableToolStripStatusLabel MainTimeRemaining;
		private System.Windows.Forms.Panel InfoPanel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.PictureBox pictureBox3;
		private System.Windows.Forms.Label InfoPanelBody;
		private System.Windows.Forms.Label InfoPanelTitle;
		private System.Windows.Forms.Timer InfoPanelTimer;
		private BindableToolStripStatusLabel OTCLabel;
		private BindableToolStripStatusLabel ChatLabel;
		private System.Windows.Forms.MenuStrip MainMenu;
		private TTSFormMenuItem portfolioToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem orderEntryToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem marketDepthToolStripMenuItem;
		private TTSFormMenuItem chatToolStripMenuItem;
		private TTSFormMenuItem killMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem logoutToolStripMenuItem;
		private TTSFormMenuItem buySellEntryToolStripMenuItem;
		private TTSFormMenuItem spreadEntryToolStripMenuItem;
		private TTSFormMenuItem transportationArbitrageEntryToolStripMenuItem;
		private TTSFormMenuItem oTCEntryToolStripMenuItem;
		private TTSFormMenuItem assetsToolStripMenuItem;
		private TTSFormMenuItem traderInfoToolStripMenuItem;
		private TTSFormMenuItem bookTraderMenuItem;
		private TTSFormMenuItem ladderTraderMenuItem;
		private TTSFormMenuItem tradeBlotterToolStripMenuItem;
		private TTSFormMenuItem transactionLogToolStripMenuItem;
		private TTSFormMenuItem newsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem chartingToolStripMenuItem;
		private TTSFormMenuItem pNLChartingToolStripMenuItem;
		private TTSFormMenuItem securityChartingToolStripMenuItem;
		private BindableToolStripStatusLabel RTDLabel;
		private System.Windows.Forms.Timer InfoPanelShowTimer;
		private BindableToolStripStatusLabel APILabel;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.NumericUpDown numericUpDown2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ToolStripMenuItem saveWorkspaceToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadWorkspaceToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.SaveFileDialog SaveWorkspaceDialog;
		private System.Windows.Forms.OpenFileDialog LoadWorkspaceDialog;
		private TTSFormMenuItem timeSalesToolStripMenuItem;
		private System.Windows.Forms.Panel ReportPanel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
		private System.Windows.Forms.PictureBox pictureBox4;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.LinkLabel TraderReportDownloadLink;
		private System.Windows.Forms.SaveFileDialog ReportDownloadDialog;
		private System.Windows.Forms.LinkLabel TraderReportOpenLink;
		private TTSFormMenuItem electricityChartingToolStripMenuItem;
		public static Client MainThread;
		public static System.Resources.ResourceManager Skin;
		public Client.UIState CurrentUIState;
		private Client.UIState LastUIState = Client.UIState.LOGIN;
		private System.Timers.Timer AutoBotTimer = new System.Timers.Timer();
		private bool AutoBotInit;
		private System.Random AutoBotRandom = new System.Random();
		private string LastDownloadedReport = "";
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
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(Client));
			this.LoginTableLayout = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			this.LoginButton = new System.Windows.Forms.Button();
			this.LoginNewUser = new System.Windows.Forms.Button();
			this.LoginLoadingCircle = new LoadingCircle();
			this.LoginLoadingStatus = new System.Windows.Forms.Label();
			this.LoginAddress = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.LoginTraderID = new System.Windows.Forms.TextBox();
			this.LoginPassword = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.LoginPort = new System.Windows.Forms.NumericUpDown();
			this.LoginTableLayoutContainer = new System.Windows.Forms.TableLayoutPanel();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.LoginPanel = new System.Windows.Forms.Panel();
			this.NewUserPanel = new System.Windows.Forms.Panel();
			this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
			this.NewUserButton = new System.Windows.Forms.Button();
			this.NewUserCancel = new System.Windows.Forms.Button();
			this.NewUserLoadingCircle = new LoadingCircle();
			this.NewUserLoadingStatus = new System.Windows.Forms.Label();
			this.NewUserLastName = new System.Windows.Forms.TextBox();
			this.NewUserFirstName = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.NewUserPasswordConfirm = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.NewUserTraderID = new System.Windows.Forms.TextBox();
			this.NewUserPassword = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.NewUserAddress = new System.Windows.Forms.TextBox();
			this.NewUserPort = new System.Windows.Forms.NumericUpDown();
			this.label18 = new System.Windows.Forms.Label();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.NewUserErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
			this.MainGameStatus = new BindableToolStripStatusLabel();
			this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
			this.MainPeriodStatus = new BindableToolStripStatusLabel();
			this.bindableToolStripStatusLabel1 = new BindableToolStripStatusLabel();
			this.MainTimeRemaining = new BindableToolStripStatusLabel();
			this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
			this.MainTickStatus = new BindableToolStripStatusLabel();
			this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
			this.MainTotalTickStatus = new BindableToolStripStatusLabel();
			this.MainTickProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.GameLabel = new BindableToolStripStatusLabel();
			this.MainStatusLabel = new BindableToolStripStatusLabel();
			this.APILabel = new BindableToolStripStatusLabel();
			this.RTDLabel = new BindableToolStripStatusLabel();
			this.ChatLabel = new BindableToolStripStatusLabel();
			this.OTCLabel = new BindableToolStripStatusLabel();
			this.NewsLabel = new BindableToolStripStatusLabel();
			this.WorkspacesDropDown = new System.Windows.Forms.ToolStripDropDownButton();
			this.newWorkspaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.removeCurrentWorkspaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.saveWorkspaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadWorkspaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.WindowsDropDown = new System.Windows.Forms.ToolStripDropDownButton();
			this.closeAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeOrderEntriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tileHorizontallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tileVerticallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.InfoPanel = new System.Windows.Forms.Panel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.InfoPanelBody = new System.Windows.Forms.Label();
			this.pictureBox3 = new System.Windows.Forms.PictureBox();
			this.InfoPanelTitle = new System.Windows.Forms.Label();
			this.InfoPanelTimer = new System.Windows.Forms.Timer(this.components);
			this.MainMenu = new System.Windows.Forms.MenuStrip();
			this.portfolioToolStripMenuItem = new TTSFormMenuItem();
			this.orderEntryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.buySellEntryToolStripMenuItem = new TTSFormMenuItem();
			this.spreadEntryToolStripMenuItem = new TTSFormMenuItem();
			this.transportationArbitrageEntryToolStripMenuItem = new TTSFormMenuItem();
			this.oTCEntryToolStripMenuItem = new TTSFormMenuItem();
			this.marketDepthToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bookTraderMenuItem = new TTSFormMenuItem();
			this.ladderTraderMenuItem = new TTSFormMenuItem();
			this.tradeBlotterToolStripMenuItem = new TTSFormMenuItem();
			this.assetsToolStripMenuItem = new TTSFormMenuItem();
			this.transactionLogToolStripMenuItem = new TTSFormMenuItem();
			this.newsToolStripMenuItem = new TTSFormMenuItem();
			this.traderInfoToolStripMenuItem = new TTSFormMenuItem();
			this.timeSalesToolStripMenuItem = new TTSFormMenuItem();
			this.chartingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pNLChartingToolStripMenuItem = new TTSFormMenuItem();
			this.securityChartingToolStripMenuItem = new TTSFormMenuItem();
			this.electricityChartingToolStripMenuItem = new TTSFormMenuItem();
			this.chatToolStripMenuItem = new TTSFormMenuItem();
			this.killMenuItem1 = new TTSFormMenuItem();
			this.logoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.InfoPanelShowTimer = new System.Windows.Forms.Timer(this.components);
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.SaveWorkspaceDialog = new System.Windows.Forms.SaveFileDialog();
			this.LoadWorkspaceDialog = new System.Windows.Forms.OpenFileDialog();
			this.ReportPanel = new System.Windows.Forms.Panel();
			this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
			this.TraderReportOpenLink = new System.Windows.Forms.LinkLabel();
			this.pictureBox4 = new System.Windows.Forms.PictureBox();
			this.label8 = new System.Windows.Forms.Label();
			this.TraderReportDownloadLink = new System.Windows.Forms.LinkLabel();
			this.ReportDownloadDialog = new System.Windows.Forms.SaveFileDialog();
			this.SnapForm = new SnapFormExtender(this.components);
			this.LoginTableLayout.SuspendLayout();
			this.flowLayoutPanel1.SuspendLayout();
			((ISupportInitialize)this.LoginPort).BeginInit();
			this.LoginTableLayoutContainer.SuspendLayout();
			((ISupportInitialize)this.pictureBox1).BeginInit();
			this.LoginPanel.SuspendLayout();
			this.NewUserPanel.SuspendLayout();
			this.tableLayoutPanel3.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.flowLayoutPanel3.SuspendLayout();
			((ISupportInitialize)this.NewUserPort).BeginInit();
			((ISupportInitialize)this.pictureBox2).BeginInit();
			((ISupportInitialize)this.NewUserErrorProvider).BeginInit();
			this.MainStatusStrip.SuspendLayout();
			this.InfoPanel.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			((ISupportInitialize)this.pictureBox3).BeginInit();
			this.MainMenu.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((ISupportInitialize)this.numericUpDown2).BeginInit();
			((ISupportInitialize)this.numericUpDown1).BeginInit();
			this.ReportPanel.SuspendLayout();
			this.tableLayoutPanel4.SuspendLayout();
			((ISupportInitialize)this.pictureBox4).BeginInit();
			((ISupportInitialize)this.SnapForm).BeginInit();
			base.SuspendLayout();
			this.LoginTableLayout.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.LoginTableLayout.AutoSize = true;
			this.LoginTableLayout.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.LoginTableLayout.ColumnCount = 3;
			this.LoginTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.LoginTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.LoginTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.LoginTableLayout.Controls.Add(this.flowLayoutPanel1, 1, 4);
			this.LoginTableLayout.Controls.Add(this.LoginAddress, 1, 3);
			this.LoginTableLayout.Controls.Add(this.label3, 0, 3);
			this.LoginTableLayout.Controls.Add(this.label1, 0, 1);
			this.LoginTableLayout.Controls.Add(this.LoginTraderID, 1, 1);
			this.LoginTableLayout.Controls.Add(this.LoginPassword, 1, 2);
			this.LoginTableLayout.Controls.Add(this.label2, 0, 2);
			this.LoginTableLayout.Controls.Add(this.label4, 1, 0);
			this.LoginTableLayout.Controls.Add(this.LoginPort, 2, 3);
			this.LoginTableLayout.Location = new System.Drawing.Point(289, 66);
			this.LoginTableLayout.Name = "LoginTableLayout";
			this.LoginTableLayout.RowCount = 5;
			this.LoginTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.LoginTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.LoginTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.LoginTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.LoginTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.LoginTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
			this.LoginTableLayout.Size = new System.Drawing.Size(390, 149);
			this.LoginTableLayout.TabIndex = 0;
			this.flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.flowLayoutPanel1.AutoSize = true;
			this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.LoginTableLayout.SetColumnSpan(this.flowLayoutPanel1, 2);
			this.flowLayoutPanel1.Controls.Add(this.LoginButton);
			this.flowLayoutPanel1.Controls.Add(this.LoginNewUser);
			this.flowLayoutPanel1.Controls.Add(this.LoginLoadingCircle);
			this.flowLayoutPanel1.Controls.Add(this.LoginLoadingStatus);
			this.flowLayoutPanel1.Location = new System.Drawing.Point(70, 120);
			this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel1.Name = "flowLayoutPanel1";
			this.flowLayoutPanel1.Size = new System.Drawing.Size(197, 29);
			this.flowLayoutPanel1.TabIndex = 4;
			this.LoginButton.Location = new System.Drawing.Point(3, 3);
			this.LoginButton.Name = "LoginButton";
			this.LoginButton.Size = new System.Drawing.Size(75, 23);
			this.LoginButton.TabIndex = 0;
			this.LoginButton.Text = "Login";
			this.LoginButton.UseVisualStyleBackColor = true;
			this.LoginButton.Click += new System.EventHandler(this.LoginButton_Click);
			this.LoginButton.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Login_KeyPress);
			this.LoginNewUser.Location = new System.Drawing.Point(84, 3);
			this.LoginNewUser.Name = "LoginNewUser";
			this.LoginNewUser.Size = new System.Drawing.Size(75, 23);
			this.LoginNewUser.TabIndex = 1;
			this.LoginNewUser.Text = "New User";
			this.LoginNewUser.UseVisualStyleBackColor = true;
			this.LoginNewUser.Click += new System.EventHandler(this.LoginNewUser_Click);
			this.LoginLoadingCircle.Active = true;
			this.LoginLoadingCircle.Color = System.Drawing.Color.DarkGray;
			this.LoginLoadingCircle.InnerCircleRadius = 8;
			this.LoginLoadingCircle.Location = new System.Drawing.Point(165, 3);
			this.LoginLoadingCircle.Name = "LoginLoadingCircle";
			this.LoginLoadingCircle.NumberSpoke = 24;
			this.LoginLoadingCircle.OuterCircleRadius = 9;
			this.LoginLoadingCircle.RotationSpeed = 100;
			this.LoginLoadingCircle.Size = new System.Drawing.Size(23, 23);
			this.LoginLoadingCircle.SpokeThickness = 4;
			this.LoginLoadingCircle.StylePreset = LoadingCircle.StylePresets.IE7;
			this.LoginLoadingCircle.TabIndex = 2;
			this.LoginLoadingCircle.Visible = false;
			this.LoginLoadingStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.LoginLoadingStatus.AutoSize = true;
			this.LoginLoadingStatus.Location = new System.Drawing.Point(194, 8);
			this.LoginLoadingStatus.Name = "LoginLoadingStatus";
			this.LoginLoadingStatus.Size = new System.Drawing.Size(0, 13);
			this.LoginLoadingStatus.TabIndex = 4;
			this.LoginAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", Settings.Default, "ServerAddress", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.LoginAddress.Location = new System.Drawing.Point(73, 96);
			this.LoginAddress.Name = "LoginAddress";
			this.LoginAddress.Size = new System.Drawing.Size(228, 21);
			this.LoginAddress.TabIndex = 2;
			this.LoginAddress.Text = Settings.Default.ServerAddress;
			this.LoginAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Login_KeyPress);
			this.label3.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label3.Location = new System.Drawing.Point(19, 100);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(48, 13);
			this.label3.TabIndex = 5;
			this.label3.Text = "Server:";
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new System.Drawing.Point(3, 46);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Trader ID:";
			this.LoginTableLayout.SetColumnSpan(this.LoginTraderID, 2);
			this.LoginTraderID.Location = new System.Drawing.Point(73, 42);
			this.LoginTraderID.Name = "LoginTraderID";
			this.LoginTraderID.Size = new System.Drawing.Size(314, 21);
			this.LoginTraderID.TabIndex = 0;
			this.LoginTraderID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Login_KeyPress);
			this.LoginTableLayout.SetColumnSpan(this.LoginPassword, 2);
			this.LoginPassword.Location = new System.Drawing.Point(73, 69);
			this.LoginPassword.Name = "LoginPassword";
			this.LoginPassword.Size = new System.Drawing.Size(314, 21);
			this.LoginPassword.TabIndex = 1;
			this.LoginPassword.UseSystemPasswordChar = true;
			this.LoginPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Login_KeyPress);
			this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label2.Location = new System.Drawing.Point(3, 73);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 13);
			this.label2.TabIndex = 3;
			this.label2.Text = "Password:";
			this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label4.AutoSize = true;
			this.LoginTableLayout.SetColumnSpan(this.label4, 2);
			this.label4.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.label4.Location = new System.Drawing.Point(73, 0);
			this.label4.Name = "label4";
			this.label4.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
			this.label4.Size = new System.Drawing.Size(239, 39);
			this.label4.TabIndex = 8;
			this.label4.Text = "Rotman Interactive Trader Login";
			this.LoginPort.DataBindings.Add(new System.Windows.Forms.Binding("Value", Settings.Default, "ServerPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.LoginPort.Location = new System.Drawing.Point(307, 96);
			System.Windows.Forms.NumericUpDown arg_FDE_0 = this.LoginPort;
			int[] array = new int[4];
			array[0] = 65535;
			arg_FDE_0.Maximum = new decimal(array);
			System.Windows.Forms.NumericUpDown arg_FFA_0 = this.LoginPort;
			int[] array2 = new int[4];
			array2[0] = 1;
			arg_FFA_0.Minimum = new decimal(array2);
			this.LoginPort.Name = "LoginPort";
			this.LoginPort.Size = new System.Drawing.Size(80, 21);
			this.LoginPort.TabIndex = 3;
			this.LoginPort.Value = Settings.Default.ServerPort;
			this.LoginPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Login_KeyPress);
			this.LoginTableLayoutContainer.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.LoginTableLayoutContainer.AutoSize = true;
			this.LoginTableLayoutContainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.LoginTableLayoutContainer.ColumnCount = 2;
			this.LoginTableLayoutContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.LoginTableLayoutContainer.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.LoginTableLayoutContainer.Controls.Add(this.pictureBox1, 0, 0);
			this.LoginTableLayoutContainer.Controls.Add(this.LoginTableLayout, 1, 0);
			this.LoginTableLayoutContainer.Location = new System.Drawing.Point(33, 7);
			this.LoginTableLayoutContainer.Name = "LoginTableLayoutContainer";
			this.LoginTableLayoutContainer.RowCount = 1;
			this.LoginTableLayoutContainer.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.LoginTableLayoutContainer.Size = new System.Drawing.Size(682, 282);
			this.LoginTableLayoutContainer.TabIndex = 1;
			this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.pictureBox1.Location = new System.Drawing.Point(3, 3);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(280, 276);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.LoginPanel.Controls.Add(this.LoginTableLayoutContainer);
			this.LoginPanel.Location = new System.Drawing.Point(21, 342);
			this.LoginPanel.Name = "LoginPanel";
			this.LoginPanel.Size = new System.Drawing.Size(749, 297);
			this.LoginPanel.TabIndex = 0;
			this.LoginPanel.Visible = false;
			this.NewUserPanel.Controls.Add(this.tableLayoutPanel3);
			this.NewUserPanel.Location = new System.Drawing.Point(21, 43);
			this.NewUserPanel.Name = "NewUserPanel";
			this.NewUserPanel.Size = new System.Drawing.Size(975, 293);
			this.NewUserPanel.TabIndex = 1;
			this.NewUserPanel.Visible = false;
			this.tableLayoutPanel3.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.tableLayoutPanel3.AutoSize = true;
			this.tableLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel3.ColumnCount = 2;
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel2, 1, 0);
			this.tableLayoutPanel3.Controls.Add(this.pictureBox2, 0, 0);
			this.tableLayoutPanel3.Location = new System.Drawing.Point(146, 5);
			this.tableLayoutPanel3.Name = "tableLayoutPanel3";
			this.tableLayoutPanel3.RowCount = 1;
			this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel3.Size = new System.Drawing.Size(682, 282);
			this.tableLayoutPanel3.TabIndex = 1;
			this.tableLayoutPanel2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.tableLayoutPanel2.AutoSize = true;
			this.tableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel2.ColumnCount = 3;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel3, 1, 7);
			this.tableLayoutPanel2.Controls.Add(this.NewUserLastName, 1, 5);
			this.tableLayoutPanel2.Controls.Add(this.NewUserFirstName, 1, 4);
			this.tableLayoutPanel2.Controls.Add(this.label12, 0, 5);
			this.tableLayoutPanel2.Controls.Add(this.label13, 0, 4);
			this.tableLayoutPanel2.Controls.Add(this.NewUserPasswordConfirm, 1, 3);
			this.tableLayoutPanel2.Controls.Add(this.label14, 0, 6);
			this.tableLayoutPanel2.Controls.Add(this.label15, 0, 3);
			this.tableLayoutPanel2.Controls.Add(this.label16, 0, 1);
			this.tableLayoutPanel2.Controls.Add(this.NewUserTraderID, 1, 1);
			this.tableLayoutPanel2.Controls.Add(this.NewUserPassword, 1, 2);
			this.tableLayoutPanel2.Controls.Add(this.label17, 0, 2);
			this.tableLayoutPanel2.Controls.Add(this.NewUserAddress, 1, 6);
			this.tableLayoutPanel2.Controls.Add(this.NewUserPort, 2, 6);
			this.tableLayoutPanel2.Controls.Add(this.label18, 1, 0);
			this.tableLayoutPanel2.Location = new System.Drawing.Point(289, 26);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 8;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel2.Size = new System.Drawing.Size(390, 230);
			this.tableLayoutPanel2.TabIndex = 0;
			this.flowLayoutPanel3.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.flowLayoutPanel3.AutoSize = true;
			this.flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel2.SetColumnSpan(this.flowLayoutPanel3, 2);
			this.flowLayoutPanel3.Controls.Add(this.NewUserButton);
			this.flowLayoutPanel3.Controls.Add(this.NewUserCancel);
			this.flowLayoutPanel3.Controls.Add(this.NewUserLoadingCircle);
			this.flowLayoutPanel3.Controls.Add(this.NewUserLoadingStatus);
			this.flowLayoutPanel3.Location = new System.Drawing.Point(117, 201);
			this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
			this.flowLayoutPanel3.Name = "flowLayoutPanel3";
			this.flowLayoutPanel3.Size = new System.Drawing.Size(197, 29);
			this.flowLayoutPanel3.TabIndex = 7;
			this.NewUserButton.Location = new System.Drawing.Point(3, 3);
			this.NewUserButton.Name = "NewUserButton";
			this.NewUserButton.Size = new System.Drawing.Size(75, 23);
			this.NewUserButton.TabIndex = 0;
			this.NewUserButton.Text = "Submit";
			this.NewUserButton.UseVisualStyleBackColor = true;
			this.NewUserButton.Click += new System.EventHandler(this.NewUserButton_Click);
			this.NewUserCancel.Location = new System.Drawing.Point(84, 3);
			this.NewUserCancel.Name = "NewUserCancel";
			this.NewUserCancel.Size = new System.Drawing.Size(75, 23);
			this.NewUserCancel.TabIndex = 1;
			this.NewUserCancel.Text = "Cancel";
			this.NewUserCancel.UseVisualStyleBackColor = true;
			this.NewUserCancel.Click += new System.EventHandler(this.NewUserCancel_Click);
			this.NewUserLoadingCircle.Active = true;
			this.NewUserLoadingCircle.Color = System.Drawing.Color.DarkGray;
			this.NewUserLoadingCircle.InnerCircleRadius = 8;
			this.NewUserLoadingCircle.Location = new System.Drawing.Point(165, 3);
			this.NewUserLoadingCircle.Name = "NewUserLoadingCircle";
			this.NewUserLoadingCircle.NumberSpoke = 24;
			this.NewUserLoadingCircle.OuterCircleRadius = 9;
			this.NewUserLoadingCircle.RotationSpeed = 100;
			this.NewUserLoadingCircle.Size = new System.Drawing.Size(23, 23);
			this.NewUserLoadingCircle.SpokeThickness = 4;
			this.NewUserLoadingCircle.StylePreset = LoadingCircle.StylePresets.IE7;
			this.NewUserLoadingCircle.TabIndex = 2;
			this.NewUserLoadingCircle.Visible = false;
			this.NewUserLoadingStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.NewUserLoadingStatus.AutoSize = true;
			this.NewUserLoadingStatus.Location = new System.Drawing.Point(194, 8);
			this.NewUserLoadingStatus.Name = "NewUserLoadingStatus";
			this.NewUserLoadingStatus.Size = new System.Drawing.Size(0, 13);
			this.NewUserLoadingStatus.TabIndex = 3;
			this.tableLayoutPanel2.SetColumnSpan(this.NewUserLastName, 2);
			this.NewUserLastName.Location = new System.Drawing.Point(120, 150);
			this.NewUserLastName.Name = "NewUserLastName";
			this.NewUserLastName.Size = new System.Drawing.Size(267, 21);
			this.NewUserLastName.TabIndex = 4;
			this.NewUserLastName.TextChanged += new System.EventHandler(this.NewUser_ValueChanged);
			this.NewUserLastName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NewUser_KeyPress);
			this.tableLayoutPanel2.SetColumnSpan(this.NewUserFirstName, 2);
			this.NewUserFirstName.Location = new System.Drawing.Point(120, 123);
			this.NewUserFirstName.Name = "NewUserFirstName";
			this.NewUserFirstName.Size = new System.Drawing.Size(267, 21);
			this.NewUserFirstName.TabIndex = 3;
			this.NewUserFirstName.TextChanged += new System.EventHandler(this.NewUser_ValueChanged);
			this.NewUserFirstName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NewUser_KeyPress);
			this.label12.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label12.Location = new System.Drawing.Point(45, 154);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(69, 13);
			this.label12.TabIndex = 5;
			this.label12.Text = "Last Name:";
			this.label13.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label13.Location = new System.Drawing.Point(44, 127);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(70, 13);
			this.label13.TabIndex = 4;
			this.label13.Text = "First Name:";
			this.tableLayoutPanel2.SetColumnSpan(this.NewUserPasswordConfirm, 2);
			this.NewUserPasswordConfirm.Location = new System.Drawing.Point(120, 96);
			this.NewUserPasswordConfirm.Name = "NewUserPasswordConfirm";
			this.NewUserPasswordConfirm.Size = new System.Drawing.Size(267, 21);
			this.NewUserPasswordConfirm.TabIndex = 2;
			this.NewUserPasswordConfirm.UseSystemPasswordChar = true;
			this.NewUserPasswordConfirm.TextChanged += new System.EventHandler(this.NewUser_ValueChanged);
			this.NewUserPasswordConfirm.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NewUser_KeyPress);
			this.label14.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label14.Location = new System.Drawing.Point(66, 181);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(48, 13);
			this.label14.TabIndex = 9;
			this.label14.Text = "Server:";
			this.label15.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label15.AutoSize = true;
			this.label15.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label15.Location = new System.Drawing.Point(3, 100);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(111, 13);
			this.label15.TabIndex = 5;
			this.label15.Text = "Confirm Password:";
			this.label16.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label16.Location = new System.Drawing.Point(50, 46);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(64, 13);
			this.label16.TabIndex = 1;
			this.label16.Text = "Trader ID:";
			this.tableLayoutPanel2.SetColumnSpan(this.NewUserTraderID, 2);
			this.NewUserTraderID.Location = new System.Drawing.Point(120, 42);
			this.NewUserTraderID.Name = "NewUserTraderID";
			this.NewUserTraderID.Size = new System.Drawing.Size(267, 21);
			this.NewUserTraderID.TabIndex = 0;
			this.NewUserTraderID.TextChanged += new System.EventHandler(this.NewUser_ValueChanged);
			this.NewUserTraderID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NewUser_KeyPress);
			this.tableLayoutPanel2.SetColumnSpan(this.NewUserPassword, 2);
			this.NewUserPassword.Location = new System.Drawing.Point(120, 69);
			this.NewUserPassword.Name = "NewUserPassword";
			this.NewUserPassword.Size = new System.Drawing.Size(267, 21);
			this.NewUserPassword.TabIndex = 1;
			this.NewUserPassword.UseSystemPasswordChar = true;
			this.NewUserPassword.TextChanged += new System.EventHandler(this.NewUser_ValueChanged);
			this.NewUserPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NewUser_KeyPress);
			this.label17.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.label17.AutoSize = true;
			this.label17.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label17.Location = new System.Drawing.Point(50, 73);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(64, 13);
			this.label17.TabIndex = 3;
			this.label17.Text = "Password:";
			this.NewUserAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", Settings.Default, "ServerAddress", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.NewUserAddress.Location = new System.Drawing.Point(120, 177);
			this.NewUserAddress.Name = "NewUserAddress";
			this.NewUserAddress.Size = new System.Drawing.Size(181, 21);
			this.NewUserAddress.TabIndex = 5;
			this.NewUserAddress.Text = Settings.Default.ServerAddress;
			this.NewUserAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NewUser_KeyPress);
			this.NewUserPort.DataBindings.Add(new System.Windows.Forms.Binding("Value", Settings.Default, "ServerPort", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.NewUserPort.Location = new System.Drawing.Point(307, 177);
			System.Windows.Forms.NumericUpDown arg_2051_0 = this.NewUserPort;
			int[] array3 = new int[4];
			array3[0] = 65535;
			arg_2051_0.Maximum = new decimal(array3);
			System.Windows.Forms.NumericUpDown arg_2070_0 = this.NewUserPort;
			int[] array4 = new int[4];
			array4[0] = 1;
			arg_2070_0.Minimum = new decimal(array4);
			this.NewUserPort.Name = "NewUserPort";
			this.NewUserPort.Size = new System.Drawing.Size(80, 21);
			this.NewUserPort.TabIndex = 6;
			this.NewUserPort.Value = Settings.Default.ServerPort;
			this.NewUserPort.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NewUser_KeyPress);
			this.label18.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label18.AutoSize = true;
			this.tableLayoutPanel2.SetColumnSpan(this.label18, 2);
			this.label18.Font = new System.Drawing.Font("Tahoma", 12f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.label18.Location = new System.Drawing.Point(120, 0);
			this.label18.Name = "label18";
			this.label18.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
			this.label18.Size = new System.Drawing.Size(77, 39);
			this.label18.TabIndex = 8;
			this.label18.Text = "New User";
			this.pictureBox2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.pictureBox2.Location = new System.Drawing.Point(3, 3);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(280, 276);
			this.pictureBox2.TabIndex = 0;
			this.pictureBox2.TabStop = false;
			this.NewUserErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.NewUserErrorProvider.ContainerControl = this;
			this.NewUserErrorProvider.Icon = (System.Drawing.Icon)componentResourceManager.GetObject("NewUserErrorProvider.Icon");
			this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripStatusLabel5,
				this.MainGameStatus,
				this.toolStripStatusLabel1,
				this.MainPeriodStatus,
				this.bindableToolStripStatusLabel1,
				this.MainTimeRemaining,
				this.toolStripStatusLabel2,
				this.MainTickStatus,
				this.toolStripStatusLabel3,
				this.MainTotalTickStatus,
				this.MainTickProgressBar,
				this.GameLabel,
				this.MainStatusLabel,
				this.APILabel,
				this.RTDLabel,
				this.ChatLabel,
				this.OTCLabel,
				this.NewsLabel,
				this.WorkspacesDropDown,
				this.WindowsDropDown
			});
			this.MainStatusStrip.Location = new System.Drawing.Point(0, 708);
			this.MainStatusStrip.Name = "MainStatusStrip";
			this.MainStatusStrip.Size = new System.Drawing.Size(1084, 22);
			this.MainStatusStrip.TabIndex = 6;
			this.MainStatusStrip.Text = "statusStrip1";
			this.toolStripStatusLabel5.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
			this.toolStripStatusLabel5.Size = new System.Drawing.Size(45, 17);
			this.toolStripStatusLabel5.Text = "Status:";
			this.MainGameStatus.Name = "MainGameStatus";
			this.MainGameStatus.Size = new System.Drawing.Size(22, 17);
			this.MainGameStatus.Text = "???";
			this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new System.Drawing.Size(46, 17);
			this.toolStripStatusLabel1.Text = "Period:";
			this.MainPeriodStatus.Name = "MainPeriodStatus";
			this.MainPeriodStatus.Size = new System.Drawing.Size(22, 17);
			this.MainPeriodStatus.Text = "???";
			this.bindableToolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.bindableToolStripStatusLabel1.Name = "bindableToolStripStatusLabel1";
			this.bindableToolStripStatusLabel1.Size = new System.Drawing.Size(69, 17);
			this.bindableToolStripStatusLabel1.Text = "Remaining:";
			this.MainTimeRemaining.Name = "MainTimeRemaining";
			this.MainTimeRemaining.Size = new System.Drawing.Size(22, 17);
			this.MainTimeRemaining.Text = "???";
			this.toolStripStatusLabel2.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
			this.toolStripStatusLabel2.Size = new System.Drawing.Size(58, 17);
			this.toolStripStatusLabel2.Text = "Progress:";
			this.MainTickStatus.Name = "MainTickStatus";
			this.MainTickStatus.Size = new System.Drawing.Size(22, 17);
			this.MainTickStatus.Text = "???";
			this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
			this.toolStripStatusLabel3.Size = new System.Drawing.Size(12, 17);
			this.toolStripStatusLabel3.Text = "/";
			this.MainTotalTickStatus.Name = "MainTotalTickStatus";
			this.MainTotalTickStatus.Size = new System.Drawing.Size(22, 17);
			this.MainTotalTickStatus.Text = "???";
			this.MainTickProgressBar.Name = "MainTickProgressBar";
			this.MainTickProgressBar.Size = new System.Drawing.Size(100, 16);
			this.MainTickProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
			this.GameLabel.Font = new System.Drawing.Font("Segoe UI", 9f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.GameLabel.Name = "GameLabel";
			this.GameLabel.Size = new System.Drawing.Size(69, 17);
			this.GameLabel.Text = "Simulation:";
			this.MainStatusLabel.Name = "MainStatusLabel";
			this.MainStatusLabel.Size = new System.Drawing.Size(177, 17);
			this.MainStatusLabel.Spring = true;
			this.MainStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.APILabel.Enabled = false;
			this.APILabel.ForeColor = System.Drawing.Color.Green;
			this.APILabel.Image = Resources.computer_link;
			this.APILabel.IsLink = true;
			this.APILabel.Name = "APILabel";
			this.APILabel.Size = new System.Drawing.Size(41, 17);
			this.APILabel.Text = "API";
			this.RTDLabel.Enabled = false;
			this.RTDLabel.ForeColor = System.Drawing.Color.Green;
			this.RTDLabel.Image = Resources.excel;
			this.RTDLabel.IsLink = true;
			this.RTDLabel.Name = "RTDLabel";
			this.RTDLabel.Size = new System.Drawing.Size(45, 17);
			this.RTDLabel.Text = "RTD";
			this.RTDLabel.Click += new System.EventHandler(this.RTDLabel_Click);
			this.ChatLabel.Image = Resources.comments;
			this.ChatLabel.IsLink = true;
			this.ChatLabel.Name = "ChatLabel";
			this.ChatLabel.Size = new System.Drawing.Size(37, 17);
			this.ChatLabel.Text = "(0)";
			this.ChatLabel.Click += new System.EventHandler(this.ChatLabel_Click);
			this.OTCLabel.Image = Resources.group;
			this.OTCLabel.IsLink = true;
			this.OTCLabel.Name = "OTCLabel";
			this.OTCLabel.Size = new System.Drawing.Size(37, 17);
			this.OTCLabel.Text = "(0)";
			this.OTCLabel.Click += new System.EventHandler(this.OTCLabel_Click);
			this.NewsLabel.Image = Resources.newspaper;
			this.NewsLabel.IsLink = true;
			this.NewsLabel.Name = "NewsLabel";
			this.NewsLabel.Size = new System.Drawing.Size(37, 17);
			this.NewsLabel.Text = "(0)";
			this.NewsLabel.Click += new System.EventHandler(this.NewsLabel_Click);
			this.WorkspacesDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.newWorkspaceToolStripMenuItem,
				this.removeCurrentWorkspaceToolStripMenuItem,
				this.toolStripSeparator3,
				this.saveWorkspaceToolStripMenuItem,
				this.loadWorkspaceToolStripMenuItem,
				this.toolStripSeparator4
			});
			this.WorkspacesDropDown.Image = Resources.application_view_tile;
			this.WorkspacesDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.WorkspacesDropDown.Name = "WorkspacesDropDown";
			this.WorkspacesDropDown.Size = new System.Drawing.Size(99, 20);
			this.WorkspacesDropDown.Text = "Workspaces";
			this.newWorkspaceToolStripMenuItem.Image = Resources.add;
			this.newWorkspaceToolStripMenuItem.Name = "newWorkspaceToolStripMenuItem";
			this.newWorkspaceToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.newWorkspaceToolStripMenuItem.Text = "New Workspace";
			this.newWorkspaceToolStripMenuItem.Click += new System.EventHandler(this.newWorkspaceToolStripMenuItem_Click);
			this.removeCurrentWorkspaceToolStripMenuItem.Image = Resources.delete;
			this.removeCurrentWorkspaceToolStripMenuItem.Name = "removeCurrentWorkspaceToolStripMenuItem";
			this.removeCurrentWorkspaceToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.removeCurrentWorkspaceToolStripMenuItem.Text = "Delete Workspace";
			this.removeCurrentWorkspaceToolStripMenuItem.Click += new System.EventHandler(this.removeCurrentWorkspaceToolStripMenuItem_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(165, 6);
			this.saveWorkspaceToolStripMenuItem.Image = Resources.disk;
			this.saveWorkspaceToolStripMenuItem.Name = "saveWorkspaceToolStripMenuItem";
			this.saveWorkspaceToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.saveWorkspaceToolStripMenuItem.Text = "Save Workspace";
			this.saveWorkspaceToolStripMenuItem.Click += new System.EventHandler(this.saveWorkspaceToolStripMenuItem_Click);
			this.loadWorkspaceToolStripMenuItem.Image = Resources.folder_page;
			this.loadWorkspaceToolStripMenuItem.Name = "loadWorkspaceToolStripMenuItem";
			this.loadWorkspaceToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.loadWorkspaceToolStripMenuItem.Text = "Load Workspace";
			this.loadWorkspaceToolStripMenuItem.Click += new System.EventHandler(this.loadWorkspaceToolStripMenuItem_Click);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(165, 6);
			this.WindowsDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.closeAllToolStripMenuItem,
				this.closeOrderEntriesToolStripMenuItem,
				this.toolStripSeparator1,
				this.tileHorizontallyToolStripMenuItem,
				this.tileVerticallyToolStripMenuItem,
				this.cascadeToolStripMenuItem,
				this.toolStripSeparator2
			});
			this.WindowsDropDown.Image = Resources.application;
			this.WindowsDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.WindowsDropDown.Name = "WindowsDropDown";
			this.WindowsDropDown.Size = new System.Drawing.Size(85, 20);
			this.WindowsDropDown.Text = "Windows";
			this.closeAllToolStripMenuItem.Image = Resources.application_delete;
			this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
			this.closeAllToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.closeAllToolStripMenuItem.Text = "Close All";
			this.closeAllToolStripMenuItem.Click += new System.EventHandler(this.closeAllToolStripMenuItem_Click);
			this.closeOrderEntriesToolStripMenuItem.Image = Resources.application_form_delete;
			this.closeOrderEntriesToolStripMenuItem.Name = "closeOrderEntriesToolStripMenuItem";
			this.closeOrderEntriesToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.closeOrderEntriesToolStripMenuItem.Text = "Close Order Entries";
			this.closeOrderEntriesToolStripMenuItem.Click += new System.EventHandler(this.closeOrderEntriesToolStripMenuItem_Click);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(171, 6);
			this.tileHorizontallyToolStripMenuItem.Image = Resources.application_tile_vertical;
			this.tileHorizontallyToolStripMenuItem.Name = "tileHorizontallyToolStripMenuItem";
			this.tileHorizontallyToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.tileHorizontallyToolStripMenuItem.Text = "Tile Horizontally";
			this.tileHorizontallyToolStripMenuItem.Click += new System.EventHandler(this.tileHorizontallyToolStripMenuItem_Click);
			this.tileVerticallyToolStripMenuItem.Image = Resources.application_tile_horizontal;
			this.tileVerticallyToolStripMenuItem.Name = "tileVerticallyToolStripMenuItem";
			this.tileVerticallyToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.tileVerticallyToolStripMenuItem.Text = "Tile Vertically";
			this.tileVerticallyToolStripMenuItem.Click += new System.EventHandler(this.tileVerticallyToolStripMenuItem_Click);
			this.cascadeToolStripMenuItem.Image = Resources.application_cascade;
			this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
			this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.cascadeToolStripMenuItem.Text = "Cascade";
			this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.cascadeToolStripMenuItem_Click);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(171, 6);
			this.InfoPanel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.InfoPanel.AutoSize = true;
			this.InfoPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.InfoPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.InfoPanel.Controls.Add(this.tableLayoutPanel1);
			this.InfoPanel.Location = new System.Drawing.Point(15, 645);
			this.InfoPanel.Name = "InfoPanel";
			this.InfoPanel.Size = new System.Drawing.Size(277, 50);
			this.InfoPanel.TabIndex = 8;
			this.InfoPanel.Click += new System.EventHandler(this.InfoPanel_Click);
			this.tableLayoutPanel1.AutoSize = true;
			this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel1.Controls.Add(this.InfoPanelBody, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.pictureBox3, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.InfoPanelTitle, 1, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(275, 48);
			this.tableLayoutPanel1.TabIndex = 0;
			this.tableLayoutPanel1.Click += new System.EventHandler(this.InfoPanel_Click);
			this.InfoPanelBody.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.InfoPanelBody.AutoSize = true;
			this.InfoPanelBody.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			this.InfoPanelBody.Location = new System.Drawing.Point(35, 24);
			this.InfoPanelBody.Name = "InfoPanelBody";
			this.InfoPanelBody.Padding = new System.Windows.Forms.Padding(0, 1, 0, 0);
			this.InfoPanelBody.Size = new System.Drawing.Size(227, 14);
			this.InfoPanelBody.TabIndex = 3;
			this.InfoPanelBody.Text = "Valuable information would be displayed here.";
			this.InfoPanelBody.Click += new System.EventHandler(this.InfoPanel_Click);
			this.pictureBox3.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.pictureBox3.Image = Resources.information;
			this.pictureBox3.Location = new System.Drawing.Point(13, 16);
			this.pictureBox3.Name = "pictureBox3";
			this.tableLayoutPanel1.SetRowSpan(this.pictureBox3, 2);
			this.pictureBox3.Size = new System.Drawing.Size(16, 16);
			this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox3.TabIndex = 0;
			this.pictureBox3.TabStop = false;
			this.pictureBox3.Click += new System.EventHandler(this.InfoPanel_Click);
			this.InfoPanelTitle.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.InfoPanelTitle.AutoSize = true;
			this.InfoPanelTitle.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.InfoPanelTitle.Location = new System.Drawing.Point(35, 10);
			this.InfoPanelTitle.Name = "InfoPanelTitle";
			this.InfoPanelTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.InfoPanelTitle.Size = new System.Drawing.Size(75, 14);
			this.InfoPanelTitle.TabIndex = 2;
			this.InfoPanelTitle.Text = "Information";
			this.InfoPanelTitle.Click += new System.EventHandler(this.InfoPanel_Click);
			this.InfoPanelTimer.Interval = 10000;
			this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.portfolioToolStripMenuItem,
				this.orderEntryToolStripMenuItem,
				this.marketDepthToolStripMenuItem,
				this.tradeBlotterToolStripMenuItem,
				this.assetsToolStripMenuItem,
				this.transactionLogToolStripMenuItem,
				this.newsToolStripMenuItem,
				this.traderInfoToolStripMenuItem,
				this.timeSalesToolStripMenuItem,
				this.chartingToolStripMenuItem,
				this.chatToolStripMenuItem,
				this.killMenuItem1,
				this.logoutToolStripMenuItem
			});
			this.MainMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
			this.MainMenu.Location = new System.Drawing.Point(0, 0);
			this.MainMenu.Name = "MainMenu";
			this.MainMenu.Size = new System.Drawing.Size(1084, 24);
			this.MainMenu.TabIndex = 5;
			this.portfolioToolStripMenuItem.Image = Resources.book;
			this.portfolioToolStripMenuItem.Name = "portfolioToolStripMenuItem";
			this.portfolioToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.portfolioToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
			this.portfolioToolStripMenuItem.Text = "Portfolio";
			this.portfolioToolStripMenuItem.TTSWindowType = WindowType.PORTFOLIO;
			this.portfolioToolStripMenuItem.Click += new System.EventHandler(this.marketViewToolStripMenuItem_Click);
			this.orderEntryToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.buySellEntryToolStripMenuItem,
				this.spreadEntryToolStripMenuItem,
				this.transportationArbitrageEntryToolStripMenuItem,
				this.oTCEntryToolStripMenuItem
			});
			this.orderEntryToolStripMenuItem.Image = Resources.bullet_arrow_down;
			this.orderEntryToolStripMenuItem.Name = "orderEntryToolStripMenuItem";
			this.orderEntryToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.orderEntryToolStripMenuItem.Size = new System.Drawing.Size(91, 20);
			this.orderEntryToolStripMenuItem.Text = "Order Entry";
			this.buySellEntryToolStripMenuItem.Image = Resources.lightning;
			this.buySellEntryToolStripMenuItem.Name = "buySellEntryToolStripMenuItem";
			this.buySellEntryToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
			this.buySellEntryToolStripMenuItem.Text = "Buy/Sell";
			this.buySellEntryToolStripMenuItem.TTSWindowType = WindowType.BUYSELL_ENTRY;
			this.buySellEntryToolStripMenuItem.Click += new System.EventHandler(this.orderEntryToolStripMenuItem_Click);
			this.spreadEntryToolStripMenuItem.Image = Resources.arrow_divide;
			this.spreadEntryToolStripMenuItem.Name = "spreadEntryToolStripMenuItem";
			this.spreadEntryToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
			this.spreadEntryToolStripMenuItem.Text = "Spread";
			this.spreadEntryToolStripMenuItem.TTSWindowType = WindowType.SPREAD_ENTRY;
			this.spreadEntryToolStripMenuItem.Click += new System.EventHandler(this.spreadEntryToolStripMenuItem_Click);
			this.transportationArbitrageEntryToolStripMenuItem.Image = Resources.arrow_right;
			this.transportationArbitrageEntryToolStripMenuItem.Name = "transportationArbitrageEntryToolStripMenuItem";
			this.transportationArbitrageEntryToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
			this.transportationArbitrageEntryToolStripMenuItem.Text = "Transportation Arbitrage";
			this.transportationArbitrageEntryToolStripMenuItem.TTSWindowType = WindowType.TRANSPORTATION_ARBITRAGE_ENTRY;
			this.transportationArbitrageEntryToolStripMenuItem.Click += new System.EventHandler(this.transportationArbitrageEntryToolStripMenuItem_Click);
			this.oTCEntryToolStripMenuItem.Image = Resources.group;
			this.oTCEntryToolStripMenuItem.Name = "oTCEntryToolStripMenuItem";
			this.oTCEntryToolStripMenuItem.Size = new System.Drawing.Size(204, 22);
			this.oTCEntryToolStripMenuItem.Text = "OTC Trade";
			this.oTCEntryToolStripMenuItem.TTSWindowType = WindowType.OTC_ENTRY;
			this.oTCEntryToolStripMenuItem.Click += new System.EventHandler(this.oTCTradeToolStripMenuItem_Click);
			this.marketDepthToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.bookTraderMenuItem,
				this.ladderTraderMenuItem
			});
			this.marketDepthToolStripMenuItem.Image = Resources.bullet_arrow_down;
			this.marketDepthToolStripMenuItem.Name = "marketDepthToolStripMenuItem";
			this.marketDepthToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.marketDepthToolStripMenuItem.Size = new System.Drawing.Size(103, 20);
			this.marketDepthToolStripMenuItem.Text = "Market Depth";
			this.bookTraderMenuItem.Image = Resources.book_open;
			this.bookTraderMenuItem.Name = "bookTraderMenuItem";
			this.bookTraderMenuItem.Size = new System.Drawing.Size(147, 22);
			this.bookTraderMenuItem.Text = "Book Trader";
			this.bookTraderMenuItem.TTSWindowType = WindowType.BOOK_TRADER;
			this.bookTraderMenuItem.Click += new System.EventHandler(this.bookTraderMenuItem_Click);
			this.ladderTraderMenuItem.Image = Resources.text_align_justify;
			this.ladderTraderMenuItem.Name = "ladderTraderMenuItem";
			this.ladderTraderMenuItem.Size = new System.Drawing.Size(147, 22);
			this.ladderTraderMenuItem.Text = "Ladder Trader";
			this.ladderTraderMenuItem.TTSWindowType = WindowType.LADDER_TRADER;
			this.ladderTraderMenuItem.Click += new System.EventHandler(this.ladderTraderMenuItem_Click);
			this.tradeBlotterToolStripMenuItem.Image = Resources.page;
			this.tradeBlotterToolStripMenuItem.Name = "tradeBlotterToolStripMenuItem";
			this.tradeBlotterToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.tradeBlotterToolStripMenuItem.Size = new System.Drawing.Size(99, 20);
			this.tradeBlotterToolStripMenuItem.Text = "Trade Blotter";
			this.tradeBlotterToolStripMenuItem.TTSWindowType = WindowType.TRADE_BLOTTER;
			this.tradeBlotterToolStripMenuItem.Click += new System.EventHandler(this.tradeBlotterToolStripMenuItem_Click);
			this.assetsToolStripMenuItem.Image = Resources.building;
			this.assetsToolStripMenuItem.Name = "assetsToolStripMenuItem";
			this.assetsToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.assetsToolStripMenuItem.Size = new System.Drawing.Size(64, 20);
			this.assetsToolStripMenuItem.Text = "Assets";
			this.assetsToolStripMenuItem.TTSWindowType = WindowType.ASSETS;
			this.assetsToolStripMenuItem.Click += new System.EventHandler(this.assetsToolStripMenuItem_Click);
			this.transactionLogToolStripMenuItem.Image = Resources.report;
			this.transactionLogToolStripMenuItem.Name = "transactionLogToolStripMenuItem";
			this.transactionLogToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.transactionLogToolStripMenuItem.Size = new System.Drawing.Size(116, 20);
			this.transactionLogToolStripMenuItem.Text = "Transaction Log";
			this.transactionLogToolStripMenuItem.TTSWindowType = WindowType.TRANSACTION_LOG;
			this.transactionLogToolStripMenuItem.Click += new System.EventHandler(this.transactionLogToolStripMenuItem_Click);
			this.newsToolStripMenuItem.Image = Resources.newspaper;
			this.newsToolStripMenuItem.Name = "newsToolStripMenuItem";
			this.newsToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.newsToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
			this.newsToolStripMenuItem.Text = "News";
			this.newsToolStripMenuItem.TTSWindowType = WindowType.NEWS;
			this.newsToolStripMenuItem.Click += new System.EventHandler(this.newsToolStripMenuItem_Click);
			this.traderInfoToolStripMenuItem.Image = Resources.vcard;
			this.traderInfoToolStripMenuItem.Name = "traderInfoToolStripMenuItem";
			this.traderInfoToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.traderInfoToolStripMenuItem.Size = new System.Drawing.Size(89, 20);
			this.traderInfoToolStripMenuItem.Text = "Trader Info";
			this.traderInfoToolStripMenuItem.TTSWindowType = WindowType.TRADER_INFO;
			this.traderInfoToolStripMenuItem.Click += new System.EventHandler(this.traderInfoToolStripMenuItem_Click);
			this.timeSalesToolStripMenuItem.Image = Resources.calendar_view_day;
			this.timeSalesToolStripMenuItem.Name = "timeSalesToolStripMenuItem";
			this.timeSalesToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.timeSalesToolStripMenuItem.Size = new System.Drawing.Size(100, 20);
			this.timeSalesToolStripMenuItem.Text = "Time && Sales";
			this.timeSalesToolStripMenuItem.TTSWindowType = WindowType.TIME_AND_SALES;
			this.timeSalesToolStripMenuItem.Click += new System.EventHandler(this.timeSalesToolStripMenuItem_Click);
			this.chartingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[]
			{
				this.pNLChartingToolStripMenuItem,
				this.securityChartingToolStripMenuItem,
				this.electricityChartingToolStripMenuItem
			});
			this.chartingToolStripMenuItem.Image = Resources.bullet_arrow_down;
			this.chartingToolStripMenuItem.Name = "chartingToolStripMenuItem";
			this.chartingToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.chartingToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
			this.chartingToolStripMenuItem.Text = "Charting";
			this.pNLChartingToolStripMenuItem.Image = Resources.chart_line;
			this.pNLChartingToolStripMenuItem.Name = "pNLChartingToolStripMenuItem";
			this.pNLChartingToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.pNLChartingToolStripMenuItem.Text = "P&&L Charting";
			this.pNLChartingToolStripMenuItem.TTSWindowType = WindowType.PNL_CHARTING;
			this.pNLChartingToolStripMenuItem.Click += new System.EventHandler(this.nLVChartingToolStripMenuItem_Click);
			this.securityChartingToolStripMenuItem.Image = Resources.chart_curve;
			this.securityChartingToolStripMenuItem.Name = "securityChartingToolStripMenuItem";
			this.securityChartingToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.securityChartingToolStripMenuItem.Text = "Security Charting";
			this.securityChartingToolStripMenuItem.TTSWindowType = WindowType.SECURITY_CHARTING;
			this.securityChartingToolStripMenuItem.Click += new System.EventHandler(this.securityChartingToolStripMenuItem_Click);
			this.electricityChartingToolStripMenuItem.Image = Resources.lightbulb;
			this.electricityChartingToolStripMenuItem.Name = "electricityChartingToolStripMenuItem";
			this.electricityChartingToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
			this.electricityChartingToolStripMenuItem.Text = "Electricity Charting";
			this.electricityChartingToolStripMenuItem.TTSWindowType = WindowType.ELECTRICITY_CHARTING;
			this.electricityChartingToolStripMenuItem.Click += new System.EventHandler(this.electricityChartingToolStripMenuItem_Click);
			this.chatToolStripMenuItem.Image = Resources.comments;
			this.chatToolStripMenuItem.Name = "chatToolStripMenuItem";
			this.chatToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.chatToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
			this.chatToolStripMenuItem.Tag = RIT.rtd_string;
			this.chatToolStripMenuItem.Text = "Chat";
			this.chatToolStripMenuItem.TTSWindowType = WindowType.CHAT;
			this.chatToolStripMenuItem.Click += new System.EventHandler(this.chatToolStripMenuItem_Click);
			this.killMenuItem1.Image = Resources.cross;
			this.killMenuItem1.Name = "killMenuItem1";
			this.killMenuItem1.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.killMenuItem1.Size = new System.Drawing.Size(47, 20);
			this.killMenuItem1.Tag = RIT.rtd_string;
			this.killMenuItem1.Text = "Kill";
			this.killMenuItem1.TTSWindowType = WindowType.KILL;
			this.killMenuItem1.Click += new System.EventHandler(this.killMenuItem1_Click);
			this.logoutToolStripMenuItem.Image = Resources.disconnect;
			this.logoutToolStripMenuItem.Name = "logoutToolStripMenuItem";
			this.logoutToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.logoutToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
			this.logoutToolStripMenuItem.Text = "Logout";
			this.logoutToolStripMenuItem.Click += new System.EventHandler(this.logoutToolStripMenuItem_Click);
			this.InfoPanelShowTimer.Interval = 1000;
			this.InfoPanelShowTimer.Tick += new System.EventHandler(this.InfoPanelShowTimer_Tick);
			this.groupBox1.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.groupBox1.Controls.Add(this.button2);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.numericUpDown2);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.numericUpDown1);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Location = new System.Drawing.Point(795, 535);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(277, 98);
			this.groupBox1.TabIndex = 10;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "AUTO TRADER 9000";
			this.groupBox1.Visible = false;
			this.button2.Location = new System.Drawing.Point(167, 69);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 5;
			this.button2.Text = "STOOOP!";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			this.button1.Location = new System.Drawing.Point(86, 69);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "GO GO GO";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			this.numericUpDown2.Location = new System.Drawing.Point(86, 43);
			System.Windows.Forms.NumericUpDown arg_3FE7_0 = this.numericUpDown2;
			int[] array5 = new int[4];
			array5[0] = 1000000;
			arg_3FE7_0.Maximum = new decimal(array5);
			this.numericUpDown2.Name = "numericUpDown2";
			this.numericUpDown2.Size = new System.Drawing.Size(120, 21);
			this.numericUpDown2.TabIndex = 3;
			System.Windows.Forms.NumericUpDown arg_4037_0 = this.numericUpDown2;
			int[] array6 = new int[4];
			array6[0] = 10;
			arg_4037_0.Value = new decimal(array6);
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 45);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(48, 13);
			this.label6.TabIndex = 2;
			this.label6.Text = "max size";
			this.numericUpDown1.Location = new System.Drawing.Point(86, 18);
			System.Windows.Forms.NumericUpDown arg_40CD_0 = this.numericUpDown1;
			int[] array7 = new int[4];
			array7[0] = 1000000;
			arg_40CD_0.Maximum = new decimal(array7);
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(120, 21);
			this.numericUpDown1.TabIndex = 1;
			System.Windows.Forms.NumericUpDown arg_411D_0 = this.numericUpDown1;
			int[] array8 = new int[4];
			array8[0] = 120;
			arg_411D_0.Value = new decimal(array8);
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(6, 20);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(74, 13);
			this.label5.TabIndex = 0;
			this.label5.Text = "orders/minute";
			this.SaveWorkspaceDialog.DefaultExt = "wkspc";
			this.SaveWorkspaceDialog.Filter = "Workspace files|*.wkspc";
			this.LoadWorkspaceDialog.DefaultExt = "wkspc";
			this.LoadWorkspaceDialog.Filter = "Workspace files|*.wkspc";
			this.ReportPanel.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right);
			this.ReportPanel.AutoSize = true;
			this.ReportPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.ReportPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ReportPanel.Controls.Add(this.tableLayoutPanel4);
			this.ReportPanel.Location = new System.Drawing.Point(875, 645);
			this.ReportPanel.Name = "ReportPanel";
			this.ReportPanel.Size = new System.Drawing.Size(197, 49);
			this.ReportPanel.TabIndex = 12;
			this.tableLayoutPanel4.AutoSize = true;
			this.tableLayoutPanel4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.tableLayoutPanel4.ColumnCount = 3;
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tableLayoutPanel4.Controls.Add(this.TraderReportOpenLink, 0, 1);
			this.tableLayoutPanel4.Controls.Add(this.pictureBox4, 0, 0);
			this.tableLayoutPanel4.Controls.Add(this.label8, 1, 0);
			this.tableLayoutPanel4.Controls.Add(this.TraderReportDownloadLink, 1, 1);
			this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel4.Name = "tableLayoutPanel4";
			this.tableLayoutPanel4.Padding = new System.Windows.Forms.Padding(10);
			this.tableLayoutPanel4.RowCount = 2;
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20f));
			this.tableLayoutPanel4.Size = new System.Drawing.Size(195, 47);
			this.tableLayoutPanel4.TabIndex = 0;
			this.TraderReportOpenLink.AutoSize = true;
			this.TraderReportOpenLink.Location = new System.Drawing.Point(35, 24);
			this.TraderReportOpenLink.Name = "TraderReportOpenLink";
			this.TraderReportOpenLink.Size = new System.Drawing.Size(33, 13);
			this.TraderReportOpenLink.TabIndex = 4;
			this.TraderReportOpenLink.TabStop = true;
			this.TraderReportOpenLink.Text = "Open";
			this.TraderReportOpenLink.Visible = false;
			this.TraderReportOpenLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.TraderReportOpenLink_LinkClicked);
			this.pictureBox4.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.pictureBox4.Image = Resources.report_user;
			this.pictureBox4.Location = new System.Drawing.Point(13, 15);
			this.pictureBox4.Name = "pictureBox4";
			this.tableLayoutPanel4.SetRowSpan(this.pictureBox4, 2);
			this.pictureBox4.Size = new System.Drawing.Size(16, 16);
			this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox4.TabIndex = 0;
			this.pictureBox4.TabStop = false;
			this.label8.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label8.AutoSize = true;
			this.tableLayoutPanel4.SetColumnSpan(this.label8, 2);
			this.label8.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
			this.label8.Location = new System.Drawing.Point(35, 10);
			this.label8.Name = "label8";
			this.label8.Padding = new System.Windows.Forms.Padding(0, 0, 0, 1);
			this.label8.Size = new System.Drawing.Size(147, 14);
			this.label8.TabIndex = 2;
			this.label8.Text = "Trading Report Available";
			this.TraderReportDownloadLink.AutoSize = true;
			this.TraderReportDownloadLink.Location = new System.Drawing.Point(74, 24);
			this.TraderReportDownloadLink.Name = "TraderReportDownloadLink";
			this.TraderReportDownloadLink.Size = new System.Drawing.Size(54, 13);
			this.TraderReportDownloadLink.TabIndex = 3;
			this.TraderReportDownloadLink.TabStop = true;
			this.TraderReportDownloadLink.Text = "Download";
			this.TraderReportDownloadLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.TraderReportDownloadLink_LinkClicked);
			this.ReportDownloadDialog.DefaultExt = "pdf";
			this.ReportDownloadDialog.Filter = "Portable Document files|*.pdf";
			this.SnapForm.Distance = 10;
			this.SnapForm.Form = this;
			base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			base.ClientSize = new System.Drawing.Size(1084, 730);
			base.Controls.Add(this.ReportPanel);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.NewUserPanel);
			base.Controls.Add(this.LoginPanel);
			base.Controls.Add(this.InfoPanel);
			base.Controls.Add(this.MainStatusStrip);
			base.Controls.Add(this.MainMenu);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			base.IsMdiContainer = true;
			base.MainMenuStrip = this.MainMenu;
			this.MinimumSize = new System.Drawing.Size(800, 600);
			base.Name = "Client";
			this.Text = "RIT 2.0 Client";
			this.LoginTableLayout.ResumeLayout(false);
			this.LoginTableLayout.PerformLayout();
			this.flowLayoutPanel1.ResumeLayout(false);
			this.flowLayoutPanel1.PerformLayout();
			((ISupportInitialize)this.LoginPort).EndInit();
			this.LoginTableLayoutContainer.ResumeLayout(false);
			this.LoginTableLayoutContainer.PerformLayout();
			((ISupportInitialize)this.pictureBox1).EndInit();
			this.LoginPanel.ResumeLayout(false);
			this.LoginPanel.PerformLayout();
			this.NewUserPanel.ResumeLayout(false);
			this.NewUserPanel.PerformLayout();
			this.tableLayoutPanel3.ResumeLayout(false);
			this.tableLayoutPanel3.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.tableLayoutPanel2.PerformLayout();
			this.flowLayoutPanel3.ResumeLayout(false);
			this.flowLayoutPanel3.PerformLayout();
			((ISupportInitialize)this.NewUserPort).EndInit();
			((ISupportInitialize)this.pictureBox2).EndInit();
			((ISupportInitialize)this.NewUserErrorProvider).EndInit();
			this.MainStatusStrip.ResumeLayout(false);
			this.MainStatusStrip.PerformLayout();
			this.InfoPanel.ResumeLayout(false);
			this.InfoPanel.PerformLayout();
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			((ISupportInitialize)this.pictureBox3).EndInit();
			this.MainMenu.ResumeLayout(false);
			this.MainMenu.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((ISupportInitialize)this.numericUpDown2).EndInit();
			((ISupportInitialize)this.numericUpDown1).EndInit();
			this.ReportPanel.ResumeLayout(false);
			this.ReportPanel.PerformLayout();
			this.tableLayoutPanel4.ResumeLayout(false);
			this.tableLayoutPanel4.PerformLayout();
			((ISupportInitialize)this.pictureBox4).EndInit();
			((ISupportInitialize)this.SnapForm).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}
		[System.Runtime.InteropServices.DllImport("user32")]
		public static extern int SetParent(int hWndChild, int hWndNewParent);
		public Client()
		{
			this.InitializeComponent();
			string format = "{0}.{1}.{2}/{3}";
			System.Collections.Generic.List<string> versionitems = new System.Collections.Generic.List<string>();
			System.Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			if (ApplicationDeployment.IsNetworkDeployed)
			{
				version = ApplicationDeployment.CurrentDeployment.CurrentVersion;
			}
			versionitems.Add(string.Format(format, new object[]
			{
				version.Major,
				version.Minor,
				version.Build,
				version.Revision
			}));
			if (ApplicationDeployment.IsNetworkDeployed)
			{
				versionitems.Add("oc");
			}
			else
			{
				versionitems.Add("msi");
			}
			versionitems.Add("rc1");
			base.Load += delegate(object sender, System.EventArgs e)
			{
				Client expr_06 = this;
				expr_06.Text += string.Format(" ({0})", string.Join("-", versionitems));
			};
			if (Settings.Default.ApplicationVersion != version.ToString())
			{
				Settings.Default.Upgrade();
				Settings.Default.ApplicationVersion = version.ToString();
				Settings.Default.Save();
			}
			ThreadHelper.MainThread = this;
			ServiceManager.Disconnected += delegate
			{
				ThreadHelper.MainThread.BeginInvokeIfRequired(delegate
				{
					if (TTSFormManager.Instance != null)
					{
						Settings.Default.Workspace = TTSFormManager.Instance.SaveLayout();
						Settings.Default.Save();
						TTSFormManager.Instance.ClearWindows();
						TTSFormManager.Instance = null;
						this.ClosePromptWindows();
					}
					this.SetUIState(this.LastUIState);
				});
			};
			this.InfoPanel.Visible = false;
			this.InfoPanelTimer.Tick += delegate(object sender, System.EventArgs e)
			{
				this.InfoPanelTimer.Stop();
				this.InfoPanelShowTimer.Stop();
				this.InfoPanel.Visible = false;
			};
			Settings.Default.IsSilentAlerts = true;
			Settings.Default.Save();
			this.ToggleTraderReportDownload(false);
			GameManager.StatusUpdated += delegate
			{
				if (Game.State.Current.Status == GameStatus.STOPPED)
				{
					this.ClosePromptWindows();
				}
			};
			base.Load += delegate(object sender, System.EventArgs e)
			{
				this.SetUIState(Client.UIState.LOGIN);
			};
			base.SizeChanged += delegate(object sender, System.EventArgs e)
			{
				this.Refresh();
			};
			Game.Reset = (Action)System.Delegate.Combine(Game.Reset, delegate
			{
				this.BindUIElements();
				this.EnforceAllowedWindows();
				this.ToggleTraderReportDownload(false);
			});
			Client.Skin = RIT.ResourceManager;
			this.pictureBox1.Image = (System.Drawing.Image)Client.Skin.GetObject("login_image");
			this.pictureBox2.Image = (System.Drawing.Image)Client.Skin.GetObject("login_image");
			this.BackgroundImage = (System.Drawing.Image)Client.Skin.GetObject("bg_image");
			this.Text = Client.Skin.GetString("program_name") + " Client";
			this.label4.Text = Client.Skin.GetString("greeting_string");
			base.Icon = (System.Drawing.Icon)Client.Skin.GetObject("program_icon");
			RTDManager.StateChanged += delegate(bool isrtd)
			{
				ThreadHelper.MainThread.BeginInvokeIfRequired(delegate
				{
					this.RTDLabel.Enabled = isrtd;
				});
			};
			APIManager.StateChanged += delegate(bool isapi)
			{
				ThreadHelper.MainThread.BeginInvokeIfRequired(delegate
				{
					this.APILabel.Enabled = isapi;
				});
			};
			System.Threading.ThreadPool.QueueUserWorkItem(delegate(object x)
			{
				new GridEX();
				new Chart();
				MathHelper.Evaluate("0");
				Serializer.Serialize<int>(new System.IO.MemoryStream(), 0);
			});
			base.KeyPreview = true;
			base.KeyDown += delegate(object sender, System.Windows.Forms.KeyEventArgs e)
			{
				if (e.Control && e.Alt && e.Shift && e.KeyCode == System.Windows.Forms.Keys.F9)
				{
					this.groupBox1.Visible = !this.groupBox1.Visible;
				}
			};
			base.Load += delegate(object sender, System.EventArgs e)
			{
				base.Bounds = System.Windows.Forms.Screen.PrimaryScreen.WorkingArea;
			};
		}
		public void ShowInfo(string title, string body)
		{
			ThreadHelper.MainThread.BeginInvokeIfRequired(delegate
			{
				this.InfoPanelTimer.Stop();
				this.InfoPanelTimer.Start();
				this.InfoPanelShowTimer.Stop();
				this.InfoPanelShowTimer.Start();
				this.InfoPanelBody.Text = body;
				this.InfoPanelTitle.Text = title;
				this.InfoPanel.BackColor = ColorHelper.TTSYellow;
				this.InfoPanel.Visible = true;
				SystemSounds.Exclamation.Play();
			});
		}
		private void InfoPanel_Click(object sender, System.EventArgs e)
		{
			this.InfoPanelTimer.Stop();
			this.InfoPanelShowTimer.Stop();
			this.InfoPanel.Visible = false;
		}
		private void InfoPanelShowTimer_Tick(object sender, System.EventArgs e)
		{
			this.InfoPanel.BackColor = ((this.InfoPanel.BackColor == System.Drawing.SystemColors.Control) ? ColorHelper.TTSYellow : System.Drawing.SystemColors.Control);
		}
		public void SetLoadingStatus(string text = "")
		{
			ThreadHelper.MainThread.BeginInvokeIfRequired(delegate
			{
				this.LoginLoadingStatus.Text = text;
				this.NewUserLoadingStatus.Text = text;
			});
		}
		public void SetUIState(Client.UIState state)
		{
			ThreadHelper.MainThread.BeginInvokeIfRequired(delegate
			{
				this.UnbindUIElements();
				switch (state)
				{
				case Client.UIState.LOGIN:
					this.MainMenu.Enabled = (this.MainStatusStrip.Enabled = (this.MainMenu.Visible = (this.MainStatusStrip.Visible = false)));
					this.NewUserPanel.Hide();
					this.LoginPanel.Show();
					this.LoginPanel.Dock = System.Windows.Forms.DockStyle.Fill;
					this.SetChildrenState(this.LoginPanel, true);
					this.LoginLoadingCircle.Visible = false;
					this.LoginTraderID.Focus();
					this.LoginTraderID.SelectAll();
					this.SetLoadingStatus("");
					this.LastUIState = state;
					break;
				case Client.UIState.LOGIN_CONNECTING:
					this.MainMenu.Enabled = (this.MainStatusStrip.Enabled = (this.MainMenu.Visible = (this.MainStatusStrip.Visible = false)));
					this.NewUserPanel.Hide();
					this.LoginPanel.Show();
					this.LoginPanel.Dock = System.Windows.Forms.DockStyle.Fill;
					this.SetChildrenState(this.LoginPanel, false);
					this.LoginLoadingCircle.Visible = true;
					this.SetLoadingStatus("Connecting...");
					break;
				case Client.UIState.NEW_USER:
					this.MainMenu.Enabled = (this.MainStatusStrip.Enabled = (this.MainMenu.Visible = (this.MainStatusStrip.Visible = false)));
					this.LoginPanel.Hide();
					this.NewUserPanel.Show();
					this.NewUserPanel.Dock = System.Windows.Forms.DockStyle.Fill;
					this.SetChildrenState(this.NewUserPanel, true);
					this.NewUserLoadingCircle.Visible = false;
					this.NewUserTraderID.Focus();
					this.NewUserTraderID.SelectAll();
					this.NewUserValidate();
					this.SetLoadingStatus("");
					if (!string.IsNullOrWhiteSpace(this.LoginTraderID.Text))
					{
						this.NewUserTraderID.Text = this.LoginTraderID.Text;
						this.NewUserPassword.Focus();
					}
					if (!string.IsNullOrWhiteSpace(this.LoginPassword.Text))
					{
						this.NewUserPassword.Text = this.LoginPassword.Text;
						if (!string.IsNullOrWhiteSpace(this.NewUserTraderID.Text))
						{
							this.NewUserPasswordConfirm.Focus();
						}
					}
					this.LastUIState = state;
					break;
				case Client.UIState.NEW_USER_CONNECTING:
					this.MainMenu.Enabled = (this.MainStatusStrip.Enabled = (this.MainMenu.Visible = (this.MainStatusStrip.Visible = false)));
					this.LoginPanel.Hide();
					this.NewUserPanel.Show();
					this.NewUserPanel.Dock = System.Windows.Forms.DockStyle.Fill;
					this.SetChildrenState(this.NewUserPanel, false);
					this.NewUserLoadingCircle.Visible = true;
					this.SetLoadingStatus("Connecting...");
					break;
				case Client.UIState.ACTIVE:
				{
					this.EnforceAllowedWindows();
					Settings.Default.IsOTCAutoAccept = false;
					Settings.Default.IsSilentAlerts = true;
					this.LastUIState = Client.UIState.LOGIN;
					System.Windows.Forms.TextBox[] array = new System.Windows.Forms.TextBox[]
					{
						this.NewUserFirstName,
						this.NewUserLastName,
						this.NewUserTraderID,
						this.NewUserPassword,
						this.NewUserPasswordConfirm,
						this.LoginTraderID,
						this.LoginPassword
					};
					System.Windows.Forms.TextBox[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						System.Windows.Forms.TextBox textBox = array2[i];
						textBox.Clear();
					}
					this.MainMenu.Enabled = (this.MainStatusStrip.Enabled = (this.MainMenu.Visible = (this.MainStatusStrip.Visible = true)));
					TTSFormManager.Instance = new TTSFormManager(this, this.WorkspacesDropDown, this.WindowsDropDown);
					TTSFormManager.Instance.LoadLayout(Settings.Default.Workspace);
					this.BindUIElements();
					this.LoginPanel.Hide();
					this.NewUserPanel.Hide();
					this.SetLoadingStatus("");
					break;
				}
				}
				this.CurrentUIState = state;
			});
		}
		private void SetChildrenState(System.Windows.Forms.Control parent, bool isenabled)
		{
			foreach (System.Windows.Forms.Control control in parent.Controls)
			{
				if (control is System.Windows.Forms.TableLayoutPanel || control is System.Windows.Forms.FlowLayoutPanel || control is System.Windows.Forms.Panel)
				{
					this.SetChildrenState(control, isenabled);
				}
				else
				{
					if (control is System.Windows.Forms.Button || control is System.Windows.Forms.TextBox || control is System.Windows.Forms.NumericUpDown)
					{
						control.Enabled = isenabled;
					}
				}
			}
		}
		public void EnforceAllowedWindows()
		{
			ThreadHelper.MainThread.BeginInvokeIfRequired(delegate
			{
				foreach (System.Windows.Forms.ToolStripMenuItem toolStripMenuItem in this.MainMenu.Items)
				{
					if (toolStripMenuItem.GetType() == typeof(TTSFormMenuItem))
					{
						toolStripMenuItem.Enabled = Game.State.General.AllowedWindows.HasFlag(((TTSFormMenuItem)toolStripMenuItem).TTSWindowType);
					}
					else
					{
						foreach (TTSFormMenuItem current in toolStripMenuItem.DropDownItems.OfType<TTSFormMenuItem>())
						{
							current.Enabled = Game.State.General.AllowedWindows.HasFlag(current.TTSWindowType);
						}
					}
				}
			});
		}
		private void BindUIElements()
		{
			this.UnbindUIElements();
			this.MainPeriodStatus.DataBindings.Add("Text", Game.State.Current, "Period");
			this.MainTickStatus.DataBindings.Add("Text", Game.State.Current, "Tick");
			this.MainTotalTickStatus.DataBindings.Add("Text", Game.State.General, "TicksPerPeriod");
			this.MainTickProgressBar.ProgressBar.DataBindings.Add("Maximum", Game.State.General, "TicksPerPeriod");
			this.MainTickProgressBar.ProgressBar.DataBindings.Add("Value", Game.State.Current, "Tick");
			this.MainGameStatus.DataBindings.Add("Text", Game.State.Current, "Status");
			System.Windows.Forms.Binding binding = new System.Windows.Forms.Binding("Text", Game.State.Current, "Tick", true);
			binding.Format += delegate(object sender, System.Windows.Forms.ConvertEventArgs e)
			{
				e.Value = new System.TimeSpan(0, 0, 0, (Game.State.General.TicksPerPeriod - (int)e.Value) * Game.State.Current.MillisecondsPerTick / 1000).ToString("g");
			};
			this.MainTimeRemaining.DataBindings.Add(binding);
			binding = new System.Windows.Forms.Binding("ForeColor", Game.State.Current, "Status", true);
			binding.Format += delegate(object sender, System.Windows.Forms.ConvertEventArgs e)
			{
				switch ((GameStatus)e.Value)
				{
				case GameStatus.STOPPED:
					e.Value = System.Drawing.Color.Red;
					return;
				case GameStatus.ACTIVE:
					e.Value = System.Drawing.Color.Green;
					return;
				case GameStatus.PAUSED:
					e.Value = System.Drawing.Color.Gray;
					return;
				default:
					return;
				}
			};
			this.MainGameStatus.DataBindings.Add(binding);
			this.MainStatusLabel.DataBindings.Add("Text", Game.State.General, "GameName");
			binding = new System.Windows.Forms.Binding("Visible", Game.State.General, "GameName", true);
			binding.Format += delegate(object sender, System.Windows.Forms.ConvertEventArgs e)
			{
				if (!string.IsNullOrWhiteSpace(e.Value as string))
				{
					e.Value = true;
					return;
				}
				e.Value = false;
			};
			this.GameLabel.DataBindings.Add(binding);
			this.NewsLabel.DataBindings.Add("Text", Game.State, "UnreadNewsCount", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "(0)");
			binding = new System.Windows.Forms.Binding("Font", Game.State, "UnreadNewsCount", true);
			binding.Format += delegate(object sender, System.Windows.Forms.ConvertEventArgs e)
			{
				if ((int)e.Value > 0)
				{
					e.Value = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
					return;
				}
				e.Value = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			};
			this.NewsLabel.DataBindings.Add(binding);
			binding = new System.Windows.Forms.Binding("LinkColor", Game.State, "UnreadNewsCount", true);
			binding.Format += delegate(object sender, System.Windows.Forms.ConvertEventArgs e)
			{
				if ((int)e.Value > 0)
				{
					e.Value = System.Drawing.Color.Red;
					return;
				}
				e.Value = System.Drawing.Color.Black;
			};
			this.NewsLabel.DataBindings.Add(binding);
			this.OTCLabel.DataBindings.Add("Text", Game.State, "PendingOTCCount", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "(0)");
			binding = new System.Windows.Forms.Binding("Font", Game.State, "PendingOTCCount", true);
			binding.Format += delegate(object sender, System.Windows.Forms.ConvertEventArgs e)
			{
				if ((int)e.Value > 0)
				{
					e.Value = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
					return;
				}
				e.Value = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			};
			this.OTCLabel.DataBindings.Add(binding);
			binding = new System.Windows.Forms.Binding("LinkColor", Game.State, "PendingOTCCount", true);
			binding.Format += delegate(object sender, System.Windows.Forms.ConvertEventArgs e)
			{
				if ((int)e.Value > 0)
				{
					e.Value = System.Drawing.Color.Red;
					return;
				}
				e.Value = System.Drawing.Color.Black;
			};
			this.OTCLabel.DataBindings.Add(binding);
			this.ChatLabel.DataBindings.Add("Text", Game.State, "UnreadChatCount", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged, null, "(0)");
			binding = new System.Windows.Forms.Binding("Font", Game.State, "UnreadChatCount", true);
			binding.Format += delegate(object sender, System.Windows.Forms.ConvertEventArgs e)
			{
				if ((int)e.Value > 0)
				{
					e.Value = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 0);
					return;
				}
				e.Value = new System.Drawing.Font("Tahoma", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			};
			this.ChatLabel.DataBindings.Add(binding);
			binding = new System.Windows.Forms.Binding("LinkColor", Game.State, "UnreadChatCount", true);
			binding.Format += delegate(object sender, System.Windows.Forms.ConvertEventArgs e)
			{
				if ((int)e.Value > 0)
				{
					e.Value = System.Drawing.Color.Red;
					return;
				}
				e.Value = System.Drawing.Color.Black;
			};
			this.ChatLabel.DataBindings.Add(binding);
		}
		private void UnbindUIElements()
		{
			this.MainStatusLabel.DataBindings.Clear();
			this.GameLabel.DataBindings.Clear();
			this.MainPeriodStatus.DataBindings.Clear();
			this.MainTickStatus.DataBindings.Clear();
			this.MainTotalTickStatus.DataBindings.Clear();
			this.MainTickProgressBar.ProgressBar.DataBindings.Clear();
			this.MainGameStatus.DataBindings.Clear();
			this.NewsLabel.DataBindings.Clear();
			this.MainTimeRemaining.DataBindings.Clear();
			this.OTCLabel.DataBindings.Clear();
			this.ChatLabel.DataBindings.Clear();
		}
		private void ClosePromptWindows()
		{
			System.Windows.Forms.Form[] mdiChildren = base.MdiChildren;
			for (int i = 0; i < mdiChildren.Length; i++)
			{
				System.Windows.Forms.Form form = mdiChildren[i];
				if (form is TenderOrder)
				{
					form.Close();
				}
			}
		}
		private void LoginButton_Click(object sender, System.EventArgs e)
		{
			Settings.Default.Save();
			this.SetUIState(Client.UIState.LOGIN_CONNECTING);
			string address = this.LoginAddress.Text.Trim();
			int port = System.Convert.ToInt32(this.LoginPort.Value);
			string traderid = this.LoginTraderID.Text.Trim();
			string password = CrytoHelper.GenerateMD5(this.LoginPassword.Text.Trim());
			System.Threading.ThreadPool.QueueUserWorkItem(delegate(object x)
			{
				if (ServiceManager.Connect(address, port))
				{
					ServiceManager.Execute(delegate(IClientService proxy)
					{
						proxy.Authenticate(traderid, password);
					});
				}
			});
		}
		private void Login_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.LoginButton.PerformClick();
			}
		}
		private void NewUserCancel_Click(object sender, System.EventArgs e)
		{
			this.SetUIState(Client.UIState.LOGIN);
		}
		private void LoginNewUser_Click(object sender, System.EventArgs e)
		{
			this.SetUIState(Client.UIState.NEW_USER);
		}
		private bool NewUserValidate()
		{
			bool result = true;
			System.Windows.Forms.TextBox[] array = new System.Windows.Forms.TextBox[]
			{
				this.NewUserFirstName,
				this.NewUserLastName,
				this.NewUserTraderID,
				this.NewUserPassword,
				this.NewUserPasswordConfirm
			};
			System.Windows.Forms.TextBox[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				System.Windows.Forms.TextBox textBox = array2[i];
				if (string.IsNullOrWhiteSpace(textBox.Text))
				{
					this.NewUserErrorProvider.SetIconPadding(textBox, -20);
					this.NewUserErrorProvider.SetError(textBox, "Must not be blank.");
					result = false;
				}
				else
				{
					this.NewUserErrorProvider.SetError(textBox, "");
				}
			}
			if (!string.IsNullOrWhiteSpace(this.NewUserPasswordConfirm.Text))
			{
				if (this.NewUserPassword.Text != this.NewUserPasswordConfirm.Text)
				{
					this.NewUserErrorProvider.SetIconPadding(this.NewUserPasswordConfirm, -20);
					this.NewUserErrorProvider.SetError(this.NewUserPasswordConfirm, "Confirmation password does not match.");
					result = false;
				}
				else
				{
					this.NewUserErrorProvider.SetError(this.NewUserPasswordConfirm, "");
				}
			}
			return result;
		}
		private void NewUser_ValueChanged(object sender, System.EventArgs e)
		{
			this.NewUserValidate();
		}
		private void NewUser_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.NewUserButton.PerformClick();
			}
		}
		private void NewUserButton_Click(object sender, System.EventArgs e)
		{
			if (this.NewUserValidate())
			{
				Settings.Default.Save();
				this.SetUIState(Client.UIState.NEW_USER_CONNECTING);
				string address = this.NewUserAddress.Text.Trim();
				int port = System.Convert.ToInt32(this.NewUserPort.Value);
				string traderid = this.NewUserTraderID.Text.Trim();
				string password = this.NewUserPassword.Text.Trim();
				string firstname = this.NewUserFirstName.Text.Trim();
				string lastname = this.NewUserLastName.Text.Trim();
				System.Threading.ThreadPool.QueueUserWorkItem(delegate(object x)
				{
					if (ServiceManager.Connect(address, port))
					{
						ServiceManager.Execute(delegate(IClientService proxy)
						{
							proxy.NewUser(traderid, password, firstname, lastname);
						});
					}
				});
				return;
			}
			DialogHelper.ShowError("Please fill in all of the fields.", "Error");
		}
		private void logoutToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			if (DialogHelper.Confirm("Are you sure you want to logout?", "Confirmation", false) == System.Windows.Forms.DialogResult.OK)
			{
				ServiceManager.Disconnect();
			}
		}
		private void newWorkspaceToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			PromptDialog promptDialog = new PromptDialog("New Workspace", "Enter a unique name for the new workspace.", new System.Windows.Forms.Control[]
			{
				new System.Windows.Forms.TextBox()
			}, new string[]
			{
				"Name"
			});
			if (promptDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				try
				{
					TTSFormManager.Instance.AddWorkspace((string)promptDialog.Values[0], true);
				}
				catch (System.Exception ex)
				{
					DialogHelper.ShowError(ex.Message, "Error");
				}
			}
		}
		private void closeAllToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			if (DialogHelper.Confirm("Are you sure you want to close all open windows in this workspace?", "Confirmation", false) == System.Windows.Forms.DialogResult.OK)
			{
				TTSFormManager.Instance.ClearWorkspaceWindows(null);
			}
		}
		private void closeOrderEntriesToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			if (DialogHelper.Confirm("Are you sure you want to close all open order entry windows in this workspace?", "Confirmation", false) == System.Windows.Forms.DialogResult.OK)
			{
				TTSFormManager.Instance.ClearWorkspaceWindows(typeof(BuySellEntry));
			}
		}
		private void NewsLabel_Click(object sender, System.EventArgs e)
		{
			TTSFormManager.Instance.FindAddWindow(typeof(News), null);
		}
		private void tileHorizontallyToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			base.LayoutMdi(System.Windows.Forms.MdiLayout.TileHorizontal);
		}
		private void tileVerticallyToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			base.LayoutMdi(System.Windows.Forms.MdiLayout.TileVertical);
		}
		private void cascadeToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			base.LayoutMdi(System.Windows.Forms.MdiLayout.Cascade);
		}
		private void removeCurrentWorkspaceToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			try
			{
				if (DialogHelper.Confirm("Are you sure you want to delete this workspace?", "Confirmation", false) == System.Windows.Forms.DialogResult.OK)
				{
					TTSFormManager.Instance.RemoveWorkspace();
				}
			}
			catch (System.Exception ex)
			{
				DialogHelper.ShowError(ex.Message, "Error");
			}
		}
		private void saveWorkspaceToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			if (this.SaveWorkspaceDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
			{
				try
				{
					System.IO.File.WriteAllText(this.SaveWorkspaceDialog.FileName, TTSFormManager.Instance.SaveLayout());
				}
				catch (System.Exception ex)
				{
					DialogHelper.ShowError("Error saving workspace file: \n\n" + ex.Message, "Error");
				}
			}
		}
		private void loadWorkspaceToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			if (this.LoadWorkspaceDialog.ShowDialog(this) == System.Windows.Forms.DialogResult.OK && !TTSFormManager.Instance.LoadLayout(System.IO.File.ReadAllText(this.LoadWorkspaceDialog.FileName)))
			{
				DialogHelper.ShowError("Invalid workspace file.", "Error");
			}
		}
		private void marketViewToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			TTSFormManager.Instance.AddWindow(typeof(Portfolio), null, null);
		}
		private void traderInfoToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			TTSFormManager.Instance.AddWindow(typeof(TraderInfo), null, null);
		}
		private void transactionLogToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			TTSFormManager.Instance.AddWindow(typeof(TransactionLog), null, null);
		}
		private void tradeBlotterToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			TTSFormManager.Instance.AddWindow(typeof(TradeBlotter), null, null);
		}
		private void assetsToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			TTSFormManager.Instance.AddWindow(typeof(Assets), null, null);
		}
		private void newsToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			TTSFormManager.Instance.AddWindow(typeof(News), null, null);
		}
		private void killMenuItem1_Click(object sender, System.EventArgs e)
		{
			TTSFormManager.Instance.AddWindow(typeof(KillAll), null, null);
		}
		private void ladderTraderMenuItem_Click(object sender, System.EventArgs e)
		{
			TTSFormManager.Instance.AddWindow(typeof(LadderTrader), null, null);
		}
		private void bookTraderMenuItem_Click(object sender, System.EventArgs e)
		{
			TTSFormManager.Instance.AddWindow(typeof(BookTrader), null, null);
		}
		private void orderEntryToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			TTSFormManager.Instance.AddWindow(typeof(BuySellEntry), null, null);
		}
		private void spreadEntryToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			TTSFormManager.Instance.AddWindow(typeof(SpreadEntry), null, null);
		}
		private void transportationArbitrageEntryToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			TTSFormManager.Instance.AddWindow(typeof(TransportArbEntry), null, null);
		}
		private void chatToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			TTSFormManager.Instance.FindAddWindow(typeof(Chat), null);
		}
		private void oTCTradeToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			TTSFormManager.Instance.AddWindow(typeof(OTCEntry), null, null);
		}
		private void OTCLabel_Click(object sender, System.EventArgs e)
		{
			TTSFormManager.Instance.FindAddWindow(typeof(OTCEntry), null);
		}
		private void ChatLabel_Click(object sender, System.EventArgs e)
		{
			TTSFormManager.Instance.FindAddWindow(typeof(Chat), null);
		}
		private void securityChartingToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			TTSFormManager.Instance.AddWindow(typeof(SecurityCharting), null, null);
		}
		private void nLVChartingToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			TTSFormManager.Instance.AddWindow(typeof(PNLCharting), null, null);
		}
		private void timeSalesToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			TTSFormManager.Instance.AddWindow(typeof(TimeSales), null, null);
		}
		private void electricityChartingToolStripMenuItem_Click(object sender, System.EventArgs e)
		{
			TTSFormManager.Instance.AddWindow(typeof(ElectricityCharting), null, null);
		}
		private void RTDLabel_Click(object sender, System.EventArgs e)
		{
			new RTDInfo().ShowDialog(this);
		}
		private void button1_Click(object sender, System.EventArgs e)
		{
			if (!this.AutoBotInit)
			{
				this.AutoBotTimer.Elapsed += delegate(object se, ElapsedEventArgs ea)
				{
					base.BeginInvoke(delegate
					{
						string ticker = (
							from x in Game.State.Securities.Values
							where x.Parameters.IsTradeable && x.Parameters.StartPeriod <= Game.State.Current.Period && x.Parameters.StopPeriod >= Game.State.Current.Period
							orderby this.AutoBotRandom.NextDouble()
							select x).First<SecurityItem>().Parameters.Ticker;
						int volume = System.Convert.ToInt32(System.Math.Max(System.Convert.ToDouble(this.numericUpDown2.Value) * this.AutoBotRandom.NextDouble(), 1.0)) * System.Math.Sign(this.AutoBotRandom.NextDouble() - 0.5);
						OrderType type = (this.AutoBotRandom.NextDouble() > 0.5) ? OrderType.LIMIT : OrderType.MARKET;
						decimal price = 0m;
						if (type == OrderType.LIMIT)
						{
							price = Game.State.Securities[ticker].Last + Game.State.Securities[ticker].Last / 4m * System.Convert.ToDecimal(this.AutoBotRandom.NextDouble() - 0.5);
						}
						try
						{
							ServiceManager.Execute(delegate(IClientService p)
							{
								p.AddOrder(ticker, volume, type, price);
							});
						}
						catch
						{
						}
					});
				};
				this.AutoBotTimer.AutoReset = true;
			}
			this.AutoBotTimer.Interval = 60.0 / System.Convert.ToDouble(this.numericUpDown1.Value) * 1000.0;
			this.AutoBotTimer.Start();
		}
		private void button2_Click(object sender, System.EventArgs e)
		{
			this.AutoBotTimer.Stop();
		}
		public void ToggleTraderReportDownload(bool visible)
		{
			if (visible)
			{
				this.ReportPanel.Visible = true;
				this.ReportPanel.BackColor = ColorHelper.TTSYellow;
				this.TraderReportDownloadLink.Text = "Download";
				this.TraderReportDownloadLink.Enabled = true;
				this.TraderReportOpenLink.Visible = false;
				SystemSounds.Exclamation.Play();
				return;
			}
			this.ReportPanel.Visible = false;
		}
		private void TraderReportDownloadLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			if (this.ReportDownloadDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				this.TraderReportDownloadLink.Text = "Downloading...";
				this.TraderReportDownloadLink.Enabled = false;
				this.TraderReportOpenLink.Visible = false;
				try
				{
					using (System.IO.FileStream fileStream = new System.IO.FileStream(this.ReportDownloadDialog.FileName, System.IO.FileMode.Create))
					{
						int i = 0;
						while (true)
						{
							byte[] array = ServiceManager.Execute<byte[]>((IClientService x) => x.GetReportChunk(i));
							if (array == null || array.Length <= 0)
							{
								break;
							}
							fileStream.Write(array, 0, array.Length);
							i++;
						}
					}
					this.TraderReportDownloadLink.Text = "Download Again";
					this.TraderReportDownloadLink.Enabled = true;
					this.LastDownloadedReport = this.ReportDownloadDialog.FileName;
					this.TraderReportOpenLink.Visible = true;
				}
				catch (System.Exception ex)
				{
					DialogHelper.ShowError(ex.ToString(), "Report Download Error");
					this.ToggleTraderReportDownload(true);
				}
			}
		}
		private void TraderReportOpenLink_LinkClicked(object sender, System.Windows.Forms.LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(this.LastDownloadedReport);
		}
	}
}
