using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.Catalog
{
    public partial class ProductMap
    {
        protected override void PostInitialize()
        {
            this.Property(p => p.IsIngredient);//.HasColumnType("bit");
        }
    }
}
