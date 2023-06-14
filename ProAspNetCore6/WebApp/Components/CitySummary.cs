using Microsoft.AspNetCore.Mvc;
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

        public IViewComponentResult Invoke()
        {
            CityViewModel cvm = new CityViewModel 
            { 
                Cities = _cityData.Cities.Count(),
                Population = _cityData.Cities.Sum(c => c.Population)
            };
            return View(cvm);
        }
    }
}
