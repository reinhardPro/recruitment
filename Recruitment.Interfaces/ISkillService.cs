using Recruitment.Dto.Requests;
using Recruitment.Dto.Responses;

namespace Recruitment.Interfaces
{
    public interface ISkillService
    {
        Task<IEnumerable<SkillResponse>> GetAllAsync();
        Task<SkillResponse?> GetByIdAsync(int id);
        Task<SkillResponse> CreateAsync(CreateSkillRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
