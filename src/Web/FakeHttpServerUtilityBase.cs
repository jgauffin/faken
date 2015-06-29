using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;

namespace FakeN.Web
{
    public class FakeHttpServerUtilityBase : HttpServerUtilityBase
    {
        public FakeHttpServerUtilityBase()
        {
            TransferRequests = new List<TransferRequest>();
            MappedPaths = new Dictionary<string, string>();
        }

        /// <summary>
        /// Contains all invocations to one of the <c>TransferRequest</c> overloads.
        /// </summary>
        public List<TransferRequest> TransferRequests { get; private set; }

        /// <summary>
        /// Use this property to be able to return custom values for <c>MapPath</c>.
        /// </summary>
        public IDictionary<string, string> MappedPaths { get; set; }

        /// <summary>
        ///     When overridden in a derived class, returns the physical file path that corresponds to the specified virtual path
        ///     on the Web server.
        /// </summary>
        /// <returns>
        ///     The physical file path that corresponds to <paramref name="path" />.
        /// </returns>
        /// <param name="path">The virtual path to get the physical path for.</param>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override string MapPath(string path)
        {
            string mappedPath;
            return !MappedPaths.TryGetValue(path, out mappedPath)
                ? "/teststub/"
                : mappedPath;
        }

        /// <summary>
        ///     When overridden in a derived class, terminates execution of the current process and starts execution of a new
        ///     request, using a custom HTTP handler and a value that specifies whether to clear the
        ///     <see cref="P:System.Web.HttpRequestBase.QueryString" /> and <see cref="P:System.Web.HttpRequestBase.Form" />
        ///     collections.
        /// </summary>
        /// <param name="handler">The HTTP handler to transfer the current request to.</param>
        /// <param name="preserveForm">
        ///     true to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and
        ///     <see cref="P:System.Web.HttpRequest.Form" /> collections; false to clear the
        ///     <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.
        /// </param>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override void Transfer(IHttpHandler handler, bool preserveForm)
        {
            TransferRequests.Add(new TransferRequest(handler, preserveForm));
            //base.Transfer(handler, preserveForm);
        }

        /// <summary>
        ///     When overridden in a derived class, asynchronously executes the end point at the specified URL.
        /// </summary>
        /// <param name="path">The URL of the page or handler to execute.</param>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override void TransferRequest(string path)
        {
            TransferRequests.Add(new TransferRequest(path));
        }

        /// <summary>
        ///     When overridden in a derived class, asynchronously executes the endpoint at the specified URL and specifies whether
        ///     to clear the <see cref="P:System.Web.HttpRequestBase.QueryString" /> and
        ///     <see cref="P:System.Web.HttpRequestBase.Form" /> collections.
        /// </summary>
        /// <param name="path">The URL of the page or handler to execute.</param>
        /// <param name="preserveForm">
        ///     true to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and
        ///     <see cref="P:System.Web.HttpRequest.Form" /> collections; false to clear the
        ///     <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.
        /// </param>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override void TransferRequest(string path, bool preserveForm)
        {
            TransferRequests.Add(new TransferRequest(path, preserveForm));
        }

        /// <summary>
        ///     When overridden in a derived class, asynchronously executes the endpoint at the specified URL by using the
        ///     specified HTTP method and headers.
        /// </summary>
        /// <param name="path">The URL of the page or handler to execute.</param>
        /// <param name="preserveForm">
        ///     true to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and
        ///     <see cref="P:System.Web.HttpRequest.Form" /> collections; false to clear the
        ///     <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.
        /// </param>
        /// <param name="method">
        ///     The HTTP method (GET, POST, and so on) to use for the new request. If null, the HTTP method of the
        ///     original request is used.
        /// </param>
        /// <param name="headers">A collection of request headers for the new request.</param>
        /// <exception cref="T:System.NotImplementedException">Always.</exception>
        public override void TransferRequest(string path, bool preserveForm, string method, NameValueCollection headers)
        {
            TransferRequests.Add(new TransferRequest(path, preserveForm, method, headers));
        }
    }
}