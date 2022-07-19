using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Api.Common.Contracts;
using DomainAvailabilityWorker.Api.Services;
using DomainAvailabilityWorker.Api.Controllers;

namespace DomainAvailabilityWorker.Api.Tests.Controllers
{
    public class DomainAvailabilityControllerTests
    {
        private readonly Mock<IDomainAvailabilityService> _mockIDomainLookUpService;
        private readonly DomainAvailabilityWorkerController _domainLookUpController;

        public DomainAvailabilityControllerTests()
        {
            _mockIDomainLookUpService = new Mock<IDomainAvailabilityService>();
            _domainLookUpController = new DomainAvailabilityWorkerController(_mockIDomainLookUpService.Object);
        }

        [Fact]
        public async Task GetResultAsync_should_return_success_for_valid_address()
        {
            _domainLookUpController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            var pingWorkerResult = new DomainAvailabilityResult { IsSuccess = true, Result = "Sample Domain Availability Info" };

            _mockIDomainLookUpService.Setup(e => e.GetDomainAvailabilityLookUpResultAsync(It.IsAny<string>(), default)).Returns(Task.FromResult(pingWorkerResult));
            var result = await _domainLookUpController.GetDomainAvailabilityLookUpResultAsync("google.com") as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result);
            var resultValue = result.Value as DomainAvailabilityResult;
            Assert.True(resultValue.IsSuccess);
        }

        [Fact]
        public async Task GetResultAsync_should_return_Error_for_invalid_address()
        {
            _domainLookUpController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            var pingWorkerResult = new DomainAvailabilityResult { IsSuccess = false, Result = "Not a valid address" };

            _mockIDomainLookUpService.Setup(e => e.GetDomainAvailabilityLookUpResultAsync(It.IsAny<string>(), default)).Returns(Task.FromResult(pingWorkerResult));
            var result = await _domainLookUpController.GetDomainAvailabilityLookUpResultAsync("abcde") as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result);
            var resultValue = result.Value as DomainAvailabilityResult;
            Assert.False(resultValue.IsSuccess);
        }
    }
}
