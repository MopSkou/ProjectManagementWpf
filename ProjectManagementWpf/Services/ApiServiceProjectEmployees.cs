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
    internal class ApiServiceProjectEmployees
    {
        private readonly HttpClient _httpClient;

        public ApiServiceProjectEmployees()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7208/api/ProjectEmployees/") };
        }

        internal async Task<List<ProjectEmployees>> GetProjectEmployeesAsync()
        {
            var response = await _httpClient.GetAsync("");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<ProjectEmployees>>();
        }

        internal async Task<ProjectEmployees> GetProjectEmployeeByIdAsync(int projectId, int employeeId)
        {
            var response = await _httpClient.GetAsync($"{projectId}/{employeeId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<ProjectEmployees>();
        }

        internal async Task AddProjectEmployeeAsync(ProjectEmployees projectEmployee)
        {
            var response = await _httpClient.PostAsJsonAsync("", projectEmployee);
            response.EnsureSuccessStatusCode();
        }

        internal async Task UpdateProjectEmployeeAsync(int projectId, int employeeId, ProjectEmployees projectEmployee)
        {
            var response = await _httpClient.PutAsJsonAsync($"{projectId}/{employeeId}", projectEmployee);
            response.EnsureSuccessStatusCode();
        }

        internal async Task DeleteProjectEmployeeAsync(int projectId, int employeeId)
        {
            var response = await _httpClient.DeleteAsync($"{projectId}/{employeeId}");
            response.EnsureSuccessStatusCode();
        }
    }
}