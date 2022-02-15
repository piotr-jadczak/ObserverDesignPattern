using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulowanyRynek
{
    public class BuyProduct
    {
        public string name { get; set; }
        public int quantity { get; private set; } //quantity he needs
        public int buyWilligness { get; private set; } //willigness to buy given product from 0 to 100
        // ex. quantity = 10, buyWilligness = 80, buys 8 products

        public void setQuantity(int quantity)
        {
            if (quantity < 0)
                throw new ArgumentOutOfRangeException("Quantity cannot be negative");
            this.quantity = quantity;
        }

        public void setBuyWilligness(int buyWilligness)
        {
            if (buyWilligness < 0 || buyWilligness > 100)
                throw new ArgumentOutOfRangeException("Buy willigness must be between 0 and 100.");
            this.buyWilligness = buyWilligness;

        }

        public BuyProduct()
        {
        }

        public BuyProduct(string name, int quantity, int buyWilligness)
        {
            this.name = name;
            this.quantity = quantity;
            setBuyWilligness(buyWilligness);
        }

        public int wants() => (int) (quantity * (double) buyWilligness / 100);

        public override string ToString()
        {
            return "Product[name=" + name + ", quantity= " + quantity + ", buyWilligness=" + buyWilligness +"]";
        }
    }
}
