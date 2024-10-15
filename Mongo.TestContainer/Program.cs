using Mongo.TestContainer.Configurations;

var builder = WebApplication.CreateBuilder(args);
var configurationBuilder = builder.Configuration;
configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
await builder.Services.ConfigureServices(configurationBuilder);
await builder.Build().ConfigureWebApplication();