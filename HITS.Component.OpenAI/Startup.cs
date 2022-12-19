using MudBlazor.Services;

namespace HITS.Component.OpenAI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMudServices();
        }
    }
}