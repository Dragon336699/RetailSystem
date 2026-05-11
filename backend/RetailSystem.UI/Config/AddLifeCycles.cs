using RetailSystem.UI.Services.ApiClients;
using RetailSystem.UI.Services.Intefaces;

namespace RetailSystem.UI.Config
{
    public static class AddLifeCycles
    {
        public static void ConfigureLifeCycles(this IServiceCollection services)
        {
            services.AddScoped<IProductApiClient, ProductApiClient>();
            services.AddScoped<ICategoryApiClient, CategoryApiClient>();
            services.AddScoped<ISizeApiClient, SizeApiClient>();
        }
    }
}
