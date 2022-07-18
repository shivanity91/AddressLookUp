using Microsoft.AspNetCore.Mvc;
using Api.Common.Contracts;
using ReverseDnsWorker.Api.Services;

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

        [ProducesResponseType(typeof(ReverseDnsLookUpResult), 200)]
        [HttpGet("{address}")]
        public async Task<IActionResult> GetReverseDnsLookUpResultAsync(string address)
        {
            var result = await _reverseDnsLookUpService.GetReverseDnsLookUpResultAsync(address);
            return Ok(result);
        }
    }
}
