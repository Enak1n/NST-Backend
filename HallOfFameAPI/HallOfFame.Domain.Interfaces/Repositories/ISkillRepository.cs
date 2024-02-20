using HallOfFame.Domain.Entities;

namespace HallOfFame.Domain.Interfaces.Repositories
{
    public interface ISkillRepository : IGenericRepository<Skill>
    {
        Task EditAsync(long id, string name, string description, byte level);
    }
}
