using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket
{
    public class PricingRule
    {

        public string Sku { get; set; }
        public int UnitPrice { get; set; }
        public int SpecialQuantity { get; set; }
        public int SpecialPrice { get; set; }

    }
}
