using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.ViewModels;

namespace SportsStore.Controllers
{
    public class HomeController : Controller
    {
        private IStoreRepository _repository;
        public int PageSize = 4; //Anzahl der Produkte pro Seite

        public HomeController(IStoreRepository repository)
        {
            _repository = repository;
        }

        public IActionResult Index(int productPage = 1)
        =>
            View(new ProductsListViewModel()
            {
                Products = _repository.Products
                    .OrderBy(p => p.ProductID)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                
                PagingInfo = new PagingInfo()
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Products.Count()
                }

            });
    }
}
