using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Steps.Shared;
using Steps.Shared.Exceptions;

namespace Steps.Client.Services.Api.Base;

public class HttpClientService
{
    private readonly HttpClient _httpClient;

    private static readonly JsonSerializerOptions Options = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };
    
    public HttpClientService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<TResponse> GetAsync<TResponse>(string resource)
        where TResponse : Result, new()
    {
        return await SendRequest<TResponse, object>(HttpMethod.Get, resource);
    }

    public async Task<TResponse> PostAsync<TResponse, TRequest>(string resource, TRequest? data = null)
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
        TRequest? data = null) 
        where TRequest : class, new()
        where TResponse : Result, new()
    {
        return await ExecuteSafeAsync(async () =>
        {
            var request = new HttpRequestMessage(method, resource);

            if (data is not null)
            {
                request.Content =
                    new StringContent(JsonSerializer.Serialize(data), Encoding.UTF8, "application/json");
            }

            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            return await HandleResponse<TResponse>(response);
        });
    }

    private static async Task<TResponse> HandleResponse<TResponse>(HttpResponseMessage response)
        where TResponse : Result, new()
    {
        if (response.StatusCode == HttpStatusCode.InternalServerError)
        {
            return GetErrorResponse<TResponse>("Произошла ошибка. Попробуйте позже.");
        }
        
        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            throw new AppUnauthorizedAccessException();
        }
        
        var contentString = await response.Content.ReadAsStringAsync();
        var content = JsonSerializer.Deserialize<TResponse>(contentString, Options);
        
        return content ?? GetErrorResponse<TResponse>("Пустой ответ от сервера.");
    }
    
    private static async Task<T> ExecuteSafeAsync<T>(Func<Task<T>> action)
        where T : Result, new()
    {
        try
        {
            var result = await action();
            return result;
        }
        catch (AppUnauthorizedAccessException)
        {
            return GetErrorResponse<T>("Неавторизован");
        }
        catch (Exception ex)
        {
            return GetErrorResponse<T>(ex.Message);
        }
    }

    private static TResponse GetErrorResponse<TResponse>(string message)
        where TResponse : Result, new()
    {
        var errorResponse = new TResponse();
        errorResponse.SetMessage(message);
        return errorResponse;
    }
}