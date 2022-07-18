using Api.Common.Contracts;
using Api.Common.DataProvider;
using RdapWorker.Api.Models;

namespace RdapWorker.Api.Services
{
    public class RdapLookUpService : IRdapLookUpService
    {
        private readonly RdapApiOptions _rdapApiOptions;
        private readonly IDataProviderService _dataProviderService;

        public RdapLookUpService(RdapApiOptions rdapApiOptions, IDataProviderService dataProviderService)
        {
            _rdapApiOptions = rdapApiOptions;
            _dataProviderService = dataProviderService;
        }

        public async Task<RdapLookUpResult> GetRdapLookUpResultAsync(string address, string addressType, CancellationToken cancellationToken = default)
        {
            var rdapUrl = addressType?.ToLower() switch
            {
                "ip" => $"{_rdapApiOptions.lookUpUrl}/ip",
                "domain" => $"{_rdapApiOptions.lookUpUrl}/domain",
                _ => throw new ("invalid address type"),
            };


            var data = await _dataProviderService.GetResultAsync($"{rdapUrl}/{address}", cancellationToken);
            var result = new RdapLookUpResult { Result = data, IsSuccess = !string.IsNullOrWhiteSpace(data) };
            return result;
        }
    }
}
