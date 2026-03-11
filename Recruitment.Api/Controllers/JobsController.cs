using Microsoft.AspNetCore.Mvc;
using Recruitment.Dto.Requests;
using Recruitment.Dto.Responses;

namespace Recruitment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JobsController : ControllerBase
    {
        private static readonly List<JobResponse> _jobs = new()
        {
            new JobResponse { Id = 1, Title = "Software Engineer", Description = "Build and maintain web applications.", Location = "Amsterdam", ExperienceLevel = "Mid", EducationLevel = "Bachelor", RecruiterId = 1, CreatedAt = new DateTime(2025, 3, 4, 0, 0, 0, DateTimeKind.Utc) },
            new JobResponse { Id = 2, Title = "Data Analyst", Description = "Analyse recruitment data and generate insights.", Location = "Utrecht", ExperienceLevel = "Junior", EducationLevel = "Bachelor", RecruiterId = 1, CreatedAt = new DateTime(2025, 3, 8, 0, 0, 0, DateTimeKind.Utc) }
        };
        private static int _nextId = 3;

        [HttpGet]
        public ActionResult<IEnumerable<JobResponse>> GetAll([FromQuery] int? recruiterId)
        {
            var result = recruiterId.HasValue
                ? _jobs.Where(j => j.RecruiterId == recruiterId.Value)
                : _jobs;
            return Ok(result);
        }

        [HttpGet("{id:int}")]
        public ActionResult<JobResponse> GetById(int id)
        {
            var job = _jobs.FirstOrDefault(j => j.Id == id);
            if (job is null)
                return NotFound();
            return Ok(job);
        }

        [HttpPost]
        public ActionResult<JobResponse> Create([FromBody] CreateJobRequest request)
        {
            var job = new JobResponse
            {
                Id = _nextId++,
                Title = request.Title,
                Description = request.Description,
                Location = request.Location,
                ExperienceLevel = request.ExperienceLevel,
                EducationLevel = request.EducationLevel,
                RecruiterId = request.RecruiterId,
                CreatedAt = DateTime.UtcNow
            };
            _jobs.Add(job);
            return CreatedAtAction(nameof(GetById), new { id = job.Id }, job);
        }

        [HttpPut("{id:int}")]
        public ActionResult<JobResponse> Update(int id, [FromBody] UpdateJobRequest request)
        {
            var job = _jobs.FirstOrDefault(j => j.Id == id);
            if (job is null)
                return NotFound();

            job.Title = request.Title;
            job.Description = request.Description;
            job.Location = request.Location;
            job.ExperienceLevel = request.ExperienceLevel;
            job.EducationLevel = request.EducationLevel;

            return Ok(job);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var job = _jobs.FirstOrDefault(j => j.Id == id);
            if (job is null)
                return NotFound();

            _jobs.Remove(job);
            return NoContent();
        }
    }
}
