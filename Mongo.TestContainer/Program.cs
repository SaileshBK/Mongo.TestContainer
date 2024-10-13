
using Mongo.TestContainer.Configurations;

var builder = WebApplication.CreateBuilder(args);
await builder.Services.ConfigureServices();
builder.Build().ConfigureWebApplication();