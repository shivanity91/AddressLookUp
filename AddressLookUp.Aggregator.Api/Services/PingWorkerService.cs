using AddressLookUp.Aggregator.Api.Helpers;
using AddressLookUp.Aggregator.Api.Models;
using Api.Common;
using Api.Common.Contracts;
using Api.Common.WorkerServices;

namespace AddressLookUp.Aggregator.Api.Services;

public class PingWorkerService : IPingWorkerService
{
    private readonly LookUpApiOptions _serviceOptions;

    public PingWorkerService(LookUpApiOptions serviceOptions)
    {
        _serviceOptions = serviceOptions;
    }
    public async Task<PingLookUpResult> GetPingDataAsync(string address)
    {
        var serviceUrl = HttpClientHelper.GetServiceUrl(address, Constants.Ping, _serviceOptions.PingApiUrl);
        PingLookUpWorker pingWorker = new(serviceUrl);
        PingLookUpResult result = await pingWorker.GetAddressLookUpResultAsync();
        return result;
    }
}
