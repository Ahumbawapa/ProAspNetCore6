using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController]
    [Route("/api/{Controller}")]
    public class ContentController : ControllerBase
    {
        private DataContext context;

        public ContentController(DataContext dataContext)
        {
            context = dataContext;
        }

        [HttpGet("string")]
        public string GetString() => "This is a string response";

        [HttpGet("object")]
        public async Task<Product> GetObject()
        {
            return await context.Products.FirstAsync();
        }
    }
}

/*
PS E:\work\ProAspNetCore6\ProAspNetCore6> Invoke-WebRequest http://localhost:5000/api/content/string | select @{n='Content-Type'; e={$_.Headers."Content-Type"}}, Content
Content-Type              Content
------------              -------
text/plain; charset=utf-8 This is a string response            

PS E:\work\ProAspNetCore6\ProAspNetCore6> Invoke-WebRequest http://localhost:5000/api/content/object | select @{n='Content-Type'; e={$_.Headers."Content-Type"}}, Content

Content-Type                    Content
------------                    -------
application/json; charset=utf-8 {"productId":1,"name":"Kayak","price":275.00,"categoryId":1,"supplierId":1}


PS E:\work\ProAspNetCore6\ProAspNetCore6> Invoke-WebRequest http://localhost:5000/api/content/object -Headers @{Accept="application/xml"}| select @{n='Content-Type'; e={$_.Headers."Content-Type"}}, Content

Content-Type                    Content
------------                    -------
application/json; charset=utf-8 {"productId":1,"name":"Kayak","price":275.00,"categoryId":1,"supplierId":1} 
 */
