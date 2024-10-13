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
        app.Run();
    }

    private static void MapCustomEndpoints(this WebApplication webApplication)
    {
        webApplication.Map("/main", map =>
        {
            map.Run(async context =>
            {
                await context.Response.WriteAsync("Started");
            });
        });

        if (Debugger.IsAttached)
        {
            webApplication.Map("/shutdown", (IHostApplicationLifetime appLifetime) =>
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
}
