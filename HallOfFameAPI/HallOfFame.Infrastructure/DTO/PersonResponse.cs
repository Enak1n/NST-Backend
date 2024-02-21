namespace HallOfFame.Infrastructure.DTO
{
    public class PersonResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public string DisplayName { get; set; }
        public string? Description { get; set; }
        public List<SkillResponse> Skills { get; set; }
    }
}
