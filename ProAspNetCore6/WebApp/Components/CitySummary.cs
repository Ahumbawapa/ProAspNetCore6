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

        public string Invoke()
        {
            return $"{_cityData.Cities.Count()} cities, "
                + $"{_cityData.Cities.Sum(c => c.Population)} people";
        }
    }
}
