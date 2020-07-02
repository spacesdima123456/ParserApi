using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace ParserApi.Expansions
{
    public static class HtmlWebOptions
    {
        public static void SettingsHtmlWeb(this IServiceCollection services)
        {
            var htmlWeb = new HtmlWeb();
            htmlWeb.OverrideEncoding = Encoding.Default;
            services.AddSingleton(sp => htmlWeb); 
        }
    }
}
