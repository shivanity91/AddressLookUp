using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Api.Common.Contracts;
using RdapWorker.Api.Controllers;
using RdapWorker.Api.Services;

namespace RdapWorker.Api.Tests.Controllers;

public class RdapLookUpControllerTests
{
    private readonly Mock<IRdapLookUpService> _mockIRdapLookUpService;
    private readonly RdapLookUpController _rdapLookUpController;

    public RdapLookUpControllerTests()
    {
        _mockIRdapLookUpService = new Mock<IRdapLookUpService>();
        _rdapLookUpController = new RdapLookUpController(_mockIRdapLookUpService.Object);
    }

    [Fact]
    public async Task GetResultAsync_should_return_success_for_valid_address()
    {
        _rdapLookUpController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        var rdapWorkerResult = new RdapLookUpResult { IsSuccess = true, Result = "Sample Rdap Address Info" };

        _mockIRdapLookUpService.Setup(e => e.GetRdapLookUpResultAsync(It.IsAny<string>(), It.IsAny<string>(), default)).Returns(Task.FromResult(rdapWorkerResult));
        var result = await _rdapLookUpController.GetRdapLookUpResultAsync("test.com","Domain") as OkObjectResult;
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        Assert.NotNull(result);
        var resultValue = result.Value as RdapLookUpResult;
        Assert.True(resultValue.IsSuccess);
    }

    [Fact]
    public async Task GetResultAsync_should_return_Error_for_invalid_address()
    {
        _rdapLookUpController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        var rdapWorkerResult = new RdapLookUpResult { IsSuccess = false, Result = "Not a valid address" };

        _mockIRdapLookUpService.Setup(e => e.GetRdapLookUpResultAsync(It.IsAny<string>(), It.IsAny<string>(), default)).Returns(Task.FromResult(rdapWorkerResult));
        var result = await _rdapLookUpController.GetRdapLookUpResultAsync("abcde","Domain") as OkObjectResult;
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        Assert.NotNull(result);
        var resultValue = result.Value as RdapLookUpResult;
        Assert.False(resultValue.IsSuccess);
    }
}
