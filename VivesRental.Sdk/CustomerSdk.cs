using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Vives.Services.Model;
using VivesRental.Enums;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Sdk
{
    public class CustomerSdk
    {
        private HttpClient _HttpClient;

        public CustomerSdk(HttpClient httpClient)
        {
            _HttpClient = httpClient;
        }

        public async Task<List<CustomerResult>> FindAsync(CustomerFilter? filter)
        {
            var route = $"https://localhost:7236/api/Customer?Search={filter}";
            var response = await _HttpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();

            var articles = await response.Content.ReadFromJsonAsync<List<CustomerResult>>();
            if (articles is null)
            {
                return new List<CustomerResult>();
            }

            return articles;
        }
        public async Task<CustomerResult> GetAsync(Guid id)
        {
            var route = $"https://localhost:7236/api/Customer/{id}";
            var respons = await _HttpClient.GetAsync(route);
            respons.EnsureSuccessStatusCode();

            var article = await respons.Content.ReadFromJsonAsync<CustomerResult>();
            return article;
        }

        public async Task<ServiceResult<CustomerResult>> CreateAsync(CustomerRequest request)
        {
            var route = "https://localhost:7236/api/Customer/Create";
            var response = await _HttpClient.PostAsJsonAsync(route, request);
            response.EnsureSuccessStatusCode();

            var article = await response.Content.ReadFromJsonAsync<ServiceResult<CustomerResult>>();
            return article;
        }

        public async Task<ServiceResult<CustomerResult>> Edit(Guid id, CustomerRequest status)
        {
            var route = $"https://localhost:7236/api/Customer/Edit/{id}";
            var response = await _HttpClient.PostAsJsonAsync(route, status);
            response.EnsureSuccessStatusCode();

            var article = await response.Content.ReadFromJsonAsync<ServiceResult<CustomerResult>>();
            return article;
        }

        public async Task<ServiceResult> Delete(Guid id)
        {
            var route = $"https://localhost:7236/api/Customer/Delete/{id}";
            var response = await _HttpClient.DeleteAsync(route);
            response.EnsureSuccessStatusCode();

            var article = await response.Content.ReadFromJsonAsync<ServiceResult>();
            return article;
        }
    }
}
