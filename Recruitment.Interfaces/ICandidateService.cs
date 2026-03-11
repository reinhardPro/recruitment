using Recruitment.Dto.Requests;
using Recruitment.Dto.Responses;

namespace Recruitment.Interfaces
{
    public interface ICandidateService
    {
        Task<IEnumerable<CandidateResponse>> GetAllAsync();
        Task<CandidateResponse?> GetByIdAsync(int id);
        Task<CandidateResponse> CreateAsync(CreateCandidateRequest request);
        Task<CandidateResponse?> UpdateAsync(int id, UpdateCandidateRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
