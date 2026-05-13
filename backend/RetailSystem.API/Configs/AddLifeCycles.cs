using RetailSystem.Application.Interfaces.Repositories;
using RetailSystem.Application.Interfaces.Seeder;
using RetailSystem.Application.Interfaces.Services;
using RetailSystem.Application.Interfaces.UnitOfWork;
using RetailSystem.Application.Mappers;
using RetailSystem.Application.Services;
using RetailSystem.Infrastructure.Persistence.Seed;
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
            services.AddScoped<ISizeRepository, SizeRepository>();

            services.AddScoped<ISizeService, SizeService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();

            services.AddScoped<IDataSeeder, RoleSeeder>();
            services.AddScoped<IDataSeeder, UserSeeder>();

            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(typeof(ProductMappingProfile));
            services.AddScoped<RoleSeeder>();
        }
    }
}
