using ParserApi.Services.HttpWebRequestService;
using ParserApi.Services.ParserServic.Contract;
using System.IO;
using System.Net;
using System.Text;

namespace ParserApi.Services.ParserServic
{
    public class ParserHtml : ParserBase
    {
        private readonly IHttpWebRequest _httpWebRequest;
        public ParserHtml(IHttpWebRequest httpWebRequest)
        {
            _httpWebRequest = httpWebRequest;
        }

        public override string ToParse(string url)
        {
            var responce = _httpWebRequest.ExecuteRequest(url);
            var html = GetHtml((HttpWebResponse)responce);
            return html;
        }

        private string GetHtml(HttpWebResponse httpWeb)
        {
            var data = "";
            if (httpWeb.StatusCode == HttpStatusCode.OK)
            {
                using var receiveStream = httpWeb.GetResponseStream();
                StreamReader readStream = null;

                if (string.IsNullOrWhiteSpace(httpWeb.CharacterSet))
                    readStream = new StreamReader(receiveStream);

                else
                    readStream = new StreamReader(receiveStream, Encoding.Default /*Encoding.GetEncoding(httpWeb.CharacterSet)*/);

                 data = readStream.ReadToEnd();
            }
            return data;
        }
    }
}
