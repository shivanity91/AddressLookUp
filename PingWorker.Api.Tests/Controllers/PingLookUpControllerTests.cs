using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Api.Common.Contracts;
using PingWorker.Api.Services;
using PingWorker.Api.Controllers;

namespace PingWorker.Api.Tests.Controllers;

public class PingLookUpControllerTests
{
    private readonly Mock<IPingLookUpService> _mockIPingLookUpService;
    private readonly PingLookUpController _pingLookUpController;

    public PingLookUpControllerTests()
    {
        _mockIPingLookUpService = new Mock<IPingLookUpService>();
        _pingLookUpController = new PingLookUpController(_mockIPingLookUpService.Object);
    }

    [Fact]
    public async Task GetResultAsync_should_return_success_for_valid_address()
    {
        _pingLookUpController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        var pingWorkerResult = new PingLookUpResult { IsSuccess = true, Result = "Sample Ping Address Info" };

        _mockIPingLookUpService.Setup(e => e.GetPingLookUpResultAsync(It.IsAny<string>(), default)).Returns(Task.FromResult(pingWorkerResult));
        var result = await _pingLookUpController.GetPingLookUpResultAsync("google.com") as OkObjectResult;
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        Assert.NotNull(result);
        var resultValue = result.Value as PingLookUpResult;
        Assert.True(resultValue.IsSuccess);
    }

    [Fact]
    public async Task GetResultAsync_should_return_Error_for_invalid_address()
    {
        _pingLookUpController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        var pingWorkerResult = new PingLookUpResult { IsSuccess = false, Result = "Not a valid address" };

        _mockIPingLookUpService.Setup(e => e.GetPingLookUpResultAsync(It.IsAny<string>(), default)).Returns(Task.FromResult(pingWorkerResult));
        var result = await _pingLookUpController.GetPingLookUpResultAsync("abcde") as OkObjectResult;
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        Assert.NotNull(result);
        var resultValue = result.Value as PingLookUpResult;
        Assert.False(resultValue.IsSuccess);
    }
}
