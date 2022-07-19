using Microsoft.AspNetCore.Mvc;
using Api.Common.Contracts;
using PingWorker.Api.Services;
using System.Net;
using Api.Common.Validation;

namespace PingWorker.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/ping")]
    public class PingLookUpController : Controller
    {
        private readonly IPingLookUpService _pingLookUpService;

        public PingLookUpController(IPingLookUpService pingLookUpService)
        {
            _pingLookUpService = pingLookUpService;
        }


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
}
