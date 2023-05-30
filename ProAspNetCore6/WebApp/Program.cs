using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Configuration;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>( opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging();
});

builder.Services.AddControllers();
var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();

var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
SeedData.SeedDataBase(context);
app.Run();