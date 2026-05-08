using RetailSystem.Application.Interfaces.Repositories;
using RetailSystem.Application.Interfaces.Services;
using RetailSystem.Application.Interfaces.UnitOfWork;
using RetailSystem.Application.Mappers;
using RetailSystem.Application.Services;
using RetailSystem.Infrastructure.Repositories;
using RetailSystem.Infrastructure.Services;
using RetailSystem.Infrastructure.UnitOfWork;

namespace RetailSystem.API.Configs
{
    public static class AddLifeCycles
    {
        public static void ConfigureLifeCycles(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductImageRepository, ProductImageRepository>();
            services.AddScoped<IProductVariantRepository, ProductVariantRepository>();
            services.AddScoped<IColorRepository, ColorRepository>();
            services.AddScoped<ISizeRepository, SizeRepository>();

            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();

            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(typeof(ProductMappingProfile));
        }
    }
}
