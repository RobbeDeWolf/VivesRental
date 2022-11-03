using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Json;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using VivesRental.Enums;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Sdk
{
    public class ArticleSdk
    {
        private HttpClient _HttpClient;

        public ArticleSdk(HttpClient httpClient)
        {
            _HttpClient = httpClient;
        }

        public async Task<List<ArticleResult>> FindAsync(ArticleFilter? filter)
        {
            var route =
                $"https://localhost:7236/api/Article?ArticleIds={filter.ArticleIds}&ProductId={filter.ProductId}&RentedByCustomerId={filter.RentedByCustomerId}&ReservedByCustomerId={filter.ReservedByCustomerId}&AvailableFromDateTime={filter.AvailableFromDateTime}&AvailableUntilDateTime={filter.AvailableUntilDateTime}&RentedFromDateTime={filter.RentedFromDateTime}&RentedUntilDateTime={filter.RentedUntilDateTime}&ReservedFromDateTime={filter.ReservedFromDateTime}&ReservedUntilDateTime={filter.ReservedUntilDateTime}";
            var response = await _HttpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();

            var articles = await response.Content.ReadFromJsonAsync<List<ArticleResult>>();
            if (articles is null)
            {
                return new List<ArticleResult>();
            }

            return articles;
        }

        public async Task<ArticleResult> GetAsync(Guid id)
        { 
            var route = $"https://localhost:7236/api/Article/{id}";
            var respons = await _HttpClient.GetAsync(route);
            respons.EnsureSuccessStatusCode();

            var article = await respons.Content.ReadFromJsonAsync<ArticleResult>();
            return article;
        }

        public async Task<ArticleResult> CreateAsync(ArticleRequest request)
        {
            var route = $"https://localhost:7236/api/Article/Create";
            var response = await _HttpClient.PostAsJsonAsync(route, request);
            response.EnsureSuccessStatusCode();

            var article = await response.Content.ReadFromJsonAsync<ArticleResult>();
            return article;
        }

        public async Task<ArticleResult> Update(Guid id, ArticleStatus status)
        {
            var route = $"https://localhost:7236/api/Article/Edit/{id}";
            var response = await _HttpClient.PostAsJsonAsync(route, status);
            response.EnsureSuccessStatusCode();

            var article = await response.Content.ReadFromJsonAsync<ArticleResult>();
            return article;
        }

        public async Task<Boolean> Delete(Guid id)
        {
            var route = $"https://localhost:7236/api/Article/Delete/{id}";
            var response = await _HttpClient.DeleteAsync(route);
            response.EnsureSuccessStatusCode();

            var article = await response.Content.ReadFromJsonAsync<Boolean>();
            return article;
        }
    }
}
