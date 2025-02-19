using ProjectManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Json;



namespace ProjectManagementWpf.Services
{
    internal class ApiServiceDoljnosti
    {
        private readonly HttpClient _httpClient;

        public ApiServiceDoljnosti()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7208/api/Doljnosti/") };
        }

        internal async Task<List<DoljnostiDto>> GetDoljnostiListAsync()
        {
            var response = await _httpClient.GetAsync("");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<DoljnostiDto>>();
        }

        internal async Task<DoljnostiDto> GetDoljnostiByIdAsync(int doljnosId)
        {
            var response = await _httpClient.GetAsync($"{doljnosId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<DoljnostiDto>();
        }

        internal async Task<string> GetDoljnostiFioAsync(int doljnosId)
        {
            var response = await _httpClient.GetAsync($"{doljnosId}/FIO");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        internal async Task AddDoljnostiAsync(DoljnostiDto doljnosti)
        {
            var response = await _httpClient.PostAsJsonAsync("POST", doljnosti);
            response.EnsureSuccessStatusCode();
        }

        internal async Task UpdateDoljnostiAsync(int id_doljnosti, DoljnostiDto doljnosti)
        {
            var response = await _httpClient.PutAsJsonAsync($"PUT/{id_doljnosti}", doljnosti);
            response.EnsureSuccessStatusCode();
        }

        internal async Task DeleteDoljnostiAsync(int id_doljnosti)
        {
            var response = await _httpClient.DeleteAsync($"DELETE/{id_doljnosti}");
            response.EnsureSuccessStatusCode();
        }
    }
}