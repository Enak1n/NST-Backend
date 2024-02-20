using CSharpFunctionalExtensions;

namespace HallOfFame.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string? DisplayName { get; set; }

        public ICollection<Skill> Skills { get; set; }
    }
}
