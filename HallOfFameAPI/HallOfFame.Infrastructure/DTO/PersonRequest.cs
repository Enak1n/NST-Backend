using System.ComponentModel.DataAnnotations;

namespace HallOfFame.Infrastructure.DTO
{
    public class PersonRequest
    {
        [Required]
        public string Name { get; set; }
        public string DispayName { get; set; }
        public string Description { get; set; }


        [Required]
        public List<SkillRequest> Skills { get; set; }
    }
}
