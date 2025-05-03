using Microsoft.EntityFrameworkCore;
using Vektorel.Api.Common;
using Vektorel.Api.Context;

namespace Vektorel.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddNorthwind(this IServiceCollection services, ConfigurationManager configuration)
    {
        // appSettings.Development.json dosyasında ilgili alanı class'a çevir
        var databaseOptions = configuration.GetSection(nameof(DatabaseOptions)).Get<DatabaseOptions>();
        services.AddDbContext<NorthwindContext>(options => 
        {
            options.UseSqlServer(databaseOptions.NorthwindConnection);
        });
    }

    public static void AddDevelopmentCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("Development", policy =>
            {
                //policy.AllowAnyHeader().WithOrigins("http://localhost:5173").WithMethods("GET");
                policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
            });
        });
    }
}
