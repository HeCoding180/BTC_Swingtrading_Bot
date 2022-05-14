namespace BTC_Swingtrade_Simulator
{
    partial class Form1
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.MainPannel = new System.Windows.Forms.Panel();
            this.timeLabel = new System.Windows.Forms.Label();
            this.lLiveBalanceWorth = new System.Windows.Forms.Label();
            this.LliveBalance = new System.Windows.Forms.Label();
            this.lShort = new System.Windows.Forms.Label();
            this.lLong = new System.Windows.Forms.Label();
            this.LDispShort = new System.Windows.Forms.Label();
            this.LDispLong = new System.Windows.Forms.Label();
            this.LIntegrals = new System.Windows.Forms.Label();
            this.lBTCBal = new System.Windows.Forms.Label();
            this.lUSDBal = new System.Windows.Forms.Label();
            this.lBalances = new System.Windows.Forms.Label();
            this.BTCWorthChangeLabel = new System.Windows.Forms.Label();
            this.InfoBar = new System.Windows.Forms.Label();
            this.BTCWorthLabel = new System.Windows.Forms.Label();
            this.lTitle = new System.Windows.Forms.Label();
            this.TrayIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.minimizeButton = new System.Windows.Forms.Button();
            this.bClose = new System.Windows.Forms.Button();
            this.TimeDisplayTimer = new System.Windows.Forms.Timer(this.components);
            this.notificationButton = new System.Windows.Forms.Button();
            this.HistoryChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.MainPannel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryChart)).BeginInit();
            this.SuspendLayout();
            // 
            // MainPannel
            // 
            this.MainPannel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainPannel.BackColor = System.Drawing.SystemColors.ControlDark;
            this.MainPannel.Controls.Add(this.timeLabel);
            this.MainPannel.Controls.Add(this.lLiveBalanceWorth);
            this.MainPannel.Controls.Add(this.LliveBalance);
            this.MainPannel.Controls.Add(this.lShort);
            this.MainPannel.Controls.Add(this.lLong);
            this.MainPannel.Controls.Add(this.LDispShort);
            this.MainPannel.Controls.Add(this.LDispLong);
            this.MainPannel.Controls.Add(this.LIntegrals);
            this.MainPannel.Controls.Add(this.lBTCBal);
            this.MainPannel.Controls.Add(this.lUSDBal);
            this.MainPannel.Controls.Add(this.lBalances);
            this.MainPannel.Controls.Add(this.BTCWorthChangeLabel);
            this.MainPannel.Controls.Add(this.InfoBar);
            this.MainPannel.Controls.Add(this.BTCWorthLabel);
            this.MainPannel.Controls.Add(this.HistoryChart);
            this.MainPannel.Location = new System.Drawing.Point(1, 26);
            this.MainPannel.Name = "MainPannel";
            this.MainPannel.Size = new System.Drawing.Size(798, 473);
            this.MainPannel.TabIndex = 0;
            // 
            // timeLabel
            // 
            this.timeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.timeLabel.AutoSize = true;
            this.timeLabel.Font = new System.Drawing.Font("Open Sans Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeLabel.Location = new System.Drawing.Point(673, 450);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(122, 19);
            this.timeLabel.TabIndex = 16;
            this.timeLabel.Text = "00.00.0000 00:00:00";
            this.timeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lLiveBalanceWorth
            // 
            this.lLiveBalanceWorth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lLiveBalanceWorth.AutoSize = true;
            this.lLiveBalanceWorth.Font = new System.Drawing.Font("Open Sans Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lLiveBalanceWorth.Location = new System.Drawing.Point(11, 380);
            this.lLiveBalanceWorth.Name = "lLiveBalanceWorth";
            this.lLiveBalanceWorth.Size = new System.Drawing.Size(76, 42);
            this.lLiveBalanceWorth.TabIndex = 15;
            this.lLiveBalanceWorth.Text = "0.0$";
            this.lLiveBalanceWorth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LliveBalance
            // 
            this.LliveBalance.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LliveBalance.AutoSize = true;
            this.LliveBalance.Font = new System.Drawing.Font("Open Sans Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LliveBalance.Location = new System.Drawing.Point(11, 349);
            this.LliveBalance.Name = "LliveBalance";
            this.LliveBalance.Size = new System.Drawing.Size(136, 31);
            this.LliveBalance.TabIndex = 13;
            this.LliveBalance.Text = "Live Balance:";
            this.LliveBalance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lShort
            // 
            this.lShort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lShort.AutoSize = true;
            this.lShort.Font = new System.Drawing.Font("Open Sans Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lShort.Location = new System.Drawing.Point(104, 307);
            this.lShort.Name = "lShort";
            this.lShort.Size = new System.Drawing.Size(35, 42);
            this.lShort.TabIndex = 12;
            this.lShort.Text = "0";
            this.lShort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lLong
            // 
            this.lLong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lLong.AutoSize = true;
            this.lLong.Font = new System.Drawing.Font("Open Sans Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lLong.Location = new System.Drawing.Point(104, 265);
            this.lLong.Name = "lLong";
            this.lLong.Size = new System.Drawing.Size(35, 42);
            this.lLong.TabIndex = 11;
            this.lLong.Text = "0";
            this.lLong.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LDispShort
            // 
            this.LDispShort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LDispShort.AutoSize = true;
            this.LDispShort.Font = new System.Drawing.Font("Open Sans Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LDispShort.Location = new System.Drawing.Point(11, 307);
            this.LDispShort.Name = "LDispShort";
            this.LDispShort.Size = new System.Drawing.Size(96, 42);
            this.LDispShort.TabIndex = 10;
            this.LDispShort.Text = "Short:";
            this.LDispShort.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LDispLong
            // 
            this.LDispLong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LDispLong.AutoSize = true;
            this.LDispLong.Font = new System.Drawing.Font("Open Sans Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LDispLong.Location = new System.Drawing.Point(11, 265);
            this.LDispLong.Name = "LDispLong";
            this.LDispLong.Size = new System.Drawing.Size(89, 42);
            this.LDispLong.TabIndex = 9;
            this.LDispLong.Text = "Long:";
            this.LDispLong.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // LIntegrals
            // 
            this.LIntegrals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LIntegrals.AutoSize = true;
            this.LIntegrals.Font = new System.Drawing.Font("Open Sans Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LIntegrals.Location = new System.Drawing.Point(11, 234);
            this.LIntegrals.Name = "LIntegrals";
            this.LIntegrals.Size = new System.Drawing.Size(248, 31);
            this.LIntegrals.TabIndex = 8;
            this.LIntegrals.Text = "Simple Moving Averages:";
            this.LIntegrals.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lBTCBal
            // 
            this.lBTCBal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lBTCBal.AutoSize = true;
            this.lBTCBal.Font = new System.Drawing.Font("Open Sans Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lBTCBal.Location = new System.Drawing.Point(11, 192);
            this.lBTCBal.Name = "lBTCBal";
            this.lBTCBal.Size = new System.Drawing.Size(176, 42);
            this.lBTCBal.TabIndex = 7;
            this.lBTCBal.Text = "BTC: 0.0011";
            this.lBTCBal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lUSDBal
            // 
            this.lUSDBal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lUSDBal.AutoSize = true;
            this.lUSDBal.Font = new System.Drawing.Font("Open Sans Light", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lUSDBal.Location = new System.Drawing.Point(11, 150);
            this.lUSDBal.Name = "lUSDBal";
            this.lUSDBal.Size = new System.Drawing.Size(165, 42);
            this.lUSDBal.TabIndex = 6;
            this.lUSDBal.Text = "USD: 50.0$";
            this.lUSDBal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lBalances
            // 
            this.lBalances.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lBalances.AutoSize = true;
            this.lBalances.Font = new System.Drawing.Font("Open Sans Light", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lBalances.Location = new System.Drawing.Point(11, 119);
            this.lBalances.Name = "lBalances";
            this.lBalances.Size = new System.Drawing.Size(168, 31);
            this.lBalances.TabIndex = 5;
            this.lBalances.Text = "Virtual Balances:";
            this.lBalances.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BTCWorthChangeLabel
            // 
            this.BTCWorthChangeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTCWorthChangeLabel.Font = new System.Drawing.Font("Open Sans Light", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTCWorthChangeLabel.Location = new System.Drawing.Point(11, 96);
            this.BTCWorthChangeLabel.Name = "BTCWorthChangeLabel";
            this.BTCWorthChangeLabel.Size = new System.Drawing.Size(753, 23);
            this.BTCWorthChangeLabel.TabIndex = 4;
            this.BTCWorthChangeLabel.Text = "+0.00%";
            this.BTCWorthChangeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // InfoBar
            // 
            this.InfoBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.InfoBar.AutoSize = true;
            this.InfoBar.Font = new System.Drawing.Font("Open Sans Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.InfoBar.Location = new System.Drawing.Point(3, 450);
            this.InfoBar.Name = "InfoBar";
            this.InfoBar.Size = new System.Drawing.Size(165, 19);
            this.InfoBar.TabIndex = 3;
            this.InfoBar.Text = "Init, retrieving bitcoin value";
            // 
            // BTCWorthLabel
            // 
            this.BTCWorthLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BTCWorthLabel.Font = new System.Drawing.Font("Open Sans Light", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BTCWorthLabel.Location = new System.Drawing.Point(11, 11);
            this.BTCWorthLabel.Name = "BTCWorthLabel";
            this.BTCWorthLabel.Size = new System.Drawing.Size(776, 91);
            this.BTCWorthLabel.TabIndex = 0;
            this.BTCWorthLabel.Text = "00000.0000$";
            this.BTCWorthLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lTitle
            // 
            this.lTitle.AutoSize = true;
            this.lTitle.Font = new System.Drawing.Font("Open Sans Light", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lTitle.Location = new System.Drawing.Point(5, 4);
            this.lTitle.Name = "lTitle";
            this.lTitle.Size = new System.Drawing.Size(193, 19);
            this.lTitle.TabIndex = 2;
            this.lTitle.Text = "Bitcoin Swing Trading Simulator";
            // 
            // TrayIcon
            // 
            this.TrayIcon.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.TrayIcon.BalloonTipTitle = "Live Balance:";
            this.TrayIcon.Text = "Bitcoin Swing Trading Simulator";
            this.TrayIcon.DoubleClick += new System.EventHandler(this.TrayIcon_DoubleClick);
            // 
            // minimizeButton
            // 
            this.minimizeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.minimizeButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.minimizeButton.BackgroundImage = global::BTC_Swingtrade_Simulator.Properties.Resources.Minimize_Icon;
            this.minimizeButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.minimizeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.minimizeButton.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minimizeButton.Location = new System.Drawing.Point(750, 1);
            this.minimizeButton.Name = "minimizeButton";
            this.minimizeButton.Size = new System.Drawing.Size(24, 24);
            this.minimizeButton.TabIndex = 3;
            this.minimizeButton.TabStop = false;
            this.minimizeButton.UseVisualStyleBackColor = false;
            this.minimizeButton.Click += new System.EventHandler(this.minimizeButton_Click);
            // 
            // bClose
            // 
            this.bClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bClose.BackColor = System.Drawing.SystemColors.ControlDark;
            this.bClose.BackgroundImage = global::BTC_Swingtrade_Simulator.Properties.Resources.CloseIcon_Red;
            this.bClose.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bClose.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bClose.Location = new System.Drawing.Point(775, 1);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(24, 24);
            this.bClose.TabIndex = 1;
            this.bClose.TabStop = false;
            this.bClose.UseVisualStyleBackColor = false;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // TimeDisplayTimer
            // 
            this.TimeDisplayTimer.Enabled = true;
            this.TimeDisplayTimer.Interval = 1000;
            this.TimeDisplayTimer.Tick += new System.EventHandler(this.TimeDisplayTimer_Tick);
            // 
            // notificationButton
            // 
            this.notificationButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.notificationButton.BackColor = System.Drawing.SystemColors.ControlDark;
            this.notificationButton.BackgroundImage = global::BTC_Swingtrade_Simulator.Properties.Resources.MuteIcon;
            this.notificationButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.notificationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.notificationButton.Font = new System.Drawing.Font("Open Sans", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.notificationButton.Location = new System.Drawing.Point(725, 1);
            this.notificationButton.Name = "notificationButton";
            this.notificationButton.Size = new System.Drawing.Size(24, 24);
            this.notificationButton.TabIndex = 4;
            this.notificationButton.TabStop = false;
            this.notificationButton.UseVisualStyleBackColor = false;
            this.notificationButton.Click += new System.EventHandler(this.infoButton_Click);
            // 
            // HistoryChart
            // 
            this.HistoryChart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HistoryChart.BackColor = System.Drawing.SystemColors.ControlDark;
            this.HistoryChart.BorderlineColor = System.Drawing.SystemColors.ControlDark;
            chartArea2.AxisX.IsMarginVisible = false;
            chartArea2.AxisX.IsMarksNextToAxis = false;
            chartArea2.AxisX.LabelStyle.Enabled = false;
            chartArea2.AxisX.LineWidth = 0;
            chartArea2.AxisX.MajorGrid.Enabled = false;
            chartArea2.AxisX.MajorTickMark.Enabled = false;
            chartArea2.AxisX2.MajorGrid.Enabled = false;
            chartArea2.AxisY.IsStartedFromZero = false;
            chartArea2.AxisY.LabelStyle.Enabled = false;
            chartArea2.AxisY.LineWidth = 0;
            chartArea2.AxisY.MajorGrid.Enabled = false;
            chartArea2.AxisY.MajorTickMark.Enabled = false;
            chartArea2.AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.True;
            chartArea2.AxisY2.InterlacedColor = System.Drawing.Color.White;
            chartArea2.AxisY2.IsStartedFromZero = false;
            chartArea2.AxisY2.MajorGrid.Enabled = false;
            chartArea2.BackColor = System.Drawing.SystemColors.AppWorkspace;
            chartArea2.BorderWidth = 0;
            chartArea2.InnerPlotPosition.Auto = false;
            chartArea2.InnerPlotPosition.Height = 100F;
            chartArea2.InnerPlotPosition.Width = 100F;
            chartArea2.Name = "HistoryChart";
            chartArea2.Position.Auto = false;
            chartArea2.Position.Height = 100F;
            chartArea2.Position.Width = 100F;
            this.HistoryChart.ChartAreas.Add(chartArea2);
            this.HistoryChart.Location = new System.Drawing.Point(0, 0);
            this.HistoryChart.Margin = new System.Windows.Forms.Padding(0);
            this.HistoryChart.Name = "HistoryChart";
            this.HistoryChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.None;
            series5.ChartArea = "HistoryChart";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Color = System.Drawing.Color.DodgerBlue;
            series5.Name = "BTC Value";
            series5.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series5.YValuesPerPoint = 4;
            series6.ChartArea = "HistoryChart";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Name = "Short SMA";
            series7.ChartArea = "HistoryChart";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Name = "Long SMA";
            series8.ChartArea = "HistoryChart";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series8.Name = "SimValue";
            series8.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            this.HistoryChart.Series.Add(series5);
            this.HistoryChart.Series.Add(series6);
            this.HistoryChart.Series.Add(series7);
            this.HistoryChart.Series.Add(series8);
            this.HistoryChart.Size = new System.Drawing.Size(798, 473);
            this.HistoryChart.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.notificationButton);
            this.Controls.Add(this.minimizeButton);
            this.Controls.Add(this.lTitle);
            this.Controls.Add(this.bClose);
            this.Controls.Add(this.MainPannel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Trading Bot Emulator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MainPannel.ResumeLayout(false);
            this.MainPannel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel MainPannel;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.Label lTitle;
        private System.Windows.Forms.Label BTCWorthLabel;
        private System.Windows.Forms.Label InfoBar;
        private System.Windows.Forms.Label BTCWorthChangeLabel;
        private System.Windows.Forms.Label lBalances;
        private System.Windows.Forms.Label lBTCBal;
        private System.Windows.Forms.Label lUSDBal;
        private System.Windows.Forms.Label LIntegrals;
        private System.Windows.Forms.Label LDispShort;
        private System.Windows.Forms.Label LDispLong;
        private System.Windows.Forms.Label lShort;
        private System.Windows.Forms.Label lLong;
        private System.Windows.Forms.Label lLiveBalanceWorth;
        private System.Windows.Forms.Label LliveBalance;
        private System.Windows.Forms.Button minimizeButton;
        private System.Windows.Forms.NotifyIcon TrayIcon;
        private System.Windows.Forms.Label timeLabel;
        private System.Windows.Forms.Timer TimeDisplayTimer;
        private System.Windows.Forms.Button notificationButton;
        private System.Windows.Forms.DataVisualization.Charting.Chart HistoryChart;
    }
}

