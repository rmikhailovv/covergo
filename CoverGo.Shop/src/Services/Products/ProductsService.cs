using System;
using System.Threading;
using System.Threading.Tasks;
using CoverGo.Shop.Services.Products.Dto;

namespace CoverGo.Shop.Services.Products
{
    public class ProductsService : IProductsService
    {

        public static readonly ProductDto TShirt = new ProductDto
        {
            Id = new Guid("5C9FA41D-29CB-4DDD-B4BA-BDEEECDD148F"),
            Name = "T-Shirt",
            Price = 10.00m
        };

        public static readonly ProductDto Jeans = new ProductDto
        {
            Id = new Guid("5C9FA41D-29CB-4DDD-B4BA-BDEEECDD148E"),
            Name = "Jeans",
            Price = 20.00m
        };
        
        private static readonly ProductDto[] ProductsStore = new[]
        {
            Jeans,
            TShirt
        };
        
        public Task<ProductDto[]> GetAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(ProductsStore);
        }
    }
}