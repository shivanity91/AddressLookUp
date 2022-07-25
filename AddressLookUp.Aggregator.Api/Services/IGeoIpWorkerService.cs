using Api.Common.Contracts;

namespace AddressLookUp.Aggregator.Api.Services;

public interface IGeoIpWorkerService
{
    Task<GeoIpLookUpResult> GetGeoIpDataAsync(string address);
}
