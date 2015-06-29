using System.Collections.Specialized;
using System.Web;

namespace FakeN.Web
{
    /// <summary>
    ///     Container for <see cref="FakeHttpServerUtilityBase" />
    /// </summary>
    public class TransferRequest
    {
        public TransferRequest(IHttpHandler handler, bool preserveForm)
        {
            Handler = handler;
            PreserveForm = preserveForm;
        }

        public TransferRequest(string path)
        {
            Path = path;
        }

        public TransferRequest(string path, bool preserveForm)
        {
            Path = path;
            PreserveForm = preserveForm;
        }

        public TransferRequest(string path, bool preserveForm, string method, NameValueCollection headers)
        {
            Path = path;
            PreserveForm = preserveForm;
            Method = method;
            Headers = headers;
        }

        public string Path { get; set; }
        public IHttpHandler Handler { get; set; }
        public bool PreserveForm { get; set; }
        public string Method { get; set; }
        public NameValueCollection Headers { get; set; }
    }
}