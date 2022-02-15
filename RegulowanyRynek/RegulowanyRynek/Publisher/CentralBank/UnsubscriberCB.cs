using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulowanyRynek.Publisher.CentralBank
{
    internal class UnsubscriberCB : IDisposable
    {
        private List<IObserver<CentralBankData>> lstObservers;
        private IObserver<CentralBankData> observer;

        internal UnsubscriberCB(List<IObserver<CentralBankData>> observersCollection, IObserver<CentralBankData> observer)
        {
            this.lstObservers = observersCollection;
            this.observer = observer;
        }

        public void Dispose()
        {
            if(this.observer != null)
            {
                lstObservers.Remove(this.observer);
            }
        }
    }
}
