
using Mongo.TestContainer.Configurations;

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureServices();
builder.Build().ConfigureWebApplication();