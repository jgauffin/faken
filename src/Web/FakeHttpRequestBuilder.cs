using System;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using System.Web;

namespace FakeN.Web
{
    public class FakeHttpRequestBuilder
    {
        private readonly FakeHttpContextBuilder _builder;
        private readonly string _url;
        private FakeHttpRequest _request;
        public FakeHttpRequestBuilder(FakeHttpContextBuilder builder, string url, string method)
        {
            _builder = builder;
            _url = url;
            _request = new FakeHttpRequest(new Uri(url)) {Control = {HttpMethod = method}};
        }

        public FakeHttpRequestBuilder WithForm(NameValueCollection form)
        {
            _request.Control.Form = form;
            return this;
        }

        public FakeHttpRequestBuilder WithForm(object anonObject)
        {
            _request.Control.Form = Helper.ToNameValue(anonObject);
            return this;
        }

        public FakeHttpRequestBuilder WithBody(string body, string contentType = "application/x-form-url-encoded")
        {
            _request.Control.Headers.Add("Content-Type", contentType);
            var buf = Encoding.UTF8.GetBytes(body);
            _request.Control.InputStream.Write(buf,0,buf.Length);
            return this;
        }

        public FakeHttpRequestBuilder WithJsonBody(string body)
        {
            _request.Control.Headers.Add("Content-Type", "application/json");
            var buf = Encoding.UTF8.GetBytes(body);
            _request.Control.InputStream.Write(buf,0,buf.Length);
            return this;
        }
        
        public FakeHttpRequestBuilder WithBinaryBody(Stream body, string contentType = "application/octet-stream")
        {
            _request.Control.Headers.Add("Content-Type", contentType);
            _request.Control.InputStream = body;
            return this;
        }

        public FakeHttpRequestBuilder WithBinaryBody(byte[] body, string contentType = "application/octet-stream")
        {
            _request.Control.Headers.Add("Content-Type", contentType);
            _request.Control.InputStream.Write(body, 0, body.Length);
            return this;
        }

        public FakeHttpRequest BuildRequest()
        {
            return _request;
        }


        public FakeHttpResponseBuilder RespondWith()
        {
            return _builder.RespondWith();
        }

        public FakeHttpContextBuilder RespondWith(string body, string contentType = "text/html")
        {
            return _builder.RespondWith(body, contentType);
        }

        /// <summary>
        /// Requires that <see cref="FakeHttpContextBuilder.BodySerializer"/> is specified.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public FakeHttpContextBuilder RespondWith(object body, string contentType = "application/json")
        {
            return _builder.RespondWith(body, contentType);
        }

        public FakeHttpRequestBuilder AddCookie(string name, string value)
        {
            _request.Cookies.Add(new HttpCookie(name, value));
            return this;
        }

        public FakeHttpRequestBuilder AddCookie(HttpCookie cookie)
        {
            _request.Cookies.Add(cookie);
            return this;
        }
    }
}