using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.Api.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.Api.Controllers
{
    public class DummyController : Controller
    {
        private CityInfoContext _cityInfoContext;

        public DummyController(CityInfoContext cityInfoContext)
        {
            _cityInfoContext = cityInfoContext;
        }

        [HttpGet]
        [Route("api/database")]
        public IActionResult TestDatabase()
        {
            return Ok();
        }
    }
}