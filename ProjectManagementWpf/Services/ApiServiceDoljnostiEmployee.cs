using ProjectManagementApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagementWpf.Services
{
    internal class ApiServiceDoljnostiEmployee
    {
        private readonly HttpClient _httpClient;

        public ApiServiceDoljnostiEmployee()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7208/api/DoljnostiEmployee/") };
        }

        internal async Task<List<DoljnostiEmployee>> GetDoljnostiEmployeesAsync()
        {
            var response = await _httpClient.GetAsync("");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<DoljnostiEmployee>>();
        }

        internal async Task<DoljnostiEmployee> GetDoljnostiEmployeeByIdAsync(int employeeId, int postId)
        {
            var response = await _httpClient.GetAsync($"{employeeId}/{postId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<DoljnostiEmployee>();
        }

        internal async Task AddDoljnostiEmployeeAsync(DoljnostiEmployee doljnostiEmployee)
        {
            var response = await _httpClient.PostAsJsonAsync("", doljnostiEmployee);
            response.EnsureSuccessStatusCode();
        }

        internal async Task UpdateDoljnostiEmployeeAsync(int employeeId, int postId, DoljnostiEmployee doljnostiEmployee)
        {
            var response = await _httpClient.PutAsJsonAsync($"{employeeId}/{postId}", doljnostiEmployee);
            response.EnsureSuccessStatusCode();
        }

        internal async Task DeleteDoljnostiEmployeeAsync(int employeeId, int postId)
        {
            var response = await _httpClient.DeleteAsync($"{employeeId}/{postId}");
            response.EnsureSuccessStatusCode();
        }
    }
}