namespace Api.Common
{
    public class Constants
    {
        private static readonly List<string> _defaultServiceOptions = new() { Ping, ReverseDNS, RDAP, GeoIP };

        public static IReadOnlyList<string> DefaultServiceOptions = _defaultServiceOptions;

        public const string Ping = "ping";
        public const string ReverseDNS = "reversedns";
        public const string RDAP = "rdap";
        public const string GeoIP = "geoip";
    }
}