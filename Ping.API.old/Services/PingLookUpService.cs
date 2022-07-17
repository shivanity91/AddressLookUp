using Api.Common.Contracts;
using Api.Common.DataProvider;
using Ping.Api.Models;

namespace Ping.Api.Services
{
    public class PingLookUpService : IPingLookUpService
    {
        private readonly PingApiOptions _pingApiOptions;
        private readonly IDataProviderService _dataProviderService;

        public PingLookUpService(PingApiOptions pingApiOptions, IDataProviderService dataProviderService)
        {
            _pingApiOptions = pingApiOptions;
            _dataProviderService = dataProviderService;
        }

        public async Task<PingLookUpResult> GetPingLookUpResultAsync(string address, CancellationToken cancellationToken = default)
        {
            var data = await _dataProviderService.GetResultAsync($"{_pingApiOptions.lookUpUrl}/?host={address}", cancellationToken);
            var result = new PingLookUpResult { Result = data, IsSuccess = !string.IsNullOrWhiteSpace(data) };
            return result;
        }
    }
}
