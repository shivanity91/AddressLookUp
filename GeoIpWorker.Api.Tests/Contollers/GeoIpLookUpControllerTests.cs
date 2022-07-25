using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Api.Common.Contracts;
using GeoIpWorker.Api.Services;
using GeoIpWorker.Api.Controllers;
using System.Threading;

namespace GeoIpWorker.Api.Tests.Controllers;

public class GeoIpLookUpControllerTests
{
    private readonly Mock<IGeoIpLookUpService> _mockIGeoIpLookUpService;
    private readonly GeoIpLookUpController _geoIpLookUpController;

    public GeoIpLookUpControllerTests()
    {
        _mockIGeoIpLookUpService = new Mock<IGeoIpLookUpService>();
        _geoIpLookUpController = new GeoIpLookUpController(_mockIGeoIpLookUpService.Object);
    }

    [Fact]
    public async Task GetResultAsync_should_return_success_for_valid_address()
    {
        _geoIpLookUpController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        var geoIpWorkerResult = new GeoIpLookUpResult { IsSuccess = true, Result = "Sample GeoIp Address Info" };

        _mockIGeoIpLookUpService.Setup(e => e.GetGeoIpLookUpResultAsync(It.IsAny<string>(), default)).Returns(Task.FromResult(geoIpWorkerResult));
        var result = await _geoIpLookUpController.GetGeoIpLookUpResultAsync("test.com") as OkObjectResult;
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        Assert.NotNull(result);
        var resultValue = result.Value as GeoIpLookUpResult;
        Assert.True(resultValue.IsSuccess);
    }

    [Fact]
    public async Task GetResultAsync_should_return_Error_for_invalid_address()
    {
        _geoIpLookUpController.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };

        var geoIpWorkerResult = new GeoIpLookUpResult { IsSuccess = false, Result = "Not a valid address" };

        _mockIGeoIpLookUpService.Setup(e => e.GetGeoIpLookUpResultAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(geoIpWorkerResult));
        var result = await _geoIpLookUpController.GetGeoIpLookUpResultAsync("abcde") as OkObjectResult;
        Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        Assert.NotNull(result);
        var resultValue = result.Value as GeoIpLookUpResult;
        Assert.False(resultValue.IsSuccess);
    }
}
