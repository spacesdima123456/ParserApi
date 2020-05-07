using System.Text;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using ParserApi.Services.ParserServic.Contract;
using ParserApi.Services.LinksToVideosService.Contract;

namespace ParserApi.Services.LinksToVideosService.OsKgVideo
{
    public class VideoParserLinksOcKg: IVideoLinks
    {
        private readonly ParserBase _parser;
        private readonly IConfiguration _configuration;
    
        public VideoParserLinksOcKg(ParserBase parser, IConfiguration configuration)
        {
            _parser = parser;
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
            var html = _parser.ToParse(temporaryLink);
            var videoUrl = GetVideoUrl(html);
            var proxy = ConcatenateValueByKey(videoUrl,"Proxy");
            return proxy;
        }

        private string GetLicenseByUrl(string url)
        {
            var apiUrl = ConcatenateValueByKey(url, "OsKgUrlApi");
            var responce = _parser.ToParse(apiUrl).Trim();
            var license = GetLicenseByJson(responce);
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

        private string GetLicenseByJson(string json)
        {
            var jsonValid = json.Replace("JsHttpRequest.dataReady(", "").Replace(")", "");
            var license = (string)JObject.Parse(jsonValid).SelectToken("js[0].response.movie.files[0].links.license");
            return license;
        }
    }
}
