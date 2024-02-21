namespace HallOfFame.Domain.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; } = 0;
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
