using System;
using System.Collections.Generic;
using CoverGo.Shop.Services.Products.Dto;

namespace CoverGo.Shop.Services.Pricing.Models
{
    public class SimpleProductPricingRule
    {
        public List<ProductDto> Products { get; set; }

        public int Count { get; set; }

        public Dictionary<Guid, decimal> PriceDiscount { get; set; }

        public bool Free { get; set; }
    }
}