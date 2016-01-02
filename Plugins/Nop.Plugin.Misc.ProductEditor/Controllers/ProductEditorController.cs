using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Localization;
using Nop.Core.Caching;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.ProductEditor.Models;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Net;
using Nop.Plugin.Misc.ProductEditor.Filters;
using System.Data.SqlClient;
using Nop.Core.Data;
using System.Data;
using System.Globalization;

namespace Nop.Plugin.Misc.ProductEditor.Controllers
{
    public class ProductEditorController:BasePluginController
    {

        private readonly ISettingService _settingService;
        private readonly IProductService _productService;
        private readonly ILocalizationService _localizationService;
        private ICacheManager _cacheManager;
        private CultureInfo ci = new CultureInfo("en-US");

        public ProductEditorController(ISettingService settingService, IProductService productService, 
            ILocalizationService localizationService, ICacheManager cacheManager)
        {
            _settingService = settingService;
            _productService = productService;
            _localizationService = localizationService;
            _cacheManager = cacheManager;
        }

        [HttpGet]
        public string SetPrice(Int32 productId)
        {
            string PRODUCT_PRICE_MODEL_KEY = "Nop.totals.productprice-{0}";


            Product product = _productService.GetProductById(productId);
            product.Price = Convert.ToDecimal(254);
            //this go to ProductService.cs which remove everything from cache, so, it won't help if i remove manually just my productId price from cache
            _productService.UpdateProduct(product);

            //if (_cacheManager.IsSet(string.Format(PRODUCT_PRICE_MODEL_KEY, productId)))
            //{
            //    var cachedPrice = _cacheManager.Get<decimal>(string.Format(PRODUCT_PRICE_MODEL_KEY, productId));
            //}
            //_cacheManager.RemoveByPattern(string.Format(PRODUCT_PRICE_MODEL_KEY, productId));

            
            return "ok";
        }

        public ActionResult ProductDetailsEditor(int productId, int updatecartitemid = 0)
        {
            Product product = _productService.GetProductById(productId);

            ProductIngredientModel model = PrepareProductIngredientModel(product);
            return View(model);
        }

        [NopHttpsRequirement(SslRequirement.No)]
        private ProductIngredientModel PrepareProductIngredientModel(Product product)
        {

            ProductIngredientModel model = new ProductIngredientModel
            {
                Id = product.Id,
                ProductName = product.Name,
                Price = 8.17M,
                Description =
                    "It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. Nam liber tempor cum soluta nobis eleifend option congue nihil imperdiet doming id quod mazim placerat facer possim assum.It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. Nam liber tempor cum soluta nobis eleifend option congue nihil imperdiet doming id quod mazim placerat facer possim assum.It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. Nam liber tempor cum soluta nobis eleifend option congue nihil imperdiet doming id quod mazim placerat facer possim assum.It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. Nam liber tempor cum soluta nobis eleifend option congue nihil imperdiet doming id quod mazim placerat facer possim assum.",
                ProductImage = "massage_oil.jpg"
            };

            List<IngredientModel> itmList = new List<IngredientModel>();

            IngredientModel itm1 = new IngredientModel();
            itm1.Id = 1;
            itm1.Name = "Coconut";
            itm1.Price = 8.18M;
            itm1.Image = "coconut.jpg";

            itmList.Add(itm1);

            IngredientModel itm2 = new IngredientModel();
            itm2.Id = 2;
            itm2.Name = "Chamomile";
            itm2.Price = 8.20M;
            itm2.Image = "chamomile.jpg";

            itmList.Add(itm2);
            IngredientModel itm3 = new IngredientModel();
            itm3.Id = 3;
            itm3.Name = "Cherry Berry";
            itm3.Price = 8.17M;
            itm3.Image = "cherry.jpg";
            itmList.Add(itm3);

            IngredientModel itm4 = new IngredientModel();
            itm4.Id = 4;
            itm4.Name = "Ginkgo";
            itm4.Price = 8.22M;
            itm4.Image = "ginkgo.jpg";
            itmList.Add(itm4);

            IngredientModel itm5 = new IngredientModel();
            itm5.Id = 5;
            itm5.Name = "Marigold";
            itm5.Price = 8.20M;
            itm5.Image = "marigold.jpg";
            itmList.Add(itm5);

            IngredientModel itm6 = new IngredientModel();
            itm6.Id = 6;
            itm6.Name = "Pomegranate";
            itm6.Price = 8.17M;
            itm6.Image = "pomegranate.jpg";           
            itmList.Add(itm6);

            IngredientModel itm7 = new IngredientModel();
            itm7.Id = 7;
            itm7.Name = "Rosemary-Verbena";
            itm7.Price = 8.22M;
            itm7.Image = "verbena.jpg";
            itmList.Add(itm7);

            IngredientModel itm8 = new IngredientModel();
            itm8.Id = 8;
            itm8.Name = "Vanilla";
            itm8.Price = 8.22M;
            itm8.Image = "vanilla.jpg";
            itmList.Add(itm8);

            IngredientModel itm9 = new IngredientModel();
            itm9.Id = 9;
            itm9.Name = "Lavender";
            itm9.Price = 8.22M;
            itm9.Image = "lavender.jpg";
            itmList.Add(itm9);

            IngredientModel itm10 = new IngredientModel();
            itm10.Id = 10;
            itm10.Name = "Cypress";
            itm10.Price = 8.22M;
            itm10.Image = "cypress.jpg";
            itmList.Add(itm10);

            model.Ingredient = itmList;
            return model;
        }

