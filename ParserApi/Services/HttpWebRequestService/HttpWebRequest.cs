using System.Net;

namespace ParserApi.Services.HttpWebRequestService
{
    public class HttpWebRequest : IHttpWebRequest
    {
        private readonly string _userAgent;

        public HttpWebRequest(string userAgent)
        {
            _userAgent = userAgent;
        }

        public WebResponse ExecuteRequest(string url)
        {
            var request = (System.Net.HttpWebRequest)WebRequest.Create(url);
            request.UserAgent = _userAgent;
            return (HttpWebResponse)request.GetResponse();
        }
    }
}
