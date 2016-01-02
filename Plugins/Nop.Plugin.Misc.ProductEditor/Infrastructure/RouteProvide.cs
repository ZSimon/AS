using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Mvc.Routes;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.ProductEditor.Infrastructure
{
    class RouteProvide: IRouteProvider
    {
        public int Priority
        {
            get { return 1; }
        }

        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            ViewEngines.Engines.Insert(0, new ProductEditorViewEngine());
        }
    }
}
