using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTC_Swingtrade_Simulator
{
    class Logger
    {
        public char Delimiter { private set; get; }
        public string LogFilePath { private set; get; }

        public Logger(char delimiter, string FilePath)
        {
            Delimiter = delimiter;
            LogFilePath = FilePath;
            File.WriteAllText(LogFilePath, "Date/Time" + Delimiter
                                         + "USD Balance" + Delimiter
                                         + "BTC Balance" + Delimiter
                                         + "BTC Value" + Delimiter
                                         + "Total Simulation Value" + Delimiter
                                         + "Short SMA" + Delimiter
                                         + "Long SMA" + Delimiter
                                         + "BTC Change" + Delimiter
                                         + "Trading Direction" + Delimiter
                                         + "Trading Action"
                                         + Environment.NewLine);
        }

        public void AppenLogEntry(MoneyTracker moneyTracker, ReadBTC_Value readBTC_Value, TradingBot tradingBot, TradingDirection TradeDir, string tradingAction)
        {
            File.AppendAllText(LogFilePath, GetCSVDateTimeString(readBTC_Value.currentEventArgs.timeUpdated) + Delimiter
                                          + moneyTracker.USD_Balance.ToString() + Delimiter
                                          + moneyTracker.BTC_Balance.ToString() + Delimiter
                                          + moneyTracker.BTCValue.ToString() + Delimiter
                                          + moneyTracker.GetTrackerValue().ToString() + Delimiter
                                          + tradingBot.ShortSMA.ToString() + Delimiter
                                          + tradingBot.LongSMA.ToString() + Delimiter
                                          + readBTC_Value.currentEventArgs.BTCPercentageChange + Delimiter
                                          + TradeDir.ToString() + Delimiter
                                          + tradingAction
                                          + Environment.NewLine);
        }
        public void AppenLogEntry(MoneyTracker moneyTracker, ReadBTC_Value readBTC_Value, TradingBot tradingBot, TradingDirection TradeDir, string tradingAction, string DateTimeAlternative)
        {
            File.AppendAllText(LogFilePath, DateTimeAlternative + Delimiter
                                          + moneyTracker.USD_Balance.ToString() + Delimiter
                                          + moneyTracker.BTC_Balance.ToString() + Delimiter
                                          + moneyTracker.BTCValue.ToString() + Delimiter
                                          + moneyTracker.GetTrackerValue().ToString() + Delimiter
                                          + tradingBot.ShortSMA.ToString() + Delimiter
                                          + tradingBot.LongSMA.ToString() + Delimiter
                                          + readBTC_Value.currentEventArgs.BTCPercentageChange + Delimiter
                                          + TradeDir.ToString() + Delimiter
                                          + tradingAction
                                          + Environment.NewLine);
        }

        public static string GetShortDateTimeString(DateTime dateTime)
        {
            DateTime CurrentDateTime = dateTime;
            return CurrentDateTime.Year.ToString("D4")
                 + CurrentDateTime.Month.ToString("D2")
                 + CurrentDateTime.Day.ToString("D2") + "_"
                 + CurrentDateTime.Hour.ToString("D2")
                 + CurrentDateTime.Minute.ToString("D2")
                 + CurrentDateTime.Second.ToString("D2");
        }

        public static string GetCSVDateTimeString(DateTime dateTime)
        {
            DateTime CurrentDateTime = dateTime;
            return CurrentDateTime.Day.ToString("D2") + "/"
                 + CurrentDateTime.Month.ToString("D2") + "/"
                 + CurrentDateTime.Year.ToString("D4") + " "
                 + CurrentDateTime.Hour.ToString("D2") + ":"
                 + CurrentDateTime.Minute.ToString("D2") + ":"
                 + CurrentDateTime.Second.ToString("D2");
        }
    }
}
