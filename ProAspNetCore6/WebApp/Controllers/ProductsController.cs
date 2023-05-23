using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
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

        [HttpGet("{id}")] // Bearbeitet Anfragen mit URL-Muster api/products/{id}
        // Dependencies that are declared by action methods must be decorated with the FromServices attribute
        public async Task<Product?> GetProduct(long id, [FromServices] ILogger<ProductsController> logger)
        {
            logger.LogDebug("GetProduct Action invoked");
            return await context.Products.FindAsync(id);
        }

        [HttpPost] // can process POST-Request
        public async Task SaveProduct([FromBody] Product product, [FromServices] ILogger<ProductsController> logger) // value obtained from Request-Body
        {
            logger.LogInformation("SaveProduct invoked");
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
        }

        [HttpPut]
        public async Task UpdateProduct([FromBody] Product product)
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
    }
}
