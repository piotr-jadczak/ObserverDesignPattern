using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulowanyRynek.Publisher.Seller
{
    public class SellerProducts
    {
        public List<SellProduct> products;
        public SellerProducts(List<SellProduct> products)
        {
            this.products = products;
        }
    }
}
