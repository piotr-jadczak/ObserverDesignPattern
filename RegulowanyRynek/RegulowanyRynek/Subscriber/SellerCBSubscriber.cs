using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegulowanyRynek.Publisher.CentralBank;
using RegulowanyRynek.Publisher.Seller;

namespace RegulowanyRynek.Subscriber
{
    public class SellerCBSubscriber : IObserver<CentralBankData>
    {
        public CentralBankData data { get; set; }
        private IDisposable _unsubscriber;
        public Seller seller { get; set; }

        public SellerCBSubscriber()
        {
        }

        public SellerCBSubscriber(IObservable<CentralBankData> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }

        public void Subscribe(IObservable<CentralBankData> provider, Seller seller)
        {
            if(_unsubscriber == null)
            {
                _unsubscriber = provider.Subscribe(this);
            }
            this.seller = seller;
        }

        public void Unsubscribe()
        {
            _unsubscriber.Dispose();
        }

        public void OnCompleted()
        {
        }

        public void OnError(Exception error)
        {
        }

        public void OnNext(CentralBankData data)
        {
            this.data = data;
            if(seller != null && seller.dataProducts != null)
            {
                seller.updateProductionCostByInflation(data.inflation);
            }
        }

    }
}
