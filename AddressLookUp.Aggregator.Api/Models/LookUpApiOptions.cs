namespace AddressLookUp.Aggregator.Api.Models
{
    public class LookUpApiOptions
    {
        public string PingApiUrl { get; set; }
        public string GeoIpApiUrl { get; set; }
        public string RdapApiUrl { get; set; }
        public string ReverseDnsApiUrl { get; set; }
    }
}
