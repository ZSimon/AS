using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Orders;
using Nop.Core.Events;
using Nop.Services.Catalog;
using Nop.Services.Discounts;
using Nop.Services.Events;

namespace Nop.Plugin.Misc.ProductEditor.Extensions
{
    public class PriceCalculation : PriceCalculationService, IConsumer<EntityUpdated<Product>>
    {
        private readonly IProductService _productService;
        private readonly ICacheManager _cacheManager;
        private readonly IWorkContext _workContext;

        public PriceCalculation(IWorkContext workContext, IStoreContext storeContext, IDiscountService discountService, ICategoryService categoryService, 
            IManufacturerService manufacturerService, IProductAttributeParser productAttributeParser, IProductService productService, 
            ICacheManager cacheManager, ShoppingCartSettings shoppingCartSettings, CatalogSettings catalogSettings) : 
            base(workContext, storeContext, discountService, categoryService, manufacturerService, productAttributeParser, productService, 
                cacheManager, shoppingCartSettings, catalogSettings)
        {
            _productService = productService;
            _cacheManager = cacheManager;
            _workContext = workContext;
        }

        public override decimal GetProductAttributeValuePriceAdjustment(ProductAttributeValue value)
        {
            //cene za attribute value izracunaj relativno glede na preselected value - le ta je dodan k osnovni ceni!
            //return Convert.ToDecimal(100);
            return value.IsPreSelected ? 0 : base.GetProductAttributeValuePriceAdjustment(value);
        }

        public void HandleEvent(EntityUpdated<Product> eventMessage)
        {
            var i = 1;

            //_cacheManager.RemoveByPattern(String.Format("Nop.totals.productprice-{0}", eventMessage.Entity.Id));
            _cacheManager.RemoveByPattern($"Nop.totals.productprice-{eventMessage.Entity.Id}");
        }
    }
}
