using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Principal;
using System.Web;
using System.Web.Routing;

namespace FakeN.Web
{
    public class FakeHttpRequestControl
    {
        private NameValueCollection _params;
        private int _contentLength = -1;
        private FakeHttpContext _httpContext;

        public FakeHttpRequestControl(Uri url = null, string httpMethod = "GET")
        {
            Url = url ?? new Uri("http://localhost");
            HttpMethod = httpMethod;
            AcceptTypes = new List<string>();
            UserLanguages = new List<string>() {"en-US"};
            QueryString = HttpUtility.ParseQueryString(Url.Query);
            Form = new NameValueCollection();
            Headers = new NameValueCollection();
            ServerVariables = new NameValueCollection();
            Cookies = new HttpCookieCollection();
            InputStream = new MemoryStream();
            PathInfo = "";
            PhysicalApplicationPath = "C:\\temp";
            PhysicalPath = Path.Combine(PhysicalApplicationPath, Url.AbsolutePath.TrimStart('/').Replace('/', '\\'));
            RequestContext = new FakeRequestContext();
        }

        public FakeHttpContext HttpContext
        {
            get { return _httpContext; }
            set
            {
                _httpContext = value;
                RequestContext.HttpContext = value;
            }
        }

        public HttpCookieCollection Cookies { get; set; }
        public NameValueCollection Form { get; set; }
        public NameValueCollection Headers { get; set; }
        public NameValueCollection QueryString { get; set; }
        public NameValueCollection ServerVariables { get; set; }
        public List<string> AcceptTypes { get; set; }
        public string AnonymousId { get; set; }
        public string ApplicationPath { get; set; }
        public string AppRelativeCurrentExecutionFilePath { get; set; }
        public HttpBrowserCapabilitiesBase Browser { get; set; }
        public HttpClientCertificate ClientCertificate { get; set; }

        public int ContentLength
        {
            get
            {
                return (int) (_contentLength != -1 ?  _contentLength : InputStream.Length);
            }
            set { _contentLength = value; }
        }

        public string CurrentExecutionFilePath { get; set; }
        public HttpFileCollectionBase Files { get; set; }
        public Stream Filter { get; set; }
        public ChannelBinding HttpChannelBinding { get; set; }
        public Stream InputStream { get; set; }
        public bool IsAuthenticated { get; set; }
        public bool IsLocal { get; set; }
        public bool IsSecureConnection { get; set; }
        public WindowsIdentity LogonUserIdentity { get; set; }
        public string HttpMethod { get; set; }

        public NameValueCollection Params
        {
            get
            {
                return _params ?? QueryString;
            }
            set { _params = value; }
        }

        public string PathInfo { get; set; }
        public string PhysicalApplicationPath { get; set; }
        public string PhysicalPath { get; set; }
        public RequestContext RequestContext { get; set; }
        public int TotalBytes { get; set; }
        public Uri Url { get; set; }
        public Uri UrlReferrer { get; set; }
        public string UserAgent { get; set; }
        public string UserHostAddress { get; set; }
        public string UserHostName { get; set; }
        public List<string> UserLanguages { get; set; }

        public FakeHttpRequestControl SetUrl(Uri url)
        {
            this.Url = url;
            QueryString.Clear();
            QueryString.Add(HttpUtility.ParseQueryString(url.Query));
            return this;
        }
    }
}