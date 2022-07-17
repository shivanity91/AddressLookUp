using Api.Common;

namespace AddressLookUp.Aggregator.Api.Helpers
{
    public static class HttpClientHelper
    {
        public static string GetServiceUrl(string address, string service, string baseUrl)
        {
            switch (service)
            {
                case Constants.Ping:
                    return $"{baseUrl}/api/ping/{address}";
                default:
                    break;
            }
            return string.Empty;
        }
    }
}
