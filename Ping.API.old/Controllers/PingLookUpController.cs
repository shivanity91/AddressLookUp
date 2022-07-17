using Api.Common.Contracts;
using Microsoft.AspNetCore.Mvc;
using Ping.Api.Services;

namespace Ping.Api.Controllers
{
    public class PingLookUpController : Controller
    {
        private readonly IPingLookUpService _pingLookUpService;

        public PingLookUpController(IPingLookUpService pingLookUpService)
        {
            _pingLookUpService = pingLookUpService;
        }

        [ProducesResponseType(typeof(PingLookUpResult), 200)]
        [HttpGet("{address}")]
        public async Task<IActionResult> GetPingLookUpAsync(string address)
        {
            var result = await _pingLookUpService.GetPingLookUpResultAsync(address);
            return Ok(result);
        }
    }
}
