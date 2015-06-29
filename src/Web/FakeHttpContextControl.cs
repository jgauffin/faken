using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Caching;

namespace FakeN.Web
{
    public class FakeHttpContextControl
    {
        private readonly FakeHttpContext _httpContext;
        private FakeHttpRequest _request;
        private FakeHttpResponse _response;

        public FakeHttpContextControl(FakeHttpRequest request = null,
            FakeHttpResponse response = null,
            FakeHttpSessionState session = null)
        {
            AllErrors = new List<Exception>();
            ApplicationInstance = new FakeHttpApplication();
            Application = new FakeHttpStateBase();
            CurrentHandler = new DefaultHttpHandler();
            Items = new Dictionary<object, object>();
            Request = request ?? new FakeHttpRequest();
            Response = response ?? new FakeHttpResponse();
            Server = new FakeHttpServerUtilityBase();
            Session = session ?? new FakeHttpSessionState();
            TimeStamp = DateTime.Now;
            User = new GenericPrincipal(new MutableIdentity(), new string[] { });
        }

        public FakeHttpContextControl()
            : this(null, null, null)
        {
        }

        public FakeHttpContextControl(FakeHttpContext httpContext)
            : this()
        {
            _httpContext = httpContext;
        }

        public IPrincipal User { get; set; }
        public Dictionary<object, object> Items { get; set; }

        public FakeHttpRequest Request
        {
            get { return _request; }
            set
            {
                _request = value;
                _request.Control.HttpContext = _httpContext;
            }
        }

        public FakeHttpResponse Response
        {
            get { return _response; }
            set
            {
                _response = value;
            }
        }

        public HttpSessionStateBase Session { get; set; }
        public TraceContext TraceContext { get; set; }
        public FakeHttpServerUtilityBase Server { get; set; }
        public IHttpHandler PreviousHandler { get; set; }
        public bool IsPostNotification { get; set; }
        public bool IsDebuggingEnabled { get; set; }
        public Exception Error { get; set; }
        public RequestNotification CurrentNotification { get; set; }
        public IHttpHandler CurrentHandler { get; set; }
        public Cache Cache { get; set; }
        public FakeHttpApplication ApplicationInstance { get; set; }
        public FakeHttpStateBase Application { get; set; }
        public List<Exception> AllErrors { get; set; }
        public DateTime TimeStamp { get; set; }
        public bool IsCustomErrorsEnabled { get; set; }
        public IHttpHandler Handler { get; set; }
    }
}