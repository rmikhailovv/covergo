using System.Threading;
using System.Threading.Tasks;
using CoverGo.Shop.Services.Products.Dto;

namespace CoverGo.Shop.Services.Products
{
    public interface IProductsService
    {
        Task<ProductDto[]> GetAsync(CancellationToken cancellationToken = default);
    }
}