using Api.Common.Contracts;

namespace GeoIpWorker.Api.Services
{
    public interface IGeoIpLookUpService
    {
        Task<GeoIpLookUpResult> GetGeoIpLookUpResultAsync(string address, CancellationToken cancellationToken = default);
    }
}
