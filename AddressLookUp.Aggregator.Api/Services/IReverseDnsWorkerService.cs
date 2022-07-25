using Api.Common.Contracts;

namespace AddressLookUp.Aggregator.Api.Services;

public interface IReverseDnsWorkerService
{
    Task<ReverseDnsLookUpResult> GetReverseDnsDataAsync(string address);
}
