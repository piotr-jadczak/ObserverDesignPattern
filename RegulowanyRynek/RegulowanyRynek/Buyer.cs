using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegulowanyRynek.Subscriber;

namespace RegulowanyRynek
{
    public class Buyer
    {
        public List<BuyProduct> products { get; set; }
        public double money { get; set; }

        public Buyer(List<BuyProduct> products, double money)
        {
            this.products = products;
            this.money = money;
        }

        public Buyer()
        {
        }

        //decrease willigness to buy for each product by 5 for every margin/inflation percent
        public void updateBuyWilligness(double change) 
        {
            int RATIO = 5;
            int willDecrease = (int)change * RATIO;
            foreach(BuyProduct product in products)
            {
                if (product.buyWilligness - willDecrease < 0)
                    product.setBuyWilligness(0);
                else
                {
                    if (product.buyWilligness - willDecrease > 100)
                        product.setBuyWilligness(100);
                    else
                        product.setBuyWilligness(100 - willDecrease);
                }

            }
        }

        public void addMoney(int amount)
        {
            this.money += amount;
        }

        public void addProductNeeds(int amount)
        {
            foreach (BuyProduct product in products)
                product.setQuantity(product.quantity + amount);
        }

        public void showInfo()
        {
            Console.WriteLine("Kupujacy");
            Console.WriteLine("Pieniadze: " + money);
            foreach (BuyProduct product in products)
                Console.WriteLine(product);
            Console.WriteLine();
        }
    }
}
