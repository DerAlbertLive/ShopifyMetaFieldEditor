using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using ShopifySharp;

namespace ShopifyMetaFieldEditor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IShopifyFactory _shopifyFactory;

        public IndexModel(IShopifyFactory shopifyFactory)
        {
            _shopifyFactory = shopifyFactory;
        }

        public async Task OnGet()
        {
            var service = _shopifyFactory.CreateProductService();

            var products = await service.ListAsync();
            Products = products;
        }

        public IEnumerable<Product> Products { get; set; }
    }
}
