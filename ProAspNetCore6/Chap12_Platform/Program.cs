var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Eigene Middleware wird durch app.Use eingebunden
app.Use(async (context, next) =>
{

    if (context.Request.Method == HttpMethods.Get
         && context.Request.Query["custom"] == "true")
    {
        // wird ausgeführt bei Aufruf http://localhost:5000/?custom=true

        string message = $"Request.Header: {context.Request.Headers.ToString()}";
        message += $"\nCustom middleware";


        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync(message);
    }
    await next(); // pass the request to the next component in the middleware pipeline
});

app.MapGet("/", () => "Hello World!");

app.Run();
