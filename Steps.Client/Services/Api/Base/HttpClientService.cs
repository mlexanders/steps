using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Steps.Shared;

namespace Steps.Client.Services.Api.Base;

public class HttpClientService
{
    private readonly HttpClient _httpClient;

    public HttpClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TResponse> GetAsync<TResponse>(string resource)
        where TResponse : Result, new()
    {
        return await SendRequest<TResponse, object>(HttpMethod.Get, resource);
    }

    public async Task<TResponse> PostAsync<TResponse, TRequest>(string resource, TRequest? data)
        where TRequest : class, new()
        where TResponse : Result, new()
    {
        return await SendRequest<TResponse, TRequest>(HttpMethod.Post, resource, data);
    }

    public async Task<TResponse> PatchAsync<TResponse, TRequest>(string resource, TRequest data)
        where TRequest : class, new()
        where TResponse : Result, new()
    {
        return await SendRequest<TResponse, TRequest>(HttpMethod.Patch, resource, data);
    }

    public async Task<TResponse> DeleteAsync<TResponse>(string resource)
        where TResponse : Result, new()
    {
        return await SendRequest<TResponse, object>(HttpMethod.Delete, resource);
    }

    private async Task<TResponse> SendRequest<TResponse, TRequest>(HttpMethod method, string resource,
        TRequest? data = null) where TRequest : class, new()
        where TResponse : Result, new()
    {
        var request = new HttpRequestMessage(method, resource);

        if (data is not null)
        {
            request.Content = new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
        }

        var response = await _httpClient.SendAsync(request);
        return await HandleResponse<TResponse>(response);
    }

    private async Task<TResponse> HandleResponse<TResponse>(HttpResponseMessage response)
        where TResponse : Result, new()
    {
        try
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var contentString = await response.Content.ReadAsStringAsync();

            // if (response.IsSuccessStatusCode)
            // {
            //     var content = JsonSerializer.Deserialize<TResponse>(contentString, options);
            //     return content ?? GetErrorResponse<TResponse>("Пустой ответ от сервера.");
            // }
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                return GetErrorResponse<TResponse>("Произошла ошибка. Попробуйте позже.");
            }
            
            var content = JsonSerializer.Deserialize<TResponse>(contentString, options);
            return content ?? GetErrorResponse<TResponse>("Пустой ответ от сервера.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при обработке ответа: {ex.Message}");
            return GetErrorResponse<TResponse>("Ошибка при обработке ответа сервера.");
        }
    }

    private static TResponse GetErrorResponse<TResponse>(string message) where TResponse : Result, new()
    {
        var errorResponse = new TResponse();
        errorResponse.SetMessage(message);
        return errorResponse;
    }
}