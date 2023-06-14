namespace WebApp.Models
{
    public class CitiesData
    {
        private List<City> cities = new List<City>
        {
            new City { Name = "London", Country = "UK", Population = 8_359_000 },
            new City { Name = "New York", Country = "USA", Population = 8_406_000},
            new City { Name = "San Jose", Country = "USA", Population = 998_537},
            new City { Name = "Paris", Country = "France", Population = 2_244_000}
        };

        public IEnumerable<City> Cities => cities;

        public void AddCity(City newCity)
        { 
            cities.Add(newCity);
        }
    }
}
