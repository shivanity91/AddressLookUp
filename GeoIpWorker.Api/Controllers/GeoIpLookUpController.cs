using Api.Common.Contracts;
using GeoIpWorker.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeoIpWorker.Api.Controllers
{

    [Produces("application/json")]
    [Route("api/geoip")]
    public class GeoIpLookUpController : Controller
    {
        private readonly IGeoIpLookUpService _geoIpLookUpService;

        public GeoIpLookUpController(IGeoIpLookUpService geoIpLookUpService)
        {
            _geoIpLookUpService = geoIpLookUpService;
        }

        [ProducesResponseType(typeof(GeoIpLookUpResult), 200)]
        [HttpGet("{address}")]
        public async Task<IActionResult> GetPingLookUpResultAsync(string address)
        {
            var result = await _geoIpLookUpService.GetGeoIpLookUpResultAsync(address);
            return Ok(result);
        }
    }
}

