using ShopifySharp;

namespace ShopifyMetaFieldEditor
{
    public interface IShopifyFactory
    {
        MetaFieldService CreateMetaFieldService();
        ProductService CreateProductService();
    }
}