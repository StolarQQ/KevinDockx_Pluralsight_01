using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CityInfo.Api.Models;
using CityInfo.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CitiesController : Controller
    {
        private readonly ICityInfoRepository _cityInfoRepository;

        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }
        [HttpGet]
        public IActionResult GetCities()
        {

            var cities = _cityInfoRepository.GetCities();
            //var results = new List<CityWithoutPointsOfInterestDto>();

            //foreach (var city in cities)
            //{
            //    results.Add(new CityWithoutPointsOfInterestDto
            //    {
            //        Id = city.Id,
            //        Name = city.Name,
            //        Description = city.Description
            //    });
            //}

            var results = Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(cities);

            return Ok(results);

        }

        [HttpGet("{id}")]
        public IActionResult GetCityById(int id, bool includePointOofInterest = false)
        {
            //    var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == id);
            //    if (cityToReturn == null)
            //    {
            //        return NotFound();
            //    }
            //    return Ok(cityToReturn);

            var city = _cityInfoRepository.GetCity(id, includePointOofInterest);
            if (city == null)
            {
                NotFound($"City with id '{id}' was not found");
            }

            if (includePointOofInterest)
            {
                var cityResult = Mapper.Map<CityDto>(city);

                //foreach (var pointOfInterest in city.PointsOfInterest)
                //{
                //    cityResult.PointsOfInterest.Add(new PointOfInterestDto
                //    {
                //        Id = pointOfInterest.Id,
                //        Name = pointOfInterest.Name,
                //        Description = pointOfInterest.Description
                //    });
                //}

                return Ok(cityResult);
            }
            else
            {
                //var resultCityWithoutPointsOfInterestDto = new CityWithoutPointsOfInterestDto
                //{
                //    Id = city.Id,
                //    Name = city.Name,
                //    Description = city.Description
                //};

                var resultCityWithoutPointsOfInterestDto = Mapper.Map<CityWithoutPointsOfInterestDto>(city);

                return Ok(resultCityWithoutPointsOfInterestDto);
            }
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