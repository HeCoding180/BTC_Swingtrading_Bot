using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
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
        equal,
        unknown
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

    public class OfflineSimulationDoneEventArgs : EventArgs
    {
        public List<TradingStep> Trades { set; get; }
        public double EndBalance { set; get; }
    }

    public class SMA
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
        public static ComparisonResult getSMAComparisonResult(SMA smaA, SMA smaB, int offsetA, int offsetB)
        {
            ComparisonResult comparisonResult;
            if ((smaA.get() + offsetA) > (smaB.get() + offsetB)) comparisonResult = ComparisonResult.greater;
            else if ((smaA.get() + offsetA) < (smaB.get() + offsetB)) comparisonResult = ComparisonResult.less;
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
    
    public class TradingStep
    {
        public double LongSMA;
        public double ShortSMA;

        public MoneyTracker Balance;

        public TradingDirection Action;

        public TradingStep()
        {
            LongSMA = 0.0;
            ShortSMA = 0.0;
            Balance = new MoneyTracker(0);
            Action = TradingDirection.NoAction;
            
        }
        public TradingStep(double longSMA, double shortSMA, MoneyTracker balance)
        {
            LongSMA = longSMA;
            ShortSMA = shortSMA;
            Balance = balance;
            Action = TradingDirection.NoAction;
        }
        public TradingStep(double longSMA, double shortSMA, MoneyTracker balance, TradingDirection tradingAction)
        {
            LongSMA = longSMA;
            ShortSMA = shortSMA;
            Balance = balance;
            Action = tradingAction;
        }
    }

    public class TradingBot
    {
        public event EventHandler<TradingResultEventArgs> TradeOccurred;
        public event EventHandler<SMAUpdateEventArgs> SMAChangeOccurred;
        public event EventHandler<OfflineSimulationDoneEventArgs> OfflineSimulationDone;

        private int lSmaSize;
        private int sSmaSize;

        public int LongSmaSize
        {
            set
            {
                lSmaSize = value;
                LongSMA = new SMA(value);
            }
            get { return lSmaSize; }
        }
        public int ShortSmaSize
        {
            set
            {
                sSmaSize = value;
                ShortSMA = new SMA(value);
            }
            get { return sSmaSize; }
        }

        public SMA LongSMA;
        public SMA ShortSMA;

        public int SmaOffset;

        private ComparisonResult prevComparisonResult;

        public double TransactionCooldown { private set; get; }
        public double TransactionCooldown_Duration { private set; get; }

        public TradingBot(int longSmaSize, int shortSmaSize, int thresholdOffset)
        {
            if (longSmaSize <= shortSmaSize) throw new InvalidOperationException("the Long SMA Count must be higher than the Low SMA Count");

            LongSmaSize = longSmaSize;
            ShortSmaSize = shortSmaSize;

            SmaOffset = thresholdOffset;

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
            SMAUpdateEventArgs smaUpdateEvArgs = new SMAUpdateEventArgs
            {
                longSMAValue = LongSMA.get(),
                shortSMAValue = ShortSMA.get()
            };

            EventHandler<SMAUpdateEventArgs> handler = SMAChangeOccurred;
            handler(this, smaUpdateEvArgs);
        }

        public void RunAlgorithm(double NewBtcValue, ref MoneyTracker BalanceTracker)
        {
            LongSMA.refresh(NewBtcValue);
            ShortSMA.refresh(NewBtcValue);

            InvokeSMAUpdateEvent();

            //Decide on trading action
            ComparisonResult liveComparisonResult = SMA.getSMAComparisonResult(ShortSMA, LongSMA, 0, SmaOffset);

            if (TransactionCooldown < TransactionCooldown_Duration)
            {
                // Cooldown
                TransactionCooldown++;
                InvokeTradeEvent(TradingDirection.Cooldown, (double)(TransactionCooldown_Duration - TransactionCooldown));
            }
            else if ((prevComparisonResult == ComparisonResult.less) && (liveComparisonResult == ComparisonResult.greater))
            {
                // Buy
                if (BalanceTracker.USD_Balance > 0)
                {
                    InvokeTradeEvent(TradingDirection.Buy, BalanceTracker.USD_Balance);

                    //Buy the whole USD balance
                    BalanceTracker.BuyInUSD(BalanceTracker.USD_Balance);
                }
                else
                {
                    InvokeTradeEvent(TradingDirection.Buy, 0.0f);
                }
            }
            else if ((prevComparisonResult == ComparisonResult.greater) && (liveComparisonResult == ComparisonResult.less))
            {
                // Sell
                if (BalanceTracker.BTC_Balance > 0)
                {
                    InvokeTradeEvent(TradingDirection.Sell, BalanceTracker.BTC_Balance);

                    //Sell the whole BTC Balance
                    BalanceTracker.SellInBTC(BalanceTracker.BTC_Balance);
                }
                else
                {
                    InvokeTradeEvent(TradingDirection.Sell, 0.0f);
                }
            }
            else
            {
                // No action
                InvokeTradeEvent(TradingDirection.NoAction, 0.0f);
            }

            if (liveComparisonResult != ComparisonResult.equal)
                prevComparisonResult = liveComparisonResult;
        }

        public void StartOfflineSimulation(List<double> ValuesList, double startBalance)
        {
            Thread OfflineSimulationThread = new Thread(() => OfflineSimulationMethod(ValuesList, startBalance));
            OfflineSimulationThread.Start();
        }

        private void OfflineSimulationMethod(List<double> ValuesList, double startBalance)
        {
            OfflineSimulationDoneEventArgs simulationValues = new OfflineSimulationDoneEventArgs();

            MoneyTracker OfflineBalanceTracker = new MoneyTracker(startBalance);

            ComparisonResult prevSimStepResult = ComparisonResult.unknown;

            SMA OfflineLongSMA = new SMA(LongSmaSize);
            SMA OfflineShortSMA = new SMA(ShortSmaSize);

            double OfflineTransactionCooldown = 0;
            double OfflineTransactionCooldown_Duration = LongSmaSize;

            foreach (double BTC_Value in ValuesList)
            {
                OfflineBalanceTracker.BTCValue = BTC_Value;

                OfflineLongSMA.refresh(BTC_Value);
                OfflineShortSMA.refresh(BTC_Value);

                TradingStep tradingStep = new TradingStep();

                //Decide on trading action
                ComparisonResult liveComparisonResult = SMA.getSMAComparisonResult(OfflineShortSMA, OfflineLongSMA, 0, SmaOffset);

                if (OfflineTransactionCooldown < OfflineTransactionCooldown_Duration)
                {
                    // Cooldown
                    OfflineTransactionCooldown++;
                    tradingStep.Action = TradingDirection.Cooldown;
                }
                else if ((prevSimStepResult == ComparisonResult.less) && (liveComparisonResult == ComparisonResult.greater))
                {
                    // Buy
                    tradingStep.Action = TradingDirection.Buy;
                    if (OfflineBalanceTracker.USD_Balance > 0)
                    {
                        //Buy the whole USD balance
                        OfflineBalanceTracker.BuyInUSD(OfflineBalanceTracker.USD_Balance);
                    }
                }
                else if ((prevSimStepResult == ComparisonResult.greater) && (liveComparisonResult == ComparisonResult.less))
                {
                    // Sell
                    tradingStep.Action = TradingDirection.Sell;
                    if (OfflineBalanceTracker.BTC_Balance > 0)
                    {
                        //Sell the whole BTC Balance
                        OfflineBalanceTracker.SellInBTC(OfflineBalanceTracker.BTC_Balance);
                    }
                }
                else
                {
                    // No action
                    tradingStep.Action = TradingDirection.NoAction;
                }

                if (liveComparisonResult != ComparisonResult.equal)
                    prevSimStepResult = liveComparisonResult;

                simulationValues.Trades.Add(tradingStep);
            }

            simulationValues.EndBalance = OfflineBalanceTracker.GetTrackerValue();
        }
    }
}
