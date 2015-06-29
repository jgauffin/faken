using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web;

namespace FakeN.Web
{
    public class FakeHttpResponse : HttpResponseBase
    {
        public FakeHttpResponse()
        {
            Control = new FakeHttpResponseControl();
        }

        public FakeHttpResponseControl Control { get; set; }

        public override HttpCachePolicyBase Cache
        {
            get { return new FakeHttpCachePolicy(); }
        }

        public override int StatusCode
        {
            get { return Control.StatusCode; }
            set { Control.StatusCode = value; }
        }

        public override string StatusDescription
        {
            get { return Control.StatusDescription; }
            set { Control.StatusDescription = value; }
        }

        public override int SubStatusCode { get; set; }

        public override bool Buffer
        {
            get { return Control.Buffer; }
            set { Control.Buffer = value; }
        }

        public override bool BufferOutput
        {
            get { return Control.BufferOutput; }
            set { Control.BufferOutput = value; }
        }

        public override string CacheControl
        {
            get { return Control.CacheControl; }
            set { Control.CacheControl = value; }
        }

        public override string Charset
        {
            get { return Control.Charset; }
            set { Control.Charset = value; }
        }

        public override Encoding ContentEncoding
        {
            get { return Control.ContentEncoding; }
            set { Control.ContentEncoding = value; }
        }

        public override string ContentType
        {
            get { return Control.ContentType; }
            set { Control.ContentType = value; }
        }

        public override HttpCookieCollection Cookies
        {
            get { return Control.Cookies; }
        }

        public override int Expires
        {
            get { return Control.Expires; }
            set { Control.Expires = value; }
        }

        public override DateTime ExpiresAbsolute
        {
            get { return Control.ExpiresAbsolute; }
            set { Control.ExpiresAbsolute = value; }
        }

        public override Stream Filter
        {
            get { return Control.Filter; }
            set { Control.Filter = value; }
        }

        public override NameValueCollection Headers
        {
            get { return Control.Headers; }
        }

        public override Encoding HeaderEncoding
        {
            get { return Control.HeaderEncoding; }
            set { Control.HeaderEncoding = value; }
        }

        public override bool IsClientConnected
        {
            get { return Control.IsClientConnected; }
        }

        public override bool IsRequestBeingRedirected
        {
            get { return Control.IsRequestBeingRedirected; }
        }

        public override TextWriter Output
        {
            get { return Control.Output; }
            set { Control.Output = value; }
        }

        public override Stream OutputStream
        {
            get { return Control.OutputStream; }
        }

        public override string RedirectLocation
        {
            get { return Control.RedirectLocation; }
            set { Control.RedirectLocation = value; }
        }

        /// <summary>
        /// When overridden in a derived class, gets or sets the Status value that is returned to the client.
        /// </summary>
        /// <returns>
        /// The status of the HTTP output. For information about valid status codes, see HTTP Status Codes on the MSDN Web site.
        /// </returns>
        public override string Status
        {
            get { return Control.Status ?? StatusCode.ToString(); }
            set { Control.Status = value; }
        }

        public override bool SuppressContent
        {
            get { return Control.SuppressContent; }
            set { Control.SuppressContent = value; }
        }

        public override bool TrySkipIisCustomErrors
        {
            get { return Control.TrySkipIisCustomErrors; }
            set { Control.TrySkipIisCustomErrors = value; }
        }

        public override string ApplyAppPathModifier(string virtualPath)
        {
            return Control.AppPathModifier(virtualPath);
        }

        public FakeHttpResponse SetAppPathModifier(Func<string, string> appPathModifier)
        {
            Control.AppPathModifier = appPathModifier;
            return this;
        }
    }
}