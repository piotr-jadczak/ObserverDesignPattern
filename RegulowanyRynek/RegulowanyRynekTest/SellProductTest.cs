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
    public class SellProductTest
    {
        [Test]
        public void SellProduct_BuyAmount_TryBuyMoreThanItIsAvailbale()
        {
            var sellProdcut = new SellProduct("test", 10, 10, 10);

            Assert.That(sellProdcut.BuyAmount(100, 1000), Is.EqualTo(10));
        }

        [Test]
        public void SellProduct_BuyAmount_TryBuyWithNotEnoughMoney()
        {
            var sellProdcut = new SellProduct("test", 10, 10, 10);

            Assert.That(sellProdcut.BuyAmount(100, 90), Is.EqualTo(8));
        }

        [Test]
        public void SellProduct_BuyAmount_TryBuyProducts()
        {
            var sellProdcut = new SellProduct("test", 10, 10, 10);

            Assert.That(sellProdcut.BuyAmount(5, 100), Is.EqualTo(5));
        }
    }
}
