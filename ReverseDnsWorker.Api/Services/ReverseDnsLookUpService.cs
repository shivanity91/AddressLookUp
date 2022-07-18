using Api.Common.Contracts;
using Api.Common.DataProvider;
using ReverseDnsWorker.Api.Models;

namespace ReverseDnsWorker.Api.Services
{
    public class ReverseDnsLookUpService : IReverseDnsLookUpService
    {
        private readonly ReverseDnsApiOptions _reverseDnsApiOptions;
        private readonly IDataProviderService _dataProviderService;

        public ReverseDnsLookUpService(ReverseDnsApiOptions reverseDnsApiOptions, IDataProviderService dataProviderService)
        {
            _reverseDnsApiOptions = reverseDnsApiOptions;
            _dataProviderService = dataProviderService;
        }

        public async Task<ReverseDnsLookUpResult> GetReverseDnsLookUpResultAsync(string address, CancellationToken cancellationToken = default)
        {
            var data = await _dataProviderService.GetResultAsync($"{_reverseDnsApiOptions.lookUpUrl}/?q={address}", cancellationToken);
            //ip={address}&apikey={_reverseDnsApiOptions.apiKey}&output={_reverseDnsApiOptions.outputType}", cancellationToken);
            var result = new ReverseDnsLookUpResult { Result = data, IsSuccess = !string.IsNullOrWhiteSpace(data) };
            return result;
        }
    }
}
