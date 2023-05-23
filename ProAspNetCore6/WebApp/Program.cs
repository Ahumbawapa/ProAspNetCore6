using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using System.Text.Json;

//=> Creating a Web Service Using a Controller

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DataContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:ProductConnection"]);
    opts.EnableSensitiveDataLogging(true);
});


var app = builder.Build();

#region Individuelle Endpunkte für einen Webdienst
const string BASEURL = "api/products";
app.MapGet($"{BASEURL}/{{id}}"
    , async (HttpContext context, DataContext data) =>
    {
        string? id = context.Request.RouteValues["id"] as string;
        if(null != id)
        {
            Product? p = data.Products.Find(long.Parse(id));
            
            if (null == p)
            {
                context.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(
                    JsonSerializer.Serialize<Product>(p)
                    ); 
            }
        }
    });


app.MapGet(BASEURL, async (HttpContext context, DataContext data) =>
        {
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(
                JsonSerializer.Serialize<IEnumerable<Product>>(data.Products)
                );
        }

    );

app.MapPost(BASEURL, async (HttpContext context, DataContext data) =>
    { 
        Product? p = await JsonSerializer.DeserializeAsync<Product>(context.Request.Body);

        if (null != p)
        {
            await data.AddAsync(p);
            await data.SaveChangesAsync();
            context.Response.StatusCode = StatusCodes.Status200OK;
        }
    }
);

//PowerShell - Kommando für POST 
//PS C:\Users\KK> Invoke-RestMethod http://localhost:5000/api/products -Method POST -Body (@{Name = "Swimming Goggles"; Price=12.75; CategoryId=1; SupplierId=1} | ConvertTo-Json) -ContentType "application/json"
//Individuelle Endpunkte für einen Webdienst

#endregion

app.UseMiddleware<WebApp.TestMiddleware>();

app.MapGet("/", () => "Hello World!");

var context = app.Services.CreateScope()
    .ServiceProvider.GetRequiredService<DataContext>();

SeedData.SeedDataBase(context);


app.Run();
