using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace BTC_Swingtrade_Simulator
{
    public class BTCEventArgs : EventArgs
    {
        public DateTime timeUpdated { set; get; }
        public double BTCValue { set; get; }
        public double BTCValuePrev { set; get; }
        public double BTCChange { set; get; }
        public double BTCPercentageChange { set; get; }
    }

    public enum BTCRetrievalType
    {
        Online,
        Offline
    }

    class ReadBTC_Value
    {
        //Public Attributes
        public event EventHandler<BTCEventArgs> NewBTCValueAvailable;
        public BTCEventArgs currentEventArgs;

        public BTCRetrievalType ReaderType { private set; get; }

        public int OfflineBTCArrayIndex { private set; get; }

        //Private Attributes
        private Timer OneSecTimer = new Timer();

        private bool FirstCycle = true;
        private double PreviousBTCValue = 0.0f;

        private double[] OfflineBTCArray;
        private int nOfflineBTCValues;

        public ReadBTC_Value()
        {
            OneSecTimer.Interval = 1000; //ms
            OneSecTimer.Tick += OneSecTimer_Tick;
            ReaderType = BTCRetrievalType.Online;
        }
        public ReadBTC_Value(string filePath, int TimerInterval)
        {
            //Running from File
            OneSecTimer.Interval = TimerInterval; //ms
            OneSecTimer.Tick += OneSecTimer_Tick;
            ReaderType = BTCRetrievalType.Offline;

            OfflineBTCArrayIndex = 0;

            string[] historyValuesArray = File.ReadAllLines(filePath);
            OfflineBTCArray = new double[historyValuesArray.Length];

            for(int i = 0; i < (historyValuesArray.Length - 1); i++)
            {
                if(double.TryParse(historyValuesArray[i], out OfflineBTCArray[i]) == true)
                    nOfflineBTCValues++;
            }
        }

        public void Start()
        {
            readBTCValue();
            FirstCycle = true;
            OneSecTimer.Start();
        }
        public void Stop()
        {
            OneSecTimer.Stop();
        }
        private void OneSecTimer_Tick(object sender, EventArgs e)
        {
            readBTCValue();
        }
        protected virtual void NewBTC_Value_Availalble()
        {
            EventHandler<BTCEventArgs> handler = NewBTCValueAvailable;
            handler(this, currentEventArgs);
        }

        public void readBTCValue()
        {
            if (ReaderType == BTCRetrievalType.Online)
            {
                currentEventArgs = new BTCEventArgs();
                dynamic BTCJson;
                try
                {
                    WebClient wc = new WebClient();
                    BTCJson = JObject.Parse(wc.DownloadString("https://api.coindesk.com/v1/bpi/currentprice.json"));
                    currentEventArgs.BTCValue = (double)(BTCJson.bpi.USD.rate_float);

                    //Reformat DateTime string
                    //string RoundtripDateTime = ((string)BTCJson.time.updatedISO).Split('+')[0] + "Z";
                    currentEventArgs.timeUpdated = DateTime.Now;//DateTime.Parse(RoundtripDateTime, null, System.Globalization.DateTimeStyles.RoundtripKind);
                }
                catch (WebException e)
                {
                    MessageBox.Show(e.ToString(), "Web Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }

                if (currentEventArgs.BTCValue != PreviousBTCValue)
                {
                    if (FirstCycle)
                        currentEventArgs.BTCChange = 0.0f;
                    else
                        currentEventArgs.BTCChange = currentEventArgs.BTCValue - PreviousBTCValue;
                    FirstCycle = false;

                    currentEventArgs.BTCValuePrev = PreviousBTCValue;
                    currentEventArgs.BTCPercentageChange = currentEventArgs.BTCValue / currentEventArgs.BTCValuePrev - 1;

                    PreviousBTCValue = currentEventArgs.BTCValue;

                    NewBTC_Value_Availalble();
                }
            }
            else
            {
                if (OfflineBTCArrayIndex == nOfflineBTCValues)
                {
                    Stop();
                    MessageBox.Show("Done!", "Offline Simulation Status");
                }
                else
                {
                    currentEventArgs = new BTCEventArgs();
                    currentEventArgs.BTCValue = OfflineBTCArray[OfflineBTCArrayIndex];
                    OfflineBTCArrayIndex++;

                    if (currentEventArgs.BTCValue != PreviousBTCValue)
                    {
                        if (FirstCycle)
                            currentEventArgs.BTCChange = 0.0f;
                        else
                            currentEventArgs.BTCChange = currentEventArgs.BTCValue - PreviousBTCValue;
                        FirstCycle = false;

                        currentEventArgs.BTCValuePrev = PreviousBTCValue;
                        currentEventArgs.BTCPercentageChange = currentEventArgs.BTCValue / currentEventArgs.BTCValuePrev - 1;

                        PreviousBTCValue = currentEventArgs.BTCValue;

                        NewBTC_Value_Availalble();
                    }
                }
            }
        }
    }
}
