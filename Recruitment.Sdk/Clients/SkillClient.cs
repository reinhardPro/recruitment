using System.Net.Http.Json;
using Recruitment.Dto.Requests;
using Recruitment.Dto.Responses;
using Recruitment.Interfaces;

namespace Recruitment.Sdk.Clients
{
    public class SkillClient : ISkillService
    {
        private readonly HttpClient _httpClient;

        public SkillClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<SkillResponse>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<SkillResponse>>("api/skills")
                   ?? Enumerable.Empty<SkillResponse>();
        }

        public async Task<SkillResponse?> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<SkillResponse>($"api/skills/{id}");
        }

        public async Task<SkillResponse> CreateAsync(CreateSkillRequest request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/skills", request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<SkillResponse>()
                   ?? throw new InvalidOperationException("The API returned a null response for the created skill.");
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/skills/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
