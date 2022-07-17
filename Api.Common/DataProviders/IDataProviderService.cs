namespace Api.Common.DataProvider
{
    public interface IDataProviderService
    {
        Task<string> GetResultAsync(string url, CancellationToken cancellationToken = default);
    }
}