        #region OpenPanel
        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult OpenPanel()
        {
            /*
            var settingsModel = new BsQuickViewSettingsModel();
            var quickViewsettings = _settingService.LoadSetting<QuickViewSettings>();
            settingsModel.ButtonContainerName = quickViewsettings.ButtonContainerName;
            settingsModel.EnableWidget = quickViewsettings.EnableWidget;
            settingsModel.ShowAlsoPurchased = quickViewsettings.ShowAlsoPurchased;
            settingsModel.ShowRelatedProducts = quickViewsettings.ShowRelatedProducts;
            settingsModel.EnableEnlargePicture = quickViewsettings.EnableEnlargePicture;
            */

            return View("QuickViewPanel");//, settingsModel);


        }
        #endregion

        public ActionResult GetTranslations_test()
        {
            var translations = new[]
            {
                new SelectListItem { Value = "US", Text = "United States" },
                new SelectListItem { Value = "CA", Text = "Canada" },
                new SelectListItem { Value = "MX", Text = "Mexico" },
            };

            var model = new PEDictionaryModel
            {
                Translations = translations,
            };

            return View(model);
        }

        [HttpGet, EtagFilter]
        public JsonResult configGetTranslations()
        {
            if(Response.StatusCode==304)
                return Json(new{ }, JsonRequestBehavior.AllowGet);

            int langId = EngineContext.Current.Resolve<IWorkContext>().WorkingLanguage.Id;
            IList < LocaleStringResource > localStrings =_localizationService.GetAllResources(langId);

            Dictionary<string, string> lcStrings=localStrings.Where(i => i.ResourceName.StartsWith("qa.",true, ci)).Select(l=> new {
                name=l.ResourceName, value=l.ResourceValue
            }).ToDictionary(g=>g.name, g=>g.value);
            
            //tst o see if cache works
            //var cs = new DataSettingsManager().LoadSettings().DataConnectionString;
            //using (SqlConnection conn = new SqlConnection(cs))
            //{
            //    using (SqlCommand cmd = new SqlCommand("dbo.P_QAGet", conn))
            //    {
            //        cmd.CommandType = CommandType.StoredProcedure;
            //        cmd.Parameters.Add("@LanguageId", SqlDbType.Int).Value = 10;

            //        SqlDataReader rdr;
            //        conn.Open();
            //        rdr = cmd.ExecuteReader();
            //        rdr.Close();
            //    }
            //}

            return Json(new { time = DateTime.Now.ToString("MM/dd/yyyy h:mm:ss tt"), lcStrings= lcStrings }, JsonRequestBehavior.AllowGet);
        }
    }


}
