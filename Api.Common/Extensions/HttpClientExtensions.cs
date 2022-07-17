using System.Text.Json;

namespace Api.Common.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T> ReadFromJsonAsync<T>(this HttpResponseMessage response)
        {
            var stringContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(stringContent, null, response.StatusCode);
            }
            else
            {
                return JsonSerializer.Deserialize<T>(stringContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
        }
        //public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        //{
        //    if (!response.IsSuccessStatusCode)
        //        throw new ApplicationException($"Something went wrong calling the API: {response.ReasonPhrase}");

        //    var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        //    return JsonSerializer.Deserialize<T>(dataAsString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        //}
    }
}
