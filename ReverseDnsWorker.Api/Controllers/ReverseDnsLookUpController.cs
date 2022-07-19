using Microsoft.AspNetCore.Mvc;
using Api.Common.Contracts;
using ReverseDnsWorker.Api.Services;
using System.Net;
using Api.Common.Validation;

namespace ReverseDnsWorker.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/reversedns")]
    public class ReverseDnsLookUpController : Controller
    {
        private readonly IReverseDnsLookUpService _reverseDnsLookUpService;

        public ReverseDnsLookUpController(IReverseDnsLookUpService reverseDnsLookUpService)
        {
            _reverseDnsLookUpService = reverseDnsLookUpService;
        }


        [HttpGet("{address}")]
        [ProducesResponseType(typeof(ReverseDnsLookUpResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationErrorModel), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetReverseDnsLookUpResultAsync(string address)
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

            var result = await _reverseDnsLookUpService.GetReverseDnsLookUpResultAsync(address);
            return Ok(result);
        }
    }
}
