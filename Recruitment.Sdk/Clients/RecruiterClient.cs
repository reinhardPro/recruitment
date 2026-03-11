using System.Net.Http.Json;
using Recruitment.Dto.Requests;
using Recruitment.Dto.Responses;
using Recruitment.Interfaces;

namespace Recruitment.Sdk.Clients
{
    public class RecruiterClient : IRecruiterService
    {
        private readonly HttpClient _httpClient;

        public RecruiterClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<RecruiterResponse>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<RecruiterResponse>>("api/recruiters")
                   ?? Enumerable.Empty<RecruiterResponse>();
        }

        public async Task<RecruiterResponse?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<RecruiterResponse>($"api/recruiters/{id}");
        }

        public async Task<RecruiterResponse> CreateAsync(CreateRecruiterRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/recruiters", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<RecruiterResponse>()
                   ?? throw new InvalidOperationException("The API returned a null response for the created recruiter.");
        }

        public async Task<RecruiterResponse?> UpdateAsync(int id, UpdateRecruiterRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/recruiters/{id}", request);
            if (!response.IsSuccessStatusCode)
                return null;
            return await response.Content.ReadFromJsonAsync<RecruiterResponse>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/recruiters/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
