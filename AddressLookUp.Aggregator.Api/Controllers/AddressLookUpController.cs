using Microsoft.AspNetCore.Mvc;
using System.Net;
using AddressLookUp.Aggregator.Api.Services;
using Api.Common.Contracts;
using Api.Common.Validation;

namespace AddressLookUp.Aggregator.Api.Controllers;

[ApiController]
[Route("api/addresslookup")]
public class AddressLookUpController : Controller
{
    private readonly IAddressLookUpService _addressLookUpService;

    public AddressLookUpController(IAddressLookUpService addressLookUpService)
    {
        _addressLookUpService = addressLookUpService;
    }

    /// <summary>
    /// Aggregated API to fetch address lookup from respective services
    /// </summary>
    /// <param name="address">Required: IP Address or Domain Name</param>
    /// <param name="servicelist">Optional: services to call for address lookup</param>
    /// <returns></returns>
    [HttpGet("{address}", Name = "GetAddressLookUpAsync")]
    [ProducesResponseType(typeof(AddressLookUpResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationErrorModel), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAddressLookUpAsync(string address, [FromQuery] string? servicelist)
    {
        if (!AddressValidator.IsAddressValid(address))
        {
            return BadRequest(new ValidationErrorModel("Validation Failed", "Invalid Address"));
        }

        var result = await _addressLookUpService.GetAddressLookUpAsync(address, servicelist, Request.HttpContext.RequestAborted);
        if (result is null)
        {
            return NotFound(new ValidationErrorModel("Empty response", "No data found"));
        }

        return Ok(result);
    }
}

