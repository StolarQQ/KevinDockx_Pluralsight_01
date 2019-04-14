using System.Linq;
using CityInfo.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        [HttpGet("{cityid}/pointsofinterest")]
        public IActionResult GetPointOfInterest(int cityid)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityid);
            if (city == null)
                return NotFound();

            return Ok(city.PointsOfInterest);
        }

        [HttpGet("{cityid}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityid, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityid);
            if (city == null)
                return NotFound();

            var specifyPointOfInterest = city.PointsOfInterest.FirstOrDefault(x => x.Id == id);
            if (specifyPointOfInterest == null)
                return NotFound();

            return Ok(specifyPointOfInterest);
        }

        [HttpPost("{cityid}/pointsofinterest")]
        public IActionResult CreatePostPointOfInterest(int cityid, [FromBody] PointOfInterestCreationDto pointOfInterest)
        {
            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityid);

            if (city == null)
            {
                return NotFound();
            }

            var maxPointOfIneresId = CitiesDataStore.Current.Cities.SelectMany
                (x => x.PointsOfInterest).Max(p => p.Id);

            // Manual mapping, future we can use AutoMapper
            var finalPointOfInterest = new PointOfInterestDto
            {
                Id = ++maxPointOfIneresId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(finalPointOfInterest);

            // Return a response with location header
            return CreatedAtRoute("GetPointOfInterest", new
            { cityid = cityid, id = finalPointOfInterest.Id }, finalPointOfInterest);
        }
    }
}