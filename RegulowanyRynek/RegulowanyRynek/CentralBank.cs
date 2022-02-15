using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulowanyRynek.Publisher.CentralBank
{
    public class CentralBank
    {
        public double inflation { get; set; }
        public double marketTurnover { get; set; }
        public double prevMarketTurnover { get; set; }
        public CentralBankProvider dataProvider { get; set; }

        public CentralBank()
        {
        }

        public CentralBank(CentralBankProvider dataProvider, double inflation, double marketTurnover, double prevMarketTurnover)
        {
            this.dataProvider = dataProvider;
            this.inflation = inflation;
            this.marketTurnover = marketTurnover;
            this.prevMarketTurnover = prevMarketTurnover;
        }

        public void UpdateInflation(double inflation)
        {
            this.inflation = inflation;
            dataProvider.setMeasurements(inflation);
        }

        public void AdjustInflation()
        {
            if (prevMarketTurnover > marketTurnover)
                UpdateInflation(inflation + 1);
            else
                UpdateInflation(inflation - 1);

            prevMarketTurnover = marketTurnover;
            marketTurnover = 0;
        }

        public void showRaport()
        {
            Console.WriteLine("Inflacja w poprzednim miesiacu wyniosla " + inflation);
            Console.WriteLine("Obrot wyniosl " + marketTurnover);
            Console.WriteLine();
        }
    }
}
