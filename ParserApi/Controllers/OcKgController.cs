using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ParserApi.Services.LinksToVideosService.Contract;

namespace ParserApi.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    public class OcKgController : ControllerBase
    {
        private readonly IOcKgVideoFactory _videoLinks;

        public OcKgController(IOcKgVideoFactory videoLinks)
        {
            _videoLinks = videoLinks;
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetLinkMovieById(string id)
        {
            var url =  _videoLinks.Create().GetLinkMovie(id);
            if (url!=null)
            {
                return Redirect(url);
            }
            else 
            {
                return "Выбранный фильм не найден";
            }
        }

    }
}