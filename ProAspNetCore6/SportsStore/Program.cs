//Das builder-Objekt wird verwendet, um Objekte (services) zu instanzieren
//und per Dependency Injection bereit zu stellen
var builder = WebApplication.CreateBuilder(args);

//Stellt geteilte Objekte f�r das MVC Framework bereit
builder.Services.AddControllersWithViews();

var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.UseStaticFiles(); // stellt Unterst�tzung f�r statische Seiten in wwwroot bereit

//Registriert das MVC - Framework als Quelle von Endpunkten mit
//Standard-Mapping Einstellungen
app.MapDefaultControllerRoute();

app.Run();
