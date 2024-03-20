using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTC_Swingtrade_Simulator
{
    public partial class Form1 : Form
    {
        public readonly Color DEFAULT_BACKCOLOR = Color.FromArgb(52, 57, 69);
        public readonly Color LIGHT_BACKCOLOR = Color.FromArgb(66, 68, 84);

        public readonly Color DEFAULT_FORECOLOR = Color.FromArgb(40, 121, 235);
        public readonly Color DEFAULT_FORECOLOR_DARK = Color.FromArgb(37, 85, 153);

        public readonly Color NEGATIVE_COURSE_COLOR = Color.FromArgb(255, 0, 0);
        public readonly Color POSITIVE_COURSE_COLOR = Color.FromArgb(43, 255, 0);

        public readonly Color SHORT_SMA_COLOR = Color.FromArgb(58, 168, 50);
        public readonly Color LONG_SMA_COLOR = Color.FromArgb(171, 219, 57);

        public readonly Color SHORT_SMA_COLOR_DARK = Color.FromArgb(49, 110, 45);
        public readonly Color LONG_SMA_COLOR_DARK = Color.FromArgb(116, 145, 45);

        public readonly Color LIVE_BALANCE_COLOR = Color.FromArgb(152, 22, 217);
        public readonly Color LIVE_BALANCE_COLOR_DARK = Color.FromArgb(85, 41, 107);

        public readonly Color BUY_COLOR = Color.FromArgb(0, 255, 0);
        public readonly Color SELL_COLOR = Color.FromArgb(255, 0, 0);

        bool BTC_InitValue = true;
        bool AbortSimulation = false;

        bool NotificationsEnabled = false;

        int HistoryX = -1;

        MoneyTracker moneyTracker;
        ReadBTC_Value BTCReader;
        TradingBot tradingBot;
        InfoLabelHandler infoLabelHandler;
        Logger TradeLogger;

        public Form1()
        {
            InitializeComponent();

            // Install font
            using (Font checkFont = new Font("Open Sans Light", 9.75f, FontStyle.Regular, GraphicsUnit.Point))
            {
                if (checkFont.Name != "Open Sans Light")
                {
                    Process fontview_exe = Process.Start(Directory.GetCurrentDirectory().Replace(@"\bin\Debug", @"\Resources\Fonts\OpenSans-Light.ttf"));

                    while (fontview_exe.HasExited == false);

                    Application.Restart();
                }
            }

            using (SaveFileDialog SaveLogFileDialog = new SaveFileDialog())
            {
                SaveLogFileDialog.Filter = "csv files (*.csv)|*.csv";
                SaveLogFileDialog.FilterIndex = 1;
                SaveLogFileDialog.InitialDirectory = @"c:\\";
                SaveLogFileDialog.RestoreDirectory = true;
                SaveLogFileDialog.FileName = "BTC_SwingTrade_Simulator_Log__" + Logger.GetShortDateTimeString(DateTime.Now) + ".csv";

                DialogResult SaveLogFileResult = SaveLogFileDialog.ShowDialog();

                if (SaveLogFileResult == DialogResult.OK)
                {
                    TradeLogger = new Logger(',', SaveLogFileDialog.FileName);
                }
                else if(SaveLogFileResult == DialogResult.Cancel)
                {
                    AbortSimulation = true;
                }
            }

            if (!AbortSimulation)
            {
                DialogResult RunBotOnlineResult = MessageBox.Show("Do you want to run the Bot online?", "Set bot type", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (RunBotOnlineResult == DialogResult.Yes)
                {
                    BTCReader = new ReadBTC_Value();
                    BTCReader.NewBTCValueAvailable += BTC_Tracker_NewBTCValueAvailable;
                }
                else
                {
                    using (OpenFileDialog OpenHistoryFileDialog = new OpenFileDialog())
                    {
                        OpenHistoryFileDialog.Multiselect = false;
                        OpenHistoryFileDialog.Filter = "txt files (*.txt)|*.txt";
                        OpenHistoryFileDialog.FilterIndex = 1;
                        OpenHistoryFileDialog.InitialDirectory = @"c:\\";
                        OpenHistoryFileDialog.RestoreDirectory = true;

                        DialogResult OpenHistoryFileResult = OpenHistoryFileDialog.ShowDialog();

                        if (OpenHistoryFileResult == DialogResult.OK)
                        {
                            BTCReader = new ReadBTC_Value(OpenHistoryFileDialog.FileName, 1);
                            BTCReader.NewBTCValueAvailable += BTC_Tracker_NewBTCValueAvailable;
                        }
                        else if (OpenHistoryFileResult == DialogResult.Cancel)
                        {
                            AbortSimulation = true;
                        }
                    }
                }
            }

            moneyTracker = new MoneyTracker(10);
            tradingBot = new TradingBot(48, 40, 8, 8);
            infoLabelHandler = new InfoLabelHandler(5000);

            tradingBot.TradeOccurred += TradingBot_TradeOccurred;
            tradingBot.SMAChangeOccurred += TradingBot_SMAChangeOccurred;
            infoLabelHandler.InfoLabelUpdate += InfoLabelHandler_InfoLabelUpdate;

            this.BackColor = DEFAULT_BACKCOLOR;
            MainPannel.BackColor = LIGHT_BACKCOLOR;

            lTitle.ForeColor = DEFAULT_FORECOLOR_DARK;

            minimizeButton.BackColor = LIGHT_BACKCOLOR;

            notificationButton.BackColor = LIGHT_BACKCOLOR;

            BTCWorthLabel.ForeColor = DEFAULT_FORECOLOR;

            lLong.ForeColor = LONG_SMA_COLOR;
            lShort.ForeColor = SHORT_SMA_COLOR;

            lLiveBalanceWorth.ForeColor = LIVE_BALANCE_COLOR;

            HistoryChart.BackColor = LIGHT_BACKCOLOR;
            HistoryChart.BorderlineColor = LIGHT_BACKCOLOR;
            HistoryChart.ChartAreas["HistoryChart"].BackColor = LIGHT_BACKCOLOR;

            HistoryChart.Series["BTC Value"].Color = DEFAULT_FORECOLOR_DARK;
            HistoryChart.Series["Short SMA"].Color = SHORT_SMA_COLOR_DARK;
            HistoryChart.Series["Upper Long SMA"].Color = LONG_SMA_COLOR_DARK;
            HistoryChart.Series["Lower Long SMA"].Color = LONG_SMA_COLOR_DARK;
            HistoryChart.Series["SimValue"].Color = LIVE_BALANCE_COLOR_DARK;
            HistoryChart.Series["Buy Actions"].Color = BUY_COLOR;
            HistoryChart.Series["Sell Actions"].Color = SELL_COLOR;

            //Set MainPannel Objects' BackColor to Transparent
            BTCWorthLabel.Parent = HistoryChart;
            BTCWorthChangeLabel.Parent = HistoryChart;
            timeLabel.Parent = HistoryChart;
            lBalances.Parent = HistoryChart;
            lUSDBal.Parent = HistoryChart;
            lBTCBal.Parent = HistoryChart;
            LIntegrals.Parent = HistoryChart;
            LDispLong.Parent = HistoryChart;
            LDispShort.Parent = HistoryChart;
            lLong.Parent = HistoryChart;
            lShort.Parent = HistoryChart;
            LliveBalance.Parent = HistoryChart;
            lLiveBalanceWorth.Parent = HistoryChart;
            InfoBar.Parent = HistoryChart;

            BTCWorthLabel.BackColor = Color.Transparent;
            BTCWorthChangeLabel.BackColor = Color.Transparent;
            timeLabel.BackColor = Color.Transparent;
            lBalances.BackColor = Color.Transparent;
            lUSDBal.BackColor = Color.Transparent;
            lBTCBal.BackColor = Color.Transparent;
            LIntegrals.BackColor = Color.Transparent;
            LDispLong.BackColor = Color.Transparent;
            LDispShort.BackColor = Color.Transparent;
            lLong.BackColor = Color.Transparent;
            lShort.BackColor = Color.Transparent;
            LliveBalance.BackColor = Color.Transparent;
            lLiveBalanceWorth.BackColor = Color.Transparent;
            InfoBar.BackColor = Color.Transparent;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (AbortSimulation) Application.Exit();
            else
            {
                BTCReader.Start();
                this.Icon = new Icon(Directory.GetCurrentDirectory().Replace(@"bin\Debug", @"Resources\Bitcoin.ico"));
                TrayIcon.Icon = new Icon(Directory.GetCurrentDirectory().Replace(@"bin\Debug", @"Resources\Bitcoin.ico"));
            }
        }
        private void bClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x84:
                    base.WndProc(ref m);
                    if ((int)m.Result == 0x1)
                        m.Result = (IntPtr)0x2;
                    return;
            }
            base.WndProc(ref m);
        }

        private void BTC_Tracker_NewBTCValueAvailable(object sender, BTCEventArgs e)
        {
            BTCWorthLabel.Text = e.BTCValue.ToString() + "$";

            if (e.BTCPercentageChange >= 0)
            {
                BTCWorthChangeLabel.Text = "+" + e.BTCPercentageChange.ToString() + "%";
                BTCWorthChangeLabel.ForeColor = POSITIVE_COURSE_COLOR;
            }
            else
            {
                BTCWorthChangeLabel.Text = e.BTCPercentageChange.ToString() + "%";
                BTCWorthChangeLabel.ForeColor = NEGATIVE_COURSE_COLOR;
            }

            moneyTracker.BTCValue = e.BTCValue;
            tradingBot.RunAlgorythm(e.BTCValue, ref moneyTracker);

            if(BTC_InitValue) // Initial BTC Value (Initialization Cycle)
            {
                BTC_InitValue = false;
                //moneyTracker.BuyInUSD(0.5f * moneyTracker.USD_Balance);
            }

            lUSDBal.Text = "USD: " + moneyTracker.USD_Balance.ToString() + "$";
            lBTCBal.Text = "BTC: " + moneyTracker.BTC_Balance.ToString();
            lLiveBalanceWorth.Text = moneyTracker.GetTrackerValue().ToString() + "$";
            TrayIcon.BalloonTipText = moneyTracker.GetTrackerValue().ToString() + "$";
            if(NotificationsEnabled) TrayIcon.ShowBalloonTip(1000);
        }

        private void TradingBot_SMAChangeOccurred(object sender, SMAUpdateEventArgs e)
        {
            HistoryX++;

            lLong.Text = tradingBot.LongSMA.ToString();
            lShort.Text = tradingBot.ShortSMA.ToString();

            HistoryChart.Series["BTC Value"].Points.AddXY(HistoryX, moneyTracker.BTCValue);

            if (!double.IsInfinity(tradingBot.LongSMA.get()))
            {
                HistoryChart.Series["Upper Long SMA"].Points.AddXY(HistoryX, tradingBot.LongSMA.get() + tradingBot.upperSmaOffset);
                HistoryChart.Series["Lower Long SMA"].Points.AddXY(HistoryX, tradingBot.LongSMA.get() - tradingBot.lowerSmaOffset);
            }
            if (!double.IsInfinity(tradingBot.ShortSMA.get()))
                HistoryChart.Series["Short SMA"].Points.AddXY(HistoryX, tradingBot.ShortSMA.get());

            HistoryChart.Series["SimValue"].Points.AddXY(HistoryX, moneyTracker.GetTrackerValue());
        }
        private void TradingBot_TradeOccurred(object sender, TradingResultEventArgs e)
        {
            TradingDirection LogTradeDir = TradingDirection.NoAction;
            switch (e.TradeDirection)
            {
                case TradingDirection.Buy:
                    if (e.Amount > 0)
                    {
                        infoLabelHandler.setInfo(e.Amount + " USD were traded into BTC");
                        LogTradeDir = TradingDirection.Buy;
                        HistoryChart.Series["Buy Actions"].Points.AddXY(HistoryX, moneyTracker.BTCValue);
                    }
                    else if (e.Amount == 0.0f)
                        infoLabelHandler.setInfo("Insufficient Funds to Buy BTC");
                    else if (e.Amount == -1.0f)
                        infoLabelHandler.setInfo("Would have sold BTC but it would've been a bad trade");
                    break;
                case TradingDirection.Sell:
                    if (e.Amount > 0)
                    {
                        infoLabelHandler.setInfo(e.Amount + " BTC were traded into USD");
                        LogTradeDir = TradingDirection.Sell;
                        HistoryChart.Series["Sell Actions"].Points.AddXY(HistoryX, moneyTracker.BTCValue);
                    }
                    else if (e.Amount == 0.0f)
                        infoLabelHandler.setInfo("Insufficient Funds to Sell BTC");
                    else if (e.Amount == -1.0f)
                        infoLabelHandler.setInfo("Would have bought BTC but it would've been a bad trade");
                    break;
                case TradingDirection.Cooldown:
                    infoLabelHandler.setInfo("Bot on cooldown for another " + e.Amount + " Cycles");
                    LogTradeDir = TradingDirection.Cooldown;
                    break;
                case TradingDirection.NoAction:
                    if (BTCReader.ReaderType == BTCRetrievalType.Online)
                        infoLabelHandler.setInfo("No action was performed");
                    LogTradeDir = TradingDirection.NoAction;
                    break;
                default:
                    break;
            }
            if (BTCReader.ReaderType == BTCRetrievalType.Online)
                TradeLogger.AppenLogEntry(moneyTracker, BTCReader, tradingBot, LogTradeDir, infoLabelHandler.InfoLabelText);
            else
            {
                TradeLogger.AppenLogEntry(moneyTracker, BTCReader, tradingBot, LogTradeDir, infoLabelHandler.InfoLabelText, (BTCReader.OfflineBTCArrayIndex + 1).ToString());
            }
        }
        private void InfoLabelHandler_InfoLabelUpdate(object sender, InfoLabelEventArgs e)
        {
            InfoBar.Text = e.InfoLabelText;
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            this.Hide();
            TrayIcon.Visible = true;
        }

        private void TrayIcon_DoubleClick(object sender, EventArgs e)
        {
            this.Show();
            TrayIcon.Visible = false;
        }

        private void TimeDisplayTimer_Tick(object sender, EventArgs e)
        {
            DateTime DT = DateTime.Now;
            timeLabel.Text = DT.Day.ToString("D2") + "/" + DT.Month.ToString("D2") + "/" + DT.Year.ToString("D4") + " "
                           + DT.Hour.ToString("D2") + ":" + DT.Minute.ToString("D2") + ":" + DT.Second.ToString("D2");
        }

        private void infoButton_Click(object sender, EventArgs e)
        {
            if(NotificationsEnabled)
            {
                NotificationsEnabled = false;
                notificationButton.BackgroundImage = Image.FromFile(Directory.GetCurrentDirectory().Replace(@"bin\Debug", @"Resources\MuteIcon.png"));
            }
            else
            {
                NotificationsEnabled = true;
                notificationButton.BackgroundImage = Image.FromFile(Directory.GetCurrentDirectory().Replace(@"bin\Debug", @"Resources\InfoIcon.png"));
            }
        }
    }
}
