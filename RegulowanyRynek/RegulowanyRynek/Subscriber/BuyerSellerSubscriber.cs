using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegulowanyRynek.Publisher.Seller;
using RegulowanyRynek.Publisher.CentralBank;

namespace RegulowanyRynek.Subscriber
{
    public class BuyerSellerSubscriber : IObserver<SellerProducts>
    {
        public SellerProducts sellerProducts { get; set; }
        private IDisposable _unsubscriber;
        public Buyer _buyer { get; set; }
        public CentralBank _centralBank { get; set; }

        public BuyerSellerSubscriber()
        {
        }

        public BuyerSellerSubscriber(IObservable<SellerProducts> provider)
        {
            _unsubscriber = provider.Subscribe(this);
        }

        public void Subscribe(IObservable<SellerProducts> provider, Buyer buyer, CentralBank centralBank)
        {
            if(_unsubscriber == null)
            {
                _unsubscriber = provider.Subscribe(this);
            }
            _buyer = buyer;
            _centralBank = centralBank;
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

        public void OnNext(SellerProducts sellerProducts)
        {
            this.sellerProducts = sellerProducts;
            if(_buyer != null && _buyer.products != null && sellerProducts != null && _centralBank != null)
            {
                buyProductsFromSeller();
            }
        }

        public void buyProductsFromSeller()
        {
            foreach(BuyProduct buyProduct in _buyer.products)
            {
                foreach(SellProduct sellProduct in sellerProducts.products)
                {
                    if(buyProduct.name.Equals(sellProduct.name))
                    {
                        int quantityBought = sellProduct.BuyAmount(buyProduct.wants(), _buyer.money);
                        _buyer.money -= quantityBought * sellProduct.productPrice;
                        buyProduct.setQuantity(buyProduct.quantity - quantityBought);
                        sellProduct.setQuantity(sellProduct.quantity - quantityBought);
                        _centralBank.marketTurnover += quantityBought * sellProduct.productPrice;
                    }
                }
            }
        }
    }
}
