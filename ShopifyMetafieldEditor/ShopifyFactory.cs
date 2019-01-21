using Microsoft.Extensions.Options;
using ShopifySharp;

namespace ShopifyMetaFieldEditor
{
    public class ShopifyFactory : IShopifyFactory
    {
        private readonly ShopifyAuthenticationOptions _options;

        public ShopifyFactory(IOptions<ShopifyAuthenticationOptions> optionsAccessor)
        {
            _options = optionsAccessor.Value;
        }

        public MetaFieldService CreateMetaFieldService()
        {
            return new MetaFieldService(_options.ShopUrl, _options.Password);
        }

        public ProductService CreateProductService()
        {
            return new ProductService(_options.ShopUrl, _options.Password);
        }
    }
}