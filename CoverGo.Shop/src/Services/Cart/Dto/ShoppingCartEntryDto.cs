using CoverGo.Shop.Services.Products.Dto;

namespace CoverGo.Shop.Services.Cart.Dto
{
    public class ShoppingCartEntryDto
    {
        public ProductDto Product { get; set; }
        
        public int Count { get; set; }
    }
}