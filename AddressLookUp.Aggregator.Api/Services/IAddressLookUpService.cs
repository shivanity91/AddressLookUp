using Api.Common.Contracts;

namespace AddressLookUp.Aggregator.Api.Services
{
    public interface IAddressLookUpService
    {
        Task<AddressLookUpResult> GetAddressLookUpAsync(string address, string services, CancellationToken cancellationToken = default);
    }
}
