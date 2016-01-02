using System.Web.Routing;
using Nop.Core.Plugins;
using Nop.Services.Common;

namespace Nop.Plugin.Misc.FacebookShop
{
    public class FacebookShopPlugin : BasePlugin, IMiscPlugin
    {
        #region Methods

        /// <summary>
        /// Gets a route for provider configuration
        /// </summary>
        /// <param name="actionName">Action name</param>
        /// <param name="controllerName">Controller name</param>
        /// <param name="routeValues">Route values</param>
        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = null;//"Configure";
            controllerName = null;//"MiscFacebookShop";
            routeValues = null;// new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Misc.FacebookShop.Controllers" }, { "area", null } };
         
        }

        #endregion
    }
}
