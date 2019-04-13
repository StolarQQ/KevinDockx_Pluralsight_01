using System.Linq;
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

        [HttpGet("{cityid}/pointsofinterest/{id}")]
        public IActionResult GetPointOfInterest(int cityid, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityid);
            if (city == null)
                return NotFound();

            var specifyPointOfInterest = city.PointsOfInterest.FirstOrDefault(x => x.Id == id);
            if(specifyPointOfInterest == null)
                return NotFound();

            return Ok(specifyPointOfInterest);
        }
    }
}