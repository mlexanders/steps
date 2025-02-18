using System.Text;
using System.Text.Json;

namespace Steps.UI.Client.Services;

public class HttpClientService 
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

    public HttpClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TResult> GetAsync<TResult, TResponse>(string resource) where TResponse : class, new()
    {
        return await SendRequest<TResult, TResponse>(HttpMethod.Get, resource);
    }

    public async Task<TResult> PostAsync<TResult, TResponse>(string resource, TResponse data) where TResponse : class, new()
    {
        return await SendRequest<TResult, TResponse>(HttpMethod.Post, resource, data);
    }

    public async Task<TResult> PatchAsync<TResult, TResponse>(string resource, TResponse data) where TResponse : class, new()
    {
        return await SendRequest<TResult, TResponse>(HttpMethod.Patch, resource, data);
    }

    public async Task<TResult> DeleteAsync<TResult>(string resource)
    {
        return await SendRequest<TResult, object>(HttpMethod.Delete, resource);
    }

    private async Task<TResult> SendRequest<TResult, TResponse>(HttpMethod method, string resource, TResponse? data = null) where TResponse : class, new()
    {
        var request = new HttpRequestMessage(method, resource);

        if (data is not null)
        {
            request.Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        }

        var response = await _httpClient.SendAsync(request);
        return await HandleResponse<TResult, TResponse>(response);
    }

    private async Task<TResult> HandleResponse<TResult, TResponse>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return (TResult)Activator.CreateInstance(typeof(TResult), $"Ошибка {response.StatusCode}: {content}")!;
        }

        var deserialized = JsonSerializer.Deserialize<TResponse>(content, _jsonOptions);
        return (TResult)Activator.CreateInstance(typeof(TResult), deserialized)!;
    }
}