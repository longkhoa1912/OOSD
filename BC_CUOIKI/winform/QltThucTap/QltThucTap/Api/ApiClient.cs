using Newtonsoft.Json;
using QltThucTap.WinForms.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace QltThucTap.WinForms.Api
{
public static class ApiClient
{
    private static readonly HttpClient Http = CreateClient();

    private static HttpClient CreateClient()
    {
        var client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:8080/"),
            Timeout = TimeSpan.FromSeconds(30)
        };

        client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        return client;
    }

    public static Task<ApiResponse<T>> GetAsync<T>(string url) =>
        SendAsync<T>(new HttpRequestMessage(HttpMethod.Get, url));

    public static Task<ApiResponse<T>> PostAsync<T>(string url, object body) =>
        SendJsonAsync<T>(HttpMethod.Post, url, body);

    public static Task<ApiResponse<T>> PutAsync<T>(string url, object body) =>
        SendJsonAsync<T>(HttpMethod.Put, url, body);

    public static Task<ApiResponse<T>> DeleteAsync<T>(string url) =>
        SendAsync<T>(new HttpRequestMessage(HttpMethod.Delete, url));

    private static Task<ApiResponse<T>> SendJsonAsync<T>(
        HttpMethod method, string url, object body)
    {
        var json = JsonConvert.SerializeObject(body);

        var request = new HttpRequestMessage(method, url)
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        return SendAsync<T>(request);
    }

    private static async Task<ApiResponse<T>> SendAsync<T>(HttpRequestMessage request)
    {
        using (request)
        using (var response = await Http.SendAsync(request))
        {
            var json = await response.Content.ReadAsStringAsync();

            ApiResponse<T> result;
            try
            {
                result = JsonConvert.DeserializeObject<ApiResponse<T>>(json);
            }
            catch (JsonException ex)
            {
                throw new HttpRequestException(
                    $"Backend trả JSON không đúng định dạng. HTTP {(int)response.StatusCode}.",
                    ex);
            }

            if (result is null)
            {
                throw new HttpRequestException(
                    $"Backend không trả ApiResponse hợp lệ. HTTP {(int)response.StatusCode}.");
            }

            if (!response.IsSuccessStatusCode && result.Success)
            {
                result.Success = false;
                if (result.Message == null)
                    result.Message = $"Yêu cầu thất bại. HTTP {(int)response.StatusCode}.";
            }

            return result;
        }
    }
}
}
