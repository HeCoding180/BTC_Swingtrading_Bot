using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTC_Swingtrade_Simulator
{
    public enum TradingDirection
    {
        Buy,
        Sell,
        Cooldown,
        NoAction
    }

    public enum ComparisonResult
    {
        greater,
        less,
        equal
    }

    public class TradingResultEventArgs : EventArgs
    {
        public TradingDirection TradeDirection { set; get; }
        public double Amount { set; get; }
    }

    public class SMAUpdateEventArgs : EventArgs
    {
        public double longSMAValue { set; get; }
        public double shortSMAValue { set; get; }
    }

    class SMA
    {
        //public attributes
        public int length { private set; get; }

        //private attributes
        //value at index 0 is the newest value, value at index length - 1 is the oldest value in the history array
        private double[] history { set; get; }
        private int nValuesRetrieved { set; get; }

        public SMA(int averagingLength)
        {
            length = averagingLength;
            history = new double[averagingLength];
            nValuesRetrieved = 0;
        }

        //Non-static methods
        public void refresh(double input)
        {
            Array.Copy(history, 0, history, 1, length - 1);
            history[0] = input;
            if (nValuesRetrieved < length)
                nValuesRetrieved++;
        }

        public double get()
        {
            if (nValuesRetrieved == length)
                return history.Sum() / length;
            else
            {
                return double.PositiveInfinity;
            }
        }

        public bool Equals(SMA extSMA)
        {
            return get() == extSMA.get();
        }

        public override string ToString()
        {
            return get().ToString();
        }

        //Static methods
        public static ComparisonResult getSMAComparisonResult(SMA smaA, SMA smaB)
        {
            ComparisonResult comparisonResult;
            if (smaA > smaB) comparisonResult = ComparisonResult.greater;
            else if (smaA < smaB) comparisonResult = ComparisonResult.less;
            else comparisonResult = ComparisonResult.equal;

            return comparisonResult;
        }

        public static bool operator <(SMA smaA, SMA smaB)
        {
            return smaA.get() < smaB.get();
        }
        public static bool operator >(SMA smaA, SMA smaB)
        {
            return smaA.get() > smaB.get();
        }
        public static bool operator <=(SMA smaA, SMA smaB)
        {
            return smaA.get() <= smaB.get();
        }
        public static bool operator >=(SMA smaA, SMA smaB)
        {
            return smaA.get() >= smaB.get();
        }
        public static bool operator ==(SMA smaA, SMA smaB)
        {
            return smaA.get() == smaB.get();
        }
        public static bool operator !=(SMA smaA, SMA smaB)
        {
            return smaA.get() != smaB.get();
        }
    }

    class TradingBot
    {
        public event EventHandler<TradingResultEventArgs> TradeOccurred;
        public event EventHandler<SMAUpdateEventArgs> SMAChangeOccurred;

        public SMA LongSMA;
        public SMA ShortSMA;

        private ComparisonResult prevComparisonResult;

        public double LastBuyTrackerValue { private set; get; }

        public double TransactionCooldown { private set; get; }
        public double TransactionCooldown_Duration { private set; get; }

        public TradingBot(int nLongSMA, int nShortSMA)
        {
            if (nLongSMA <= nShortSMA) throw new InvalidOperationException("the Long SMA Count must be higher than the Low SMA Count");

            LongSMA = new SMA(nLongSMA);
            ShortSMA = new SMA(nShortSMA);

            TransactionCooldown = 0;
            TransactionCooldown_Duration = LongSMA.length;
        }

        private void InvokeTradeEvent(TradingDirection tradingDirection, double amount)
        {
            TradingResultEventArgs tradingResultArgs = new TradingResultEventArgs();
            tradingResultArgs.Amount = amount;
            tradingResultArgs.TradeDirection = tradingDirection;

            EventHandler<TradingResultEventArgs> handler = TradeOccurred;
            handler(this, tradingResultArgs);
        }
        private void InvokeSMAUpdateEvent()
        {
            SMAUpdateEventArgs smaUpdateEvArgs = new SMAUpdateEventArgs();
            smaUpdateEvArgs.longSMAValue = LongSMA.get();
            smaUpdateEvArgs.shortSMAValue = ShortSMA.get();

            EventHandler<SMAUpdateEventArgs> handler = SMAChangeOccurred;
            handler(this, smaUpdateEvArgs);
        }

        private TradingDirection DecideTradingAction()
        {
            TradingDirection returnAction;
            ComparisonResult liveComparisonResult = SMA.getSMAComparisonResult(ShortSMA, LongSMA);

            if (TransactionCooldown < TransactionCooldown_Duration)
                returnAction = TradingDirection.Cooldown;
            else if ((prevComparisonResult == ComparisonResult.less) && (liveComparisonResult == ComparisonResult.greater))
                returnAction = TradingDirection.Buy;
            else if ((prevComparisonResult == ComparisonResult.greater) && (liveComparisonResult == ComparisonResult.less))
                returnAction = TradingDirection.Sell;
            else
                returnAction = TradingDirection.NoAction;

            if(liveComparisonResult != ComparisonResult.equal)
                prevComparisonResult = liveComparisonResult;

            return returnAction;
        }

        public void RunAlgorythm(double NewBtcValue, ref MoneyTracker BalanceTracker)
        {
            LongSMA.refresh(NewBtcValue);
            ShortSMA.refresh(NewBtcValue);

            InvokeSMAUpdateEvent();

            //CalculateSMAValues
            TradingDirection tradingAction = DecideTradingAction();

            if (tradingAction == TradingDirection.Cooldown)
            {
                TransactionCooldown++;
                InvokeTradeEvent(TradingDirection.Cooldown, (double)(TransactionCooldown_Duration - TransactionCooldown));
            }
            else if (tradingAction == TradingDirection.Sell)
            {
                if (BalanceTracker.BTC_Balance > 0)
                {
                    //if (LastBuyTrackerValue < BalanceTracker.GetTrackerValue())
                    //{
                    InvokeTradeEvent(TradingDirection.Sell, BalanceTracker.BTC_Balance);

                    //Sell the whole BTC Balance
                    BalanceTracker.SellInBTC(BalanceTracker.BTC_Balance);

                    //TransactionCooldown = 0;
                    //TransactionCooldown_Duration = ShortSMA.length;
                    //}
                    //else
                    //{
                    //    InvokeTradeEvent(TradingDirection.Sell, -1.0f);
                    //}
                }
                else
                {
                    InvokeTradeEvent(TradingDirection.Sell, 0.0f);
                }
            }
            else if (tradingAction == TradingDirection.Buy)
            {
                if (BalanceTracker.USD_Balance > 0)
                {
                    InvokeTradeEvent(TradingDirection.Buy, BalanceTracker.USD_Balance);

                    //Buy the whole USD balance
                    BalanceTracker.BuyInUSD(BalanceTracker.USD_Balance);

                    LastBuyTrackerValue = BalanceTracker.GetTrackerValue();

                    //TransactionCooldown = 0;
                    //TransactionCooldown_Duration = ShortSMA.length;
                }
                else
                {
                    InvokeTradeEvent(TradingDirection.Buy, 0.0f);
                }
            }
            else
            {
                InvokeTradeEvent(TradingDirection.NoAction, 0.0f);
            }
        }
    }
}
