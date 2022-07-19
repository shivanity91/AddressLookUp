using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Api.Common.DataProvider;
using RdapWorker.Api.Services;
using RdapWorker.Api.Models;

namespace PingWorker.Api.Tests.Services
{
    public class RdapLookUpServiceTests
    {
        private readonly RdapLookUpService _rdapLookUpService;
        private readonly Mock<IDataProviderService> _dataProviderService;

        public RdapLookUpServiceTests()
        {
            _dataProviderService = new Mock<IDataProviderService>();
            _rdapLookUpService = new RdapLookUpService(new RdapApiOptions { lookUpUrl = string.Empty }, _dataProviderService.Object);
        }

        [Fact]
        public async Task GetResultAsync_should_return_result_for_valid_address()
        {
            _dataProviderService.Setup(e => e.GetResultAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult("test result"));
            var result = await _rdapLookUpService.GetRdapLookUpResultAsync("8.8.8.8", "domain");
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GetResultAsync_should_not_return_result_for_invalid_address()
        {
            _dataProviderService.Setup(e => e.GetResultAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(string.Empty));
            var result = await _rdapLookUpService.GetRdapLookUpResultAsync("8888", "domain");
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
        }
    }
}
