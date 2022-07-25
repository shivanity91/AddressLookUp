using Api.Common;
using Api.Common.Contracts;

namespace AddressLookUp.Aggregator.Api.Services;

public class AddressLookUpService : IAddressLookUpService
{
    private readonly IPingWorkerService _pingWorkerService;
    private readonly IRdapWorkerService _rdapWorkerService;
    private readonly IGeoIpWorkerService _geoIpWorkerService;
    private readonly IReverseDnsWorkerService _reverseDnsWorkerService;
    private readonly IDomainAvailabilityWorkerService _domainAvailabilityWorkerService;

    public AddressLookUpService(IPingWorkerService pingWorkerService, IRdapWorkerService rdapWorkerService,
        IGeoIpWorkerService geoIpWorkerService, IReverseDnsWorkerService reverseDnsWorkerService, IDomainAvailabilityWorkerService domainAvailabilityWorkerService)
    {
        _pingWorkerService = pingWorkerService;
        _rdapWorkerService = rdapWorkerService;
        _geoIpWorkerService = geoIpWorkerService;
        _reverseDnsWorkerService = reverseDnsWorkerService;
        _domainAvailabilityWorkerService = domainAvailabilityWorkerService;
    }

    /// <summary>
    /// To fetch address lookup for all services provided in serviceslist
    /// </summary>
    /// <param name="address"></param>
    /// <param name="servicesList"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<AddressLookUpResult> GetAddressLookUpAsync(string address, string servicesList, CancellationToken cancellationToken = default)
    {
        var services = servicesList is null ? Constants.DefaultServiceOptions : servicesList?.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(str => str.Trim())?.Distinct();
        return await RunWorkerTasksAsync(address, services?.ToList());
    }

    /// <summary>
    /// To Run Service Workers asynchronously for the required services
    /// </summary>
    /// <param name="address"></param>
    /// <param name="services"></param>
    /// <returns></returns>
    private async Task<AddressLookUpResult> RunWorkerTasksAsync(string address, List<string> services)
    {
        AddressLookUpResult servicesLookUpResult = new();
        var tasks = services.Select(async serviceType =>
        {
            switch (serviceType)
            {
                case Constants.Ping:
                    servicesLookUpResult.Ping = await _pingWorkerService.GetPingDataAsync(address);
                    break;
                case Constants.RDAP:
                    servicesLookUpResult.Rdap = await _rdapWorkerService.GetRdapDataAsync(address);
                    break;
                case Constants.GeoIP:
                    servicesLookUpResult.GeoIp = await _geoIpWorkerService.GetGeoIpDataAsync(address);
                    break;
                case Constants.ReverseDNS:
                    servicesLookUpResult.ReverseDns = await _reverseDnsWorkerService.GetReverseDnsDataAsync(address);
                    break;
                case Constants.DomainAvailability:
                    servicesLookUpResult.DomainAvailability = await _domainAvailabilityWorkerService.GetDomainAvailabilityDataAsync(address);
                    break;
                default:
                    break;
            }
        });
        await Task.WhenAll(tasks);
        return servicesLookUpResult;
    }

    
}
