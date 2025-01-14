using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket
{
    public class Checkout : ICheckout
    {
        // Stores the rules for pricing
        private readonly List<PricingRule> pricingRules;

        // Track scanned item counts
        private readonly Dictionary<string, int> scannedItems;



        public Checkout(List<PricingRule> rules)
        {
            if (rules == null || rules.Count == 0)
            {
                throw new ArgumentException("Pricing rules must be provided.");
            }

            pricingRules = rules;
            scannedItems = new Dictionary<string, int>();
        }



        public void Scan(string item)
        {
            // Null handling for invalid item
            var rule = pricingRules.Find(r => r.Sku == item);
            if (rule == null)
            {
                throw new ArgumentException($"Item '{item}' is not recognized.");
            }

            if (scannedItems.ContainsKey(item))
            {


                scannedItems[item]++;
            }
            else
            {
                scannedItems[item] = 1;
            }
        }

        public int GetTotalPrice()
        {
            int total = 0;

            foreach (var entry in scannedItems)
            {
                var sku = entry.Key;
                var quantity = entry.Value;

                // To find rule for the current SKU
                var rule = pricingRules.Find(r => r.Sku == sku);



                if (rule.SpecialQuantity > 0 || rule.SpecialPrice > 0)
                {
                    // Special pricing 
                    var specialSets = quantity / rule.SpecialQuantity;

                    total += (specialSets * rule.SpecialPrice);

                }
                else
                {
                    // Regular pricing
                    total += quantity * rule.UnitPrice;
                }
            }

            return total;
        }
    }
}
