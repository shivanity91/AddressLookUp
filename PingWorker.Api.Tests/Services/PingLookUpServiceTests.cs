using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using PingWorker.Api.Models;
using PingWorker.Api.Services;
using Api.Common.DataProvider;

namespace PingWorker.Api.Tests.Services
{
    public class PingLookUpServiceTests
    {
        private readonly PingLookUpService _pingLookUpService;
        private readonly Mock<IDataProviderService> _dataProviderService;

        public PingLookUpServiceTests()
        {
            _dataProviderService = new Mock<IDataProviderService>();
            _pingLookUpService = new PingLookUpService(new PingApiOptions { lookUpUrl = string.Empty }, _dataProviderService.Object);
        }

        [Fact]
        public async Task GetResultAsync_should_return_result_for_valid_address()
        {
            _dataProviderService.Setup(e => e.GetResultAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult("test result"));
            var result = await _pingLookUpService.GetPingLookUpResultAsync("8.8.8.8");
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GetResultAsync_should_not_return_result_for_invalid_address()
        {
            _dataProviderService.Setup(e => e.GetResultAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<string>(string.Empty));
            var result = await _pingLookUpService.GetPingLookUpResultAsync("8888");
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
        }
    }
}
