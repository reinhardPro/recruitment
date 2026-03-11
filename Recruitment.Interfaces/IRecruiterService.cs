using Recruitment.Dto.Requests;
using Recruitment.Dto.Responses;

namespace Recruitment.Interfaces
{
    public interface IRecruiterService
    {
        Task<IEnumerable<RecruiterResponse>> GetAllAsync();
        Task<RecruiterResponse?> GetByIdAsync(int id);
        Task<RecruiterResponse> CreateAsync(CreateRecruiterRequest request);
        Task<RecruiterResponse?> UpdateAsync(int id, UpdateRecruiterRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
