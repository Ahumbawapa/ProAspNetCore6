var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Anwort der Http-Anfrage anpassen
app.Use(async (context, next) =>
{
    await next(); // zur nächsten Middleware-Komponente weiterreichen
    await context.Response.WriteAsync($"\nStatus Code: {context.Response.StatusCode}");
});

//Kurzschließen der Request Pipeline
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/short")
    {
        //ohne Aufruf von await next() wird hier direkt die Antwort gestartet
        await context.Response.WriteAsync($"Request short circuited!");
        
    }
    else
    { 
        await next();
    }
});


//Eigene Middleware wird durch app.Use eingebunden
app.Use(async (context, next) =>
{

    if (context.Request.Method == HttpMethods.Get
         && context.Request.Query["custom"] == "true")
    {
        // wird ausgeführt bei Aufruf http://localhost:5000/?custom=true

        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("Custom middleware \n");
    }
    await next(); // pass the request to the next component in the middleware pipeline
});

app.UseMiddleware<Chap12_Platform.QueryStringMiddleWare>();

app.MapGet("/", () => "Hello World!");

app.Run();
