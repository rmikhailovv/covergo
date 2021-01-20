using System;

namespace CoverGo.Shop.Services.Products.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public enum ProductCategory
    {
        TShirt,
        Jeans
    }
}