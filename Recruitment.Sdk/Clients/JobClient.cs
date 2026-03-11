using System.Net.Http.Json;
using Recruitment.Dto.Requests;
using Recruitment.Dto.Responses;
using Recruitment.Interfaces;

namespace Recruitment.Sdk.Clients
{
    public class JobClient : IJobService
    {
        private readonly HttpClient _httpClient;

        public JobClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<JobResponse>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<JobResponse>>("api/jobs")
                   ?? Enumerable.Empty<JobResponse>();
        }

        public async Task<JobResponse?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<JobResponse>($"api/jobs/{id}");
        }

        public async Task<IEnumerable<JobResponse>> GetByRecruiterIdAsync(int recruiterId)
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<JobResponse>>($"api/jobs?recruiterId={recruiterId}")
                   ?? Enumerable.Empty<JobResponse>();
        }

        public async Task<JobResponse> CreateAsync(CreateJobRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/jobs", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<JobResponse>()
                   ?? throw new InvalidOperationException("The API returned a null response for the created job.");
        }

        public async Task<JobResponse?> UpdateAsync(int id, UpdateJobRequest request)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/jobs/{id}", request);
            if (!response.IsSuccessStatusCode)
                return null;
            return await response.Content.ReadFromJsonAsync<JobResponse>();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/jobs/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
