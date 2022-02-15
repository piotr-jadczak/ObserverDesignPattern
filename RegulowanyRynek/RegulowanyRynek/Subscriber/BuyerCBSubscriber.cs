using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegulowanyRynek.Publisher.CentralBank;

namespace RegulowanyRynek.Subscriber
{
    public class BuyerCBSubscriber : IObserver<CentralBankData>
    {
        public CentralBankData data { get; set; }
        private IDisposable _unsubscriber;
        public Buyer _buyer { get; set; }

        public BuyerCBSubscriber()
        {
        }

        public BuyerCBSubscriber(IObservable<CentralBankData> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }

        public void Subscribe(IObservable<CentralBankData> provider, Buyer buyer)
        {
            if(_unsubscriber == null)
            {
                _unsubscriber = provider.Subscribe(this);
            }
            _buyer = buyer;
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
            if(_buyer != null && _buyer.products != null)
            {
                _buyer.updateBuyWilligness(data.inflation);
            }
        }
    }
}
