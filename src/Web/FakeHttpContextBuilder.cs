using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace FakeN.Web
{
    /// <summary>
    ///     Use this class to build a fake request easily.
    /// </summary>
    /// <example>
    /// <para>Basic</para>
    ///     <code>
    /// <![CDATA[
    /// var builder = new FakeHttpContextBuilder();
    /// builder.Get("http://localhost/hello?a=1")
    ///        .RespondWith("<html><body>Hello World!</body></html>");
    /// 
    /// var httpContext = builder.Build();
    /// ]]>
    /// </code>
    /// <para>With session:</para>
    /// <code>
    /// <![CDATA[
    /// var builder = new FakeHttpContextBuilder();
    /// builder.Post("http://localhost/hello?a=1")
    ///        .RespondWith("<html><body>Hello World!</body></html>")
    ///        .UsingSession(new { UserId = 1 });
    /// 
    /// var httpContext = builder.Build();
    /// ]]>
    /// </code>
    /// <para>With principal:</para>
    /// <code>
    /// <![CDATA[
    /// var builder = new FakeHttpContextBuilder();
    /// builder.Put("http://localhost/hello?a=1")
    ///        .RespondWith("<html><body>Hello World!</body></html>")
    ///        .UsePrincipal(new GenericPrincipal(/*....*));
    /// 
    /// var httpContext = builder.Build();
    /// ]]>
    /// </code>
    /// </example>
    public class FakeHttpContextBuilder
    {
        private FakeHttpRequestBuilder _request;
        private FakeHttpResponseBuilder _response;
        private IDictionary<string, object> _session;
        private IPrincipal _principal;

        /// <summary>
        /// Used to serialize the body for <see cref="RespondWith(object,string)"/>.
        /// </summary>
        [ThreadStatic]
        public static Func<object, string> BodySerializer;
        public FakeHttpContext Build()
        {
            var request = _request == null ? new FakeHttpRequest(new Uri("http://localhost/")) : _request.BuildRequest();
            var response = _response == null ? new FakeHttpResponse() : _response.BuildResponse();

            request.InputStream.Position = 0;
            response.OutputStream.Position = 0;

            var ctx = new FakeHttpContext(request, response, _session == null ? null : new FakeHttpSessionState(_session));

            if (_principal != null)
                ctx.User = _principal;
            return ctx;
        }

        public FakeHttpRequestBuilder Post(string url)
        {
            _request = new FakeHttpRequestBuilder(this, url, "POST");
            return _request;
        }

        public FakeHttpRequestBuilder Put(string url)
        {
            _request = new FakeHttpRequestBuilder(this, url, "PUT");
            return _request;
        }

        public FakeHttpRequestBuilder Get(string url)
        {
            _request = new FakeHttpRequestBuilder(this, url, "GET");
            return _request;
        }

        public FakeHttpRequestBuilder Delete(string url)
        {
            _request = new FakeHttpRequestBuilder(this, url, "DELETE");
            return _request;
        }

        public FakeHttpContextBuilder LocalFolderIs(string folder)
        {
            return this;
        }

        public FakeHttpResponseBuilder RespondWith()
        {
            _response = new FakeHttpResponseBuilder(this);
            return _response;
        }

        public FakeHttpContextBuilder RespondWith(string htmlBody, string contentType = "text/html")
        {
            _response = new FakeHttpResponseBuilder(this, htmlBody, contentType);
            return this;
        }

        /// <summary>
        /// Requires that <see cref="BodySerializer"/> is specified.
        /// </summary>
        /// <param name="body"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public FakeHttpContextBuilder RespondWith(object body, string contentType = "application/json")
        {
            _response = new FakeHttpResponseBuilder(this, BodySerializer(body), contentType);
            return this;
        }



        public FakeHttpContextBuilder UsingSession(IDictionary<string, object> session)
        {
            _session = session;
            return this;
        }

        public FakeHttpContextBuilder UsePrincipal(IPrincipal principal)
        {
            _principal = principal;
            return this;
        }

        public FakeHttpContextBuilder UsingSession(object anonObject)
        {
            _session = Helper.ToDictionary(anonObject);
            return this;
        }

        public FakeHttpContextBuilder VirtualRootIs(string absolutePath)
        {
            return this;
        }
    }
}