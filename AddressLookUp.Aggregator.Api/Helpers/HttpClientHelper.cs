using Api.Common;

namespace AddressLookUp.Aggregator.Api.Helpers;

public static class HttpClientHelper
{
    public static string GetServiceUrl(string address, string service, string baseUrl, string? addressType = null)
    {
        switch (service)
        {
            case Constants.Ping:
                return $"{baseUrl}/api/ping/{address}";
            case Constants.RDAP:
                return $"{baseUrl}/api/rdap/{address}/{addressType}";
            case Constants.GeoIP:
                return $"{baseUrl}/api/geoip/{address}";
            case Constants.ReverseDNS:
                return $"{baseUrl}/api/reversedns/{address}";
            case Constants.DomainAvailability:
                return $"{baseUrl}/api/domain/{address}";
            default:
                break;
        }
        return string.Empty;
    }
}
