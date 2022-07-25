using Api.Common.Contracts;

namespace AddressLookUp.Aggregator.Api.Services;

public interface IRdapWorkerService
{
    Task<RdapLookUpResult> GetRdapDataAsync(string address);
}
