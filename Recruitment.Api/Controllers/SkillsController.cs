using Microsoft.AspNetCore.Mvc;
using Recruitment.Dto.Requests;
using Recruitment.Dto.Responses;

namespace Recruitment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillsController : ControllerBase
    {
        private static readonly List<SkillResponse> _skills = new()
        {
            new SkillResponse { Id = 1, SkillName = "C#", Category = "Programming" },
            new SkillResponse { Id = 2, SkillName = "SQL", Category = "Database" },
            new SkillResponse { Id = 3, SkillName = "Azure", Category = "Cloud" }
        };
        private static int _nextId = 4;

        [HttpGet]
        public ActionResult<IEnumerable<SkillResponse>> GetAll()
        {
            return Ok(_skills);
        }

        [HttpGet("{id:int}")]
        public ActionResult<SkillResponse> GetById(int id)
        {
            var skill = _skills.FirstOrDefault(s => s.Id == id);
            if (skill is null)
                return NotFound();
            return Ok(skill);
        }

        [HttpPost]
        public ActionResult<SkillResponse> Create([FromBody] CreateSkillRequest request)
        {
            var skill = new SkillResponse
            {
                Id = _nextId++,
                SkillName = request.SkillName,
                Category = request.Category
            };
            _skills.Add(skill);
            return CreatedAtAction(nameof(GetById), new { id = skill.Id }, skill);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var skill = _skills.FirstOrDefault(s => s.Id == id);
            if (skill is null)
                return NotFound();

            _skills.Remove(skill);
            return NoContent();
        }
    }
}
