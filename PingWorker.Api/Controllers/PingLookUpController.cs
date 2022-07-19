using Microsoft.AspNetCore.Mvc;
using System.Net;
using PingWorker.Api.Services;
using Api.Common.Contracts;
using Api.Common.Validation;


namespace PingWorker.Api.Controllers;

[Produces("application/json")]
[Route("api/ping")]
public class PingLookUpController : Controller
{
    private readonly IPingLookUpService _pingLookUpService;

    public PingLookUpController(IPingLookUpService pingLookUpService)
    {
        _pingLookUpService = pingLookUpService;
    }

    /// <summary>
    /// To fetch address lookup through Ping
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    [HttpGet("{address}")]
    [ProducesResponseType(typeof(PingLookUpResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationErrorModel), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetPingLookUpResultAsync(string address)
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

        var result = await _pingLookUpService.GetPingLookUpResultAsync(address);
        return Ok(result);
    }
}
