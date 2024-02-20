using HallOfFame.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace HallOfFame.Infrastructure.DTO
{
    public class PersonRequest
    {
        [Required]
        public string Name { get; set; }

        public string? DispayName { get; set; }
        public List<Skill>? Skills { get; set; }
    }
}
