using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext _dataContext;

        public HomeController(DataContext dataContext)
        { 
            _dataContext = dataContext;
        }

        public async Task<IActionResult> Index(long id = 1)
        {
            ViewBag.AveragePrice = await _dataContext.Products.AverageAsync(p => p.Price);
            return View(await _dataContext.Products.FindAsync(id));
        }

        public IActionResult List()
        {
            return View(_dataContext.Products);
        }

        public IActionResult Html()
        {
            //Muss hier als object gecastet werden, da sonst ein View mit dem Namen gesucht würde
            return View((object)"This is a <h3><i>string</i></h3>");
        }
    }
}
