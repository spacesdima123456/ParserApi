using HtmlAgilityPack;
using static System.String;
using Microsoft.Extensions.Configuration;
using ParserApi.Services.LinksToVideosService.Contract;

namespace ParserApi.Services.LinksToVideosService.TsKgVideo
{
    public class VideoParserLinksTsKg : IVideoLinks
    {
        private readonly string _tsKg;
        private readonly HtmlWeb _htmlWeb;

        public VideoParserLinksTsKg(IConfiguration configuration, HtmlWeb htmlWeb)
        {
            _htmlWeb = htmlWeb;
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
            return GetVideoUrl(link);
        }

        private string GetVideoUrl(string url)
        {
            var downloadUrl = _tsKg +  _htmlWeb.Load(url).GetElementbyId("download-button")?.GetAttributeValue("href", "");
            var tokenUrl = _tsKg + _htmlWeb.Load(downloadUrl).GetElementbyId("dl-button").Element("a").GetAttributeValue("href", "");
            var videoUrl = _htmlWeb.Load(tokenUrl, "GET").DocumentNode.SelectSingleNode("//body/a").InnerText.Replace("amp;", Empty);
            return videoUrl;
        }
    }
}
