using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegulowanyRynek.Publisher.Seller;
using RegulowanyRynek.Subscriber;
using RegulowanyRynek.Publisher.CentralBank;

namespace RegulowanyRynek
{
    public class Seller
    {
        public SellerProducts dataProducts { get; set; }
        public SellerProductsProvider productsProvider { get; set; }

        public Seller()
        {
        }

        public Seller(List<SellProduct> products)
        {
            dataProducts = new SellerProducts(products);
            productsProvider = new SellerProductsProvider();
            productsProvider.SetMeasurements(products);
        }

        public void updateProductionCostByInflation(double inflation)
        {
            foreach (SellProduct product in dataProducts.products)
            {
                product.productionCost *= (1 + inflation / 100);
            }
        }

        public void updateProductsMarginBasedOnSales()
        {
            int RATIO = 1;
            foreach(SellProduct product in dataProducts.products)
            {
                if((double) product.startQuantity * 0.5 > (double) product.quantity) {
                    product.setMargin(product.margin + RATIO);
                }
                else
                {
                    product.setMargin(product.margin - RATIO);
                }
                product.startQuantity = product.quantity;
            }
        }

        public void addStock(int stock)
        {
            foreach (SellProduct product in dataProducts.products)
            {
                product.setQuantity(product.quantity + stock);
            }
        }

        public void openShop()
        {
            productsProvider.SetMeasurements(dataProducts.products);
        }

        public void showInfo()
        {
            Console.WriteLine("Sprzedjacy");
            foreach (SellProduct product in dataProducts.products)
                Console.WriteLine(product);
            Console.WriteLine();
        }
    }
}
