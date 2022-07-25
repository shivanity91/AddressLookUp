using AddressLookUp.Aggregator.Api.Helpers;
using AddressLookUp.Aggregator.Api.Models;
using Api.Common;
using Api.Common.Contracts;
using Api.Common.WorkerServices;

namespace AddressLookUp.Aggregator.Api.Services
{
    public class ReverseDnsWorkerService : IReverseDnsWorkerService
    {
        private readonly LookUpApiOptions _serviceOptions;

        public ReverseDnsWorkerService(LookUpApiOptions serviceOptions)
        {
            _serviceOptions = serviceOptions;
        }

        public async Task<ReverseDnsLookUpResult> GetReverseDnsDataAsync(string address)
        {
            var serviceUrl = HttpClientHelper.GetServiceUrl(address, Constants.ReverseDNS, _serviceOptions.ReverseDnsApiUrl);
            ReverseDnsLookUpWorker reverseDnsWorker = new(serviceUrl);
            ReverseDnsLookUpResult result = await reverseDnsWorker.GetAddressLookUpResultAsync();
            return result;
        }
    }
}
