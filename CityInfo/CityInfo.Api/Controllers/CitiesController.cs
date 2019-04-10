using System.Linq;
using Microsoft.AspNetCore.Mvc;


namespace CityInfo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : Controller
    {
        [HttpGet]
        public IActionResult GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCityById(int id)
        {
            var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == id);
            if (cityToReturn == null)
            {
                return NotFound();
            }
            return Ok(cityToReturn);
        }
    }
}