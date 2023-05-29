using Microsoft.Extensions.Options;
using Chap12_Platform;

var builder = WebApplication.CreateBuilder(args);

// Verwendung des Options-Pattern
builder.Services.Configure<MessageOptions>(options => {
    options.CityName = "Kassel";
    options.CountryName = "Germany";
});

var app = builder.Build();

//app.MapGet("/location", async (HttpContext context, IOptions<MessageOptions> msgOpts) =>
//{
//    Chap12_Platform.MessageOptions opts = msgOpts.Value;
//    await context.Response.WriteAsync($"{opts.CityName}, {opts.CountryName}");

//});

// Bei Aufruf wird der Konstruktor von LocationsMiddleWare untersucht und der Options-Parameter
// auf Grund der Configure-Anweisung an den Konstruktor übergeben
app.UseMiddleware<LocationMiddleWare>();

app.MapGet("/", () => "Hello, World!");
app.Run();