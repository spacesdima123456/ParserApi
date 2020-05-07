using HtmlAgilityPack;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ParserApi.Services.HttpWebRequestService;
using ParserApi.Services.LinksToVideosService.Contract;
using ParserApi.Services.LinksToVideosService.OsKgVideo;
using ParserApi.Services.LinksToVideosService.TsKgVideo;
using ParserApi.Services.LinksToVideosService.TsKgVideo.Contracy;
using ParserApi.Services.ParserServic;
using ParserApi.Services.ParserServic.Contract;

namespace ParserApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSingleton<IHttpWebRequest>(sp=>
                new HttpWebRequest (Configuration["UserAgent"]));
            services.AddSingleton<ParserBase, ParserHtml>();
            services.AddSingleton<HtmlWeb>();
            services.AddSingleton<IOcKgVideoFactory, OcKgVideoParserFactory>();
            services.AddSingleton<IVideoParserLinksTsKgFactory, VideoParserLinksTsKgFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
