using Microsoft.AspNetCore.Mvc;
using Api.Common.Contracts;
using RdapWorker.Api.Services;

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

        [ProducesResponseType(typeof(RdapLookUpResult), 200)]
        [HttpGet("{address}/{addresstype}")]
        public async Task<IActionResult> GetRdapLookUpResultAsync(string address, string addressType)
        {
            var result = await _rdapLookUpService.GetRdapLookUpResultAsync(address, addressType);
            return Ok(result);
        }
    }
}
