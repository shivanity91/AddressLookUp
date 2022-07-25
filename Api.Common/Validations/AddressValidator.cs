using System.Net;

namespace Api.Common.Validation;

public static class AddressValidator
{
    public static bool IsAddressValid(string address)
    {
        return GetAddressType(address) != AddressType.None;
    }

    public static AddressType GetAddressType(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
        {
            return AddressType.None;
        }

        AddressType addressType;
        try
        {
            bool isValidIPAddress = IsValidIPAddress(address);
            if (isValidIPAddress)
            {
                return AddressType.IPAddress;
            }

            bool isValidDomainAddress = IsValidDomainAddress(address);
            if (isValidDomainAddress)
            {
                return AddressType.Domain;
            }

            return AddressType.None;

        }
        catch (Exception)
        {
            return AddressType.None;
        }
    }

    private static bool IsValidIPAddress(string address)
    {
        IPAddress IP;
        bool isValidIP = IPAddress.TryParse(address, out IP);
        return isValidIP;
    }

    private static bool IsValidDomainAddress(string address)
    {
        Uri uriResult;
        bool isValidDomain = Uri.TryCreate(new($"http://{address}"), UriKind.Absolute, out uriResult)
            && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        return isValidDomain;
    }

    public enum AddressType
    {
        None = 0,
        IPAddress = 1,
        Domain = 2
    }

}
