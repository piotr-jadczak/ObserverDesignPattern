using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulowanyRynek.Publisher.Seller
{
    public class SellerProductsProvider : IObservable<SellerProducts>
    {
        List<IObserver<SellerProducts>> observers;

        public SellerProductsProvider()
        {
            observers = new List<IObserver<SellerProducts>>();
        }

        public IDisposable Subscribe(IObserver<SellerProducts> observer)
        {
            if(!observers.Contains(observer))
            {
                observers.Add(observer);
            }
            return new UnsubscriberSell(observers, observer);
        }

        public void MeasurementsChanged(List<SellProduct> products)
        {
            foreach(var obs in observers)
            {
                obs.OnNext(new SellerProducts(products));
            }
        }

        public void SetMeasurements(List<SellProduct> products)
        {
            MeasurementsChanged(products);
        }
    }
}
