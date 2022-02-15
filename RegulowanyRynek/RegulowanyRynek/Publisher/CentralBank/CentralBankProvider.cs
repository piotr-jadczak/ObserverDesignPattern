using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulowanyRynek.Publisher.CentralBank
{
    public class CentralBankProvider : IObservable<CentralBankData>
    {
        List<IObserver<CentralBankData>> observers;

        public CentralBankProvider()
        {
            observers = new List<IObserver<CentralBankData>>();
        }

        public IDisposable Subscribe(IObserver<CentralBankData> observer)
        {
            if (!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new UnsubscriberCB(observers, observer);
        }

        private void MeasurmentsChanged(double inflation)
        {
            foreach(var obs in observers)
            {
                obs.OnNext(new CentralBankData(inflation));
            }
        }

        public void setMeasurements(double inflation)
        {
            MeasurmentsChanged(inflation);
        }
    }
}
