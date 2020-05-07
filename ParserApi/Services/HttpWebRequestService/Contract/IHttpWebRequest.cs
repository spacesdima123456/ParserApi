using System.Net;

namespace ParserApi.Services.HttpWebRequestService
{
    public interface IHttpWebRequest
    {
        WebResponse ExecuteRequest(string url);
    }
}
