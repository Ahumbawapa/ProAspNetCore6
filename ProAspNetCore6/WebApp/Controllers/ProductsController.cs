using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")] // URL des Controllers -> /api/products
    public class ProductsController : ControllerBase
    {
        // wichtige Eigenschaften / Methoden von ControllerBase
        // die die aktuelle Anfrage beschreiben
        // HttpContext
        // ModelState
        // Request
        // Response
        // RouteData
        // User

        [HttpGet] // Behandelt Get - Anfragen
        public IEnumerable<Product> GetProducts()
        {
            return new Product[] { 
                new Product() { Name = "Product #1"},
                new Product() { Name = "Product #2"}
            };
        }

        [HttpGet("{id}")] // Bearbeitet Anfragen mit URL-Muster api/products/{id}
        public Product GetProduct()
        {
            return new Product() { ProductId = 1, Name = "Test Product"};
        }
    }
}
