using System.Net;
using HtmlAgilityPack;
using static System.String;
using Microsoft.Extensions.Configuration;
using ParserApi.Services.HttpWebRequestService;
using ParserApi.Services.LinksToVideosService.Contract;

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
            var link = Concat(_tsKg, url);
            var downloadLink = GetPartDownloadLink(link);
            var temporaryLink = GetTemporaryLink(downloadLink);
            var responce = GetLinkFromRequest(temporaryLink);
            var videoUrl = CreateVideoUrl(responce);
            return videoUrl;
        }

        private string GetPartDownloadLink(string link)
        {
            var partLink = _htmlWeb.Load(link).GetElementbyId("download-button")?.GetAttributeValue("href", "");
            return Concat(_tsKg, partLink);
        }

        private string GetTemporaryLink(string link)
        {
            var temporaryLink = _htmlWeb.Load(link).GetElementbyId("dl-button").Element("a").GetAttributeValue("href", "");
            return Concat(_tsKg, temporaryLink);
        }

        private string CreateVideoUrl(string responce)
        {
            responce = responce.Substring(173);
            responce = responce.Remove(responce.Length - 84);
            return responce;
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
    }
}
