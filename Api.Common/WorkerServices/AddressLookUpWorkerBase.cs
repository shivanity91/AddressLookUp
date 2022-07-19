using System.Text.Json;
using Api.Common.Contracts;
using Api.Common.DataProvider;

namespace Api.Common.WorkerServices;

public abstract class AddressLookUpWorkerBase<T> where T : AddressLookUpResultBase, new()
{
    protected string workerServiceUrl { get; }

    protected AddressLookUpWorkerBase(string url)
    {
        workerServiceUrl = url ?? throw new ArgumentNullException(nameof(url));
    }

    private readonly DataProviderService _provider = new(new System.Net.Http.HttpClient());

    public async Task<T> GetAddressLookUpResultAsync()
    {
        string apiResult = await _provider.GetResultAsync(workerServiceUrl);
        return JsonSerializer.Deserialize<T>(apiResult, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}
