using AddressLookUp.Aggregator.Api.Helpers;
using AddressLookUp.Aggregator.Api.Models;
using Api.Common;
using Api.Common.Contracts;
using Api.Common.WorkerServices;
using static Api.Common.Validation.AddressValidator;

namespace AddressLookUp.Aggregator.Api.Services;

public class RdapWorkerService : IRdapWorkerService
{
    private readonly LookUpApiOptions _serviceOptions;

    public RdapWorkerService(LookUpApiOptions serviceOptions)
    {
        _serviceOptions = serviceOptions;
    }

    public async Task<RdapLookUpResult> GetRdapDataAsync(string address)
    {
        string addressType = string.Empty;

        switch (GetAddressType(address))
        {
            case AddressType.Domain:
                addressType = "domain";
                break;
            case AddressType.IPAddress:
                addressType = "ip";
                break;
        }

        var serviceUrl = HttpClientHelper.GetServiceUrl(address, Constants.RDAP, _serviceOptions.RdapApiUrl, addressType);
        RdapLookUpWorker rdapWorker = new(serviceUrl);
        RdapLookUpResult result = await rdapWorker.GetAddressLookUpResultAsync();
        return result;
    }
}
