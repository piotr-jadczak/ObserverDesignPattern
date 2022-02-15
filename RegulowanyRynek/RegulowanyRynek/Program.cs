using System;
using RegulowanyRynek.Publisher.CentralBank;
using RegulowanyRynek.Publisher.Seller;
using RegulowanyRynek.Subscriber;
using System.Collections.Generic;

namespace RegulowanyRynek
{
    class Program
    {
        static void Main(string[] args)
        {
            //Set Up
            //Central Bank
            var centralBankProvider = new CentralBankProvider();
            CentralBank centralBank = new CentralBank(centralBankProvider, 2, 100, 100);

            //Seller
            List<SellProduct> offeredProducts = new List<SellProduct>();
            offeredProducts.Add(new SellProduct("chleb", 3, 30, 10));
            offeredProducts.Add(new SellProduct("maslo", 6, 10, 20));
            offeredProducts.Add(new SellProduct("ser", 10, 20, 15));

            Seller seller = new Seller(offeredProducts);

            //Buyer
            List<BuyProduct> wantedProducts = new List<BuyProduct>();
            wantedProducts.Add(new BuyProduct("ser", 15, 100));
            wantedProducts.Add(new BuyProduct("chleb", 20, 100));
            wantedProducts.Add(new BuyProduct("maslo", 8, 100));
            Buyer buyer = new Buyer(wantedProducts, 500);

            //Subscription
            BuyerCBSubscriber buyerCBSubscriber = new BuyerCBSubscriber();
            BuyerSellerSubscriber buyerSellerSubscriber = new BuyerSellerSubscriber();
            SellerCBSubscriber sellerCBSubscriber = new SellerCBSubscriber();

            buyerCBSubscriber.Subscribe(centralBank.dataProvider, buyer);
            buyerSellerSubscriber.Subscribe(seller.productsProvider, buyer, centralBank);
            sellerCBSubscriber.Subscribe(centralBank.dataProvider, seller);

            //Program
            for(int i = 0; i < 12; i++)
            {
                centralBank.showRaport();
                if(i != 0)
                {
                    centralBank.AdjustInflation();
                    seller.updateProductsMarginBasedOnSales();
                    seller.addStock(5);
                    buyer.addMoney(100);
                    buyer.addProductNeeds(5);
                }
                seller.openShop();

                seller.showInfo();
                buyer.showInfo();
                Console.ReadKey();
            }
        }
    }
}
