using System.Threading.Tasks;
using CoverGo.Shop.Services.Cart;
using CoverGo.Shop.Services.Products.Dto;

namespace CoverGo.Shop.Tests.Services.Cart
{
    public static class ShoppingCartTestExtensions
    {
        public static async Task AddProducts(this IShoppingCartService service, ProductDto[] products)
        {
            foreach (var p in products)
            {
                await service.AddAsync(p);
            }
        }
    }
}