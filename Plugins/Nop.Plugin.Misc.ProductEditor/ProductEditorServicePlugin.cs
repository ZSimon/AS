using Nop.Core.Domain.Localization;
using Nop.Core.Events;
using Nop.Core.Plugins;
using Nop.Services.Cms;
using Nop.Services.Common;
using Nop.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Routing;
using Nop.Plugin.Misc.ProductEditor.Filters;
using System.Globalization;
using Nop.Plugin.Misc.ProductEditor.Extensions;

namespace Nop.Plugin.Misc.ProductEditor
{
    class ProductEditorServicePlugin:BasePlugin, IMiscPlugin, IWidgetPlugin, IConsumer<EntityInserted<LocaleStringResource>>,
        IConsumer<EntityUpdated<LocaleStringResource>>, IConsumer<EntityDeleted<LocaleStringResource>>
    {
        #region Ctor

        CultureInfo ci=new CultureInfo("en-US");
        public ProductEditorServicePlugin()
        {
        }

        #endregion

        #region Methods

        public override void Install()
        {
            base.Install();
        }

        public override void Uninstall()
        {
            base.Uninstall();
        }

        //definiram controller, kjer imam konfiguracijo, trenutno jo nimam
        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "ProductEditor";
            routeValues = new RouteValueDictionary(){
                {"Namespaces", "Nop.Plugin.Misc.ProductEditor.Controllers"},
                {"area", null}
            };
        }

        public IList<string> GetWidgetZones()
        {
            return new List<string>() { "footer" };
        }

        public void GetDisplayWidgetRoute(string widgetZone, out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "OpenPanel";
            controllerName = "ProductEditor";
            routeValues = new RouteValueDictionary()
            {
                {"Namespaces", "Nop.Plugin.Misc.ProductEditor.Controllers"},
                {"area", null},
                {"widgetZone", widgetZone}
            };
        }

        public void HandleEvent(EntityInserted<LocaleStringResource> eventMessage)
        {
            //change eTag
            if (eventMessage.Entity.ResourceName.StartsWith("qa.", true, ci))
            {
                EditorGlobal.GenerateEtag(eventMessage.Entity.Language.LanguageCulture);
            }

        }

        public void HandleEvent(EntityUpdated<LocaleStringResource> eventMessage)
        {
            if (eventMessage.Entity.ResourceName.StartsWith("qa.",true, ci))
            {
                EditorGlobal.GenerateEtag(eventMessage.Entity.Language.LanguageCulture);
            }
        }

        public void HandleEvent(EntityDeleted<LocaleStringResource> eventMessage)
        {
            if (eventMessage.Entity.ResourceName.StartsWith("qa.", true, ci))
            {
                EditorGlobal.GenerateEtag(eventMessage.Entity.Language.LanguageCulture);
            }
        }

        #endregion
    }
}
