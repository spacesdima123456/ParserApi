using Microsoft.Extensions.Configuration;
using ParserApi.Services.HttpWebRequestService;
using ParserApi.Services.LinksToVideosService.Contract;

namespace ParserApi.Services.LinksToVideosService.OsKgVideo
{
    public class OcKgVideoParserFactory : IOcKgVideoFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpWebRequest _webRequest;

        public OcKgVideoParserFactory(IHttpWebRequest webRequest, IConfiguration configuration)
        {
            _webRequest = webRequest;
            _configuration = configuration;
        }

        public VideoParserLinksOcKg Create()
        {
            return new VideoParserLinksOcKg(_webRequest, _configuration);
        }
    }
}
