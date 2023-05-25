using Microsoft.EntityFrameworkCore;
using WebApp.Models;

//-> Using Action Results

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging(true);
});

//Add support for cross-origin requests (CORS)
builder.Services.AddCors();

// Definiert die Dienste, die für die Verwendung von Controllers gebraucht werden
builder.Services.AddControllers();


var app = builder.Build();

app.MapControllers(); // Definiert die Routen bei Verwendung von Controllern

app.UseMiddleware<WebApp.TestMiddleware>();

app.MapGet("/", () => "Hello World!");

var context = app.Services.CreateScope()
    .ServiceProvider.GetRequiredService<DataContext>();

SeedData.SeedDataBase(context);


app.Run();
