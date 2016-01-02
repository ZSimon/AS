using Nop.Core;
using Nop.Core.Infrastructure;
using Nop.Web.Controllers;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Nop.Plugin.Misc.ProductEditor.Extensions;

namespace Nop.Plugin.Misc.ProductEditor.Filters
{
    public class CompressFilter : ActionFilterAttribute, IFilterProvider
    {
        //http://stackoverflow.com/questions/25723821/how-to-implement-an-action-filter-in-nopcommerce
        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
        //    if (controllerContext.Controller is CheckoutController && actionDescriptor.ActionName.Equals("OpcSaveBilling", StringComparison.InvariantCultureIgnoreCase))
        //    {
        //        return new List<Filter>() { new Filter(this, FilterScope.Action, 0) };
        //    }
            return new List<Filter>();
        }
        /*When you compress the content whose content length is less than 1000, this method would not be successful, because that takes more time to compress than to send that amount of content.*/
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpRequestBase request = filterContext.HttpContext.Request;

            string acceptEncoding = request.Headers["Accept-Encoding"];

            if (string.IsNullOrEmpty(acceptEncoding)) return;

            acceptEncoding = acceptEncoding.ToUpperInvariant();

            HttpResponseBase response = filterContext.HttpContext.Response;

            if (acceptEncoding.Contains("GZIP"))
            {
                response.AppendHeader("Content-encoding", "gzip");
                response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
            }
            else if (acceptEncoding.Contains("DEFLATE"))
            {
                response.AppendHeader("Content-encoding", "deflate");
                response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
            }
        }
    }

    public class EtagFilter : ActionFilterAttribute, IFilterProvider
    {
        private DateTime _currentTime = DateTime.Now;

        //public Dictionary<string,string> _currentEtag= new Dictionary<string, string>();

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            return new List<Filter>();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpRequestBase request = filterContext.HttpContext.Request;
            string language = EngineContext.Current.Resolve<IWorkContext>().WorkingLanguage.LanguageCulture;

            //string eTag = request.Headers["ETag"];
            string eTag = request.Headers["If-None-Match"];
            string responseEtag;
            EditorGlobal.CurrentEtag.TryGetValue(language, out responseEtag);

            if (string.IsNullOrEmpty(responseEtag))
            {
                EditorGlobal.GenerateEtag(language);
                responseEtag = EditorGlobal.CurrentEtag[language];
            }

            HttpResponseBase response = filterContext.HttpContext.Response;

            if (!string.IsNullOrEmpty(eTag))
            {
                if (eTag.Equals(responseEtag))
                {
                    response.StatusCode = 304;
                    response.StatusDescription = "Not Modified";
                    response.Cache.SetETag(responseEtag);
                    return;
                }
            }

            response.Cache.SetCacheability(HttpCacheability.ServerAndPrivate);
            //response.AddHeader("ETag", responseETag);
            response.Cache.SetETag(responseEtag);
        }

                
        //public void GenerateEtag(Boolean add, string language)
        //{
            
        //    if (add)
        //        _currentEtag.Add(language, language + Guid.NewGuid().ToString().Substring(0, 20));
        //    else
        //        _currentEtag[language] = language + Guid.NewGuid().ToString().Substring(0, 20);
        //}
    }



    
}
