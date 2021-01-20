using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoverGo.Shop.Services.Cart.Dto;
using CoverGo.Shop.Services.Pricing;
using CoverGo.Shop.Services.Products.Dto;

namespace CoverGo.Shop.Services.Cart
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IProductPricingService _productPricingService;

        public ShoppingCartService(IProductPricingService productPricingService)
        {
            _productPricingService = productPricingService;
        }
        
        private readonly List<ShoppingCartEntryDto> _shoppingCartStorage = new List<ShoppingCartEntryDto>();
        public Task<ShoppingCartEntryDto[]> GetAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_shoppingCartStorage.ToArray());
        }

        public async Task<decimal> TotalAsync(CancellationToken cancellationToken = default)
        {
            var total = 0.00m;
            foreach (var entry in _shoppingCartStorage)
            {
                total += await _productPricingService.TotalPriceAsync(entry.Product, entry.Count);
            }

            return total;
        }

        public Task AddAsync(ProductDto product, CancellationToken cancellationToken = default)
        {
            var record = _shoppingCartStorage.FirstOrDefault(x => x.Product.Id == product.Id);
            if (record != null)
            {
                record.Count++;
            }
            else
            {
                _shoppingCartStorage.Add(new ShoppingCartEntryDto
                {
                    Product = product,
                    Count = 1
                });
            }
            return Task.CompletedTask;
        }
    }
}