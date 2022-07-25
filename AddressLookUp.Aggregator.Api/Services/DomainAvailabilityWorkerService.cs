using AddressLookUp.Aggregator.Api.Helpers;
using AddressLookUp.Aggregator.Api.Models;
using Api.Common;
using Api.Common.Contracts;
using Api.Common.WorkerServices;

namespace AddressLookUp.Aggregator.Api.Services;

public class DomainAvailabilityWorkerService : IDomainAvailabilityWorkerService
{
    private readonly LookUpApiOptions _serviceOptions;

    public DomainAvailabilityWorkerService(LookUpApiOptions serviceOptions)
    {
        _serviceOptions = serviceOptions;
    }

    public async Task<DomainAvailabilityResult> GetDomainAvailabilityDataAsync(string address)
    {
        var serviceUrl = HttpClientHelper.GetServiceUrl(address, Constants.DomainAvailability, _serviceOptions.DomainAvailabilityApiUrl);
        DomainAvailabilityWorker domainAvailabilityWorker = new(serviceUrl);
        DomainAvailabilityResult result = await domainAvailabilityWorker.GetAddressLookUpResultAsync();
        return result;
    }
}
