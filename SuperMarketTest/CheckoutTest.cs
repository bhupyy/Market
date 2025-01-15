using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using SuperMarket;

namespace SuperMarketTest
{

        [TestFixture]
        public class CheckoutTests
        {
            private List<PricingRule> PricingRules = new();


            [SetUp]
            public void SetUp()
            {
                PricingRules = new List<PricingRule>
                 {
                     new PricingRule { Sku = "A", UnitPrice = 50, SpecialQuantity = 3, SpecialPrice = 130 },
                     new PricingRule { Sku = "B", UnitPrice = 30, SpecialQuantity = 2, SpecialPrice = 45 },
                     new PricingRule { Sku = "C", UnitPrice = 20, SpecialQuantity = 0, SpecialPrice = 0 },
                     new PricingRule { Sku = "D", UnitPrice = 15, SpecialQuantity = 0, SpecialPrice = 0 }
                 };
            }


            [Test]
            public void InvalidItem_ThrowsException()
            {
                var checkout = new Checkout(PricingRules);
                Assert.Throws<ArgumentException>(() => checkout.Scan("X"));
            }

            [Test]
            public void NoPricingRuless_ThrowsException()
            {
                Assert.Throws<ArgumentException>(() => new Checkout(new List<PricingRule>()));
            }

            [Test]
            public void SingleItem_ReturnsCorrectPrice()
            {
                var checkout = new Checkout(PricingRules);
                checkout.Scan("A");
                Assert.That(checkout.GetTotalPrice(), Is.EqualTo(50));
            }

            [Test]
            public void MultipleItemsWithoutSpecial_ReturnsCorrectPrice()
            {
                var checkout = new Checkout(PricingRules);
                checkout.Scan("A");
                checkout.Scan("C");
                Assert.That(checkout.GetTotalPrice(), Is.EqualTo(70));
            }

            [Test]
            public void SpecialPrice_IsAppliedCorrectly()
            {
                var checkout = new Checkout(PricingRules);
                checkout.Scan("A");
                checkout.Scan("A");
                checkout.Scan("A");
                Assert.That(checkout.GetTotalPrice(), Is.EqualTo(130));
            }

            [Test]
            public void MixedItems_WithSpecials_ReturnsCorrectPrice()
            {
                var checkout = new Checkout(PricingRules);
                checkout.Scan("A");
                checkout.Scan("A");
                checkout.Scan("A");
                checkout.Scan("B");
                checkout.Scan("B");
                checkout.Scan("C");
                Assert.That(checkout.GetTotalPrice(), Is.EqualTo(195));
            }


        }


}
