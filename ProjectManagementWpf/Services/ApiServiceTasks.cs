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
    internal class ApiServiceTasks
    {
        private readonly HttpClient _httpClient;

        public ApiServiceTasks()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7208/api/Tasks/") };
        }

        internal async Task<List<Tasks>> GetTasksAsync()
        {
            var response = await _httpClient.GetAsync("");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Tasks>>();
        }

        internal async Task<Tasks> GetTaskByIdAsync(int taskId)
        {
            var response = await _httpClient.GetAsync($"{taskId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Tasks>();
        }

        internal async Task AddTaskAsync(Tasks task)
        {
            var response = await _httpClient.PostAsJsonAsync("", task);
            response.EnsureSuccessStatusCode();
        }

        internal async Task UpdateTaskAsync(int taskId, Tasks task)
        {
            var response = await _httpClient.PutAsJsonAsync($"{taskId}", task);
            response.EnsureSuccessStatusCode();
        }

        internal async Task DeleteTaskAsync(int taskId)
        {
            var response = await _httpClient.DeleteAsync($"{taskId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
