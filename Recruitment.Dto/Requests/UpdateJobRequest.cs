using System.ComponentModel.DataAnnotations;

namespace Recruitment.Dto.Requests
{
    public class UpdateJobRequest
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(2000)]
        public string? Description { get; set; }

        [MaxLength(200)]
        public string? Location { get; set; }

        [MaxLength(100)]
        public string? ExperienceLevel { get; set; }

        [MaxLength(100)]
        public string? EducationLevel { get; set; }
    }
}
