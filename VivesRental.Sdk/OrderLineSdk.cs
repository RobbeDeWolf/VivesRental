using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Vives.Services.Model;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Results;

namespace VivesRental.Sdk
{
    public class OrderLineSdk
    {
        private HttpClient _HttpClient;

        public OrderLineSdk(HttpClient httpClient)
        {
            _HttpClient = httpClient;
        }

        public async Task<List<OrderLineResult>> FindAsync(OrderLineFilter? filter)
        {
            var route = $"https://localhost:7236/api/OrderLine?OrderId={filter}";
            var response = await _HttpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();

            var articles = await response.Content.ReadFromJsonAsync<List<OrderLineResult>>();
            if (articles is null)
            {
                return new List<OrderLineResult>();
            }

            return articles;
        }

        public async Task<OrderLineResult> GetAsync(Guid id)
        {
            var route = $"https://localhost:7236/api/OrderLine/{id}";
            var respons = await _HttpClient.GetAsync(route);
            respons.EnsureSuccessStatusCode();

            var article = await respons.Content.ReadFromJsonAsync<OrderLineResult>();
            return article;
        }
        public async Task<ServiceResult> RentAsync(Guid orderid, Guid articleId)
        {
            var route = $"https://localhost:7236/api/OrderLine/Rent?orderid={orderid}&articleId={articleId}";
            var respons = await _HttpClient.GetAsync(route);
            respons.EnsureSuccessStatusCode();

            var article = await respons.Content.ReadFromJsonAsync<ServiceResult>();
            return article;
        }

        public async Task<ServiceResult> RentListAsync(Guid orderid, IList<Guid> articleIds)
        {
            var route = $"https://localhost:7236/api/OrderLine/RentList?orderId={orderid}";
            var respons = await _HttpClient.PostAsJsonAsync(route, articleIds);
            respons.EnsureSuccessStatusCode();

            var article = await respons.Content.ReadFromJsonAsync<ServiceResult>();
            return article;
        }

        public async Task<ServiceResult> Return(Guid orderlineId, DateTime returnedAt)
        {
            var route = $"https://localhost:7236/api/Order/Return?orderId={orderlineId}&returnedAt={returnedAt}";
            var respons = await _HttpClient.GetAsync(route);
            respons.EnsureSuccessStatusCode();

            var article = await respons.Content.ReadFromJsonAsync<ServiceResult>();
            return article;
        }
    }
}
