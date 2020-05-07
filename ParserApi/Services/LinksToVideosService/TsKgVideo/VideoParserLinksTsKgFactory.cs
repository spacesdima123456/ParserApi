using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using ParserApi.Services.HttpWebRequestService;
using ParserApi.Services.LinksToVideosService.Contract;
using ParserApi.Services.LinksToVideosService.TsKgVideo.Contracy;

namespace ParserApi.Services.LinksToVideosService.TsKgVideo
{
    public class VideoParserLinksTsKgFactory : IVideoParserLinksTsKgFactory
    {
        private readonly HtmlWeb _htmlWeb;
        private readonly IConfiguration _configuration;
        private readonly IHttpWebRequest _webReques
            ;
        public VideoParserLinksTsKgFactory( IConfiguration configuration,
                                            IHttpWebRequest webReques,
                                            HtmlWeb htmlWeb)
        {
            _htmlWeb = htmlWeb;
            _configuration = configuration;
            _webReques = webReques;
        }
        public IVideoLinks Create()
        {
            return new VideoParserLinksTsKg(_configuration, _webReques, _htmlWeb);
        }
    }
}
