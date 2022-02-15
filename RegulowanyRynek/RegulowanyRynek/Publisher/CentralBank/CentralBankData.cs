using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulowanyRynek.Publisher.CentralBank
{
    public class CentralBankData
    {
        public double inflation { get; set; }

        public CentralBankData(double inflation)
        {
            this.inflation = inflation;
        }
    }
}
