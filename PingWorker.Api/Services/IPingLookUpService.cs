using Api.Common.Contracts;

namespace PingWorker.Api.Services;

public interface IPingLookUpService
{
    Task<PingLookUpResult> GetPingLookUpResultAsync(string address, CancellationToken cancellationToken = default);
}
