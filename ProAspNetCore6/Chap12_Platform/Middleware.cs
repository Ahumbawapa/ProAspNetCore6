using Microsoft.Extensions.Options;

namespace Chap12_Platform
{
    public class QueryStringMiddleWare
    {
        private RequestDelegate? next;

        public QueryStringMiddleWare()
        {
            //do nothing
        }

        public QueryStringMiddleWare(RequestDelegate nextDelegate) 
        {
            next = nextDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method == HttpMethods.Get
                && context.Request.Query["custom"] == "true")
            {
                if (!context.Response.HasStarted)
                {
                    context.Response.ContentType = "text/plain";
                }

                await context.Response.WriteAsync("Class-based middleware \n");
            }


            if (null != next)
            {
                await next(context);
            }
        }
    }

    public class LocationMiddleWare 
    {
        private RequestDelegate next;
        private MessageOptions options;

        public LocationMiddleWare(RequestDelegate nextDelegate,
            IOptions<MessageOptions> opts)
        {
            next = nextDelegate;
            options = opts.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/location")
            {
                await context.Response.WriteAsync($"{options.CityName},{options.CountryName}");
            }
            else
            {
                await next(context);
            }
        
        }
    }
}
