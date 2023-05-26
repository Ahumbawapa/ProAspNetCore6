using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    [ApiController] // -> [FromBody], [ModelState.IsValid] sind eingeschlossen
    [Route("api/[controller]")] // URL des Controllers -> /api/products
    public class ProductsController : ControllerBase
    {
        private DataContext context;
        public ProductsController(DataContext ctx) { context = ctx; }

        
        [HttpGet] // Behandelt Get - Anfragen
        public IAsyncEnumerable<Product> GetProducts()
        {
            return context.Products.AsAsyncEnumerable();
        }

        //[HttpGet("{id}")] // Bearbeitet Anfragen mit URL-Muster api/products/{id}
        //// Dependencies that are declared by action methods must be decorated with the FromServices attribute
        //public async Task<Product?> GetProduct(long id, [FromServices] ILogger<ProductsController> logger)
        //{
        //    logger.LogDebug("GetProduct Action invoked");
        //    return await context.Products.FindAsync(id);
        //}

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(long id) 
        {
            Product? p = await context.Products.FindAsync(id);
            if (null == p)
            {
                return NotFound();
            }
            else
            {
                return Ok(p);
            }
        }


        //[HttpPost] // can process POST-Request
        //public async Task SaveProduct([FromBody] ProductBindingTarget target, [FromServices] ILogger<ProductsController> logger) // value obtained from Request-Body
        //{
        //    logger.LogInformation("SaveProduct invoked");
        //    //await context.Products.AddAsync(product);
        //    // prevent overloading
        //    await context.Products.AddAsync(target.ToProduct());
        //    await context.SaveChangesAsync();
        //}

        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductBindingTarget target) 
        {
            Product p = target.ToProduct();
            await context.Products.AddAsync(p);
            await context.SaveChangesAsync();
            return Ok(p);
        }

        [HttpPut]
        public async Task UpdateProduct(Product product)
        { 
            context.Products.Update(product);
            await context.SaveChangesAsync();
        }

        [HttpDelete("{id}")]
        public async Task DeleteProduct(long id) 
        {
            context.Products.Remove(new Product { ProductId = id });
            await context.SaveChangesAsync();
        }

        [HttpGet("redirect")]
        public IActionResult Redirect()
        {
            // Redirection to URL
            //return Redirect("/api/products/1");

            // Redirection to Action
            //return RedirectToAction(nameof(GetProduct), new { Id = 1 });

            return RedirectToRoute(new { controller = "Products", action = "GetProduct", Id = 1 });
        }

    }
}
