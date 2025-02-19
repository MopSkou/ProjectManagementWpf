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
    internal class ApiServiceCompanies
    {
        private readonly HttpClient _httpClient;

        public ApiServiceCompanies()
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7208/api/Companies/") };
        }

        internal async Task<List<Companies>> GetCompaniesListAsync()
        {
            var response = await _httpClient.GetAsync("");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Companies>>();
        }

        internal async Task<Companies> GetCompanyByIdAsync(int companyId)
        {
            var response = await _httpClient.GetAsync($"{companyId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Companies>();
        }

        internal async Task AddCompanyAsync(Companies company)
        {
            var response = await _httpClient.PostAsJsonAsync("", company);
            response.EnsureSuccessStatusCode();
        }

        internal async Task UpdateCompanyAsync(int companyId, Companies company)
        {
            var response = await _httpClient.PutAsJsonAsync($"{companyId}", company);
            response.EnsureSuccessStatusCode();
        }

        internal async Task DeleteCompanyAsync(int companyId)
        {
            var response = await _httpClient.DeleteAsync($"{companyId}");
            response.EnsureSuccessStatusCode();
        }
    }
}