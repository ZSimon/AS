using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.ProductEditor.Models
{
    public class PEDictionaryModel
    {
        public IEnumerable<SelectListItem> Translations { get; set; }
    }
}
