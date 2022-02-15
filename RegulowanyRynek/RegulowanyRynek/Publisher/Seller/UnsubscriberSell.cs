using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulowanyRynek.Publisher.Seller
{
    public class UnsubscriberSell : IDisposable
    {
        private List<IObserver<SellerProducts>> lstObservers;
        private IObserver<SellerProducts> observer;

        internal UnsubscriberSell(List<IObserver<SellerProducts>> observersCollection, IObserver<SellerProducts> observer)
        {
            this.lstObservers = observersCollection;
            this.observer = observer;
        }

        public void Dispose()
        {
            if (this.observer != null)
            {
                lstObservers.Remove(this.observer);
            }
        }
    }
}
