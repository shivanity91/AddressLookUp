using Api.Common.Contracts;
using Api.Common.Validation;
using DomainAvailabilityWorker.Api.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DomainAvailabilityWorker.Api.Controllers;

[Produces("application/json")]
[Route("api/domain")]
public class DomainAvailabilityWorkerController : Controller
{
    private readonly IDomainAvailabilityService _domainAvailabilityService;

    public DomainAvailabilityWorkerController(IDomainAvailabilityService domainAvailabilityService)
    {
        _domainAvailabilityService = domainAvailabilityService;
    }

    /// <summary>
    /// To check Domain Availability
    /// </summary>
    /// <param name="address"></param>
    /// <returns></returns>
    [HttpGet("{address}")]
    [ProducesResponseType(typeof(DomainAvailabilityResult), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ValidationErrorModel), (int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetDomainAvailabilityLookUpResultAsync(string address)
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

        var result = await _domainAvailabilityService.GetDomainAvailabilityLookUpResultAsync(address);
        return Ok(result);
    }
}
