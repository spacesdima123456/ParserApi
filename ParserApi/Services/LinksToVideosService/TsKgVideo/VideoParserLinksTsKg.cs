using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using ParserApi.Services.HttpWebRequestService;
using ParserApi.Services.LinksToVideosService.Contract;
using System.Net;

namespace ParserApi.Services.LinksToVideosService.TsKgVideo
{
    public class VideoParserLinksTsKg : IVideoLinks
    {
        private readonly string _tsKg;
        private readonly HtmlWeb _htmlWeb;
        private readonly IHttpWebRequest _webRequest;

        public VideoParserLinksTsKg(IConfiguration configuration, IHttpWebRequest webRequest, HtmlWeb htmlWeb)
        {
            _htmlWeb = htmlWeb;
            _webRequest = webRequest;
            _tsKg = configuration["TsKg"];
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
            var link = _tsKg + url;
            var downloadLink = GetPartDownloadLink(link);
            var temporaryLink = GetTemporaryLink(downloadLink);
            var responce = GetLinkFromRequest(temporaryLink);
            var videoUrl = CreateVideoUrl(responce);
            return videoUrl;
        }

        private string GetPartDownloadLink(string link)
        {
            var downloadLink = _htmlWeb.Load(link).GetElementbyId("download-button")?.GetAttributeValue("href", "");
            return ConcatenateValue(downloadLink);
        }

        private string GetTemporaryLink(string link)
        {
            var temporaryLink = _htmlWeb.Load(link).
               GetElementbyId("dl-button").
               Element("a").
               GetAttributeValue("href", "");
            return ConcatenateValue(temporaryLink);
        }

        private string CreateVideoUrl(string responce)
        {
            var header = responce.Split('/');
            var data = header[3];
            var catalog = header[4];
            var numberCatalog = header[5];
            var name = header[6];
            var season = header[7];
            var token = header[8].Replace("\r\nContent-Type: text", string.Empty);
            var videoUrl = $"http://{data}/{catalog}/{numberCatalog}/{name}/{season}/{token}";
            return videoUrl;
        }

        /// <summary>
        /// Данный метод используется для получения ссылки при переадресации
        /// </summary>
        /// <returns></returns>
        private string GetLinkFromRequest(string url)
        {
            try
            {
                var link = _webRequest.ExecuteRequest(url);
                return link.ContentType;
            }
            catch (WebException ex)
            {
                return ex.Response.Headers.ToString();
            }
        }

        private string ConcatenateValue(string partUrl)
        {
            var result = _tsKg + partUrl;
            return result;
        }
    }
}
