using System.Linq;
using CityInfo.Api.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace CityInfo.Api.Controllers
{
    [ApiController]
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

            // We can add custom modelstate rules, but we need using manual model valid
            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError(string.Empty, "Name have to be different from the description");
            }

            //If controller got attribute[ApiController], Asp Core validate model automatic.
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

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

        [HttpPut("{cityid}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityid, int id, [FromBody]PointOfInterestUpdateDto pointOfInterestUpdate)
        {
            if (pointOfInterestUpdate == null)
            {
                return BadRequest();
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityid);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(x => x.Id == id);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            pointOfInterest.Name = pointOfInterestUpdate.Name;
            pointOfInterest.Description = pointOfInterestUpdate.Description;

            return NoContent();
        }

        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id,
               [FromBody] JsonPatchDocument<PointOfInterestUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(c => c.Id == id);
            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch =
                new PointOfInterestUpdateDto()
                {
                    Name = pointOfInterestFromStore.Name,
                    Description = pointOfInterestFromStore.Description
                };
            
            // Apply data from json documents
            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

            TryValidateModel(pointOfInterestToPatch);

            // In case of JsonPatchDocument, we should manually validate model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult RemovePointOfInterest(int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(x => x.Id == id);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            city.PointsOfInterest.Remove(pointOfInterest);
       
            return NoContent();
        }
    }
}