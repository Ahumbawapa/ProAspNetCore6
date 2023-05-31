using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Configuration;
using WebApp.Models;

//-> Selecting a View by Name
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>( opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging();
});

builder.Services.AddControllersWithViews();
var app = builder.Build();

app.UseStaticFiles();
app.MapControllers();
//app.MapControllerRoute("Default",
//    "{controller=Home}/{action=Index}/{id?}");

app.MapDefaultControllerRoute();

var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
SeedData.SeedDataBase(context);
app.Run();