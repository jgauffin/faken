using System.IO;
using System.Net;
using System.Text;

namespace FakeN.Web
{
    public class FakeHttpResponseBuilder
    {
        private readonly FakeHttpContextBuilder _httpContextBuilder;
        private readonly  FakeHttpResponse _response = new FakeHttpResponse();

        public FakeHttpResponseBuilder(FakeHttpContextBuilder httpContextBuilder)
        {
            _httpContextBuilder = httpContextBuilder;
        }

        public FakeHttpResponseBuilder(FakeHttpContextBuilder httpContextBuilder, string htmlBody)
        {
            _httpContextBuilder = httpContextBuilder;
            WithBody(htmlBody);
        }

        public FakeHttpResponseBuilder(FakeHttpContextBuilder httpContextBuilder, string body, string contentType)
        {
            _httpContextBuilder = httpContextBuilder;
            WithBody(body, contentType);
        }

        public FakeHttpResponseBuilder Status(HttpStatusCode statusCode, string statusDescription = null)
        {
            _response.StatusCode = (int)statusCode;
            _response.StatusDescription = statusDescription ?? statusCode.ToString();
            return this;
        }

        public FakeHttpResponseBuilder Status(int statusCode, string statusDescription = null)
        {
            _response.StatusCode = statusCode;
            _response.StatusDescription = statusDescription ?? statusCode.ToString();
            return this;
        }


        public FakeHttpResponseBuilder WithBody(string body, string contentType = "text/html")
        {
            _response.Control.Headers.Add("Content-Type", contentType);
            var buf = Encoding.UTF8.GetBytes(body);
            _response.Control.OutputStream.Write(buf, 0, buf.Length);
            return this;
        }

        public FakeHttpResponseBuilder WithJsonBody(string body)
        {
            _response.Control.Headers.Add("Content-Type", "application/json");
            var buf = Encoding.UTF8.GetBytes(body);
            _response.Control.OutputStream.Write(buf, 0, buf.Length);
            return this;
        }

        public FakeHttpResponseBuilder WithBinaryBody(Stream body, string contentType = "application/octet-stream")
        {
            _response.Control.Headers.Add("Content-Type", contentType);
            _response.Control.OutputStream = body;
            return this;
        }

        public FakeHttpResponse BuildResponse()
        {
            return _response;
        }

        public FakeHttpContext Build()
        {
            return _httpContextBuilder.Build();
        }

    }
}