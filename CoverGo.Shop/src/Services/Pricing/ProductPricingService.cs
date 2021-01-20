using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoverGo.Shop.Services.Pricing.Models;
using CoverGo.Shop.Services.Products;
using CoverGo.Shop.Services.Products.Dto;

namespace CoverGo.Shop.Services.Pricing
{
    public class ProductPricingService : IProductPricingService
    {
        private static readonly List<SimpleProductPricingRule> PricingRules = new List<SimpleProductPricingRule>()
        {
            new SimpleProductPricingRule
            {
                Products = new List<ProductDto>{ ProductsService.Jeans },
                Count = 3,
                Free = true
            },
            new SimpleProductPricingRule
            {
                Products = new List<ProductDto>{ProductsService.Jeans, ProductsService.TShirt},
                Count = 2,
                PriceDiscount = new Dictionary<Guid, decimal>
                {
                    {ProductsService.Jeans.Id, 10.00m },
                    {ProductsService.TShirt.Id, 5.00m }
                }
            }
        };
        
        public Task<decimal> TotalPriceAsync(ProductDto product, int count)
        {
            var rules = PricingRules.Where(r => r.Products.All(p => p.Id == product.Id)).ToArray();
            if (!rules.Any()) return Task.FromResult(product.Price * count);

            var prices = new List<decimal>();
            
            foreach (var rule in rules)
            {
                var total = 0.00m;
                for (var i = 1; i <= count; i++)
                {
                    if (i % rule.Count == 0)
                    {
                        if (rule.Free)
                        {
                            continue;
                        }
                        else
                        {
                            total += rule.PriceDiscount[product.Id];
                            continue;
                        }
                    }
                    total += product.Price;
                }
                prices.Add(total);
            }
            return Task.FromResult(prices.Min());
        }
    }
}