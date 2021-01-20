using System.Linq;
using System.Threading.Tasks;
using CoverGo.Shop.Services.Cart;
using CoverGo.Shop.Services.Cart.Dto;
using CoverGo.Shop.Services.Pricing;
using CoverGo.Shop.Services.Products;
using CoverGo.Shop.Services.Products.Dto;
using CoverGo.Shop.Tests.Services.Products;
using FluentAssertions;
using NUnit.Framework;

namespace CoverGo.Shop.Tests.Services.Cart
{
    [TestFixture]
    public class ShoppingCartTests
    {
        private ShoppingCartService _shoppingCartService;
        private ProductsService _productsService;
        
        [OneTimeSetUp]
        public async Task SetUp()
        {
            _shoppingCartService = new ShoppingCartService(new ProductPricingService());
            _productsService = new ProductsService();
        }

        [Test]
        [Order(0)]
        public async Task ShoppingCart_Empty_WhenInitialized()
        {
            var entries = await _shoppingCartService.GetAsync();
            entries.Should().BeEmpty();
        }

        [Test]
        [Order(1)]
        public async Task Shopping_Cart_Add_Success()
        {
            var products = await _productsService.GetAsync();
            await _shoppingCartService.AddProducts(products);
            
            var entries = await _shoppingCartService.GetAsync();
            entries.Should().NotBeEmpty();
            entries.Length.Should().Be(2);

            ShoppingCartAssertInternal(entries[0], products[0], 1);
            ShoppingCartAssertInternal(entries[1], products[1], 1);
        }

        [Test]
        [Order(2)]
        public async Task Shopping_Cart_ProductCount_Increase_Success()
        {
            var products = await _productsService.GetAsync();
            var product = products.First();

            await _shoppingCartService.AddAsync(product);
            var entries = await _shoppingCartService.GetAsync();
            var entry = entries.First(e => e.Product.Id == product.Id);
            ShoppingCartAssertInternal(entry, product, 2);
        }

        [Test]
        public async Task Shopping_Cart_TotalPrice_Get_Success()
        {
            var products = await _productsService.GetAsync();
            products.Should().NotBeNullOrEmpty();

            var total = products.Sum(p => p.Price);

            var shoppingCartService = new ShoppingCartService(new ProductPricingService());
            await shoppingCartService.AddProducts(products);

            var totalPrice = await shoppingCartService.TotalAsync();
            totalPrice.Should().Be(total);
        }

        [Test]
        public async Task ShoppingCart_MultipleProducts_Total_Success()
        {
            var products = await _productsService.GetAsync();
            var product = products.First();

            var cartService = new ShoppingCartService(new ProductPricingService());
            await cartService.AddAsync(product);
            await cartService.AddAsync(product);

            var total = await cartService.TotalAsync();
            total.Should().Be(product.Price * 2);
        }

        [Test]
        [TestCase(1, 20.00)]
        [TestCase(3, 40.00)]
        [TestCase(5, 80.00)]
        [TestCase(6, 80.00)]
        public async Task ShoppingCart_Discount(int count, decimal expectedPrice)
        {
            var products = await _productsService.GetAsync();
            var jeans = products.First(x => x.Id == ProductsService.Jeans.Id);

            var shoppingCartService = new ShoppingCartService(new ProductPricingService());

            for (var i = 0; i < count; i++)
            {
                await shoppingCartService.AddAsync(jeans);
            }
            var actualPrice = await shoppingCartService.TotalAsync();
            actualPrice.Should().Be(expectedPrice);
        }

        private static void ShoppingCartAssertInternal(ShoppingCartEntryDto entry, ProductDto product, int count)
        {
            entry.Should().NotBeNull();
            entry.Product.Should().BeEquivalentTo(product);
            entry.Count.Should().Be(count);
        }
    }
}