using System;
using System.Collections;
using System.Security.Principal;
using System.Web;
using System.Web.Caching;
using System.Web.Profile;

namespace FakeN.Web
{
    public class FakeHttpContext : HttpContextBase
    {
        private readonly ProfileBase _profile;
        private readonly HttpRequestBase request;
        private readonly HttpResponseBase response;
        private readonly HttpSessionStateBase session;

        public FakeHttpContext(
            HttpRequestBase request = null,
            HttpResponseBase response = null,
            HttpSessionStateBase session = null)
        {
            this.request = request;
            this.response = response;
            this.session = session;
            _profile = new ProfileBase();
            Control = new FakeHttpContextControl(this);
        }

        public FakeHttpContext(FakeHttpContextControl control)
        {
            Control = control;
            _profile = new ProfileBase();
        }

        public FakeHttpContext()
        {
            Control = new FakeHttpContextControl();
            _profile = new ProfileBase();
        }

        /// <summary>
        ///     Used to control this context.
        /// </summary>
        public FakeHttpContextControl Control { get; private set; }

        public override HttpRequestBase Request
        {
            get { return request ?? Control.Request; }
        }

        public override HttpResponseBase Response
        {
            get { return response ?? Control.Response; }
        }

        public override HttpSessionStateBase Session
        {
            get { return session ?? Control.Session; }
        }
        
        public override IDictionary Items
        {
            get { return Control.Items; }
        }

        public override IPrincipal User { get { return Control.User; } set { Control.User = value; } }

        public override TraceContext Trace
        {
            get { return Control.TraceContext; }
        }

        public override DateTime Timestamp
        {
            get { return Control.TimeStamp; }
        }

        public override bool SkipAuthorization { get; set; }

        public override HttpServerUtilityBase Server
        {
            get { return Control.Server; }
        }

        public override ProfileBase Profile
        {
            get { return _profile; }
        }

        public override IHttpHandler PreviousHandler
        {
            get { return Control.PreviousHandler; }
        }

        public override bool IsPostNotification
        {
            get { return Control.IsPostNotification; }
        }

        public override bool IsDebuggingEnabled
        {
            get { return Control.IsDebuggingEnabled; }
        }

        public override bool IsCustomErrorEnabled
        {
            get { return Control.IsCustomErrorsEnabled; }
        }

        public override IHttpHandler Handler
        {
            get { return Control.Handler; }
            set { Control.Handler = value; }
        }

        public override Exception Error
        {
            get { return Control.Error; }
        }

        public override RequestNotification CurrentNotification
        {
            get { return Control.CurrentNotification; }
        }

        public override IHttpHandler CurrentHandler
        {
            get { return Control.CurrentHandler; }
        }

        public override Cache Cache
        {
            get { return Control.Cache; }
        }

        public override HttpApplication ApplicationInstance
        {
            get { return Control.ApplicationInstance; }
            set { Control.ApplicationInstance = new FakeHttpApplication(value); }
        }

        public override HttpApplicationStateBase Application
        {
            get { return Control.Application; }
        }

        public override Exception[] AllErrors
        {
            get { return Control.AllErrors.ToArray(); }
        }
    }
}