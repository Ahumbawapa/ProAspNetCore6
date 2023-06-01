using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Configuration;
using WebApp.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;

//-> Working with Layout

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>( opts => {
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging();
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options => {
    options.Cookie.IsEssential = true;
});
builder.Services.Configure<RazorPagesOptions>(opts => {
    opts.Conventions.AddPageRoute("/Index", "/extra/page/{id:long?}");
});

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();
app.MapControllers();
app.MapRazorPages();
//app.MapControllerRoute("Default",
//    "{controller=Home}/{action=Index}/{id?}");

app.MapDefaultControllerRoute();

var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
SeedData.SeedDataBase(context);
app.Run();