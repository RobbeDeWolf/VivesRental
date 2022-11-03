using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Sdk
{
    public class ProductSdk
    {
        private HttpClient _HttpClient;

        public ProductSdk(HttpClient httpClient)
        {
            _HttpClient = httpClient;
        }

        public async Task<List<ProductResult>> FindAsync(ProductFilter? filter)
        {
            var route = $"https://localhost:7236/api/Product?AvailableFromDateTime={filter.AvailableFromDateTime}&AvailableUntilDateTime={filter.AvailableUntilDateTime}";
            var response = await _HttpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();

            var articles = await response.Content.ReadFromJsonAsync<List<ProductResult>>();
            if (articles is null)
            {
                return new List<ProductResult>();
            }

            return articles;
        }

        public async Task<ProductResult> GetAsync(Guid id)
        {
            var route = $"https://localhost:7236/api/Product/{id}";
            var respons = await _HttpClient.GetAsync(route);
            respons.EnsureSuccessStatusCode();

            var article = await respons.Content.ReadFromJsonAsync<ProductResult>();
            return article;
        }
        public async Task<ProductResult> CreateAsync(ProductRequest request)
        {
            var route = $"https://localhost:7236/api/Product/Create";
            var response = await _HttpClient.PostAsJsonAsync(route, request);
            response.EnsureSuccessStatusCode();

            var article = await response.Content.ReadFromJsonAsync<ProductResult>();
            return article;
        }

        public async Task<ProductResult> EditAsync(Guid id, ProductRequest request)
        {
            var route = $"https://localhost:7236/api/Product/Edit?id={id}";
            var respons = await _HttpClient.PostAsJsonAsync(route,request);
            respons.EnsureSuccessStatusCode();

            var article = await respons.Content.ReadFromJsonAsync<ProductResult>();
            return article;
        }

        public async Task<Boolean> DeleteAsync(Guid id)
        {
            var route = $"https://localhost:7236/api/Product/Delete/{id}";
            var respons = await _HttpClient.DeleteAsync(route);
            respons.EnsureSuccessStatusCode();

            var article = await respons.Content.ReadFromJsonAsync<Boolean>();
            return article;
        }
    }
}
