using Microsoft.AspNetCore.Mvc;
using Api.Common.Contracts;
using RdapWorker.Api.Services;
using Api.Common.Validation;
using System.Net;

namespace RdapWorker.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/rdap")]
    public class RdapLookUpController : Controller
    {
        private readonly IRdapLookUpService _rdapLookUpService;

        public RdapLookUpController(IRdapLookUpService rdapLookUpService)
        {
            _rdapLookUpService = rdapLookUpService;
        }


        [HttpGet("{address}/{addresstype}")]
        [ProducesResponseType(typeof(RdapLookUpResult), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationErrorModel), (int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetRdapLookUpResultAsync(string address, string addressType)
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

            var result = await _rdapLookUpService.GetRdapLookUpResultAsync(address, addressType);
            return Ok(result);
        }
    }
}
