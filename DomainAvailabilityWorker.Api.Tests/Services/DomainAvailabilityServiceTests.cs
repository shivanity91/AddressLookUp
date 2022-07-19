using System.Threading;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Api.Common.DataProvider;
using DomainAvailabilityWorker.Api.Services;
using DomainAvailabilityWorker.Api.Models;

namespace DomainAvailabilityWorker.Api.Tests.Services
{
    public class DomainAvailabilityServiceTests
    {
        private readonly DomainAvailabilityService _domainLookUpService;
        private readonly Mock<IDataProviderService> _dataProviderService;

        public DomainAvailabilityServiceTests()
        {
            _dataProviderService = new Mock<IDataProviderService>();
            _domainLookUpService = new DomainAvailabilityService(new DomainAvailabilityApiOptions { lookUpUrl = string.Empty, apiKey = string.Empty, outputFormat = "JSON" }, _dataProviderService.Object);
        }

        [Fact]
        public async Task GetResultAsync_should_return_result_for_valid_address()
        {
            _dataProviderService.Setup(e => e.GetResultAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult("test result"));
            var result = await _domainLookUpService.GetDomainAvailabilityLookUpResultAsync("8.8.8.8");
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task GetResultAsync_should_not_return_result_for_invalid_address()
        {
            _dataProviderService.Setup(e => e.GetResultAsync(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult<string>(string.Empty));
            var result = await _domainLookUpService.GetDomainAvailabilityLookUpResultAsync("8888");
            Assert.NotNull(result);
            Assert.False(result.IsSuccess);
        }
    }
}
