using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RetailSystem.API.Common.Middlewares;
using RetailSystem.API.Configs;
using RetailSystem.Application.Interfaces.Seeder;
using RetailSystem.Infrastructure.Data;
using RetailSystem.Infrastructure.Persistence.Seed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Configure OpenAPI/Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.ConfigureLifeCycles();

builder.Services.AddDbContext<RetailSystemDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Enable middleware to serve generated Swagger as a JSON endpoint and the Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(options => options
    .WithOrigins("http://localhost:5173", "https://localhost:44301")
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowCredentials()
);

using (var scope = app.Services.CreateScope())
{
    var seeders = scope.ServiceProvider.GetServices<IDataSeeder>();

    foreach (var seeder in seeders)
    {
        await seeder.SeedAsync();
    }
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
