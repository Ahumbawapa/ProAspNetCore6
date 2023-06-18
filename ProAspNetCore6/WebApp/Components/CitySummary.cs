using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using WebApp.Models;

namespace WebApp.Components
{
    public class CitySummary : ViewComponent
    { 
        private CitiesData _cityData;

        public CitySummary(CitiesData cityData)
        {
            _cityData = cityData;
        }

        //public IViewComponentResult Invoke()
        //{
        //    CityViewModel cvm = new CityViewModel
        //    {
        //        Cities = _cityData.Cities.Count(),
        //        Population = _cityData.Cities.Sum(c => c.Population)
        //    };
        //    return View(cvm);
        //}

        public IViewComponentResult Invoke()
        {
            // Razor-Ansicht zurückgeben /Views/Shared/Components/CitySummary/Default.cshtml
            //    CityViewModel cvm = new CityViewModel
            //    {
            //        Cities = _cityData.Cities.Count(),
            //        Population = _cityData.Cities.Sum(c => c.Population)
            //    };
            //    return View(cvm);

            // Html- codierten String zurückgeben
            //return Content("This is a <h3><i>string</i><h3>");

            //Interpretierten HTML - String zurückgeben
            return new HtmlContentViewComponentResult(new HtmlString("This is a <h3><i>string</i><h3>"));

        }

    }
}
