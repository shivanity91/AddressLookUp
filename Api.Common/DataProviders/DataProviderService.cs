namespace Api.Common.DataProvider;

public class DataProviderService : IDataProviderService
{
    private readonly HttpClient _httpClient;

    public DataProviderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> GetResultAsync(string url, CancellationToken cancellationToken = default)
    {
        using var response = await _httpClient.GetAsync(url, cancellationToken);
        return await response.Content.ReadAsStringAsync(cancellationToken);
    }
}
