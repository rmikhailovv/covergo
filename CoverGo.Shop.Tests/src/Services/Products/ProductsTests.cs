using System.Threading.Tasks;
using CoverGo.Shop.Services.Products;
using FluentAssertions;
using NUnit.Framework;

namespace CoverGo.Shop.Tests.Services.Products
{
    public class ProductsTests
    {
        private ProductsService _service;
        
        [OneTimeSetUp]
        public async Task SetUp()
        {
            _service = new ProductsService();
        }
        
        [Test]
        public async Task Products_Get_Success()
        {
            var products = await _service.GetAsync();
            products.Should().NotBeNull();
            products.Should().NotBeEmpty();
            products.Length.Should().Be(2);
        }
    }
}