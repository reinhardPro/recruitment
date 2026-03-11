using Microsoft.AspNetCore.Mvc;
using Recruitment.Dto.Requests;
using Recruitment.Dto.Responses;

namespace Recruitment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecruitersController : ControllerBase
    {
        private static readonly List<RecruiterResponse> _recruiters = new()
        {
            new RecruiterResponse { Id = 1, FirstName = "Carol", LastName = "White", Email = "carol@talentco.com", Company = "TalentCo", CreatedAt = new DateTime(2025, 2, 9, 0, 0, 0, DateTimeKind.Utc) }
        };
        private static int _nextId = 2;

        [HttpGet]
        public ActionResult<IEnumerable<RecruiterResponse>> GetAll()
        {
            return Ok(_recruiters);
        }

        [HttpGet("{id:int}")]
        public ActionResult<RecruiterResponse> GetById(int id)
        {
            var recruiter = _recruiters.FirstOrDefault(r => r.Id == id);
            if (recruiter is null)
                return NotFound();
            return Ok(recruiter);
        }

        [HttpPost]
        public ActionResult<RecruiterResponse> Create([FromBody] CreateRecruiterRequest request)
        {
            var recruiter = new RecruiterResponse
            {
                Id = _nextId++,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Company = request.Company,
                CreatedAt = DateTime.UtcNow
            };
            _recruiters.Add(recruiter);
            return CreatedAtAction(nameof(GetById), new { id = recruiter.Id }, recruiter);
        }

        [HttpPut("{id:int}")]
        public ActionResult<RecruiterResponse> Update(int id, [FromBody] UpdateRecruiterRequest request)
        {
            var recruiter = _recruiters.FirstOrDefault(r => r.Id == id);
            if (recruiter is null)
                return NotFound();

            recruiter.FirstName = request.FirstName;
            recruiter.LastName = request.LastName;
            recruiter.Email = request.Email;
            recruiter.Company = request.Company;

            return Ok(recruiter);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var recruiter = _recruiters.FirstOrDefault(r => r.Id == id);
            if (recruiter is null)
                return NotFound();

            _recruiters.Remove(recruiter);
            return NoContent();
        }
    }
}
