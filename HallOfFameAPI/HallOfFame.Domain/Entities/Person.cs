namespace HallOfFame.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string? DisplayName { get; set; }

        public List<Skill> Skills { get; set; }
    }
}
