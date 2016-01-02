using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.ProductEditor.Models
{
    public class IngredientCategory
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public Int16 Removable { get; set; }
        public Int16 Editable { get; set; }
        public List<Ingredient> Items { get; set; }
    }
    
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Min { get; set; }
        public decimal Max { get; set; }
        public decimal Value { get; set; }
        public decimal Price { get; set; }
        public Int16 Selected { get; set; }
    }
}
