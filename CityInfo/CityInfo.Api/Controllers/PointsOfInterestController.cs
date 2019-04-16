using System;
using System.Collections.Generic;
using AutoMapper;
using CityInfo.Api.Entities;
using CityInfo.Api.Models;
using CityInfo.Api.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CityInfo.Api.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly ICityInfoRepository _cityInfoRepository;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService mailService, ICityInfoRepository cityInfoRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet("{cityid}/pointsofinterest")]
        public IActionResult GetPointOfInterest(int cityid)
        {
            try
            {
                var city = _cityInfoRepository.CityExists(cityid);
                if (city == false)
                {
                    _logger.LogInformation($"City with id '{cityid}' was not found");
                    return NotFound("City not exist");
                }

                var pointsOfInterest = _cityInfoRepository.GetPointsOfInterestForCity(cityid);
                var result = Mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterest);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception while getting points of interest for city with id {cityid}.", ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }

        [HttpGet("{cityid}/pointsofinterest/{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityid, int id)
        {
            if (!_cityInfoRepository.CityExists(cityid))
            {
                return NotFound();
            }

            var poi = _cityInfoRepository.GetPointOfInterestForCity(cityid, id);
            if (poi == null)
            {
                return NotFound();
            }

            var result = Mapper.Map<PointOfInterestDto>(poi);

            return Ok(result);
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
            
            if (!_cityInfoRepository.CityExists(cityid))
            {
                return NotFound();
            }
            

            // Manual mapping, future we can use AutoMapper
            //var finalPointOfInterest = new PointOfInterestDto
            //{
            //    Id = ++maxPointOfInterest,
            //    Name = pointOfInterest.Name,
            //    Description = pointOfInterest.Description
            //};

            var finalPointOfInterest = Mapper.Map<PointOfInterest>(pointOfInterest);

            _cityInfoRepository.AddPointOfInterestForCity(cityid, finalPointOfInterest);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request");
            }

            var createdPointOfInterestToReturn = Mapper.Map<PointOfInterestDto>(finalPointOfInterest);

            // Return a response with location header
            return CreatedAtRoute("GetPointOfInterest", new
            { cityid = cityid, id = createdPointOfInterestToReturn.Id }, createdPointOfInterestToReturn);
        }

        [HttpPut("{cityid}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityid, int id, [FromBody]PointOfInterestUpdateDto pointOfInterestUpdate)
        {
            if (pointOfInterestUpdate == null)
            {
                return BadRequest();
            }
            
            if (!_cityInfoRepository.CityExists(cityid))
            {
                return NotFound();
            }

            var pointOfInterest = _cityInfoRepository.GetPointOfInterestForCity(cityid, id);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            Mapper.Map(pointOfInterestUpdate, pointOfInterest);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request");
            }

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

            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterest = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = Mapper.Map<PointOfInterestUpdateDto>(pointOfInterest);

            // Apply data from json documents
            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);
            TryValidateModel(pointOfInterestToPatch);

            // In case of JsonPatchDocument, we should manually validate model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(pointOfInterestToPatch, pointOfInterest);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request");
            }

            return NoContent();
        }

        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult RemovePointOfInterest(int cityId, int id)
        {
            if (!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterest = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            _cityInfoRepository.DeletePointOfInterest(pointOfInterest);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request");
            }

            _mailService.Send($"Point deleted", $"Point with {id} id was deleted");

            return NoContent();
        }
    }
}