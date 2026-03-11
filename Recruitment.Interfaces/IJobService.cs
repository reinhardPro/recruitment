using Recruitment.Dto.Requests;
using Recruitment.Dto.Responses;

namespace Recruitment.Interfaces
{
    public interface IJobService
    {
        Task<IEnumerable<JobResponse>> GetAllAsync();
        Task<JobResponse?> GetByIdAsync(int id);
        Task<IEnumerable<JobResponse>> GetByRecruiterIdAsync(int recruiterId);
        Task<JobResponse> CreateAsync(CreateJobRequest request);
        Task<JobResponse?> UpdateAsync(int id, UpdateJobRequest request);
        Task<bool> DeleteAsync(int id);
    }
}
