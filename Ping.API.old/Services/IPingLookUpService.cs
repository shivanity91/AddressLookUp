using Api.Common.Contracts;

namespace Ping.Api.Services
{
    public interface IPingLookUpService
    {
        Task<PingLookUpResult> GetPingLookUpResultAsync(string address, CancellationToken cancellationToken = default);
    }
}
