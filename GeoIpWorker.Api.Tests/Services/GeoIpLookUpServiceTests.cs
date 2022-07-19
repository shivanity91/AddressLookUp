using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Api.Common.DataProvider;
using GeoIpWorker.Api.Services;
using GeoIpWorker.Api.Models;

namespace GeoIpWorker.Api.Tests.Services
{
    public class GeoIpLookUpServiceTests
    {
        private readonly GeoIpLookUpService _geoIpLookUpService;
        private readonly Mock<IDataProviderService> _dataProviderService;

        public GeoIpLookUpServiceTests()
        {
            _dataProviderService = new Mock<IDataProviderService>();
            _geoIpLookUpService = new GeoIpLookUpService(new GeoIpApiOptions { lookUpUrl = string.Empty }, _dataProviderService.Object);
        }

        [Fact]
        public async Task GetResultAsync_should_return_result_for_valid_address()
        {
            _dataProviderService.Setup(e => e.GetResultAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult("test result"));
            var result = await _geoIpLookUpService.GetGeoIpLookUpResultAsync("8.8.8.8");
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GetResultAsync_should_not_return_result_for_invalid_address()
        {
            _dataProviderService.Setup(e => e.GetResultAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(string.Empty));
            var result = await _geoIpLookUpService.GetGeoIpLookUpResultAsync("8888");
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
        }
    }
}
