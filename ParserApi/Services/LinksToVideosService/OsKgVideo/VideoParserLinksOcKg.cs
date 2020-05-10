using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using ParserApi.Services.HttpWebRequestService;
using ParserApi.Services.LinksToVideosService.Contract;

namespace ParserApi.Services.LinksToVideosService.OsKgVideo
{
    public class VideoParserLinksOcKg: IVideoLinks
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpWebRequest _webRequest;

        public VideoParserLinksOcKg(IHttpWebRequest webRequest, IConfiguration configuration)
        {
            _webRequest = webRequest;
            _configuration = configuration;
        }

        public string GetLinkMovie(string url)
        {
            try
            {
                return GetLinkMovieResult(url);
            }
            catch 
            {
                return null;
            }
        }

        private string GetLinkMovieResult(string url)
        {
            var license = GetLicenseByUrl(url);
            var temporaryLink = ConcatenateValueByKey(license, "OsKg");
            var html = GetHtml(temporaryLink);
            var videoUrl = GetVideoUrl(html);
            var proxy = ConcatenateValueByKey(videoUrl,"Proxy");
            return proxy;
        }

        private string GetLicenseByUrl(string url)
        {
            var apiUrl = ConcatenateValueByKey(url, "OsKgUrlApi");
            var html = GetHtml(apiUrl);
            var license = GetLicenseByHtml(html);
            return license;
        }

        private string GetVideoUrl(string html)
        {
            var link = FindLinkInHtmlCode(html);
            var croppedLink = RemoveLastCharacters(link, 7);
            var replacedString = Replace(croppedLink);
            return replacedString;
        }

        private string FindLinkInHtmlCode(string jsFile)
        {
            var link = Regex.Matches(jsFile, @"src:(?<url>.*)")[1].Value;
            return link;
        }

        private string RemoveLastCharacters(string line, int count)
        {
            var length = line.Length - count;
            return line.Remove(length);
        }
            
        private string Replace(string url)
        {
            var sb = new StringBuilder(url);
            sb.Replace("p2", "p1");
            sb.Replace("8081", "8080");
            sb.Replace("https:/", string.Empty);
            sb.Replace("http:/", string.Empty);
            sb.Replace("src: \"", string.Empty);
            return sb.ToString();
        }

        private string ConcatenateValueByKey(string value,  string key)
        {
            var result = _configuration[key] + value;
            return result;
        }

        private string GetLicenseByHtml(string html)
        {
            var jsonValid = html.Replace("JsHttpRequest.dataReady(", "").Replace(")", "");
            var license = (string)JObject.Parse(jsonValid).SelectToken("js[0].response.movie.files[0].links.license");
            return license;
        }

        private string GetHtml(string url)
        {
            var data = "";
            var httpWeb = (HttpWebResponse)_webRequest.ExecuteRequest(url);

            if (httpWeb.StatusCode == HttpStatusCode.OK)
            {
                using var receiveStream = httpWeb.GetResponseStream();
                StreamReader readStream = null;

                if (string.IsNullOrWhiteSpace(httpWeb.CharacterSet))
                    readStream = new StreamReader(receiveStream);

                else
                    readStream = new StreamReader(receiveStream, Encoding.Default);

                data = readStream.ReadToEnd();
            }
            return data;
        }
    }
}
