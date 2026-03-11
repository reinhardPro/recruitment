using System.Net.Http.Json;
using Recruitment.Dto.Requests;
using Recruitment.Dto.Responses;
using Recruitment.Interfaces;

namespace Recruitment.Sdk.Clients
{
    public class CandidateClient : ICandidateService
    {
        private readonly HttpClient _httpClient;

        public CandidateClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<CandidateResponse>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<CandidateResponse>>("api/candidates")
                   ?? Enumerable.Empty<CandidateResponse>();
        }

        public async Task<CandidateResponse?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<CandidateResponse>($"api/candidates/{id}");
        }

        public async Task<CandidateResponse> CreateAsync(CreateCandidateRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/candidates", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<CandidateResponse>()
                   ?? throw new InvalidOperationException("The API returned a null response for the created candidate.");
        }

        public async Task<CandidateResponse?> UpdateAsync(int id, UpdateCandidateRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/candidates/{id}", request);
            if (!response.IsSuccessStatusCode)
                return null;
            return await response.Content.ReadFromJsonAsync<CandidateResponse>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/candidates/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
