using Microsoft.AspNetCore.Mvc;
using AddressLookUp.Aggregator.Api.Services;
using Api.Common.Contracts;
using Api.Common.Validation;
using System.Net;

namespace AddressLookUp.Aggregator.Api.Controllers
{
    [ApiController]
    [Route("api/lookup")]
    public class AddressLookUpController : Controller
    {
        private readonly IAddressLookUpService _addressLookUpService;

        public AddressLookUpController(IAddressLookUpService addressLookUpService)
        {
            _addressLookUpService = addressLookUpService;
        }

        [HttpGet("{address}", Name = "GetAddressLookUpAsync")]
        [ProducesResponseType(typeof(AddressLookUpResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationErrorModel), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(ValidationErrorModel), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetAddressLookUpAsync(string address, [FromQuery]string? servicelist)
        {
            if (!AddressValidator.IsAddressValid(address))
            {
                return BadRequest(new ValidationErrorModel("Validation Failed", "Invalid Address"));
            }

            var result = await _addressLookUpService.GetAddressLookUpAsync(address, servicelist);
            if (result is null)
            {
                return NotFound(new ValidationErrorModel("Empty response", "No data found"));
            }

            return Ok(result);
        }
    }
}
