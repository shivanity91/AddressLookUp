using AddressLookUp.Aggregator.Api.Helpers;
using AddressLookUp.Aggregator.Api.Models;
using Api.Common;
using Api.Common.Contracts;
using Api.Common.WorkerServices;

namespace AddressLookUp.Aggregator.Api.Services;

public class GeoIpWorkerService : IGeoIpWorkerService
{
    private readonly LookUpApiOptions _serviceOptions;

    public GeoIpWorkerService(LookUpApiOptions serviceOptions)
    {
        _serviceOptions = serviceOptions;
    }

    public async Task<GeoIpLookUpResult> GetGeoIpDataAsync(string address)
    {
        var serviceUrl = HttpClientHelper.GetServiceUrl(address, Constants.GeoIP, _serviceOptions.GeoIpApiUrl);
        GeoIpLookUpWorker geoIpWorker = new(serviceUrl);
        GeoIpLookUpResult result = await geoIpWorker.GetAddressLookUpResultAsync();
        return result;
    }
}
