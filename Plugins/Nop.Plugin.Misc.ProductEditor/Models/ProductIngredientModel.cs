using Nop.Web.Framework.Mvc;
using Nop.Web.Models.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.ProductEditor.Models
{
    public class ProductIngredientModel : BaseNopEntityModel
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        //public PictureModel DefaultPictureModel { get; set; }
        public string ProductImage { get; set; }
        public List<IngredientModel> Ingredient { get; set; }
    }

    public class IngredientModel : BaseNopEntityModel
    {
        //public int Id { get; set; } je že iz baseNop
        public string Name { get; set; }
        public string Description { get; set; }
        public Decimal Price { get; set; }
        public string Image { get; set; }
    }
}
