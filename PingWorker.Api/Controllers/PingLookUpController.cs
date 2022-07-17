using Microsoft.AspNetCore.Mvc;
using Api.Common.Contracts;
using PingWorker.Api.Services;

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

        [ProducesResponseType(typeof(PingLookUpResult), 200)]
        [HttpGet("{address}")]
        public async Task<IActionResult> GetPingLookUpResultAsync(string address)
        {
            var result = await _pingLookUpService.GetPingLookUpResultAsync(address);
            return Ok(result);
        }
    }
}
