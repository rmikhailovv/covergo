using System.Threading.Tasks;
using CoverGo.Shop.Services.Products.Dto;

namespace CoverGo.Shop.Services.Pricing
{
    public interface IProductPricingService
    {
        Task<decimal> TotalPriceAsync(ProductDto product, int count);
    }
}