using System;
using System.Collections.Generic;
using NUnit.Framework;
using RegulowanyRynek;
using RegulowanyRynek.Publisher.CentralBank;
using RegulowanyRynek.Publisher.Seller;
using RegulowanyRynek.Subscriber;

namespace RegulowanyRynekTest
{
    [TestFixture]
    public class BuyerSellerSubscriberTest
    {
        [Test]
        public void BuyerSellerSub_BuyProductsFromSeller_SellerProductQuantityUpdates()
        {
            var seller = new Seller();
            var provider = new SellerProductsProvider();
            var dataProvider = new CentralBankProvider();
            var centralBank = new CentralBank(dataProvider, 0, 0, 0);

            seller.productsProvider = provider;

            var subscriber = new BuyerSellerSubscriber(seller.productsProvider);
            var product3 = new BuyProduct("test2", 10, 100);
            var product4 = new BuyProduct("test", 5, 10);
            List<BuyProduct> buyProducts = new List<BuyProduct>();
            buyProducts.Add(product3);
            buyProducts.Add(product4);
            var buyer = new Buyer(buyProducts, 100000);
            subscriber.Subscribe(seller.productsProvider, buyer, centralBank);

            var product = new SellProduct("test", 10, 20, 30);
            var product2 = new SellProduct("test2", 100, 20, 30);
            List<SellProduct> products = new List<SellProduct>();
            products.Add(product);
            products.Add(product2);
            seller.dataProducts = new SellerProducts(products);
            seller.productsProvider.SetMeasurements(products);

            Assert.That(seller.dataProducts.products[1].quantity, Is.EqualTo(10));
        }

        [Test]
        public void BuyerSellerSub_BuyProductsFromSeller_SellerProductQuantityUpdates2()
        {
            var seller = new Seller();
            var provider = new SellerProductsProvider();
            var dataProvider = new CentralBankProvider();
            var centralBank = new CentralBank(dataProvider, 0, 0, 0);

            seller.productsProvider = provider;

            var subscriber = new BuyerSellerSubscriber(seller.productsProvider);
            var product3 = new BuyProduct("test2", 10, 100);
            var product4 = new BuyProduct("test", 5, 100);
            List<BuyProduct> buyProducts = new List<BuyProduct>();
            buyProducts.Add(product3);
            buyProducts.Add(product4);
            var buyer = new Buyer(buyProducts, 100000);
            subscriber.Subscribe(seller.productsProvider, buyer, centralBank);

            var product = new SellProduct("test", 10, 20, 30);
            var product2 = new SellProduct("test2", 100, 20, 30);
            List<SellProduct> products = new List<SellProduct>();
            products.Add(product);
            products.Add(product2);
            seller.dataProducts = new SellerProducts(products);
            seller.productsProvider.SetMeasurements(products);

            Assert.That(seller.dataProducts.products[0].quantity, Is.EqualTo(15));
        }

        [Test]
        public void BuyerSellerSub_BuyProductsFromSeller_BuyerProductsQuantityUpdates()
        {
            var seller = new Seller();
            var provider = new SellerProductsProvider();
            var dataProvider = new CentralBankProvider();
            var centralBank = new CentralBank(dataProvider, 0, 0, 0);

            seller.productsProvider = provider;

            var subscriber = new BuyerSellerSubscriber(seller.productsProvider);
            var product3 = new BuyProduct("test2", 10, 100);
            var product4 = new BuyProduct("test", 5, 100);
            List<BuyProduct> buyProducts = new List<BuyProduct>();
            buyProducts.Add(product3);
            buyProducts.Add(product4);
            var buyer = new Buyer(buyProducts, 100000);
            subscriber.Subscribe(seller.productsProvider, buyer, centralBank);

            var product = new SellProduct("test", 10, 20, 30);
            var product2 = new SellProduct("test2", 100, 20, 30);
            List<SellProduct> products = new List<SellProduct>();
            products.Add(product);
            products.Add(product2);
            seller.dataProducts = new SellerProducts(products);
            seller.productsProvider.SetMeasurements(products);

            Assert.That(buyer.products[0].quantity, Is.EqualTo(0));
        }

        [Test]
        public void BuyerSellerSub_BuyProductsFromSeller_BuyerMoneyUpdates()
        {
            var seller = new Seller();
            var provider = new SellerProductsProvider();
            var dataProvider = new CentralBankProvider();
            var centralBank = new CentralBank(dataProvider, 0, 0, 0);

            seller.productsProvider = provider;

            var subscriber = new BuyerSellerSubscriber(seller.productsProvider);
            var product3 = new BuyProduct("test2", 10, 100);
            var product4 = new BuyProduct("test", 5, 100);
            List<BuyProduct> buyProducts = new List<BuyProduct>();
            buyProducts.Add(product3);
            buyProducts.Add(product4);
            var buyer = new Buyer(buyProducts, 10000);
            subscriber.Subscribe(seller.productsProvider, buyer, centralBank);

            var product = new SellProduct("test", 100, 5, 10);
            var product2 = new SellProduct("test2", 100, 5, 10);
            List<SellProduct> products = new List<SellProduct>();
            products.Add(product);
            products.Add(product2);
            seller.dataProducts = new SellerProducts(products);
            seller.productsProvider.SetMeasurements(products);

            Assert.That(buyer.money, Is.EqualTo(10000-1100));
        }

        [Test]
        public void BuyerSellerSub_BuyProductsFromSeller_CentralBankMarketTurnoverUpdates()
        {
            var seller = new Seller();
            var provider = new SellerProductsProvider();
            var dataProvider = new CentralBankProvider();
            var centralBank = new CentralBank(dataProvider, 0, 0, 0);

            seller.productsProvider = provider;

            var subscriber = new BuyerSellerSubscriber(seller.productsProvider);
            var product3 = new BuyProduct("test2", 10, 100);
            var product4 = new BuyProduct("test", 5, 100);
            List<BuyProduct> buyProducts = new List<BuyProduct>();
            buyProducts.Add(product3);
            buyProducts.Add(product4);
            var buyer = new Buyer(buyProducts, 10000);
            subscriber.Subscribe(seller.productsProvider, buyer, centralBank);

            var product = new SellProduct("test", 100, 5, 10);
            var product2 = new SellProduct("test2", 100, 5, 10);
            List<SellProduct> products = new List<SellProduct>();
            products.Add(product);
            products.Add(product2);
            seller.dataProducts = new SellerProducts(products);
            seller.productsProvider.SetMeasurements(products);

            Assert.That(centralBank.marketTurnover, Is.EqualTo(1100).Within(0.001));
        }
    }
}
