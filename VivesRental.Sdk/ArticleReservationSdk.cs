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

    public class ArticleReservationSdk
    {
        private HttpClient _HttpClient;

        public ArticleReservationSdk(HttpClient httpClient)
        {
            _HttpClient = httpClient;
        }

        public async Task<List<ArticleReservationResult>> FindAsync(ArticleReservationFilter? filter)
        {
            var route = $"https://localhost:7236/api/ArticleReservation?ArticleId={filter.ArticleId}&CustomerId={filter.CustomerId}";
            var response = await _HttpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();

            var reservations = await response.Content.ReadFromJsonAsync<List<ArticleReservationResult>>();
            if (reservations is null)
            {
                return new List<ArticleReservationResult>();
            }

            return reservations;
        }

        public async Task<ArticleReservationResult> GetAsync(Guid id)
        {
            var route = $"https://localhost:7236/api/ArticleReservation/{id}";
            var response = await _HttpClient.GetAsync(route);
            response.EnsureSuccessStatusCode();

            var reservations = await response.Content.ReadFromJsonAsync<ArticleReservationResult>();

            return reservations;
        }

        public async Task<ArticleReservationResult> CreateAsync(ArticleReservationRequest request)
        {
            var route = "https://localhost:7236/api/ArticleReservation/Create";
            var response = await _HttpClient.PostAsJsonAsync(route, request);
            response.EnsureSuccessStatusCode();

            var article = await response.Content.ReadFromJsonAsync<ArticleReservationResult>();
            return article;
        }

        public async Task<Boolean> Delete(Guid id)
        {
            var route = $"https://localhost:7236/api/ArticleReservation/Delete/{id}";
            var response = await _HttpClient.DeleteAsync(route);
            response.EnsureSuccessStatusCode();

            var article = await response.Content.ReadFromJsonAsync<Boolean>();
            return article;
        }
    }
}
