using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace HelpDesk.Services.Services
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> DeleteAsync(string url);
        Task<T?> GetAsync<T>(string url);
        Task<HttpResponseMessage> PostAsync<T>(string url, T data);
        Task<HttpResponseMessage> PutAsync<T>(string url, T data);
    }

    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public HttpService()
        {
            this._httpClient = new HttpClient();
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            return await _httpClient.GetFromJsonAsync<T>(
                url,
                _jsonOptions);
        }


        public async Task<HttpResponseMessage> PostAsync<T>(
            string url,
            T data)
        {
            return await _httpClient.PostAsJsonAsync(
                url,
                data);
        }


        public async Task<HttpResponseMessage> PutAsync<T>(
            string url,
            T data)
        {
            return await _httpClient.PutAsJsonAsync(
                url,
                data);
        }


        public async Task<HttpResponseMessage> DeleteAsync(
            string url)
        {
            return await _httpClient.DeleteAsync(url);
        }
    }
}
