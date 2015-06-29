using System;
using System.Web;
using System.Web.Routing;

namespace FakeN.Web
{
    public class FakeRequestContext : RequestContext
    {
        public FakeRequestContext(FakeHttpContext httpContext)
        {
            if (httpContext == null) throw new ArgumentNullException("httpContext");
            HttpContext = httpContext;
            RouteData = new RouteData();
        }

        public FakeRequestContext()
        {
            RouteData = new RouteData();
        }

        /// <summary>
        /// Gets information about the requested route.
        /// </summary>
        /// <returns>
        /// An object that contains information about the requested route.
        /// </returns>
        public override RouteData RouteData { get; set; }

        /// <summary>
        /// Gets information about the HTTP request.
        /// </summary>
        /// <returns>
        /// An object that contains information about the HTTP request.
        /// </returns>
        public override HttpContextBase HttpContext { get; set; }
    }
}