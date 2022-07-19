using Api.Common.Contracts;

namespace DomainAvailabilityWorker.Api.Services
{
    public interface IDomainAvailabilityService
    {
        Task<DomainAvailabilityResult> GetDomainAvailabilityResultAsync(string address, CancellationToken cancellationToken = default);
    }
}
