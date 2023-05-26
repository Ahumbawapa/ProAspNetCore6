//-> Adding the Tag Helper Class


using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

//Das builder-Objekt wird verwendet, um Objekte (services) zu instanzieren
//und per Dependency Injection bereit zu stellen
var builder = WebApplication.CreateBuilder(args);

//Stellt geteilte Objekte für das MVC Framework bereit
builder.Services.AddControllersWithViews();

//EFCore muss konfiguriert werden, so dass der Datenbank-Typ
//und der ConnectionString sowie die Context-Klasse bekannt ist
builder.Services.AddDbContext<StoreDbContext>(opts =>
{
    opts.UseSqlServer( // SqlServer wird verwendet
        // Die IConfiguration-Schnittstelle bietet Zugriff auf das 
        // AspNetCore - Konfigurationssystem in appsettings.json
        // über das der ConnectionString ausgelesen werden kann
        builder.Configuration["ConnectionStrings:SportsStoreConnection"]
        );
});

//Sorgt dafür, dass bei jedem Request ein Objekt der Klasse EFStoreRepository 
//erzeugt wird, wo eine IStoreRepository - Instanz benötigt wird
builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.UseStaticFiles(); // stellt Unterstützung für statische Seiten in wwwroot bereit


app.MapControllerRoute(
      "pagination"
    , "Products/Page{productPage}"
    , new { Controller = "Home", action = "Index" }
    );

//Registriert das MVC - Framework als Quelle von Endpunkten mit
//Standard-Mapping Einstellungen
app.MapDefaultControllerRoute();

//Aufruf der statischen Seed-Methode
SeedData.EnsurePopulated(app);

app.Run();
