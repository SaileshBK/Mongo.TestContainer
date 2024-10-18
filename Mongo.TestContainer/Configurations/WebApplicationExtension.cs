using System.Diagnostics;
using Mongo.TestContainer.Services.Interfaces;

namespace Mongo.TestContainer.Configurations;

internal static class WebApplicationExtension
{
    public static async Task ConfigureWebApplication(this WebApplication app)
    {
        app.UseCors("AllowSpecificOrigin");
        app.UseAuthorization();
        app.UseStaticFiles();
        app.MapCustomEndpoints();
        app.UseExceptionHandler("/error");
        app.MapControllers();
        await SeedMongoData(app);
        app.UseSwagger();
        app.Run();
    }

    private static void MapCustomEndpoints(this WebApplication app)
    {
        app.Map("/main", map =>
        {
            map.Run(async context =>
            {
                await context.Response.WriteAsync("Started");
            });
        });

        if (Debugger.IsAttached)
        {
            app.Map("/shutdown", (IHostApplicationLifetime appLifetime) =>
            {
                appLifetime.StopApplication();
            });
        }
    }

    private static async Task SeedMongoData(WebApplication app)
    {
        var dataSeeder = app.Services.GetRequiredService<IDataSeeder>();
        await dataSeeder.SeedDataAsync();
    }

    private static void UseSwagger(this WebApplication app)
    {
        app.UseSwagger(options =>
        {
            options.SerializeAsV2 = true;
        });

        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = "swagger";
        });
    }
}
