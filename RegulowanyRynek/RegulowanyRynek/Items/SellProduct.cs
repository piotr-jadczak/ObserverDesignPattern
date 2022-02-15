using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulowanyRynek
{
    public class SellProduct
    {
        public string name { get; set; }
        public double productionCost { get; set; }
        public int quantity { get; private set; }
        public double margin { get; private set; } //margin in percent

        public int startQuantity { get; set; }

        public void setQuantity(int quantity)
        {
            if (quantity < 0)
                throw new ArgumentOutOfRangeException("Quantity cannot be negative");
            this.quantity = quantity;
        }

        public void setMargin(double margin)
        {
            if (margin < 0)
                throw new ArgumentOutOfRangeException("Margin cannot be negative");
            this.margin = margin;
        }

        public SellProduct()
        {
        }

        public SellProduct(string name, double productionCost, int quantity, double margin)
        {
            this.name = name;
            this.productionCost = productionCost;
            setQuantity(quantity);
            setMargin(margin);
            startQuantity = quantity;
        }

        public double productPrice => productionCost * (1 + margin / 100);

        public int BuyAmount(int amountWanted, double availableMoney)
        {
            int currAmount = 0;
            while(currAmount < quantity && currAmount < amountWanted && (currAmount + 1) * productPrice < availableMoney)
            {
                currAmount++;
            }

            return currAmount;
        }

        public override string ToString()
        {
            return "Product[name=" + name + ", quantity= " + quantity + ", productionCost=" + productionCost + ", margin=" + margin + ", price" + productPrice + "]";
        }
    }
}
