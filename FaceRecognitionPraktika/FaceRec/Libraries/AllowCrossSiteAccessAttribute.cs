using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Text.RegularExpressions;

namespace FaceRec.Library
{
    public class AllowCrossSiteAccessAttribute : ActionFilterAttribute
    {
        private static Regex OriginExpression = new Regex(System.Configuration.ConfigurationManager.AppSettings["AllowedOriginExpression"], RegexOptions.Compiled);

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();

            string currentOrigin = filterContext.RequestContext.HttpContext.Request.Headers["Origin"];
            if (OriginExpression.IsMatch(currentOrigin))
            {
                filterContext.RequestContext.HttpContext.Response.AppendHeader("Access-Control-Allow-Origin", currentOrigin);
            }

            string requestedMethod = HttpContext.Current.Request.Headers["Access-Control-Request-Method"];
            if (requestedMethod == "OPTIONS" || requestedMethod == "POST")
            {
                filterContext.RequestContext.HttpContext.Response.AppendHeader("Access-Control-Allow-Methods", "GET, POST, OPTIONS");
                filterContext.RequestContext.HttpContext.Response.AppendHeader("Access-Control-Allow-Headers", "X-Requested-With, Accept, Access-Control-Allow-Origin");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}