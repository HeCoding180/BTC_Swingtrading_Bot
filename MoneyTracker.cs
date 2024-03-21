using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BTC_Swingtrade_Simulator
{
    public class MoneyTracker
    {
        public double USD_Balance { private set; get; }
        public double BTC_Balance { private set; get; }

        public double BTCValue { set; get; }

        public MoneyTracker(double InitialBalance)
        {
            USD_Balance = InitialBalance;
            BTC_Balance = 0;
            BTCValue = 0;
        }
        public MoneyTracker(double InitialUSDBalance, double InitialBTCBalance, double InitialBTCWorth)
        {
            USD_Balance = InitialUSDBalance;
            BTC_Balance = InitialBTCBalance;
            BTCValue = InitialBTCWorth;
        }

        public void BTC_Tracker_NewBTCValueAvailable(object sender, BTCEventArgs e)
        {
            BTCValue = e.BTCValue;
        }

        public void BuyInBTC(double Amount)
        {
            USD_Balance -= BTCValue * Amount;
            BTC_Balance += Amount;
        }
        public void BuyInUSD(double Amount)
        {
            BTC_Balance += Amount / BTCValue;
            USD_Balance -= Amount;
        }
        public void SellInBTC(double Amount)
        {
            USD_Balance += BTCValue * Amount;
            BTC_Balance -= Amount;
        }
        public void SellInUSD(double Amount)
        {
            BTC_Balance -= Amount / BTCValue;
            USD_Balance += Amount;
        }
        public double GetTrackerValue()
        {
            return USD_Balance + BTC_Balance * BTCValue;
        }
    }
}
