

namespace WebApplication8
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IElasticClient>(a =>
            //{
            //    var settings = new ConnectionSettings(new Uri("http://localhost:9200"));
            //    return new ElasticClient(settings);
            //}

            //);

            //services.AddSingleton<ElasticSearch>();
        }

        public void Configure(IApplicationBuilder app)
        {

        }

    }
}
