using System;
using System.Web;

namespace FakeN.Web
{
    public class FakeHttpApplication : HttpApplication
    {
        /// <summary>
        ///     Used to override <c>GetOutputCacheProviderName</c>
        /// </summary>
        public Func<HttpContext, string> GetOutputCacheProviderNameCallback;

        /// <summary>
        ///     Used to ovveride <c>GetVaryByCustomString</c>.
        /// </summary>
        public Func<HttpContext, string, string> GetVaryByCustomStringCallback;

        public FakeHttpApplication(HttpApplication assignedApplication)
        {
            AssignedApplication = assignedApplication;
        }

        public FakeHttpApplication()

        {
        }

        public HttpApplication AssignedApplication { get; set; }

        /// <summary>
        ///     Gets the name of the default output-cache provider that is configured for a Web site.
        /// </summary>
        /// <returns>
        ///     The name of the default provider.
        /// </returns>
        /// <param name="context">
        ///     An <see cref="T:System.Web.HttpContext" /> that provides references to intrinsic server objects
        ///     that are used to service HTTP requests.
        /// </param>
        /// <exception cref="T:System.Configuration.Provider.ProviderException">
        ///     <paramref name="context" /> is null or is an empty
        ///     string.
        /// </exception>
        public override string GetOutputCacheProviderName(HttpContext context)
        {
            if (GetOutputCacheProviderNameCallback != null)
                return GetOutputCacheProviderNameCallback(context);

            return base.GetOutputCacheProviderName(context);
        }

        /// <summary>
        ///     Provides an application-wide implementation of the
        ///     <see cref="P:System.Web.UI.PartialCachingAttribute.VaryByCustom" /> property.
        /// </summary>
        /// <returns>
        ///     If the value of the <paramref name="custom" /> parameter is "browser", the browser's
        ///     <see cref="P:System.Web.Configuration.HttpCapabilitiesBase.Type" />; otherwise, null.
        /// </returns>
        /// <param name="context">
        ///     An <see cref="T:System.Web.HttpContext" /> object that contains information about the current Web
        ///     request.
        /// </param>
        /// <param name="custom">The custom string that specifies which cached response is used to respond to the current request. </param>
        public override string GetVaryByCustomString(HttpContext context, string custom)
        {
            if (GetVaryByCustomStringCallback != null)
                return GetVaryByCustomStringCallback(context, custom);

            return base.GetVaryByCustomString(context, custom);
        }
    }
}