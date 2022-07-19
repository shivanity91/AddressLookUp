using Api.Common.Contracts;

namespace DomainAvailabilityWorker.Api.Services
{
    public interface IDomainAvailabilityService
    {
        Task<DomainAvailabilityResult> GetDomainAvailabilityLookUpResultAsync(string address, CancellationToken cancellationToken = default);
    }
}
