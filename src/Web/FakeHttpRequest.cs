using System;
using System.Collections.Specialized;
using System.Dynamic;
using System.IO;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Routing;

namespace FakeN.Web {
    public class FakeHttpRequest : HttpRequestBase, IHttpObject {
		
		public FakeHttpRequest(Uri url = null, string method = "GET") {
            Control = new FakeHttpRequestControl(url, method);
		}

		public FakeHttpRequest SetUrl(Uri url)
		{
		    Control.SetUrl(url);
			return this;
		}

        /// <summary>
        /// Used to control what this request returns.
        /// </summary>
        public FakeHttpRequestControl Control { get; set; }

		public override string this[string key] {
            get { return new NameValueCollection { Control.Form, Control.QueryString }[key]; }
		}

        public override bool IsAuthenticated { get { return Control.IsAuthenticated; } }
        public override Uri Url { get { return Control.Url; } }
        public override bool IsLocal { get { return Control.IsLocal; } }
        public override string ApplicationPath { get { return Control.ApplicationPath; } }
        public override string HttpMethod { get { return Control.HttpMethod; } }
        public override string UserHostAddress { get { return Control.UserHostAddress; } }
        public override string[] AcceptTypes { get { return Control.AcceptTypes.ToArray(); } }
		public override string RequestType { get; set; }
		public override string ContentType { get; set; }
		public override Encoding ContentEncoding { get; set; }
		public override void ValidateInput() { }
        public override string RawUrl { get { return Control.Url.PathAndQuery; } }
        public override NameValueCollection Form { get { return Control.Form; } }
        public override NameValueCollection QueryString { get { return Control.QueryString; } }
        public override NameValueCollection Headers { get { return Control.Headers; } }
        public override NameValueCollection ServerVariables { get { return Control.ServerVariables; } }
        public override HttpCookieCollection Cookies { get { return Control.Cookies; } }
        public override HttpFileCollectionBase Files { get { return Control.Files; } }
		public override string Path { get { return Url.AbsolutePath; } }
		public override string FilePath { get { return Url.AbsolutePath; } }
        public override string PathInfo { get { return Control.PathInfo; } }
        public override Stream InputStream { get { return Control.InputStream; } }
        public override string AnonymousID { get { return Control.AnonymousId; } }
        public override string AppRelativeCurrentExecutionFilePath { get { return Control.AppRelativeCurrentExecutionFilePath; } }
        public override HttpBrowserCapabilitiesBase Browser { get { return Control.Browser; } }
        public override ChannelBinding HttpChannelBinding { get { return Control.HttpChannelBinding; } }
        public override HttpClientCertificate ClientCertificate { get { return Control.ClientCertificate; } }
        public override int ContentLength { get { return Control.ContentLength; } }
        public override string CurrentExecutionFilePath { get { return Control.CurrentExecutionFilePath; } }
        public override Stream Filter { get { return Control.Filter; } set { Control.Filter = value; } }
        public override bool IsSecureConnection { get { return Control.IsSecureConnection; } }
        public override WindowsIdentity LogonUserIdentity { get { return Control.LogonUserIdentity; } }
        public override NameValueCollection Params { get { return Control.Params; } }
        public override string PhysicalApplicationPath { get { return Control.PhysicalApplicationPath; } }
        public override string PhysicalPath { get { return Control.PhysicalPath; } }
        public override RequestContext RequestContext { get { return Control.RequestContext; } }
        public override int TotalBytes { get { return Control.TotalBytes; } }
        public override Uri UrlReferrer { get { return Control.UrlReferrer; } }
        public override string UserAgent { get { return Control.UserAgent; } }
        public override string[] UserLanguages { get { return Control.UserLanguages.ToArray(); } }
        public override string UserHostName { get { return Control.UserHostName; } }

    }
}