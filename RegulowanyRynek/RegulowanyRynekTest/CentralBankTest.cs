using NUnit.Framework;
using RegulowanyRynek;
using RegulowanyRynek.Publisher.CentralBank;
using RegulowanyRynek.Publisher.Seller;
using RegulowanyRynek.Subscriber;
using System.Collections.Generic;

namespace RegulowanyRynekTest
{
    [TestFixture]
    public class CentralBankTest
    {
        [Test]
        public void Seller_NotifyWhenCentralBank_FirstChangeInflation()
        {
            var dataProvider = new CentralBankProvider();
            var centralBank = new CentralBank(dataProvider, 0, 0, 0);

            var seller = new Seller();
            var sellersub = new SellerCBSubscriber();
            sellersub.Subscribe(centralBank.dataProvider, seller);

            centralBank.dataProvider.setMeasurements(2);

            Assert.That(sellersub.data.inflation, Is.EqualTo(2.0).Within(0.001));
        }

        [Test]
        public void Seller_NotifyWhenCentralBank_SecondChangeInflation()
        {
            var dataProvider = new CentralBankProvider();
            var centralBank = new CentralBank(dataProvider, 0, 0, 0);

            var seller = new Seller();
            var sellersub = new SellerCBSubscriber();
            sellersub.Subscribe(centralBank.dataProvider, seller);

            centralBank.dataProvider.setMeasurements(2);
            centralBank.dataProvider.setMeasurements(4);

            Assert.That(sellersub.data.inflation, Is.EqualTo(4.0).Within(0.001));
        }

        [Test]
        public void Buyer_NotifyWhenCentralBank_FirstChangeInflation()
        {
            var dataProvider = new CentralBankProvider();
            var centralBank = new CentralBank(dataProvider, 0, 0, 0);

            var buyer = new Buyer();
            var buyerSub = new BuyerCBSubscriber(centralBank.dataProvider);
            buyerSub.Subscribe(centralBank.dataProvider, buyer);

            centralBank.dataProvider.setMeasurements(2);

            Assert.That(buyerSub.data.inflation, Is.EqualTo(2.0).Within(0.001));
        }

        [Test]
        public void Buyer_NotifyWhenCentralBank_SecondChangeInflation()
        {
            var dataProvider = new CentralBankProvider();
            var centralBank = new CentralBank(dataProvider, 0, 0, 0);

            var buyer = new Buyer();
            var buyerSub = new BuyerCBSubscriber(centralBank.dataProvider);
            buyerSub.Subscribe(centralBank.dataProvider, buyer);

            centralBank.dataProvider.setMeasurements(2);
            centralBank.dataProvider.setMeasurements(4);

            Assert.That(buyerSub.data.inflation, Is.EqualTo(4.0).Within(0.001));
        }

        [Test]
        public void CentralBank_NotifySellerUpdatesProductionCostsForProducts_FirstChangeInflation()
        {
            var dataProvider = new CentralBankProvider();
            var centralBank = new CentralBank(dataProvider, 0, 0, 0);

            var seller = new Seller();
            var subscriber = new SellerCBSubscriber();
            

            var product = new SellProduct("test", 10, 20, 30);
            var product2 = new SellProduct("test2", 100, 20, 30);
            List<SellProduct> productsN = new List<SellProduct>();
            productsN.Add(product);
            productsN.Add(product2);
            seller.dataProducts = new SellerProducts(productsN);
            subscriber.Subscribe(centralBank.dataProvider, seller);

            centralBank.UpdateInflation(10);

            Assert.That(seller.dataProducts.products[1].productionCost, Is.EqualTo(110.0).Within(0.001));
        }

        [Test]
        public void CentralBank_NotifySellerUpdatesProductionCostsForProducts_SecondChangeInflation()
        {
            var dataProvider = new CentralBankProvider();
            var centralBank = new CentralBank(dataProvider, 0, 0, 0);

            var seller = new Seller();
            var subscriber = new SellerCBSubscriber();


            var product = new SellProduct("test", 10, 20, 30);
            var product2 = new SellProduct("test2", 100, 20, 30);
            List<SellProduct> productsN = new List<SellProduct>();
            productsN.Add(product);
            productsN.Add(product2);
            seller.dataProducts = new SellerProducts(productsN);
            subscriber.Subscribe(centralBank.dataProvider, seller);

            centralBank.UpdateInflation(10);
            centralBank.UpdateInflation(10);

            Assert.That(seller.dataProducts.products[1].productionCost, Is.EqualTo(121.0).Within(0.001));
        }

        [Test]
        public void CentralBank_NotifyBuyerUpdateBuyWillignessForProducts_FirstChangeInflation()
        {
            var dataProvider = new CentralBankProvider();
            var centralBank = new CentralBank(dataProvider, 0, 0, 0);

            var buyer = new Buyer();
            var buyerSub = new BuyerCBSubscriber();
            

            List<BuyProduct> productsN = new List<BuyProduct>();
            var product = new BuyProduct("test", 100, 100);
            var product2 = new BuyProduct("test2", 20, 100);
            productsN.Add(product);
            productsN.Add(product2);

            buyer.products = productsN;
            buyerSub.Subscribe(centralBank.dataProvider, buyer);

            centralBank.UpdateInflation(1);

            Assert.That(buyer.products[1].buyWilligness, Is.EqualTo(95));
        }

        [Test]
        public void CentralBank_Adjustinflation_IncreaseInflationWhenMarketTurnoverDecreases()
        {
            var dataProvider = new CentralBankProvider();
            var centralBank = new CentralBank(dataProvider, 0, 0, 0);
            centralBank.prevMarketTurnover = 1000;
            centralBank.marketTurnover = 900;
            centralBank.AdjustInflation();

            Assert.That(centralBank.inflation, Is.EqualTo(1).Within(0.0001));
        }

        [Test]
        public void CentralBank_Adjustinflation_IncreaseInflationSecondTimeWhenMarketTurnoverDecreases()
        {
            var dataProvider = new CentralBankProvider();
            var centralBank = new CentralBank(dataProvider, 0, 0, 0);
            centralBank.prevMarketTurnover = 1000;
            centralBank.marketTurnover = 900;
            centralBank.AdjustInflation();
            centralBank.marketTurnover = 800;
            centralBank.AdjustInflation();

            Assert.That(centralBank.inflation, Is.EqualTo(2).Within(0.0001));
        }

        [Test]
        public void CentralBank_Adjustinflation_DecreaseInflationWhenMarketTurnoverIncrease()
        {
            var dataProvider = new CentralBankProvider();
            var centralBank = new CentralBank(dataProvider, 0, 0, 0);
            centralBank.prevMarketTurnover = 1000;
            centralBank.marketTurnover = 900;
            centralBank.AdjustInflation();
            centralBank.marketTurnover = 1000;
            centralBank.AdjustInflation();

            Assert.That(centralBank.inflation, Is.EqualTo(0).Within(0.0001));
        }
    }
}
