using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ParserApi.Expansions;
using ParserApi.Services.HttpWebRequestService;
using ParserApi.Services.LinksToVideosService.Contract;
using ParserApi.Services.LinksToVideosService.OsKgVideo;
using ParserApi.Services.LinksToVideosService.TsKgVideo;
using ParserApi.Services.LinksToVideosService.TsKgVideo.Contracy;

namespace ParserApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IHttpWebRequest>(sp=>
                new HttpWebRequest (Configuration["UserAgent"]));

            services.SettingsHtmlWeb();
            services.AddSingleton<IOcKgVideoFactory, OcKgVideoParserFactory>();
            services.AddSingleton<IVideoParserLinksTsKgFactory, VideoParserLinksTsKgFactory>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
