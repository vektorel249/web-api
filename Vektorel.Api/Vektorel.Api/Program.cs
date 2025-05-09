
using Vektorel.Api.Extensions;
using Vektorel.Api.Middlewares;

namespace Vektorel.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        builder.Services.AddControllers();
        builder.Services.AddNorthwind(builder.Configuration);
        builder.Services.AddDevelopmentCors();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("Development");
        }

        app.UseHttpsRedirection();
        app.UseMiddleware<AccessCheckMiddleware>();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
