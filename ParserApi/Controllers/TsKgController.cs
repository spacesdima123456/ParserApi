using Microsoft.AspNetCore.Mvc;
using ParserApi.Services.LinksToVideosService.TsKgVideo.Contracy;

namespace ParserApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TsKgController : ControllerBase
    {
        private readonly IVideoParserLinksTsKgFactory _videoParser;

        public TsKgController(IVideoParserLinksTsKgFactory videoParser)
        {
            _videoParser = videoParser;
        }

        [HttpGet("{name}&{season}&{series}")]
        public ActionResult<string> GetLinkMovie(string name, int season, int series)
        {
            var url = ($"/show/{name}/{season}/{series}");
            var link = _videoParser.Create().GetLinkMovie(url);
            if (link != null)
            {
                return Redirect(link);
            }
            else
            {
                return "Выбранный фильм не найден";
            }
        }

    }
}
    
