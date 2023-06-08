using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Models;

namespace WebApp.Pages
{
    public class IndexModel : PageModel
    {
        private DataContext context;

        public Product? Product { get; set; }

        public IndexModel(DataContext ctx)
        {
            context = ctx;
        }

        // Razor Page handler verwenden implizit das IActionResult Interface
        // durch �nderung explizite R�ckgabe von IActionResult
        public async Task<IActionResult> OnGetAsync(long id = 1)
        {
            Product = await context.Products.FindAsync(id);
            if (Product == null)
            {
                return RedirectToPage("NotFound");
            }
            
            return Page();
        }
    }
}
