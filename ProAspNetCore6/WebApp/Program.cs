using Microsoft.EntityFrameworkCore;
using WebApp.Models;
//using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

//-> Understanding Content Formatting

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging(true);
});

builder.Services.AddControllers().AddNewtonsoftJson().AddXmlDataContractSerializerFormatters();

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.Configure<MvcNewtonsoftJsonOptions>(opts => {
    opts.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;

});

//Add support for cross-origin requests (CORS)
builder.Services.AddCors();

// Definiert die Dienste, die für die Verwendung von Controllers gebraucht werden
builder.Services.AddControllers();
//builder.Services.Configure<JsonOptions>(opts => {
//    opts.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
//}); 


var app = builder.Build();

app.MapControllers(); // Definiert die Routen bei Verwendung von Controllern

app.UseMiddleware<WebApp.TestMiddleware>();

app.MapGet("/", () => "Hello World!");

var context = app.Services.CreateScope()
    .ServiceProvider.GetRequiredService<DataContext>();

SeedData.SeedDataBase(context);


app.Run();
