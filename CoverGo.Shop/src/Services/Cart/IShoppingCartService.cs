using System.Threading;
using System.Threading.Tasks;
using CoverGo.Shop.Services.Cart.Dto;
using CoverGo.Shop.Services.Products.Dto;

namespace CoverGo.Shop.Services.Cart
{
    public interface IShoppingCartService
    {
        Task<ShoppingCartEntryDto[]> GetAsync(CancellationToken cancellationToken = default);

        Task AddAsync(ProductDto product, CancellationToken cancellationToken = default);

        Task<decimal> TotalAsync(CancellationToken cancellationToken = default);
    }
}