using Microsoft.AspNetCore.Mvc;
using AddressLookUp.Aggregator.Api.Services;
using Api.Common.Contracts;

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
        [ProducesResponseType(typeof(AddressLookUpResult), 200)]
        public async Task<IActionResult> GetAddressLookUpAsync(string address, [FromQuery]string? servicelist)
        {

            var result = await _addressLookUpService.GetAddressLookUpAsync(address, servicelist);
            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }
    }
}
