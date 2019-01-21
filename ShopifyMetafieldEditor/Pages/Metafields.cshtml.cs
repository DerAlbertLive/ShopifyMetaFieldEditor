using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopifyMetaFieldEditor.Models;
using ShopifySharp;
using ShopifySharp.Filters;

namespace ShopifyMetaFieldEditor.Pages
{
    public class MetaFieldsModel : PageModel
    {
        private readonly IShopifyFactory _shopifyFactory;

        public MetaFieldsModel(IShopifyFactory shopifyFactory)
        {
            _shopifyFactory = shopifyFactory;
        }

        public async Task<ActionResult> OnGet(long productId)
        {
            var productService = _shopifyFactory.CreateProductService();
            var metafieldService = _shopifyFactory.CreateMetaFieldService();
            var product = await productService.GetAsync(productId,"title,id");
            Product = product;
            MetaFields =  await metafieldService.ListAsync(productId, "products", new MetaFieldFilter()
            {
                Namespace = "rls"
            });
            if (CreateModel == null)
            {
                CreateModel = new MetaFieldModel
                {
                    Key = "subtitle",
                    Namespace = "rls"
                };
            }

            return Page();
        }

        public async Task<ActionResult> OnPostUpdate(long productId, MetaFieldModel metaField)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                var metafieldService = _shopifyFactory.CreateMetaFieldService();
                var field = await metafieldService.UpdateAsync(metaField.Id, new MetaField()
                {
                    Namespace = metaField.Namespace,
                    Key = metaField.Key,
                    Value = metaField.Value
                });
                return RedirectToPage("Metafields", new {productId});
            }
            return await OnGet(productId);
        }

        public async Task<ActionResult> OnPostCreate(long productId)
        {
            if (ModelState.IsValid)
            {
                var metafieldService = _shopifyFactory.CreateMetaFieldService();
                await metafieldService.CreateAsync(new MetaField()
                {
                    Key = CreateModel.Key,
                    Namespace = CreateModel.Namespace,
                    Value = CreateModel.Value,
                    ValueType = "string",
                }, productId, "products");
                return RedirectToPage("Metafields", new {productId});
            }
            return await OnGet(productId);
        }

        public Product Product { get; set; }

        public IEnumerable<MetaField> MetaFields { get; set; }

        [BindProperty]
        public MetaFieldModel CreateModel { get; set; }
    }
}