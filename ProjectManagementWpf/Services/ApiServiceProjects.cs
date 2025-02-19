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
    internal class ApiServiceProjects
    {
        private readonly HttpClient _httpClient;

        public ApiServiceProjects()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7208/api/Projects/") };
        }

        internal async Task<List<Projects>> GetProjectsAsync()
        {
            var response = await _httpClient.GetAsync("");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Projects>>();
        }

        internal async Task<Projects> GetProjectByIdAsync(int projectId)
        {
            var response = await _httpClient.GetAsync($"{projectId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Projects>();
        }

        internal async Task AddProjectAsync(Projects project)
        {
            var response = await _httpClient.PostAsJsonAsync("", project);
            response.EnsureSuccessStatusCode();
        }

        internal async Task UpdateProjectAsync(int projectId, Projects project)
        {
            var response = await _httpClient.PutAsJsonAsync($"{projectId}", project);
            response.EnsureSuccessStatusCode();
        }

        internal async Task DeleteProjectAsync(int projectId)
        {
            var response = await _httpClient.DeleteAsync($"{projectId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
