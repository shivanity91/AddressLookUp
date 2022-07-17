using AddressLookUp.Aggregator.Api.Helpers;
using AddressLookUp.Aggregator.Api.Models;
using Api.Common;
using Api.Common.Contracts;
using Api.Common.WorkerServices;

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
    }
}
