using Api.Common.Contracts;
using Api.Common.DataProvider;
using DomainAvailabilityWorker.Api.Models;

namespace DomainAvailabilityWorker.Api.Services
{
    public class DomainAvailabilityService : IDomainAvailabilityService
    {
        private readonly DomainAvailabilityApiOptions _domainCheckApiOptions;
        private readonly IDataProviderService _dataProviderService;

        public DomainAvailabilityService(DomainAvailabilityApiOptions domainCheckApiOptions, IDataProviderService dataProviderService)
        {
            _domainCheckApiOptions = domainCheckApiOptions;
            _dataProviderService = dataProviderService;
        }

        public async Task<DomainAvailabilityResult> GetDomainAvailabilityResultAsync(string address, CancellationToken cancellationToken = default)
        {
            var data = await _dataProviderService.GetResultAsync($"{_domainCheckApiOptions.lookUpUrl}?apiKey={_domainCheckApiOptions.apiKey}&domainName={address}", cancellationToken);
            var result = new DomainAvailabilityResult { Result = data, IsSuccess = !string.IsNullOrWhiteSpace(data) };
            return result;
        }
    }
}
