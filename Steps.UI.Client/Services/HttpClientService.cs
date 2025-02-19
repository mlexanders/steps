using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Steps.UI.Client.Services;

public class HttpClientService 
{
    private readonly HttpClient _httpClient;

    public HttpClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TResponse> GetAsync<TResponse, TRequest>(string resource) where TRequest : class, new()
    {
        return await SendRequest<TResponse, TRequest>(HttpMethod.Get, resource);
    }

    public async Task<TResponse> PostAsync<TResponse, TRequest>(string resource, TRequest data) where TRequest : class, new()
    {
        return await SendRequest<TResponse, TRequest>(HttpMethod.Post, resource, data);
    }

    public async Task<TResponse> PatchAsync<TResponse, TRequest>(string resource, TRequest data) where TRequest : class, new()
    {
        return await SendRequest<TResponse, TRequest>(HttpMethod.Patch, resource, data);
    }

    public async Task<TResponse> DeleteAsync<TResponse>(string resource)
    {
        return await SendRequest<TResponse, object>(HttpMethod.Delete, resource);
    }

    private async Task<TResponse> SendRequest<TResponse, TRequest>(HttpMethod method, string resource, TRequest? data = null) where TRequest : class, new()
    {
        var request = new HttpRequestMessage(method, resource);

        if (data is not null)
        {
            request.Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        }

        var response = await _httpClient.SendAsync(request);
        return await HandleResponse<TResponse, TRequest>(response);
    }

    private async Task<TResponse> HandleResponse<TResponse, TRequest>(HttpResponseMessage response)
    {
        //TODO: 
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadFromJsonAsync<TResponse>();
            return content; 
        }
        
        var message = await response.Content.ReadAsStringAsync();
        throw new HttpRequestException(message);
    }
}