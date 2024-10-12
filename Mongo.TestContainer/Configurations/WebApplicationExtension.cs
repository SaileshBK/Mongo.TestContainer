using System.Diagnostics;

namespace Mongo.TestContainer.Configurations;

public static class WebApplicationExtension
{
    public static void ConfigureWebApplication(this WebApplication webApplication)
    {
        webApplication.UseCors("AllowSpecificOrigin");
        webApplication.UseAuthorization();
        webApplication.UseStaticFiles();
        webApplication.MapCustomEndpoints();
        webApplication.UseExceptionHandler("/error");
        webApplication.MapControllers();
        webApplication.Run();
    }

    public static void MapCustomEndpoints(this WebApplication webApplication)
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
}
