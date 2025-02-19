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
    internal class ApiServiceEmployees
    {
        private readonly HttpClient _httpClient;

        public ApiServiceEmployees()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7208/api/Employees/") };
        }

        internal async Task<List<Employees>> GetEmployeesAsync()
        {
            var response = await _httpClient.GetAsync("");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Employees>>();
        }

        internal async Task<Employees> GetEmployeeByIdAsync(int employeeId)
        {
            var response = await _httpClient.GetAsync($"{employeeId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Employees>();
        }

        internal async Task AddEmployeeAsync(Employees employee)
        {
            var response = await _httpClient.PostAsJsonAsync("", employee);
            response.EnsureSuccessStatusCode();
        }

        internal async Task UpdateEmployeeAsync(int employeeId, Employees employee)
        {
            var response = await _httpClient.PutAsJsonAsync($"{employeeId}", employee);
            response.EnsureSuccessStatusCode();
        }

        internal async Task DeleteEmployeeAsync(int employeeId)
        {
            var response = await _httpClient.DeleteAsync($"{employeeId}");
            response.EnsureSuccessStatusCode();
        }
    }
}