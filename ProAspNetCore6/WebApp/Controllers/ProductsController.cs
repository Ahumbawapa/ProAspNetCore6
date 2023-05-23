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
        public IEnumerable<Product> GetProducts()
        {
            return context.Products;
        }

        [HttpGet("{id}")] // Bearbeitet Anfragen mit URL-Muster api/products/{id}
        // Dependencies that are declared by action methods must be decorated with the FromServices attribute
        public Product? GetProduct(long id, [FromServices] ILogger<ProductsController> logger)
        {
            logger.LogDebug("GetProduct Action invoked");
            return context.Products.Find(id);
        }

        [HttpPost] // can process POST-Request
        public void SaveProduct([FromBody] Product product, [FromServices] ILogger<ProductsController> logger) // value obtained from Request-Body
        {
            logger.LogInformation("SaveProduct invoked");
            context.Products.Add(product);
            context.SaveChanges();
        }
    }
}
