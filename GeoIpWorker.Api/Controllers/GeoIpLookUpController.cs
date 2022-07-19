using Microsoft.AspNetCore.Mvc;
using System.Net;
using Api.Common.Contracts;
using Api.Common.Validation;
using GeoIpWorker.Api.Services;

namespace GeoIpWorker.Api.Controllers;

[Produces("application/json")]
[Route("api/geoip")]
public class GeoIpLookUpController : Controller
{
    private readonly IGeoIpLookUpService _geoIpLookUpService;

    public GeoIpLookUpController(IGeoIpLookUpService geoIpLookUpService)
    {
        _geoIpLookUpService = geoIpLookUpService;
    }

    /// <summary>
    /// To fetch address lookup through GeoIP
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    [HttpGet("{address}")]
    [ProducesResponseType(typeof(GeoIpLookUpResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationErrorModel), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetGeoIpLookUpResultAsync(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            return BadRequest(new ValidationErrorModel("Validation Failed", new[] { new Error("Get Address LookUp Results", "address cannot be empty") }));
        }

        var isValidAddress = AddressValidator.IsAddressValid(address);
        if (!isValidAddress)
        {
            return BadRequest(new ValidationErrorModel("Validation Failed", "Invalid Address"));
        }

        var result = await _geoIpLookUpService.GetGeoIpLookUpResultAsync(address);
        return Ok(result);
    }
}

