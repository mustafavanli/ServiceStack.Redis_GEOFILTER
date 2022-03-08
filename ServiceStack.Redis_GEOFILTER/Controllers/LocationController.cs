using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Redis_GEOFILTER.Models;
using ServiceStack.Redis_GEOFILTER.Service;

namespace ServiceStack.Redis_GEOFILTER.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        LocationService locationService;
        public LocationController(LocationService _locationService)
        {
            locationService = _locationService;
        }

        [HttpPost("Add")]
        public IActionResult Add(Location location)
        {
            locationService.LocationAdd(location);
            return Ok();
        }

        [HttpPost("FindGeo")]
        public IActionResult FindGeo(FindGeo findGeo)
        {
            // Country : "TR","EN","FR" key in find geo
            return Ok(locationService.Any(findGeo));
        }
    }
}
