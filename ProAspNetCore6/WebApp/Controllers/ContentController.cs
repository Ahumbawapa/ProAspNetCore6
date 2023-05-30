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

        [HttpGet("object/{format?}")] // z.B. http://localhost:5000/api/content/object/json
        [FormatFilter]
        [Produces("application/json", "application/xml")]
        public async Task<ProductBindingTarget> GetObject()
        {
            Product p = await context.Products.FirstAsync();

            return new ProductBindingTarget()
            {
                Name = p.Name,
                Price = p.Price,
                CategoryId = p.CategoryId,
                SupplierId = p.SupplierId
            };
        }

        [HttpPost]
        [Consumes("application/json")] //restricting the datatype to be handled
        public string SaveProductJson(ProductBindingTarget product) 
        {
            return $"JSON: {product.Name}";
        }

        [HttpPost]
        [Consumes("application/xml")]
        public string SaveProductXml(ProductBindingTarget product) 
        {
            return $"XML: {product.Name}";
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
