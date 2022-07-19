using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Api.Common.DataProvider;
using ReverseDnsWorker.Api.Models;
using ReverseDnsWorker.Api.Services;

namespace ReverseDnsWorker.Api.Tests.Services;

public class ReverseDnsLookUpServiceTests
{
    private readonly ReverseDnsLookUpService _reverseDnsLookUpService;
    private readonly Mock<IDataProviderService> _dataProviderService;

    public ReverseDnsLookUpServiceTests()
    {
        _dataProviderService = new Mock<IDataProviderService>();
        _reverseDnsLookUpService = new ReverseDnsLookUpService(new ReverseDnsApiOptions { lookUpUrl = string.Empty }, _dataProviderService.Object);
    }

    [Fact]
    public async Task GetResultAsync_should_return_result_for_valid_address()
    {
        _dataProviderService.Setup(e => e.GetResultAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult("test result"));
        var result = await _reverseDnsLookUpService.GetReverseDnsLookUpResultAsync("8.8.8.8");
        Assert.NotNull(result);
        Assert.True(result.IsSuccess);
    }

    [Fact]
    public async Task GetResultAsync_should_not_return_result_for_invalid_address()
    {
        _dataProviderService.Setup(e => e.GetResultAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<string>(string.Empty));
        var result = await _reverseDnsLookUpService.GetReverseDnsLookUpResultAsync("8888");
        Assert.NotNull(result);
        Assert.False(result.IsSuccess);
    }
}
