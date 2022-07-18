namespace Api.Common.Contracts
{
    public class AddressLookUpResult
    {
        public PingLookUpResult Ping { get; set; }
        public RdapLookUpResult Rdap { get; set; }
        public GeoIpLookUpResult GeoIp { get; set; }
        public ReverseDnsLookUpResult ReverseDns { get; set; }
    }
}
