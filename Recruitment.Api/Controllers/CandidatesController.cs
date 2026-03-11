using Microsoft.AspNetCore.Mvc;
using Recruitment.Dto.Requests;
using Recruitment.Dto.Responses;

namespace Recruitment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatesController : ControllerBase
    {
        private static readonly List<CandidateResponse> _candidates = new()
        {
            new CandidateResponse { Id = 1, FirstName = "Alice", LastName = "Johnson", Email = "alice@example.com", Location = "Amsterdam", CreatedAt = new DateTime(2025, 3, 1, 0, 0, 0, DateTimeKind.Utc) },
            new CandidateResponse { Id = 2, FirstName = "Bob", LastName = "Smith", Email = "bob@example.com", Location = "Rotterdam", CreatedAt = new DateTime(2025, 3, 6, 0, 0, 0, DateTimeKind.Utc) }
        };
        private static int _nextId = 3;

        [HttpGet]
        public ActionResult<IEnumerable<CandidateResponse>> GetAll()
        {
            return Ok(_candidates);
        }

        [HttpGet("{id:int}")]
        public ActionResult<CandidateResponse> GetById(int id)
        {
            var candidate = _candidates.FirstOrDefault(c => c.Id == id);
            if (candidate is null)
                return NotFound();
            return Ok(candidate);
        }

        [HttpPost]
        public ActionResult<CandidateResponse> Create([FromBody] CreateCandidateRequest request)
        {
            var candidate = new CandidateResponse
            {
                Id = _nextId++,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                Location = request.Location,
                CreatedAt = DateTime.UtcNow
            };
            _candidates.Add(candidate);
            return CreatedAtAction(nameof(GetById), new { id = candidate.Id }, candidate);
        }

        [HttpPut("{id:int}")]
        public ActionResult<CandidateResponse> Update(int id, [FromBody] UpdateCandidateRequest request)
        {
            var candidate = _candidates.FirstOrDefault(c => c.Id == id);
            if (candidate is null)
                return NotFound();

            candidate.FirstName = request.FirstName;
            candidate.LastName = request.LastName;
            candidate.Email = request.Email;
            candidate.Phone = request.Phone;
            candidate.Location = request.Location;

            return Ok(candidate);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var candidate = _candidates.FirstOrDefault(c => c.Id == id);
            if (candidate is null)
                return NotFound();

            _candidates.Remove(candidate);
            return NoContent();
        }
    }
}
