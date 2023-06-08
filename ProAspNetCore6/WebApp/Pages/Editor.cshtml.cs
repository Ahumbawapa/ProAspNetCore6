using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Pages
{
    public class EditorModel : PageModel
    {
        private readonly DataContext _dataContext;

        public Product? Product { get; set; }

        public EditorModel(DataContext dataContext)
        { 
            _dataContext = dataContext;
        }

       

        public async Task OnGetAsync(long id)
        {
            Product = await _dataContext.FindAsync<Product>(id);
        }

        public async Task<IActionResult> OnPostAsync(long id, decimal price)
        {
            Product? p = await _dataContext.Products.FindAsync(id);

            if (p != null)
            { 
                p.Price = price;
            }

            await _dataContext.SaveChangesAsync();

            return RedirectToPage();
        }
    }
}
