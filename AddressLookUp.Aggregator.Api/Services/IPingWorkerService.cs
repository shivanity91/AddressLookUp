using Api.Common.Contracts;

namespace AddressLookUp.Aggregator.Api.Services;

public interface IPingWorkerService
{
    Task<PingLookUpResult> GetPingDataAsync(string address);
}
