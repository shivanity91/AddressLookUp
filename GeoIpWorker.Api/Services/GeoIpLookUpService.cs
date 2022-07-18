using Api.Common.Contracts;
using Api.Common.DataProvider;
using GeoIpWorker.Api.Models;

namespace GeoIpWorker.Api.Services
{
    public class GeoIpLookUpService : IGeoIpLookUpService
    {
        private readonly GeoIpApiOptions _geoIpApiOptions;
        private readonly IDataProviderService _dataProviderService;

        public GeoIpLookUpService(GeoIpApiOptions geoIpApiOptions, IDataProviderService dataProviderService)
        {
            _geoIpApiOptions = geoIpApiOptions;
            _dataProviderService = dataProviderService;
        }

        public async Task<GeoIpLookUpResult> GetGeoIpLookUpResultAsync(string address, CancellationToken cancellationToken = default)
        {
            var data = await _dataProviderService.GetResultAsync($"{_geoIpApiOptions.lookUpUrl}/?host={address}", cancellationToken);
            var result = new GeoIpLookUpResult { Result = data, IsSuccess = !string.IsNullOrWhiteSpace(data) };
            return result;
        }
    }
}
