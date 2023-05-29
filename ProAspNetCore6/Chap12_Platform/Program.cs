using Microsoft.Extensions.Options;
using Chap12_Platform;

var builder = WebApplication.CreateBuilder(args);

// Verwendung des Options-Pattern
builder.Services.Configure<MessageOptions>(options => {
    options.CityName = "Albany";
});

var app = builder.Build();

app.MapGet("/location", async (HttpContext context, IOptions<MessageOptions> msgOpts) =>
{
    Chap12_Platform.MessageOptions opts = msgOpts.Value;
    await context.Response.WriteAsync($"{opts.CityName}, {opts.CountryName}");

});

app.MapGet("/", () => "Hello, World!");
app.Run();