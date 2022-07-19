using Api.Common.Contracts;

namespace RdapWorker.Api.Services;

public interface IRdapLookUpService
{
    Task<RdapLookUpResult> GetRdapLookUpResultAsync(string address, string addressType, CancellationToken cancellationToken = default);
}
