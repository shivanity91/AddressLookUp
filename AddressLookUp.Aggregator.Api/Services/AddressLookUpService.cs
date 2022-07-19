using AddressLookUp.Aggregator.Api.Helpers;
using AddressLookUp.Aggregator.Api.Models;
using Api.Common;
using Api.Common.Contracts;
using Api.Common.WorkerServices;
using static Api.Common.Validation.AddressValidator;

namespace AddressLookUp.Aggregator.Api.Services
{
    public class AddressLookUpService : IAddressLookUpService
    {
        private readonly LookUpApiOptions _serviceOptions;

        public AddressLookUpService(LookUpApiOptions serviceOptions)
        {
            _serviceOptions = serviceOptions;
        }
        public async Task<AddressLookUpResult> GetAddressLookUpAsync(string address, string servicesList, CancellationToken cancellationToken = default)
        {
            //Validate if address is valid
            var services = servicesList is null ? Constants.DefaultServiceOptions : servicesList?.Split(",", StringSplitOptions.RemoveEmptyEntries).Select(str => str.Trim())?.Distinct();
            return await RunWorkerTasksAsync(address, services?.ToList());
        }
        private async Task<AddressLookUpResult> RunWorkerTasksAsync(string address, List<string> services)
        {
            AddressLookUpResult servicesLookUpResult = new();
            var tasks = services.Select(async serviceType =>
            {
                switch (serviceType)
                {
                    case Constants.Ping:
                        servicesLookUpResult.Ping = await GetPingDataAsync(address);
                        break;
                    case Constants.RDAP:
                        servicesLookUpResult.Rdap = await GetRdapDataAsync(address);
                        break;
                    case Constants.GeoIP:
                        servicesLookUpResult.GeoIp = await GetGeoIpDataAsync(address);
                        break;
                    case Constants.ReverseDNS:
                        servicesLookUpResult.ReverseDns = await GetReverseDnsDataAsync(address);
                        break;
                    default:
                        break;
                }
            });
            await Task.WhenAll(tasks);
            return servicesLookUpResult;
        }

        private async Task<PingLookUpResult> GetPingDataAsync(string address)
        {
            var serviceUrl = HttpClientHelper.GetServiceUrl(address, Constants.Ping, _serviceOptions.PingApiUrl);
            PingLookUpWorker pingWorker = new(serviceUrl);
            PingLookUpResult result = await pingWorker.GetAddressLookUpResultAsync();
            return result;
        }

        private async Task<RdapLookUpResult> GetRdapDataAsync(string address)
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

        private async Task<GeoIpLookUpResult> GetGeoIpDataAsync(string address)
        {
            var serviceUrl = HttpClientHelper.GetServiceUrl(address, Constants.GeoIP, _serviceOptions.GeoIpApiUrl);
            GeoIpLookUpWorker geoIpWorker = new(serviceUrl);
            GeoIpLookUpResult result = await geoIpWorker.GetAddressLookUpResultAsync();
            return result;
        }

        private async Task<ReverseDnsLookUpResult> GetReverseDnsDataAsync(string address)
        {
            var serviceUrl = HttpClientHelper.GetServiceUrl(address, Constants.ReverseDNS, _serviceOptions.ReverseDnsApiUrl);
            ReverseDnsLookUpWorker reverseDnsWorker = new(serviceUrl);
            ReverseDnsLookUpResult result = await reverseDnsWorker.GetAddressLookUpResultAsync();
            return result;
        }
    }
}
