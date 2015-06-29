using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web;

namespace FakeN.Web
{
    public class FakeHttpResponseControl
    {
        public FakeHttpResponseControl()
		{
            AppPathModifier = x => x;
            Charset = "utf-8";
            ContentEncoding = Encoding.UTF8;
            ContentType = "text/html";
            Cookies = new HttpCookieCollection();
            Expires = 60;
            ExpiresAbsolute = DateTime.Now.AddSeconds(60);
            Headers = new NameValueCollection();
            OutputStream = new MemoryStream();
            Output = new StreamWriter(OutputStream);
            StatusCode = 200;
            StatusDescription = "OK";
		}

        public Func<string, string> AppPathModifier { get; set; }

        public bool Buffer { get; set; }

        public bool BufferOutput { get; set; }

        public string CacheControl { get; set; }

        public string Charset { get; set; }

        public Encoding ContentEncoding { get; set; }

        public string ContentType { get; set; }

        public HttpCookieCollection Cookies { get; set; }

        public int Expires { get; set; }

        public DateTime ExpiresAbsolute { get; set; }

        public Stream Filter { get; set; }

        public NameValueCollection Headers { get; set; }

        public Encoding HeaderEncoding { get; set; }

        public bool IsClientConnected { get; set; }

        public bool IsRequestBeingRedirected { get; set; }

        public TextWriter Output { get; set; }

        public Stream OutputStream { get; set; }

        public string RedirectLocation { get; set; }

        public string Status { get; set; }

        public bool SuppressContent { get; set; }

        public bool TrySkipIisCustomErrors { get; set; }
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
    }
}