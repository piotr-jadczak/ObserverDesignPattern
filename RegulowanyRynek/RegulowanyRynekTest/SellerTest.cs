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
    public class SellerTest
    {
        [Test]
        public void Buyer_NotifyWhenSeller_FirstChangeProducts()
        {
            var seller = new Seller();
            var provider = new SellerProductsProvider();
            var dataProvider = new CentralBankProvider();
            var centralBank = new CentralBank(dataProvider, 0, 0, 0);

            seller.productsProvider = provider;

            var buyer = new Buyer();
            var subscriber = new BuyerSellerSubscriber(seller.productsProvider);
            subscriber.Subscribe(seller.productsProvider, buyer, centralBank);

            var product = new SellProduct("test", 10, 20, 30);
            var product2 = new SellProduct("test2", 100, 20, 30);
            List<SellProduct> products = new List<SellProduct>();
            products.Add(product);
            products.Add(product2);


            seller.productsProvider.SetMeasurements(products);

            Assert.That(subscriber.sellerProducts.products.Count, Is.EqualTo(2));
        }

        [Test]
        public void Buyer_NotifyWhenSeller_SecondChangeProducts()
        {
            var seller = new Seller();
            var provider = new SellerProductsProvider();
            var dataProvider = new CentralBankProvider();
            var centralBank = new CentralBank(dataProvider, 0, 0, 0);

            seller.productsProvider = provider;

            var buyer = new Buyer();
            var subscriber = new BuyerSellerSubscriber(seller.productsProvider);
            subscriber.Subscribe(seller.productsProvider, buyer, centralBank);

            var product = new SellProduct("test", 10, 20, 30);
            var product2 = new SellProduct("test2", 100, 20, 30);
            List<SellProduct> products = new List<SellProduct>();
            products.Add(product);
            products.Add(product2);


            seller.productsProvider.SetMeasurements(products);
            products.Remove(product);
            seller.productsProvider.SetMeasurements(products);

            Assert.That(subscriber.sellerProducts.products.Count, Is.EqualTo(1));
        }

        [Test]
        public void Seller_UpdateProductsPrices_ProductMarginChanged()
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

            seller.updateProductsMarginBasedOnSales();

            Assert.That(seller.dataProducts.products[1].margin, Is.EqualTo(11).Within(0.001));
        }

        [Test]
        public void Seller_UpdateProductsPrices_SecondTimeProductMarginChanged()
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

            seller.updateProductsMarginBasedOnSales();
            buyer.products[0].setQuantity(buyer.products[0].quantity + 10);
            seller.addStock(5);
            seller.openShop();
            seller.updateProductsMarginBasedOnSales();

            Assert.That(seller.dataProducts.products[0].margin, Is.EqualTo(10).Within(0.001));
        }

    }
}
