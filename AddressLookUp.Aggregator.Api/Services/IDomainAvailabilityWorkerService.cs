using Api.Common.Contracts;

namespace AddressLookUp.Aggregator.Api.Services;

public interface IDomainAvailabilityWorkerService
{
    Task<DomainAvailabilityResult> GetDomainAvailabilityDataAsync(string address);
}
