using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Api.Common.Contracts;
using ReverseDnsWorker.Api.Services;
using ReverseDnsWorker.Api.Controllers;

namespace PingWorker.Api.Tests.Controllers
{
    public class ReverseDnsLookUpControllerTests
    {
        private readonly Mock<IReverseDnsLookUpService> _mockIReverseDnsLookUpService;
        private readonly ReverseDnsLookUpController _reverseDnsLookUpController;

        public ReverseDnsLookUpControllerTests()
        {
            _mockIReverseDnsLookUpService = new Mock<IReverseDnsLookUpService>();
            _reverseDnsLookUpController = new ReverseDnsLookUpController(_mockIReverseDnsLookUpService.Object);
        }

        [Fact]
        public async Task GetResultAsync_should_return_success_for_valid_address()
        {
            _reverseDnsLookUpController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            var reverseDnsWorkerResult = new ReverseDnsLookUpResult { IsSuccess = true, Result = "Sample Reverse DNS Address Info" };

            _mockIReverseDnsLookUpService.Setup(e => e.GetReverseDnsLookUpResultAsync(It.IsAny<string>(), default)).Returns(Task.FromResult(reverseDnsWorkerResult));
            var result = await _reverseDnsLookUpController.GetReverseDnsLookUpResultAsync("test.com") as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result);
            var resultValue = result.Value as ReverseDnsLookUpResult;
            Assert.True(resultValue.IsSuccess);
        }

        [Fact]
        public async Task GetResultAsync_should_return_Error_for_invalid_address()
        {
            _reverseDnsLookUpController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext()
            };

            var reverseDnsWorkerResult = new ReverseDnsLookUpResult{ IsSuccess = false, Result = "Not a valid address" };

            _mockIReverseDnsLookUpService.Setup(e => e.GetReverseDnsLookUpResultAsync(It.IsAny<string>(), default)).Returns(Task.FromResult(reverseDnsWorkerResult));
            var result = await _reverseDnsLookUpController.GetReverseDnsLookUpResultAsync("abcde") as OkObjectResult;
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
            Assert.NotNull(result);
            var resultValue = result.Value as ReverseDnsLookUpResult;
            Assert.False(resultValue.IsSuccess);
        }
    }
}
