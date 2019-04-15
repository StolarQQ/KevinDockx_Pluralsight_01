using System;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.Api;
using CityInfo.Api.Models;
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

        [HttpPost]
        public IActionResult PostCity([FromBody] CityDto city)
        {
            if (city == null)
            {
                return BadRequest();
            }
            
            var cityExist = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == city.Id);
            if (cityExist != null)
            {
               // throw new Exception($"City with '{city.Id}' already exist");
                return StatusCode(400, $"City with '{city}' already exist");
            }

            CitiesDataStore.Current.Cities.Add(city);

            return NoContent();
        }
    }
}