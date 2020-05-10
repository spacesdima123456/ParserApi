using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using ParserApi.Services.LinksToVideosService.Contract;
using ParserApi.Services.LinksToVideosService.TsKgVideo.Contracy;

namespace ParserApi.Services.LinksToVideosService.TsKgVideo
{
    public class VideoParserLinksTsKgFactory : IVideoParserLinksTsKgFactory
    {
        private readonly HtmlWeb _htmlWeb;
        private readonly IConfiguration _configuration;

        public VideoParserLinksTsKgFactory( IConfiguration configuration, HtmlWeb htmlWeb)
        {
            _htmlWeb = htmlWeb;
            _configuration = configuration;
        }
        public IVideoLinks Create()
        {
            return new VideoParserLinksTsKg(_configuration, _htmlWeb);
        }
    }
}
