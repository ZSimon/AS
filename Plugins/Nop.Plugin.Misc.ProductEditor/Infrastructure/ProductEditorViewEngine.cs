using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Themes;

namespace Nop.Plugin.Misc.ProductEditor.Infrastructure
{
    class ProductEditorViewEngine: ThemeableRazorViewEngine
    {
        public ProductEditorViewEngine(){
            ViewLocationFormats = new[] { "~/Plugins/Misc.ProductEditor/Views/{0}.cshtml" };
            PartialViewLocationFormats = new[] { "~/Plugins/Misc.ProductEditor/Views/{0}.cshtml" };
        }
    }
}
