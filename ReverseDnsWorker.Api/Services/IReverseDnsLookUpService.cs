using Api.Common.Contracts;

namespace ReverseDnsWorker.Api.Services
{
    public interface IReverseDnsLookUpService
    {
        Task<ReverseDnsLookUpResult> GetReverseDnsLookUpResultAsync(string address, CancellationToken cancellationToken = default);
    }
}
