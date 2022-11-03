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
    public class OrderSdk
    {
        private HttpClient _HttpClient;

        public OrderSdk(HttpClient httpClient)
        {
            _HttpClient = httpClient;
        }
        public async Task<List<OrderResult>> FindAsync(OrderFilter? filter)
        {
            var route = $"https://localhost:7236/api/Order?CustomerId={filter.CustomerId}&Search={filter.Search}";
            var response = await _HttpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();

            var articles = await response.Content.ReadFromJsonAsync<List<OrderResult>>();
            if (articles is null)
            {
                return new List<OrderResult>();
            }

            return articles;
        }

        public async Task<OrderResult> GetAsync(Guid id)
        {
            var route = $"https://localhost:7236/api/Order/{id}";
            var respons = await _HttpClient.GetAsync(route);
            respons.EnsureSuccessStatusCode();

            var article = await respons.Content.ReadFromJsonAsync<OrderResult>();
            return article;
        }

        public async Task<OrderResult> CreateAsync(Guid CustomerId)
        {
            var route = $"https://localhost:7236/api/Order/Create";
            var response = await _HttpClient.PostAsJsonAsync(route, CustomerId);
            response.EnsureSuccessStatusCode();

            var article = await response.Content.ReadFromJsonAsync<OrderResult>();
            return article;
        }

        public async Task<Boolean> Return(Guid orderId, DateTime returnedAt)
        {
            var route = $"https://localhost:7236/api/Order/Return?orderId={orderId}&returnedAt={returnedAt}";
            var respons = await _HttpClient.GetAsync(route);
            respons.EnsureSuccessStatusCode();

            var article = await respons.Content.ReadFromJsonAsync<Boolean>();
            return article;
        }
    }
}
