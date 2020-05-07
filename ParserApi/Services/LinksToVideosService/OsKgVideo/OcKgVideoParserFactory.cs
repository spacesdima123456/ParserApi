using Microsoft.Extensions.Configuration;
using ParserApi.Services.LinksToVideosService.Contract;
using ParserApi.Services.ParserServic.Contract;

namespace ParserApi.Services.LinksToVideosService.OsKgVideo
{
    public class OcKgVideoParserFactory : IOcKgVideoFactory
    {
        private readonly ParserBase _parser;
        private readonly IConfiguration _configuration;

        public OcKgVideoParserFactory(ParserBase parser, IConfiguration configuration)
        {
            _parser = parser;
            _configuration = configuration;
        }

        public VideoParserLinksOcKg Create()
        {
            return new VideoParserLinksOcKg(_parser, _configuration);
        }
    }
}
