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
        public async Task<IActionResult> GetCities()
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

        //[HttpPost]
        //public void PostCity([FromBody] CityDto city)
        //{
        //    var cityExist = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == city.Id);
        //    if (cityExist != null)
        //    {
        //        throw new Exception($"City with '{city.Id}' already exist");
        //    }

        //    CitiesDataStore.Current.Cities.Add(cityExist);
        //}
    }
}